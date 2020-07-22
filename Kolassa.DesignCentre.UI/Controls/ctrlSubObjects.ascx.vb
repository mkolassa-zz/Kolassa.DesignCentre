Imports System.IO
Imports System.Data
Imports Telerik.Web.UI
Public Class ctrlSubObjects
    Inherits System.Web.UI.UserControl
    Public msParentID As String

    Protected Sub Page_Load(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            BindCombo()
            ShowDoctor()

        End If
    End Sub
    '*************************  Subcrap
    Protected Sub BindCombo()
        RadComboBox1.DataSource = GetDoctorsList()
        RadComboBox1.DataTextField = "Name"
        RadComboBox1.DataValueField = "Country"
        RadComboBox1.DataBind()
        RadComboBox1.Items(0).Selected = True
    End Sub

    Protected Sub ShowDoctor()
        Dim item As RadComboBoxItem = RadComboBox1.SelectedItem
        lblName.Text = item.Text

    End Sub
    Protected Sub RadComboBox1_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs)
        ShowDoctor()
        BindCombo()
    End Sub
    Protected Function GetSchedule() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Subject", GetType(String))
        dt.Columns.Add("Start", GetType(DateTime))
        dt.Columns.Add("End", GetType(DateTime))
        dt.Columns.Add("Doctor", GetType(String))
        dt.Rows.Add("1", "Surgery", New DateTime(2013, 7, 8, 10, 0, 0), New DateTime(2013, 7, 8, 12, 0, 0), "Dr. Olivia Rodriguez")
        dt.Rows.Add("2", "Medical Exams", New DateTime(2013, 7, 10, 8, 0, 0), New DateTime(2013, 7, 10, 12, 0, 0), "Dr. Olivia Rodriguez")
        dt.Rows.Add("3", "Medical Exams", New DateTime(2013, 7, 9, 9, 0, 0), New DateTime(2013, 7, 9, 13, 0, 0), "Dr. Ross Martin")
        dt.Rows.Add("4", "Medical Exams", New DateTime(2013, 7, 12, 8, 0, 0), New DateTime(2013, 7, 12, 11, 0, 0), "Dr. Ross Martin")
        dt.Rows.Add("5", "Surgery", New DateTime(2013, 7, 10, 11, 0, 0), New DateTime(2013, 7, 10, 14, 0, 0), "Dr. Diana Palmer")
        dt.Rows.Add("6", "Medical Exams", New DateTime(2013, 7, 12, 8, 0, 0), New DateTime(2013, 7, 12, 11, 0, 0), "Dr. Diana Palmer")
        Return dt
    End Function
    Protected Function GetDoctorsList() As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("ID", GetType(Integer))
        dt.Columns.Add("Name", GetType(String))
        dt.Columns.Add("Country", GetType(String))
        dt.Columns.Add("Phone", GetType(String))
        dt.Columns.Add("Email", GetType(String))
        dt.Columns.Add("Img", GetType(String))
        dt.Rows.Add("1", "Dr. Lucia Amado", "Spain", "+1 390 552 11 ext 351", "l.amado@example.com", "Images/doc1.jpg")
        dt.Rows.Add("2", "Dr. Ross Martin", "USA", "+1 390 552 11 ext 352", "r.martin@example.com", "Images/doc2.jpg")
        dt.Rows.Add("3", "Dr. Diana Palmer", "UK", "+1 390 552 11 ext 353", "d.palmer@example.com", "Images/doc3.jpg")
        Return dt
    End Function
    Protected Sub RadComboBox1_ItemDataBound(sender As Object, e As RadComboBoxItemEventArgs)
        Dim drv As DataRowView = DirectCast(e.Item.DataItem, DataRowView)
        e.Item.ImageUrl = "Images/doctor.png"
        e.Item.Attributes("Phone") = drv.Row("Phone").ToString()
        e.Item.Attributes("Country") = drv.Row("Country").ToString()
        e.Item.Attributes("Email") = drv.Row("Email").ToString()
        e.Item.Attributes("Img") = drv.Row("Img").ToString()
    End Sub

    Protected Sub RadTabStrip1_TabClick(sender As Object, e As RadTabStripEventArgs)
        lblSelectedTab.Text = "Selected tab: " + RadTabStrip1.SelectedTab.Text
    End Sub

    Private Sub ctrlSubObjects_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        Dim rg As RadGrid
        rg = Me.Parent.FindControl("rgMaster")
        If rg Is Nothing Then
            msParentID = "12121212-1212-1212-1212-121212121212"
        Else
            msParentID = rg.SelectedValue
        End If
    End Sub



    'Protected Sub btnUpload_Click(sender As Object, e As EventArgs) Handles btnUpload.Click
    '    Dim lsFIleName As String
    '    lsFIleName = FileUpload1.FileName
    '    '   lsFIleName = RadUpload1.UploadedFiles(1).FileName.ToString

    '    Dim intImageSize As Int64
    '    Dim strImageType As String
    '    Dim ImageStream As Stream

    '    '*** Gets the Size of the Image
    '    intImageSize = FileUpload1.PostedFile.ContentLength

    '    '*** Gets the Image Type
    '    strImageType = FileUpload1.PostedFile.ContentType

    '    '*** Reads the Image
    '    ImageStream = FileUpload1.PostedFile.InputStream



    '    Dim ImageContent(intImageSize) As Byte
    '    Dim intStatus As Integer
    '    intStatus = ImageStream.Read(ImageContent, 0, intImageSize)
    '    Dim c As New clsSelectDataLoader
    '    c.InsertImages("e83a120a-073c-4eab-8e32-f6cd218eb2cf", 2, lsFIleName, txtDescription.Text, 1, ImageContent, strImageType, "")

    'End Sub
End Class