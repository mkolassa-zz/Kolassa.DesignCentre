﻿Imports System
Imports System.Web
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Owin

Partial Public Class ForgotPassword
    Inherits System.Web.UI.Page

    Protected Property StatusMessage() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Protected Sub Forgot(sender As Object, e As EventArgs)
        If IsValid Then
            ' Validate the user's email address
            Dim manager = Context.GetOwinContext().GetUserManager(Of ApplicationUserManager)()
            Dim user As ApplicationUser = manager.FindByName(Email.Text)
            If user Is Nothing OrElse Not manager.IsEmailConfirmed(user.Id) Then
                FailureText.Text = "The user either does not exist or is not confirmed."
                ErrorMessage.Visible = True
                Return
            End If
            ' For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
            ' Send email with the code and the redirect to reset password page
            '*** Next 3 lines were commented out to bypass forgot
            Dim code = manager.GeneratePasswordResetToken(user.Id)
            Dim callbackUrl = IdentityHelper.GetResetPasswordRedirectUrl(code, Request)
            Dim sURL = Net.WebUtility.HtmlEncode(callbackUrl)
            Dim body As String = "Please reset your password by clicking <a href='" + callbackUrl + "'>here</a><br />" '& sURL '& Net.WebUtility.HtmlEncode(callbackUrl)
            '"Please reset your password by clicking " & callbackUrl
            'Net.WebUtility.HtmlEncode(callbackUrl)
            'manager.SendEmailAsync(user.Id, "Reset Password", body)
            SendEmail(Email.Text, body)
            loginForm.Visible = False
            DisplayEmail.Visible = True
        End If
    End Sub
    Public Sub SendEmail(lsTo As String, lsBody As String)

        Dim email As New clsEmail
        email.strMailTo = lsTo

        Try
            email.SendNewCustomerEmail(lsBody, "Password Reset from DesignCentre")
        Catch e As Exception
            FailureText.Text = e.Message
        End Try

    End Sub
End Class