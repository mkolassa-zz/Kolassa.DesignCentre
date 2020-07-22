Imports Telerik.Web.UI
Public Class ctrlCustomers
    Inherits System.Web.UI.UserControl
    Private msParentID As String

    Public Property ParentID() As String
        Get
            Return msParentID
        End Get
        Set(ByVal value As String)
            msParentID = value
            txtParentID.Text = value

            odsCustomer.DataBind()
            Me.RadListView1.Rebind()

        End Set
    End Property





    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Not IsPostBack Then

        End If
        txtParentID.Text = msParentID
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


    Private Sub odsContact_Updated(sender As Object, e As ObjectDataSourceStatusEventArgs) Handles odsCustomer.Updated

        RadListView1.DataBind()

    End Sub



    Protected Sub odsCustomer_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsCustomer.Selecting
        Dim rg As Telerik.Web.UI.RadGrid
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
            e.InputParameters("lsObjectID") = "12121212-1212-1212-1212-121212121212"
        Else
            e.InputParameters("lsObjectID") = rg.SelectedValue
        End If
    End Sub
End Class