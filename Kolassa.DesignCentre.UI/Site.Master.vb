Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Public Class SiteMaster
    Inherits MasterPage
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String

    Protected Sub Page_Init(sender As Object, e As EventArgs)
        ' The code below helps to protect against XSRF attacks
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                 .HttpOnly = True,
                 .Value = _antiXsrfTokenValue
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)

        Else
            ' Validate the Anti-XSRF token
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '*** Get The User ID
        '*** Get the Current NodeID
        '*** Get the Current Selected Project

        '*** Are we coming from a new project selection? If so, its in the querystring
        Dim lsProject As String = Request.QueryString("ProjectId")
        If lsProject = "" Then lsProject = Session("Project")
        Dim cp As New clsPersonalData
        Dim a As New GlobalFunctionsDC
        Dim lsLast As String

        If Session("NodeID") Is Nothing Then
            ' Response.Redirect("Login.aspx")
            Session("NodeID") = 0
        End If
        txtnodeid.Text = Session("NodeID")
        '*** Get all the current User Info
        Dim context = New ApplicationDbContext()
        ' Create an instance of the generic type UserStore(Of T), with
        ' ApplicationUser as the type parameter, T.
        ' The type of userStore is therefore UserStore(Of ApplicationUser).
        ' context is passed as a parameter to the constructor.
        Dim userStore = New UserStore(Of ApplicationUser)(context)
        ' Create an instance of the generic type UserManager(Of T), with
        ' ApplicationUser as the type parameter, T.
        ' The type of myUserManager is therefore UserManager(Of ApplicationUser).
        ' userStore is passed to the constructor as a parameter
        Dim myUserManager = New UserManager(Of ApplicationUser)(userStore)

        Dim u As New ApplicationUser
        Dim lsUser = Web.HttpContext.Current.User.Identity.GetUserName
        u = myUserManager.FindByName(lsUser)




        '*** If u is nothing, No user logged in.  Get Out of here.
        If u Is Nothing Then Exit Sub
        'Dim ac As application
        Dim uc As New IdentityUserClaim
        uc.ClaimType = "RealName"
        uc.ClaimValue ="Mike"
        Dim sc As New System.Security.Claims.Claim(uc.ClaimType, uc.ClaimValue)


        u.Claims.Add(uc)
        myUserManager.AddClaimAsync(lsUser, sc)
        Dim nr As New IdentityUserRole
        Dim lbAdmin As Boolean = False
        For Each r As IdentityUserRole In u.Roles
            Debug.Print(r.GetType.ToString)
            If r.RoleId = "Admin" Then
                lbAdmin = True
                pnlNodeChange.Visible = True
            End If
        Next

        If Session("NodeID") = 0 Then
            Session("NodeID") = u.NodeID
            Session("UserEmail") = u.Email
        End If
        If lsUser <> "" Then
            If Not Me.IsPostBack Then
                '*** Set the Sesion Variables for the User if they got lost
                If Session("UserFriendlyName") Is Nothing Or Session("UserFriendlyName") = "" Then
                    If u.UserFriendlyName Is Nothing Or u.UserFriendlyName = "" Then
                        Session("UserFriendlyName") = Web.HttpContext.Current.User.Identity.GetUserName
                    Else
                        Session("UserFriendlyName") = u.UserFriendlyName
                    End If
                End If
                Dim ltasks As New clsTasks
                Dim lsUserID As String = HttpContext.Current.User.Identity.GetUserId
                Dim liNodeID As Integer = Session("NodeID")

                ltasks.GetRecords("", "", "", liNodeID, lsUserID)
                Session("TaskCount") = ltasks.ObjectCount

            End If
            '*** Ask the Personal Data Object (Activity Log) for the Last Project Selected
            cp.getlastValue("Project")
                lsLast = cp.GuidValue

                '*** Was there a project ID in the querystring? Iff Not we need to get it from the log
                If lsProject Is Nothing Or lsProject = "" Then
                    If Session("ProjectName") = "" Or Session("ProjectName") Is Nothing Then
                        '*** Load Project from Database from personal data table for last Project
                        If a.isGUIDString(lsLast) Then lsProject = lsLast
                    Else
                        '*** We still have all the data in the Session variables
                        Exit Sub
                    End If
                Else
                    If a.isGUIDString(lsProject) Then
                        cp = New clsPersonalData
                        cp.GuidValue = lsProject
                        cp.UsageType = "Project"
                        cp.Insert()
                    End If
                End If


                If Not a.isGUIDString(lsProject) Then lsProject = ""
                Dim lsCurrentProj As String = Session("Project")
                If Session("Project") Is Nothing Then lsCurrentProj = ""
                If lsProject Is Nothing Then lsProject = ""
                If lsProject = "" Then
                    Exit Sub
                End If
                If Session("Project") = lsProject Then
                    Exit Sub
                End If
                Session("Project") = lsProject

                Dim c = New clsProject
                c.ID = lsProject
                c.GetRecord(lsProject, True)
                Session("ProjectObject") = c
                Session("ProjectName") = c.Name
            End If



    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
    End Sub

    Protected Sub SetNode(sender As Object, e As EventArgs) Handles btnNode.Click
        Dim lsNodeClient As String = Replace(txtnodeid.ClientID, "_", "$")
        If IsPostBack Then
            Dim lsNodeID As String = Request.Form("txtnodeid")
            For i = 1 To Request.Form().Count - 1
                lsNodeID = Request.Form.GetKey(i)
                If InStr(lsNodeID, lsNodeClient) > 0 Then
                    lsNodeID = Request.Form(i)
                    Exit For
                End If
            Next

            If Session("NodeID") <> lsNodeID Then
                Session("NodeID") = lsNodeID
                Session("ProjectObject") = Nothing
                Session("ProjectName") = Nothing
                Session("Project") = Nothing
            End If
        End If
    End Sub
End Class

