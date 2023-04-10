Option Strict Off
Imports System.ComponentModel
Imports System.Drawing
Imports System.Web.UI
Imports System.IO

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlCheckBox_ runat=server></{0}:ctrlCheckBox>"), ToolboxBitmap(GetType(Calendar))>
Public Class ReportResults
    Inherits ctrlBase
    Public ds As DataSet
    Public SQL As String
    Public WhereClause, SearchClause As String
    Public liReportID As Integer
    Public SearchCriteria As String
    Public coldataview As DataView

    Dim gvPageNum As Long
    Dim pHeading As Panel
    Dim uPanelResults, uPanelControls As UpdatePanel
    Dim upPrgResults As UpdateProgress
    Dim Sorting As String
    Dim gv As GridView
    Dim lblFieldName As Label
    Dim lblWhere As Label
    Dim DropDownList1 As DropDownList
    Dim lblView As Label
    Dim ddlView As DropDownList
    Dim li As ListItem
    Dim liCol As New ListItemCollection
    Dim lblViewName As Label
    Dim lblField1 As Label
    Dim lblField2 As Label
    Dim ctrlField1 As DropDownList
    Dim ctrlField2 As DropDownList
    Dim CustomValidator1 As CustomValidator
    Dim trig, trigView, trigControl As AsyncPostBackTrigger
    Dim cn As clsDataLoader
    Dim imgSort As ImageButton
    Dim lnkDel, lnkImg, lnkImg2 As LinkButton
    Dim radShowAll As RadioButtonList
    Dim msHide As String = "d-none " ' *** Hides CSS Element
    Public ShowAll As String
    ' Public WhereDesc As String
    'Public ReportDescription As String
    ' Dim btnSave As Button 'Doing this at the page level
    'Dim btnMike As Button
    'Dim btnPrintAllPages, btnPrintCurrentPage, btnExportToExcel As Button

    Protected Overrides Sub CreateChildControlsSub()
        'debug.print("<clsReportResults.CreateChildControlsSub>")
        Controls.Clear() '*** Clear all Controls from collection

        lblViewName = New Label
        lblViewName.ID = "lblViewName"
        lblViewName.Text = "The View"
        lblViewName.CssClass = msHide & " input-group-prepend"
        '*** LOAD VIEWS DDL
        '*** Get Views From Database
        pHeading = New Panel
        pHeading.CssClass = "input-group my-1"
        lblView = New Label
        lblView.Text = "View: "
        lblView.CssClass = "input-group-text"
        ddlView = New DropDownList
        ddlView.ID = "ddlView"
        ddlView.CssClass = "custom-select selectpicker Padding:3px;"
        ddlView.Text = "Default"
        ddlView.ViewStateMode = ViewStateMode.Enabled
        ddlView.AutoPostBack = True
        AddHandler ddlView.SelectedIndexChanged, AddressOf ddlView_SelectedIndexChange
        radShowAll = New RadioButtonList
        radShowAll.Items.Add("Active Only")
        radShowAll.Items.Add("All")
        If ShowAll = "All" Then
            radShowAll.SelectedValue = "All"
        Else
            radShowAll.SelectedValue = "Active Only"
        End If
        radShowAll.CssClass = "form-check form-check-inline"
        radShowAll.RepeatDirection = RepeatDirection.Horizontal
        radShowAll.Items(0).Attributes.CssStyle.Add("margin", "5px")
        radShowAll.Items(1).Attributes.CssStyle.Add("margin", "5px")
        radShowAll.AutoPostBack = True
        AddHandler radShowAll.SelectedIndexChanged, AddressOf radShowAll_SelectedIndexChange
        If cn Is Nothing Then
            cn = New clsDataLoader
        End If
        Dim ds As New DataSet
        If liReportID > 0 Then
            Dim dr As DataRow
            ds = cn.LoadReportViews(liReportID.ToString)
            Dim dt As DataTable
            dt = ds.Tables(0)
            For Each dr In dt.Rows
                li = New ListItem()
                li.Value = dr("ID").ToString
                li.Text = dr("Name")
                ddlView.Items.Add(li)
            Next
        End If
        pHeading.Controls.Add(lblView)
        pHeading.Controls.Add(ddlView)
        pHeading.Controls.Add(radShowAll)
        '*** End LOAD View DDL
        lblWhere = New Label
        lblWhere.ID = "lblWhere"


        If SQL <> "" Then

        End If

        '*** The Update Panel for the View Drop Down Control  This is due to the Selected Item Change event firing correctly
        uPanelControls = New UpdatePanel
        uPanelControls.ID = "uPanelControls"
        uPanelControls.UpdateMode = UpdatePanelUpdateMode.Always
        uPanelControls.EnableViewState = True
        trigControl = New AsyncPostBackTrigger
        trigControl.ControlID = "ddlView"
        trigControl.EventName = "SelectedIndexChanged"
        uPanelControls.Triggers.Add(trigControl)

        '*** The Update Panel
        uPanelResults = New UpdatePanel
        uPanelResults.ID = "uPanelResults"
        uPanelResults.Attributes("class") = "form-group"


        '// Dynamically create columns to display the desired
        '// fields from the data source. Columns that are 
        '// dynamically added to the GridView control are Not persisted 
        '// across posts And must be recreated each time the page Is 
        '// loaded.
        Dim bfFieldName As BoundField = New BoundField()
        bfFieldName.DataField = "FieldName"
        bfFieldName.HeaderText = "Field Name"

        Dim bfColumnName As BoundField = New BoundField()
        bfColumnName.DataField = "ColumnName"
        bfColumnName.HeaderText = "Column Name"


        Dim chkVisible As CheckBoxField = New CheckBoxField()
        chkVisible.DataField = "Visible"
        chkVisible.HeaderText = "Visible"

        Dim bfColumnFormat As BoundField = New BoundField()
        bfColumnFormat.DataField = "ColumnFormat"
        bfColumnFormat.HeaderText = "Format"

        '*** Create the GridView
        gv = New GridView
        gv.CssClass = "table table-hover table-sm table-bordered"

        gv.ID = "gvResults"
        gv.AutoGenerateColumns = False


        '*** Add Columns to the COLUMN DEFINITION GridView
        gv.Columns.Add(bfFieldName)
        gv.Columns.Add(bfColumnName)
        gv.Columns.Add(chkVisible)
        gv.Columns.Add(bfColumnFormat)
        gv.PageSize = 20
        '    gv.Attributes("data-reportdesc") = ReportDescription
        gv.RowStyle.CssClass = "clickable-row"

        If gvPageNum > 0 Then gv.PageIndex = gvPageNum
        gv.AllowPaging = True
        '   gv.AllowCustomPaging = True
        gv.AllowSorting = True
        gv.EmptyDataText = "There is nothing to display!!"
        AddHandler gv.Sorting, AddressOf gv_Sorting
        AddHandler gv.PageIndexChanging, AddressOf gv_PageIndexChanging
        AddHandler gv.RowDataBound, AddressOf gv_RowDataBound
        AddHandler gv.RowCommand, AddressOf gv_Delete
        '*** Add Controls to Main Panel for View Name
        'btnSave = New Button
        'btnSave.ID = "btnSaveVuew"
        'btnSave.Text = "Save View"
        'btnSave.CssClass = "btn btn-primary"
        'AddHandler btnSave.Click, AddressOf btnSave_Click

        'btnPrintAllPages = New Button
        'btnPrintAllPages.ID = "btnPrintAllPages"
        'btnPrintAllPages.Text = "Print All Pages"
        'btnPrintAllPages.CssClass = "btn btn-secondary"
        'AddHandler btnPrintAllPages.Click, AddressOf PrintAllPages

        'btnPrintCurrentPage = New Button
        'btnPrintCurrentPage.ID = "btnPrintCurrentPage"
        'btnPrintCurrentPage.Text = "Print Current Page"
        'btnPrintCurrentPage.CssClass = "btn btn-secondary"
        'AddHandler btnPrintCurrentPage.Click, AddressOf PrintCurrentPage


        'btnExportToExcel = New Button
        'btnExportToExcel.ID = "btnExportToExcel"
        'btnExportToExcel.Text = "Download"
        'btnExportToExcel.CssClass = "btn btn-success"
        'AddHandler btnExportToExcel.Click, AddressOf ExportToExcel

        trig = New AsyncPostBackTrigger
        trig.ControlID = "gvResults"
        trig.EventName = "PageIndexChanging"

        uPanelResults.Triggers.Add(trig)

        trigView = New AsyncPostBackTrigger
        trigView.ControlID = "ddlView"
        trigView.EventName = "SelectedIndexChanged"


        uPanelResults.Triggers.Add(trigView)

        Dim lab As New Label
        lab.ID = "lblPrgTemp"
        lab.Text = "Please wait..."

        ' Create the ProgressTemplate based on a class the implements ITemplate.
        Dim value As New KolassaProgressTemplate
        upPrgResults = New UpdateProgress
        upPrgResults.ProgressTemplate = value

        lblWhere.Text = "Hey Hey"
        ' Add the Label to the UpdateProgress and the UpdateProgress to the form.
        uPanelControls.ContentTemplateContainer.Controls.Add(pHeading)
        '  uPanelControls.ContentTemplateContainer.Controls.Add(ddlView)
        '*** Doing this at the Page Level
        'uPanelControls.ContentTemplateContainer.Controls.Add(btnSave)
        'uPanelControls.ContentTemplateContainer.Controls.Add(btnPrintAllPages)
        'uPanelControls.ContentTemplateContainer.Controls.Add(btnPrintCurrentPage)
        'uPanelControls.ContentTemplateContainer.Controls.Add(btnExportToExcel)

        uPanelControls.ContentTemplateContainer.Controls.Add(lblWhere)
        upPrgResults.Controls.Add(lab)


        upPrgResults.AssociatedUpdatePanelID = uPanelResults.ClientID
        upPrgResults.ID = "upprgResults"
        uPanelResults.ContentTemplateContainer.Controls.Add(gv)

        Controls.Add(upPrgResults)
        Controls.Add(uPanelControls)
        Controls.Add(uPanelResults)

        bindgv()
        Try
            gv.DataBind()
        Catch
            'debug.print(Err.Description)
        End Try

        'debug.print("</clsReportResults.CreateChildControlsSub>")
    End Sub
    Protected Sub gv_Delete(sender As Object, e As GridViewCommandEventArgs)
        'Stop
    End Sub
    Protected Sub ddlView_SelectedIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles DropDownList1.TextChanged
        'debug.print("<ddlView_SelectedIndexChanged>" & ddlView.SelectedValue)
        '     bindgv()
        'debug.print("</ddlView_SelectedIndexChanged>")
    End Sub
    Protected Sub radShowAll_SelectedIndexChange(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles DropDownList1.TextChanged
        'debug.print("<radShowAll_SelectedIndexChanged>" & radShowAll.SelectedValue)
        ShowAll = radShowAll.SelectedValue
        bindgv()
        'debug.print("</radShowAll_SelectedIndexChanged>")
    End Sub
    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub
    Protected Sub gv_Sorting(ByVal sender As Object, ByVal e As GridViewSortEventArgs)
        Dim direction As SortDirection
        Dim sortfield As String = ""
        GridViewSortDirection(gv, e, direction, sortfield)
        Sorting = ""
        If e.SortExpression = "" Then
            Sorting = ""
        Else

            Sorting = " ORDER BY " & sortfield & " " & IIf(direction = SortDirection.Ascending, " ASC ", " DESC ")

        End If

        bindgv()

        '*** Set the header style for the sorted field
        Dim col As DataControlField
        Dim index As Integer
        For Each col In gv.Columns
            If (col.SortExpression = sortfield) Then
                index = gv.Columns.IndexOf(col)
                imgSort = New ImageButton
                imgSort.CssClass = ".height:10px;"
                If direction = SortDirection.Ascending Then
                    gv.Columns(index).HeaderText = gv.Columns(index).HeaderText & " V"
                    '     gv.Columns(index).HeaderStyle.CssClass = "btn btn-outline-primary"
                    imgSort.ImageUrl = "~/images/sort-" + "asc" + ".png"
                Else
                    gv.Columns(index).HeaderText = gv.Columns(index).HeaderText & " ^"
                    '      gv.Columns(index).HeaderStyle.CssClass = "btn btn-outline-danger"
                    imgSort.ImageUrl = "~/images/sort-" + "desc" + ".png"
                End If
                gv.HeaderRow.Cells(index).Controls.Add(imgSort)
            End If
        Next
    End Sub
    Private Sub GridViewSortDirection(g As GridView, e As GridViewSortEventArgs, ByRef d As SortDirection, ByRef f As String)
        f = e.SortExpression
        d = e.SortDirection

        '//Check if GridView control has required Attributes
        If Not (g.Attributes("data-CurrentSortField")) Is Nothing And Not (g.Attributes("data-CurrentSortDir") Is Nothing) Then
            '*** Not Null So there was already a Sorting.  Is the Field the same?
            If (f = g.Attributes("data-CurrentSortField")) Then
                '*** Yes The field is the same, flip the direction
                If g.Attributes("data-CurrentSortDir") = "ASC" Then
                    g.Attributes("data-CurrentSortDir") = "DESC"
                    d = SortDirection.Descending
                Else
                    g.Attributes("data-CurrentSortDir") = "ASC"
                    d = SortDirection.Ascending
                End If
            Else
                g.Attributes("data-CurrentSortField") = f
                g.Attributes("data-CurrentSortDir") = "ASC"
            End If
        Else
            g.Attributes("data-CurrentSortField") = f
            If d = SortDirection.Ascending Then
                g.Attributes.Add("data-CurrentSortDir", "ASC")
            Else
                g.Attributes.Add("data-CurrentSortDir", "DESC")
            End If
        End If
    End Sub



    Protected Overrides Sub RenderSubControls(writer As HtmlTextWriter)
        lblViewName.RenderControl(writer)
        ' lblView.RenderControl(writer)
        ' ddlView.RenderControl(writer)
        pHeading.RenderControl(writer)
        '*** Doing this at the page level
        'btnSave.RenderControl(writer)
        'btnPrintAllPages.RenderControl(writer)
        'btnPrintCurrentPage.RenderControl(writer)
        'btnExportToExcel.RenderControl(writer)


        lblWhere.RenderControl(writer)
        uPanelResults.RenderControl(writer)
        upPrgResults.RenderControl(writer)
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
            'debug.print("<clsComboBox.Set.List2Source>")
            malist2Values = value
            '         RefreshList2()
            'debug.print("</clsComboBox.Set.List2Source>")
        End Set
    End Property
    Public Overrides Sub refreshLists()
        'debug.print("<clsComboBox.refreshLists>")
        Dim rli As New ReportListItem
        Dim li As New ListItem
        '*** Clear the Items from the List
        ctrlField1.Items.Clear()
        ctrlField2.Items.Clear()

        '*** Refresh the ReportListITems Collection
        '*** NOT DONE YET

        '*** Iterate through the list Items and add to list Control
        For Each rli In mrptCtrl.ListItems
            li = New ListItem
            li.Value = rli.Value
            li.Text = rli.Description
            ctrlField1.Items.Add(li)
            ' If ctrlField2.Visible Then
            ctrlField2.Items.Add(li)
            'End If
            li = Nothing
        Next
        'debug.print("</clsComboBox.refreshLists>")
    End Sub
    Public Overrides Sub ForceValidation()
        'debug.print("<clsComboBox.ForceValidation>")
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
        'debug.print("</clsComboBox.ForceValidation>")
    End Sub
    Public Sub New()
        mrptCtrl = New ReportControl
    End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
    Protected Sub UpdatePanel1_DataBinding(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles UpdatePanel1.DataBinding
        'debug.print("<clsComboBox.UpdatePanel1_DataBinding>")

        'debug.print("</clsComboBox.UpdatePanel1_DataBinding>")
    End Sub
    Protected Sub Pagie_iLoad(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles Me.Load
        '  On Error Resume Next
    End Sub
    Public Sub SetColumns(dv As DataView)
        coldataview = dv
        bindgv()

    End Sub
    Public Sub bindgv()

        '*** Binds the Main Gridview Columns and Data
        Dim ds As New DataSet
        Dim dsCol As DataSet
        Dim dr As DataRow
        Dim lsSearchClause As String = ""
        Dim dc As DataColumn
        Dim dtv As DataTable
        Dim dt As DataTable

        If SQL = "" Then
            If liReportID > 0 Then
                Dim cn As New clsDataLoader
                ds = cn.LoadReport(liReportID)
                If ds.Tables.Count > 0 Then
                    dt = ds.Tables(0)
                    dr = dt.Rows(0)
                    If IsDBNull(dr("SelectStatement")) Then Exit Sub
                    SQL = dr("SelectStatement")
                    ' SQL = "Select * from vQuote"
                    SearchClause = IIf(IsDBNull(dr("SearchClause")), "", dr("Searchclause"))
                End If
            End If
        End If

        WhereClause = ReportWhereClause() & SearchCriteria
        '*** Get WHERE CLAUSE
        If WhereClause Is Nothing Then
            WhereClause = " 1=1 "
        End If
        If WhereClause = "" Then
            WhereClause = " 1=1 AND NodeID = " & HttpContext.Current.Session("NodeID") & " "
        Else
            WhereClause = WhereClause & " AND NodeID = " & HttpContext.Current.Session("NodeID") & " "
        End If
        If radShowAll.SelectedValue = "All" Then
        Else
            WhereClause = WhereClause & "  and Active=1 "
        End If
        If Sorting = "" Then
        Else
            WhereClause = WhereClause & Sorting
        End If
        lblWhere.Text = ReportWhereDesc
        ds = fGetDataset(SQL, WhereClause, SearchClause)
        If ds Is Nothing Then Exit Sub
        If ds.Tables.Count = 0 Then
            Exit Sub
        End If
        '*** This should have columns if there is no VIew
        dtv = ds.Tables(0)

        '*** Do we already have a view with Columns?
        dsCol = GetColumns()
        Dim formatstring As String
        If dsCol.Tables.Count > 0 Then
            dt = dsCol.Tables(0)
            If dt.Rows.Count > 0 Then
                '*** Yes we have a view
                gv.Columns.Clear()
                If dsCol.Tables(0).Rows.Count > 0 Then
                    gv.DataKeyNames = New String() {"ID"}
                    Dim cf As CommandField = New CommandField
                    'cf.ShowEditButton = True
                    '    cf.ShowDeleteButton = True
                    '   cf.DeleteImageUrl = "~/images/delete.png"
                    cf.ControlStyle.CssClass = " del table-hover"
                    cf.ShowCancelButton = True
                    Dim itmTmpF As New TemplateField
                    itmTmpF.ItemTemplate = New LinkColumn
                    gv.Columns.Add(itmTmpF)
                    'gv.Columns.Add(cf)

                End If
                Dim colVisExists As Boolean
                For Each dr In dsCol.Tables(0).Rows
                    Try
                        colVisExists = dr("columnVisible")
                    Catch ex As Exception
                        colVisExists = False
                    End Try
                    If colVisExists = True Then
                        Dim bf As BoundField = New BoundField()
                        bf.DataField = dr("FieldName")
                        bf.SortExpression = dr("FieldName")
                        bf.HeaderText = dr("ColumnName")
                        If Not IsDBNull(dr("columnformat")) Then
                            formatstring = dr("columnformat")
                            If formatstring <> "" Then
                                Select Case formatstring.ToUpper
                                    Case "CURRENCY", "C", "USD"
                                        bf.DataFormatString = "{0:c}"
                                    Case "D", "d"
                                        bf.DataFormatString = "{0:d}"
                                    Case Else
                                        bf.DataFormatString = dr("columnFormat")
                                End Select
                            Else

                            End If
                        End If
                        gv.Columns.Add(bf)
                    End If
                Next
            Else
                gv.Columns.Clear()
                For Each dc In dtv.Columns
                    Dim bf As BoundField = New BoundField()
                    bf.DataField = dc.Caption
                    bf.HeaderText = dc.Caption
                    gv.Columns.Add(bf)
                Next
            End If


        End If

        '  <asp:TemplateField HeaderText="Name">
        '    <ItemTemplate>
        '       <label id="lbl" data-id="<%# Eval("ID") %>"><%# Eval("Name") %></label>
        '      </ItemTemplate>


        '     gv = Me.Page.FindControl("gvResults1")
        gv.DataSource = dtv
        Try
            gv.DataBind()
            If gvPageNum > 0 Then gv.PageIndex = gvPageNum
        Catch e As Exception
            'debug.print(e.Message)
        End Try
    End Sub
    Sub gv_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lsID As String = e.Row.DataItem("ID").ToString()

            Dim cell As TableCellCollection = e.Row.Cells
            Dim l As New Label
            l.ID = "lbl"
            l.Attributes.Add("data-id", lsID)
            cell(0).Controls.Add(l)
            Dim lnk, lnkImg, lnkImg2 As LinkButton
            lnk = cell(0).FindControl("lnkDel")
            If Not lnk Is Nothing Then
                lnk.CommandArgument = lsID
                lnk.Attributes.Add("data-id", lsID)
            End If
            lnkImg = cell(0).FindControl("lnkImg")
            If Not lnkImg Is Nothing Then
                lnkImg.CommandArgument = lsID
                lnkImg.Attributes.Add("data-id", lsID)
            End If
            lnkImg2 = cell(0).FindControl("lnkImg2")
            If Not lnkImg2 Is Nothing Then
                lnkImg2.CommandArgument = lsID
                lnkImg2.Attributes.Add("data-id", lsID)
            End If
        End If
    End Sub
    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) 'Handles DropDownList1.TextChanged
        'debug.print("<ctrlReportColumns_btnSave_CLick>")
        'SAVE ROUTINE
        'debug.print("</ctrlReportColumns_btnSave_CLick>")
    End Sub
    Public Property ReportWhereClause()
        Get
            ReportWhereClause = Me.Attributes("data-WhereClause")
        End Get
        Set(value)
            Me.Attributes.Add("data-WhereClause", value)
        End Set
    End Property
    Public Property ReportWhereDesc()
        Get
            ReportWhereDesc = Me.Attributes("data-reportwheredesc")
        End Get
        Set(value)
            Me.Attributes.Add("data-reportwheredesc", value)
        End Set
    End Property
    Public Property ReportDesc()
        Get
            ReportDesc = Me.Attributes("data-reportedesc")
        End Get
        Set(value)
            Me.Attributes.Add("data-reportdesc", value)
        End Set
    End Property
    Protected Sub gv_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        'Stop
        gv.PageIndex = e.NewPageIndex
        bindgv()
        'gvPageNum = e.NewPageIndex
        'debug.print(e.NewPageIndex)
        'gv.SetPageIndex(e.NewPageIndex)
    End Sub
    Protected Sub gv_PageIndexChanged(sender As Object, e As GridViewPageEventArgs)
        Stop
    End Sub




    Public Function fGetDataset(lsSQL As String, lsWhere As String, SearchClause As String) As DataSet
        Dim c As New clsDataLoader
        fGetDataset = c.LoadReportResults(lsSQL, lsWhere, liReportID, SearchClause)
    End Function
    Public Function GetColumns() As DataSet
        '*** Gets the Columns from the VIew Columns Table
        Dim lsView As String = GetViewID.ToString
        If lsView = "00000000-0000-0000-0000-000000000000" Then lsView = ddlView.SelectedValue
        Dim c As New clsDataLoader
        If lsView = "" Then
            Dim ds As New DataSet
            Dim dt As New DataTable
            ds.Tables.Add(dt)
            Return ds
        End If
        GetColumns = c.LoadReportViewColumns(lsView)

    End Function

    Private Sub ReportResults_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender

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
    '*************************************************************************************
    '*** CAN'T Run This from ServerControl.  Im doing it on the page
    'Protected Sub PrintCurrentPage(ByVal sender As Object, ByVal e As EventArgs)
    '    gv.PagerSettings.Visible = False
    '    gv.DataBind()
    '    Dim sw As New StringWriter()
    '    Dim hw As New HtmlTextWriter(sw)

    '    gv.RenderControl(hw)
    '    Dim gridHTML As String = sw.ToString().Replace("""", "'").Replace(System.Environment.NewLine, "")
    '    Dim sb As New StringBuilder()

    '    sb.Append("<script type='text/javascript'>")
    '    sb.Append("window.onload = new function(){")
    '    sb.Append("var printWin = window.open('', '', 'left=0")
    '    sb.Append(",top=0,width=1000,height=600,status=0');")
    '    sb.Append("printWin.document.write(""")
    '    sb.Append(gridHTML)
    '    sb.Append(""");")
    '    sb.Append("printWin.document.close();")
    '    sb.Append("printWin.focus();")
    '    sb.Append("printWin.print();")
    '    sb.AppendLine("setTimeout(function(){window.close();}, 1); /* In fact timeout doesn't start until print dialog closes */")
    '    sb.Append("printWin.close();};")
    '    sb.Append("</script>")
    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "GridPrint", sb.ToString())
    '    gv.PagerSettings.Visible = True
    '    gv.DataBind()

    'End Sub
    'Protected Sub PrintAllPages(ByVal sender As Object, ByVal e As EventArgs)
    '    gv.AllowPaging = False
    '    gv.DataBind()
    '    Dim sw As New StringWriter()
    '    Dim hw As New HtmlTextWriter(sw)

    '    gv.RenderControl(hw)

    '    Dim gridHTML As String = sw.ToString().Replace("""", "'").Replace(System.Environment.NewLine, "")
    '    Dim sb As New StringBuilder()

    '    sb.Append("<script type='text/javascript'>")
    '    sb.Append("window.onload = new function(){")
    '    sb.Append("var printWin = window.open('', '', 'left=0")
    '    sb.Append(",top=0,width=1000,height=1000,status=0');")
    '    sb.Append("printWin.document.write(""")
    '    sb.Append(gridHTML)
    '    sb.Append(""");")
    '    sb.Append("printWin.document.close();")
    '    sb.Append("printWin.focus();")
    '    sb.Append("printWin.print();")
    '    sb.AppendLine("setTimeout(function(){window.close();}, 1); /* In fact timeout doesn't start until print dialog closes */")
    '    sb.Append("printWin.close();};")
    '    sb.Append("</script>")
    '    Page.ClientScript.RegisterStartupScript(Me.[GetType](), "GridPrint", sb.ToString())
    '    gv.AllowPaging = True
    '    gv.DataBind()

    'End Sub

    'Protected Sub ExportToExcel()
    '    Page.Response.Clear()
    '    Page.Response.Buffer = True
    '    Page.Response.AddHeader("content-disposition", "attachment;filename=DataExport.xls")
    '    Page.Response.Charset = ""
    '    Page.Response.ContentType = "application/vnd.ms-excel"
    '    Using sw As New StringWriter()
    '        Dim hw As New HtmlTextWriter(sw)

    '        'To Export all pages
    '        gv.AllowPaging = False
    '        gv.DataBind() 'Me.BindGrid()

    '        'gv.HeaderRow.BackColor = Color.White
    '        For Each cell As TableCell In gv.HeaderRow.Cells
    '            cell.BackColor = gv.HeaderStyle.BackColor
    '        Next
    '        '			For Each row As GridViewRow In gv.Rows
    '        '	row.BackColor = Color.White
    '        '	For Each cell As TableCell In row.Cells
    '        '		If row.RowIndex Mod 2 = 0 Then
    '        '			cell.BackColor = gv.AlternatingRowStyle.BackColor
    '        '				Else
    '        '				cell.BackColor = gv.RowStyle.BackColor
    '        '				End If
    '        '				cell.CssClass = "textmode"
    '        '			Next
    '        '			Next

    '        '*** Put this in     gv.RenderControl(hw)
    '        'style to format numbers to string
    '        Dim style As String = "<style> .textmode { } </style>"
    '        Page.Response.Write(style)
    '        Page.Response.Output.Write(sw.ToString())
    '        Page.Response.Flush()
    '        Page.Response.[End]()
    '    End Using
    'End Sub
