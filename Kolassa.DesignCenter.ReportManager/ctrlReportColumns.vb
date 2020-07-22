Option Strict Off
Imports System.ComponentModel
Imports System.Drawing
Imports System.Web.HttpContext

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlReportColumns
    Inherits ctrlBase
    Dim uPanel1 As UpdatePanel
    Dim gv As GridView
    Dim btnSave As Button
    Public coldataview As DataView
    Dim lblViewTitle As Label
    ' Dim DropDownList1 As DropDownList
    Dim txtViewID As TextBox
    Dim txtViewName As TextBox
    '  Dim lblField1 As Label
    '  Dim lblField2 As Label
    '  Dim ctrlField1 As DropDownList
    '  Dim ctrlField2 As DropDownList
    ' Dim CustomValidator1 As CustomValidator
    ' Dim trig As AsyncPostBackTrigger
    Dim cn As clsDataLoader



    Protected Overrides Sub CreateChildControlsSub()
        Debug.Print("<clsReportColumns.CreateChildControlsSub>")

        Controls.Clear()

        uPanel1 = New UpdatePanel
        uPanel1.ID = "uPanel1"
        uPanel1.Attributes("class") = "form-group"
        lblViewTitle = New Label
        lblViewTitle.ID = "lblViewTitle"
        lblViewTitle.AssociatedControlID = "txtViewName"
        lblViewTitle.Text = "View Name"

        txtViewName = New TextBox
        txtViewName.ID = "txtViewName"
        txtViewName.BackColor = Color.Azure
        txtViewID = New TextBox

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
        txtFieldName.ItemStyle.CssClass = ".hidden-xl-down"
        txtFieldName.HeaderStyle.CssClass = ".hidden-xl-down"


        Dim txtID As TemplateField = New TemplateField()
        txtID.HeaderText = "ID"
        txtID.ItemTemplate = New textBoxTemplateImpl("ID", True, True)
        txtID.ItemStyle.CssClass = ""
        txtID.HeaderStyle.CssClass = ""


        '// Create a CheckBoxField object to indicate whether the author
        '// Is on contract.
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

        gv.CssClass = "table-sm"
        gv.Columns.Add(txtID)
        gv.Columns.Add(txtFieldName)
        gv.Columns.Add(txtColumnName)
        gv.Columns.Add(chkVisible)
        gv.Columns.Add(ddlColumnFormat)
        gv.Columns.Add(txtFormat)
        gv.EmptyDataText = "There is nothing to display!"
        AddHandler gv.RowDataBound, AddressOf gv_RowDataBound
        AddHandler gv.DataBound, AddressOf gv_Nothing



        btnSave = New Button
        btnSave.ID = "btnSave"
        btnSave.Text = "Save"
        AddHandler btnSave.Click, AddressOf btnSave_Click


        lblViewTitle.CssClass = "form-control"
        txtViewID.CssClass = "form-control"
        txtViewName.CssClass = "form-control"
        btnSave.CssClass = ("btn-primary")
        'Controls.Add(lblFieldName)
        'Controls.Add(DropDownList1)
        Controls.Add(btnSave)
        Controls.Add(txtViewID)
        Controls.Add(lblViewTitle)
        Controls.Add(txtViewName)

        uPanel1.ContentTemplateContainer.Controls.Add(gv)
        Controls.Add(uPanel1)
        Dim liReportID As Integer = Val(HttpContext.Current.Request("rpt"))
        If liReportID > 0 Then
            bindColumnsgv(liReportID)
        End If
        gv.DataBind()

        Debug.Print("</clsComboBox.CreateChildControlsSub>")
    End Sub
    Protected Sub gv_Nothing()
        Debug.Print("<DataBoundGV />")
    End Sub
    Protected Sub gv_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        Debug.Print("<Binding>" & e.Row.RowType.ToString)
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim row As GridViewRow = e.Row
            row.Attributes("ID") = "ROW" & e.Row.RowIndex.ToString()
            row.Attributes("NAME") = "ROW" & e.Row.RowIndex.ToString()
            row.Attributes("Id") = "ROW" & e.Row.RowIndex.ToString()
            row.Attributes("Name") = "ROW" & e.Row.RowIndex.ToString()
            row.ClientIDMode = ClientIDMode.Predictable
        End If
        Debug.Print("<Binding>" & e.Row.RowType.ToString)
    End Sub

    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub


    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        txtViewID.RenderControl(writer)
        lblViewTitle.RenderControl(writer)
        txtViewName.RenderControl(writer)
        btnSave.RenderControl(writer)
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
    '        Debug.Print("<clsComboBox.Set.List2Source>")
    '        malist2Values = value
    '        '         RefreshList2()
    '        Debug.Print("</clsComboBox.Set.List2Source>")
    '    End Set
    'End Property
    'Public Overrides Sub refreshLists()
    '    Debug.Print("<clsComboBox.refreshLists>")
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
    '    Debug.Print("</clsComboBox.refreshLists>")
    'End Sub
    'Public Overrides Sub ForceValidation()
    '    Debug.Print("<clsComboBox.ForceValidation>")
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
    '    Debug.Print("</clsComboBox.ForceValidation>")
    'End Sub
    'Public Sub New()
    '    mrptCtrl = New ReportControl
    'End Sub
    'Public Sub New(ByRef lrptctrl As ReportControl)
    '    mrptCtrl = lrptctrl
    'End Sub
    'Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles UpdatePanel1.DataBinding
    '    Debug.Print("<clsComboBox.UpdatePanel1_DataBinding>")

    '    Debug.Print("</clsComboBox.UpdatePanel1_DataBinding>")
    'End Sub
    Protected Sub Pagie_iLoad(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles Me.Load
        '  On Error Resume Next
    End Sub
    Public Sub SetColumns(liReportID As Integer)
        ' coldataview = dv
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
        '**********
        '*** Get The Current View ID
        '**********
        lsViewID = GetViewID().ToString
        If lsViewID = "00000000-0000-0000-0000-000000000000" Then


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

        Else
            '*** View Definition Does Exist .  Load Columns from there
            ds = cn.LoadReportViewColumns(lsViewID)
            If ds.Tables.Count > 0 Then
                dt = ds.Tables(0)
            End If
        End If

        'You can then bind your GridView to the DataTable...
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
                    Debug.Print(s.ToString() + ":" + HttpContext.Current.Request.Form(s) + " ")
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
        Debug.Print("<ctrlReportColumns_btnSave_CLick>")
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
        lsViewID = txtViewID.Text

        'SAVE ROUTINE
        '   Me.gv.Rows.Count
        Dim s As String
        For Each s In HttpContext.Current.Request.Form.Keys
            '*** Are we on the Grid Control
            Debug.Print(s.ToString() + ":" + HttpContext.Current.Request.Form(s) + " ")
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
                Debug.Print(s.ToString() + ":" + HttpContext.Current.Request.Form(s) + " ")
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
        Next

        '*** if New Name was entered Set the New View ID to a new GUID
        If lsViewName <> "" Then
            lsViewID = ""
        End If

        '*** Now we have the View ID and the Control Array, Add the Array as ReportColumn Object Instances
        Dim c As New ReportColumns
        c.Columns = New Collection
        Dim r As ReportColumn
        Dim liUpperBound As Integer = lsVals.GetUpperBound(0)
        For licounter = 2 To liUpperBound
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
            If r.FieldName Is Nothing Then Exit For
            c.Columns.Add(r)
        Next

        c.ViewID = lsViewID
        If lsViewName = "" Then lsViewName = "Default"
        c.ViewName = lsViewName
        c.Save()



        '     Exit
        'Dim locationIds As String() = HttpContext.Current.Request.Form("ID").Split(",")
        'Dim preference As Integer = 1
        'For Each locationId As Integer In locationIds
        '    Me.UpdatePreference(locationId, preference)
        '    preference += 1
        'Next

        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)
        Debug.Print("</ctrlReportColumns_btnSave_CLick>")
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
        Debug.Print("<BindingDDL>" & sender.ToString)
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
        Debug.Print("<BindingDDL>" & sender.ToString)
    End Sub



End Class
Class ReportColumns
    Public Columns As Collection
    Public ViewID As String
    Public ViewName As String = "Default"
    Public Sub Save()
        If Columns.Count > 0 Then
            Dim cn As New clsDataLoader
            If ViewID = "" Then
                ViewID = Guid.NewGuid.ToString
                cn.InsertReportView(ViewID, ViewName, "Global", HttpContext.Current.Request.Path, HttpContext.Current.Request.QueryString("rpt"), HttpContext.Current.Session("NodeID"))
            Else
                '*** Delete the existing DB Records for the View if they already exist
                cn.DeleteReportViewColumns(ViewID)
            End If
            For Each c As ReportColumn In Columns
                If Not c.FieldName Is Nothing Then
                    cn.InsertReportViewColumns(ViewID, c.ID, c.FieldName, c.ColumnName, c.Format, c.ColumnOrder, c.Visible)
                End If
            Next
        End If
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
