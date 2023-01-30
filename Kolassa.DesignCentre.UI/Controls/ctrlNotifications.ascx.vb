Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Public Class ctrlNotifications
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Form.Enctype = "multipart/form-data"
    End Sub
    Function GetObjectID() As String
        Dim lsObjectID As String
        Dim lit As Literal
        lit = Me.Parent.FindControl("litID")
        If lit Is Nothing Then
            lit = Me.Parent.Parent.FindControl("litID")
        End If
        If lit Is Nothing Then
            lit = Me.Parent.Parent.Parent.FindControl("litID")
        End If
        If lit Is Nothing Then
            lit = Me.Parent.Parent.Parent.Parent.FindControl("litID")
        End If
        If lit Is Nothing Then
            lsObjectID = "12121212-1212-1212-1212-121212121212"
        Else
            lsObjectID = lit.Text
        End If

        GetObjectID = lsObjectID
    End Function
    Public Sub BindData()
        gvData.DataBind()
        repeaterData.DataBind()
    End Sub
    Protected Sub gvData_DataBinding(sender As Object, e As EventArgs) Handles gvData.DataBinding
        Dim t As New clsTasks()
        gvData.DataSource = t.GetRecords("", "", Session("Project"), Session("NodeID"), HttpContext.Current.User.Identity.GetUserId)
        repeaterData.DataSource = gvData.DataSource
    End Sub
    Protected Sub cmdPostComm_Click(sender As Object, e As EventArgs) Handles cmdPostComm.Click
        gvData.DataBind()
        repeaterData.DataBind()
    End Sub
    Protected Sub cmdBindData_Click(sender As Object, e As EventArgs) Handles cmdBindData.Click
        BindData()
    End Sub
    Protected Sub gvData_RowCreated(sender As Object, e As GridViewRowEventArgs)
        e.Row.CssClass = "table border"
    End Sub
End Class