End Class

Public Class KolassaProgressTemplate : Implements ITemplate
    '*** When implementing the ITemplate interface, you must
    '*** implement the InstantiateIn method. The FormView
    '*** control calls this method to create the template's content. 
    Public Sub InstantiateIn(ByVal container As Control) Implements Web.UI.ITemplate.InstantiateIn
        '*** Create the child controls contained in the template.
    End Sub

End Class
Public Class LinkColumn : Implements ITemplate

    Public Sub InstantiateIn(container As Control) Implements ITemplate.InstantiateIn
        'Throw New NotImplementedException()
        Dim lnkDel As LinkButton = New LinkButton
        AddHandler lnkDel.DataBinding, AddressOf l_Databinding
        lnkDel.ID = "lnkDel"
        container.Controls.Add(lnkDel)

        'Dim lnkImg As LinkButton = New LinkButton
        'AddHandler lnkImg.DataBinding, AddressOf img_Databinding
        'lnkImg.ID = "lnkImg"
        'container.Controls.Add(lnkImg)

        'Dim lnkImg2 As LinkButton = New LinkButton
        'AddHandler lnkImg2.DataBinding, AddressOf img2_Databinding
        'lnkImg2.ID = "lnkImg2"
        'container.Controls.Add(lnkImg2)
    End Sub
    Private Sub l_Databinding(sender As Object, e As EventArgs)
        Dim l As LinkButton = sender
        Dim gvr As GridViewRow = l.NamingContainer
        Dim drv As DataRowView = gvr.DataItem

        If drv("Active").ToString.ToUpper = "TRUE" Then
            l.Text = "<i class='dc dc-bin del table-hover' ></i>"
            l.CommandName = "gv_delete"
        Else
            l.Text = ""
            l.CommandName = ""
        End If
    End Sub
    'Private Sub img_Databinding(sender As Object, e As EventArgs)
    '    Dim l As LinkButton = sender
    '    Dim gvr As GridViewRow = l.NamingContainer
    '    Dim drv As DataRowView = gvr.DataItem

    '    If drv("Active").ToString.ToUpper = "TRUE" Then
    '        l.Text = "<i class='dc dc-images del table-hover' ></i>"
    '        'l.CommandName = "gv_delete"
    '    Else
    '        l.Text = ""
    '        l.CommandName = ""
    '    End If
    'End Sub
    'Private Sub img2_Databinding(sender As Object, e As EventArgs)
    '    Dim l As LinkButton = sender
    '    Dim gvr As GridViewRow = l.NamingContainer
    '    Dim drv As DataRowView = gvr.DataItem

    '    If drv("Active").ToString.ToUpper = "TRUE" Then
    '        l.Text = "<i class='dc dc-image del table-hover' ></i>"
    '        'l.CommandName = "gv_delete"
    '    Else
    '        l.Text = ""
    '        l.CommandName = ""
    '    End If
    'End Sub
End Class
