Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.Owin

Partial Public Class ManageLogins
    Inherits System.Web.UI.Page

    Protected Property SuccessMessage() As String
        Get
            Return m_SuccessMessage
        End Get
        Private Set
            m_SuccessMessage = Value
        End Set
    End Property
    Private m_SuccessMessage As String

    Protected Property CanRemoveExternalLogins() As Boolean
        Get
            Return m_CanRemoveExternalLogins
        End Get
        Private Set
            m_CanRemoveExternalLogins = Value
        End Set
    End Property
    Private m_CanRemoveExternalLogins As Boolean

    Private Function HasPassword(manager As ApplicationUserManager) As Boolean
        Return manager.HasPassword(User.Identity.GetUserId())
    End Function

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim cn As New clsSelectDataLoader
        Dim ds As DataSet
        If Not IsPostBack Then
            txtID.Attributes.Add("readonly", "readonly")

            PopulateGrid()
        End If
        ds = cn.LoadAppRoles
            If ds.Tables.Count > 0 Then
                Dim chk As CheckBox
                Dim dt As DataTable = ds.Tables(0)
                'chkRoles.Items.Clear()
                Dim liCounter As Integer
                For liCounter = 0 To dt.Rows.Count - 1
                    chk = New CheckBox
                    chk.Text = dt(liCounter)(0).ToString
                    chk.ID = dt(liCounter)(0).ToString
                    chk.CssClass = "chk"
                    chk.Attributes.Add("name", dt(liCounter)(0).ToString)
                    pnlRoles.Controls.Add(chk)
                    ' chkRoles.Items.Add(dt(liCounter)(0).ToString)
                Next
            End If

        grdUsers.DataBind()


    End Sub
    Sub PopulateGrid()
        Dim cn As New clsSelectDataLoader
        Dim ds As DataSet
        ds = cn.LoadAppUsers("")
        grdUsers.DataSource = ds
        If Not ds Is Nothing Then
            If ds.Tables.Count > 0 Then
                '   grdUsers.DataBind()
            End If
        End If



    End Sub
    Protected Sub deletethis(sender As Object, e As EventArgs)
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim lsUserID As String = User.Identity.GetUserId()
        If lsUserID Is Nothing Then
            lsUserID = ""
        Else


            CanRemoveExternalLogins = manager.GetLogins(lsUserID).Count() > 1
        End If

        SuccessMessage = String.Empty
        SuccessMessagePlaceholder.Visible = Not String.IsNullOrEmpty(SuccessMessage)
    End Sub

    Public Function GetUsers() As Collection
        GetUsers = New Collection
        Dim ds As DataSet
        Dim cn As New clsSelectDataLoader
        Dim u As clsUser
        Dim dt As DataTable
        Dim dr As DataRow
        Dim liRow As Integer
        ds = cn.LoadAppUsers()

        If ds.Tables.Count > 0 Then
            dt = ds.Tables(0)
            Do While liRow < dt.Rows.Count

                u = New clsUser
                dr = dt.Rows(liRow)
                u.UserFriendlyName = dr("UserFriendlyName")
                u.Email = dr("email")
                u.ID = dr("ID")
                u.UserName = dr("UserName")
                liRow = liRow + 1
                GetUsers.Add(u)
            Loop
            Return GetUsers

        End If
        Return GetUsers

        Dim lsUserID As String = User.Identity.GetUserId()
        If Not lsUserID Is Nothing Then
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()


            Dim accounts = manager.GetLogins(lsUserID)
            CanRemoveExternalLogins = accounts.Count() > 1 OrElse HasPassword(manager)
            Return accounts
        End If
        Return Nothing
    End Function

    Public Sub RemoveLogin(loginProvider As String, providerKey As String)
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim signInManager = Context.GetOwinContext().Get(Of ApplicationSignInManager)()
        Dim result = manager.RemoveLogin(User.Identity.GetUserId(), New UserLoginInfo(loginProvider, providerKey))

        Dim msg As String = String.Empty
        If result.Succeeded Then
            Dim userInfo = manager.FindById(User.Identity.GetUserId())
            signInManager.SignIn(userInfo, isPersistent:=False, rememberBrowser:=False)
            msg = "?m=RemoveLoginSuccess"
        End If
        Response.Redirect("~/Account/ManageLogins" & msg)
    End Sub
    Async Function setPassord(Optional lsUserID As String = "11111111-2222-3333-4444-555566667777") As Threading.Tasks.Task(Of String)

        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()


        Dim token As String = Await manager.GeneratePasswordResetTokenAsync(lsUserID)

        Dim resul As Object = Await manager.ResetPasswordAsync(lsUserID, token, txtPassword.Text)
        Return "Success"
    End Function




    Protected Sub OnRowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdUsers.RowDataBound
        Dim ID As String
        If e.Row.RowType = DataControlRowType.DataRow Then
          '  e.Row.Attributes("onclick") = Page.ClientScript.GetPostBackClientHyperlink(grdUsers, "Select$" & e.Row.RowIndex)
            e.Row.ToolTip = "Click to select this row."
            ' e.Row.Attributes("data-target") = "#UserModal"
            ' e.Row.Attributes("data-toggle") = "modal"
            For Each cell As DataControlFieldCell In e.Row.Cells
                e.Row.Attributes("data-" & Replace(cell.ContainingField.ToString, " ", "")) = Trim(cell.Text)
                If cell.ContainingField.ToString = "ID" Then
                    ID = Trim(cell.Text)
                End If
            Next

            Dim cn As New clsSelectDataLoader
            Dim ds As DataSet
            If ID.Length = 36 Then
                Dim lsRoles As String = ""
                ds = cn.LoadAppUserRoles(ID)
                If ds.Tables.Count > 0 Then
                    Dim dt As DataTable
                    dt = ds.Tables(0)
                    For Each dr As DataRow In dt.Rows
                        lsRoles = lsRoles & dr(1).ToString & ","
                    Next
                    e.Row.Attributes("data-Roles") = lsRoles
                End If
            End If
        End If
    End Sub

    Protected Sub grdUsers_DataBinding(sender As Object, e As EventArgs) Handles grdUsers.DataBinding
        PopulateGrid()
    End Sub

    Protected Sub cmdSubmit_Click(sender As Object, e As EventArgs) Handles cmdSubmit.Click
        Dim lsEmail, lsUserName, lsFriendlyName, lsID, lsNode As String
        Dim liNodeID As Integer = CInt(txtNodeID.Text)
        Dim Roles() As String
        Dim chk As CheckBox
        lsEmail = txtEmail.Text
        lsUserName = txtUserName.Text
        lsID = txtID.Text
        lsNode = txtNodeID.Text
        lsFriendlyName = txtFriendlyName.Text
        Dim cn As New clsSelectDataLoader
        cn.UpdateAppUsers(lsFriendlyName, lsEmail, lsUserName, lsNode, lsID)
        For Each ctrl As Control In pnlRoles.Controls
            If TypeOf ctrl Is CheckBox Then
                chk = ctrl
                If chk.Checked Then
                    cn.InsertAppUserRoles(lsID, chk.ID)
                Else
                    cn.DeleteAppUserRoles(lsID, ctrl.ID)
                End If
            End If
        Next
        grdUsers.DataBind()

    End Sub

    Protected Sub cmdResetPassword_Click(sender As Object, e As EventArgs) Handles cmdResetPassword.Click
        Dim unused = setPassord(txtID.Text)
    End Sub
End Class

