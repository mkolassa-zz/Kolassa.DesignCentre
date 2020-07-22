Public Class ctrlContactEntry
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack Then

            ddlCat.Items.Add(New ListItem("Phy"))
            ddlCat.Items.Add("Chem")
            ddlCat.Items.Add("Math")
        End If
    End Sub

    Protected Sub btn_Submit_Click1(sender As Object, e As EventArgs) Handles btn_save.Click
        '  If (Page.IsValid) Then
        Dim txt As TextBox = txtCat
        Dim t As String = txt.Text
        ddlCat.Items.Insert(3, t)
        lblmsg.Text = "Item successfullyy added to category dropdown "
        lblmsg.CssClass = "alert alert-success pull-right"
        Dim strJsSuccess As String = New StringBuilder("$('#sample_modal').modal('hide');").ToString()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Hide", strJsSuccess, True)

        '    End If
    End Sub
End Class