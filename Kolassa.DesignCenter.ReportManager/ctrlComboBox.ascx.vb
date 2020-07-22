Partial Class ctrlComboBox
    Inherits clsBase
    Dim maList1Values(1000, 2) As Array
    Dim malist2Values(1000, 2) As Array
    Public Property List1Source() As Array
        Get
            List1Source = maList1Values
        End Get
        Set(ByVal value As Array)
            maList1Values = value
            refreshLists()
        End Set
    End Property
    Public Property List2Source() As Array
        Get
            List2Source = malist2Values
        End Get
        Set(ByVal value As Array)
            malist2Values = value
            '         RefreshList2()
        End Set
    End Property
    Public Overrides Sub refreshLists()

        Dim rli As New ReportListItem
        Dim li As New ListItem
        '*** Clear the Items from the List
        ctrlField1.Items.Clear()
        ctrlField2.Items.Clear()

        '*** Refresh the ReportListITems Collection
        '*** NOT DONE YET

        '*** Iterate through the list Items and add to list Control
        For Each rli In miListItems
            li = New ListItem
            li.Value = rli.Value
            li.Text = rli.Description
            ctrlField1.Items.Add(li)
            ' If ctrlField2.Visible Then
            ctrlField2.Items.Add(li)
            'End If
            li = Nothing
        Next

    End Sub
    Public Overrides Sub ForceValidation()
        Dim lsReason As String = ""
        mcSelectedItems.Clear()
        mcSelectedItems2.Clear()
        If msDataType = "" Then msDataType = "Text"

        If ctrlField1.Text <> "" Then
            miSelectedItem.Value = CStr(ctrlField1.Text)
            miSelectedItem.Description = CStr(ctrlField1.Text)
            mcSelectedItems.Add(miSelectedItem)
        End If

        If ctrlField2.Visible And CStr(ctrlField2.Text) <> "" Then
            miSelectedItem2.Value = CStr(ctrlField2.Text)
            miSelectedItem2.Description = CStr(ctrlField2.Text)
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
        refreshLists()
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
