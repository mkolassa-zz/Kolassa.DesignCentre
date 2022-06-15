Option Strict Off
Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlListBox
    Inherits ctrlBase

    Dim lblFieldName As Label
    Dim DropDownList1 As DropDownList
    Dim uPanel1 As Panel 'UpdatePanel
    Dim lblField1 As Label
    Dim lblField2 As Label
    Dim ctrlField1 As ListBox
    Dim ctrlField2 As ListBox
    Dim CustomValidator1 As CustomValidator
    Dim trig As AsyncPostBackTrigger
    Public Overrides Property ControlPanelcss() As String
        Get
            ControlPanelcss = msControlPanelcss
        End Get
        Set(sValue As String)
            msControlPanelcss = sValue
            '  uPanel1.Attributes.Add("class", sValue)
        End Set
    End Property
    Protected Overrides Sub CreateChildControlsSub()
        mcSelectedItems.Clear()
        mcSelectedItems2.Clear()
        'debug.print("<ctrlListbox.CreateChildControls>")
        Controls.Clear()
        Dim li As ListItem
        Dim liCol As New ListItemCollection

        lblFieldName = New Label
        lblFieldName.ID = "lblFieldName"
        DropDownList1 = New DropDownList
        DropDownList1.ID = "DropDownList1"
        DropDownList1.CssClass = "my-1 mr-sm-2"
        DropDownList1.Text = "THis"
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
        uPanel1 = New Panel ' UpdatePanel
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
        'ClearChildViewState()

        ctrlField1.ID = "ctrlField1"
        ctrlField1.CssClass = "form-control form-control-sm"
        ctrlField2 = New ListBox
        ctrlField2.ID = "ctrlField2"
        ctrlField2.CssClass = "form-control form-control-sm"
        ctrlField2.Visible = False
        CustomValidator1 = New CustomValidator
        CustomValidator1.ID = "CustomValidator1"

        AddHandler DropDownList1.TextChanged, AddressOf DropDownList1_TextChanged
        AddHandler uPanel1.DataBinding, AddressOf UpdatePanel1_DataBinding
        AddHandler CustomValidator1.ServerValidate, AddressOf CustomValidator1_ServerValidate

        Controls.Add(lblFieldName)


        uPanel1.Controls.Add(fGetFieldButton)
        uPanel1.Controls.Add(fGetFieldDeleteButton)


        Controls.Add(DropDownList1)


        Controls.Add(uPanel1)
        uPanel1.Controls.Add(lblField1)
        uPanel1.Controls.Add(ctrlField1)
        uPanel1.Controls.Add(lblField2)
        uPanel1.Controls.Add(ctrlField2)
        uPanel1.Controls.Add(CustomValidator1)
        uPanel1.Controls.Add(ctrlHelpText)
        uPanel1.Attributes.Add("class", msControlPanelcss)
        '  SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
        'debug.print("</ctrlListbox.CreateChildControls>")
    End Sub
    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub


    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        'debug.print("<ctrlListbox.RenderSubControls>")
        '       On Error Resume Next
        DropDownList1.RenderControl(writer)
        lblFieldName.RenderControl(writer)
        lblField1.RenderControl(writer)
        lblField2.RenderControl(writer)
        uPanel1.RenderControl(writer)
        On Error GoTo 0

        'debug.print("</ctrlListbox.RenderSubControls>")
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
        'debug.print("<ctrlListbox.Refreshlists>")
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
        'debug.print("</ctrlListbox.Refreshlists>")
    End Sub
    Public Overrides Sub ForceValidation()
        'debug.print("<ctrlListbox.ForceValidation>")
        Dim lsReason As String = ""
        Dim liCounter As Integer
        Dim liSelectedIndices(1000) As Integer
        Dim liSelectedIndices2(1000) As Integer

        dump()
        mcSelectedItems.Clear()
        mcSelectedItems2.Clear()

        If msDataType = "" Then msDataType = "Text"

        '*** Can we get the selected indices from the control or will we need to query
        '*** the Request.Form object
        liSelectedIndices = ctrlField1.GetSelectedIndices()
        liSelectedIndices2 = ctrlField2.GetSelectedIndices()

        If UBound(liSelectedIndices) = -1 Then
            '*** NO Indices found through normal object.  Due to some bug in ASP.NET I
            '*** will now check the Request.Form object
            Dim lsForm(1000) As String ' = Context.Request.Form(Me.Parent.ClientID)
            Dim lcForm As NameValueCollection = Context.Request.Form()
            Dim lsVal As String
            Dim listitemTemp As ListItem
            For Each lsVal In lcForm.Keys
                If InStr(lsVal, Me.ID) > 0 Then
                    If InStr(lsVal, "ctrlField1") Then
                        lsForm = Context.Request.Form.GetValues(lsVal)
                        For liCounter = 0 To UBound(lsForm)
                            listitemTemp = ctrlField1.Items.FindByValue(lsForm(liCounter))
                            miSelectedItem = New ReportListItem
                            miSelectedItem.Value = CStr(lsForm(liCounter))
                            '*** Dont know why this listitem is nothing
                            If listitemTemp Is Nothing Then
                                miSelectedItem.Description = "No Value Check this out"
                            Else
                                miSelectedItem.Description = IIf(listitemTemp Is Nothing, "No Value, Check this out", CStr(listitemTemp.Text))
                            End If

                            mcSelectedItems.Add(miSelectedItem)
                        Next
                    End If
                    If InStr(lsVal, "ctrlField2") Then
                        lsForm = Context.Request.Form.GetValues(lsVal)
                        For liCounter = 0 To UBound(lsForm)
                            listitemTemp = ctrlField2.Items.FindByValue(lsForm(liCounter))
                            miSelectedItem2 = New ReportListItem
                            miSelectedItem2.Value = CStr(lsForm(liCounter))
                            miSelectedItem2.Description = CStr(listitemTemp.Text)
                            mcSelectedItems2.Add(miSelectedItem2)
                        Next
                    End If
                End If
            Next
        Else
            For liCounter = 0 To UBound(liSelectedIndices)
                miSelectedItem = New ReportListItem
                miSelectedItem.Value = CStr(ctrlField1.Items(liSelectedIndices(liCounter)).Value)
                miSelectedItem.Description = CStr(ctrlField1.Items(liSelectedIndices(liCounter)).Text)
                mcSelectedItems.Add(miSelectedItem)
            Next
            For liCounter = 0 To UBound(liSelectedIndices2)
                miSelectedItem2 = New ReportListItem
                miSelectedItem2.Value = CStr(ctrlField2.Items(liSelectedIndices2(liCounter)).Value)
                miSelectedItem2.Description = CStr(ctrlField2.Items(liSelectedIndices2(liCounter)).Text)
                mcSelectedItems2.Add(miSelectedItem2)
                ' System.Diagnostics.'debug.print(mcSelectedItems(0). & " " & mcSelectedItems2.Count)
            Next
        End If

        mbValid = Validate()
        'debug.print("</ctrlListbox.ForceValidation>")
    End Sub
    Public Sub New()
        'debug.print("<ctrlListbox.New />")
        mrptCtrl = New ReportControl
    End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
    Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles uPanel.DataBinding
        'debug.print("<ctrlListbox.DataBinding>")
        SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
        'debug.print("</ctrlListbox.DataBinding>")
    End Sub
    Protected Sub iPagei_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'debug.print("<ctrlListbox.iPagei_load>")
        Try
            uPanel1.DataBind()
            refreshLists()
        Catch
            'debug.print("<err msg=" & Err.Description & "</err>")
        End Try
        'debug.print("</ctrlListbox.iPagei_load>")
    End Sub
    Protected Sub DropDownList1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles DropDownList1.TextChanged
        'debug.print("<ctrlListbox.DropDownList1_TextChanged>")
        Try
            SetFields(Me.DropDownList1.Text, Me.DropDownList1.SelectedValue)
        Catch
            'debug.print("<err msg=" & Err.Description & "</err>")
        End Try
        'debug.print("</ctrlListbox.DropDownList1_TextChanged>")
    End Sub
    Protected Sub CustomValidator1_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) ' Handles CustomValidator1.ServerValidate
        'debug.print("<ctrlListbox.CustomValidator1_ServerValidate>")
        mbValid = False
        ForceValidation()
        args.IsValid = mbValid
        CustomValidator1.ErrorMessage = msValidationReason
        'debug.print("</ctrlListbox.CustomValidator1_ServerValidate>")
    End Sub

    Public Overrides Sub SetFields(lsOption As String, lsValue As String)
        '*** Added 6/1/2020  This was missing from Listbox control
        Dim lstItem As ListItem
        MyBase.SetFields(lsOption, lsValue)
        Dim lrli As ReportListItem
        If SelectedItems.Count > 0 Then
            For Each lrli In SelectedItems
                For Each lstItem In ctrlField1.Items
                    If lstItem.Value = lrli.Value Then
                        lstItem.Selected = True
                        'debug.print("CTRLListBox Set Fields: " & lblFieldName.Text & " Value: " & lstItem.Text)
                    End If
                Next

            Next

        End If
    End Sub

    Sub dump()
        Dim lsText1 As String = ""
        Dim lsText2 As String = ""
        If UBound(ctrlField1.GetSelectedIndices) >= 0 Then lsText1 = ctrlField1.Items(ctrlField1.GetSelectedIndices(0)).Text
        If UBound(ctrlField2.GetSelectedIndices) >= 0 Then lsText2 = ctrlField2.Items(ctrlField2.GetSelectedIndices(0)).Text
        '  System.Diagnostics.'debug.print(Me.msFieldName)
        ' System.Diagnostics.'debug.print(lsText1 & " " & lsText2)

    End Sub

    Protected Sub ctrlField2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'debug.print("<ctrlListbox.ctrlField2_SelectedIndexChanged>")
        Dim l As ListBox = sender
        '  On Error Resume Next
        '   System.Diagnostics.debug.print(l.ID & " " & l.GetSelectedIndices(0))

        'debug.print("</ctrlListbox.ctrlField2_SelectedIndexChanged>")
    End Sub

    Private Sub ctrlListBox_ControlUpdated(sender As Object, e As EventArgs) Handles Me.ControlUpdated
        On Error Resume Next
        'debug.print("<ctrlListBox_ControlUpdate selectedindex=" & ctrlField1.SelectedIndex & " />")
    End Sub

    Private Sub ctrlListBox_DataBinding(sender As Object, e As EventArgs) Handles Me.DataBinding
        On Error Resume Next
        'debug.print("<ctrlListBox_DataBinding selectedindex=" & ctrlField1.SelectedIndex & " />")
    End Sub

    Private Sub ctrlListBox_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        On Error Resume Next
        'debug.print("<ctrlListBox_Disposed selectedindex=" & ctrlField1.SelectedIndex & " />")
    End Sub

    Private Sub ctrlListBox_Init(sender As Object, e As EventArgs) Handles Me.Init
        On Error Resume Next
        If Not ctrlField1 Is Nothing Then
            'debug.print("<ctrlListBox_Init selectedindex=" & ctrlField1.SelectedIndex & " />")
        End If
    End Sub


    Private Sub ctrlListBox_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
        On Error Resume Next
        'debug.print("<ctrlListBox_PreRender selectedindex=" & ctrlField1.SelectedIndex & " />")
    End Sub

    Private Sub ctrlListBox_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        On Error Resume Next
        'debug.print("<ctrlListBox_Unload selectedindex=" & ctrlField1.SelectedIndex & " />")
    End Sub
End Class

