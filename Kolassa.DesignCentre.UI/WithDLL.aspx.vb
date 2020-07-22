Imports Kolassa.DesignCenter.ReportManager
Imports System.Diagnostics
Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Partial Class frmwithdll
	Inherits Page
	' Dim ReportContainer1 As ReportContainer
	'  Protected Sub Mycontrol_1_DateSelected(Sender As Object, e As Kolassa.DesignCenter.ReportManager.DateSelectedEventArgs) Handles Mycontrol_1.DateSelected
	'     Response.Write(e.SelectedDate.ToShortDateString())

	'    End Sub
	Protected Sub btnGetWhereClause_Click(sender As Object, e As EventArgs) Handles btnGetWhereClause.Click
		Debug.Print("btnGetWhereClause_Click")
		rptBase.Refresh()
		lblModalTitle.Text = "Report Manager Data"
		lblModalBody.Text = rptBase.WhereClause & "<br/>" & rptBase.ReportOut


	End Sub

	Private Sub _Default_Load(sender As Object, e As EventArgs) Handles Me.Load
		If Me.IsPostBack Then
			Debug.Print("WithDLL Postback")
		Else
			Debug.Print("WithDLL Load NOT POSTBACK")
			Dim lsQS As String = Request.QueryString("ff")
			Dim liQS As Integer = Val(lsQS)
			rptBase.ReportID = liQS
		End If
		'  ReportContainer1 = New ReportContainer
		' ReportContainer1.ID = "ReportContainer1"
		'Me.form1.Controls.Add(ReportContainer1)

	End Sub

	Private Sub _Default_PreInit(sender As Object, e As EventArgs) Handles Me.PreInit
		Debug.Print("Page_PreInit")
		Session("ProjectID") = "742d682d-278f-4cf3-b527-c9115c5028a7"
		Session("NodeID") = 1
	End Sub

	Protected Sub btnLoadRecord_Click(sender As Object, e As EventArgs) Handles btnLoadRecord.Click
		Debug.Print("Page BTNLoadRecord_Click")
		Stop
		Dim o As New clsTest
		o = New clsTest
		o.FormValue = rptBase.f_LoadFromObject
		o.Insert()
	End Sub

	Private Sub _Default_LoadComplete(sender As Object, e As EventArgs) Handles Me.LoadComplete
		Debug.Print("Load_Complete")
		'	RaiseEvent me.btnLoadRecord().click
	End Sub
End Class
Public Class clsTest
	Public Code As String
	Public ID As String
	Public Name As String
	Public Description As String
	Public ImageURL As String = ""
	Public Active As Boolean
	Public LastUpdatedBy As String
	Public CreatedBy As String
	Public LastUpdatedDate As Date
	Public CreadedDate As Date
	Public FormValue As List(Of KeyValuePair(Of String, String))
	Public Sub New()
		Dim xguy As Integer
		xguy = 1
	End Sub

	Public Sub Insert()
		Dim lsString As String = ""
		For Each kvp As KeyValuePair(Of String, String) In FormValue
			lsString = lsString & Chr(13) & Chr(10) & kvp.Key & kvp.Value
			'Stop
		Next
		lsString = lsString
	End Sub
	Public Sub Update()

	End Sub
	Public Sub SelectRecords()

	End Sub
	Public Sub DeleteRecords()

	End Sub
End Class