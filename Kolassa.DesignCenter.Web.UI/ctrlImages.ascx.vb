Imports System.IO
Imports Telerik.Web.UI
Public Class ctrlImages
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Form.Enctype = "multipart/form-data"
    End Sub






    Protected Sub ObjectDataSource1_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles ObjectDataSource1.Selecting
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