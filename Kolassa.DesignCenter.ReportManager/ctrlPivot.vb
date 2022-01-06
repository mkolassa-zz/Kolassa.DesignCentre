
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data

<DefaultProperty("Text"), ToolboxData("<{0}:ctrlBase_ runat=server></{0}:ctrlBase>"), ToolboxBitmap(GetType(Calendar))>
Public Class ctrlPivot
    Inherits CompositeControl

    Dim Div1, Div2 As Panel



    Public Function DataTableForTesting() As DataTable

        Dim dt As DataTable = New DataTable("Sales Table")
        dt.Columns.Add("Sales Person")
        dt.Columns.Add("Product")
        dt.Columns.Add("Quantity")
        dt.Columns.Add("Sale Amount")
        dt.Rows.Add(New Object() {"John", "Pens", 200, 350.0})
        dt.Rows.Add(New Object() {"John", "Pencils", 400, 500.0})
        dt.Rows.Add(New Object() {"John", "Notebooks", 100, 300.0})
        dt.Rows.Add(New Object() {"John", "Rulers", 50, 100.0})
        dt.Rows.Add(New Object() {"John", "Calculators", 120, 1200.0})
        dt.Rows.Add(New Object() {"John", "Back Packs", 75, 1500.0})
        dt.Rows.Add(New Object() {"Jane", "Pens", 225, 393.75})
        dt.Rows.Add(New Object() {"Jane", "Pencils", 335, 418.75})
        dt.Rows.Add(New Object() {"Jane", "Notebooks", 200, 600.0})
        dt.Rows.Add(New Object() {"Jane", "Rulers", 75, 150.0})
        dt.Rows.Add(New Object() {"Jane", "Calculators", 80, 800.0})
        dt.Rows.Add(New Object() {"Jane", "Back Packs", 97, 1940.0})
        dt.Rows.Add(New Object() {"Sally", "Pens", 202, 353.5})
        dt.Rows.Add(New Object() {"Sally", "Pencils", 303, 378.75})
        dt.Rows.Add(New Object() {"Sally", "Notebooks", 198, 600.0})
        dt.Rows.Add(New Object() {"Sally", "Rulers", 98, 594.0})
        dt.Rows.Add(New Object() {"Sally", "Calculators", 80, 800.0})
        dt.Rows.Add(New Object() {"Sally", "Back Packs", 101, 2020.0})
        dt.Rows.Add(New Object() {"Sarah", "Pens", 112, 196.0})
        dt.Rows.Add(New Object() {"Sarah", "Pencils", 245, 306.25})
        dt.Rows.Add(New Object() {"Sarah", "Notebooks", 198, 594.0})
        dt.Rows.Add(New Object() {"Sarah", "Rulers", 50, 100.0})
        dt.Rows.Add(New Object() {"Sarah", "Calculators", 66, 660.0})
        dt.Rows.Add(New Object() {"Sarah", "Back Packs", 50, 2020.0})
        Return dt
    End Function



    Protected Sub CreateChildControls()

        '/Advanced Pivot 
        Dim advPivot As pivot = New pivot(DataTableForTesting)
        Dim advancedPivotHTML As HtmlTable = advPivot.PivotTable("Sales Person", "Product", New String() {"Sale Amount", "Quantity"})
        Div1.Controls.Add(advancedPivotHTML)


        '/Simple Pivot 
        Dim SimplePivot As pivot = New pivot(DataTableForTesting)
        '//override default style with css 
        SimplePivot.CssTopHeading = "Heading"
        SimplePivot.CssLeftColumn = "LeftColumn"
        SimplePivot.CssItems = "Items"
        SimplePivot.CssTotals = "Totals"
        SimplePivot.CssTable = "Table"

        Dim simplePivotHTML As HtmlTable = advPivot.PivotTable("Product", "Sales Person", New String() {"Sale Amount"})
        Div2.Controls.Add(simplePivotHTML)


    End Sub




    Protected Sub Render(writer As HtmlTextWriter)
        RenderSubControls(writer)
    End Sub
    Protected Overridable Sub RenderSubControls(writer As HtmlTextWriter)
        '	lblTitle2.RenderControl(writer)
    End Sub
End Class




