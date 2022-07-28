Option Strict Off
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Diagnostics
<DefaultProperty("Text"), ToolboxData("<{0}:ReportContainer runat=server></{0}:ReportContainer>")>
Public Class ReportContainer
    Inherits CompositeControl


	Dim cboReportCategory As DropDownList
	Dim miCategoryPassThrough As Integer ' If Category is Passed externally, hold it here
	Dim d As DropDownList
	Dim ReportType As String
	Dim updatePanel1 As Panel ' UpdatePanel
	Dim updatePanel2 As Panel ' UpdatePanel
    Dim lstReports As BulletedList
    Dim txtReportID As TextBox
    Dim litDebug As Literal
    Dim btnSetReport As Button
	Dim litmsg As Literal
	Dim upHeader As Panel
	Dim up1 As Panel
    Dim up2 As Panel ' UpdatePanel
    Dim ctrlRMFieldEditor As mycontrol_
    Dim LoadImage As System.Web.UI.WebControls.Image
	Dim tbl As Table
	Dim tblRow As TableRow
	Dim tblCell As TableCell
    Dim btn As Button
    Dim cmdNewField, cmdEditMode As LinkButton
    Dim mbDebug As Boolean
    Dim mbShowRFPanel ' *** SHowHides RFMODAL
    Dim EditMode As Boolean
    Public ReportDescription As String
    Public NodeID As Long
	Public ProjectID As String
	Public DataObject As Object
    Public TableName As String
    Public SelectStatement As String
    Public msReportListClass As String
    Public msReportListSelectedClass As String
    Dim iTimeThrough As Integer ' Tracks number of times UPData update is run.
    Dim msHide As String = "" ' "d-none"
    Dim RFModal, RFModalDialog, RFModalContent, RFModalHeader, RFModalBody As System.Web.UI.HtmlControls.HtmlGenericControl
    Dim RFSpan, rfLitContent As Literal
    Dim RFCLose, lnkshowrf As HtmlAnchor
    Dim upFieldEditor As UpdatePanel
    Dim upt As UpdatePanelTrigger
    Dim apt As AsyncPostBackTrigger
    'Dim cmdRefresh As Button

    ' Dim asyncPostBackTrigger1 As AsyncPostBackTrigger
    ' Dim asyncPostbackTrigger2 As AsyncPostBackTrigger
    ' Dim UpdateProgress1 As UpdateProgress

    <Category("Appearance")>
    <Description("Show Debug")>
    <DefaultValue("False")>
    Public Property showDebug() As Boolean
        Get
            EnsureChildControls()
            Return mbDebug

        End Get
        Set(ByVal value As Boolean)
            EnsureChildControls()
            mbDebug = value
        End Set
    End Property
    <Category("Appearance")>
    <Description("ReportListClass")>
    <DefaultValue("list-group-item ")>
    Public Property ReportListClass() As String
        Get
            EnsureChildControls()
            Return msReportListClass

        End Get
        Set(ByVal value As String)
            EnsureChildControls()
            msReportListClass = value
        End Set
    End Property
    <Category("Appearance")>
    <Description("ReportListSelectedClass")>
    <DefaultValue("list-group-item active text-white")>
    Public Property ReportListSelectedClass() As String
        Get
            EnsureChildControls()
            Return msReportListSelectedClass

        End Get
        Set(ByVal value As String)
            EnsureChildControls()
            msReportListSelectedClass = value
        End Set
    End Property
    Protected Overrides Sub RecreateChildControls()
        'debug.print("<RecreateChildControls>")
        EnsureChildControls()
        'debug.print("</RecreateChildControls>")
    End Sub
    'Protected Sub cmdEditMode_Click()
    '    EditMode = True
    '    Me.Attributes("EditMode") = "True"

    'End Sub
    Protected Overrides Sub CreateChildControls()
        'debug.print("<ReportContainer_CreateChildControls>")
        Dim ctrlid As String
        Dim lsHidden As String = ""
        Dim p As Page
        Dim ctrlName As String = ""
        p = Me.Page
        If p Is Nothing Then Exit Sub
        ctrlid = p.Request.Form("__LASTFOCUS")
        If p.IsPostBack Then
            ctrlName = p.Request.Params.Get("__EVENTTARGET")
        End If
        Controls.Clear()
        lnkshowrf = New HtmlAnchor

        lnkshowrf.ID = "lnkshowrf"
        lnkshowrf.ClientIDMode = 3 '"Static" ' *** Must make static so bubble up event know what to fire 
        lnkshowrf.HRef = "#field_edit_modal"
        lnkshowrf.Attributes.Add("class", "btn btn-link btn-sm mt-1 d-none anewrec")
        lnkshowrf.Attributes.Add("data-toggle", "modal")
        lnkshowrf.Title = "Show the Modal"
        lnkshowrf.InnerText = "Show the Modal!"
        '  Page.Controls.Add(lnkshowrf) '*** Moved down to UP1

        '*** Field Edit Modal ***
        RFModal = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        RFModal.Attributes.Add("class", "modal") ' arecnew show")
        RFModal.Attributes.Add("ID", "field_edit_modal")
        RFModal.Attributes.Add("role", "dialog")
        RFModal.Attributes.Add("tabindex", "-1")
        RFModal.Attributes.Add("aria-modal", "true")
        '     RFModal.Attributes.Add("style", "display: block;")
        RFModalDialog = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        With RFModalDialog
            .Attributes.Add("class", "modal-dialog")
            .Attributes.Add("role", "document")
        End With
        RFModalContent = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        With RFModalContent
            .Attributes.Add("class", "modal-content")
        End With
        RFModalHeader = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        With RFModalHeader
            .Attributes.Add("class", "modal-header")
        End With
        RFModalBody = New System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
        RFModalBody.Attributes.Add("class", "modal-body")
        RFSpan = New Literal
        RFSpan.Text = "Configure Field Form"

        '  rfLitContent = New Literal
        'RFModalBody.Controls.Add(rfLitContent)

        RFCLose = New HtmlAnchor
        RFCLose.Attributes.Add("class", "H5")
        RFCLose.HRef = "#field_edit_modal"
        RFCLose.Attributes.Add("data-toggle", "modal")
        '  RFCLose.Attributes.Add("data-dismiss", "modal")
        RFCLose.Attributes.Add("data-target", "#field_edit_modal")
        RFCLose.Attributes.Add("class", "anewrec text-white close")
        RFCLose.InnerText = "X"


        RFModalHeader.Controls.Add(RFSpan)
        RFModalHeader.Controls.Add(RFCLose)
        'cmdRefresh = New Button
        'cmdRefresh.ID = "cmdRefresh"
        'cmdRefresh.Text = "Refresh"
        'RFModalHeader.Controls.Add(cmdRefresh)
        '*** Create the Control Config tool for editing fields on forms
        ctrlRMFieldEditor = New mycontrol_
        ctrlRMFieldEditor.EnableViewState = True
        ctrlRMFieldEditor.ID = "ReportManagerFieldEditor"
        ctrlRMFieldEditor.Attributes.Clear()
        ctrlRMFieldEditor.Attributes.Remove("style")
        upFieldEditor = New UpdatePanel
        upFieldEditor.ID = "upFieldEditor"
        upFieldEditor.ContentTemplateContainer.Controls.Add(ctrlRMFieldEditor)

        '  apt = New AsyncPostBackTrigger
        '  apt.ControlID = "cmdRefresh"
        '  apt.EventName = "click"
        '  upFieldEditor.Triggers.Add(apt)


        RFModalBody.Controls.Add(upFieldEditor)
        RFModalContent.Controls.Add(RFModalHeader)
        RFModalContent.Controls.Add(RFModalBody)
        RFModalDialog.Controls.Add(RFModalContent)
        RFModal.Controls.Add(RFModalDialog)
        ' RFModal.Controls.Add(ctrlRMFieldEditor)
        '  Controls.Add(RFModal) 'Added to UP1 below

        '*** Create the Category Drop Down
        cboReportCategory = New DropDownList
        cboReportCategory.ID = "cboReportCategory"
        cboReportCategory.AutoPostBack = True
        cboReportCategory.Attributes("class") = "form-group"
        AddHandler cboReportCategory.SelectedIndexChanged, AddressOf cboReportCategory_Changed
        AddHandler cboReportCategory.TextChanged, AddressOf cboReportCategory_Changed

        btnSetReport = New Button
        btnSetReport.Text = "Set Report"
        btnSetReport.ID = "btnSetReport"
        btnSetReport.CssClass = msHide
        AddHandler btnSetReport.Click, AddressOf btnSetReport_Click


        txtReportID = New TextBox
        txtReportID.ID = "txtReportID"
        txtReportID.CssClass = msHide
        updatePanel1 = New Panel ' UpdatePanel
        updatePanel2 = New Panel ' UpdatePanel
        updatePanel1.ID = "updatePanel1"
        updatePanel2.ID = "updatePanel2"

        upHeader = New Panel
        upHeader.ID = "upHeader"

        '*** Create the Panel to hold the Dynamic Controls
        up1 = New Panel
        up1.ID = "up1"
        up1.Attributes.Remove("style") '202009

        '*** Create the Panel for Holding the Category Dropdown and the Report List
        '*** Hidden for Form Creation
        up2 = New Panel 'UpdatePanel
        up2.ID = "up2"
        AddHandler up2.Load, AddressOf UP2Load

        litDebug = New Literal
        litDebug.ID = "txtDebug"
        ' txtDebug.CssClass = msHide

        litmsg = New Literal
        litmsg.ID = "litmsg"     'debug.print("<Clear control='litmessage' />")
        litmsg.Text = "" 'ReportDescription '<B>This is the litmsg control</B>"

        '*** For an Administrator, They should see the New Field Button to Add
        '*** a field to the form
        If 2 = 2 Then
            cmdEditMode = New LinkButton
            cmdEditMode.ID = "cmdEditMode"
            cmdEditMode.Text = "<i class='fa fa-cog float-right'></i>"
            '2022-07-28'   AddHandler cmdEditMode.Click, AddressOf cmdEditMode_Click
            cmdEditMode.Attributes.Add("onClick", "return false;") ' Added 2022-07-28
            cmdEditMode.OnClientClick = "fResetForm()"
            up1.Controls.Add(cmdEditMode)
        End If

        cmdNewField = New LinkButton
        cmdNewField.ID = "cmdNewField"
        cmdNewField.Text = "<i Class='fa fa-plus'></i>"
        cmdNewField.Attributes.Add("style", "display:none;")
        cmdNewField.CssClass = "editmode"
        AddHandler cmdNewField.Click, AddressOf cmdNewfield_Click
        '     If Me.Attributes("EditMode") = "True" Then
        '     cmdNewField.Visible = True
        '     Else
        '     cmdNewField.Visible = False
        '     End If
        up1.Controls.Add(cmdNewField)
        up1.Controls.Add(lnkshowrf) 'created above
        up1.Controls.Add(RFModal) 'Created Above)
        '   asyncPostBackTrigger1 = New AsyncPostBackTrigger
        '   asyncPostBackTrigger1.ControlID = "cboReportCategory"
        '   asyncPostbackTrigger2 = New AsyncPostBackTrigger
        '   asyncPostbackTrigger2.ControlID = "lstReports"


        '*************************************
        '*** Add lstReports Report list
        '*************************************
        'Dim TemplateText As String = " <ItemTemplate> <asp:label id=""Label1"" runat=""server"" 
        '                 Text='<%# DataBinder.Eval(Container, ""DataItem.ReportID"")%>'></asp:label>
        '                <asp:Hyperlink id=""Hyperlink1"" runat=""server"" Text='<%# DataBinder.Eval(Container, ""DataItem.ReportID"") %>'
        '                 NavigateURL='<%# DataBinder.Eval(Container, ""DataItem.ReportID"") %>'>
        '                </asp:Hyperlink></ItemTemplate>"
        'TemplateText = "          <tr runat=""server""><td> <asp:Label ID=""LastNameLabel"" runat=""Server"" Text='Rush' />    </td> </tr>"
        'Dim lsA As String = "<AlternatingItemTemplate><tr style='background-color: #FAFAD2;color: #284775;'></tr></AlternatingItemTemplate>"
        'Dim lsd As String = "<EditItemTemplate><tr style='background-color: #FFCC66;color: #000080;'><td><asp:Button ID='UpdateButton' runat='server' CommandName='Update' Text='Update' /><asp:Button ID='CancelButton' runat='server' CommandName='Cancel' Text='Cancel' /></td></tr></EditItemTemplate>"
        'Dim lsE As String = "<EmptyDataTemplate><table runat='server' style='background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;'><tr><td>No data was returned.</td></tr></table></EmptyDataTemplate>"
        'Dim lsN As String = "<InsertItemTemplate><tr style=''><td><asp:Button ID='InsertButton' runat='server' CommandName='Insert' Text='Insert' /><asp:Button ID='CancelButton' runat='server' CommandName='Cancel' Text='Clear' /></td></tr></InsertItemTemplate>"
        'Dim lsI As String = "<ItemTemplate><tr style='background-color: #FFFBD6;color: #333333;'>Item</tr></ItemTemplate>"
        'Dim lsL As String = "<LayoutTemplate><table runat='server'><tr runat='server'><td runat='server'><table id='itemPlaceholderContainer' runat='server' border='1' style='background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;'><tr runat='server' style='background-color: #FFFBD6;color: #333333;'></tr><tr id='itemPlaceholder' runat='server'></tr></table></td></tr><tr runat='server'><td runat='server' style='text-align: center;background-color: #FFCC66;font-family: Verdana, Arial, Helvetica, sans-serif;color: #333333;'></td></tr></table></LayoutTemplate>"
        'Dim lsS As String = "<SelectedItemTemplate><tr style ='background-color: #FFCC66;font-weight: bold;color: #000080;'></tr></SelectedItemTemplate>"

        lstReports = New BulletedList
        lstReports.DisplayMode = BulletedListDisplayMode.HyperLink
        lstReports.DataTextField = "ReportName"
        lstReports.CssClass = "list-group-item list-group-item-action borderless"
        lstReports.ID = "lstReports"
        'lstReports.Height = 400
        'lstReports.AutoPostBack = True
        'lstReports.AlternatingItemTemplate = New StringTemplate(lsA)
        'lstReports.LayoutTemplate = New StringTemplate(lsL)
        'lstReports.EditItemTemplate = New StringTemplate(lsd)
        'lstReports.EmptyDataTemplate = New StringTemplate(lsE)
        'lstReports.InsertItemTemplate = New StringTemplate(lsN)
        'lstReports.SelectedItemTemplate = New StringTemplate(lsS)
        'lstReports.ItemTemplate = New StringTemplate(lsI)

        Dim lsDatKeyName As String
        Dim lsDataKeyNames() As String
        lsDatKeyName = "ReportID"
        lsDataKeyNames = lsDatKeyName.Split(",")

        'lstReports.DataKeyNames = lsDataKeyNames
        AddHandler lstReports.Load, AddressOf lstReports_Load
        AddHandler lstReports.SelectedIndexChanged, AddressOf lstReports_SelectedIndexChanged



        tbl = New Table
        tbl.ID = "tbl"
        tbl.Attributes.Add("style", "width:100%")

        tblRow = New TableRow
        tblRow.ID = "tblRow"
        tblCell = New TableCell
        tblCell.ID = "tblCell"

        '  UpdateProgress1 = New UpdateProgress
        '  UpdateProgress1.ID = "UpdateProgress1"
        LoadImage = New System.Web.UI.WebControls.Image
        LoadImage.ImageUrl = "images/Loading.gif"
        '  UpdateProgress1.Controls.Add(LoadImage)

        'upHeader.Controls.Add(cboReportCategory)
        'upHeader.Controls.Add(btn)
        upHeader.Controls.Add(litDebug)
        upHeader.Controls.Add(litmsg)

        'updatePanel2.Triggers.Add(asyncPostBackTrigger1)
        'up2.Triggers.Add(asyncPostbackTrigger2)

        up2.CssClass = "form-group"
        up2.Controls.Add(cboReportCategory)
        up2.Controls.Add(lstReports) 'up2.ContentTemplateContainer.Controls.Add(lstReports) '20190315
        up2.Controls.Add(txtReportID)
        up2.Controls.Add(btnSetReport)

        'up2.Controls.Add(d)
        tblRow.Cells.Add(tblCell)
        tbl.Rows.Add(tblRow)
        up1.Controls.Add(tbl) 'up2.ContentTemplateContainer.Controls.Add(tbl)

        Controls.Add(updatePanel1)
        Controls.Add(updatePanel2)
        Controls.Add(up2)
        Controls.Add(up1)
        setPanelVisibility() '*** Should Panels be visible?

        '*** Border for the entire Group
        'Me.tbl.CssClass = "border border-primary col-md"

        ' Controls.Add(UpdateProgress1)
        'UpdateProgress1.Controls.Add(LoadImage)

        '*** Get the Report Definition
        ReportContainer_Load()

        '*** Get All the Controls to be added
        lstReports_Load()

        '*** Add All the Controls 
        CreateChildControlsSub()

        'debug.print("</ReportContainer_CreateChildControls>")

    End Sub
    Sub cmdNewfield_Click()
        '*** Create a new row for this report in the tblReportFields table
        '     Stop
        Dim c As New clsDataLoader
        c.InsertReportField(miReportID)
    End Sub
    Sub setPanelVisibility()
        mbShowRFPanel = True
        ' Exit Sub
        '*** Identify if the Control that caused the postback was "cmdPopulateFieldvalues"
        '*** if so, Process to form and then hide the control

        '*** Get the Control Name that caused the Postback
        Dim ctrlname As String = ""
        If Page.IsPostBack Then
            ctrlname = Page.Request.Params.Get("__EVENTTARGET")
        End If
        Exit Sub
        '*** Make sure the UP1 Panel Exists
        Dim cp As Panel = Me.FindControl("up1")
        Dim ctrlRMFieldEditor As mycontrol_ = cp.Parent.FindControl("ReportManagerFieldEditor")

        '*** If UP1 Does not exist, exit, cuz the Container is not right

        If cp Is Nothing Then Exit Sub

        Dim rfe As Panel = ctrlRMFieldEditor.FindControl("pnlRF-modal")
        ' rfe = Page.FindControl("rfmodal")
        If Not rfe Is Nothing Then
            rfe.Attributes.Remove("style")
            If Not cp Is Nothing Then
                cp.Attributes.Remove("style")
                cp.Attributes.Remove("style")
                If ctrlname.Contains("cmdPopulateFieldValues") Then
                    '   cp.Attributes.Add("style", "visibility:hidden") '202009
                    '   rfe.Attributes.Add("style", "visibility:visible")
                    mbShowRFPanel = True
                Else
                    mbShowRFPanel = False
                    cp.Visible = True
                    cp.Attributes.Add("Dude", "fgf")
                    cp.Attributes.Remove("style")
                    cp.Attributes.Add("style", "visibility:visible; display:block;")
                    '    rfe.Attributes.Add("style", "visibility:hidden")
                End If
            End If
        End If
        ' mbShowRFPanel = True
    End Sub
    Public Sub btnSetReport_Click()
        ' On Error Resume Next
        If Not cboReportCategory Is Nothing Then
            cboReportCategory.SelectedValue = 12
            cboReportCategory_Changed()
        End If

    End Sub
    Protected Sub UP2Load()
        'debug.print("<UP2Load>")
        If ReportID = 0 Then
            ReportID = f_GetReportID()
        End If
        If ReportID > 0 Then
            up2_Load(Val(txtReportID.Text), False)
        End If
        'debug.print("</UP2Load>")
    End Sub
    Protected Sub d_Load()
        'debug.print("<d_Load />")
        '	Stop
    End Sub
    '********* RENDER THE CONTROLS  ********************
    Protected Overrides Sub Render(writer As HtmlTextWriter)
        'debug.print("<ReportContainer_Render>")
        If upHeader Is Nothing Then Exit Sub
        AddAttributesToRender(writer)
        writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1")

        writer.RenderBeginTag(HtmlTextWriterTag.Span)
        'cboReportCategory.RenderControl(writer)
        'litmsg.RenderControl(writer)
        'btn.RenderControl(writer)
        upHeader.CssClass = "col-12"
        upHeader.RenderControl(writer)

        updatePanel1.RenderControl(writer)
        If 1 = 2 Then
            If mbShowRFPanel Then
                ctrlRMFieldEditor.Visible = True
                up1.Visible = vbFalse

            Else
                ctrlRMFieldEditor.Visible = False
                up1.Visible = True
                'If Me.Attributes("EditMode") = "True" Then
                '    cmdNewField.Visible = True
                'Else
                '    cmdNewField.Visible = False
                'End If
            End If
        Else
            mbShowRFPanel = True 'take this out
            up1.Visible = True

        End If

        '      If Not ctrlRMFieldEditor Is Nothing Then
        '      ctrlRMFieldEditor.Visible = True
        ' ctrlRMFieldEditor.RenderControl(writer)
        '      End If



        up1.RenderControl(writer)
        ' UpdateProgress1.RenderControl(writer)
        ''LoadImage.RenderControl(writer)


        writer.RenderEndTag() ' </span>


        RenderSubControls(writer)
        ReportContainer_Load() 'Me, New EventArgs)
        'debug.print("</ReportContainer_Render>")
    End Sub


    Protected Overridable Sub CreateChildControlsSub()

    End Sub
    Sub btn_click()
        'debug.print("btnClick" + Now.ToShortTimeString)
    End Sub
    Protected Overridable Sub RenderSubControls(writer As HtmlTextWriter)

    End Sub

    '***********************************************************
    '**** CodeBehind
    '***********************************************************
    Dim mdlDataLoader As New clsDataLoader
    Dim mcReports As New Collection
    Dim mcReportCategories As New Collection
    Dim msReportCategoryType As String = ""
    Public ReportOut As String = ""
    Public miReportID As Integer
    Dim msOpenArgs As String
    Dim msCnStr As String
    Dim mb_UseFilter As Boolean
    Dim rptCtrls As ReportControls

    Public ReportControl As ReportControl
    Dim msWhereClause As String = ""

    Public Sub New()

    End Sub

    Public Property WhereClause() As String
        Get
            If rptCtrls.Count > 0 Then
                WhereClause = msWhereClause
                WhereClause = f_CreateWhereClause(True)
                'response.write(WhereClause)
            Else
                WhereClause = ""
            End If
            'debug.print("<PropertyGet WhereClause='" & WhereClause & "'/>")
        End Get
        Set(ByVal value As String)

        End Set
    End Property
    Public Property ReportID() As Integer
        Get
            Dim lsreport As String
            If txtReportID Is Nothing Then
                lsreport = "0"
            Else
                lsreport = txtReportID.Text
            End If
            'debug.print("<PropertyGet ReportID='" & Val(lsreport) & "'/>")
            Return Val(lsreport)

        End Get
        Set(ByVal value As Integer)
            'debug.print("<PropertySet ReportID='" & value & "'/>")
            If txtReportID Is Nothing Then
            Else
                txtReportID.Text = value
            End If

            miReportID = value
        End Set
    End Property
    Public Property ReportCategoryType() As String
        Get
            ReportCategoryType = System.Web.HttpContext.Current.Session("msReportCategoryType")
            If ReportCategoryType Is Nothing Then
                ReportCategoryType = System.Web.HttpContext.Current.Request.QueryString("objType")
            End If
            'debug.print("<PropertyGet ReportCategoryTYpe='" & ReportCategoryType & "'/>")
        End Get
        Set(ByVal value As String)
            System.Web.HttpContext.Current.Session("msReportCategoryType") = value
        End Set
    End Property

    Public Property ReportName() As String
        Get
            If f_GetReportID().ToString = "" Then
                ReportName = ""
                ReportDescription = ""
            Else
                If lstReports Is Nothing Then
                    ReportName = ""
                Else
                    ReportName = lstReports.BulletImageUrl
                    ReportDescription = lstReports.DataTextField
                End If

            End If
            'debug.print("<PropertyGet ReportName='" & ReportName & "'/>")
        End Get
        Set(ByVal value As String)

        End Set
    End Property


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '*** Load All the Data sets used by the control
        'debug.print("<ReportContainer_Page_init>")


        Dim lsobjType As String = ""
        lsobjType = System.Web.HttpContext.Current.Request("objType")
        System.Web.HttpContext.Current.Session("objType") = lsobjType
        If lsobjType = ReportCategoryType Then
        Else
            ReportCategoryType = lsobjType
        End If


        If System.Web.HttpContext.Current.Session("mdlDataLoader") Is Nothing Then
            mdlDataLoader = New clsDataLoader
            mdlDataLoader.LoadReportCategories(lsobjType)
            If mdlDataLoader.mdsReportCategories.Tables.Count > 0 And lsobjType <> "" Then
                If mdlDataLoader.mdsReportCategories.Tables(0).Rows.Count > 0 Then
                    miCategoryPassThrough = mdlDataLoader.mdsReportCategories.Tables(0).Rows(0)("ReportCategoryID")
                End If
            End If

            mdlDataLoader.LoadAllControls()
            System.Web.HttpContext.Current.Session("mdlDataLoader") = mdlDataLoader
        End If
        mdlDataLoader = System.Web.HttpContext.Current.Session("mdlDataLoader")

        If System.Web.HttpContext.Current.Session("rptCtrls") Is Nothing Then
            rptCtrls = New ReportControls
            rptCtrls.Load()
            System.Web.HttpContext.Current.Session("rptctrls") = rptCtrls
        End If

        rptCtrls = System.Web.HttpContext.Current.Session("rptCtrls")
        mcReports = System.Web.HttpContext.Current.Session("Reports")
        'debug.print("</ReportContainer_Page_init>")
    End Sub
    Private Sub me_load() Handles Me.Load
        'debug.print("<me_load />")
    End Sub
    Private Sub ReportContainer_Load() 'ByVal sender As Object, ByVal e As System.EventArgs) 'Handles Me.Load
        'debug.print("<ReportContainer_Load>")
        Dim lstitm As ListItem
        Dim rptcat As ReportCategory


        '*** Was there a Form Name Passed?
        msOpenArgs = HttpContext.Current.Request("objType")

        '*** If No Categories in cbo, load them from database
        If Me.cboReportCategory.Items.Count <= 0 Then
            ReportContainer_LoadCategories()
            For Each rptcat In mcReportCategories
                If msOpenArgs Is Nothing Then msOpenArgs = ""
                If Len(msOpenArgs) = 0 Or rptcat.CategoryDescription.ToUpper = msOpenArgs.ToUpper Then
                    lstitm = New ListItem
                    lstitm.Value = rptcat.CategoryID
                    lstitm.Text = rptcat.CategoryDescription
                    cboReportCategory.Items.Add(lstitm)
                    lstitm = Nothing
                End If
            Next


            '*** Load ALL THE Report Controls into the ReportControls Collection
            Dim mbLoadControls As Boolean = False
            If rptCtrls Is Nothing Then
                mbLoadControls = True
            Else
                If rptCtrls.Count <= 0 Then
                    mbLoadControls = True
                End If
            End If
            If mbLoadControls = True Then
                rptCtrls = New ReportControls
                rptCtrls.Load()
            End If
            lstReports.Items.Clear()
            txtReportID.Text = ""
        End If

        'debug.print("</ReportContainer_Load>")
    End Sub
    Private Sub ReportContainer_LoadCategories()
        Try
            'debug.print("<ReportContainer_LoadCategories>")
            If Me.cboReportCategory.Items.Count > 0 Then
                'debug.print("<msg ms='Categories already exist, exit without doing anything' />")

            Else

                '*** Should I reload mdlDataLoader.mdsReportCategories? 
                Dim ds As DataSet = mdlDataLoader.mdsReportCategories
                Dim ReportCategoryName As String = ""
                'debug.print("<c cboReportCategory.SelectedValue=" & cboReportCategory.SelectedValue & "/>")
                '*** Have We loaded the categories from the DB?
                If Not ds Is Nothing Then
                    ReportCategoryName = ds.Tables("ReportCategoryType").Rows(0)(0)
                Else
                    'No Categories, Exit
                    Exit Sub
                End If
                Dim lsCatName As String = ReportCategoryName.ToLower
                If ReportCategoryType Is Nothing Then
                    Exit Sub
                End If
                Dim lsCatType As String = ReportCategoryType.ToLower

                If lsCatName <> ReportCategoryType.ToLower Or (ReportCategoryName = "" And ReportCategoryType = "") Then
                    mdlDataLoader.LoadReportCategories(ReportCategoryType)
                    ds = mdlDataLoader.mdsReportCategories
                End If
                Dim dt As DataTable = ds.Tables("Categories")
                Dim dr As DataRow
                Dim cat As ReportCategory
                cboReportCategory.Items.Clear()
                If mcReportCategories Is Nothing Then
                    mcReportCategories = New Collection
                End If
                mcReportCategories.Clear()

                '*** Should we load the Category Dropdown box?  Not if it is NOT  reporttype
                For Each dr In dt.Rows
                    ReportType = dr("ReportCategoryType")
                    If ReportType = "Form" Then
                        'Stop
                    End If
                    If 2 = 2 Or ReportCategoryType = "" Or dr("ReportCategoryType") = ReportCategoryType Then
                        cat = New ReportCategory

                        cat.HideReportLists = CBool(dr("ReportCategoryHideLists"))
                        '     If cat.HideReportLists = True Then
                        'up2.CssClass = msHide
                        'Else
                        '   up2.CssClass = "col-sm-9"
                        'End If
                        'response.write(dr("reportCategoryID")) & " " &  dr("reportCategoryDescription")
                        cat.CategoryID = CInt(dr("reportCategoryID"))
                        cat.CategoryDescription = dr("reportCategoryDescription")
                        'lstItm = New ListItem
                        'lstItm.Value = cat.CategoryID
                        'lstItm.Text = cat.CategoryDescription
                        'lstItm = Nothing
                        'cboReportCategory.Items.Add(lstItm)
                        If ReportCategoryType Is Nothing Or ReportCategoryType = "" Then
                            mcReportCategories.Add(cat)
                        Else
                            If ReportCategoryType.ToUpper = cat.CategoryDescription.ToUpper Then
                                mcReportCategories.Add(cat)
                            End If


                        End If
                    End If
                Next dr

                dr = Nothing
                dt = Nothing
                ds = Nothing

            End If
            'debug.print("</ReportContainer_LoadCategories>")
        Catch
        End Try
    End Sub


    Private Sub ResizeContainer(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'debug.print("<ResizeContainer />")
        ' Me.Height = Me.ParentForm.Height - Me.Top - 30
        ' Me.Width = Me.ParentForm.Width - Me.Left - 15
    End Sub

    Protected Sub lstReports_Load() 'ByVal sender As Object, ByVal e As System.EventArgs) 'Handles lstReports.Load
        'debug.print("<lstReports_Load SelectedReport=" & f_GetReportID() & ">")
        Dim cat As New ReportCategory
        Dim cat2 As New ReportCategory
        Dim rpt As New Report
        Dim llistitem As New ListItem
        Dim llBulletedListItem As New ListItem

        Dim li As Integer = 0


        If f_GetReportID() <> 0 Then
            li = CInt(f_GetReportID())
            ReportID = li
            'Exit Sub ' Maybe take this out 20190314  This might work for forms but not reports
            'debug.print("<msg ms='Already Processed this Category' />")
        End If

        cat = New ReportCategory
        '*** Find Selected Category
        llistitem = cboReportCategory.SelectedItem
        '*** If there are no categories in the cbo, we can't load Reports
        If llistitem Is Nothing Then ' was Exit Sub 2020
            Exit Sub
        End If
        cat.CategoryID = llistitem.Value
        For Each cat2 In mcReportCategories
            If cat2.CategoryID = llistitem.Value Then
                cat.HideReportLists = cat2.HideReportLists
                If cat.HideReportLists = True Then
                    up2.CssClass = msHide
                Else
                    up2.CssClass = "col-sm-3"
                End If
                Exit For
            End If
        Next

        Me.lstReports.Items.Clear()
        cat.Load()
        mcReports = cat.GetReports
        ' Session("Reports") = mcReports

        Dim lsrpt As String

        '*** Current Selected Reports SelectStatement (If it has one)
        SelectStatement = ""
        For Each rpt In mcReports
            'llistViewDataItem = New ListViewDataItem(0, 0)
            'llistViewDataItem.DataItem = rpt.ReportID
            'llistViewDataItem.DataItem = rpt.ReportDescription
            llBulletedListItem = New ListItem
            llBulletedListItem.Text = rpt.ReportDescription
            llBulletedListItem.Value = "?rpt=" & rpt.ReportID
            llBulletedListItem.Attributes.Add("style", "padding:0px;border:none;")

            lsrpt = HttpContext.Current.Request.QueryString("rpt")
            If Val(lsrpt) = rpt.ReportID Then
                SelectStatement = rpt.SelectStatement '***Selected Report's Select Statement
                llBulletedListItem.Attributes("class") = ReportListSelectedClass
                llBulletedListItem.Attributes("style") = "font-color: white;
                .href a: hover {    color:      Yellow;    Text-decoration: none;}"
            Else
                llBulletedListItem.Attributes("class") = ReportListClass
            End If

            Me.lstReports.Items.Add(llBulletedListItem) 'llistViewDataItem)
            llistitem = Nothing
        Next
        'On Error Resume Next
        'If lstReports.Items.Count > 0 Then
        ' 'debug.print("<msg  li=" & li & " />")
        ' Dim l As ListItem
        ' For Each l In lstReports.Items
        ' If l.Value.ToString = "?rpt=" & Trim(Str(li)) Then
        ' 'debug.print("<msg   FoundReport=" & li & " />")
        ' lstReports.SelectedIndex = li
        ' ReportID = li
        ' Exit For
        ' End If
        ' Next
        'End If
        'On Error GoTo 0


        If cat.HideReportLists = True Or 1 = 1 Then 'And 1 = 2 Then 'And lstReports.SelectedIndex = -1 And lstReports.Items.Count >= 1 Then
            updatePanel2.Visible = False
            If lstReports.Items.Count > 0 Then
                'debug.print(" <msg ms='Found Existing Reports in lstReports, Setting SelIndex to 0 if -1 and firing up2_Load' />")
                ReportID = f_GetReportID()

                up2_Load(ReportID, False)
            Else
                'debug.print("<msg ms='Found No Existing Reports in lstReports, Setting SelIndex -1' />")
                lstReports.ClearSelection()
                ReportID = f_GetReportID()
            End If
            'lstReports.Visible = False 'Put This Back in
            'cboReportCategory.Visible = False 'Put This Back in
            litDebug.Visible = False
        End If

        '*** disable controls on form
        ' sDisableControls()
        '  lstReports.ClearSelection()
        'lstReports.refresh

        'debug.print("ReportCount-" & lstReports.Items.Count.ToString & "</lstReports_Load>")
    End Sub
    Public Function f_GetReportID() As Long
        Dim rptID As Long
        Dim ds As DataSet
        Dim lsObjType As String = System.Web.HttpContext.Current.Request.QueryString("objtype")
        f_GetReportID = 0
        '*** We need to get the Report ID from the Database.  Start the Data Loader
        If System.Web.HttpContext.Current.Session("mdlDataLoader") Is Nothing Then mdlDataLoader = New clsDataLoader
        If lsObjType = "" Then
        Else
            ds = mdlDataLoader.LoadReports(0, lsObjType, 0)
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    rptID = ds.Tables(0).Rows(0)("ReportID")
                    ReportDescription = ds.Tables(0).Rows(0)("ReportDescription")
                    ReportName = ds.Tables(0).Rows(0)("ReportName")
                    If IsDBNull(ds.Tables(0).Rows(0)("TableName")) Then
                        TableName = "NoTable"
                    Else
                        TableName = ds.Tables(0).Rows(0)("TableName")
                    End If
                End If
                Return rptID
            End If

        End If

        If lstReports Is Nothing Then
            Return 0
        End If

        If lstReports.Items.Count > 0 Then
            If lstReports.SelectedIndex = -1 Then
                '     lstReports.SelectedIndex = 0
            End If
        End If
        'debug.print(" <msg lstReports_SelectedIndex ='" & lstReports.SelectedIndex & "' />")
        rptID = IIf(lstReports.SelectedValue = "", 0, lstReports.SelectedValue)
        If rptID = 0 Then
            rptID = System.Web.HttpContext.Current.Request.QueryString("rpt")
            If rptID = 0 Then
                If ReportCategoryType Is Nothing Or ReportCategoryType = "" Then
                    Exit Function
                Else

                    If mdlDataLoader.mdsReportCategories Is Nothing Then
                        mdlDataLoader.LoadReportCategories(ReportCategoryType)
                    End If
                    If mdlDataLoader.mdsReportCategories.Tables.Count = 0 Then
                        mdlDataLoader.LoadReportCategories(ReportCategoryType)
                    End If

                    If mdlDataLoader.mdsReportCategories.Tables.Count > 0 And ReportCategoryType <> "" Then
                        If mdlDataLoader.mdsReportCategories.Tables(0).Rows.Count > 0 Then
                            miCategoryPassThrough = mdlDataLoader.mdsReportCategories.Tables(0).Rows(0)("ReportCategoryID")
                        End If
                    End If
                End If
            End If
        End If
        Return rptID
    End Function
    Function f_CreateWhereClause(ByVal lbUseFilter As Boolean) As String

        'debug.print("<f_CreateWhereClause>")
        f_CreateWhereClause = ""
        Dim lsSQL As String = ""
        Dim liControlCount As Integer
        Dim ctrl As Control
        'Dim p As Panel
        Dim lsType As String = ""
        Dim lsTag As String = ""
        Dim rpt As Report
        Dim rptCtrl As ReportControl
        Dim lsDescription As String = ""
        Dim lsCtrlName As String = ""
        sUpdateControls()
        Dim tbl As Table
        Try
            tbl = Me.FindControl("tbl")
            ' Exit Function
            If lbUseFilter = True Then
                liControlCount = tbl.Controls(0).Controls(0).Controls.Count
                If liControlCount = 0 Then
                    f_CreateWhereClause = " 1=1 "
                Else

                    'debug.print("<FindControlsOnForm ms=" & tbl.Controls(0).Controls(0).Controls.Count.ToString & " >")

                    For Each ctrl In tbl.Controls(0).Controls(0).Controls(0).Controls
                        '*** Iterate through All Controls for this Report
                        'debug.print("<Typename t='" & TypeName(ctrl) & ctrl.ID & "' />")

                        '*************************************************************************
                        '*** Check For Mandatory Fields.  They are designated be the Back Color
                        '*************************************************************************
                        lsCtrlName = ctrl.ID
                        'debug.print("<msg ms='checking for Manadatory Fileds ReportContainer f_createwherechause' />")
                        'debug.print("<msg ms='FindByName: " & ctrl.ID & "' />")
                        rptCtrl = rptCtrls.FindByName(lsCtrlName)
                        'debug.print("<msg ms='Found: " & rptCtrl.FieldName & "' />")
                        lsType = TypeName(ctrl)
                        lsTag = IIf(Nothing = (rptCtrl.Type), "", rptCtrl.Type)
                        If lsType <> "Label" And lsTag = "ReportControl" Then

                            If rptCtrl.Required And (IsDBNull(rptCtrl.Value) Or rptCtrl.Value = "" Or rptCtrl.Value = "-1") Then
                                '*If ctrl.BackColor = Color.Red And (IsDBNull(ctrl.Text) Or ctrl.Text = "" Or ctrl.Text = "-1") Then
                                'debug.print("<msg ms='Required Field  " & rptCtrl.FieldName & " must be filled' />")
                                f_CreateWhereClause = "Cancel"
                                ctrl.Focus()
                                Exit Function
                            End If
                        End If
                    Next
                    'debug.print("</FindControlsOnForm>")
                End If
                Dim liCounter As Integer
                '***************************************************************
                '*** Build Report Criteria
                '***************************************************************
                If f_GetReportID() = 0 Then
                    f_CreateWhereClause = ""
                    Exit Function
                End If

                rpt = FindReportByID(txtReportID.Text)
                rpt.LoadControls("")
                sUpdateControls() '*** Updates rptCtrls with updated Ctrl Values

                For Each ReportControl In rpt.Controls
                    'For Each ReportControl In tbl.Controls(0).Controls(0).Controls
                    'If ReportControl.DataOperator <> "asdf" Then
                    'Else
                    If ReportControl.SQL = "--" Then
                    Else

                        ReportOut = ReportOut & "<tr><td valign='top'>" & ReportControl.FieldDescription
                        ReportOut = ReportOut & "</td><td valign='top'>"
                        ReportOut = ReportOut & "" & ReportControl.DataOperator.ToString
                        ReportOut = ReportOut & "</td><td valign='top'>"
                        For liCounter = 1 To ReportControl.SelectedItems.Count
                            ReportOut = ReportOut & "" & ReportControl.SelectedItems(liCounter).ToString & "<br />"
                        Next
                        ReportOut = ReportOut & "</td><td valign='top'>"
                        For liCounter = 1 To ReportControl.SelectedItems2.Count
                            ReportOut = ReportOut & "" & ReportControl.SelectedItems2(liCounter).ToString & "<br />"
                        Next

                        ReportOut = ReportOut & "</td></tr>"
                        lsSQL = lsSQL & ReportControl.SQL
                        'debug.print("Control " & ReportControl.ControlName & " SQL: " & ReportControl.SQL)
                        ' End If
                    End If
                Next

                '*** Cut Last AND off the Where Clause
                If lsSQL.Length > 4 Then
                    lsSQL = lsSQL.Substring(0, lsSQL.Length - 4)
                Else
                    lsSQL = ""
                End If

                ReportOut = "<table>" & ReportOut & "</table><br />"

            Else
                'response.write("No Criteria")
                f_CreateWhereClause = ""
            End If

            f_CreateWhereClause = lsSQL
CreateWhere_Exit:

            'debug.print("<SQL>" & lsSQL & "</SQL></f_CreateWhereClause>")
            Exit Function
        Catch err As Exception
            'debug.print(err.Message)
            '  Dim st As StackTrace = New StackTrace(Err, True)
            '// Get the top stack frame
            ' Dim frame As StackFrame = st.GetFrame(0)
            ' // Get the line number from the stack frame
            'Dim line As Long = frame.GetFileLineNumber()
            'response.write(err.Message)
        Finally
            'Resume CreateWhere_Exit
        End Try
    End Function
    Function f_LoadFromObject() As List(Of KeyValuePair(Of String, String))
        'debug.print("<f_LoadFromObject text='Loading Controls from Object'>")
        f_LoadFromObject = New List(Of KeyValuePair(Of String, String))
        Dim lsSQL As String = ""

        'Dim p As Panel
        Dim lsType As String = ""
        Dim lsTag As String = ""
        '     Dim rpt As Report
        Dim lsDescription As String = ""
        Dim lsCtrlName As String = ""
        Dim tbl As Table

        sUpdateControls()
        Try
            tbl = Me.FindControl("tbl")

            'debug.print("<controlcount count='" & tbl.Controls(0).Controls(0).Controls.Count.ToString & "' />")




            Dim liCounter As Integer
            '***************************************************************
            '*** Build Report Criteria
            '***************************************************************


            '***** 2020 DEC - These might not be necessary.  Get Values from rptctrls instead
            ' rpt = FindReportByID(Val(txtReportID.Text))
            ' rpt.LoadControls("")
            ' sUpdateControls() '*** Updates rptCtrls with updated Ctrl Values
            'For Each ReportControl In rpt.Controls
            For Each ReportControl In rptCtrls
                'debug.print("<ReportControl controlName='" & ReportControl.ControlName & "' />")
                'For Each ReportControl In tbl.Controls(0).Controls(0).Controls

                For liCounter = 1 To ReportControl.SelectedItems.Count
                    ReportOut = ReportOut & "" & ReportControl.SelectedItems(liCounter).ToString & "<br />"
                Next
                ReportOut = ReportOut & "</td><td valign='top'>"
                For liCounter = 1 To ReportControl.SelectedItems2.Count
                    ReportOut = ReportOut & "" & ReportControl.SelectedItems2(liCounter).ToString & "<br />"
                Next

                ReportOut = ReportOut & "</td></tr>"
                lsSQL = lsSQL & ReportControl.SQL
                '* Get Ordered pairs of FieldName and value for form values
                ' Loop over pairs.
                For Each pair As KeyValuePair(Of String, String) In ReportControl.FormValue
                    ' Get key.
                    Dim key As String = pair.Key
                    ' Get value.
                    Dim value As String = pair.Value
                    ' Display.
                    'debug.print("<" & key & " val=" & value & " />")
                    f_LoadFromObject.Add(New KeyValuePair(Of String, String)(key, value))
                Next

            Next

            '*** Cut Last AND off the Where Clause
            If lsSQL.Length > 4 Then
                lsSQL = lsSQL.Substring(0, lsSQL.Length - 4)
            Else
                lsSQL = ""
            End If

            ReportOut = "<table>" & ReportOut & "</table><br />SQL:<br/>"




LoadFromObject_Exit:
            'debug.print("</f_LoadFromObject>")
            Exit Function
        Catch err As Exception
            'debug.print("<error>" & err.Message & "</error>")
            '  Dim st As StackTrace = New StackTrace(Err, True)
            '// Get the top stack frame
            ' Dim frame As StackFrame = st.GetFrame(0)
            ' // Get the line number from the stack frame
            'Dim line As Long = frame.GetFileLineNumber()
            'response.write(err.Message)
        Finally
            'Resume CreateWhere_Exit
        End Try
    End Function
    Function f_setFromDataRow(ds As DataSet) As List(Of KeyValuePair(Of String, String))
        'debug.print("<ReportContainer.f_setFromDataRow>")
        'debug.print("<msg ms='Loading Controls from Object' />")
        f_setFromDataRow = New List(Of KeyValuePair(Of String, String))
        Dim lsSQL As String = ""
        Dim dr As DataRow
        Dim dt As DataTable
        Dim lsType As String = ""
        Dim lsTag As String = ""
        Dim rpt As Report
        Dim dc As DataColumn
        Dim lsDescription As String = ""
        Dim lsCtrlName As String = ""
        sUpdateControls()
        Dim tbl As Table
        Try
            tbl = Me.FindControl("tbl")
            '*** How Many Controls are in the Report Control Right Now?
            '*** Each Control is in a single table cell
            'debug.print("<controlcount>" & tbl.Controls(0).Controls(0).Controls.Count.ToString & "</controlcount>")

            '***************************************************************
            '*** Build Report Criteria
            '***************************************************************
            Dim lrli As New ReportListItem
            '*** Get the Report Category
            Dim i As DropDownList = cboReportCategory
            If lstReports.SelectedIndex = -1 And lstReports.Items.Count > 0 Then
                '       lstReports.SelectedIndex = 0
                'ReportID = lstReports.Items(0).Value 'SelectedValue
            End If
            rpt = FindReportByID(Val(txtReportID.Text))
            rpt.LoadControls("")
            sUpdateControls() '*** Updates rptCtrls with updated Ctrl Values


            '*** Find each Report Control and update from the Data Row
            For Each ReportControl In rpt.Controls
                '* Get Ordered pairs of FieldName and value for form values
                ' Loop over pairs.
                For Each dt In ds.Tables
                    For Each dr In dt.Rows
                        For Each dc In dr.Table.Columns
                            ' Create List Item

                            If dc.ColumnName = ReportControl.FieldName Then
                                '     If ReportControl.FieldName = "UnitTypeID" Then Stop
                                lrli = New ReportListItem
                                lrli.Value = dr(dc).ToString.Trim
                                lrli.Description = dr(dc).ToString.Trim
                                ReportControl.SelectedItems.Add(lrli)
                                Exit For
                            End If

                        Next
                    Next
                Next
            Next

            sUpdateUIControls(rpt)

LoadFromObject_Exit:
            'debug.print("</ReportContainer.f_setFromDataRow>")
            Exit Function
        Catch err As Exception
            'Stop
            'debug.print("<f_setFromDataRowError error='" & err.Message & "' />")
            '  Dim st As StackTrace = New StackTrace(Err, True)
            '// Get the top stack frame
            ' Dim frame As StackFrame = st.GetFrame(0)
            ' // Get the line number from the stack frame
            'Dim line As Long = frame.GetFileLineNumber()
            'response.write(err.Message)
        Finally
            'Resume CreateWhere_Exit
        End Try
    End Function
    Function nz(ByVal lv As VariantType, ByVal lvv As VariantType) As VariantType
        If IsDBNull(lv) Then
            nz = lvv
        Else
            nz = lv
        End If
    End Function
    Public Sub sDispose()
        'debug.print("<sDispose>")
        Dim liCount As Integer = 1
        Dim ctrl As Control
        Dim rptctrl As New ReportControl
        Dim lsCtrlName As String = ""

        Do While liCount > 0

            liCount = 0
            For Each ctrl In Me.Controls
                If ctrl Is Nothing Then
                    lsCtrlName = ""
                Else
                    lsCtrlName = ctrl.ID
                End If
                If lsCtrlName Is Nothing Then
                    lsCtrlName = ""
                End If
                '  txtDebug.Text = txtDebug.Text & Chr(13) & Chr(10) & lsCtrlName & " " '& ctrl.
                If lsCtrlName <> "" Then
                    rptctrl = rptCtrls.FindByName(lsCtrlName)
                    If rptctrl Is Nothing Then
                        ' If ctrl.Tag = "ReportControl" Then
                    Else
                        If lsCtrlName.ToLower <> "ctrltextbox1" And lsCtrlName.ToLower <> "ph" And lsCtrlName.ToLower <> "ctrlbase" And lsCtrlName.ToLower <> "cboreportcategory" And lsCtrlName.ToLower <> "litmsg" And lsCtrlName.ToLower <> "txtdebug" And lsCtrlName.ToLower <> "lstreports" Then
                            ' response.write("Disposing of " & ctrl.Name)
                            liCount = liCount + 1
                            ctrl.Dispose()
                        End If
                    End If
                End If
                rptctrl = Nothing
            Next
        Loop

        liCount = liCount

        'debug.print("</sDispose>")
    End Sub
    Function getControlFromName(ByRef containerObj As Object, ByVal name As String) As Control
        'debug.print("<getControlFromName><obj>" & containerObj.ToString & "</obj><name>" & name & "<name>")
        Try
            Dim tempCtrl As Control
            For Each tempCtrl In containerObj.Controls
                ' 'debug.print(tempCtrl.Name)
                If tempCtrl.ID.ToUpper.Trim = name.ToUpper.Trim Then
                    Return tempCtrl
                    'debug.print("<found>" & name & "</found></getControlFromName>")
                    Exit Function
                End If
            Next tempCtrl
        Catch ex As Exception
            'debug.print("<error>" & Err.Description & "</error>")
        End Try
        'debug.print("</getControlFromName>")
        Return Nothing
    End Function

    Private Sub sUpdateUIControls(rpt As Report)
        '*** SET UI Control Values From Report Control Objects
        '*** This is done to separate the SQL Logic from the User Controls
        '*** This should allow users to build user controls easier
        'Exit Sub
        Try
            'debug.print("<ReportContainer.sUpdateUIControls>")
            Dim ctrl As Control
            Dim ctrlb As ctrlBase
            ' Dim rpt As Report
            Dim p As Panel
            Dim rptctrl As ReportControl
            'Dim rptctrl2 As ReportControl


            '*** Get the Report ID and Set the Report object
            If txtReportID.Text = "" Then Exit Sub 'lstReports.SelectedValue = "" Then Exit Sub
            'rpt = FindReportByID(txtReportID.Text) 'lstReports.SelectedValue)
            'rpt.LoadControls(rpt.ReportName)
            ReportDescription = rpt.ReportDescription

            '** Find the UI Control Table on Container
            tbl = Me.FindControl("tbl")
            'debug.print("<msg count='" & tbl.Controls(0).Controls(0).Controls(0).Controls.Count.ToString & "' />")

            For Each rptctrl In rpt.Controls
                '*** Iterate through all UI Controls, looking for controls
                For Each p In tbl.Controls(0).Controls(0).Controls
                    For Each ctrl In p.Controls

                        ''debug.print("<msg ms='sUpdateControls:Up2-" & ctrl.ID & " Value:" & ctrl.ToString & "' />")
                        ''*** Controls capable of Criteria have a ReportControl Tag
                        'rptctrl2 = Nothing
                        'rptctrl2 = rpt.Controls.FindByName(ctrl.ID)
                        'If rptctrl2 Is Nothing Then
                        '    '*** Not a Report Control
                        'Else
                        '    'If ctrl.Tag = "ReportControl" Then
                        '    '*** Get the Control and assign it to ctrlb
                        '    ctrlb = ctrl
                        '    ctrlb.mbRefreshList = False
                        '    '*** Iterate through the controls in the REPORT Object
                        '    '*** Find out if they have a corresponding User Control
                        '    For Each rptctrl In rpt.Controls
                        '        '*** Is THIS the control I am looking for?
                        If rptctrl.ControlName = ctrl.ID Then '*Was ctrl.name
                            '*** Yes,  the Control has been Found,
                            '*** Set the Values
                            ctrlb = ctrl
                            ctrlb.DataOperator = (rptctrl.DataOperator)
                            ctrlb.DataType = rptctrl.DataType
                            ctrlb.SelectedItems = rptctrl.SelectedItems
                            ctrlb.SelectedItems2 = rptctrl.SelectedItems2
                            'ctrpt.Enabled = True
                            Exit For
                        End If

                    Next
                Next
            Next
            ''**********************************************************************
            ''*** Update Module Level Variable with Updated Controls Collection
            ''*********************************************************************
            'rptctrls = rpt.Controls

        Catch err As Exception
            'debug.print("<error>" & err.Message & "</error>") '& frame.ToString)
        Finally
            'debug.print("<End ReportContainer.sUpdateControls />")
        End Try
        'debug.print("</ReportContainer.sUpdateUIControls>")
    End Sub

    Private Sub sUpdateControls()
        '*** Get GUI Control Values and Update Report Control Objects
        '*** This is done to separate the SQL Logic from the User Controls
        '*** This should allow users to build user controls easier
        'Exit Sub
        Try
            'debug.print("<ReportContainer.sUpdateControls>")
            Dim ctrl As Control
            Dim ctrlb As ctrlBase
            Dim rpt As Report
            Dim p As Panel
            Dim rptctrl As ReportControl
            Dim rptctrl2 As ReportControl

            If txtReportID.Text = "" Then Exit Sub 'lstReports.SelectedValue = "" Then Exit Sub
            rpt = FindReportByID(txtReportID.Text) 'lstReports.SelectedValue)
            rpt.LoadControls(rpt.ReportName)
            ReportDescription = rpt.ReportDescription

            '*** Iterate through all UI Controls, looking for controls with Criteria
            tbl = Me.FindControl("tbl")
            'debug.print("<msg count='" & tbl.Controls(0).Controls(0).Controls(0).Controls.Count.ToString & "' />")
            For Each p In tbl.Controls(0).Controls(0).Controls
                For Each ctrl In p.Controls
                    If 2 = 1 Then
                        Exit Sub
                    End If
                    Debug.Print("<msg ms='sUpdateControls:Up2-" & ctrl.ID & " Value:" & ctrl.ToString & "' />")
                    '*** Controls capable of Criteria have a ReportControl Tag
                    rptctrl2 = Nothing
                    rptctrl2 = rpt.Controls.FindByName(ctrl.ID)
                    If rptctrl2 Is Nothing Then
                        '*** Not a Report Control
                    Else
                        'If ctrl.Tag = "ReportControl" Then
                        '*** Get the Control and assign it to ctrlb
                        ctrlb = ctrl
                        ctrlb.mbRefreshList = False
                        '*** Iterate through the controls in the REPORT Object
                        '*** Find out if they have a corresponding User Control
                        For Each rptctrl In rpt.Controls
                            '*** Is THIS the control I am looking for?
                            If rptctrl.ControlName = ctrl.ID Then '*Was ctrl.name
                                '*** Yes, If the Control has been Validated,
                                '*** get the Values
                                If ctrlb.Valid = True Then
                                    rptctrl.DataOperator = (ctrlb.DataOperator)
                                    rptctrl.DataType = ctrlb.DataType
                                    If ctrlb.SelectedItems.Count > 0 Then
                                        rptctrl.SelectedItems = ctrlb.SelectedItems
                                        'debug.print("Control " & rptctrl.ControlName & " Set to: " & rptctrl.SelectedItems(1).ToString)
                                        rptctrl.SelectedItems2 = ctrlb.SelectedItems2
                                    End If
                                    rptctrl.Enabled = True
                                    Exit For '2020-12-23
                                Else
                                    rptctrl.DataOperator = Nothing
                                    rptctrl.DataType = Nothing
                                    If rptctrl.SelectedItems.Count > 0 Then
                                        rptctrl.SelectedItems.Clear()
                                    End If
                                    If rptctrl.SelectedItems2.Count > 0 Then
                                        rptctrl.SelectedItems2.Clear()
                                    End If
                                    rptctrl.Enabled = False
                                End If
                            End If
                        Next
                    End If
                Next
            Next
            '**********************************************************************
            '*** Update Module Level Variable with Updated Controls Collection
            '*********************************************************************
            rptCtrls = rpt.Controls

        Catch err As Exception
            'debug.print("<error>" & err.Message & "</error>") '& frame.ToString)
        Finally
            'debug.print("<End ReportContainer.sUpdateControls />")
        End Try
        'debug.print("</ReportContainer.sUpdateControls>")
    End Sub

    Private Sub sUpdateChildren(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'debug.print("<sUpdateChildren sender='" & sender.ToString & "' />")
        GoTo Exit_Cmdrunreport_Click
        '*** Updates the List Controls That depend on this object for Criteria
        '*** Example: Kids Listbox would be refilled everytime a new Value was selected from
        '***          The Family Drop Down

        '*** Get GUI Control Values and Update Report Control Objects
        '*** This is done to separate the SQL Logic from the User Controls
        '*** This should allow users to build user controls easier
        Dim rptctrl As ReportControl
        Dim rpt As Report
        rpt = FindReportByID(txtReportID.Text)  'lstReports.SelectedValue)
        Dim ctrlb As ctrlBase

        Dim ctrlChild As New ReportControl
        Dim childctrl As ReportControlChildControl


        '*** Get the Control and assign it to ctrlb
        ctrlb = sender
        ctrlb.ForceValidation()
        '*** Iterate through the controls in the REPORT Object
        '*** Find out if they have a corresponding User Control
        For Each rptctrl In rpt.Controls
            '*** Is THIS the control I am looking for?
            If rptctrl.ControlName = ctrlb.ID Then
                '*** Yes, If the Control has been Validated,
                '*** get the Values
                If ctrlb.Valid = True Then
                    rptctrl.DataOperator = (ctrlb.DataOperator)
                    rptctrl.DataType = ctrlb.DataType
                    rptctrl.SelectedItems = ctrlb.SelectedItems
                    'debug.print("Control " & rptctrl.Value)
                    rptctrl.SelectedItems2 = ctrlb.SelectedItems2
                    rptctrl.Enabled = True
                    For Each childctrl In rptctrl.ControlChildren
                        SetChildListItems(childctrl.Description, rptctrl.SQL)
                    Next
                Else
                    rptctrl.DataOperator = Nothing
                    rptctrl.DataType = Nothing
                    If rptctrl.SelectedItems.Count > 0 Then
                        rptctrl.SelectedItems.Clear()
                    End If
                    If rptctrl.SelectedItems2.Count > 0 Then
                        rptctrl.SelectedItems2.Clear()
                    End If
                    rptctrl.Enabled = False
                End If
            End If
        Next

Exit_Cmdrunreport_Click:
        'debug.print("</sUpdateChildren>")
        Exit Sub

Err_Cmdrunreport_Click:
        'debug.print("<error>" & Err.Description & "</error>")
        If Err.Number <> 2501 Then
            'response.write(Err.Description & " - " & Err.Number)
        End If
        '        Resume
        Resume Exit_Cmdrunreport_Click
    End Sub


    Sub SetChildListItems(ByVal lsControlName As String, ByVal lsSQL As String)
        'debug.print("<SetChildListItems controlname='" & lsControlName & "' ><SQL>" & lsSQL & "</SQL>")
        Dim rptctrl As ReportControl
        Dim lsRowSource As String = ""
        Dim ctrl As ctrlBase
        Dim rpt As Report
        rpt = FindReportByID(txtReportID.Text) 'lstReports.SelectedValue)

        For Each rptctrl In rpt.Controls
            '*** Is THIS the control I am looking for?
            If rptctrl.ControlName = lsControlName Then
                '*** Yes, If the Control has been Validated,
                '*** get the Values
                '*** Cut Last "and " off the SQL
                If lsSQL.Length >= 4 Then
                    If lsSQL.Substring(lsSQL.Length - 4, 4) = "and " Then
                        lsSQL = lsSQL.Substring(0, lsSQL.Length - 4)
                    End If
                End If
                '*** Cut the Old Where Clause Off the Row Source
                lsRowSource = rptctrl.RowSource
                If lsRowSource.Length >= 5 Then
                    lsRowSource = lsRowSource.ToUpper
                    If lsRowSource.Contains("WHERE") Then
                        lsRowSource = lsRowSource.Substring(0, lsRowSource.IndexOf("WHERE"))
                    End If
                End If
                '*** Add the New Where Clause to the Row Source
                If lsSQL.Length > 5 Then
                    lsSQL = " Where " & lsSQL
                End If
                rptctrl.RowSource = (lsRowSource) & lsSQL
                rptctrl.LoadListItems()
                ctrl = getControlFromName(Me, lsControlName)
                ctrl.ListItems = rptctrl.ListItems
                ctrl.RefreshLists()
            End If
        Next
        'debug.print("</SetChildListItems>")
    End Sub

    Sub debugThis(ByVal msg As String)
        If mbDebug = True Then
            If msg = "Clear" Then
                litmsg.Text = ""
            Else
                litmsg.Text = litmsg.Text & msg & "<br/>" '  Chr(13) & Chr(10)
            End If
        End If
    End Sub
    Protected Sub lstReports_SelectedIndexChanged() Handles Me.Init
        'debug.print("<lstReport_SelectedIndexChanged>")
        Dim lsValue As String = ""
        If Not lstReports Is Nothing Then lsValue = lstReports.SelectedValue
        'debug.print("<msg lstReports.selectedValue= '" & lsValue & "' />")
        If lstReports Is Nothing Then
            'debug.print("There Are no Report in the Listbox Loaded")
        Else
            'debug.print("<msg lstReports_SelectedIndexChanged='Changed to " & lstReports.SelectedValue & "' />")
        End If
        ReportID = f_GetReportID()

        '   CreateChildControls() 'up2_Load(ReportID, True) ' *** THis guy was commented out for FOrms
        'debug.print("</lstReport_SelectedIndexChanged>")
    End Sub
    Protected Sub cboReportCategory_Changed()
        'debug.print("<cboReportCategory_Changed>")
        lstReports_Load()
        '   ShowMessage("Argh, password is wrong", "Error")
        'debug.print("</cboReportCategory_Changed>")
    End Sub

    Protected Sub up2_Load(liReportID As Integer, lbForce As Boolean) 'ByVal sender As Object, ByVal e As System.EventArgs) 'Handles up2.Load
        Dim createDiv As Panel ' System.Web.UI.HtmlControls.HtmlGenericControl
        createDiv = Nothing
        'debug.print("<ReportContainer.up2_load reportID='" & liReportID & "' >")
        If iTimeThrough > 0 Then 'And lbForce = False Then
            'debug.print("<msg ms='Controls only get added on the first time through or Force through Selecteditemchanged. so Exit up2.' />")
            'debug.print("</ReportContainer.up2_load>")
            Exit Sub
        End If
        iTimeThrough = iTimeThrough + 1 'Without exiting, this would empty all controls
        Dim strScript As String = "" ' "var testvar ='alert(''Yes'')';"
        If Not Page Is Nothing Then
            If (Not Page.ClientScript.IsStartupScriptRegistered("clientScript")) Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "clientScript", strScript, True)
            End If
        End If
        Dim rpt As New Report
        Dim ctrl As ctrlBase
        Dim reportCtrls As ReportControls

        '*** Get rid of Current Report Controls
        Me.tblCell.Controls.Clear()

        '*** initialize selection criteria fields
        mb_UseFilter = True

        '*** If No Report Selected, Exit Sub
        If liReportID < 1 Then Exit Sub

        '*** Get Selected Report Object
        rpt = FindReportByID(liReportID)
        TableName = rpt.TableName

        '*** Load NON Graphical Report Control Objects
        reportCtrls = rpt.LoadControls(msCnStr)
        'debug.print("<rptReportControls LoadControls='" & msCnStr & "' />")

        '*** Load Graphical User Control Report Objects
        For Each ReportControl In reportCtrls
            'debug.print("<ReportControl Type='" & ReportControl.Type & "' />")
            Select Case UCase(ReportControl.Type)
                Case "DATEBOX"
                    ctrl = New ctrlDateBox(ReportControl) 'ctrlTextBox '(ReportControl)
                    ctrl.mrptCtrl = ReportControl
                Case "LISTBOX"
                    ctrl = New ctrlListBox(ReportControl)
                    ctrl.mrptCtrl = ReportControl
                Case "CHECKBOX"
                    ctrl = New ctrlCheckBox(ReportControl)
                    'debug.print("<ctrl FieldName='" & ctrl.FieldName & "' />")
                    ctrl.mrptCtrl = ReportControl
                    'debug.print("<msg Desc='" & ctrl.FieldName & "' />")
                        ''  ctrl = New ctrlListbox(ReportControl)
                        'ctrl = LoadControl("ctrlCheckBox.ascx")
                        'ctrl.mrptCtrl = ReportControl

                Case "COMBOBOX"
                    ctrl = New ctrlComboBox(ReportControl)
                    ctrl.mrptCtrl = ReportControl
                    ' AddHandler ctrl.ControlUpdated, AddressOf sUpdateChildren '(sender, e) ' sUpdateChildren
                Case Else
                    ctrl = New ctrlTextBox(ReportControl) 'LoadControl("ctrlTextBox.ascx")
                    ctrl.mrptCtrl = ReportControl
            End Select
            If ctrl Is Nothing Then
                'debug.print("<CTRL ID='Nothing' />")
            Else
                'debug.print("<CTRL ID='" & ctrl.ID & "' />")
            End If
            '    AddHandler  ctrl.Leave, AddressOf sUpdateChildren
            ctrl.Tag = "ReportControl"
            ctrl.ID = ReportControl.ControlName
            ctrl.FieldName = ReportControl.FieldDescription
            ctrl.HelpText = ReportControl.ReportControlHelpText
            ctrl.msReportType = IIf(IsDBNull(rpt.ReportType), "Form", rpt.ReportType)
            ctrl.CssClass = "CustomControl"
            If ReportControl IsNot Nothing Then
                '  On Error Resume Next
                '*** Does the Control have a Specific Container name
                If ReportControl.ReportControlContainerName <> "" Then
                    Dim liSize As Integer = ReportControl.ReportControlColumnSize
                    If liSize < 1 Then liSize = 1
                    If liSize > 12 Then liSize = 12
                    ctrl.ControlPanelcss = ("form-group col-md-" & Trim(liSize.ToString))
                    '   ctrl.ControlStyle = "form-group col-md-" & Trim(liSize.ToString)
                    '***    Yes, Does the Container DIV Exist?
                    For Each p As Panel In tblCell.Controls
                        If p.ID = "div" & ReportControl.ReportControlContainerName Then
                            '*** Yes, Panel already exists, we dont need to create a new one, just add to this one
                            createDiv = p
                            Exit For
                        End If
                    Next
                    If createDiv Is Nothing Then
                        '***        No,  Create the Container Name based on the Controls Container Name and Get that Container
                        createDiv = New Panel
                        createDiv.ID = "div" & ReportControl.ReportControlContainerName
                        createDiv.Attributes("class") = "form-row"
                        tblCell.Controls.Add(createDiv)
                    End If
                Else
                    '***    No, Create the Container Name Based on the Control Name and get THAT Container
                    createDiv = New Panel 'System.Web.UI.HtmlControls.HtmlGenericControl("DIV")
                    createDiv.ID = "div" & ctrl.ID
                    createDiv.Attributes("class") = "form-group"
                    'createDiv.InnerHtml = " I'm a div, from code behind "
                    tblCell.Controls.Add(createDiv)
                End If


                '*** Lets try to set the values here
                'ctrl.setValues()
                '*******************
                If Me.Attributes("EditMode") = "True" Then
                    ctrl.EditMode = True
                End If
                createDiv.Controls.Add(ctrl)

                ctrl.SetFields("", "") '*** Try it here
                On Error GoTo 0
                ctrl = Nothing
                createDiv = Nothing
            End If
        Next

        EnsureChildControls()
        'debug.print("</ReportContainer.up2_load>")
    End Sub

    Function FindReportByID(ByVal llReportID As Long) As Report
        'debug.print("<FindReportByID ID='" & llReportID & "'>")
        Dim rpt As Report
        If Not mcReports Is Nothing Then

            For Each rpt In mcReports
                If llReportID = rpt.ReportID Then
                    FindReportByID = rpt
                    ReportDescription = rpt.ReportDescription
                    'debug.print("<FoundReport ID='" & llReportID & "'/></FindReportByID'>")
                    Exit Function
                End If
            Next
        End If
        FindReportByID = New Report
        Dim ds As DataSet
        ds = mdlDataLoader.LoadReports(0, "", llReportID)
        If ds Is Nothing Then ds = New DataSet
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                FindReportByID.ReportID = ds.Tables(0).Rows(0)("ReportID")
                FindReportByID.TableName = ds.Tables(0).Rows(0)("TableName")
            End If
        End If
        'debug.print("</FindReportByID>")
    End Function
    Public Sub Refresh()
        ' 'debug.print("<ReportContainer.Refresh>")
        msWhereClause = f_CreateWhereClause(True)
        ''debug.print("<WhereClause>" & msWhereClause & "</WhereClause>")
        ''debug.print("</ReportContainer.Refresh>")
    End Sub

    Private Sub ReportContainer_DataBinding(sender As Object, e As EventArgs) Handles Me.DataBinding
        'debug.print("<ReportContainer_dataBinding>")
        Dim d As DropDownList = cboReportCategory
        'debug.print("<val DropdownSelectedValue='" & d.SelectedValue & "' />")
        'debug.print("</ReportContainer_dataBinding>")
    End Sub

    Private Sub ReportContainer_Unload(sender As Object, e As EventArgs) Handles Me.Unload
        'debug.print("<Unload SelectedValue='" & f_GetReportID() & "' />")
    End Sub
    Public Sub ShowMessage(Message As String, lsType As String)
        'Show Bootstrap Message
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "myScript" & Guid.NewGuid.ToString, "ShowMessage('" & Message & "','" & lsType & "');", True)
    End Sub




    '*** Thise is my attempt to get the child control to raise an event here for clicking the edit Field Button each control should have
    Protected Overrides Function OnBubbleEvent(ByVal sender As Object, ByVal e As EventArgs) As Boolean
        Dim lsControlText As String = ""
        Dim cmd As Button
        Dim lnk As LinkButton
        '*** Bubbled up, Tell control NOT to rerender the Form Controls so we can show the Field Manager form



        If TypeOf e Is CommandEventArgs Or TypeOf e Is EventArgs Then
            Dim cp As Panel = Me.FindControl("up1")
            Dim rfe As mycontrol_ = cp.FindControl("ReportManagerFieldEditor")
            If Not rfe Is Nothing Then
                If Not cp Is Nothing Then
                    '********************************************************************
                    '*** Get Field Data from the Report fields table to configure forms
                    '********************************************************************
                    Dim lsReportID As String = "0" 'ReportID
                    Dim llcontrolNum As Long = 0
                    Dim lsFieldID As String = ""

                    If TypeOf (sender) Is Button Or TypeOf (sender) Is LinkButton Then
                        If TypeOf (sender) Is Button Then
                            cmd = sender 'Return False
                            lsControlText = cmd.ID
                            Dim keys As IEnumerator = cmd.Attributes.Keys.GetEnumerator()
                            Dim liCounter As Integer = 1
                            Dim Key As String
                            While keys.MoveNext
                                Key = keys.Current
                                Select Case Key
                                    Case "data-reportid" : lsReportID = cmd.Attributes(Key)
                                    Case "data-reportcontrolfieldnumid" : llcontrolNum = cmd.Attributes(Key)
                                    Case "data-reportcontrolfieldid" : lsFieldID = cmd.Attributes(Key)
                                End Select
                            End While

                        End If
                        If TypeOf (sender) Is LinkButton Then
                            lnk = sender 'Return False
                            lsControlText = lnk.ID
                            ' If lnk.ID = "cmdNewField" Then Return False
                            Dim keys As IEnumerator = lnk.Attributes.Keys.GetEnumerator()
                            Dim liCounter As Integer = 1
                            Dim Key As String
                            While keys.MoveNext
                                Key = keys.Current
                                Select Case Key
                                    Case "data-reportid" : lsReportID = lnk.Attributes(Key)
                                    Case "data-reportcontrolfieldnumid" : llcontrolNum = lnk.Attributes(Key)
                                    Case "data-reportcontrolfieldid" : lsFieldID = lnk.Attributes(Key)
                                End Select
                            End While

                        End If
                        If lsControlText.Length > 36 Then
                            Dim lsTemp As String = Right(lsControlText, 36)
                            Dim guidOutput As Guid
                            If Guid.TryParse(lsTemp, guidOutput) Then lsFieldID = lsTemp
                        End If
                        If lsControlText.ToUpper.Contains("POPULATE") Then
                            setPanelVisibility()

                        ElseIf lsControlText.ToUpper.Contains("DELETE") Then
                            Dim c As New clsDataLoader
                            c.DeleteReportField(lsFieldID)
                            Me.DataBind()
                        End If
                        '**** Refresh the Main Panel
                        Dim llReportID As Long = Val(lsReportID)
                        rfe.LoadForm(llReportID, llcontrolNum, lsFieldID)
                        'debug.print(Me.Controls.Count)
                    End If

                    'Stop the bubbling of the CommandEventArgs
                    Return True
                End If

            End If
        End If

        'If we are dealing with something other than a CommandEventArgs,
        'let the bubbling proceed unmolested...
        Return False
    End Function










    'Public Event DateSelected As DateSelectedEventHandler
    'Protected Overridable Sub onDateSelection(e As DateSelectedEventArgs)

    '    RaiseEvent DateSelected(Me, e)

    'End Sub

    '<Category("Appearance")>
    '<Description("This is the Selected Date")>
    'Public Property SelectedDate() As DateTime
    '    Get
    '        EnsureChildControls()
    '        If (tb.Text) Is Nothing Then
    '            Return String.Empty
    '        Else
    '            Return Convert.ToDateTime(tb.Text)
    '        End If

    '    End Get
    '    Set(ByVal value As DateTime)
    '        EnsureChildControls()
    '        If IsDBNull(value) Then
    '            tb.Text = ""
    '            cal.VisibleDate = DateTime.Today
    '        Else
    '            tb.Text = value.ToShortDateString()
    '            cal.SelectedDate = value
    '        End If
    '    End Set
    'End Property
    '<Category("Appearance")>
    '<Description("Changes the way it it looks")>
    'Public Property ImageButtonImageURL() As String
    '    Get
    '        EnsureChildControls()
    '        If (imgbt.ImageUrl) Is Nothing Then
    '            Return String.Empty
    '        Else
    '            Return imgbt.ImageUrl
    '        End If

    '    End Get
    '    Set(ByVal value As String)
    '        EnsureChildControls()
    '        imgbt.ImageUrl = value
    '    End Set
    'End Property
    'Private Sub imgbt_Click(sender As Object, e As ImageClickEventArgs)
    '    If cal.Visible Then
    '        cal.Visible = False
    '    Else
    '        cal.Visible = True
    '        If IsDBNull(tb.Text) Then
    '            cal.VisibleDate = DateTime.Today
    '        Else
    '            Dim output As DateTime = DateTime.Today
    '            Dim isDateTimeConversionSuccessful As Boolean = DateTime.TryParse(tb.Text, output)
    '            cal.VisibleDate = output

    '        End If
    '    End If
    'End Sub
    'Private Sub cal_SelectionChanged(sender As Object, e As EventArgs)
    '    tb.Text = cal.SelectedDate.ToShortDateString
    '    Dim eventData As DateSelectedEventArgs = New DateSelectedEventArgs(SelectedDate)
    '    onDateSelection(eventData)
    '    cal.Visible = False
    'End Sub
