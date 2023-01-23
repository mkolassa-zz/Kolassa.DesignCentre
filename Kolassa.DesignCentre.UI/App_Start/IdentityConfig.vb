Imports System.Security.Claims
Imports System.Threading.Tasks
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports Microsoft.Owin
Imports Microsoft.Owin.Security
'Imports SendGrid SAYS UNNECESSARY
Imports System.Net.Mail
Imports System.Net
Imports System.Configuration
Imports System.Diagnostics

Public Class EmailService
    Implements IIdentityMessageService
    '   Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
    ' Plug in your email service here to send an email.
    '  Return Task.FromResult(0)
    ' End Function
    Public Async Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        Await configSendGridasync(message) ' Fixes 
    End Function

    '*** Use NuGet to install SendGrid (Basic C# client lib) 
    Private Function configSendGridasync(message As IdentityMessage) As Task '*** Used tobe async
        'SendEmail()
        Dim msg As String = ""
        ' Dim myMessage As SendGrid.SendGridMessage
        ' myMessage = New SendGrid.SendGridMessage()
        Try

            Dim myMessage As MailMessage = New MailMessage()
            myMessage.To.Add(message.Destination)
            myMessage.From = New System.Net.Mail.MailAddress("michael.may@addisonstuart.com", "Addison Stuart")
            myMessage.Subject = message.Subject
            myMessage.Body = message.Body
            Dim smtp As SmtpClient = New SmtpClient()
            smtp.Host = "mail.addisonstuart.com" '"mail5008.site4now.net" '
            smtp.Port = "8889" '"465" '25  "465" '
            smtp.UseDefaultCredentials = False
            Dim lsUserName, lsPassword As String
            lsUserName = ConfigurationManager.AppSettings("emailServiceUserName")
            lsPassword = ConfigurationManager.AppSettings("emailServicePassword")
            smtp.Credentials = New System.Net.NetworkCredential(
                                lsUserName,
                               lsPassword)
            smtp.EnableSsl = False 'True

            smtp.Send(myMessage)
            'lblmsg.Text = "Mail Send ......."

        Catch ex As Exception
            msg = ex.Message
            msg = msg & "!"
        End Try
        msg = msg & "!"


        '  Dim myMessage As sendgridmessage = New SendGridMessage()
        '   myMessage.AddTo(message.Destination)
        ' myMessage.From = New System.Net.Mail.MailAddress("michael.may@addisonstuart.com", "Addison Stuart")
        ' myMessage.Subject = message.Subject
        ' myMessage.Text = message.Body
        ' myMessage.Html = message.Body

        ' Dim credentials As NetworkCredential = New NetworkCredential(
        ' ConfigurationManager.AppSettings("emailServiceUserName"),
        ' ConfigurationManager.AppSettings("emailServicePassword")
        '         )

        '  '*** Create a Web transport for sending email.
        '  Dim transportWeb As Web = New Web(credentials)

        '  '*** Send the email.
        '  If Not (transportWeb Is Nothing) Then
        '  Await transportWeb.DeliverAsync(myMessage)
        '  Else
        ' Trace.TraceError("Failed to create Web transport.")
        'Await Task.FromResult(0)
        ' End If
        Return New Task(New Action(AddressOf Test))
    End Function
    Sub Test()
    End Sub
    Sub SendEmail()
        Dim lsmsg As String
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)

        Dim strFrom = "michael.may@addisonstuart.com"  ''IMPORTANT: This must be same as your smtp authentication address.
        Dim strTo = "mkolassa@gmail.com"
        Dim MailMsg As New MailMessage(New MailAddress(strFrom.Trim()), New MailAddress(strTo))
        MailMsg.BodyEncoding = Encoding.Default
        MailMsg.Subject = "This is a test"
        MailMsg.Body = "This is a sample message using SMTP authentication"
        MailMsg.Priority = MailPriority.High
        MailMsg.IsBodyHtml = True
        'Smtpclient to send the mail message 

        Dim SmtpMail As New SmtpClient
        Dim basicAuthenticationInfo As New System.Net.NetworkCredential("michael.may@addisonstuart.com", "IPsum238$")

        ''IMPORANT:  Your smtp login email MUST be same as your FROM address.

        SmtpMail.Host = "mail.addisonstuart.com"
        SmtpMail.UseDefaultCredentials = False
        SmtpMail.Credentials = basicAuthenticationInfo
        SmtpMail.Port = 8889 '25  '    //alternative port number Is 8889
        SmtpMail.EnableSsl = False

        SmtpMail.Send(MailMsg)
        lsmsg = "Mail Sent"
    End Sub
End Class

Public Class SmsService
    Implements IIdentityMessageService
    Public Function SendAsync(message As IdentityMessage) As Task Implements IIdentityMessageService.SendAsync
        ' Plug in your SMS service here to send a text message.
        Return Task.FromResult(0)
    End Function

End Class

' Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
Public Class ApplicationUserManager
    Inherits UserManager(Of ApplicationUser)
    Public Sub New(store As IUserStore(Of ApplicationUser))
        MyBase.New(store)
    End Sub

    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationUserManager), context As IOwinContext) As ApplicationUserManager
        Dim manager = New ApplicationUserManager(New UserStore(Of ApplicationUser)(context.[Get](Of ApplicationDbContext)()))
        ' Configure validation logic for usernames
        manager.UserValidator = New UserValidator(Of ApplicationUser)(manager) With {
        .AllowOnlyAlphanumericUserNames = False,
        .RequireUniqueEmail = True
                }

        ' Configure validation logic for passwords
        manager.PasswordValidator = New PasswordValidator() With {
        .RequiredLength = 6,
        .RequireNonLetterOrDigit = True,
        .RequireDigit = True,
        .RequireLowercase = True,
        .RequireUppercase = True
                }
        ' Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user. 
        ' You can write your own provider and plug in here.
        manager.RegisterTwoFactorProvider("Phone Code", New PhoneNumberTokenProvider(Of ApplicationUser)() With {
        .MessageFormat = "Your security code is {0}"
                })
        manager.RegisterTwoFactorProvider("Email Code", New EmailTokenProvider(Of ApplicationUser)() With {
              .Subject = "Security Code",
        .BodyFormat = "Your security code is {0}"
                })

        ' Configure user lockout defaults
        manager.UserLockoutEnabledByDefault = True
        manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5)
        manager.MaxFailedAccessAttemptsBeforeLockout = 5

        manager.EmailService = New EmailService()
        manager.SmsService = New SmsService()
        Dim dataProtectionProvider = options.DataProtectionProvider
        If dataProtectionProvider IsNot Nothing Then
            manager.UserTokenProvider = New DataProtectorTokenProvider(Of ApplicationUser)(dataProtectionProvider.Create("ASP.NET Identity"))
        End If
        Return manager
    End Function

End Class

Public Class ApplicationSignInManager
    Inherits SignInManager(Of ApplicationUser, String)
    Public Sub New(userManager As ApplicationUserManager, authenticationManager As IAuthenticationManager)
        MyBase.New(userManager, authenticationManager)
    End Sub

    Public Overrides Function CreateUserIdentityAsync(user As ApplicationUser) As Task(Of ClaimsIdentity)
        Return user.GenerateUserIdentityAsync(DirectCast(UserManager, ApplicationUserManager))
    End Function

    Public Shared Function Create(options As IdentityFactoryOptions(Of ApplicationSignInManager), context As IOwinContext) As ApplicationSignInManager
        Return New ApplicationSignInManager(context.GetUserManager(Of ApplicationUserManager)(), context.Authentication)
    End Function


End Class
