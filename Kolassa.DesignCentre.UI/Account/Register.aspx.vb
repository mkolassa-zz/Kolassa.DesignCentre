Imports System
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Owin

Partial Public Class Register
    Inherits Page
    Protected Sub CreateUser_Click(sender As Object, e As EventArgs)
		Dim userName As String = Email.Text
		Dim userFriendlyName As String = txtUserFriendlyName.text
		Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim signInManager = Context.GetOwinContext().Get(Of ApplicationSignInManager)()
		Dim user = New ApplicationUser() With {.UserName = userName, .Email = userName, .UserFriendlyName = userFriendlyName}
		Dim result = manager.Create(user, Password.Text)
		If result.Succeeded Then
			Session("UserFriendlyName") = ""
			' For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
			' Dim code = manager.GenerateEmailConfirmationToken(user.Id)
			' Dim callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request)
			' manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=""" & callbackUrl & """>here</a>.")

			'*** SendGrid Code
			Dim code As String = manager.GenerateEmailConfirmationToken(user.Id)
			Dim callbackUrl As String = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request)
			manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + " \ ">here</a>.")
			'*** SendGrid Code END

			If user.EmailConfirmed Then
				signInManager.SignIn(user, isPersistent:=False, rememberBrowser:=False)
				IdentityHelper.RedirectToReturnUrl(Request.QueryString("ReturnUrl"), Response)
			Else
				ErrorMessage.Text = "An email has been sent to your account.  Please view the email and confirm your account to complete the registration process"
			End If
		Else
				ErrorMessage.Text = result.Errors.FirstOrDefault()
        End If
    End Sub
End Class