End Class
''Public Class DateSelectedEventArgs
''    Inherits EventArgs
''    Private _SelectedDate As DateTime
''    Public Sub New(SelectedDate As DateTime)
''        _SelectedDate = SelectedDate
''    End Sub
''    Public Sub DateSelectedEventArgs(SelectedDate As DateTime)
''        _SelectedDate = SelectedDate
''    End Sub
''    Public ReadOnly Property SelectedDate As DateTime
''        Get
''            Return _SelectedDate
''        End Get
''    End Property
''End Class
''Public Delegate Sub DateSelectedEventHandler(Sender As Object, e As DateSelectedEventArgs)



'**** <summary>
'****   Allows string-based ITempalte implementations
'**** </summary>
Public Class StringTemplate
    Implements ITemplate
    '  #Region Constructor(s)

    '****'*'*'*//
    '**** <summary>
    '****   Constructor
    '**** </summary>
    '**** <param id="quot;template">String" based version of the control template.</param>
    Public Sub New(template As String)
        template = template
    End Sub

    '**** <summary>
    '****   Constructor
    '**** </summary>
    '**** <param id="quot;template">String" based version of the control template.</param>
    '**** <param id="quot;copyToContainer">True" to copy intermediate container contents to the instantiation container, False to leave the intermediate container in place.</param>
    Public Sub New(Template As String, copyToContainer As Boolean)
        Template = Template
        copyToContainer = copyToContainer
    End Sub

    '****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****'****//

    '  #endregion

    '  #Region Properties

    '*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*'*//

    '* <summary>
    '*   String based template
    '* </summary>
    Public Template As String


    '* <summary>
    '*   When a StringTemplate is instantiated it is created inside an intermediate control
    '*   due to limitations of the .NET Framework.  Specifying True for the CopyToContainer
    '*   property copies all the controls from the intermediate container into instantiation
    '*   container passed to the InstantiateIn method.
    '* </summary>
    Public CopyToContainer As Boolean


    '*///'*'*//

    ' #endregion

    '  #Region ITemplate Members

    '*'*'*'*//

    '* <summary>
    '*   Creates the template in the specified control.
    '* </summary>
    '* <param id="quot;container">Control" in which to make the template</param>
    Private Sub ITemplate_InstantiateIn(container As Control) Implements ITemplate.InstantiateIn
        'Throw New NotImplementedException()
        Dim tempContainer As Control = container.Page.ParseControl(Template)
        If (CopyToContainer) Then

            For i As Integer = tempContainer.Controls.Count - 1 To 0 Step -1
                Dim tempControl As Control = tempContainer.Controls(i)
                tempContainer.Controls.RemoveAt(i)
                container.Controls.AddAt(0, tempControl)
            Next
        Else
            container.Controls.Add(tempContainer)
        End If
    End Sub
    '*'*'*'*//
    '  #endregion
End Class ' //class