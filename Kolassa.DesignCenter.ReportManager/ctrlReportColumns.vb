Option Strict Off
Imports System.ComponentModel
Imports System.Drawing
Imports System.Web.HttpContext

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlReportColumns
    Inherits ctrlBase
    Dim uPanel1 As UpdatePanel
    Dim gv As GridView
    Dim btnSave As LinkButton
    Dim btnDelete As LinkButton
    Public coldataview As DataView
    Dim lblViewTitle As Label
    ' Dim DropDownList1 As DropDownList
    Dim txtReportID As TextBox
    Dim txtViewID As TextBox
    Dim txtViewName As TextBox
    Public ReportID As Long
    Dim pHeading As Panel
    Dim pViewType As Panel
    Dim radViewType As RadioButtonList
    Public ViewType As String
    Dim msHide As String = "" '"d-none" '*** CSS for hiding elements
    '  Dim lblField1 As Label
    '  Dim lblField2 As Label
    '  Dim ctrlField1 As DropDownList
    '  Dim ctrlField2 As DropDownList
    ' Dim CustomValidator1 As CustomValidator
    ' Dim trig As AsyncPostBackTrigger
    Dim cn As clsDataLoader



    Protected Overrides Sub CreateChildControlsSub()
        'debug.print("<clsReportColumns.CreateChildControlsSub>")

        Controls.Clear()

        uPanel1 = New UpdatePanel
        uPanel1.ID = "uPanel1"
        uPanel1.Attributes("class") = "form-group"
        txtReportID = New TextBox
        txtReportID.ID = "txtReportID"
        txtReportID.ReadOnly = True
        txtReportID.CssClass = msHide
        txtReportID.Text = ReportID
        lblViewTitle = New Label
        lblViewTitle.ID = "lblViewTitle"
        lblViewTitle.AssociatedControlID = "txtViewName"
        lblViewTitle.Text = "View Name"


        txtViewName = New TextBox
        txtViewName.ID = "txtViewName"
        txtViewName.BackColor = Color.Azure
        txtViewID = New TextBox
        txtViewName.ToolTip = ReportID

        gv = New GridView
        gv.ID = "gvViewColumns"
        gv.AutoGenerateColumns = False
        gv.ClientIDMode = ClientIDMode.Static



        '// Dynamically create columns to display the desired
        '// fields from the data source. Columns that are 
        '// dynamically added to the GridView control are Not persisted 
        '// across posts And must be recreated each time the page Is 
        '// loaded.

        '// Create a BoundField object to display an author's last name.
        Dim txtFieldName As TemplateField = New TemplateField()
        txtFieldName.HeaderText = "Field Name"
        txtFieldName.ItemTemplate = New textBoxTemplateImpl("FieldName", True, False)
        txtFieldName.ItemStyle.CssClass = "form-control-plaintext" '.hidden-xl-down"
        txtFieldName.ItemStyle.BorderStyle = BorderStyle.None
        txtFieldName.HeaderStyle.CssClass = ".hidden-xl-down"


        Dim txtID As TemplateField = New TemplateField()
        txtID.HeaderText = "ID"
        txtID.ItemTemplate = New textBoxTemplateImpl("ID", True, True)
        txtID.ItemStyle.CssClass = ""
        txtID.HeaderStyle.CssClass = ""


        '*** Create a CheckBoxField object to indicate if the column is Visible
        Dim chkVisible As TemplateField = New TemplateField()
        chkVisible.ItemTemplate = New CheckBoxTemplateImpl("ColumnVisible")
        'chkVisible. = "tr:hover {background-color: #f5f5f5;}  text-align: center;"
        chkVisible.HeaderText = "Visible"

        '*** Create the Column Title field that users can change the description
        Dim txtColumnName As TemplateField = New TemplateField()
        txtColumnName.ItemTemplate = New textBoxTemplateImpl("ColumnName", False, False)
        txtColumnName.HeaderText = "Column Name"



        '*** Create list items for Format dropdown
        Dim Items As ListItemCollection = New ListItemCollection
        Dim m As New ListItem
        Items.Add(New ListItem("null", Nothing))
        Items.Add(New ListItem("None", ""))
        Items.Add(New ListItem("Currency", "{0:c}"))
        Items.Add(New ListItem("Short Date", "{0:d}"))
        Items.Add(New ListItem("long Date", "{0:e}"))

        Dim ddlColumnFormat As TemplateField = New TemplateField()
        ddlColumnFormat.ItemTemplate = New DropDownListTemplateImpl("ColumnFormat", False, False, Items)
        ddlColumnFormat.HeaderText = "Column Format"

        '*** Create the Format Textbox
        Dim txtFormat As TemplateField = New TemplateField()
        txtFormat.ItemTemplate = New textBoxTemplateImpl("ColumnFormat", False, False)
        txtFormat.HeaderText = "Format"

        gv.CssClass = "table table-sm table-bordered"
        gv.Columns.Add(txtID)
        gv.Columns.Add(txtFieldName)
        gv.Columns(1).ControlStyle.BorderStyle = BorderStyle.None
        gv.Columns(1).ItemStyle.BorderStyle = BorderStyle.Inset

        gv.Columns.Add(txtColumnName)
        gv.Columns.Add(chkVisible)
        gv.Columns.Add(ddlColumnFormat)
        gv.Columns.Add(txtFormat)
        gv.EmptyDataText = "There is nothing to display!"
        AddHandler gv.RowDataBound, AddressOf gv_RowDataBound
        AddHandler gv.DataBound, AddressOf gv_Nothing

        '  <div Class="form-check form-check-inline">
        '    <input Class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="option1">
        '    <Label Class="form-check-label" for="inlineRadio1">1</label>
        '</div>
        '<div Class="form-check form-check-inline">
        '<input Class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
        '   <Label Class="form-check-label" for="inlineRadio2">2</label>
        '</div>

        radViewType = New RadioButtonList
        radViewType.Items.Add("User")
        radViewType.Items.Add("Global")
        If ViewType = "Global" Then
            radViewType.SelectedValue = "Global"
        Else
            radViewType.SelectedValue = "User"
        End If
        radViewType.CssClass = "form-check form-check-inline"
        radViewType.RepeatDirection = RepeatDirection.Horizontal
        radViewType.Items(0).Attributes.CssStyle.Add("margin", "5px")
        radViewType.Items(1).Attributes.CssStyle.Add("margin", "5px")
        btnSave = New LinkButton
        btnSave.ID = "btnSave"
        btnSave.Text = "<i class='fas fa-save fa-1x'></i>"
        btnSave.CssClass = ("btn btn-link")
        AddHandler btnSave.Click, AddressOf btnSave_Click

        btnDelete = New LinkButton
        btnDelete.ID = "btnDelete"
        btnDelete.Text = "<i class='fa fa-trash fa-1x' ></i>"
        btnDelete.CssClass = ("btn btn-link")
        AddHandler btnDelete.Click, AddressOf btnDelete_Click

        lblViewTitle.CssClass = "sr-only"
        txtViewID.CssClass = msHide & " form-control sr-only"
        txtViewName.CssClass = "form-control"
        txtViewName.Attributes("placeholder") = "View Name"


        pHeading = New Panel
        pHeading.CssClass = "input-group my-1"


        pHeading.Controls.Add(txtReportID)
        pHeading.Controls.Add(txtViewID)
        pHeading.Controls.Add(lblViewTitle)
        pHeading.Controls.Add(txtViewName)
        pHeading.Controls.Add(radViewType)
        pHeading.Controls.Add(btnSave)
        pHeading.Controls.Add(btnDelete)

        Controls.Add(pHeading)
        uPanel1.ContentTemplateContainer.Controls.Add(gv)
        Controls.Add(uPanel1)
        Dim liReportID As Integer = Val(HttpContext.Current.Request("rpt"))
        If liReportID > 0 Then
            bindColumnsgv(liReportID)
        End If
        gv.DataBind()

        'debug.print("</clsComboBox.CreateChildControlsSub>")
    End Sub
    Protected Sub gv_Nothing()
        'debug.print("<DataBoundGV />")
    End Sub
    Protected Sub gv_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        'debug.print("<Binding>" & e.Row.RowType.ToString)
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim row As GridViewRow = e.Row
            row.Cells(0).Text = "<i class='fas fa-bars fa-1x'></i>"
            row.Attributes("ID") = "ROW" & e.Row.RowIndex.ToString()
            row.Attributes("NAME") = "ROW" & e.Row.RowIndex.ToString()
            row.Attributes("Id") = "ROW" & e.Row.RowIndex.ToString()
            row.Attributes("Name") = "ROW" & e.Row.RowIndex.ToString()
            row.ClientIDMode = ClientIDMode.Predictable
        End If
        'debug.print("<Binding>" & e.Row.RowType.ToString)
    End Sub

    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub


    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        '  txtViewID.RenderControl(writer)
        '  lblViewTitle.RenderControl(writer)
        '  txtViewName.RenderControl(writer)
        '  btnSave.RenderControl(writer)
        '  txtReportID.RenderControl(writer)
        pHeading.RenderControl(writer)
        uPanel1.RenderControl(writer)

    End Sub


    '*** From .vb codebehind
    'Dim maList1Values(1000, 2) As Array
    'Dim malist2Values(1000, 2) As Array
    'Public Property List1Source() As Array
    '    Get
    '        List1Source = maList1Values
    '    End Get
    '    Set(ByVal value As Array)
    '        maList1Values = value
    '        refreshLists()
    '    End Set
    'End Property
    'Public Property List2Source() As Array
    '    Get
    '        List2Source = malist2Values
    '    End Get
    '    Set(ByVal value As Array)
    '        'debug.print("<clsComboBox.Set.List2Source>")
    '        malist2Values = value
    '        '         RefreshList2()
    '        'debug.print("</clsComboBox.Set.List2Source>")
    '    End Set
    'End Property
    'Public Overrides Sub refreshLists()
    '    'debug.print("<clsComboBox.refreshLists>")
    '    Dim rli As New ReportListItem
    '    Dim li As New ListItem
    '    '*** Clear the Items from the List
    '    ctrlField1.Items.Clear()
    '    ctrlField2.Items.Clear()

    '    '*** Refresh the ReportListITems Collection
    '    '*** NOT DONE YET

    '    '*** Iterate through the list Items and add to list Control
    '    For Each rli In mrptCtrl.ListItems
    '        li = New ListItem
    '        li.Value = rli.Value
    '        li.Text = rli.Description
    '        ctrlField1.Items.Add(li)
    '        ' If ctrlField2.Visible Then
    '        ctrlField2.Items.Add(li)
    '        'End If
    '        li = Nothing
    '    Next
    '    'debug.print("</clsComboBox.refreshLists>")
    'End Sub
    'Public Overrides Sub ForceValidation()
    '    'debug.print("<clsComboBox.ForceValidation>")
    '    Dim lsReason As String = ""
    '    mcSelectedItems.Clear()
    '    mcSelectedItems2.Clear()
    '    If msDataType = "" Then msDataType = "Text"

    '    If ctrlField1.Text <> "" Then
    '        miSelectedItem.Value = CStr(ctrlField1.Text)
    '        miSelectedItem.Description = CStr(ctrlField1.Text)
    '        mcSelectedItems.Add(miSelectedItem)
    '    End If

    '    If ctrlField2.Visible And CStr(ctrlField2.Text) <> "" Then
    '        miSelectedItem2.Value = CStr(ctrlField2.Text)
    '        miSelectedItem2.Description = CStr(ctrlField2.Text)
    '        mcSelectedItems2.Add(miSelectedItem2)
    '    End If

    '    mbValid = Validate()
    '    'debug.print("</clsComboBox.ForceValidation>")
    'End Sub
    'Public Sub New()
    '    mrptCtrl = New ReportControl
    'End Sub
    'Public Sub New(ByRef lrptctrl As ReportControl)
    '    mrptCtrl = lrptctrl
    'End Sub
    'Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles UpdatePanel1.DataBinding
    '    'debug.print("<clsComboBox.UpdatePanel1_DataBinding>")

    '    'debug.print("</clsComboBox.UpdatePanel1_DataBinding>")
    'End Sub
    Protected Sub Pagie_iLoad(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles Me.Load
        '  On Error Resume Next
    End Sub
    Public Sub SetColumns(liReportID As Integer)
        ' coldataview = dv
        txtReportID.Text = liReportID
        bindColumnsgv(liReportID)

    End Sub
    Sub bindColumnsgv(liReportID As Integer)
        '**************************************
        '*** Get the Columns for the Column Picker
        '*** Grid View.  
        '***  1. Get Report ID
        '***  2. Check if there is a saved view for that Report
        '***  3. If so Get those columns
        '***  4. If Not Get the columns for that Report
        Dim gc As DataColumn
        Dim cn As New clsDataLoader
        Dim ds As New DataSet
        Dim lsSQL As String
        Dim WhereClause As String = " 1=1 "
        Dim lsViewID As String = ""
        Dim dt As DataTable = New DataTable()
        Dim dtv As New DataTable ' *** Saved View Definition

        txtViewName.Text = ""
        '**********
        '*** Get The Current View ID
        '**********
        lsViewID = GetViewID().ToString
        If lsViewID = "00000000-0000-0000-0000-000000000000" Then
            '*** TThere is no View ID, So this is a NEW View

            '*** Load the Rerport Definition and retreive the Select Statement
            If liReportID = 0 Then Exit Sub
            If cn.LoadReport(liReportID).Tables.Count = 0 Then Exit Sub
            dtv = cn.LoadReport(liReportID).Tables(0)
            If IsDBNull(dtv.Rows(0)("SelectStatement")) Then Exit Sub
            lsSQL = dtv.Rows(0)("SelectStatement")

            '*** Does the Report return a dataset?
            ds = cn.LoadReportResults(lsSQL, WhereClause, liReportID)
            If ds Is Nothing Then Exit Sub
            If ds.Tables.Count = 0 Then
                Exit Sub
            End If

            '*** This should have columns if there is no VIew
            dtv = ds.Tables(0)

            '*** Load VIew Definition from Database
            ' g = Me.Page.FindControl("gvResults")

            '*** Create the table that will be bound
            dt.Columns.Add("ID")
            dt.Columns.Add("VIEWID")
            dt.Columns.Add("FieldName")
            dt.Columns.Add("ColumnName")
            dt.Columns.Add("ColumnVisible", GetType(Boolean))
            dt.Columns.Add("ColumnFormat") 'DDLFormat
            dt.Columns.Add("Format") 'Textbox Format
            Dim dr As DataRow = dt.NewRow()

            '*** NO Saved View. Populate table with Fields from Result
            For Each gc In dtv.Columns
                dr = dt.NewRow
                dr("VIEWID") = lsViewID
                dr("FieldName") = gc.ColumnName.ToLower
                dr("ColumnName") = gc.Caption
                dr("ColumnVisible") = True
                dr("ColumnFormat") = "" ' gc.DataType.ToString
                dt.Rows.Add(dr)
            Next
            txtViewID.Text = lsViewID
            txtViewName.Text = ""
            txtViewName.Attributes.Add("data-orig", txtViewName.Text)
        Else
            '*** View Definition Does Exist .  Load Columns from there
            txtViewID.Text = lsViewID

            ds = cn.LoadReportViewColumns(lsViewID)
            If ds.Tables(0).Rows.Count > 0 Then
                txtViewName.Text = ds.Tables(0).Rows(0)(0).ToString
                txtViewName.Attributes.Add("data-orig", txtViewName.Text)
                ViewType = ds.Tables(0).Rows(0)(1).ToString
                If txtViewName.Text = "Default" Then
                    radViewType.SelectedValue = "Global"
                    radViewType.Items(0).Enabled = False
                Else
                    radViewType.SelectedValue = ViewType
                    radViewType.Items(0).Enabled = True
                End If

                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                End If
            End If
        End If

        '*** You can then bind your GridView to the DataTable...
        gv.DataSource = dt
        gv.DataBind()

    End Sub
    Public Property GetViewID As Guid
        Get
            Dim lsTemp, lsViewID As String
            Dim liTemp As Integer
            lsViewID = "00000000-0000-0000-0000-000000000000"
            For Each s In HttpContext.Current.Request.Form.Keys
                '*** Are we on the Grid Control
                If Not s Is Nothing Then
                    'debug.print(s.ToString() + ":" + HttpContext.Current.Request.Form(s) + " ")
                    lsTemp = s.ToString

                    '*** Get the View ID
                    liTemp = InStr(lsTemp, "ddlView")
                    If liTemp > 0 Then
                        lsViewID = HttpContext.Current.Request.Form(s)
                        Exit For
                    End If

                End If
            Next
            GetViewID = New Guid(lsViewID)
        End Get
        Set(value As Guid)

        End Set
    End Property

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'debug.print("<ctrlReportColumns_btnSave_CLick>")
        Dim liTemp As Integer
        Dim lsTemp As String
        Dim lsRow As String = "0"
        Dim liRow As Integer = 0
        Dim lsVals(200, 6) As String
        Dim lsval As String
        Dim liIndex As Integer
        Dim liSortOrder As Integer = 0
        Dim lsViewID As String
        Dim lsViewName As String = ""
        Dim lsFormat As String
        Dim lsViewNameOrig As String = txtViewName.Attributes("data-orig")
        lsViewID = txtViewID.Text
        ViewType = radViewType.SelectedValue
        'SAVE ROUTINE

        Dim s As String
        For Each s In HttpContext.Current.Request.Form.Keys
            If Not IsNothing(s) Then
                '*** Are we on the Grid Control
                'debug.print(s.ToString() + ":" + HttpContext.Current.Request.Form(s) + " ")
                lsTemp = s.ToString

                '*** Get the View ID
                liTemp = InStr(lsTemp, "ddlView")
                If liTemp > 0 Then
                    lsViewID = HttpContext.Current.Request.Form(s)

                End If

                '*** Get the New View Name if one was entered
                liTemp = InStr(lsTemp, "ViewName")
                If liTemp > 0 Then
                    lsViewName = HttpContext.Current.Request.Form(s)
                End If

                '*** Get the Column Data Values
                liTemp = InStr(lsTemp, "gvViewColumns$ctl")
                If liTemp > 0 Then
                    lsTemp = Right(lsTemp, lsTemp.Length - liTemp - 16)
                    'debug.print(s.ToString() + ":" + HttpContext.Current.Request.Form(s) + " ")
                    lsRow = Left(lsTemp, InStr(lsTemp, "$") - 1)
                    If liRow <> Val(lsRow) Then
                        liSortOrder = liSortOrder + 1
                        liRow = Val(lsRow)
                    End If
                    liTemp = InStr(lsTemp, "$")
                    lsval = Right(lsTemp, lsTemp.Length - liTemp)
                    Select Case lsval.ToUpper
                        Case "ID" : liIndex = 1
                        Case "FIELDNAME" : liIndex = 2
                        Case "COLUMNNAME" : liIndex = 3
                        Case "COLUMNFORMAT", "FORMAT" : liIndex = 4
                        Case "VISIBLE", "COLUMNVISIBLE" : liIndex = 5
                        Case Else : liIndex = 6
                    End Select
                    lsVals(Val(lsRow), 0) = liSortOrder
                    lsVals(Val(lsRow), liIndex) = HttpContext.Current.Request.Form(s)
                End If
            End If
        Next

        '*** if New Name was entered Set the New View ID to a new GUID
        If lsViewName <> lsViewNameOrig Then
            lsViewID = ""
        End If

        '*** Now we have the View ID and the Control Array, Add the Array as ReportColumn Object Instances
        Dim c As New ReportColumns
        c.Columns = New Collection
        Dim r As ReportColumn

        Dim liUpperBound As Integer = lsVals.GetUpperBound(0)

        Dim liLowerBound As Integer = 3
        For licounter = 2 To liUpperBound
            If Not lsVals(licounter, 2) Is Nothing Then
                liLowerBound = licounter
                Exit For
            End If
        Next
        For licounter = liUpperBound To 2 Step -1
            If Not lsVals(licounter, 2) Is Nothing Then
                liUpperBound = licounter
                Exit For
            End If
        Next
        For licounter = liLowerBound To liUpperBound
            r = New ReportColumn
            r.ColumnOrder = lsVals(licounter, 0)    '** Sort Order
            r.ID = Guid.NewGuid.ToString            'lsVals(licounter, 1)            '** ID
            r.FieldName = lsVals(licounter, 2)      '** Field Name
            r.ColumnName = lsVals(licounter, 3)     '** Column Name
            lsFormat = lsVals(licounter, 4)         '** Format
            If lsFormat Is Nothing Then lsFormat = ""
            r.Format = IIf(lsFormat.ToUpper = "NULL", "", lsFormat)
            r.Visible = If(lsVals(licounter, 5) = "on", True, False) '** Visible
            r.ViewID = lsViewID
            If Not r.FieldName Is Nothing Then
                c.Columns.Add(r)
            End If

        Next

        c.ViewID = lsViewID
        If lsViewName = "" Then lsViewName = "Default"
        c.ViewName = lsViewName
        If ReportID = 0 Then
            ReportID = Val(txtReportID.Text)
        End If
        c.ReportID = ReportID
        c.ViewType = ViewType
        c.Save()



        '     Exit
        'Dim locationIds As String() = HttpContext.Current.Request.Form("ID").Split(",")
        'Dim preference As Integer = 1
        'For Each locationId As Integer In locationIds
        '    Me.UpdatePreference(locationId, preference)
        '    preference += 1
        'Next

        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
        'debug.print("</ctrlReportColumns_btnSave_CLick>")
    End Sub
    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'debug.print("<ctrlReportColumns_btnDelete_CLick>")
        Dim lsViewID As String

        lsViewID = txtViewID.Text
        If lsViewID = "" Then Exit Sub

        '*** Now we have the View ID and the Control Array, Add the Array as ReportColumn Object Instances
        Dim c As New ReportColumns
        c.ViewID = lsViewID

        c.DeleteView()


        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
        'debug.print("</ctrlReportColumns_btnDelete_CLick>")
    End Sub
    Private Sub UpdatePreference(locationId As Integer, preference As Integer)
        Dim constr As String = ConfigurationManager.ConnectionStrings("constr").ConnectionString
    End Sub
End Class
Public Class CheckBoxTemplateImpl
    Implements ITemplate

    Public Property ColumnName As String
    Public Sub New(columnName As String)
        Me.ColumnName = columnName
    End Sub

    Public Sub InstantiateIn(container As Control) Implements ITemplate.InstantiateIn
        Dim chkBox As New CheckBox()
        chkBox.ID = ColumnName
        chkBox.ClientIDMode = ClientIDMode.Predictable
        AddHandler chkBox.DataBinding, AddressOf chkBox_DataBinding
        container.Controls.Add(chkBox)
    End Sub

    Private Sub chkBox_DataBinding(sender As Object, args As EventArgs)
        Dim chkBox As CheckBox = DirectCast(sender, CheckBox)
        Dim gridViewRow As GridViewRow = DirectCast(chkBox.NamingContainer, GridViewRow)
        Dim bindValue As Object = DataBinder.Eval(gridViewRow.DataItem, ColumnName)
        If bindValue IsNot Nothing Then
            '  chkBox.Text = bindValue.ToString()
            chkBox.Checked = bindValue
        End If
    End Sub
End Class
Public Class textBoxTemplateImpl
    Implements ITemplate
    Public Property misReadOnly As Boolean
    Public Property ColumnName As String
    Public Property Hidden As Boolean
    Public Sub New(columnName As String, isReadOnly As Boolean, isHidden As Boolean)
        Me.ColumnName = columnName
        misReadOnly = isReadOnly
        Hidden = isHidden
    End Sub

    Public Sub InstantiateIn(container As Control) Implements ITemplate.InstantiateIn
        Dim txt As New TextBox()
        If Hidden Then
            txt.Style.Add("visibility", "hidden")
            txt.Width = 0
        End If
        txt.CssClass = "no-border"
        txt.ID = ColumnName
        If misReadOnly Then
            txt.ReadOnly = True

        End If
        txt.ClientIDMode = ClientIDMode.Predictable
        '    txt.UniqueID = ColumnName
        AddHandler txt.DataBinding, AddressOf txt_DataBinding
        container.Controls.Add(txt)
        ' container.Controls.Add(New Image(  "l"))
    End Sub

    Private Sub txt_DataBinding(sender As Object, args As EventArgs)
        Dim txt As TextBox = DirectCast(sender, TextBox)
        Dim gridViewRow As GridViewRow = DirectCast(txt.NamingContainer, GridViewRow)
        Dim bindValue As Object = DataBinder.Eval(gridViewRow.DataItem, ColumnName)
        If bindValue IsNot Nothing Then
            Dim lsBindValue As String = bindValue.ToString
            If lsBindValue <> "" Then
                '  chkBox.Text = bindValue.ToString()
                txt.Text = bindValue.ToString
            End If
        End If
    End Sub
End Class

Public Class DropDownListTemplateImpl
    Implements ITemplate
    Public Property misReadOnly As Boolean
    Public Property ColumnName As String
    Public Property Hidden As Boolean
    Public Property Items As ListItemCollection
    Public Sub New(columnName As String, isReadOnly As Boolean, isHidden As Boolean, ddlItems As ListItemCollection)
        Items = ddlItems
        Me.ColumnName = columnName
        misReadOnly = isReadOnly
        Hidden = isHidden
    End Sub

    Public Sub InstantiateIn(container As Control) Implements ITemplate.InstantiateIn
        Dim ddl As New DropDownList()
        ddl.ID = String.Format("ddl{0}", ColumnName)
        Dim li As ListItem
        For Each li In Items
            ddl.Items.Add(li)
        Next


        If Hidden Then
            ddl.Style.Add("visibility", "hidden")
            ddl.Width = 0
        End If
        ddl.CssClass = "no-border"
        ddl.ID = "ddl" & ColumnName & Int(Rnd() * 1000).ToString

        ddl.ClientIDMode = ClientIDMode.Predictable
        AddHandler ddl.DataBound, AddressOf ddl_DataBinding
        container.Controls.Add(ddl)

    End Sub

    Private Sub ddl_DataBinding(sender As Object, args As EventArgs)
        'debug.print("<BindingDDL>" & sender.ToString)
        Dim ddl As DropDownList = DirectCast(sender, DropDownList)
        ddl.ID = String.Format("ddl{0}", ColumnName) & Int(Rnd() * 1000).ToString
        Dim gridViewRow As GridViewRow = DirectCast(ddl.NamingContainer, GridViewRow)


        Dim bindValue As Object = DataBinder.Eval(gridViewRow.DataItem, ColumnName)
        'ddl.SelectedValue = ""
        If bindValue IsNot Nothing Then
            Dim lsBindValue As String = bindValue.ToString
            If lsBindValue <> "" Then
                '  chkBox.Text = bindValue.ToString()
                '       ddl.SelectedValue = bindValue.ToString
                ddl.Attributes("data-val") = bindValue.ToString
                Dim i As ListItem
                Dim liCounter As Integer
                For Each i In ddl.Items
                    If i.Value = lsBindValue Then
                        ddl.SelectedIndex = liCounter
                    End If
                    liCounter = liCounter + 1
                Next
            End If
        End If
        'debug.print("<BindingDDL>" & sender.ToString)
    End Sub



End Class
Class ReportColumns
    Public Columns As Collection
    Public ViewID As String
    Public ViewName As String = "Default"
    Public ReportID As Long = 0
    Public ViewType As String
    Public Sub Save()
        If Columns.Count > 0 Then
            Dim cn As New clsDataLoader
            If ViewID = "" Or ViewID = "00000000-0000-0000-0000-000000000000" Then
                ViewID = Guid.NewGuid.ToString
                cn.InsertReportView(ViewID, ViewName, ViewType, HttpContext.Current.Request.Path, ReportID, HttpContext.Current.Session("NodeID"))
            Else
                '*** Delete the existing DB Records for the View if they already exist
                cn.DeleteReportViewColumns(ViewID)
                cn.UpdateReportViewScope(ViewID, ViewType, HttpContext.Current.Session("NodeID"))
            End If
            For Each c As ReportColumn In Columns
                If Not c.FieldName Is Nothing Then
                    cn.InsertReportViewColumns(ViewID, c.ID, c.FieldName, c.ColumnName, c.Format, c.ColumnOrder, c.Visible)
                End If
            Next
        End If
    End Sub
    Public Sub DeleteView()
        'If Columns.Count > 0 Then
        Dim cn As New clsDataLoader
        If ViewID = "" Or ViewID = "00000000-0000-0000-0000-000000000000" Then
            Exit Sub
        Else
            '*** Delete the existing DB Records for the View if they already exist
            cn.DeleteReportView(ViewID)
        End If
        '  End If
    End Sub
End Class

Class ReportColumn
    Public ViewID As String

    Public ColumnName As String
    Public FieldName As String
    Public ColumnOrder As Integer
    Public Format As String
    Public Visible As Boolean
    Public mID As String
    Public ID As String

End Class
