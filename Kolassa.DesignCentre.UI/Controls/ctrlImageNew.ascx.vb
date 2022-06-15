Imports System.IO
Imports AjaxControlToolkit
Imports Kolassa.DesignCentre.Data
Public Class ctrlImagesNew
    Inherits System.Web.UI.UserControl
    Dim msObjectID As String
    Public Sub New()

    End Sub
    Public Property ObjectID() As String
        Get
            If msObjectID = "" Or msObjectID Is Nothing Then
                msObjectID = Me.Attributes("objectID")
            End If
            Return msObjectID
        End Get
        Set(ByVal Value As String)
            msObjectID = Value
            lblObjectID.Text = msObjectID
            '      txtobj.Text = msObjectID
            AjaxFileUpload1.Attributes.Remove("objectID")
            AjaxFileUpload1.Attributes.Add("objectID", msObjectID)
            Me.Attributes.Remove("objectID")
            Me.Attributes.Add("objectID", msObjectID)
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
        Dim lsFileName As String

        If lsFileName = "" Then Exit Sub
        Dim intImageSize As Int64
        Dim strImageType As String
        Dim ImageStream As Stream

        '*** Gets the Size of the Image
        '   intImageSize = FileUpload1.PostedFile.ContentLength

        '*** Gets the Image Type
        '   strImageType = FileUpload1.PostedFile.ContentType

        '*** Reads the Image
        '   ImageStream = FileUpload1.PostedFile.InputStream



        Dim ImageContent(intImageSize) As Byte
        Dim intStatus As Integer
        intStatus = ImageStream.Read(ImageContent, 0, intImageSize)
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        Dim liNode As String = System.Web.HttpContext.Current.Session("NodeID")
        If liNode < 2 Then liNode = 2
        Dim lsObjectID As String = GetSelectedValue()
        If lsObjectID = "" Then
            'No File
        Else

            c.InsertImages(lsObjectID, liNode, lsFileName, txtDescription.Text, 1, ImageContent, strImageType, "", Session("Project"))
        End If
    End Sub

    Protected Sub UploadCompleteAll(sender As Object, e As AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs) Handles AjaxFileUpload1.UploadCompleteAll

    End Sub

    Protected Sub uploadcomplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) Handles AjaxFileUpload1.UploadComplete
        Dim fPath As String = "~/customerfiles"
        Dim objID As String = AjaxFileUpload1.Attributes("objectid")
        If HttpContext.Current.Request.Cookies("objectID") IsNot Nothing Then
            objID = HttpContext.Current.Request.Cookies("objectID").Value.ToString()
            If objID = "" Then
                objID = AjaxFileUpload1.ToolTip
                Dim qscoll As NameValueCollection = HttpUtility.ParseQueryString(Page.ClientQueryString)
                AjaxFileUpload1.ToolTip = qscoll("contextKey")
                objID = qscoll("contextKey")
                If ObjectID = "" Then ObjectID = Me.Attributes("objectID")
                If ObjectID = "" Then
                    ObjectID = objID 'e.FileId
                End If
                If ObjectID = "" Then ObjectID = Session("Project")
                ObjectID = objID 'Me.ID
            End If
        End If
        Dim lsDescription As String = ""
        If HttpContext.Current.Request.Cookies("txtdescription") IsNot Nothing Then
            lsDescription = HttpContext.Current.Request.Cookies("txtdescription").Value.ToString()
        End If


        If Not Directory.Exists(fPath) Then Directory.CreateDirectory(fPath)
        Dim c As Control = Me.Parent
        Dim lsCust As String = Session("NodeID").ToString
        lsCust = "000" & Trim(lsCust)
        lsCust = Right(lsCust, 3)
        fPath = fPath & "/cust" & lsCust
        If Not Directory.Exists(fPath) Then Directory.CreateDirectory(fPath)
        Dim fileNametoupload As String = Server.MapPath(fPath) + "\" + Guid.NewGuid.ToString + e.FileName.ToString()
        fileNametoupload = fPath + "\" + Guid.NewGuid.ToString + e.FileName.ToString()
        AjaxFileUpload1.SaveAs(fileNametoupload)

        Dim o As clsImage = New clsImage
        o.Description = lsDescription
        o.Name = e.FileName
        o.ImageURL = fileNametoupload
        o.ImageOrder = 1
        o.ImageType = e.ContentType
        o.ObjectID = objID
        o.Insert()
    End Sub

    Protected Sub lnkbSave_Click(sender As Object, e As EventArgs) Handles lnkbSave.Click
        Dim lsImageURL, lsImageName, lsImageType As String
        Dim liTemp, liTemp2 As Integer
        lsImageURL = txtImageURL.Text
        If lsImageURL = "" Then
            Exit Sub
        End If
        lsImageType = "Image"
        lsImageName = "Image"
        liTemp = InStrRev(lsImageURL, "/")
        liTemp2 = InStrRev(lsImageURL, "\")
        If liTemp2 > liTemp Then liTemp = liTemp2
        If liTemp > 0 Then
            lsImageName = Right(lsImageURL, lsImageURL.Length - liTemp)
            liTemp = InStrRev(lsImageName, ".")
            lsImageType = Right(lsImageName, lsImageName.Length - liTemp)
        End If
        Dim o As clsImage = New clsImage
        '   ObjectID = txtobj.Text
        ObjectID = lblObjectID.Text
        If ObjectID = "" Then ObjectID = Session("Project")
        o.ObjectID = ObjectID
        o.Description = txtDescription.Text
        o.Name = lsImageName
        o.ImageURL = lsImageURL
        o.ImageOrder = 1
        o.ImageType = fgetImageType(lsImageType)
        o.Insert()
    End Sub
    Function fgetImageType(lsImageType As String) As String
        fgetImageType = lsImageType
    End Function

    Private Sub ctrlImagesNew_Load(sender As Object, e As EventArgs) Handles Me.Load
        Select Case imagetype
            Case "URL"
                pnlURL.Visible = True
                upFileUploader.Visible = False
                upExisting.Visible = False
            Case "UPLOAD"
                pnlURL.Visible = False
                upFileUploader.Visible = True
                upExisting.Visible = False
            Case Else
                pnlURL.Visible = False
                upFileUploader.Visible = False
                upExisting.Visible = True
        End Select
    End Sub



    Private Sub AjaxFileUpload1_UploadStart(sender As Object, e As AjaxFileUploadStartEventArgs) Handles AjaxFileUpload1.UploadStart

    End Sub


    Protected Sub txtDescription_TextChanged(sender As Object, e As EventArgs) Handles txtDescription.TextChanged
        Dim cookie As HttpCookie = New HttpCookie("txtdescription", txtDescription.Text)
        cookie.Expires = DateTime.Now.AddMinutes(30)
    End Sub
End Class