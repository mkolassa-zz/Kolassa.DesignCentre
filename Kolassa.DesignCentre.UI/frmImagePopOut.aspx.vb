Imports System.IO
Imports AjaxControlToolkit
Imports Kolassa.DesignCentre.Data
Public Class frmImagePopOut
    Inherits System.Web.UI.Page

    Dim msObjectID As String
        Dim ErrorMessage As String
        Public Event SomethingHappened(sender As Object, e As EventArgs)

    Public Sub New()
        ' litMessage.Text = "Start Uploading Docs!"
    End Sub
    Public Property ObjectID() As String
            Get
            Return msObjectID
        End Get
            Set(ByVal Value As String)
                msObjectID = Value
                lblObjectID.Text = msObjectID
                '      txtobj.Text = msObjectID
                AjaxFileUpload1.Attributes.Remove("objectID")
                AjaxFileUpload1.Attributes.Add("objectID", msObjectID)

            lblObjectID.Attributes.Remove("objectID")
            lblObjectID.Attributes.Add("objectID", msObjectID)
            Dim lb As Label
                lb = New Label
                lb.Text = "this is a new label"
                lb.Text = ObjectID
                AjaxFileUpload1.Controls.Add(lb)
                AjaxFileUpload1.ToolTip = msObjectID
                Dim cookie As HttpCookie = New HttpCookie("objectID", msObjectID)
                cookie.Expires = DateTime.Now.AddMinutes(30)
                HttpContext.Current.Response.Cookies.Add(cookie)
                cookie = New HttpCookie("txtdescription", txtDescription.Text)
                cookie.Expires = DateTime.Now.AddMinutes(30)
            End Set
        End Property
        Public Property imagetype As String

        Private Function GetSelectedValue()
            If Len(lblObjectID.Text) = 36 Then
                GetSelectedValue = lblObjectID.Text
                Exit Function
            Else
                GetSelectedValue = "742D682D-278F-4CF3-B527-C9115C5028A7"
                Exit Function
            End If
        End Function
        Protected Sub btnUpload_Click(sender As Object, e As EventArgs) 'Handles btnUpload.Click
            Try
                Dim lsFileName As String = ""

                If lsFileName = "" Then Exit Sub
                Dim intImageSize As Int64
                Dim strImageType As String
                ' Dim ImageStream As Stream

                '*** Gets the Size of the Image
                '   intImageSize = FileUpload1.PostedFile.ContentLength

                '*** Gets the Image Type
                '   strImageType = FileUpload1.PostedFile.ContentType
                strImageType = "" 'Fix this
                '*** Reads the Image
                '   ImageStream = FileUpload1.PostedFile.InputStream
                ' ImageStream = DirectCast()


                Dim ImageContent(intImageSize) As Byte
                Dim intStatus As Integer
                intStatus = 1 ' ImageStream.Read(ImageContent, 0, intImageSize)
                Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
                Dim liNode As String = System.Web.HttpContext.Current.Session("NodeID")
                If liNode < 2 Then liNode = 2
                Dim lsObjectID As String = GetSelectedValue()
                If lsObjectID = "" Then
                    'No File
                Else
                Dim lsObjType As String = Request.QueryString("objType")
                Dim lsProject As String = Session("Project")
                If lsObjType.ToUpper = "PROJECT" Then lsProject = lsObjectID
                c.InsertImages(lsObjectID, lsObjType, liNode, lsFileName, txtDescription.Text, 1, ImageContent, strImageType, "", lsProject, False, False)
            End If
            Catch ex As Exception
                ErrorMessage = ErrorMessage & ex.Message
                litMessage.Text = ErrorMessage
            End Try
        End Sub

        Protected Sub UploadCompleteAll(sender As Object, e As AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs) Handles AjaxFileUpload1.UploadCompleteAll
            litMessage.Text = litMessage.Text & " Finished"
        End Sub

    Protected Sub uploadcomplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) Handles AjaxFileUpload1.UploadComplete
        Dim objectid As String = AjaxFileUpload1.Attributes("objectid")
        If objectid Is Nothing Or objectid = "" Then objectid = AjaxFileUpload1.ToolTip
        If objectid Is Nothing Or objectid = "" Then objectid = Request.QueryString("objectid")
        savefile(e.FileName, e.ContentType, objectid)
    End Sub
    Protected Sub savefile(filename As String, contenttype As String, objectid As String)
        Try

            Dim fPath As String = Session("ImagePath")
            If fPath = "/customerfiles" Then fPath = "customerfiles"
            ProcessDirectory(fPath)
            '       fPath = Server.MapPath(fPath)
            Dim objID As String = objectid 'Request.QueryString("ObjectID") 'AjaxFileUpload1.Attributes("objectid")
            If objID.Length <> 36 Then
                If HttpContext.Current.Request.Cookies("objectID") IsNot Nothing Then
                    objID = HttpContext.Current.Request.Cookies("objectID").Value.ToString()
                    If objID = "" Then
                        objID = AjaxFileUpload1.ToolTip
                        Dim qscoll As NameValueCollection = HttpUtility.ParseQueryString(Page.ClientQueryString)
                        AjaxFileUpload1.ToolTip = qscoll("contextKey")
                        'fu.ToolTip = qscoll("contextKey")
                        objID = qscoll("contextKey")
                        If objectid = "" Then objectid = lblObjectID.Attributes("objectID")
                        If objectid = "" Then
                            objectid = objID 'e.FileId
                        End If
                        If objectid = "" Then objectid = Session("Project")
                        objectid = objID 'Me.ID
                    End If
                End If
            End If
                Dim lsDescription As String = ""
            If HttpContext.Current.Request.Cookies("txtdescription") IsNot Nothing Then
                lsDescription = HttpContext.Current.Request.Cookies("txtdescription").Value.ToString()
            End If

            If Not Directory.Exists(fPath) Then
                Session("ErrorMessage") = Session("ErrorMessage") & "Creating: " & fPath
                Directory.CreateDirectory(fPath)
                litMessage.Text = litMessage.Text & "Creating: " & fPath
            Else
                Session("ErrorMessage") = Session("ErrorMessage") & " " & (fPath) & " Already Exists. "
                Page.Response.Write(Session("ErrorMessage"))
                litMessage.Text = litMessage.Text & " Exists: " & fPath
            End If

            Dim c As Control = Me.Parent
            Dim lsCust As String = Session("NodeID").ToString
            lsCust = "000" & Trim(lsCust)
            lsCust = Right(lsCust, 3)
            fPath = fPath & "/cust" & lsCust
            If Not Directory.Exists(fPath) Then
                Directory.CreateDirectory(fPath)
                litMessage.Text = litMessage.Text & " Creating Folder: " & fPath
            Else
                litMessage.Text = litMessage.Text & " Existing Folder: " & fPath
            End If
            '  fPath = Server.MapPath(fPath)
            Dim fileNametoupload As String ' = Server.MapPath(fPath) + "/" + Guid.NewGuid.ToString + e.FileName.ToString()

            fileNametoupload = Guid.NewGuid.ToString + filename.ToString()
            fileNametoupload = Replace(fileNametoupload, " ", "")
            litMessage.Text = litMessage.Text & " FileNameToUpload: " & fileNametoupload & "  Path:" & fPath + "/"
            AjaxFileUpload1.SaveAs(fPath + "/" + fileNametoupload)
            litMessage.Text = litMessage.Text & " Upload Complete"
            'fu.SaveAs(fileNametoupload)
            Dim o As clsImage = New clsImage
            o.Description = lsDescription
            o.Name = filename
            o.ImageUrl = "customerfiles/cust" & lsCust & "/" & fileNametoupload
            o.ImageOrder = 1
            o.ImageType = contenttype
            o.ObjectID = objID
            o.ObjectType = Request.QueryString("objType")
            o.Insert()
        Catch ex As Exception
            Session("ErrorMessage") = Session("ErrorMessage") & ex.Message
            litMessage.Text = litMessage.Text + ex.Message
            ' Throw ex
        End Try
        Session("ErrorMessage") = litMessage.Text
        '  litMessage.Text = Session("ErrorMessage")
        Page.Response.Write(Session("ErrorMessage"))

    End Sub
    Public Sub ProcessDirectory(targetDirectory As String)
            Dim liCounter As Integer = 0
            '// Process the list of files found in the directory.
            Dim fileEntries() As String = Directory.GetFiles(targetDirectory)
            For Each fileName As String In fileEntries
                liCounter = liCounter + 1
                ProcessFile(fileName, liCounter)
            Next
            ' // Recurse into subdirectories of this directory.
            Dim subdirectoryEntries() As String = Directory.GetDirectories(targetDirectory)
            For Each subdirectory As String In subdirectoryEntries
                ProcessDirectory(subdirectory)
            Next
        End Sub
        Sub ProcessFile(path As String, liCounter As Integer)
            System.Diagnostics.Debug.WriteLine("Processed file '{1}' '{0}'.", ResolveUrl(path), liCounter)
        End Sub
    'Protected Sub lnkbSave_Click(sender As Object, e As EventArgs) Handles lnkbSaveImageURL.Click
    '    Dim lsImageURL, lsImageName, lsImageType As String
    '    Dim liTemp, liTemp2 As Integer
    '    Try
    '        lsImageURL = txtImageURL.Text
    '        If lsImageURL = "" Then
    '            Exit Sub
    '        End If
    '        lsImageType = "Image"
    '        lsImageName = "Image"
    '        liTemp = InStrRev(lsImageURL, "/")
    '        liTemp2 = InStrRev(lsImageURL, "\")
    '        If liTemp2 > liTemp Then liTemp = liTemp2
    '        If liTemp > 0 Then
    '            lsImageName = Right(lsImageURL, lsImageURL.Length - liTemp)
    '            liTemp = InStrRev(lsImageName, ".")
    '            lsImageType = Right(lsImageName, lsImageName.Length - liTemp)
    '        End If
    '        Dim o As clsImage = New clsImage
    '        '   ObjectID = txtobj.Text
    '        ObjectID = lblObjectID.Text
    '        If ObjectID = "" Then ObjectID = Session("Project")
    '        o.ObjectID = ObjectID
    '        o.Description = txtDescription.Text
    '        o.Name = lsImageName
    '        o.ImageUrl = lsImageURL
    '        o.ImageOrder = 1
    '        o.ImageType = fgetImageType(lsImageType)
    '        o.ObjectType = Request.QueryString("objType")
    '        o.Insert()
    '    Catch ex As Exception

    '    End Try
    'End Sub
    Function fgetImageType(lsImageType As String) As String
            fgetImageType = lsImageType
        End Function

    Private Sub frmImagePopOut_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblObjectID.Text = Request.QueryString("objectID")
            txtPath.Text = Server.MapPath("Customerfiles")
            AjaxFileUpload1.ToolTip = Request.QueryString("ObjectID")
            AjaxFileUpload1.Attributes.Add("ObjectID", Request.QueryString("ObjectID"))
            Session("ImagePath") = Server.MapPath("customerfiles")
        Else
            AjaxFileUpload1.Attributes.Add("Path", Server.MapPath("customerfiles"))
            litMessage.Text = Session("ErrorMessage")
        End If
    End Sub

    Private Sub txtPath_TextChanged(sender As Object, e As EventArgs) Handles txtPath.TextChanged
        Session("ImagePath") = txtPath.Text
    End Sub

    'Private Sub ctrlImagesNew_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Select Case imagetype
    '        Case "URL"
    '            pnlURL.Visible = True
    '            upFileUploader.Visible = False
    '            upExisting.Visible = False
    '        Case "UPLOAD"
    '            pnlURL.Visible = False
    '            upFileUploader.Visible = True
    '            upExisting.Visible = False
    '        Case Else
    '            pnlURL.Visible = False
    '            upFileUploader.Visible = False
    '            upExisting.Visible = True
    '    End Select
    'End Sub
    'Private Sub AjaxFileUpload1_UploadStart(sender As Object, e As AjaxFileUploadStartEventArgs) Handles AjaxFileUpload1.UploadStart
    '    Session("ErrorMessage") = "Start"
    '    'litMessage.Text = ErrorMessage
    'End Sub
    'Protected Sub txtDescription_TextChanged(sender As Object, e As EventArgs) Handles txtDescription.TextChanged
    '    Dim cookie As HttpCookie = New HttpCookie("txtdescription", txtDescription.Text)
    '    cookie.Expires = DateTime.Now.AddMinutes(30)
    'End Sub

    'Protected Sub btnMessage_Click(sender As Object, e As EventArgs) Handles btnMessage.Click
    '    Session("ErrorMessage") = litMessage.Text & " My Message " & Session("ErrorMessage") & ResolveUrl("~/customerfiles")
    '    litMessage.Text = Session("ErrorMessage")
    '    hdMessage.Value = Session("ErrorMessage")
    '    Debug.Print("<Debug>")
    '    Response.Write("Dude:" & Session("ErrorMessage"))
    'End Sub

End Class