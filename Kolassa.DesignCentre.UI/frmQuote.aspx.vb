Imports Kolassa.DesignCentre.Data

Partial Class frmQuote
	Inherits System.Web.UI.Page

	<System.Web.Services.WebMethod()>
	Public Shared Function GetContactName(ByVal custid As String) As String
		GetContactName = "Fstick"
	End Function
	'*******************************************************
	'*** I think this is all accounted for
	'*** Just keeping it around for reference
	'*****************************************************
	'    Dim mbResize As Boolean
	'    '### Dim cLog As New clsEventLog

	Private Sub lstCategories_AfterUpdate()

		'lstLevels.RowSource = "SELECT tblUpgradeOptions.UnitType, tblUpgradeOptions.UpgradeCategory, " &
		'"tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeLevel " &
		'"FROM tblUpgradeOptions " &
		'"GROUP BY tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, " &
		'"tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.UpgradeLevel " &
		'"HAVING (((tblUpgradeOptions.[UnitType)='" & [Forms.frmQuote.UnitID.Column(3) & "') AND " & _
		'        "((tblUpgradeOptions.[Location)='" & [Forms.frmQuote.lstRooms.Column(2) & "') and " & _
		'        "((tblUpgradeOptions.UpgradeCategory) = '" & [Forms.frmQuote.lstCategories.Column(1) & "'))" & _
		'        "ORDER BY tblUpgradeOptions.UpgradeLevel;"

		'        'response.write (lstLevels.RowSource)

		'        lstLevels.Requery()

		'lstStyle.RowSource = ""
		'lstStyle.Requery()

		'Me.lstLevels.Visible = True
		'	Me.lstLevels.Enabled = True
		'	Me.lstStyle.Visible = True
		'	Me.lstStyle.Enabled = False

	End Sub

	Private Sub lstCategories_Click()
		sDisplayListbox(lstCategories)
	End Sub




	Private Sub BuildingPhaseOption_AfterUpdate()
		'Dim lsRoom As String = lstRooms.Text
		'Dim lsQuoteID As String = txtQuoteID.Text
		'lstRequestedUpgrades.RowSource = "SELECT tblRequestedUpgrades.RequestedUpgradeID, " &
		'        "tblRequestedUpgrades.UpgradeCategory as Category, " &
		'        "tblRequestedUpgrades.UpgradeClass as Upgrade Level, " &
		'        "tblRequestedUpgrades.UpgradeDescription as Description, " &
		'        "tblRequestedUpgrades.StyleDescription as Style, " &
		'        "tblRequestedUpgrades.CustomerPrice + tblRequestedUpgrades.Adjustments AS [Cost " &
		'        "FROM tblRequestedUpgrades " &
		'        "WHERE (tblRequestedUpgrades.RoomDescription)= '" & lsRoom & "'" &
		'        "and (tblRequestedUpgrades.BuildingPhase) = " & rblPhase.SelectedValue & " " &
		'        "and (tblRequestedUpgrades.quoteid) = " & lsQuoteID & " order by tblRequestedUpgrades.UpgradeCategory"

		'If Me.rblPhase.SelectedValue = "Structural" Then

		'    lstCategories.RowSource = ""
		'    lstCategories.Requery()
		'    Me.lstCategories.Enabled = False
		'    lstLevels.RowSource = ""
		'    lstLevels.Requery()
		'    Me.lstLevels.Enabled = False
		'    Me.lstLevels.Visible = True

		'    lstStyle.RowSource = ""
		'    lstStyle.Requery()
		'    Me.lstStyle.Enabled = False
		'    Me.lstStyle.Visible = True
		'Else


		'    lstCategories.RowSource = ""
		'    lstCategories.Requery()
		'    Me.lstCategories.Enabled = False
		'    Me.lstCategories.Visible = True
		'    '
		'    lstLevels.RowSource = ""
		'    lstLevels.Requery()
		'    Me.lstLevels.Enabled = False
		'    Me.lstLevels.Visible = True
		'    '
		'    lstStyle.RowSource = ""
		'    lstStyle.Requery()
		'    Me.lstStyle.Enabled = False
		'    Me.lstStyle.Visible = True
		'    '
		'End If
		''
		'lstRequestedUpgrades.Requery()
		sCheckPhaseComplete()
	End Sub

	Private Sub lstRooms_AfterUpdate()

		On Error GoTo lstRooms_AfterUpdate_error

		'lstRequestedUpgrades.RowSource = "SELECT tblRequestedUpgrades.RequestedUpgradeID, " &
		'        "tblRequestedUpgrades.UpgradeCategory as Category, " &
		'        "tblRequestedUpgrades.UpgradeClass as [Upgrade Level, " &
		'        "tblRequestedUpgrades.UpgradeDescription as Description, " &
		'        "tblRequestedUpgrades.StyleDescription as Style, " &
		'        "tblRequestedUpgrades.CustomerPrice + tblRequestedUpgrades.Adjustments AS Cost " &
		'        "FROM tblRequestedUpgrades " &
		'        "WHERE (tblRequestedUpgrades.RoomDescription)= '" & lstRooms.Column(2) & "'" &
		'        "and (tblRequestedUpgrades.BuildingPhase) = " & Me.BuildingPhaseOption & " " &
		'        "and (tblRequestedUpgrades.quoteid) = " & Me.QuoteID & " order by tblRequestedUpgrades.UpgradeCategory"

		'lstRequestedUpgrades.Requery()

		'lstCategories.RowSource = "SELECT tblUpgradeOptions.UnitType, tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.Location " &
		'"FROM tblUpgradeOptions " &
		'"WHERE (((tblUpgradeOptions.Active) = Yes)) " &
		'"GROUP BY tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.BuildingPhase " &
		'"HAVING (((tblUpgradeOptions.[UnitType)='" & [Forms.frmQuote.UnitID.Column(3) & "') AND " & _
		'        "((tblUpgradeOptions.[BuildingPhase)=" & [Forms.frmQuote.BuildingPhaseOption & ") AND " & _
		'        "((tblUpgradeOptions.Location)='" & [Forms.frmQuote.lstRooms.Column(2) & "'));"

		'        'response.write (lstCategories.RowSource)

		'        Me.lstCategories.Enabled = True
		'lstCategories.Requery()

		'lstLevels.RowSource = ""
		'lstLevels.Requery()
		'Me.lstLevels.Enabled = False
		''
		'lstStyle.RowSource = ""
		'lstStyle.Requery()
		'Me.lstStyle.Enabled = False
		''


		'DoCmd.DoMenuItem acFormBar, acRecordsMenu, 5, , acMenuVer70

exit_lstRooms_AfterUpdate:
		Exit Sub

