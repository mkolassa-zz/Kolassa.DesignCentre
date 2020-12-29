Imports System.Web
Imports System.Web.UI
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin.Security
Imports Owin

Partial Public Class Login
    Inherits Page
	Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
		RegisterHyperLink.NavigateUrl = "Register"
        ' Enable this once you have account confirmation enabled for password reset functionality
        ForgotPasswordHyperLink.NavigateUrl = "Forgot"
        OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")
		Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
		If Not [String].IsNullOrEmpty(returnUrl) Then
			RegisterHyperLink.NavigateUrl += "?ReturnUrl=" & returnUrl
		End If
	End Sub

    Protected Sub LogIn(sender As Object, e As EventArgs)
        Try
            If IsValid Then
                ' Validate the user password
                Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
                Dim signinManager = Context.GetOwinContext().GetUserManager(Of ApplicationSignInManager)()

                '***        // Require the user to have a confirmed email before they can log on.
                Dim user = manager.FindByName(Email.Text)
                If Not (user Is Nothing) Then
                    If Not (user.EmailConfirmed) Then
                        FailureText.Text = "Invalid login attempt. You must have a confirmed email address. Enter your email and password, then press 'Resend Confirmation'."
                        ErrorMessage.Visible = True
                        ResendConfirm.Visible = True
                    Else


                        ' This doen't count login failures towards account lockout
                        ' To enable password failures to trigger lockout, change to shouldLockout := True
                        Dim result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout:=False)

                        Select Case result
                            Case SignInStatus.Success
                                Session("NodeID") = user.NodeID
                                Session("UserFriendlyName") = ""
                                IdentityHelper.RedirectToReturnUrl("../frmProjects", Response) 'Request.QueryString("ReturnUrl"), Response)
                                Exit Select
                            Case SignInStatus.LockedOut
                                Response.Redirect("/Account/Lockout")
                                Exit Select
                            Case SignInStatus.RequiresVerification
                                Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                                                                Request.QueryString("ReturnUrl"),
                                                                RememberMe.Checked),
                                                  True)
                                Exit Select
                            Case Else
                                FailureText.Text = "Invalid login attempt"
                                ErrorMessage.Visible = True
                                Exit Select
                        End Select
                    End If
                End If
            End If
        Catch
            Dim lsErr As String = Err.Description
            If Not Err.GetException.InnerException Is Nothing Then
                lsErr = lsErr & Err.GetException.InnerException.ToString
            End If
            Debug.Print(lsErr)
        End Try
    End Sub
    Protected Sub SendEmailConfirmationToken(sender As Object, e As EventArgs)
        Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
        Dim user = manager.FindByName(Email.Text)
        If Not (User Is Nothing) Then

            If Not (User.EmailConfirmed) Then

                Dim code As String = manager.GenerateEmailConfirmationToken(User.Id)
                Dim callbackUrl As String = IdentityHelper.GetUserConfirmationRedirectUrl(code, User.Id, Request)
                manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=""" & callbackUrl & """ >here</a>.")

                FailureText.Text = "Confirmation email sent. Please view the email and confirm your account."
                ErrorMessage.Visible = True
                ResendConfirm.Visible = False
            End If
        End If
    End Sub
End Class
