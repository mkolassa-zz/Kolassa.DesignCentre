
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlListBox
    Inherits ctrlBase

    Dim lblFieldName As Label
    Dim DropDownList1 As DropDownList
    Dim uPanel1 As UpdatePanel
    Dim lblField1 As Label
    Dim lblField2 As Label
    Dim ctrlField1 As ListBox
    Dim ctrlField2 As ListBox
    Dim CustomValidator1 As CustomValidator
    Dim trig As AsyncPostBackTrigger

    Protected Overrides Sub CreateChildControlsSub()
        Controls.Clear()
        Dim li As ListItem
        Dim liCol As New ListItemCollection

        lblFieldName = New Label
        lblFieldName.ID = "lblFieldName"
        DropDownList1 = New DropDownList
        DropDownList1.ID = "DropDownList1"
        li = New ListItem()
        li.Value = "="
        li.Text = "="
        DropDownList1.Items.Add(li)
        li = New ListItem(">")
        DropDownList1.Items.Add(li)
        li = New ListItem(">=")
        DropDownList1.Items.Add(li)
        li = New ListItem("<")
        DropDownList1.Items.Add(li)
        li = New ListItem("<=")
        DropDownList1.Items.Add(li)
        li = New ListItem("Between")
        DropDownList1.Items.Add(li)

        DropDownList1.AutoPostBack = True
        DropDownList1.ToolTip = "Select the Operator"
        uPanel1 = New UpdatePanel
        uPanel1.ID = "uPanel1"
        trig = New AsyncPostBackTrigger
        trig.ControlID = "DropDownList1"
        trig.EventName = "SelectedIndexChanged"
        '  uPanel1.Triggers.Add(trig)
        lblField1 = New Label
        lblField1.ID = "lblField1"
        lblField2 = New Label
        lblField2.ID = "lblField2"
        lblField2.Visible = False
        ctrlField1 = New ListBox
        ctrlField1.ID = "ctrlField1"
        ctrlField2 = New ListBox
        ctrlField2.ID = "ctrlField2"
        ctrlField2.Visible = False
        CustomValidator1 = New CustomValidator
        CustomValidator1.ID = "CustomValidator1"

        AddHandler DropDownList1.TextChanged, AddressOf DropDownList1_TextChanged
        AddHandler uPanel1.DataBinding, AddressOf UpdatePanel1_DataBinding
        AddHandler CustomValidator1.ServerValidate, AddressOf CustomValidator1_ServerValidate

        Controls.Add(lblFieldName)
        Controls.Add(DropDownList1)
        Controls.Add(uPanel1)
        uPanel1.ContentTemplateContainer.Controls.Add(lblField1)
        uPanel1.ContentTemplateContainer.Controls.Add(ctrlField1)
        uPanel1.ContentTemplateContainer.Controls.Add(lblField2)
        uPanel1.ContentTemplateContainer.Controls.Add(ctrlField2)
        uPanel1.ContentTemplateContainer.Controls.Add(CustomValidator1)

        '  SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub


    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        On Error Resume Next
        DropDownList1.RenderControl(writer)
        lblFieldName.RenderControl(writer)
        lblField1.RenderControl(writer)
        lblField2.RenderControl(writer)
        uPanel1.RenderControl(writer)
        On Error GoTo 0
        'AddAttributesToRender(writer)
        'writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1")
        'writer.RenderBeginTag(HtmlTextWriterTag.Table)
        'writer.RenderBeginTag(HtmlTextWriterTag.Tr)
        'writer.RenderBeginTag(HtmlTextWriterTag.Td)
        'tb.RenderControl(writer)
        'writer.RenderEndTag() ' </td>

        'writer.RenderBeginTag(HtmlTextWriterTag.Td)
        'imgbt.RenderControl(writer)
        'writer.RenderEndTag() ' </td>
        'writer.RenderEndTag() ' </tr>
        'writer.RenderEndTag() ' </table>
        'cal.RenderControl(writer)

    End Sub


    '*** From .vb codebehind
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
        If mbRefreshList = False Then Exit Sub
        Dim rli As New ReportListItem
        Dim li As New ListItem
        Dim li2 As New ListItem
        '*** Clear the Items from the List
        ctrlField1.Items.Clear()
        ctrlField2.Items.Clear()

        '*** Refresh the ReportListITems Collection
        '*** NOT DONE YET

        '*** Iterate through the list Items and add to list Control
        For Each rli In mrptCtrl.ListItems
            li = New ListItem
            li2 = New ListItem
            li.Value = rli.Value
            li.Text = rli.Description
            ctrlField1.Items.Add(li)

            li2.Value = rli.Value
            li2.Text = rli.Description

            'If ctrlField2.Visible Then
            ctrlField2.Items.Add(li2)
            'End If
            li = Nothing
        Next

        If Me.DropDownList1.Text = "Between" Then
            ctrlField1.ClearSelection()
            ctrlField1.SelectionMode = ListSelectionMode.Single
        Else
            ctrlField1.SelectionMode = ListSelectionMode.Multiple
        End If

    End Sub
    Public Overrides Sub ForceValidation()
        Dim lsReason As String = ""
        Dim liCounter As Integer
        Dim liSelectedIndices(1000) As Integer
        Dim liSelectedIndices2(1000) As Integer

        dump()
        mcSelectedItems.Clear()
        mcSelectedItems2.Clear()

        If msDataType = "" Then msDataType = "Text"
        liSelectedIndices = ctrlField1.GetSelectedIndices()
        For liCounter = 0 To UBound(liSelectedIndices)
            miSelectedItem = New ReportListItem
            miSelectedItem.Value = CStr(ctrlField1.Items(liSelectedIndices(liCounter)).Value)
            miSelectedItem.Description = CStr(ctrlField1.Items(liSelectedIndices(liCounter)).Text)
            mcSelectedItems.Add(miSelectedItem)
        Next

        liSelectedIndices2 = ctrlField2.GetSelectedIndices()
        For liCounter = 0 To UBound(liSelectedIndices2)
            miSelectedItem2 = New ReportListItem
            miSelectedItem2.Value = CStr(ctrlField2.Items(liSelectedIndices2(liCounter)).Value)
            miSelectedItem2.Description = CStr(ctrlField2.Items(liSelectedIndices2(liCounter)).Text)
            mcSelectedItems2.Add(miSelectedItem2)

            ' System.Diagnostics.Debug.Print(mcSelectedItems(0). & " " & mcSelectedItems2.Count)
        Next
        mbValid = Validate()
    End Sub
    Public Sub New()
        mrptCtrl = New ReportControl
    End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
    Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles uPanel.DataBinding
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' uPanel1.DataBind()
        refreshLists()
    End Sub
    Protected Sub DropDownList1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles DropDownList1.TextChanged
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
    End Sub
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) ' Handles CustomValidator1.ServerValidate
        mbValid = False
        ForceValidation()
        args.IsValid = mbValid
        CustomValidator1.ErrorMessage = msValidationReason
    End Sub



    Sub dump()
        Dim lsText1 As String = ""
        Dim lsText2 As String = ""
        If UBound(ctrlField1.GetSelectedIndices) >= 0 Then lsText1 = ctrlField1.Items(ctrlField1.GetSelectedIndices(0)).Text
        If UBound(ctrlField2.GetSelectedIndices) >= 0 Then lsText2 = ctrlField2.Items(ctrlField2.GetSelectedIndices(0)).Text
        System.Diagnostics.Debug.Print(Me.msFieldName)
        System.Diagnostics.Debug.Print(lsText1 & " " & lsText2)

    End Sub

    Protected Sub ctrlField2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim l As ListBox = sender
        On Error Resume Next
        System.Diagnostics.Debug.Print(l.ID & " " & l.GetSelectedIndices(0))
    End Sub

End Class