lstRooms_AfterUpdate_error:
		Response.Write("error updating Requested Upgrades")
		Response.Write(Err.Description)

		Resume exit_lstRooms_AfterUpdate

	End Sub

	Private Sub lstRooms_Click()
		sDisplayListbox(lstRooms)
	End Sub

	Private Sub lstStyle_Click()
		'  sDisplayListbox(lstStyle)
	End Sub

	Private Sub lstLevels_AfterUpdate()
		'*** Think this all gets done through the ObjectDataSource
		'If Me.rblPhase.SelectedValue = 1 Then
		'	Me.lstStyle.Enabled = True

		'	lstStyle.RowSource = "SELECT tblUpgradeOptions.Description, tblUpgradeOptions.ModelorStyle as Style, " &
		'	"tblUpgradeOptions.CustomerPrice, tblUpgradeOptions.UnitType, tblUpgradeOptions.UpgradeCategory, " &
		'	"tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeLevel, tblUpgradeOptions.UpgradeOptionID " &
		'	"FROM tblUpgradeOptions " &
		'	"GROUP BY tblUpgradeOptions.active, tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, " &
		'	"tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.UpgradeLevel, " &
		'	"tblUpgradeOptions.Description, tblUpgradeOptions.ModelOrStyle, " &
		'	"tblUpgradeOptions.CustomerPrice, tblUpgradeOptions.UpgradeOptionID " &
		'	"HAVING  tblUpgradeOptions.active=true and (((tblUpgradeOptions.[UnitType)='" & [Forms.frmQuote.UnitID.Column(3) & "') AND " & _
		'            "((tblUpgradeOptions.[Location)='" & [Forms.frmQuote.lstRooms.Column(2) & "') and " & _
		'            "((tblUpgradeOptions.[UpgradeLevel)=""" & fReplaceQuotes([Forms.frmQuote.lstLevels.Column(3)) & """) and " & _
		'            "((tblUpgradeOptions.[UpgradeCategory) = """ & fReplaceQuotes([Forms.frmQuote.lstCategories.Column(1)) & """));"
		'        Else ' Me.BuildingPhaseOption = 2
		'	Me.lstStyle.Enabled = True

		'	lstStyle.RowSource = "SELECT tblUpgradeOptions.Description, tblUpgradeOptions.ModelorStyle, " &
		'	"tblUpgradeOptions.CustomerPrice, tblUpgradeOptions.UnitType, tblUpgradeOptions.UpgradeCategory, " &
		'	"tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeLevel, tblUpgradeOptions.UpgradeOptionID " &
		'	"FROM tblUpgradeOptions " &
		'	"GROUP BY tblUpgradeOptions.active, tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, " &
		'	"tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.UpgradeLevel, " &
		'	"tblUpgradeOptions.Description, tblUpgradeOptions.ModelOrStyle, " &
		'	"tblUpgradeOptions.CustomerPrice, tblUpgradeOptions.UpgradeOptionID " &
		'	"HAVING tblUpgradeOptions.active=true and (((tblUpgradeOptions.[UnitType)='" & [Forms.frmQuote.UnitID.Column(3) & "') AND " & _
		'            "((tblUpgradeOptions.[Location)='" & [Forms.frmQuote.lstRooms.Column(2) & "') and " & _
		'            "((tblUpgradeOptions.[UpgradeLevel)=""" & fReplaceQuotes([Forms.frmQuote.lstLevels.Column(3)) & """) and " & _
		'            "((tblUpgradeOptions.[UpgradeCategory) = """ & fReplaceQuotes([Forms.frmQuote.lstCategories.Column(1)) & """));"

		'           ' Debug.Print(lstStyle.RowSource)
		'	lstStyle.Requery()
		'End If

	End Sub

	'Private Sub lstLevels_Click()
	'	sDisplayListbox(lstLevels)
	'End Sub

	Private Sub UnitID_Change()
		On Error GoTo Err_UnitID_Click

		'    DoCmd.DoMenuItem acFormBar, acRecordsMenu, 5, , acMenuVer70

Exit_UnitID_Click:
		Exit Sub

Err_UnitID_Click:
		Response.Write(Err.Description)
		Resume Exit_UnitID_Click

	End Sub

	' ***
	' *** Gotta write this with JavaScript
	Private Sub txtPhase1CompleteDate_DblClick(txt As TextBox, ddl As DropDownList, ByVal Cancel As Integer)
		If Not IsDate(txt.Text) Then
			txt.Text = Date.Today
		Else
			txt.Text = DateAdd(DateInterval.Day, -1, CDate(txt.Text))
		End If

		ddl.SelectedValue = "Completed"

Exit_txtPhase1CompleteDate_DblClick:
		Exit Sub

Err_txtPhase1CompleteDate_DblClick:
		Response.Write(Err.Description)
		Resume Exit_txtPhase1CompleteDate_DblClick
	End Sub


	'***
	'*** Gotta write this with JavaScript
	Private Sub txtPhase1TargetDate_DblClick(ByVal Cancel As Integer)

		On Error GoTo Err_txtPhase1TargetDate_DblClick

		'If IsNull(Me.ActiveControl) Then
		'    Me.ActiveControl = Date + 14
		'Else
		'    Me.ActiveControl = Me.ActiveControl - 1
		'End If

Exit_txtPhase1TargetDate_DblClick:
		Exit Sub

Err_txtPhase1TargetDate_DblClick:
		Response.Write(Err.Description)
		Resume Exit_txtPhase1TargetDate_DblClick

	End Sub


	' ***
	' *** Gotta write this with JavaScript
	Private Sub txtPhase2CompleteDate_DblClick(ByVal Cancel As Integer)
		'If IsNull(Me.ActiveControl) Then
		'    Me.ActiveControl = Date
		'Else
		'    Me.ActiveControl = Me.ActiveControl - 1
		'End If

		'Me.cmbphase2status = "Completed"

Exit_txtPhase2CompleteDate_DblClick:
		Exit Sub

Err_txtPhase2CompleteDate_DblClick:
		Response.Write(Err.Description)
		Resume Exit_txtPhase2CompleteDate_DblClick
	End Sub

	Private Sub cmbphase2status_AfterUpdate()
		Dim ddlPhase2Status As DropDownList = fvQuote.FindControl("cmbphase2status_AfterUpdate")
		Dim txt As TextBox = fvQuote.FindControl("txtPhase2CompleteDate")
		If ddlPhase2Status.Text = "Completed" Then
			If Not IsDate(txt.Text) Then
				txt.Text = Now()
			End If

		End If

	End Sub

    Private Sub cmdAssign_Click(sender As Object, e As EventArgs) Handles cmdAssign.Click
        Dim lsID As String = lblQuoteID.Text 'Session("QuoteID")
        Dim q As New clsQuote(lsID)

        Dim lsAssignedToID As String = ""
		For Each key As String In Request.Form.AllKeys
			If (key.Contains("cboAssignedTo")) Then
				lsAssignedToID = Request.Form(key)
				Exit For
			End If
		Next
        Dim lsURL As String = "frmQuote" & "?QuoteID=" + lsID
		If lsID.Length = 36 And lsAssignedToID.Length = 36 Then
			q.AssignQuoteToSales(lsID, lsAssignedToID, lsURL, Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd("/") + "/")
            litAssignedToName.Text = q.AssignedTo
            lblAssigned.Text = "Assigned to " & litAssignedToName.Text
            cmdAssign.Enabled = False
			cmdAssign.CssClass = "btn btn-secondary"
		End If
	End Sub
    Private Sub cmdAssignCustomer_Click(sender As Object, e As EventArgs) Handles cmdAssignCustomer.Click
        Dim lsID As String = lblQuoteID.Text 'Session("QuoteID")
        Dim q As New clsQuote(lsID)

        Dim lsAssignedToID As String = ""
        For Each key As String In Request.Form.AllKeys
            If (key.Contains("cboAssignedCustomer")) Then
                lsAssignedToID = Request.Form(key)
                Exit For
            End If
        Next
        Dim lsURL As String = "frmQuote" & "?QuoteID=" + lsID
        If lsID.Length = 36 And lsAssignedToID.Length = 36 Then
            q.AssignQuoteToCustomer(lsID, lsAssignedToID, lsURL, Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd("/") + "/")
            LitCustomer.Text = q.CustomerName
            lblAssigned.Text = "Assigned to " & LitCustomer.Text
            cmdAssign.Enabled = False
            cmdAssign.CssClass = "btn btn-secondary"
        End If
    End Sub


    Sub sDisplayListbox(ByVal lst As ListBox)
		'*** This outputs the values of a listbox for debugging
		Dim liCounter As Integer = 0
		Dim lsMsg As String = ""
		Dim liRetVal As Integer = 0
		'For liCounter = 0 To lst.columnCOunt - 1
		'    lsMsg = lsMsg & " " & lst.Column(liCounter)
		'Next
		'liRetVal = SysCmd(acSysCmdSetStatus, lsMsg)

	End Sub

    Private Sub Form_Load()
        checkQuoteID()
        LoadQUote()
    End Sub
    Private Sub LoadQuote()
        checkQuoteID()
        Dim cmbStatus As DropDownList
		If rblPhase.SelectedValue = 1 Then
			Me.cmdAutoPick.Enabled = False
			' Me.AddNewOption.Enabled = False
		Else
			Me.cmdAutoPick.Enabled = True
			'*** Set value to True to allow new finish selection on the fly.
			' Me.AddNewOption.Enabled = False
		End If

		cmbStatus = Me.fvQuote.FindControl("cmbQuoteStatus")
        If cmbStatus.Text = "Completed" Then
            'lstRooms.Enabled = False
            lstCategories.Enabled = False
            'lstLevels.Enabled = False
            'lstStyle.Enabled = False
            lstSelectedUpgrade.Enabled = False

            Me.fvQuote.Enabled = False
            Me.cmdAddNewOption.Enabled = False
            Me.cmdAutoPick.Enabled = False
            Me.fvQuote.Enabled = False

        Else
            cmbStatus = fvQuote.FindControl("")
            If cmbStatus.Text = "Completed" Then
                rblPhase.SelectedValue = 2
            End If

            lstRooms.Enabled = True
            'lstRequestedUpgrades.Enabled = True
            fvQuote.Enabled = True

        End If
        lstRooms.Enabled = True
        lstLevels.Enabled = True
        lstSelectedUpgrade.Enabled = True
        lstCategories.Enabled = True
    End Sub
    Sub checkQuoteID()
		Dim lsQuoteID As String = Request.QueryString("QuoteID")
		Dim cp As New clsPersonalData
		Dim a As New GlobalFunctionsDC
		Dim lsLast
		cp.getlastValue("Quote")
		lsLast = cp.GuidValue
		If lsQuoteID Is Nothing Or lsQuoteID = "" Then
			'*** Load Project from Database from personal data table for last Project

			If Not a.isGUIDString(lsLast) Then lsQuoteID = lsLast
		Else
			If a.isGUIDString(lsQuoteID) Then
				cp = New clsPersonalData
				cp.GuidValue = lsQuoteID
				cp.UsageType = "Quote"
				cp.Insert()
			End If
		End If


		If Not a.isGUIDString(lsQuoteID) Then lsQuoteID = ""
		Dim lsCurrentProj As String = Session("QuoteID")
		If Session("Project") Is Nothing Then lsCurrentProj = ""
		If lsQuoteID Is Nothing Then lsQuoteID = ""
		If lsQuoteID = "" Then
			Exit Sub
		End If
        '  fvQuote.Caption = lsQuoteID ' 2023-01

        If Session("QuoteID") = lsQuoteID Then
            Exit Sub
		End If
		Session("QuoteID") = lsQuoteID
	End Sub
	'*******************************************************
	'*** END OF LEGACY CODE Already converted
	'*** Just keeping it around for reference
	'*****************************************************

	Private Sub AddNewOption_Click()
		On Error GoTo Err_AddNewOption_Click

		Dim stDocName As String
		Dim stLinkCriteria As String
		'    stLinkCriteria = "[UnitType=" & Me.CustomerID & " and [BuildingPhase=" & Me.BuildingPhaseOption

		stDocName = "frmOptionPricingQuickLoad"
		'### DoCmd.OpenForm(stDocName, , , stLinkCriteria)
