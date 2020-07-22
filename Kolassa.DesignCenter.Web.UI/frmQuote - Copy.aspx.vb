Imports System.Data
Public Class frmQuote2
    Inherits System.Web.UI.Page
    Private Sub l()

    End Sub
    '*******************************************************
    '*** I think this is all accounted for
    '*** Just keeping it around for reference
    '*****************************************************
    '    Dim mbResize As Boolean
    '    '### Dim cLog As New clsEventLog

    Private Sub lstClasses_AfterUpdate()

        '        lstUpgradesAvail.RowSource = "SELECT tblUpgradeOptions.UnitType, tblUpgradeOptions.UpgradeCategory, " & _
        '        "tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeLevel " & _
        '        "FROM tblUpgradeOptions " & _
        '        "GROUP BY tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, " & _
        '        "tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.UpgradeLevel " & _
        '        "HAVING (((tblUpgradeOptions.[UnitType)='" & [Forms.frmQuote.UnitID.Column(3) & "') AND " & _
        '        "((tblUpgradeOptions.[Location)='" & [Forms.frmQuote.lstRooms.Column(2) & "') and " & _
        '        "((tblUpgradeOptions.UpgradeCategory) = '" & [Forms.frmQuote.lstClasses.Column(1) & "'))" & _
        '        "ORDER BY tblUpgradeOptions.UpgradeLevel;"

        '        'MsgBox (lstUpgradesAvail.RowSource)

        '        lstUpgradesAvail.Requery()

        '        lstStyle.RowSource = ""
        '        lstStyle.Requery()

        '        Me.lstUpgradesAvail.Visible = True
        '        Me.lstUpgradesAvail.Enabled = True
        '        Me.lstStyle.Visible = True
        '        Me.lstStyle.Enabled = False

    End Sub

    Private Sub lstClasses_Click()
        '        sDisplayListbox(lstClasses)
    End Sub

    Private Sub lstRequestedUpgrades_Click()
        '        sDisplayListbox(lstRequestedUpgrades)
    End Sub


    Private Sub BuildingPhaseOption_AfterUpdate()
        '        '
        '        Dim lsRoom As String = lstRooms.Text
        '        Dim lsQuoteID As String = txtQuoteID.Text
        '        lstRequestedUpgrades.RowSource = "SELECT tblRequestedUpgrades.RequestedUpgradeID, " & _
        '                "tblRequestedUpgrades.UpgradeCategory as Category, " & _
        '                "tblRequestedUpgrades.UpgradeClass as Upgrade Level, " & _
        '                "tblRequestedUpgrades.UpgradeDescription as Description, " & _
        '                "tblRequestedUpgrades.StyleDescription as Style, " & _
        '                "tblRequestedUpgrades.CustomerPrice + tblRequestedUpgrades.Adjustments AS [Cost " & _
        '                "FROM tblRequestedUpgrades " & _
        '                "WHERE (tblRequestedUpgrades.RoomDescription)= '" & lsRoom & "'" & _
        '                "and (tblRequestedUpgrades.BuildingPhase) = " & rblPhase.SelectedValue & " " & _
        '                "and (tblRequestedUpgrades.quoteid) = " & lsQuoteID & " order by tblRequestedUpgrades.UpgradeCategory"

        '        If Me.rblPhase.SelectedValue = "Structural" Then

        '            lstClasses.RowSource = ""
        '            lstClasses.Requery()
        '            Me.lstClasses.Enabled = False
        '            lstUpgradesAvail.RowSource = ""
        '            lstUpgradesAvail.Requery()
        '            Me.lstUpgradesAvail.Enabled = False
        '            Me.lstUpgradesAvail.Visible = True

        '            lstStyle.RowSource = ""
        '            lstStyle.Requery()
        '            Me.lstStyle.Enabled = False
        '            Me.lstStyle.Visible = True
        '        Else


        '            lstClasses.RowSource = ""
        '            lstClasses.Requery()
        '            Me.lstClasses.Enabled = False
        '            Me.lstClasses.Visible = True
        '            '
        '            lstUpgradesAvail.RowSource = ""
        '            lstUpgradesAvail.Requery()
        '            Me.lstUpgradesAvail.Enabled = False
        '            Me.lstUpgradesAvail.Visible = True
        '            '
        '            lstStyle.RowSource = ""
        '            lstStyle.Requery()
        '            Me.lstStyle.Enabled = False
        '            Me.lstStyle.Visible = True
        '            '
        '        End If
        '        '
        '        lstRequestedUpgrades.Requery()
        '        sCheckPhaseComplete()
    End Sub

    Private Sub lstRooms_AfterUpdate()

        '        On Error GoTo lstRooms_AfterUpdate_error
        '        '
        '        lstRequestedUpgrades.RowSource = "SELECT tblRequestedUpgrades.RequestedUpgradeID, " & _
        '                "tblRequestedUpgrades.UpgradeCategory as Category, " & _
        '                "tblRequestedUpgrades.UpgradeClass as [Upgrade Level, " & _
        '                "tblRequestedUpgrades.UpgradeDescription as Description, " & _
        '                "tblRequestedUpgrades.StyleDescription as Style, " & _
        '                "tblRequestedUpgrades.CustomerPrice + tblRequestedUpgrades.Adjustments AS Cost " & _
        '                "FROM tblRequestedUpgrades " & _
        '                "WHERE (tblRequestedUpgrades.RoomDescription)= '" & lstRooms.Column(2) & "'" & _
        '                "and (tblRequestedUpgrades.BuildingPhase) = " & Me.BuildingPhaseOption & " " & _
        '                "and (tblRequestedUpgrades.quoteid) = " & Me.QuoteID & " order by tblRequestedUpgrades.UpgradeCategory"

        '        lstRequestedUpgrades.Requery()

        '        lstClasses.RowSource = "SELECT tblUpgradeOptions.UnitType, tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.Location " & _
        '        "FROM tblUpgradeOptions " & _
        '        "WHERE (((tblUpgradeOptions.Active) = Yes)) " & _
        '        "GROUP BY tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.BuildingPhase " & _
        '        "HAVING (((tblUpgradeOptions.[UnitType)='" & [Forms.frmQuote.UnitID.Column(3) & "') AND " & _
        '        "((tblUpgradeOptions.[BuildingPhase)=" & [Forms.frmQuote.BuildingPhaseOption & ") AND " & _
        '        "((tblUpgradeOptions.Location)='" & [Forms.frmQuote.lstRooms.Column(2) & "'));"

        '        'MsgBox (lstClasses.RowSource)

        '        Me.lstClasses.Enabled = True
        '        lstClasses.Requery()

        '        lstUpgradesAvail.RowSource = ""
        '        lstUpgradesAvail.Requery()
        '        Me.lstUpgradesAvail.Enabled = False
        '        '
        '        lstStyle.RowSource = ""
        '        lstStyle.Requery()
        '        Me.lstStyle.Enabled = False
        '        '


        '        'DoCmd.DoMenuItem acFormBar, acRecordsMenu, 5, , acMenuVer70

        'exit_lstRooms_AfterUpdate:
        '        Exit Sub

        'lstRooms_AfterUpdate_error:
        '        MsgBox("error updating Requested Upgrades")
        '        MsgBox(Err.Description)

        '        Resume exit_lstRooms_AfterUpdate

    End Sub

    Private Sub lstRooms_Click()
        '        sDisplayListbox(lstRooms)
    End Sub

    Private Sub lstStyle_Click()
        '        sDisplayListbox(lstStyle)
    End Sub

    Private Sub lstUpgradesAvail_AfterUpdate()

        '        If Me.BuildingPhaseOption = 1 Then
        '            Me.lstStyle.Enabled = True

        '            lstStyle.RowSource = "SELECT tblUpgradeOptions.Description, tblUpgradeOptions.ModelorStyle as Style, " & _
        '            "tblUpgradeOptions.CustomerPrice, tblUpgradeOptions.UnitType, tblUpgradeOptions.UpgradeCategory, " & _
        '            "tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeLevel, tblUpgradeOptions.UpgradeOptionID " & _
        '            "FROM tblUpgradeOptions " & _
        '            "GROUP BY tblUpgradeOptions.active, tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, " & _
        '            "tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.UpgradeLevel, " & _
        '            "tblUpgradeOptions.Description, tblUpgradeOptions.ModelOrStyle, " & _
        '            "tblUpgradeOptions.CustomerPrice, tblUpgradeOptions.UpgradeOptionID " & _
        '            "HAVING  tblUpgradeOptions.active=true and (((tblUpgradeOptions.[UnitType)='" & [Forms.frmQuote.UnitID.Column(3) & "') AND " & _
        '            "((tblUpgradeOptions.[Location)='" & [Forms.frmQuote.lstRooms.Column(2) & "') and " & _
        '            "((tblUpgradeOptions.[UpgradeLevel)=""" & fReplaceQuotes([Forms.frmQuote.lstUpgradesAvail.Column(3)) & """) and " & _
        '            "((tblUpgradeOptions.[UpgradeCategory) = """ & fReplaceQuotes([Forms.frmQuote.lstClasses.Column(1)) & """));"
        '        Else ' Me.BuildingPhaseOption = 2
        '            Me.lstStyle.Enabled = True

        '            lstStyle.RowSource = "SELECT tblUpgradeOptions.Description, tblUpgradeOptions.ModelorStyle, " & _
        '            "tblUpgradeOptions.CustomerPrice, tblUpgradeOptions.UnitType, tblUpgradeOptions.UpgradeCategory, " & _
        '            "tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeLevel, tblUpgradeOptions.UpgradeOptionID " & _
        '            "FROM tblUpgradeOptions " & _
        '            "GROUP BY tblUpgradeOptions.active, tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, " & _
        '            "tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.UpgradeLevel, " & _
        '            "tblUpgradeOptions.Description, tblUpgradeOptions.ModelOrStyle, " & _
        '            "tblUpgradeOptions.CustomerPrice, tblUpgradeOptions.UpgradeOptionID " & _
        '            "HAVING tblUpgradeOptions.active=true and (((tblUpgradeOptions.[UnitType)='" & [Forms.frmQuote.UnitID.Column(3) & "') AND " & _
        '            "((tblUpgradeOptions.[Location)='" & [Forms.frmQuote.lstRooms.Column(2) & "') and " & _
        '            "((tblUpgradeOptions.[UpgradeLevel)=""" & fReplaceQuotes([Forms.frmQuote.lstUpgradesAvail.Column(3)) & """) and " & _
        '            "((tblUpgradeOptions.[UpgradeCategory) = """ & fReplaceQuotes([Forms.frmQuote.lstClasses.Column(1)) & """));"

        '            Debug.Print(lstStyle.RowSource)
        '            lstStyle.Requery()
        '        End If

    End Sub

    Private Sub lstUpgradesAvail_Click()
        '        sDisplayListbox(lstUpgradesAvail)
    End Sub

    'Private Sub UnitID_Change()
    '        On Error GoTo Err_UnitID_Click

    '        '    DoCmd.DoMenuItem acFormBar, acRecordsMenu, 5, , acMenuVer70

    'Exit_UnitID_Click:
    '        Exit Sub

    'Err_UnitID_Click:
    '        MsgBox(Err.Description)
    '        Resume Exit_UnitID_Click

    'End Sub

    '***
    '*** Gotta write this with JavaScript
    Private Sub txtPhase1CompleteDate_DblClick(ByVal Cancel As Integer)
        '        If Not IsDate(txtPhase1CompleteDate.Text) Then
        '            txtPhase1CompleteDate.Text = Date.Today
        '        Else
        '            txtPhase1CompleteDate.Text = DateAdd(DateInterval.Day, -1, CDate(txtPhase1CompleteDate.Text))
        '        End If

        '        Me.cmbPhase1Status.SelectedValue = "Completed"

        'Exit_txtPhase1CompleteDate_DblClick:
        '        Exit Sub

        'Err_txtPhase1CompleteDate_DblClick:
        '        MsgBox(Err.Description)
        '        Resume Exit_txtPhase1CompleteDate_DblClick
    End Sub


    '***
    '*** Gotta write this with JavaScript
    Private Sub txtPhase1TargetDate_DblClick(ByVal Cancel As Integer)

        '        On Error GoTo Err_txtPhase1TargetDate_DblClick

        '        If IsNull(Me.ActiveControl) Then
        '    Me.ActiveControl = Date + 14
        '        Else
        '            Me.ActiveControl = Me.ActiveControl - 1
        '        End If

        'Exit_txtPhase1TargetDate_DblClick:
        '        Exit Sub

        'Err_txtPhase1TargetDate_DblClick:
        '        MsgBox(Err.Description)
        '        Resume Exit_txtPhase1TargetDate_DblClick

    End Sub


    '***
    '*** Gotta write this with JavaScript
    Private Sub txtPhase2CompleteDate_DblClick(ByVal Cancel As Integer)
        '        If IsNull(Me.ActiveControl) Then
        '    Me.ActiveControl = Date
        '        Else
        '            Me.ActiveControl = Me.ActiveControl - 1
        '        End If

        '        Me.cmbphase2status = "Completed"

        'Exit_txtPhase2CompleteDate_DblClick:
        '        Exit Sub

        'Err_txtPhase2CompleteDate_DblClick:
        '        MsgBox(Err.Description)
        '        Resume Exit_txtPhase2CompleteDate_DblClick
    End Sub

    Private Sub cmbphase2status_AfterUpdate()

        '        If Me.cmbphase2status = "Completed" Then
        '            If IsNull(Me.txtPhase2CompleteDate) Then
        '        Me.txtPhase2CompleteDate = Date
        '            End If

        '        End If

    End Sub



    'Sub sDisplayListbox(ByVal lst As ListBox)
    '        Dim liCounter As Integer
    '        Dim lsMsg As String
    '        Dim liRetVal As Integer
    '        For liCounter = 0 To lst.ColumnCount - 1
    '            lsMsg = lsMsg & " " & lst.Column(liCounter)
    '        Next
    '        liRetVal = SysCmd(acSysCmdSetStatus, lsMsg)

    'End Sub

    ' Private Sub Form_Load()

    '        If Me.BuildingPhaseOption = 1 Then
    '            Me.cmdAutoPick.Enabled = False
    '            Me.AddNewOption.Enabled = False
    '        Else
    '            Me.cmdAutoPick.Enabled = True
    '            '*** Set value to True to allow new finish selection on the fly.
    '            Me.AddNewOption.Enabled = False
    '        End If
    '        If Me.cmbQuoteStatus = "Completed" Then

    '            lstRooms.Enabled = False
    '            lstClasses.Enabled = False
    '            lstUpgradesAvail.Enabled = False
    '            lstStyle.Enabled = False
    '            lstRequestedUpgrades.Enabled = False
    '            Me.pnlQuoteStatus.Enabled = False

    '            Me.cmdaddnewoption.Enabled = False
    '            Me.cmdAutoPick.Enabled = False
    '            Me.pnlBuildingPhaseOption.Enabled = False

    '        Else

    '            If Me.cmbphase1status = "Completed" Then
    '                Me.BuildingPhaseOption.DefaultValue = 2
    '            End If

    '            lstRooms.Enabled = True
    '            lstRequestedUpgrades.Enabled = True
    '            pnlQuoteStatus.Enabled = True

    '        End If
    'End Sub
    '*******************************************************
    '*** END OF LEGACY CODE Already converted
    '*** Just keeping it around for reference
    '*****************************************************







    Private Sub AddNewOption_Click()
        On Error GoTo Err_AddNewOption_Click

        Dim stDocName As String
        Dim stLinkCriteria As String
        '        '    stLinkCriteria = "[UnitType=" & Me.CustomerID & " and [BuildingPhase=" & Me.BuildingPhaseOption

        stDocName = "frmOptionPricingQuickLoad"
        '        '### DoCmd.OpenForm(stDocName, , , stLinkCriteria)
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
        '        '    stLinkCriteria = "[QuoteID=" & Me.QuoteID & " and [BuildingPhase=" & Me.BuildingPhaseOption

        '        stLinkCriteria = "[QuoteID=" & Me.txtQuoteID.Text
        '        '###   DoCmd.OpenForm(stDocName, , , stLinkCriteria)

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

        '        stLinkCriteria = "[CustomerID=" & Me.CustomerID
        '        DoCmd.OpenForm(stDocName, , , stLinkCriteria)