Public Class pivot
    Dim _datatable As DataTable
    Public CssTopHeading As String
    Public CssSubHeading As String
    Public CssLeftColumn As String
    Public CssItems As String
    Public CssTotals As String
    Public CssTable As String
    Dim Div1, Div2 As Panel

    Public Sub totalCellStyle(cell As HtmlTableCell)

        End Sub
        Public Sub TableStyle(table As HtmlTable)

        End Sub
        Public Sub subHeaderCellStyle(cell As HtmlTableCell)

        End Sub
        Public Sub MainHeaderLeftCellStyle(cell As HtmlTableCell)

        End Sub
        Public Sub ItemCellStyle(cell As HtmlTableCell)

        End Sub
    Public Sub New(DataTable As DataTable)

        '	Init()
        _datatable = DataTable
    End Sub

    Private Function FindValue(xAxisField As String, xAxisValue As String, yAxisField As String, yAxisValue As String, zAxisField As String) As String
            FindValue = "" 'Initial Value
            Dim zAxisValue As String = ""
            Try

                For Each row As DataRow In _datatable.Rows
                    If (Convert.ToString(row(xAxisField)) = xAxisValue And Convert.ToString(row(yAxisField)) = yAxisValue) Then
                        zAxisValue = Convert.ToString(row(zAxisField))
                        Exit For
                    End If
                Next
            Catch
                Throw
                Return zAxisValue
            End Try
        End Function
        Private Function FindValues(xAxisField As String, xAxisValue As String, yAxisField As String, yAxisValue As String, zAxisFields As String()) As String()
            Dim zAxis As Integer = zAxisFields.Length
            If (zAxis < 1) Then
                zAxis = zAxis + 1
            End If
            Dim zAxisValues(zAxis) As String
            '//set default values 
            For i As Integer = 0 To zAxisValues.GetUpperBound(0)
                zAxisValues(i) = "0"
            Next
            Try

                For Each row As DataRow In _datatable.Rows
                    If (Convert.ToString(row(xAxisField)) = xAxisValue And Convert.ToString(row(yAxisField)) = yAxisValue) Then
                        For z As Integer = 0 To zAxis
                            zAxisValues(z) = Convert.ToString(row(zAxisFields(z)))
                            Exit For
                        Next
                    End If
                Next
            Catch
                Throw
            End Try
            Return zAxisValues
        End Function

        Private Sub MainHeaderTopCellStyle(cell As HtmlTableCell)
        If (CssTopHeading = "") Then
            cell.Style.Add("font-family", "tahoma")
            cell.Style.Add("font-size", "10pt")
            cell.Style.Add("font-weight", "normal")
            cell.Style.Add("background-color", "black")
            cell.Style.Add("color", "white")
            cell.Style.Add("text-align", "center")
        Else
            cell.Attributes.Add("Class", CssTopHeading)
        End If
        End Sub


        '/// <summary> 
        '/// Creates an advanced 3D Pivot table. 
        '/// </summary> 
        '/// <param name="xAxisField">The main heading at the top of the report.</param> 
        '/// <param name="yAxisField">The heading on the left of the report.</param> 
        '/// <param name="zAxisFields">The sub heading at the top of the report.</param> 
        '/// <returns>HtmlTable Control.</returns> 
        Public Function PivotTable(xAxisField As String, yAxisField As String, zAxisFields As String()) As HtmlTable
            Dim Table As HtmlTable = New HtmlTable()
            '//style table 
            TableStyle(Table)
            '/* 
            '* The x-axis Is the main horizontal row. 
            '* The z-axis Is the sub horizontal row. 
            '* The y-axis Is the left vertical column. 
            '*/ 
            Try

                '//get distinct xAxisFields 
                Dim xAxis As ArrayList = New ArrayList()
                Dim row As DataRow
                For Each row In _datatable.Rows
                    If Not (xAxis.Contains(row(xAxisField))) Then
                        xAxis.Add(row(xAxisField))
                    End If
                Next

                '//get distinct yAxisFields 
                Dim yAxis As ArrayList = New ArrayList()
                ' Dim row As DataRow
                For Each row In _datatable.Rows
                    If Not (yAxis.Contains(row(yAxisField))) Then
                        yAxis.Add(row(yAxisField))
                    End If
                Next
                '//create a 2D array for the y-axis/z-axis fields 
                Dim zAxis As Integer = zAxisFields.Length
                If (zAxis < 1) Then zAxis = 1
                Dim matrix((xAxis.Count * zAxis), yAxis.Count) As String
                Dim zAxisValues(zAxis) As String
                For y As Integer = 0 To yAxis.Count '//loop thru y-axis fields 
                    '//rows 
                    For x As Integer = 0 To xAxis.Count ' //loop thru x-axis fields 
                        '//main columns 
                        '//get the z-axis values 
                        zAxisValues = FindValues(xAxisField, Convert.ToString(xAxis(x)), yAxisField, Convert.ToString(yAxis(y)), zAxisFields)
                        For z As Integer = 0 To zAxis ' //loop thru z-axis fields 
                            '//sub columns 
                            matrix((((x + 1) * zAxis - zAxis) + z), y) = zAxisValues(z)
                        Next
                    Next
                Next
                '//calculate totals for the y-axis 
                Dim yTotals(xAxis.Count * zAxis) As Decimal
                For col As Integer = 0 To (xAxis.Count * zAxis)
                    yTotals(col) = 0
                    For irow As Integer = 0 To yAxis.Count
                        yTotals(col) += Convert.ToDecimal(matrix(col, irow))
                    Next
                Next
                '//calculate totals for the x-axis 
                Dim xTotals(zAxis, (yAxis.Count + 1)) As Decimal
                For y As Integer = 0 To yAxis.Count ' //loop thru the y-axis 
                    Dim zCount As Integer = 0
                    For z As Integer = 0 To (zAxis * xAxis.Count) ' //loop thru the z-axis 
                        xTotals(zCount, y) += Convert.ToDecimal(matrix(z, y))
                        If zCount = (zAxis - 1) Then
                            zCount = 0
                        Else
                            zCount = zCount + 1
                        End If
                    Next
                Next
                For xx As Integer = 0 To zAxis ' //Grand Total 
                    For xy As Integer = 0 To yAxis.Count
                        xTotals(xx, yAxis.Count) += xTotals(xx, xy)
                    Next
                Next

                '//Build HTML Table 
                '//Append main row (x-axis) 
                Dim mainRow As HtmlTableRow = New HtmlTableRow()
                mainRow.Cells.Add(New HtmlTableCell())

                For x As Integer = 0 To xAxis.Count ' //loop thru x-axis + 1 
                    Dim cell As HtmlTableCell = New HtmlTableCell()
                    cell.ColSpan = zAxis
                    If (x < xAxis.Count) Then
                        cell.InnerText = Convert.ToString(xAxis(x))
                    Else
                        cell.InnerText = "Grand Totals"
                    End If
                    '//style cell 
                    MainHeaderTopCellStyle(cell)
                    mainRow.Cells.Add(cell)
                Next

                Table.Rows.Add(mainRow)
                ' //Append sub row (z-axis) 
                Dim subRow As HtmlTableRow = New HtmlTableRow()
                subRow.Cells.Add(New HtmlTableCell())
                subRow.Cells(0).InnerText = yAxisField
                '//style cell 
                subHeaderCellStyle(subRow.Cells(0))
                For x As Integer = 0 To xAxis.Count ' //loop thru x-axis + 1 
                    For z As Integer = 0 To zAxis
                        Dim cell As HtmlTableCell = New HtmlTableCell()
                        cell.InnerText = zAxisFields(z)
                        '//style cell 
                        subHeaderCellStyle(cell)
                        subRow.Cells.Add(cell)
                    Next
                Next
                Table.Rows.Add(subRow)
                '//Append table items from matrix 
                For y As Integer = 0 To yAxis.Count ' //loop thru y-axis 
                    Dim itemRow As HtmlTableRow = New HtmlTableRow()
                    For z As Integer = 0 To (zAxis * xAxis.Count) '//loop thru z-axis + 1 
                        Dim cell As HtmlTableCell = New HtmlTableCell()
                        If (z = 0) Then
                            cell.InnerText = Convert.ToString(yAxis(y))
                            '//style cell 
                            MainHeaderLeftCellStyle(cell)
                        Else
                            cell.InnerText = Convert.ToString(matrix((z - 1), y))
                            '//style cell 
                            ItemCellStyle(cell)
                        End If
                        itemRow.Cells.Add(cell)
                    Next
                    '//append x-axis grand totals 
                    For z As Integer = 0 To zAxis
                        Dim cell As HtmlTableCell = New HtmlTableCell()
                        cell.InnerText = Convert.ToString(xTotals(z, y))
                        '//style cell 
                        totalCellStyle(cell)
                        itemRow.Cells.Add(cell)
                    Next
                    Table.Rows.Add(itemRow)
                Next
                ' //append y-axis totals 
                Dim totalRow As HtmlTableRow = New HtmlTableRow()
                For x As Integer = 0 To (zAxis * xAxis.Count)

                    Dim cell As HtmlTableCell = New HtmlTableCell()
                    If (x = 0) Then
                        cell.InnerText = "Totals"
                    Else
                        cell.InnerText = Convert.ToString(yTotals(x - 1))
                    End If
                    '//style cell 
                    totalCellStyle(cell)
                    totalRow.Cells.Add(cell)
                Next
                '//append x-axis/y-axis totals 
                For z As Integer = 0 To zAxis
                    Dim cell As HtmlTableCell = New HtmlTableCell()
                    cell.InnerText = Convert.ToString(xTotals(z, xTotals.GetUpperBound(1)))
                    '//style cell 
                    totalCellStyle(cell)
                    totalRow.Cells.Add(cell)
                Next
                Table.Rows.Add(totalRow)
            Catch
                Throw
            End Try
            Return Table
        End Function
    End Class