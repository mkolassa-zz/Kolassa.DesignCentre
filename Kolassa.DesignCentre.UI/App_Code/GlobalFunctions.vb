Public Class GlobalFunctionsDC
	Public Function isGUIDString(ByVal QFormUID As String) As Boolean
		If QFormUID Is Nothing Then
			QFormUID = "00" ' "00000000-0000-0000-0000-000000000000"
		End If
		Dim guidRegEx As Regex = New Regex("^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$")
		If guidRegEx.IsMatch(QFormUID) Then
			Return True
		Else
			Return False
		End If
	End Function
End Class
Public Class clsFunctions
	Public Property mike As String
	Public Function fmike() As String
		fmike = "DUDE"
	End Function

End Class
