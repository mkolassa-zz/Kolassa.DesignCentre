Imports System.Web.Script.Serialization.JavaScriptSerializer
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
Public Class clsDataConverter
    Public Function ConvertDataTableToJSON(ByVal dt As DataTable) As String
        If String.IsNullOrWhiteSpace(dt.TableName) Then
            dt.TableName = "Dummy"
        End If
        Dim dict As New Dictionary(Of String, Object)
        Dim RowData(dt.Rows.Count - 1) As Object
        For i As Integer = 0 To dt.Rows.Count - 1
            RowData(i) = dt.Rows(i).ItemArray
        Next
        dict.Add(dt.TableName, RowData)
        Dim json As New Web.Script.Serialization.JavaScriptSerializer
        Return json.Serialize(dict)
    End Function
    Public Function ConvertDataSetToJSON(ByVal ds As DataSet) As String
        ' output looks like:
        ' {"datasetname":[{"tablename":[{"fld1":"value1","fld2":"value2"},{"fld1":"value1","fld2":"value2"}]},{"tablename":[{"fld1":"value1","fld2":"value2"},{"fld1":"value1","fld2":"value2"}}]}
        Dim sb As New StringBuilder(String.Format("{{""{0}"":[", ds.DataSetName))
        Dim tblsep As String = String.Empty

        For Each dt In ds.Tables
            sb.Append(tblsep)
            sb.Append(ConvertDataTableToJSON(dt))
            tblsep = ","
        Next
        sb.Append("]}")

        ConvertDataSetToJSON = sb.ToString()
    End Function

    Public Function ConvertDataReaderToJSON(rdr As SqlDataReader) As String
        ' output looks like:
        ' [{"fld1":"value1","fld2":"value2"},{"fld1":"value1","fld2":"value2"}]
        Dim fldCnt As Integer = rdr.FieldCount - 1
        Dim names(fldCnt) As String
        For i As Integer = 0 To fldCnt
            names(i) = rdr.GetName(i)
        Next i
        Dim rowsep As String = "{"
        Dim sb As New StringBuilder("[")

        While rdr.Read()
            Dim fldsep As String = String.Empty
            Dim values(fldCnt) As Object
            rdr.GetValues(values)
            sb.Append(rowsep)

            For i As Integer = 0 To fldCnt
                sb.AppendFormat("{0}""{1}"":""{2}""", fldsep, names(i), values(i))
                fldsep = ","
            Next i
            sb.Append("}")
            rowsep = ",{"
        End While
        sb.Append("]")

        ConvertDataReaderToJSON = sb.ToString()
    End Function
End Class
