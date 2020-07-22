Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Public Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack Then
            ddlCat.Items.Add(New ListItem("Phy"))
            ddlCat.Items.Add("Chem")
            ddlCat.Items.Add("Math")
        End If
    End Sub






    Public Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click

        If (Page.IsValid) Then

            ddlCat.Items.Insert(3, txtCat.Text)
            lblmsg.Text = "Item successfullyy added to category dropdown "
            lblmsg.CssClass = "alert alert-success pull-right"
            Dim strJsSuccess As String = New StringBuilder("$('#sample_modal').modal('hide');").ToString()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Hide", strJsSuccess, True)

        End If
    End Sub
End Class