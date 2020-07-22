Partial Class ctrlCheckBox
    Inherits clsBase


    Private Sub ctrlField1_Load(sender As Object, e As EventArgs) Handles ctrlField1.Load
        ctrlField1.BorderColor = Drawing.Color.AliceBlue

    End Sub
    Public Overrides Sub ForceValidation()
        Dim lsReason As String = ""
        mcSelectedItems.Clear()
        mcSelectedItems2.Clear()
        If msDataType = "" Then msDataType = "Text"

        If ctrlField1.Checked = True Then
            miSelectedItem.Value = "True"
            miSelectedItem.Description = "True"
            mcSelectedItems.Add(miSelectedItem)
        End If

        If ctrlField2.Visible And CStr(ctrlField2.Checked) = True Then
            miSelectedItem2.Value = "True"
            miSelectedItem2.Description = "True"
            mcSelectedItems2.Add(miSelectedItem2)
        End If

        mbValid = Validate()
    End Sub
    Public Sub New()
        mrptCtrl = New ReportControl
    End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
    Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdatePanel1.DataBinding
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UpdatePanel1.DataBind()
    End Sub
    Protected Sub DropDownList1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownList1.TextChanged
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles CustomValidator1.ServerValidate
        mbValid = False
        ForceValidation()
        args.IsValid = mbValid
        CustomValidator1.ErrorMessage = msValidationReason
    End Sub

End Class