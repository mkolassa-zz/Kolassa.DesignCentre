Public Class frmTasks
    Inherits System.Web.UI.Page

    Protected Sub SendEmail()
        Dim ds As DataSet = New DataSet
        Dim c As New clsSelectDataLoader
        ds = c.LoadProjects(Session("NodeID"), "", True, "")
        Dim email As New clsEmail
        email.SendNewCustomerEmail("Hey There", "Mike is Great")
        email.SendNewCustomerEmail(ds.Tables(0))
        email.SendNewCustomerEmail(Session("UserEmail"), "Test Email")
        Dim h As New Hashtable
        Dim room As New clsRoom
        room.Code = "Bath"
        room.Name = "Big Bath"
        Dim unit As New clsUnit
        unit.Code = "123"
        unit.Name = "THe Unit"

        h.Add(room, unit)
        email.SendNewCustomerEmail(h, "Test Hashtable", Session("UserEmail"), "Hash table")
        email.SendNewCustomerEmail(Session("UserEmail"), "Email Message", "Email Subject", ds)
    End Sub

    Private Sub frmTasks_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        ctrlNotifications.BindData()
    End Sub
End Class