Exit_CustomerID_DblClick:
        Exit Sub

Err_CustomerID_DblClick:
        ShowErrors(Err.Number, Err.Description)
        Resume Exit_CustomerID_DblClick

    End Sub

    Private Sub Form_AfterUpdate()
        '        '*** Logging
        '        Dim rs As Recordset
        '        rs = Me.RecordsetClone
        '        Dim f As Field

        '        For Each f In rs.Fields
        '            cLog.UpdateField(f)
        '        Next

        '        '*** End Logging
    End Sub

    Private Sub Form_Current()

        '        '*** Logging
        '        Dim rs As Recordset
        '        Dim f As Field

        '        rs = Me.RecordsetClone

        '        cLog.ClearItems()

        '        For Each f In rs.Fields
        '            cLog.AddField(f, CStr(Nz(Me.QuoteID)))
        '        Next
        '        '*** End Logging

        '        '*** Check whether the Phases should be available to the user
        '        sCheckPhaseComplete()

        '        DoCmd.DoMenuItem(acFormBar, acRecordsMenu, 5, , acMenuVer70)

        '        lstRequestedUpgrades.RowSource = "SELECT tblRequestedUpgrades.RequestedUpgradeID, " & _
        '                "tblRequestedUpgrades.UpgradeCategory as [Category, " & _
        '                "tblRequestedUpgrades.UpgradeClass as [Upgrade Level, " & _
        '                "tblRequestedUpgrades.UpgradeDescription as Description, " & _
        '                "tblRequestedUpgrades.StyleDescription as Style, " & _
        '                "tblRequestedUpgrades.CustomerPrice + tblRequestedUpgrades.Adjustments AS Cost " & _
        '                "FROM tblRequestedUpgrades " & _
        '                "WHERE (tblRequestedUpgrades.RoomDescription)= '" & lstRooms.Column(2) & "'" & _
        '                "and (tblRequestedUpgrades.BuildingPhase) = " & Me.BuildingPhaseOption & " " & _
        '                "and (tblRequestedUpgrades.quoteid) = " & Me.QuoteID & " order by tblRequestedUpgrades.UpgradeCategory"



        '        lstRequestedUpgrades.Requery()
        '        Me.SetFocus()
    End Sub



    Private Sub sCheckPhaseComplete()


        '        Dim d As DropDownList = FormView1.FindControl("cmbQuoteStatus")
        '        If d Is Nothing Then Exit Sub
        '        Dim dP1 As DropDownList = FormView1.FindControl("cmbPhase1Status")
        '        Dim dP2 As DropDownList = FormView1.FindControl("cmbPhase2Status")
        '        Dim tP1 As TextBox = FormView1.FindControl("txtPhase1CompleteDate")
        '        Dim tP2 As TextBox = FormView1.FindControl("txtPhase2CompleteDate")
        '        Dim tt1 As TextBox = FormView1.FindControl("txtPhase1TargetDate")
        '        Dim tt2 As TextBox = FormView1.FindControl("txtPhase2TargetDate")
        '        If d.SelectedValue = "Completed" Then
        '            cmdCustomerReceipt.Focus()


        '            lstRooms.Enabled = False
        '            lstClasses.Enabled = False
        '            lstUpgradesAvail.Enabled = False
        '            lstStyle.Enabled = False
        '            lstRequestedUpgrades.Enabled = False

        '            FormView1.Enabled = False
        '            'Me.cmbphase1status.Enabled = False
        '            'Me.txtPhase1TargetDate.Enabled = False
        '            'Me.txtPhase1CompleteDate.Enabled = False
        '            'cmbQuoteStatus.Enabled = False
        '            'BuildingPhaseOption.Enabled = False
        '            '            Me.Phase2Status.Enabled = False
        '            '            Me.txtPhase2TargetDate.Enabled = False
        '            '            Me.txtPhase2CompleteDate.Enabled = False

        '            '            '
        '            '            Me.CustomerID.Enabled = False
        '            '            Me.UnitID.Enabled = False
        '            '            Me.QuoteComments.Enabled = False


        '            If Not IsDate(tP1.Text) Or dP1.SelectedValue <> "Completed" Then
        '                SetQuoteStatus(Session("QuoteID"), "Phase1Status", "Completed", "Phase1CompleteDate", """" & Date.Today & """")
        '                '        Me.cmbphase1status = "Completed"
        '                '        Me.txtPhase1CompleteDate = Date
        '            End If
        '            If Not IsDate(tP2.Text) Or dP2.SelectedValue <> "Completed" Then
        '                SetQuoteStatus(Session("QuoteID"), "Phase2Status", "Completed", "Phase2CompleteDate", """" & Date.Today & """")
        '                '        Me.cmbphase1status = "Completed"
        '                '        Me.txtPhase1CompleteDate = Date
        '            End If
        '            cmdAutoPick.Enabled = False
        '        Else
        '            FormView1.Enabled = True
        '            '            BuildingPhaseOption.Enabled = True
        '            '            '*** Whole thing not complete Check individual Phases


        '            If dP1.Text = "Completed" Then
        '                dP1.Enabled = False
        '                tP1.Enabled = False
        '                tt1.Enabled = False
        '                cmdAutoPick.Enabled = False

        '            Else
        '                cmdAutoPick.Enabled = True
        '                dP1.Enabled = True
        '                tP1.Enabled = True
        '                tt1.Enabled = True
        '            End If

        '            If dP2.Text = "Completed" Then
        '                dP2.Enabled = False
        '                tP2.Enabled = False
        '                tt2.Enabled = False
        '            Else
        '                dP2.Enabled = True
        '                tP2.Enabled = True
        '                tt2.Enabled = True
        '            End If


        '            If d.SelectedValue = "Completed" Then
        '                d.Enabled = False
        '            Else
        '                d.Enabled = True
        '            End If

        '        End If

        '        Select Case rblPhase.SelectedValue
        '            Case 1
        '                If dP1.SelectedValue = "Completed" Then
        '                    lstRooms.Enabled = False
        '                Else
        '                    lstRooms.Enabled = True
        '                End If
        '            Case 2
        '                If dP2.SelectedValue = "Completed" Then
        '                    lstRooms.Enabled = False
        '                Else
        '                    lstRooms.Enabled = True
        '                End If
        '        End Select
    End Sub

    Private Sub cmbphase2status_BeforeUpdate(ByVal Cancel As Integer)
        '        On Error GoTo ph2Status_Error
        '        '        If UCase(cmbphase2status.value) = "COMPLETED" Or UCase(Phase2Status.value) = "CLOSED" Then
        '        '            Cancel = fRequired(Me.QuoteID, 2)
        '        '            Phase2Status.Undo()
        '        '        End If
        'ph2Status_Exit:
        '        Exit Sub
        'ph2Status_Error:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume ph2Status_Exit
    End Sub

    Private Sub txtPhase2TargetDate_DblClick(ByVal Cancel As Integer)
        '        '        If IsNull(Me.ActiveControl) Then
        '        '    Me.ActiveControl = Date + 14
        '        '        Else
        '        '            Me.ActiveControl = Me.ActiveControl - 1
        '        '        End If

        'Exit_txtPhase2TargetDate_DblClick:
        '        Exit Sub

        'Err_txtPhase2TargetDate_DblClick:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume Exit_txtPhase2TargetDate_DblClick
    End Sub

    Private Sub cmbQuoteStatus_AfterUpdate()
        '        '***        sCheckPhaseComplete()
    End Sub

    Private Sub cmbQuoteStatus_BeforeUpdate(ByVal Cancel As Integer)
        '        On Error GoTo cmbQuoteStatus_Error
        '        '        If UCase(cmbQuoteStatus.value) = "COMPLETED" Or UCase(cmbQuoteStatus.value) = "CLOSED" Then
        '        '            Cancel = fRequired(Me.QuoteID, 0)
        '        '            cmbQuoteStatus.Undo()
        '        '        End If
        'cmbQuoteStatus_Exit:
        '        Exit Sub
        'cmbQuoteStatus_Error:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume cmbQuoteStatus_Exit
    End Sub

    Private Sub UnitID_DblClick(ByVal Cancel As Integer)
        '        On Error GoTo Err_UnitID_DblClick
        '        Dim stDocName As String
        '        Dim stLinkCriteria As String

        '        stDocName = "frmUnits"
        '        '        stLinkCriteria = "[UnitID=" & Me.UnitID
        '        '        '    DoCmd.OpenForm stDocName, , , stLinkCriteria

        'Exit_UnitID_DblClick:
        '        Exit Sub

        'Err_UnitID_DblClick:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume Exit_UnitID_DblClick

    End Sub

    Private Sub btnCustomerReceipt_Click()
        '        On Error GoTo Err_btnCustomerReceipt_Click
        '        Dim liPhase As Integer
        '        Dim lsPhase As String
        '        Dim lsMsg As String
        '        '        If BuildingPhaseOption.Enabled Then
        '        '            liPhase = BuildingPhaseOption.value
        '        '        Else
        '        '            Do While liPhase < 1 Or liPhase >= 3
        '        '                '           Do While Not IsNumeric(lsPhase)
        '        '                lsPhase = InputBox(lsMsg & "Enter Phase (1 for Structural, 2 for Finishes)", "")
        '        '                lsMsg = "Sorry, That does not seem to be a valid Phase number. " & Chr(13) & Chr(10)
        '        '                '           Loop
        '        '                liPhase = Int(Val(lsPhase))
        '        '            Loop
        '        '        End If
        '        '        If Me.BuildingPhaseOption.value <> liPhase Then Me.BuildingPhaseOption.value = liPhase
        '        '        Dim stDocName As String
        '        '        stDocName = "rptCustomerReceiptLegal" & IIf(liPhase = 1, "Structural", "Finishes")

        '        '        sMakeRider()
        '        '        DoCmd.OpenReport(stDocName, acPreview, , "UnitID = " & Me.UnitID)

        '        '        stDocName = "rptCustomerReceipt"
        '        '        DoCmd.OpenReport(stDocName, acPreview, , "UnitID = " & Me.UnitID)

        '        '        '*** Now handle all Additional Documents to Print
        '        '        Dim rs As ADODB.Recordset
        '        '        rs = New ADODB.Recordset
        '        '        rs.Open("Select * from qryGetDocuments where QuoteID = " & _
        '        '        Me.QuoteID.value & " And buildingphase = " & Me.BuildingPhaseOption.value, CurrentProject.Connection)
        '        '        Dim lsFile As String
        '        '        Dim liRetVal As Integer

        '        '        Do While Not rs.EOF
        '        '            'lsFile = rs("AdditionalFileToPrint1").value
        '        '            lsFile = Nz(rs("AdditionalFileToPrint1").value)
        '        '            If lsFile <> "" Then
        '        '                'liRetVal = MsgBox("Print " & lsFile, vbYesNo)
        '        '                liRetVal = MsgBox("Print " & lsFile, vbYesNoCancel)
        '        '                Select Case liRetVal
        '        '                    Case vbCancel
        '        '                        Exit Do
        '        '                    Case vbYes
        '        '                        PrintSpool(lsFile)
        '        '                End Select
        '        '            End If
        '        '            lsFile = Nz(rs("AdditionalFileToPrint2").value)
        '        '            If lsFile <> "" Then
        '        '                liRetVal = MsgBox("Print " & lsFile, vbYesNoCancel)
        '        '                Select Case liRetVal
        '        '                    Case vbCancel
        '        '                        Exit Do
        '        '                    Case vbYes
        '        '                        PrintSpool(lsFile)
        '        '                End Select
        '        '            End If
        '        '            rs.MoveNext()
        '        '        Loop
        '        '        rs.Close()

        'Exit_btnCustomerReceipt_Click:
        '        Exit Sub

        'Err_btnCustomerReceipt_Click:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume Exit_btnCustomerReceipt_Click

    End Sub

    Private Sub AutoPop_Click()
        '        '        On Error GoTo Err_AutoPop_Click

        '        '        Dim Del As Object

        '        '        Del = MsgBox("         Are you sure?", vbYesNo, "Auto-populate")
        '        '        If Del = 6 Then
        '        '            'delete code
        '        '            Dim stDocName As String
        '        '            DoCmd.SetWarnings(False)
        '        '            stDocName = "qryBuildStandardOptionsLevel"
        '        '            DoCmd.OpenQuery(stDocName, acNormal, acEdit)
        '        '            stDocName = "qryBuildStandardOptionsLeveltwo"
        '        '            DoCmd.OpenQuery(stDocName, acNormal, acEdit)
        '        '            DoCmd.SetWarnings(True)

        '        '            DoCmd.DoMenuItem(acFormBar, acRecordsMenu, 5, , acMenuVer70)

        '        '        End If

        '        'Exit_AutoPop_Click:
        '        '        Exit Sub

        '        'Err_AutoPop_Click:
        '        '        MsgBox(Err.Description)
        '        '        Resume Exit_AutoPop_Click

    End Sub

    Private Sub SearchQuotes_Click()
        '        On Error GoTo Err_SearchQuotes_Click

        '        Dim stDocName As String
        '        Dim stLinkCriteria As String

        '        stDocName = "frmSearchForCustomer"
        '        '***        DoCmd.OpenForm(stDocName, , , stLinkCriteria)

        'Exit_SearchQuotes_Click:
        '        Exit Sub

        'Err_SearchQuotes_Click:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume Exit_SearchQuotes_Click
    End Sub

    Private Sub btnCommunications_Click()
        '        On Error GoTo Err_btnCommunications_Click
        '        Dim stDocName As String
        '        Dim stLinkCriteria As String

        '        stDocName = "frmCommunications"

        '        '        '    stLinkCriteria = "[CustomerID=" & Me.CustomerID & " and [BuildingPhase=" & Me.BuildingPhaseOption
        '        '        stLinkCriteria = "[CustomerID=" & Me.CustomerID

        '        '        DoCmd.OpenForm(stDocName, , , stLinkCriteria)

        'Exit_btnCommunications_Click:
        '        Exit Sub

        'Err_btnCommunications_Click:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume Exit_btnCommunications_Click

    End Sub

    Private Sub btnPayments_Click()
        '        On Error GoTo Err_btnPayments_Click

        '        Dim stDocName As String
        '        Dim stLinkCriteria As String

        '        stDocName = "frmPayments"
        '        '        '    stLinkCriteria = "[QuoteID=" & Me.QuoteID & " and [BuildingPhase=" & Me.BuildingPhaseOption

        '        '        stLinkCriteria = "[QuoteID=" & Me.QuoteID
        '        '        DoCmd.OpenForm(stDocName, , , stLinkCriteria)

        'Exit_btnPayments_Click:
        '        Exit Sub

        'Err_btnPayments_Click:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume Exit_btnPayments_Click

    End Sub

    Private Sub cmdAutoPick_Click()
        '        '        On Error GoTo Err_cmdAutoPick_Click

        '        '        Dim myworkspace As Workspace, mydb As Database
        '        '        Dim qdtemp As QueryDef, SQL$, i%
        '        '        mydb = DBEngine.Workspaces(0).Databases(0)

        '        '        '
        '        '        Dim Del As Object

        '        '        Del = 1
        '        '        Del = MsgBox("            Are you sure?", vbYesNo, "Auto Pick")

        '        '        If Del = 6 Then
        '        '            'Me.QuoteID & " and [RequestedUpgradeID=" & Me.lstRequestedUpgrades.Column(0)

        '        '            SQL = "INSERT INTO tblRequestedUpgrades ( UpgradeOptionID, QuoteID, RoomDescription, " & _
        '        '            "UpgradeDescription, UpgradeCategory, UpgradeClass, StyleDescription, CustomerPrice, " & _
        '        '            "BuildingPhase, PricingRevNumber, Standard ) " & _
        '        '            "SELECT tblUpgradeOptions.UpgradeOptionID, " & Me.QuoteID & "," & _
        '        '            "tblUpgradeOptions.Location, tblUpgradeOptions.Description, " & _
        '        '            "tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.UpgradeLevel, " & _
        '        '            "tblUpgradeOptions.ModelOrStyle, tblUpgradeOptions.CustomerPrice, " & _
        '        '            "tblUpgradeOptions.BuildingPhase, tblUpgradeOptions.PricingRevNumber, " & _
        '        '            "tblUpgradeOptions.Standard FROM tblUpgradeOptions " & _
        '        '            "WHERE (((tblUpgradeOptions.CustomerPrice) = 0) And " & _
        '        '            "((tblUpgradeOptions.UpgradeLevel) = 'Standard') and " & _
        '        '            "((tblUpgradeOptions.Standard) = Yes) and " & _
        '        '            "((tblUpgradeOptions.UnitType) = '" & Me.UnitID.Column(3) & "'))" & _
        '        '            "ORDER BY tblUpgradeOptions.Location;"
        '        '            qdtemp = mydb.CreateQueryDef("", SQL)
        '        '            qdtemp.Execute()
        '        '            qdtemp.Close()

        '        '            SQL = "INSERT INTO tblRequestedUpgrades ( UpgradeOptionID, QuoteID, RoomDescription, " & _
        '        '            "UpgradeDescription, UpgradeCategory, UpgradeClass, StyleDescription, CustomerPrice, " & _
        '        '            "BuildingPhase, PricingRevNumber, Standard ) " & _
        '        '            "SELECT tblUpgradeOptions.UpgradeOptionID, " & Me.QuoteID & "," & _
        '        '            "tblUpgradeOptions.Location, tblUpgradeOptions.Description, " & _
        '        '            "tblUpgradeOptions.UpgradeCategory, tblUpgradeOptions.UpgradeLevel, " & _
        '        '            "tblUpgradeOptions.ModelOrStyle, tblUpgradeOptions.CustomerPrice, " & _
        '        '            "tblUpgradeOptions.BuildingPhase, tblUpgradeOptions.PricingRevNumber, " & _
        '        '            "tblUpgradeOptions.Standard FROM tblUpgradeOptions " & _
        '        '            "WHERE (((tblUpgradeOptions.CustomerPrice) = 0) And " & _
        '        '            "((tblUpgradeOptions.UpgradeLevel) = 'Standard') and " & _
        '        '            "((tblUpgradeOptions.UnitType) = '" & Me.UnitID.Column(3) & "'))" & _
        '        '            "ORDER BY tblUpgradeOptions.Location;"

        '        '            qdtemp = mydb.CreateQueryDef("", SQL)
        '        '            qdtemp.Execute()
        '        '            qdtemp.Close()

        '        '            lstRequestedUpgrades.Requery()

        '        '        Else
        '        '            Exit Sub
        '        '        End If

        '        '        DoCmd.DoMenuItem(acFormBar, acRecordsMenu, 5, , acMenuVer70)
        '        '        MsgBox("Selections Added")

        'Exit_cmdAutoPick_Click:
        '        Exit Sub

        'Err_cmdAutoPick_Click:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume Exit_cmdAutoPick_Click

    End Sub

    Private Sub cmdMissingSelections_Click()
        '        On Error GoTo Err_cmdMissingSelections_Click
        '        Dim lb As Boolean
        '        '***        lb = fMissingSelections(Me.QuoteID, "Report", Me.BuildingPhaseOption)

        'Exit_cmdMissingSelections_Click:
        '        Exit Sub

        'Err_cmdMissingSelections_Click:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume Exit_cmdMissingSelections_Click

    End Sub





    '    '*** Start Good Stuff
    Private Sub DeleteRequestedItem(ByVal llID As Long)
        '        On Error GoTo Requested_Upgrades_DblClick_error

        '        Session("llUpgradeOptionID") = llID

        '        Dim Del As Integer

        '        Del = 1

        '        Del = MsgBox("Would you like to Delete this selection? (Click No to edit)", vbYesNo, "Delete Selection")

        '        If Del = 6 Then
        '            'delete code
        '            Me.odsRequestedUpgrades.Delete()

        '        Else
        '            'stDocName = "frmEditNewRequestedUpgrades"
        '            'stLinkCriteria = "[QuoteID=" & Me.QuoteID & " and [RequestedUpgradeID=" & Me.lstRequestedUpgrades.Column(0)
        '            'DoCmd.OpenForm(stDocName, , , stLinkCriteria)
        '        End If

        'Exit_Requested_Upgrades_DblClick:
        '        Exit Sub

        'Requested_Upgrades_DblClick_error:
        '        MsgBox("Error Deleting or Editing entry" & Err.Description)
        '        Resume Exit_Requested_Upgrades_DblClick

    End Sub


    Private Sub InsertRequestedItem(ByVal llUpgradeOptionID As Long)
        '        Dim Style As String

        '        Dim llQuoteID As Long
        '        Dim lsRoomDesc As String
        '        Dim lsUpgradeDesc As String
        '        Dim lsUpgradeCategory As String
        '        Dim lsUpgradeClass As String
        '        Dim lsStyle As String
        '        ' Dim llUpgradeOptionID As Long
        '        Dim lsMsg As String

        '        llQuoteID = Session("QuoteID")
        '        '   lsRoomDesc = lstStyle.s
        '        '   lsUpgradeDesc = Me.lstStyle.SelectedItem.Attributes().ToString
        '        '   lsUpgradeCategory = Me.lstStyle.SelectedItem.Attributes().ToString
        '        '   lsUpgradeClass = Me.lstStyle.SelectedItem.Attributes().ToString

        '        '  llUpgradeOptionID = Me.lstStyle.SelectedItem.Attributes().ToString

        '        On Error GoTo WriteRequestStyleError




        '        '    If Me.lstStyle.SelectedItem.Attributes(1).ToString = "" Then
        '        ' Style = "**"
        '        ' Else
        '        ' Style = Me.lstStyle.SelectedItem.Attributes(1).ToString
        '        ' End If

        '        '   lsStyle = Style
        '        Dim lbInc As Boolean
        '        'PUT THIS BACK LATER    lbInc = fcheckforIncompatibilities(llQuoteID, lsRoomDesc, lsUpgradeDesc, lsUpgradeCategory, lsUpgradeClass, lsStyle, llUpgradeOptionID, lsMsg)
        '        If lbInc = True Then
        '            '*** Incompatible Canceled
        '            ' Debug.Print("Incompatible: Canceled")
        '            Exit Sub
        '        End If

        '        Session("llUpgradeOptionID") = llUpgradeOptionID
        '        If Session("RequestedUpgradeCount") = 0 Then
        '            odsRequestedUpgrades.Insert()

        '        Else

        '        End If
        '        litMsg.Text = "An Upgrade has already been chosen for this category"
        '        lstRequestedUpgrades.DataBind()

        'Exit_Style_DblClick:
        '        Exit Sub

        'WriteRequestStyleError:
        '        If Err.Number = -2147467259 Then
        '            MsgBox("You can not choose this Category, because a choice has already been made")
        '        Else
        '            MsgBox("You may not have the appropriate permissions to make selections" & Chr(13) & Chr(10) & Err.Description)
        '            ' Debug.Print(Err.Number)
        '        End If

        '        Resume Exit_Style_DblClick

    End Sub
    Protected Sub SetQuoteStatus(ByVal llQuoteID As Long, ByVal lsStatusType As String, ByVal lsVal As String, ByVal lsDateField As String, ByVal lsDate As String)
        '        Dim Quote As New clsSelectDataLoader
        '        Quote.UpdateQuoteStatus(llQuoteID, lsStatusType, lsVal, lsDateField, lsDate)
    End Sub
    Protected Sub cmbPhase1Status_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '        Dim d As DropDownList
        '        Dim t As TextBox
        '        Dim theDate As Date = Date.Today
        '        Dim lbCancel As Boolean

        '        d = sender
        '        t = FormView1.FindControl("txtPhase1CompleteDate")
        '        If IsDate(t.Text) Then
        '            theDate = t.Text
        '        End If

        '        On Error GoTo ph1Status_Error
        '        If UCase(sender.SelectedValue) = "COMPLETED" Or UCase(sender.SelectedValue) = "CLOSED" Then
        '            '*** Check for Required Items
        '            'MOVE THIS TO THE DATA LOADER
        '            'lbCancel = fRequired(Me.QuoteID, 1)
        '            If lbCancel = False Then
        '                SetQuoteStatus(Session("QuoteID"), "Phase1Status", d.SelectedValue, "Phase1CompleteDate", """" & theDate.Date & """")
        '            End If
        '        Else
        '            SetQuoteStatus(Session("QuoteID"), "Phase1Status", d.SelectedValue, "Phase1CompleteDate", "Null")


        '        End If
        '        'odsQuotes.DataBind()
        '        Me.DataBind()

        'ph1Status_Exit:
        '        Exit Sub
        'ph1Status_Error:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume ph1Status_Exit
    End Sub
    Protected Sub Page_databinding(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.DataBinding
        Dim l As Long
        l = 34
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '        If Session("UnitType") Is Nothing Then
        '            Session("UnitType") = "R134A"
        '        End If
        '        If Session("QuoteID") Is Nothing Then
        '            Session("QuoteID") = 1
        '        End If
        '        If Session("NodeID") Is Nothing Then
        '            Session("NodeID") = 1
        '        End If
        '        If rblPhase.SelectedIndex < 0 Then
        '            rblPhase.SelectedIndex = 0
        '        End If
        '        Me.FormView1.ChangeMode(FormViewMode.Edit)



        '        If Me.rblPhase.SelectedValue = 1 Then
        '            Me.cmdAutoPick.Enabled = False
        '            Me.cmdAddNewOption.Enabled = False
        '        Else
        '            Me.cmdAutoPick.Enabled = True
        '            '*** Set value to True to allow new finish selection on the fly.
        '            Me.cmdAddNewOption.Enabled = False
        '        End If
        '        Dim v As DataView = odsQuotes.Select
        '        Dim iterator As IEnumerator = v.GetEnumerator
        '        iterator.MoveNext()
        '        Dim drv As System.Data.DataRowView = iterator.Current
        '        Dim r As DataRow = drv.Row

        '        Dim cmb As DropDownList
        '        cmb = Me.FormView1.FindControl("cmbQuoteStatus")
        '        'If cmb Is Nothing Then  Else         End If
        '        'If cmb.SelectedValue = "Completed" Then
        '        If r("QuoteStatus") = "Completed" Then
        '            lstRooms.Enabled = False
        '            lstClasses.Enabled = False
        '            lstUpgradesAvail.Enabled = False
        '            lstStyle.Enabled = False
        '            lstRequestedUpgrades.Enabled = False
        '            pnlQuote.Enabled = False

        '            Me.cmdAddNewOption.Enabled = False
        '            Me.cmdAutoPick.Enabled = False
        '            Me.rblPhase.Enabled = False

        '        Else
        '            ' cmb = Me.FormView1.Controls("cmbphase1status")
        '            ' If cmb.SelectedValue = "Completed" Then
        '            If r("Phase1Status") = "Completed" Then
        '                Me.rblPhase.SelectedValue = 2
        '            End If

        '            lstRooms.Enabled = True
        '            lstRequestedUpgrades.Enabled = True
        '            pnlQuote.Enabled = True ' Quote Status

        '        End If

        '        sCheckPhaseComplete()
    End Sub

    Protected Sub lstStyle_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) 'Handles lstStyle.ItemCommand
        '        ' Dim c As Control
        '        If e.CommandName = "InsertRequestedItem" Then
        '            InsertRequestedItem(e.CommandArgument)
        '        End If
        '        '  For Each c In e.Item.Controls
        '        ' Console.WriteLine(c.ID & ":" & c.UniqueID & ":" & c.TemplateControl.ToString & ":")
        '        ' Next
    End Sub

    Protected Sub odsRequestedUpgrades_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) 'Handles odsRequestedUpgrades.Inserting
        '        '  Dim lst As ListViewDataItem = Session("RequestedItemID") ' lstStyle.Items(lstStyle.SelectedIndex)
        '        ' Dim findMe As Integer = lst.DisplayIndex

        '        Dim ds As DataSet
        '        Dim dr As DataRow
        '        Dim ll As Long
        '        ll = Session("llUpgradeOptionID")
        '        Dim dl As New clsSelectDataLoader
        '        ds = dl.LoadRoomCategoryLevelStyles(0, "", 0, "", "", "", ll)
        '        Dim RequestTypeHold As String
        '        If Me.rblPhase.SelectedValue = "3" Then
        '            RequestTypeHold = "Change"
        '        Else
        '            RequestTypeHold = "Standard"
        '        End If

        '        For Each dr In ds.Tables(0).Rows
        '            Dim llQuoteID As Long = Session("QuoteID")
        '            Dim lsRoomDesc As String = IIf(lstRooms.SelectedValue = "", "", lstRooms.SelectedValue)
        '            Dim lsPhase As String = IIf(rblPhase.SelectedValue = "", "", rblPhase.SelectedValue)
        '            Dim llNodeID = Session("NodeID")
        '            Dim lsUpgradeDesc As String = dr("Description")
        '            Dim lsUpgradeCategory As String = dr("UpgradeCategory")
        '            Dim lsUpgradeClass = dr("UpgradeLevel")
        '            Dim lsStyle As String = dr("style")
        '            Dim lsPrice As String = dr("customerprice")
        '            Dim llUpgradeOptionID As Long = Session("llUpgradeOptionID")




        '            e.InputParameters("llQuoteID") = llQuoteID
        '            e.InputParameters("lsRoomDesc") = lsRoomDesc
        '            e.InputParameters("lsPhase") = IIf(rblPhase.SelectedValue = "", "", rblPhase.SelectedValue)
        '            e.InputParameters("llNodeID") = llNodeID
        '            e.InputParameters("lsUpgradeDesc") = lsUpgradeDesc
        '            e.InputParameters("lsUpgradeCategory") = lsUpgradeCategory
        '            e.InputParameters("lsUpgradeClass") = lsUpgradeClass
        '            e.InputParameters("lsStyle") = lsStyle
        '            e.InputParameters("lsPrice") = lsPrice
        '            e.InputParameters("lsTypeHold") = RequestTypeHold
        '            e.InputParameters("llUpgradeOptionID") = llUpgradeOptionID
        '        Next
    End Sub


    Protected Sub odsRequestedUpgrade_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs) ' Handles odsRequestedUpgrade.Selected
        '        Dim t As DataSet = e.ReturnValue
        '        Session("RequestedUpgradeCount") = (t.Tables(0).Rows.Count)
        '        If Session("RequestedUpgradeCount") > 0 Then
        '            lstStyle.Enabled = False
        '            pnlStyle.Enabled = False
        '        Else
        '            lstStyle.Enabled = True
        '            pnlStyle.Enabled = True
        '        End If
    End Sub

    Protected Sub lstSelectedUpgrade_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) 'Handles lstSelectedUpgrade.ItemCommand
        '        If e.CommandName = "DeleteRequestedItem" Then
        '            DeleteRequestedItem(e.CommandArgument)
        '        End If
    End Sub

    Protected Sub odsRequestedUpgrades_Deleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) ' Handles odsRequestedUpgrades.Deleting
        '        e.InputParameters("RecordID") = Session("llUpgradeOptionID")
    End Sub



    Protected Sub lstRequestedUpgrades_ItemCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ListViewCommandEventArgs) 'Handles lstRequestedUpgrades.ItemCommand
        '        If e.CommandName = "DeleteRequestedItem" Then
        '            DeleteRequestedItem(e.CommandArgument)
        '        End If

    End Sub



    Public Sub PickDate()
        '        Dim t As TextBox
        '        Dim c As Calendar
        '        Dim d As Date
        '        ' Dim f As FormView
        '        c = mCalendar
        '        d = c.SelectedDate

        '        t = FormView1.FindControl(pnlCalendar.ToolTip)
        '        t.Text = d.ToShortDateString()
        '        ToggleCalendar()
    End Sub
    Private Sub ToggleCalendar()
        '        If pnlCalendar.Visible Then
        '            pnlCalendar.Visible = False
        '            pnlCalendar.CssClass = "calendarHide"
        '        Else
        '            pnlCalendar.Visible = True
        '            pnlCalendar.CssClass = "calendarShow"
        '        End If
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)

    End Sub


    Protected Sub Calendar_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles mCalendar.SelectionChanged
        '        PickDate()
    End Sub

    Protected Sub txtPhase1TargetDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Protected Sub imgPhase2TargetDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        '        pnlCalendar.ToolTip = "txtPhase2TargetDate"
        '        ToggleCalendar()
    End Sub

    Protected Sub imgPhase1TargetDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        '        pnlCalendar.ToolTip = "txtPhase1TargetDate"
        '        ToggleCalendar()
    End Sub

    Protected Sub imgPhase1CompleteDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        '        pnlCalendar.ToolTip = "txtPhase1CompleteDate"
        '        ToggleCalendar()
    End Sub

    Protected Sub imgPhase2CompleteDate_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        '        pnlCalendar.ToolTip = "txtPhase2CompleteDate"
        '        ToggleCalendar()
    End Sub
    Public Sub ShowErrors(ByVal llError As Long, ByVal lsError As String)
        MsgBox(llError & ": " & lsError, MsgBoxStyle.Critical)
    End Sub

    Protected Sub cmbPhase2Status_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        '        Dim d As DropDownList
        '        Dim t As TextBox
        '        Dim theDate As Date = Date.Today
        '        Dim lbCancel As Boolean

        '        d = sender
        '        t = FormView1.FindControl("txtPhase2CompleteDate")
        '        If IsDate(t.Text) Then
        '            theDate = t.Text
        '        End If

        '        On Error GoTo ph1Status_Error
        '        If UCase(sender.SelectedValue) = "COMPLETED" Or UCase(sender.SelectedValue) = "CLOSED" Then
        '            '*** Check for Required Items
        '            'MOVE THIS TO THE DATA LOADER
        '            'lbCancel = fRequired(Me.QuoteID, 1)
        '            If lbCancel = False Then
        '                SetQuoteStatus(Session("QuoteID"), "Phase2Status", d.SelectedValue, "Phase2CompleteDate", """" & theDate.Date & """")
        '            End If
        '        Else
        '            SetQuoteStatus(Session("QuoteID"), "Phase2Status", d.SelectedValue, "Phase2CompleteDate", "Null")


        '        End If
        '        'odsQuotes.DataBind()
        '        Me.DataBind()

        'ph1Status_Exit:
        '        Exit Sub
        'ph1Status_Error:
        '        ShowErrors(Err.Number, Err.Description)
        '        Resume ph1Status_Exit
    End Sub


End Class