Exit_AddNewOption_Click:
		Exit Sub

Err_AddNewOption_Click:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_AddNewOption_Click
	End Sub

	Private Sub btnAdjustments_Click()
		On Error GoTo Err_btnAdjustments_Click

		Dim stDocName As String
		Dim stLinkCriteria As String

		stDocName = "frmAdjustments"
		'    stLinkCriteria = "[QuoteID=" & Me.QuoteID & " and [BuildingPhase=" & Me.BuildingPhaseOption

		'stLinkCriteria = "[QuoteID=" & Me.txtQuoteID.Text
		'###   DoCmd.OpenForm(stDocName, , , stLinkCriteria)

Exit_btnAdjustments_Click:
		Exit Sub

Err_btnAdjustments_Click:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_btnAdjustments_Click
	End Sub

	Private Sub CustomerID_DblClick(ByVal Cancel As Integer)
		On Error GoTo Err_CustomerID_DblClick

		Dim stDocName As String
		Dim stLinkCriteria As String

		stDocName = "frmCustomers"

		'  stLinkCriteria = "[CustomerID=" & Me.CustomerID
		'  DoCmd.OpenForm(stDocName, , , stLinkCriteria)

Exit_CustomerID_DblClick:
		Exit Sub

Err_CustomerID_DblClick:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_CustomerID_DblClick

	End Sub

	Private Sub Form_AfterUpdate()
		'*** Logging
		'  Dim rs As Recordset
		'  rs = Me.RecordsetClone
		'  Dim f As Field

		'        For Each f In rs.Fields
		'        cLog.UpdateField(f)
		'        Next

		'*** End Logging
	End Sub

	Private Sub Form_Current()

		''*** Logging
		'Dim rs As Recordset
		'Dim f As Field

		'rs = Me.RecordsetClone

		'cLog.ClearItems()

		'For Each f In rs.Fields
		'    cLog.AddField(f, CStr(Nz(Me.QuoteID)))
		'Next
		''*** End Logging

		''*** Check whether the Phases should be available to the user
		'sCheckPhaseComplete()

		'DoCmd.DoMenuItem(acFormBar, acRecordsMenu, 5, , acMenuVer70)

		'lstRequestedUpgrades.RowSource = "SELECT tblRequestedUpgrades.RequestedUpgradeID, " &
		'        "tblRequestedUpgrades.UpgradeCategory as [Category, " &
		'        "tblRequestedUpgrades.UpgradeClass as [Upgrade Level, " &
		'        "tblRequestedUpgrades.UpgradeDescription as Description, " &
		'        "tblRequestedUpgrades.StyleDescription as Style, " &
		'        "tblRequestedUpgrades.CustomerPrice + tblRequestedUpgrades.Adjustments AS Cost " &
		'        "FROM tblRequestedUpgrades " &
		'        "WHERE (tblRequestedUpgrades.RoomDescription)= '" & lstRooms.Column(2) & "'" &
		'        "and (tblRequestedUpgrades.BuildingPhase) = " & Me.BuildingPhaseOption & " " &
		'        "and (tblRequestedUpgrades.quoteid) = " & Me.QuoteID & " order by tblRequestedUpgrades.UpgradeCategory"



		'lstRequestedUpgrades.Requery()
		'Me.SetFocus()
	End Sub



	Private Sub sCheckPhaseComplete()
		'*** Check the Quote Status

		Dim d As DropDownList
		If fvQuote Is Nothing Then Exit Sub
		'	If odsQuotes.s Then Exit Sub
		If fvQuote.Controls.Count = 0 Then Exit Sub

		d = fvQuote.FindControl("cmbQuoteStatus")
		If d Is Nothing Then Exit Sub
		Dim dP1 As DropDownList = fvQuote.FindControl("cmbPhase1Status")
		Dim dP2 As DropDownList = fvQuote.FindControl("cmbPhase2Status")
		Dim tP1 As TextBox = fvQuote.FindControl("txtPhase1CompleteDate")
		Dim tP2 As TextBox = fvQuote.FindControl("txtPhase2CompleteDate")
		Dim tt1 As TextBox = fvQuote.FindControl("txtPhase1TargetDate")
		Dim tt2 As TextBox = fvQuote.FindControl("txtPhase2TargetDate")

		'*** Quote is Complete - Disable all Selection Boxes, Set Focus on Customer Receipt
		If d.SelectedValue = "Completed" Then
			cmdCustomerReceipt.Focus()
			lstRooms.Enabled = False
			lstCategories.Enabled = False
			'lstLevels.Enabled = False
			'lstStyle.Enabled = False
			'lstRequestedUpgrades.Enabled = False ' This was commented
			'*** Disable Quote
			fvQuote.Enabled = False

			'Me.cmbphase1status.Enabled = False
			'Me.txtPhase1TargetDate.Enabled = False
			'Me.txtPhase1CompleteDate.Enabled = False
			'cmbQuoteStatus.Enabled = False
			'BuildingPhaseOption.Enabled = False
			'            Me.Phase2Status.Enabled = False
			'            Me.txtPhase2TargetDate.Enabled = False
			'            Me.txtPhase2CompleteDate.Enabled = False
			'            Me.CustomerID.Enabled = False
			'            Me.UnitID.Enabled = False
			'            Me.QuoteComments.Enabled = False


			If Not IsDate(tP1.Text) Or dP1.SelectedValue <> "Completed" Then
				SetQuoteStatus(Session("QuoteID"), "Phase1Status", "Completed", "Phase1CompleteDate", """" & Date.Today & """")
				'        Me.cmbphase1status = "Completed"
				'        Me.txtPhase1CompleteDate = Date
			End If
			If Not IsDate(tP2.Text) Or dP2.SelectedValue <> "Completed" Then
				SetQuoteStatus(Session("QuoteID"), "Phase2Status", "Completed", "Phase2CompleteDate", """" & Date.Today & """")
				'        Me.cmbphase1status = "Completed"
				'        Me.txtPhase1CompleteDate = Date
			End If
			cmdAutoPick.Enabled = False
		Else
			fvQuote.Enabled = True
			'            BuildingPhaseOption.Enabled = True
			'            '*** Whole thing not complete Check individual Phases


			If dP1.Text = "Completed" Then
				dP1.Enabled = False
				tP1.Enabled = False
				tt1.Enabled = False
				cmdAutoPick.Enabled = False

			Else
				cmdAutoPick.Enabled = True
				dP1.Enabled = True
				tP1.Enabled = True
				tt1.Enabled = True
			End If

			If dP2.Text = "Completed" Then
				dP2.Enabled = False
				tP2.Enabled = False
				tt2.Enabled = False
			Else
				dP2.Enabled = True
				tP2.Enabled = True
				tt2.Enabled = True
			End If


			If d.SelectedValue = "Completed" Then
				d.Enabled = False
			Else
				d.Enabled = True
			End If

		End If

		Select Case rblPhase.SelectedValue
			Case 1
				If dP1.SelectedValue = "Completed" Then
					lstRooms.Enabled = False
				Else
					lstRooms.Enabled = True
				End If
			Case 2
				If dP2.SelectedValue = "Completed" Then
					lstRooms.Enabled = False
				Else
					lstRooms.Enabled = True
				End If
		End Select
	End Sub


    Private Sub txtPhase2TargetDate_DblClick(ByVal Cancel As Integer)
		'        If IsNull(Me.ActiveControl) Then
		'    Me.ActiveControl = Date + 14
		'        Else
		'            Me.ActiveControl = Me.ActiveControl - 1
		'        End If

Exit_txtPhase2TargetDate_DblClick:
		Exit Sub

Err_txtPhase2TargetDate_DblClick:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_txtPhase2TargetDate_DblClick
	End Sub

	Private Sub cmbQuoteStatus_AfterUpdate()
		'***        sCheckPhaseComplete()
	End Sub

	Private Sub cmbQuoteStatus_BeforeUpdate(ByVal Cancel As Integer)
		On Error GoTo cmbQuoteStatus_Error
		'        If UCase(cmbQuoteStatus.value) = "COMPLETED" Or UCase(cmbQuoteStatus.value) = "CLOSED" Then
		'            Cancel = fRequired(Me.QuoteID, 0)
		'            cmbQuoteStatus.Undo()
		'        End If
cmbQuoteStatus_Exit:
		Exit Sub
cmbQuoteStatus_Error:
		ShowErrors(Err.Number, Err.Description)
		Resume cmbQuoteStatus_Exit
	End Sub

	Private Sub UnitID_DblClick(ByVal Cancel As Integer)
		On Error GoTo Err_UnitID_DblClick
		Dim stDocName As String
		Dim stLinkCriteria As String

		stDocName = "frmUnits"
		'        stLinkCriteria = "[UnitID=" & Me.UnitID
		'        '    DoCmd.OpenForm stDocName, , , stLinkCriteria

Exit_UnitID_DblClick:
		Exit Sub

Err_UnitID_DblClick:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_UnitID_DblClick

	End Sub

	Private Sub btnCustomerReceipt_Click()
		On Error GoTo Err_btnCustomerReceipt_Click
		Dim liPhase As Integer
		Dim lsPhase As String
		Dim lsMsg As String
		'        If BuildingPhaseOption.Enabled Then
		'            liPhase = BuildingPhaseOption.value
		'        Else
		'            Do While liPhase < 1 Or liPhase >= 3
		'                '           Do While Not IsNumeric(lsPhase)
		'                lsPhase = InputBox(lsMsg & "Enter Phase (1 for Structural, 2 for Finishes)", "")
		'                lsMsg = "Sorry, That does not seem to be a valid Phase number. " & Chr(13) & Chr(10)
		'                '           Loop
		'                liPhase = Int(Val(lsPhase))
		'            Loop
		'        End If
		'        If Me.BuildingPhaseOption.value <> liPhase Then Me.BuildingPhaseOption.value = liPhase
		'        Dim stDocName As String
		'        stDocName = "rptCustomerReceiptLegal" & IIf(liPhase = 1, "Structural", "Finishes")

		'        sMakeRider()
		'        DoCmd.OpenReport(stDocName, acPreview, , "UnitID = " & Me.UnitID)

		'        stDocName = "rptCustomerReceipt"
		'        DoCmd.OpenReport(stDocName, acPreview, , "UnitID = " & Me.UnitID)

		'        '*** Now handle all Additional Documents to Print
		'        Dim rs As ADODB.Recordset
		'        rs = New ADODB.Recordset
		'        rs.Open("Select * from qryGetDocuments where QuoteID = " & _
		'        Me.QuoteID.value & " And buildingphase = " & Me.BuildingPhaseOption.value, CurrentProject.Connection)
		'        Dim lsFile As String
		'        Dim liRetVal As Integer

		'        Do While Not rs.EOF
		'            'lsFile = rs("AdditionalFileToPrint1").value
		'            lsFile = Nz(rs("AdditionalFileToPrint1").value)
		'            If lsFile <> "" Then
		'                'liRetVal = response.write("Print " & lsFile, vbYesNo)
		'                liRetVal = response.write("Print " & lsFile, vbYesNoCancel)
		'                Select Case liRetVal
		'                    Case vbCancel
		'                        Exit Do
		'                    Case vbYes
		'                        PrintSpool(lsFile)
		'                End Select
		'            End If
		'            lsFile = Nz(rs("AdditionalFileToPrint2").value)
		'            If lsFile <> "" Then
		'                liRetVal = response.write("Print " & lsFile, vbYesNoCancel)
		'                Select Case liRetVal
		'                    Case vbCancel
		'                        Exit Do
		'                    Case vbYes
		'                        PrintSpool(lsFile)
		'                End Select
		'            End If
		'            rs.MoveNext()
		'        Loop
		'        rs.Close()

Exit_btnCustomerReceipt_Click:
		Exit Sub

Err_btnCustomerReceipt_Click:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_btnCustomerReceipt_Click

	End Sub





	Private Sub SearchQuotes_Click()
		On Error GoTo Err_SearchQuotes_Click

		Dim stDocName As String
		Dim stLinkCriteria As String

		stDocName = "frmSearchForCustomer"
		'***        DoCmd.OpenForm(stDocName, , , stLinkCriteria)

Exit_SearchQuotes_Click:
		Exit Sub

Err_SearchQuotes_Click:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_SearchQuotes_Click
	End Sub

	Private Sub btnCommunications_Click()
		On Error GoTo Err_btnCommunications_Click
		Dim stDocName As String
		Dim stLinkCriteria As String

		stDocName = "frmCommunications"

		'        '    stLinkCriteria = "[CustomerID=" & Me.CustomerID & " and [BuildingPhase=" & Me.BuildingPhaseOption
		'        stLinkCriteria = "[CustomerID=" & Me.CustomerID

		'        DoCmd.OpenForm(stDocName, , , stLinkCriteria)

Exit_btnCommunications_Click:
		Exit Sub

Err_btnCommunications_Click:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_btnCommunications_Click

	End Sub

	Private Sub btnPayments_Click()
		On Error GoTo Err_btnPayments_Click

		Dim stDocName As String
		Dim stLinkCriteria As String

		stDocName = "frmPayments"
		'        '    stLinkCriteria = "[QuoteID=" & Me.QuoteID & " and [BuildingPhase=" & Me.BuildingPhaseOption

		'        stLinkCriteria = "[QuoteID=" & Me.QuoteID
		'        DoCmd.OpenForm(stDocName, , , stLinkCriteria)

Exit_btnPayments_Click:
		Exit Sub

Err_btnPayments_Click:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_btnPayments_Click

	End Sub









    '*** Start Good Stuff
    Private Sub DeleteRequestedItem(ByVal llID As Long)
		On Error GoTo Requested_Upgrades_DblClick_error

		Session("llUpgradeOptionID") = llID

		Dim Del As Integer

		Del = 1

		'	Del = '("Would you like to Delete this selection? (Click No to edit)", vbYesNo, "Delete Selection")

		If Del = 6 Then
			'delete code
			Me.odsRequestedUpgrades.Delete()

		Else
			'stDocName = "frmEditNewRequestedUpgrades"
			'stLinkCriteria = "[QuoteID=" & Me.QuoteID & " and [RequestedUpgradeID=" & Me.lstRequestedUpgrades.Column(0)
			'DoCmd.OpenForm(stDocName, , , stLinkCriteria)
		End If

Exit_Requested_Upgrades_DblClick:
		Exit Sub

Requested_Upgrades_DblClick_error:
		Response.Write("Error Deleting or Editing entry" & Err.Description)
		Resume Exit_Requested_Upgrades_DblClick

	End Sub


    Private Sub InsertRequestedItem(ByVal lsUpdgradeOptionID As String)
        Dim Style As String
        Dim ErrorText As String
        Dim lsQuoteID As String
        Dim lsRoomDesc As String
        Dim lsUpgradeDesc As String
        Dim lsUpgradeCategory As String
        Dim lsUpgradeClass As String
        Dim lsStyle As String
        Dim lsMsg As String

        lsQuoteID = Session("QuoteID")

        On Error GoTo WriteRequestStyleError

        Dim cn As clsSelectDataLoader    '   lsStyle = Style
        Dim lbInc As Boolean
        Dim lsInc As String = ""
        cn = Session("cn")
        If cn Is Nothing Then
            Session("cn") = New clsSelectDataLoader
            cn = Session("cn")
        End If


        Dim ds As New DataSet
        ds = cn.fcheckforIncompatibilities(lsQuoteID, lsUpdgradeOptionID)
        Dim lstest As String = ds.Tables(0).Rows(0).ItemArray(0).ToString

        If lstest = "" Then
            lbInc = True
        Else
            lbInc = False
            '*** Incompatible Canceled
            ' Debug.Print("Incompatible: Canceled")
            Dim li As Integer
            Dim dt As DataTable
            dt = ds.Tables(0)
            For li = 0 To dt.Rows.Count - 1
                lsInc = lsInc & ConvertDataTableToHTML(dt) & "<br/>"

            Next
            litMsg.Text = "An Upgrade has already been chosen for this category" & ds.Tables(0).ToString
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "myScript" & Guid.NewGuid.ToString, "ShowMessage('" & lsInc & "','Error','');", True)

            Exit Sub
        End If

        Session("llUpgradeOptionID") = lsUpdgradeOptionID
        If Session("RequestedUpgradeCount") = 0 Then
            odsRequestedUpgrades.InsertParameters("lsOptionID").DefaultValue = lsUpdgradeOptionID
            odsRequestedUpgrades.InsertParameters("lsProjectID").DefaultValue = Session("Project")
            odsRequestedUpgrades.Insert()

        Else

        End If
        litMsg.Text = "An Upgrade has already been chosen for this category"
        'lstRequestedUpgrades.DataBind()

Exit_Style_DblClick:
        Exit Sub

WriteRequestStyleError:
        If Err.Number = -2147467259 Then
            Response.Write("You can not choose this Category, because a choice has already been made")
        Else
            ErrorText = Err.Description
            Response.Write("You may not have the appropriate permissions to make selections" & Chr(13) & Chr(10) & Err.Description)
            ' Debug.Print(Err.Number)
        End If

        Resume Exit_Style_DblClick

    End Sub
    Protected Sub SetQuoteStatus(ByVal llQuoteID As Long, ByVal lsStatusType As String, ByVal lsVal As String, ByVal lsDateField As String, ByVal lsDate As String)
		'Dim Quote As New 
		'Quote.UpdateQuoteStatus(llQuoteID, lsStatusType, lsVal, lsDateField, lsDate)
	End Sub

	Protected Sub cmbPhase1Status_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSelectedUpgrade.DataBound
		Dim d As ListView
		Dim t As TextBox
		Dim theDate As Date = Date.Today
		Dim lbCancel As Boolean

		d = sender
		t = d.FindControl("txtPhase1CompleteDate")
		If t Is Nothing Then Exit Sub
		If IsDate(t.Text) Then
			theDate = t.Text
		End If

		On Error GoTo ph1Status_Error
		If UCase(sender.SelectedValue) = "COMPLETED" Or UCase(sender.SelectedValue) = "CLOSED" Then
			'*** Check for Required Items
			'MOVE THIS TO THE DATA LOADER
			'lbCancel = fRequired(Me.QuoteID, 1)
			If lbCancel = False Then
				SetQuoteStatus(Session("QuoteID"), "Phase1Status", d.SelectedValue, "Phase1CompleteDate", """" & theDate.Date & """")
			End If
		Else
			SetQuoteStatus(Session("QuoteID"), "Phase1Status", d.SelectedValue, "Phase1CompleteDate", "Null")


		End If
		'odsQuotes.DataBind()
		Me.DataBind()

ph1Status_Exit:
		Exit Sub
ph1Status_Error:
		ShowErrors(Err.Number, Err.Description)
		Resume ph1Status_Exit
	End Sub

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '     Exit Sub
        If Not Page.IsPostBack Then
            Dim li As ListItem
            cboAssignedTo.Items.Clear()
            cboAssignedTo.Items.Add("-- Assign Sales Consultant --")
            Dim c As New clsSelectDataLoader
            Dim ds As DataSet = c.LoadAppUsers
			For Each dt As DataTable In ds.Tables
				For Each dr As DataRow In dt.Rows
					li = New ListItem
					li.Value = dr("ID").ToString
					li.Text = dr("userFriendlyName") & " - " & dr("email")
					cboAssignedTo.Items.Add(li)
				Next
			Next
            cboAssignedCustomer.Items.Clear()
            cboAssignedCustomer.Items.Add("-- Assign Customer  --")

            ds = c.LoadCustomers(Session("NodeID"), "", True, "", "f.name", "")
            For Each dt As DataTable In ds.Tables
                For Each dr As DataRow In dt.Rows
                    li = New ListItem
                    li.Value = dr("ID").ToString
                    li.Text = dr("Name") & " - " & dr("CustomerEmail")
                    cboAssignedCustomer.Items.Add(li)
                Next
            Next

            ds = c.LoadQuotes(Session("NodeID"), "", True, "", 4, " updateDate DESC", "")
            rptRecentQuotes.DataSource = ds
            rptRecentQuotes.DataBind()
        End If
        loadPage()


    End Sub
	Function getQuoteID() As String
		Return String.Format("QuoteID={0}", Eval("QuoteID"))
	End Function
    Private Sub loadPage()
        '   Exit Sub
        '*** Make Sure we have the propert Quote ID
        '*** Is there an ID in the URL? IF so, Use that
        Dim lsQuoteID As String = Request.QueryString("QuoteID")
        '*** If So, Set the Session Variable 'Might want to not use Session, but . . . 
        Dim dc As New GlobalFunctionsDC
        If dc.isGUIDString(lsQuoteID) Then Session("QuoteID") = lsQuoteID
        '*** if Not, check the page.  See if we have been working on a quote already
        If Not lblQuoteID.Text Is Nothing Then
            If lblQuoteID.Text.Length = 36 Then
                lsQuoteID = lblQuoteID.Text
            Else
                lsQuoteID = Request.QueryString("QuoteID")
            End If
        End If
        '*** Finally, Try the Session Variable
        If lsQuoteID Is Nothing Then lsQuoteID = Session("QuoteID")

        If txtSelectedItemID Is Nothing Then
            Exit Sub
        End If
        txtSelectedItemID.Attributes.Add("readonly", "readonly")
        txtItemText.Attributes.Add("readonly", "readonly")
        txtItemPrice.Attributes.Add("readonly", "readonly")

        Dim q As New clsQuote(lsQuoteID)
        If q.ID Is Nothing Then
            'Exit Sub '*** Took this out to hide the Quote Panel 2023-01
        End If
        Session("QuoteString") = Replace(q.ToHTML, """", "'")
        Session("UnitType") = q.UnitTypeName
        Session("UnitTypeID") = q.UnitTypeID
        txtUnitTypeID.Text = q.UnitTypeID
        Dim l As Literal = Master.FindControl("LitPageInfo")
        If Not l Is Nothing And Not Session("QuoteID") Is Nothing Then l.Text = "QuoteID: " & Session("QuoteID")
        litID.Text = Session("QuoteID")
        If Session("NodeID") Is Nothing Then Session("NodeID") = 0
        If Session("Project") Is Nothing Then
            pnlButtons.Visible = False
        Else
            pnlButtons.Visible = True
        End If
        If Session("Project") Is Nothing Or Session("QuoteID") Is Nothing Then
            pnlQuote.Visible = False
            '    Me.ddmActions.Visible = False
            Exit Sub
        Else
            pnlQuote.Visible = True
            Dim lsPhase As String
            lsPhase = rblPhase.SelectedValue
            If lsPhase = "" Then
                Try
                    lsPhase = rblPhase.Items(0).Value
                Catch exc As Exception
                    Exit Sub '**** PUT IN SOME CODE TO EXPLAIN
                End Try

            End If
            '    Me.ddmActions.Visible = True
            Dim hl As LinkButton
            hl = pnlQuote.FindControl("cmdStandardReport")
            If Not hl Is Nothing Then
                hl.Attributes.Add("OnClick", "javascript:w= window.open('frmReport.aspx?ReportName=rptStandardSelections&QuoteID=" & Session("QuoteID") & "&Phase=" & lsPhase & "','mywin','left=20,top=20,width=1000,height=1000,toolbar=0,resizable=0');")
            End If
            Dim b As LinkButton
            b = pnlQuote.FindControl("cmdCustomerReceipt")
            If Not b Is Nothing Then
                b.Attributes.Add("OnClick", "javascript:w= window.open('frmReport.aspx?ReportName=rptQuoteReceipt&QuoteID=" & Session("QuoteID") & "&Phase=" & lsPhase & "','mywin','left=20,top=20,width=1000,height=1000,toolbar=0,resizable=0');")
            End If
            b = pnlQuote.FindControl("cmdVendorInstallationReport")
            If Not b Is Nothing Then
                b.Attributes.Add("OnClick", "javascript:w= window.open('frmReport.aspx?ReportName=rptVendorInstall&QuoteID=" & Session("QuoteID") & "&Phase=" & lsPhase & "','mywin','left=20,top=20,width=1000,height=1000,toolbar=0,resizable=0');")
            End If
            'Session("QuoteID") = 1
        End If

        If rblPhase.SelectedIndex < 0 Then
            rblPhase.SelectedIndex = 0
        End If
        Me.fvQuote.ChangeMode(FormViewMode.Edit)

        'If Me.rblPhase.SelectedValue = 1 Then
        'Me.cmdAutoPick.Enabled = False
        'Me.cmdAddNewOption.Enabled = False
        'Else
        Me.cmdAutoPick.Enabled = True
        ''*** Set value to True to allow new finish selection on the fly.
        Me.cmdAddNewOption.Enabled = False
        'End If



        Try

            '*** If the Quote is complete, Dis-allow any editing
            If q.QuoteStatus = "Completed" Then
                lstRooms.Enabled = False
                lstCategories.Enabled = False
                '	lstLevels.Enabled = False
                'lstStyle.Enabled = False
                'lstRequestedUpgrades.Enabled = False
                pnlQuote.Enabled = False

                Me.cmdAddNewOption.Enabled = False
                Me.cmdAutoPick.Enabled = False
                Me.rblPhase.Enabled = False

            Else
                If q.Phase1Status Is Nothing Then
                Else

                    If q.Phase1Status = "Completed" Then
                        Me.rblPhase.SelectedValue = 2
                    End If

                    lstRooms.Enabled = True
                    'lstRequestedUpgrades.Enabled = True
                    pnlQuote.Enabled = True ' Quote Status

                End If

                '	sCheckPhaseComplete()
            End If
        Catch ex As Exception
            Dim msg As String = ex.Message
            Response.Write(ex.Message)
        End Try


    End Sub


    Protected Sub odsRequestedUpgrades_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) 'Handles odsRequestedUpgrades.Inserting
        '  dim lst as listviewdataitem = session("requesteditemid") ' lststyle.items(lststyle.selectedindex)
        ' dim findme as integer = lst.displayindex

        Dim ds As DataSet
		Dim dr As DataRow
		Dim ll As Long
		ll = Session("llupgradeoptionid")
		Dim dl As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		ds = dl.LoadRoomCategoryLevelStyles(0, "", 0, "", "", "", ll)
		Dim requesttypehold As String
		If Me.rblPhase.SelectedValue = "3" Then
			requesttypehold = "change"
		Else
			requesttypehold = "standard"
		End If

		For Each dr In ds.Tables(0).Rows
			Dim llquoteid As Long = Session("quoteid")
			Dim lsroomdesc As String = IIf(lstRooms.SelectedValue = "", "", lstRooms.SelectedValue)
			Dim lsphase As String = IIf(rblPhase.SelectedValue = "", "", rblPhase.SelectedValue)
			Dim llnodeid = Session("nodeid")
			Dim lsupgradedesc As String = dr("description")
			Dim lsupgradecategory As String = dr("upgradecategory")
			Dim lsupgradeclass = dr("upgradelevel")
			Dim lsstyle As String = dr("style")
			Dim lsprice As String = dr("customerprice")
			Dim llupgradeoptionid As Long = Session("llupgradeoptionid")




			e.InputParameters("llquoteid") = llquoteid
			e.InputParameters("lsroomdesc") = lsroomdesc
			e.InputParameters("lsphase") = IIf(rblPhase.SelectedValue = "", "", rblPhase.SelectedValue)
			e.InputParameters("llnodeid") = llnodeid
			e.InputParameters("lsupgradedesc") = lsupgradedesc
			e.InputParameters("lsupgradecategory") = lsupgradecategory
			e.InputParameters("lsupgradeclass") = lsupgradeclass
			e.InputParameters("lsstyle") = lsstyle
			e.InputParameters("lsprice") = lsprice
			e.InputParameters("lstypehold") = requesttypehold
			e.InputParameters("llupgradeoptionid") = llupgradeoptionid
		Next
	End Sub


	Protected Sub odsSelectedUpgrade_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) Handles odsSelectedUpgrade.Selected
		Dim t As DataSet = e.ReturnValue
		Session("RequestedUpgradeCount") = (t.Tables(0).Rows.Count)
		If Session("RequestedUpgradeCount") > 0 Then
			'lstStyle.Enabled = False
			pnlStyle.Enabled = False
		Else
			'lstStyle.Enabled = True
			pnlStyle.Enabled = True
		End If
        lstRooms.DataBind()
        lstRooms2.DataBind()
        lstCategories.DataBind()
    End Sub

	Protected Sub lstSelectedUpgrade_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) 'Handles lstSelectedUpgrade.ItemCommand
		If e.CommandName = "DeleteRequestedItem" Then
            DeleteRequestedItem(e.CommandArgument)
            lstRooms.DataBind()

            lstRooms2.DataBind()
            lstCategories.DataBind()
        End If
	End Sub

	Protected Sub odsRequestedUpgrades_Deleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) ' Handles odsRequestedUpgrades.Deleting
		e.InputParameters("RecordID") = Session("llUpgradeOptionID")
	End Sub
    Protected Sub lstRequestedUpgrades_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) 'Handles lstRequestedUpgrades.ItemCommand
        If e.CommandName = "DeleteRequestedItem" Then
            DeleteRequestedItem(e.CommandArgument)
        End If

    End Sub








    Public Sub ShowErrors(ByVal llError As Long, ByVal lsError As String)
		Response.Write(llError & ": " & lsError)
	End Sub



    Private Sub frmQuote_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
        '   Exit Sub
        sCheckPhaseComplete()
        If txtSelectedItemID Is Nothing Then Exit Sub
		If Session("NodeID") = 0 Then Exit Sub
        loadPage() 'Stop
		If Not Page.IsPostBack Then
			lblQuoteID.Text = Session("QuoteID")
		End If

        Dim q As New clsQuote(Session("QuoteID"))
        cboAssignedTo.SelectedValue = q.AssignedTo
        If Not IsNothing(q.AssignedTo) Then
            litAssignedToName.Text = "<div style='color:#990000;     font-weight: 700 !important;' > " & q.AssignedToName & "</div>"
        Else
            litAssignedToName.Text = "No Sales Professional Selected"
        End If
		txtPhaseID.Attributes.Add("ReadOnly", "ReadOnly")
        txtPhaseName.Attributes.Add("ReadOnly", "ReadOnly")

		ctrlCommunications.DataBind()
		CtrlAdjustments.DataBind()
		Me.ctrlPayments.DataBind()
        ctrlImagesDisplay.DataBind()
    End Sub



	Protected Sub lstStyle_ItemInserting(sender As Object, e As ListViewInsertEventArgs) 'Handles lstStyle.ItemInserting
		Stop
	End Sub

	Protected Sub rblPhase_Init(sender As Object, e As EventArgs) Handles rblPhase.Init
        ' Exit Sub
        sSetPhase()
    End Sub
	Protected Sub sSetPhase()
		Dim c As New clsPhases
		Dim cp As New clsPhases
		Dim cpc As New Collection
		Dim row, rowp As clsPhase
		Dim l As ListItem
		Dim lbDefault As Boolean
		Dim liSelectedIndex As Integer = 0
		Dim liCounter As Integer
		Try
			'*** load Quote Phases with Status
			For Each rowp In cp.GetRecords("SortOrder", "SortOrder", Session("QuoteID"))
				cpc.Add(rowp)
			Next

			rblPhase.Items.Clear()
			For Each row In c.GetRecords("Name", "1", Session("Project"))
                l = New ListItem
                l.Value = row.Code
                l.Text = " " & row.Name

                For Each rowp In cpc
					If rowp.Code = row.Code Then
						If Trim(rowp.PhaseStatus) = "Completed" Or Trim(rowp.PhaseStatus) = "Closed" Then
							l.Enabled = False
							'l.Attributes.Add("style", "btn-disabled")
							l.Attributes.Add("disabled", "disabled")
						Else
							'*** ITs a good match , Lets see if we set the deault Yet
							l.Enabled = True
							If lbDefault = False Then
								lbDefault = True
								l.Selected = True
								liSelectedIndex = liCounter

							End If
						End If
					End If
				Next
				liCounter = liCounter + 1
				rblPhase.Items.Add(l)

			Next
		Catch
			Response.Write(Err.Description)
		End Try
		rblPhase.SelectedIndex = liSelectedIndex
		Dim i As Integer
		Dim tcount As Integer = rblPhase.Items.Count
		If tcount > 0 Then
			For i = 0 To tcount - 1
				'If rblPhase.Items(i).Enabled Then Stop
			Next
		End If
	End Sub

	Protected Sub grdPhases_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles grdPhases.RowDataBound
		e.Row.CssClass = "clickable-row"
		e.Row.Cells(21).Visible = False
	End Sub

	Protected Sub cmdPhaseStatusSave_Click(sender As Object, e As EventArgs) Handles cmdPhaseStatusSave.Click
		Dim p As New clsPhase
		Dim sID, sStatus, sTarget, sComplete As String
        Dim t As TextBox = txtPhaseName
        Dim cn As New clsSelectDataLoader
        Dim ds As New DataSet
        Dim dt As New DataTable
        Dim dc As New DataColumn
		Dim lbRemoved As Boolean
		Dim lsMsg As String
        Dim liRecordCount As Integer

        '*** Set Values from the Phase Panel
        sID = txtPhaseID.Text
        sStatus = ddlPhaseStatus.SelectedValue
        sTarget = txtTargetDate.Text
        sComplete = txtCompleteDate.Text

        Dim lbHasCompDate As Boolean = IsDate(sComplete)
		If (sStatus = "Completed" And lbHasCompDate = False) Then sComplete = Now().ToShortDateString
        If sStatus = "Completed" Or sStatus = "Closed" Then
            '*** Check to Make sure there are no Required Selections unfullfilled in this phase
            ds = cn.LoadUnfullfilledRequiredItems(txtPhaseNum.Text, "ALL", litID.Text) 'rblPhase.SelectedValue, "ALL", litID.Text)

            '*** Cut the table down to just the appropriate columns that I want to convert to HTML to show if there are items
            For Each dt In ds.Tables
                liRecordCount = liRecordCount + dt.Rows.Count
                lbRemoved = True
                While lbRemoved = True
                    lbRemoved = False
                    For Each dc In dt.Columns
                        Select Case dc.ColumnName.ToUpper
                            Case "REQUIRED", "ID", "UNITTYPEID", "CATREQUIRED", "CATEGORYDETAILID", "THECOUNT"
                                lbRemoved = True
                                dt.Columns.Remove(dc)
                                Exit For
                        End Select
                    Next
                End While
            Next

            If liRecordCount > 0 Then
                '*** There are required picks that have not yet been made Let's list them and quit
                lsMsg = ConvertDataTableToHTML(ds.Tables(0))
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Required" & Guid.NewGuid.ToString, "ShowMessage('" & lsMsg & "','Warning','Required Selections have NOT been made!',);", True)
                Exit Sub
            End If
        End If
        If sID.Length > 10 And sStatus <> "" Then
			p.ID = sID
			p.PhaseStatus = sStatus
			If IsDate(sTarget) Then
				p.PhaseTargetDate = CDate(txtTargetDate.Text)
			End If

			If IsDate(sComplete) And sStatus = "Completed" Then
				p.PhaseCompleteDate = CDate(sComplete)
			End If
			p.Active = True
			p.Level = "Quote"
			p.Update()
			odsPhases.DataBind()
			grdPhases.DataBind()
			upPhase.DataBind()
			grdPhases.DataBind()
            CreateTask(lblQuoteID.Text, cboAssignedTo.SelectedValue, "frmQuote?ID=" & lblQuoteID.Text, " Phase Update for Phase: " & txtPhaseName.Text &
                " Status: " & ddlPhaseStatus.Text)
        Else
			lsMsg = ConvertDataTableToHTML(ds.Tables(0))
			ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Required" & Guid.NewGuid.ToString, "ShowMessage('" & lsMsg & "','Error','Data NOT Valid!',);", True)

		End If
	End Sub
    Function CreateTask(lsID As String, lsAssignedToID As String, lsURL As String _
                        , lsDescription As String) As Boolean

        If lsID.Length = 36 And lsAssignedToID.Length = 36 Then
            Dim q As New clsQuote(lsID)
            Dim AssignTask As New clstask
            AssignTask.ProjectID = q.ProjectID
            AssignTask.NodeID = Session("NodeID")
            AssignTask.ObjectID = lblQuoteID.Text
            AssignTask.Code = q.Code
            AssignTask.Description = q.UnitCode + ": " + q.UnitName & lsDescription
            AssignTask.Name = "Quote Phase Information has been Updated:  " & txtPhaseName.Text
            AssignTask.AssignedToEmail = q.AssignedToEmail
            AssignTask.AssignedTo = q.AssignedTo
            AssignTask.AppUrl = lsURL
            AssignTask.BaseURL = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd("/") + "/"
            AssignTask.Insert()
        End If
        Return True
    End Function
    Protected Sub upPhase_DataBinding(sender As Object, e As EventArgs) Handles upPhase.DataBinding
		'Stop
	End Sub

	Protected Sub odsSelectedUpgrade_Inserting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsSelectedUpgrade.Inserting
		Dim a As Integer = 0
	End Sub

	Protected Sub lstStyle_ItemCommand1(sender As Object, e As ListViewCommandEventArgs) Handles lstStyle.ItemCommand
        Exit Sub
        'Stop
        'System.Threading.Thread.Sleep(1000)
        Dim p As Panel = pnlSelectedUpgrades
		'Dim lbl As Label = p.FindControl("lblTIme")
		'lbl.Text = Now.ToLongDateString
		'	Exit Sub
		Dim l As ListView = sender
		Dim s As String = l.DataKeys(e.Item.DataItemIndex).Value.ToString
		Dim c As New clsSelectDataLoader

		c.InsertRequestedUpgrades(Session("NodeID"), Session("Project"), Session("QuoteID"), s, 1)
		'System.Threading.Thread.Sleep(1000)
		updPnlSelectedUpgrades.DataBind()
	End Sub

	Protected Sub lstSelectedUpgrade_ItemCommand1(sender As Object, e As ListViewCommandEventArgs) Handles lstSelectedUpgrade.ItemCommand
		Dim l As ListView = sender
		Dim s As String = l.DataKeys(e.Item.DataItemIndex).Value.ToString
		Dim c As New clsSelectDataLoader
		c.DeleteRequestedUpgrades(s)
		updPnlSelectedUpgrades.DataBind()
	End Sub

	Private Sub frmQuote_Init(sender As Object, e As EventArgs) Handles Me.Init
        '        Exit Sub
        Dim lsQuoteID As String = Request.QueryString("QuoteID")
        checkQuoteID()
		Dim dc As New GlobalFunctionsDC
		If dc.isGUIDString(lsQuoteID) Then Session("QuoteID") = lsQuoteID

	End Sub

	Protected Sub lstSelectedUpgrade_ItemDataBound(sender As Object, e As ListViewItemEventArgs) Handles lstSelectedUpgrade.ItemDataBound
		If 1 = 2 Then Stop
	End Sub

	Protected Sub lstSelectedUpgrade_DataBound(sender As Object, e As EventArgs)
		'Stop
		If lstSelectedUpgrade.Items.Count > 0 Then
			lstStyle.Enabled = False
		Else
			lstStyle.Enabled = True
		End If

	End Sub

	Protected Sub cmdMissingSelections_Click(sender As Object, e As EventArgs) Handles cmdMissingSelections.Click
		On Error GoTo Err_cmdMissingSelections_Click
		Dim lb As Boolean
		Dim c As New clsSelectDataLoader
		Dim f As New GlobalFunctionsDC
		Dim lsPhase As String = rblPhase.SelectedValue
		If lsPhase Is Nothing Then lsPhase = ""
		If f.isGUIDString(litID.Text) And lsPhase <> "" Then
			lb = c.fMissingSelections(Me.litID.Text, "Report", rblPhase.SelectedValue)
			Dim url As String = "frmReport.aspx?ReportName=rptMissing&QuoteID=" & Session("QuoteID") & "&Phase=" & lsPhase
			Dim s As String = "window.open('" & url + "', 'popup_window', 'width=300,height=100,left=100,top=100,resizable=yes');"
			ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
		Else
			'RaiseEvent "You need to select an quote first and then a phase
		End If
Exit_cmdMissingSelections_Click:
		Exit Sub

Err_cmdMissingSelections_Click:
		ShowErrors(Err.Number, Err.Description)
		Resume Exit_cmdMissingSelections_Click
	End Sub

	Protected Sub cmdAutoPick_Click1(sender As Object, e As EventArgs) Handles cmdAutoPick.Click
        On Error GoTo Err_cmdAutoPick_Click
		Dim dt As New DataTable
		Dim cn As New clsSelectDataLoader
		Dim lsMessage As String = ""
		Dim lbSuccess As Boolean = cn.InsertAutoPick(Session("NodeID"), Session("QuoteID"), Session("UnitTypeID"), rblPhase.SelectedValue, dt, lsMessage)
		'	If dt.Rows.Count > 0 Then
		'*** There are required picks that have not yet been made Let's list them and quit
		Dim lsMsg As String = ConvertDataTableToHTML(dt)
			ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Required" & Guid.NewGuid.ToString, "ShowMessage('" & lsMsg & "','','" & lsMessage & "!',);", True)

		'	Exit Sub
		'	End If
Exit_cmdAutoPick_Click:
        Exit Sub

Err_cmdAutoPick_Click:
        ShowErrors(Err.Number, Err.Description)
        Resume Exit_cmdAutoPick_Click
    End Sub

    '	Protected Sub btnAutoPop_Click(sender As Object, e As EventArgs) Handles btnAutoPop.Click
    '    On Error GoTo Err_AutoPop_Click

    '        Dim Del As Object

    '        Del = response.write("         Are you sure?", vbYesNo, "Auto-populate")
    '        If Del = 6 Then
    '            'delete code
    '            Dim stDocName As String
    '            DoCmd.SetWarnings(False)
    '            stDocName = "qryBuildStandardOptionsLevel"
    '            DoCmd.OpenQuery(stDocName, acNormal, acEdit)
    '            stDocName = "qryBuildStandardOptionsLeveltwo"
    '            DoCmd.OpenQuery(stDocName, acNormal, acEdit)
    '            DoCmd.SetWarnings(True)

    '            DoCmd.DoMenuItem(acFormBar, acRecordsMenu, 5, , acMenuVer70)

    '        End If

    'Exit_AutoPop_Click:
    '    Exit Sub

    'Err_AutoPop_Click:
    '        response.write(Err.Description)
    '    Resume Exit_AutoPop_Click
    '    End Sub

    Protected Sub cmdAddNewOption_Click(sender As Object, e As EventArgs) Handles cmdAddNewOption.Click

	End Sub

	Protected Sub cmdSelectedItemSave_Click(sender As Object, e As EventArgs) Handles cmdSelectedItemSave.Click
		Dim p As New clsSelectedItem

		Dim t As TextBox = txtPhaseName
		p.Active = True
		p.AdditionalFileToPrint1 = ""
		p.AdditionalFileToPrint2 = ""
		If IsNumeric(txtAdjustment.Text) Then
			p.Adjustments = txtAdjustment.Text
		End If
		p.BuildingPhase = rblPhase.SelectedValue
		p.Code = ""
        p.Comments = txtComment.Text ' "txtCommments.text"
        p.CustomerPrice = txtItemPrice.Text
        p.ID = txtSelectedItemID.Text
        p.LeadVendor = ""
		p.Name = ""
		p.ObjectID = ""
		p.OptionID = ""
		p.PricingRevNumber = 0
		p.ProjectID = Session("Project")
		p.Quantity = txtQuantity.Text
		p.RequestedUpgradeFlexText1 = ""
		p.RequestedUpgradeFlexText2 = ""
		p.RequestedUpgradeFlexText3 = ""
		p.RequestedUpgradeFlexText4 = ""
		p.RequestType = "Custom"
		p.RoomDescription = lstRooms.SelectedValue
		p.Standard = 1
		p.StyleDescription = lstStyle.SelectedValue
		p.UpgradeCategory = lstCategories.SelectedValue
        'p.UpgradeClass = lst
        'p.UpgradeDescription =

        If p.ID = "" Then
            p.Insert()
        Else
            p.Update()
        End If
        lstSelectedUpgrade.DataBind()

        '			odsPhases.DataBind()
        '			grdPhases.DataBind()
        '			upPhase.DataBind()
        '			grdPhases.DataBind()

        'Response.Write("Data Not Valid")

    End Sub

    Protected Sub InsertItem_Command(sender As Object, e As CommandEventArgs)
        Dim lnk As LinkButton = CType(sender, LinkButton)
        Select Case lnk.CommandName
            Case "InsertRequestedItem"
                InsertRequestedItem(lnk.CommandArgument)
                lstStyle.DataBind()
                lstSelectedUpgrade.DataBind()

                ' Case "delete"
                '    DeleteItem(lnk.CommandArgument)
        End Select
    End Sub

    Protected Sub lstRooms_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRooms.SelectedIndexChanged

    End Sub
	Public Function ConvertDataTableToHTML(dt As DataTable) As String
		Dim i, j As Integer
		'  Dim lsColName As String
		Dim html As String = "<table>"
		'//add header row

		'       For i = 0 To dt.Columns.Count - 1
		'lsColName = dt.Columns(i).ColumnName

		'html = html & "<th style='display:None;'>"
		'html = html & dt.Columns(i).ColumnName

		'html = html & "</th>"
		'Next
		'//add rows

		For i = 0 To dt.Rows.Count - 1
			html = html & "<tr>"
			For j = 0 To dt.Columns.Count - 1
				html = html & "<td>" + dt.Rows(i)(j).ToString() + "</td>"
			Next
			html = html & "</tr>"
		Next

		html = html & "</table>"
		Return html
	End Function


End Class
