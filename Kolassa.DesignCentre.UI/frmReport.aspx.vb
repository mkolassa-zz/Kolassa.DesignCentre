Imports System
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Net
Imports Microsoft.Reporting.WebForms
Public Class frmReport
	Inherits System.Web.UI.Page

	Private Sub frmReport_Load(sender As Object, e As EventArgs) Handles Me.Load
		Dim ReportName As String = Request.QueryString("ReportName")
		Dim rp As ReportParameter
		If ReportName = "" Then Exit Sub
		If Not Me.IsPostBack Then
			ReportViewer1.Width = 1000
			ReportViewer1.Height = 1000
			ReportViewer1.ProcessingMode = ProcessingMode.Remote
			Dim irsc As IReportServerCredentials = New CustomReportCredentials() '; // e.g.:   ("demo-001", "123456789", "ifc")
			ReportViewer1.ServerReport.ReportServerCredentials = irsc
			ReportViewer1.ServerReport.ReportServerUrl = New Uri("http://sql5030.site4now.net/ReportServer")
			ReportViewer1.ServerReport.ReportPath = "/mkolassa-001/" & ReportName '; //e.g.: /demo-001/test

			Dim cParams As NameValueCollection = HttpUtility.ParseQueryString(ClientQueryString)
			If cParams.Count = 0 Then
			Else
				For Each pkey As String In cParams.Keys
					Dim values() As String = cParams.GetValues(pkey)
					For Each value As String In values
						If pkey = "ReportName" Then
						Else
							rp = New ReportParameter(pkey, value)

							ReportViewer1.ServerReport.SetParameters(rp)
						End If
					Next
				Next




				ReportViewer1.ServerReport.Refresh()
			End If
		End If
	End Sub



	Public NotInheritable Class CustomReportCredentials
		Implements IReportServerCredentials

		Private _UserName As String = "mkolassa-001"
		Private _PassWord As String = "DesignCenter1!"
		Private _DomainName As String = "ifc"
		Public Function GetFormsCredentials(ByRef authCookie As Cookie,
										ByRef userName As String,
										ByRef password As String,
										ByRef authority As String) _
										As Boolean _
			Implements IReportServerCredentials.GetFormsCredentials

			authCookie = Nothing
			userName = Nothing
			password = Nothing
			authority = Nothing

			'Not using form credentials
			Return False

		End Function
		Public Sub CustomReportCredentials(UserName As String, PassWord As String, DomainName As String)
			_UserName = UserName
			_PassWord = PassWord
			_DomainName = DomainName
		End Sub

		Public ReadOnly Property ImpersonationUser As System.Security.Principal.WindowsIdentity _
			Implements IReportServerCredentials.ImpersonationUser
			Get
				Return Nothing
			End Get

		End Property

		Public ReadOnly Property NetworkCredentials As ICredentials Implements IReportServerCredentials.NetworkCredentials
			Get
				Return New NetworkCredential(_UserName, _PassWord, _DomainName)
			End Get
		End Property


	End Class
End Class

