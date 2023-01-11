Imports System.Data
Imports System.Net.Mail
Imports System.Net
Public Class clsEmail

    Dim Strbuilder As StringBuilder = New StringBuilder()
    Dim obj1 As StringBuilder = New StringBuilder()
    Dim strSmtpServer As String = ConfigurationManager.AppSettings("emailServiceHost") '"Mail.addisonstuart.com""mail5008.site4now.net" '
    Dim strUserName As String = ConfigurationManager.AppSettings("emailServiceUserName")
    Dim strPort = ConfigurationManager.AppSettings("emailServicePort") '"8889""465" '25  "465" '
    Dim strPassword As String = ConfigurationManager.AppSettings("emailServicePassword")
    Dim ErrorMessage As String
    Dim strMailFrom As String = System.Configuration.ConfigurationManager.AppSettings("mailFrom")
    Public Property strMailTo As String = System.Configuration.ConfigurationManager.AppSettings("mailTo")
    Public Property strMailCC As String = System.Configuration.ConfigurationManager.AppSettings("MailCC")
    Public Property strMailBcc As String = System.Configuration.ConfigurationManager.AppSettings("MailBcc")

    '*** <summary>
    '*** procedure used to send the mail notification to the end user.
    '*** </summary>
    '*** <param name="message"></param>
    '*** <param name="subject"></param>
    Public Sub SendNewCustomerEmail(message As String, subject As String)
        Dim Style1 As String = "style=’font-family: Verdana;font-weight: normal;font-size: 11px;color: black’"
        Dim strbuilder As StringBuilder = New StringBuilder()
        Try
            If (message <> String.Empty) Then
                '/// Create the Objects
                Dim objMSg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
                Dim SmtpMail As System.Net.Mail.SmtpClient = New SmtpClient(strSmtpServer, strPort)
                SmtpMail.Credentials = New NetworkCredential(strUserName, strPassword)
                Dim fromAddress As MailAddress = New MailAddress(strMailFrom, "AS Design Centre")
                SmtpMail.EnableSsl = False
                objMSg.To.Add(strMailTo)
                objMSg.CC.Add(strMailCC)
                objMSg.Bcc.Add(strMailBcc)
                objMSg.From = fromAddress
                objMSg.Subject = subject
                strbuilder.Append("<form><div " + Style1 + "> Hi,<Br><br> " + message + "</Div></form>")
                strbuilder.Append("<form><div " + Style1 + "> Regards,<Br> Dev Team </Div></form>")
                objMSg.Body = strbuilder.ToString()
                objMSg.IsBodyHtml = True
                objMSg.Priority = MailPriority.High
                objMSg.BodyEncoding = System.Text.Encoding.UTF8
                SmtpMail.Send(objMSg)
            Else
            End If
        Catch e As SmtpException
            ErrorMessage = e.Message.ToString() & " : " & e.InnerException.Message.ToString()
        Finally
        End Try
    End Sub
    '*** <summary>
    '*** procedure used to send the mail notification to the end user.
    '*** </summary>
    '*** <param name="message"></param>
    '*** <param name="subject"></param>
    Public Sub SendNewCustomerEmail(EmailId As String, message As String, subject As String)
        Dim Style1 As String = "style=’font-family: Verdana;font-weight: normal;font-size: 11px;color: black’"
        Dim strbuilder As StringBuilder = New StringBuilder()
        '    Dim strSmtpServer As String = System.Configuration.ConfigurationManager.AppSettings("MailServer")
        Try
            If (message <> String.Empty) Then
                '// Create the Objects
                Dim objMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
                Dim SmtpMail As System.Net.Mail.SmtpClient = New SmtpClient(strSmtpServer, strPort)
                SmtpMail.Credentials = New NetworkCredential("EmailId", "Password")
                Dim fromAddress As MailAddress = New MailAddress(EmailId, "Testing")
                objMsg.To.Add(EmailId)
                '//objMsg.CC.Add(strMailCC)
                '//objMsg.Bcc.Add(strMailBcc)
                objMsg.From = fromAddress
                objMsg.Subject = subject
                strbuilder.Append("<form><div " + Style1 + "> Hi,<Br><br> " + message + "</Div></form>")
                strbuilder.Append("<form><div " + Style1 + "> Thnx,<Br> Dev Team </Div></form>")
                objMsg.Body = strbuilder.ToString()
                objMsg.IsBodyHtml = True
                objMsg.Priority = MailPriority.High
                objMsg.BodyEncoding = System.Text.Encoding.UTF8
                SmtpMail.Send(objMsg)
            Else
            End If
        Catch e As SmtpException
            ErrorMessage = "Error :" + e.Message.ToString() + " : " + e.InnerException.Message.ToString()
        Finally
        End Try
    End Sub
    '*** <summary>
    '*** procedure used to send the mail notification to the end user.
    '*** </summary>
    '*** <param name="message"></param>
    '*** <param name="subject"></param>
    Public Sub SendNewCustomerEmail(EmailId As String, message As String, subject As String, _dsLoginDetails As DataSet)
        Dim Style1 As String = "style=’font-family: Verdana;font-weight: normal;font-size: 11px;color: black’"
        Dim strbuilder As StringBuilder = New StringBuilder()
        '      Dim strSmtpServer As String = System.Configuration.ConfigurationManager.AppSettings("MailServer")
        Try
            If (message <> String.Empty) Then
                '// Create the Objects
                Dim objMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
                Dim SmtpMail As System.Net.Mail.SmtpClient = New SmtpClient(strSmtpServer, strPort)
                SmtpMail.Credentials = New NetworkCredential("EmailID", "Password")
                Dim fromAddress As MailAddress = New MailAddress(EmailId, "Testing")
                objMsg.To.Add(EmailId)
                ' //objMsg.CC.Add(strMailCC)
                '//objMsg.Bcc.Add(strMailBcc)
                objMsg.From = fromAddress
                objMsg.Subject = subject
                strbuilder.Append("<form><div " + Style1 + "> Hi,<Br><br> " + message + "</Div></form>")
                strbuilder.Append("<form><div " + Style1 + "> Regards,<Br> Dev Team </Div></form>")
                objMsg.Body = strbuilder.ToString()
                objMsg.IsBodyHtml = True
                objMsg.Priority = MailPriority.High
                objMsg.BodyEncoding = System.Text.Encoding.UTF8
                SmtpMail.Send(objMsg)
            Else
            End If
        Catch e As SmtpException
            ErrorMessage = "Error :" + e.Message.ToString() + " : " + e.InnerException.Message.ToString()
        Finally
        End Try
    End Sub
    '*** <summary>
    '*** procedure used to send the mail Notification to the end user.
    '*** </summary>
    '*** <param name="obj_h">Hashtable</param>
    '*** <param name="subject">string</param>
    Public Sub SendNewCustomerEmail(obj_h As Hashtable, subject As String, EmailId As String, message As String)
        Dim strbuilder As StringBuilder = New StringBuilder(5000)
        Dim obj1 As StringBuilder = New StringBuilder()
        Dim Strvalue As String = ""
        Dim Style1 As String = "style=’font-family: Verdana;font-weight: normal;font-size: 11px;color:black’"
        Dim Style2 As String = "style=’text-align: left;font-family: Verdana;font-weight: normal;font-size: 11px;width: 150px;border: 1px #EFEFEF solid;border-collapse: collapse;border-spacing: 0px’"
        Try
            Dim objMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
            Dim SmtpMail As System.Net.Mail.SmtpClient = New SmtpClient(strSmtpServer, strPort)
            SmtpMail.Credentials = New NetworkCredential(strUserName, strPassword)
            Dim fromAddress As MailAddress = New MailAddress(EmailId, "Testing")
            objMsg.To.Add("EmailAddress")
            objMsg.From = fromAddress
            objMsg.Subject = subject
            Dim En As IDictionaryEnumerator = obj_h.GetEnumerator()
            strbuilder.Append("<form><div " + Style1 + "> Hello,<Br><Br></Div></form>")
            strbuilder.Append("<table border=1 width=60%" + Style2 + ">")
            While (En.MoveNext())
                If (En.Value.ToString().Trim() <> "") Then
                    Strvalue = En.Value.ToString()
                Else
                    Strvalue = "-"
                End If
                strbuilder.Append("<tr><td " + Style2 + ">" + En.Key + "</td><td " + Style2 + ">" + Strvalue + "</td> </tr>")
            End While
            strbuilder.Append("</table>")
            strbuilder.Append("<form><div " + Style1 + "> Regards,<Br> Dev Team </Div></form>")
            objMsg.Body = strbuilder.ToString()
            obj_h.Clear()
            objMsg.IsBodyHtml = True
            objMsg.Priority = MailPriority.High
            objMsg.BodyEncoding = System.Text.Encoding.UTF8
            SmtpMail.Send(objMsg)
        Catch e As SmtpException
            ErrorMessage = "Error :" + e.Message.ToString() + " : " + e.InnerException.Message.ToString()
        Finally
        End Try
    End Sub
    '*** <summary>
    '*** procedure used to send the mail Notification to the end user.
    '*** </summary>
    '*** <param name="obj_h">Hashtable</param>
    '*** <param name="subject">string</param>
    Public Sub SendNewCustomerEmail(dt As DataTable)
        Dim strbuilder As StringBuilder = New StringBuilder(5000)
        Dim obj1 As StringBuilder = New StringBuilder()
        Dim Strvalue As String = ""
        Dim Style1 As String = "style=’font-family: Verdana;font-weight: normal;font-size: 11px;color:black’"
        Dim Style2 As String = "style=’text-align: left;font-family: Verdana;font-weight: normal;font-size: 11px;width: 150px;border: 1px #EFEFEF solid;border-collapse: collapse;border-spacing: 0px’"
        Try
            Dim objMsg As System.Net.Mail.MailMessage = New System.Net.Mail.MailMessage()
            Dim SmtpMail As System.Net.Mail.SmtpClient = New SmtpClient(strSmtpServer, strPort)
            SmtpMail.Credentials = New NetworkCredential(strUserName, strPassword)
            Dim fromAddress As MailAddress = New MailAddress(strMailFrom, "Testing")
            objMsg.To.Add(strMailTo)
            objMsg.CC.Add(strMailCC)
            objMsg.Bcc.Add(strMailBcc)
            objMsg.From = fromAddress
            Dim obj_h As Hashtable = New Hashtable()
            Dim En As IDictionaryEnumerator = obj_h.GetEnumerator()
            objMsg.Subject = "TheSubject" ' dt.Rows(0)("EmailSubject").ToString()
            strbuilder.Append("<form><div " + Style1 + "> Hello,<Br><Br></Div></form>")
            strbuilder.Append("<table border=1 width=60%" + Style2 + ">")
            While (En.MoveNext())
                If (En.Value.ToString().Trim() <> "") Then
                    Strvalue = En.Value.ToString()
                Else
                    Strvalue = "-"
                End If
                strbuilder.Append("<tr><td " + Style2 + ">" + En.Key + "</td><td " + Style2 + ">" + Strvalue + "</td> </tr>")
            End While
            strbuilder.Append("</table>")
            strbuilder.Append("<form><div " + Style1 + "> Regards,<Br> Billing Team </Div></form>")
            objMsg.Body = "" ' dt.Rows(0)("EmailBody").ToString()
            ' Dim attach As Attachment = New Attachment("hjkl") 'dt.Rows(0)("EmailAttachments").ToString())
            ' objMsg.Attachments.Add(attach)
            objMsg.IsBodyHtml = True
            objMsg.Priority = MailPriority.High
            objMsg.BodyEncoding = System.Text.Encoding.UTF8
            SmtpMail.Send(objMsg)
        Catch e As SmtpException
            ErrorMessage = "Error :" + e.Message.ToString() + " : " + e.InnerException.Message.ToString()
        Finally
        End Try
    End Sub
End Class