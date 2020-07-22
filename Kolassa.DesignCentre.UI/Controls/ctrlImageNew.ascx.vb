Imports System.IO
Imports Telerik.Web.UI
Imports Kolassa.DesignCentre.Data
Public Class ctrlImagesNew
    Inherits System.Web.UI.UserControl

    Private Function GetSelectedValue()
        Dim rg As RadGrid
        GetSelectedValue = ""

        rg = Me.Parent.FindControl("rgMaster")
        If rg Is Nothing Then
            rg = Me.Parent.Parent.FindControl("rgMaster")
        End If
        If rg Is Nothing Then
            rg = Me.Parent.Parent.Parent.FindControl("rgMaster")
        End If
        If rg Is Nothing Then
            rg = Me.Parent.Parent.Parent.Parent.FindControl("rgMaster")
        End If
        If rg Is Nothing Then
        Else
            If rg.SelectedItems.Count > 0 Then
                GetSelectedValue = rg.SelectedValue
            End If
        End If
    End Function
    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
        Dim lsFileName As String

        lsFileName = FileUpload1.FileName
        If FileUpload1.HasFile Then
            lsFIleName = FileUpload1.FileName
        End If
        ' lsFIleName = RadUpload1.UploadedFiles(0).FileName.ToString
        If lsFIleName = "" Then Exit Sub
        Dim intImageSize As Int64
        Dim strImageType As String
        Dim ImageStream As Stream

        '*** Gets the Size of the Image
        intImageSize = FileUpload1.PostedFile.ContentLength

        '*** Gets the Image Type
        strImageType = FileUpload1.PostedFile.ContentType

        '*** Reads the Image
        ImageStream = FileUpload1.PostedFile.InputStream



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

			c.InsertImages(lsObjectID, liNode, lsFileName, txtDescription.Text, 1, ImageContent, strImageType, "")
        End If
    End Sub






End Class