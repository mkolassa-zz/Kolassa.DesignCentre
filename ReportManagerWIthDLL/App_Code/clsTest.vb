Imports Microsoft.VisualBasic
Imports System.Collections.Generic
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
