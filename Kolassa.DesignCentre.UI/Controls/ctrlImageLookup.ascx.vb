﻿Imports System.IO
'Imports Te lerik.Web.UI
Imports Kolassa.DesignCentre.Data
Public Class ctrlImageLookup
    Inherits System.Web.UI.UserControl

    Public Sub New()

    End Sub

    Private Function GetSelectedValue()
        If Len(lblObjectID.Text) = 36 Then
            GetSelectedValue = lblObjectID.Text
            Exit Function
        Else
            GetSelectedValue = "742D682D-278F-4CF3-B527-C9115C5028A7"
            Exit Function
        End If
        Dim rg As GridView 'RadGrid
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
            If rg.SelectedValue > 1 Then ' rg.SelectedItems.Count > 0 Then
                GetSelectedValue = rg.SelectedValue
            End If
        End If
    End Function
    Public Property objectId() As String
        Get
            objectId = lblObjectID.Text
        End Get
        Set(value As String)
            lblObjectID.Text = value
        End Set
    End Property

    Protected Sub btnUpload_Click(sender As Object, e As EventArgs) 'Handles btnUpload.Click
        Dim lsFileName As String

        ' lsFileName = FileUpload1.FileName
        '  If FileUpload1.HasFile Then
        '      lsFIleName = FileUpload1.FileName
        '  End If
        ' lsFIleName = RadUpload1.UploadedFiles(0).FileName.ToString
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

            'CH  c.InsertImages(lsObjectID, liNode, lsFileName, txtDescription.Text, 1, ImageContent, strImageType, "", Session("Project"))
        End If
    End Sub


    Sub cmdSearch_Click()

    End Sub



    Function fgetImage(lsImageType As String, ls As String) As String
        fgetImage = lsImageType
    End Function
End Class