Imports System.Web.Optimization

Public Class Global_asax
    Inherits HttpApplication

    Sub Application_Start(sender As Object, e As EventArgs)
        ' Fires when the application is started
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
    Sub Session_OnStart()
        'Make sure that new users start on the correct
        'page of the ASP application.

        'Replace the value given to startPage below
        'with the virtual path to your application's
        'start page.
        Dim startPage As String
        startPage = "/Default.aspx"
        Dim lsUserID As String = System.Threading.Thread.CurrentPrincipal.Identity.Name 'web.HttpContext.Current.User.Identity.GetUserId()
        If lsUserID <> "" Then
            ' Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            '   Dim user = manager.FindByName(Email.Text)
            '  Dim cn As New clsSelectDataLoader
            ' cn.LoadPersonalData("Project", 1)
            'Session("NodeID") = user.nodeid
        End If
        Dim llNodeID As Long = 2
        ' currentPage = Request.ServerVariables("SCRIPT_NAME")

        'Do a case-insensitive comparison, and if they
        'don't match, send the user to the start page.


    End Sub
End Class