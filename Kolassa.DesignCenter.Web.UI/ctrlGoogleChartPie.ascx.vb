Imports System.Data
Imports System.Web.Services
Imports System.Configuration
Imports System.Data.SqlClient
Public Class CtrlGoogleChartPie
    Inherits System.Web.UI.UserControl
    Public SQL As String
    Public xValueMember As String
    Public Property YValueMember As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim query As String = "	select QuoteStatus, Count(QUoteSTatus) as QuoteCount
	from v_QuoteLookup
	Group By QuoteStatus"
        Dim t As TextBox = txtSQL
        If IsNothing(t) Then
        Else
            txtSQL.Text = query
            Chart1.Series(0).XValueMember = "QuoteStatus"
            Chart1.Series(0).YValueMembers = "QuoteCount"
        End If
        If IsPostBack then
            else
            GetChartTypes()
        End If
        End Sub

        Private Sub GetChartTypes()
        Dim charttype As Integer
        Dim ChartTypes As Array
        Dim ChartTypeNames As Array
        ChartTypes = System.Enum.GetValues(GetType(DataVisualization.Charting.SeriesChartType))
        ChartTypeNames = System.Enum.GetNames(GetType(DataVisualization.Charting.SeriesChartType))
        Dim li As ListItem
        Dim d As DropDownList
        d = New DropDownList
        Dim lic As New ListItemCollection
        For Each charttype In ChartTypes


            li = New ListItem(ChartTypes(0).ToString, charttype)
            lic.Add(li)
            d.Items.Add(li)


        Next

        For Each li In lic
            d = FindControl("DropDownList1")
            ' d.Items.Add(li)
        Next
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        Dim sct As DataVisualization.Charting.SeriesChartType
        Dim ChartTypes As Object = System.Enum.GetValues(GetType(DataVisualization.Charting.SeriesChartType))
        For Each sct In ChartTypes
            If (sct) = DropDownList1.SelectedValue Then
                Exit For
            End If
        Next

        Me.Chart1.Series("Series1").ChartType = sct

    End Sub
    Public Sub Rebind()
        txtSQL.Text = SQL
        Chart1.Series(0).XValueMember = xValueMember
        Chart1.Series(0).YValueMembers = YValueMember
        ObjectDataSource1.DataBind()
    End Sub
End Class