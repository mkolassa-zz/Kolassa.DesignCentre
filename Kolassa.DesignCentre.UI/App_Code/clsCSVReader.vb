Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Public Class CsvRow
    Inherits List(Of String)
    Public Property objTypeCol As Integer
    Public Property objType As String
    Public Property LineText As String
End Class

''' <summary>
''' Class to write data to a CSV file
''' </summary>
Public Class CsvFileWriter
    Inherits StreamWriter

    Public Sub New(ByVal stream As Stream)
        MyBase.New(stream)
    End Sub

    Public Sub New(ByVal filename As String)
        MyBase.New(filename)
    End Sub

    ''' <summary>
    ''' Writes a single row to a CSV file.
    ''' </summary>
    '''  paramname="row">The row to be writtenparamname
    Public Sub WriteRow(ByVal row As CsvRow)
        Dim builder As StringBuilder = New StringBuilder()
        Dim firstColumn = True

        For Each value In row
            ' Add separator if this isn't the first value
            If Not firstColumn Then builder.Append(","c)
            ' Implement special handling for values that contain comma or quote
            ' Enclose in quotes and double up any double quotes
            If value.IndexOfAny(New Char() {""""c, ","c}) <> -1 Then
                builder.AppendFormat("""{0}""", value.Replace("""", """"""))
            Else
                builder.Append(value)
            End If

            firstColumn = False
        Next

        row.LineText = builder.ToString()
        WriteLine(row.LineText)
    End Sub
End Class

''' <summary>
''' Class to read data from a CSV file
''' </summary>
Public Class CsvFileReader
    Inherits StreamReader
    Dim objType As String
    Public Sub New(ByVal stream As Stream)
        MyBase.New(stream)
    End Sub

    Public Sub New(ByVal filename As String)
        MyBase.New(filename)
    End Sub

    ''' <summary>
    ''' Reads a row of data from a CSV file
    ''' </summary>
    ''' paramname "row"> paramname>
    ''' <returns></returns>
    Public Function ReadRow(ByVal row As CsvRow) As Boolean
        row.LineText = ReadLine()
        If String.IsNullOrEmpty(row.LineText) Then Return False
        Dim pos = 0
        Dim rows = 0

        While pos < row.LineText.Length
            Dim value As String

            ' Special handling for quoted field
            If row.LineText(pos) = """"c Then
                ' Skip initial quote
                pos += 1

                ' Parse quoted value
                Dim start = pos

                While pos < row.LineText.Length
                    ' Test for quote character
                    If row.LineText(pos) = """"c Then
                        ' Found one
                        pos += 1

                        ' If two quotes together, keep one
                        ' Otherwise, indicates end of value
                        If pos >= row.LineText.Length OrElse row.LineText(pos) <> """"c Then
                            pos -= 1
                            Exit While
                        End If
                    End If

                    pos += 1
                End While

                value = row.LineText.Substring(start, pos - start)
                value = value.Replace("""""", """")
            Else
                ' Parse unquoted value
                Dim start = pos

                While pos < row.LineText.Length AndAlso row.LineText(pos) <> ","c
                    pos += 1
                End While

                value = row.LineText.Substring(start, pos - start)
            End If

            ' Add field to list
            If rows < row.Count Then
                row(rows) = value
            Else
                row.Add(value)
            End If

            rows += 1

            ' Eat up to and including next comma
            While pos < row.LineText.Length AndAlso row.LineText(pos) <> ","c
                pos += 1
            End While

            If pos < row.LineText.Length Then pos += 1
        End While
        ' Delete any unused items
        While row.Count > rows
            row.RemoveAt(rows)
        End While

        ' Return true if any columns read
        Return row.Count > 0
    End Function
End Class
Public Class clsTestCSV
    Public ObjectType As String
    Public Sub csvWriteTest()
        ' Write sample data to CSV file
        Using writer As CsvFileWriter = New CsvFileWriter("WriteTest.csv")

            For i = 0 To 99
                Dim row As CsvRow = New CsvRow()

                For j = 0 To 4
                    row.Add(String.Format("Column{0}", j))
                Next

                writer.WriteRow(row)
            Next
        End Using
    End Sub

    Public Sub csvReadTest(lsFileName As String)
        Dim clsb As clsBases
        Dim clsObj As clsBase
        Dim formvalues As List(Of KeyValuePair(Of String, String))
        Dim lbUpdate As Boolean = False
        Dim str1, str2 As String
        Dim sHeader(10, 255) As String
        Dim objTypeCol As Integer
        Dim lsObjType As String = ""
        str1 = ""
        str2 = ""
        Dim iRow, iCol As Integer
        iRow = 0
        iCol = 0

        ' Read sample data from CSV file
        Try


            Using reader As CsvFileReader = New CsvFileReader(lsFileName)
                Dim row As CsvRow = New CsvRow()
                objTypeCol = 0
                While reader.ReadRow(row)
                    lsObjType = ""

                    iRow = iRow + 1
                    iCol = 0
                    formvalues = New List(Of KeyValuePair(Of String, String))
                    lbUpdate = False
                    For Each s As String In row
                        iCol = iCol + 1
                        If iRow = 1 Then
                            sHeader(1, iCol) = s
                            If s.ToUpper = "OBJTYPE" Then
                                objTypeCol = iCol
                                lsObjType = ""
                            End If
                        Else
                            If objTypeCol > 0 And iCol = objTypeCol Then lsObjType = s
                            str2 = s
                        End If
                        'System.Diagnostics.Debug.Print(s)
                        str1 = sHeader(1, iCol)
                        System.Diagnostics.Debug.Print(str1 & ": " & s)
                        If str1.ToUpper.Trim = "ID" Then
                            If isGUID(str2) Then
                                lbUpdate = True
                            End If
                        End If
                        formvalues.Add(New KeyValuePair(Of String, String)(str1, str2))
                    Next
                    If iRow > 1 Then '*** Not a header, process row
                        clsb = New clsBases

                        If lsObjType = "" Then
                            clsb.objType = ObjectType
                        Else
                            clsb.objType = lsObjType
                        End If
                        clsObj = clsb.GetObject(clsb.objType)

                        clsObj.FormValue = formvalues
                        clsObj.processFormValues()

                        '     If lbUpdate = True Then
                        '     clsObj.Update()
                        ' Else
                        '     clsObj.Insert()
                        ' End If
                    End If

                End While
            End Using
        Catch ex As Exception
            System.Diagnostics.Debug.Print(ex.Message)
        End Try
    End Sub
    Public Function isGUID(ByVal QFormUID As String) As Boolean
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


