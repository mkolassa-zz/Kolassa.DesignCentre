Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Specialized
Imports System.Data.SqlClient

Partial Class ReportManager_ctrlDynamicDataGrid
    Inherits System.Web.UI.UserControl
#Region "Data members"
    Dim msDS As DataSet
    Public table As DataTable = New DataTable()
    Dim ParameterArray As ArrayList = New ArrayList()

    Public Property GridDataSet() As DataSet
        Get
            GridDataSet = Session("msDS")
        End Get
        Set(ByVal value As DataSet)
            Session("msDS") = value
        End Set
    End Property
#End Region

#Region "Events Handlers"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If IsPostBack Then
            If Session("msDS") Is Nothing Then
                ' GetDataSet(TableGridView.DataSource)
            Else
                '   GetDataSet(Session("msDS")) '  
                CreateTemplatedGridView()
                End If
            End If

    End Sub

    Public Sub TableGridView_RowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs) Handles TableGridView.RowEditing
        TableGridView.EditIndex = e.NewEditIndex
        TableGridView.DataBind()
        Session("SelecetdRowIndex") = e.NewEditIndex
        TableGridView.EditIndex = e.NewEditIndex
    End Sub
    Public Sub TableGridView_RowCancelingEdit(ByVal sender As Object, ByVal e As GridViewCancelEditEventArgs) Handles TableGridView.RowCancelingEdit
        TableGridView.EditIndex = -1
        TableGridView.DataBind()
        Session("SelecetdRowIndex") = -1
    End Sub

    Protected Sub TableGridView_RowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)

        Dim ServerName As String = Session("Server")
        Dim UserName As String = Session("UserName")
        Dim Password As String = Session("Password")
        Dim DatabaseName As String = Session("DatabaseSelected")

        Dim Connection As SqlConnection = New System.Data.SqlClient.SqlConnection("Data Source=" + ServerName + ";Initial Catalog=" + DatabaseName + ";User ID=" + UserName + ";Password=" + Password + ";Integrated Security=True; Connect Timeout=120")
        Dim Query As String = GenerateDeleteQuery(e.RowIndex)
        Dim command As SqlCommand = New System.Data.SqlClient.SqlCommand(Query, Connection)
        Try
            If Connection.State = ConnectionState.Closed Then
                Connection.Open()
                command.ExecuteNonQuery()
            End If
        Catch se As SqlException

            msg_lbl.Text = se.ToString()
            MsgPanel.Visible = True
            Connection.Close()

        End Try
        CreateTemplatedGridView()
    End Sub

    Public Sub TableGridView_RowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)

    
        Dim i As Integer
        Dim row As GridViewRow = TableGridView.Rows(e.RowIndex)
        For i = 0 To table.Columns.Count
            Dim field_value As String = row.FindControl(table.Columns(i).ColumnName).ToString
            ParameterArray.Add(field_value)
        Next
        Dim Query As String = ""
        If Session("InsertFlag") = 1 Then
            Query = GenerateInsertQuery()
        Else
            Query = GenerateUpdateQuery()
        End If
        '       Dim command As SqlCommand = New System.Data.SqlClient.SqlCommand(Query, connection)

        Try
            '   If connection.State = ConnectionState.Closed Then
            'connection.Open()
            'command.ExecuteNonQuery()
            Session("InsertFlag") = If(Session("InsertFlag") = 1, 0, 1)
            'End If
        Catch se As SqlException
            msg_lbl.Text = se.ToString()
            MsgPanel.Visible = True
            'connection.Close()
        End Try
        TableGridView.EditIndex = -1
        CreateTemplatedGridView()
    End Sub


    Protected Sub TableGridView_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        ' //CreateTemplatedGridView();
        TableGridView.PageIndex = e.NewPageIndex
        TableGridView.DataBind()
    End Sub

    Protected Sub msg_button_Click(ByVal sender As Object, ByVal e As EventArgs)
        MsgPanel.Visible = False
    End Sub



#End Region

#Region "Methods"
    Sub GetDataSet(ByVal ds As DataSet)

        If ds Is Nothing Then
            ds = GridDataSet
        Else
            GridDataSet = ds
        End If
        If ds Is Nothing Then Exit Sub
        Dim dt As DataTable

        dt = ds.Tables(0)

        '*** clear any existing columns
        TableGridView.Columns.Clear()
        Dim dr As DataControlRowType
        Dim colName As String
        Dim lsTypeName As String
        Dim BtnTmpField As TemplateField = New TemplateField()
        BtnTmpField.ItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Item, "...", "Command")
        BtnTmpField.HeaderTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Header, "...", "Command")
        BtnTmpField.EditItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.EditItem, "...", "Command")
        TableGridView.Columns.Add(BtnTmpField)
        '*** walk the DataTable and add columns to the GridView
        Dim i As Integer
        For i = 0 To dt.Columns.Count - 1

            Dim tf As TemplateField = New TemplateField()

            '*** create the data rows
            'On Error Resume Next
            dr = DataControlRowType.DataRow
            colName = dt.Columns(i).ColumnName
            lsTypeName = dt.Columns(i).DataType.Name
            'On Error GoTo 0
            tf.ItemTemplate = New DynamicallyTemplatedGridViewHandler(dr, colName, lsTypeName)
            tf.EditItemTemplate = New DynamicallyTemplatedGridViewHandler(dr, colName, lsTypeName)
            '*** create the header
            tf.HeaderTemplate = New DynamicallyTemplatedGridViewHandler(DataControlRowType.Header, dt.Columns(i).ColumnName, dt.Columns(i).DataType.Name)
            '*** add to the GridView
            TableGridView.Columns.Add(tf)
        Next

        '*** bind and display the data
        System.Diagnostics.Debug.WriteLine("Edit Index" & TableGridView.EditIndex)
        ' TableGridView.EditIndex = 0
        TableGridView.DataSource = dt
        TableGridView.DataBind()
        System.Diagnostics.Debug.WriteLine("Edit Index" & TableGridView.EditIndex)
        TableGridView.Visible = True
    End Sub
 

    Sub CreateTemplatedGridView()

        '  // fill the table which is to bound to the GridView
        '   PopulateDataTable()
        Dim table As DataTable = GridDataSet.Tables(0) 'New DataTable
        'table.Columns.Add("Dude")
        'table.Rows.Add("Mike")
        'table.Rows.Add("Bob")
        '  // add templated fields to the GridView
        Dim BtnTmpField As TemplateField = New TemplateField()
        BtnTmpField.ItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Item, "...", "Command")
        BtnTmpField.HeaderTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Header, "...", "Command")
        BtnTmpField.EditItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.EditItem, "...", "Command")
        TableGridView.Columns.Add(BtnTmpField)

        For i = 0 To table.Columns.Count - 1
            Dim ItemTmpField As TemplateField = New TemplateField()
            '  // create HeaderTemplate
            ItemTmpField.HeaderTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Header, _
                                                          table.Columns(i).ColumnName, _
                                                          table.Columns(i).DataType.Name)
            '    // create ItemTemplate
            ItemTmpField.ItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.Item, _
                                                          table.Columns(i).ColumnName, _
                                                          table.Columns(i).DataType.Name)
            '  //create EditItemTemplate
            ItemTmpField.EditItemTemplate = New DynamicallyTemplatedGridViewHandler(ListItemType.EditItem, _
                                                          table.Columns(i).ColumnName, _
                                                          table.Columns(i).DataType.Name)
            '  // then add to the GridView
            TableGridView.Columns.Add(ItemTmpField)
        Next

        '// bind and display the data
        TableGridView.DataSource = table
        TableGridView.DataBind()
    End Sub

    Function GenerateUpdateQuery() As String
        Dim i As Integer = 0
        Dim tempstr As String = ""
        Dim temp_index As Integer = -1

        Dim TableName As String = Session("TableSelected")
        Dim Query As String = ""
        Query = "Update  " + TableName + " set "

        For i = 0 To table.Columns.Count
            Select Case (table.Columns(i).DataType.Name)
                Case "Boolean"
                Case "Int32"
                Case "Byte"
                Case "Decimal"
                    If (ParameterArray(i) = "True") Then
                        ParameterArray(i) = "1"
                    ElseIf (ParameterArray(i) = "False") Then
                        ParameterArray(i) = "0"
                    End If
                    If (i = table.Columns.Count - 1) Then
                        Query = Query + table.Columns(i).ColumnName + "=" + ParameterArray(i)
                    Else
                        Query = Query + table.Columns(i).ColumnName + "=" + ParameterArray(i) + ", "
                    End If
                Case "String"
                Case "DateTime"
                    If ((ParameterArray(i)).Contains("'")) Then
                        tempstr = (ParameterArray(i))
                        ParameterArray(i) = (ParameterArray(i)).Replace("'", "''")
                        temp_index = i
                    End If

                    If i = table.Columns.Count - 1 Then
                        Query = Query + table.Columns(i).ColumnName + "='" + ParameterArray(i) + "' "
                    Else
                        Query = Query + table.Columns(i).ColumnName + "='" + ParameterArray(i) + "', "
                    End If
            End Select
        Next
        If (temp_index > -1) Then
            ParameterArray(temp_index) = tempstr
        End If
        If (table.Columns(0).DataType.Name = "String" Or table.Columns(0).DataType.Name = "DateTime") Then
            Query = Query + " where " + table.Columns(0).ColumnName + " = '" + ParameterArray(0) + "'"
        Else
            Query = Query + " where " + table.Columns(0).ColumnName + " = " + ParameterArray(0)


        End If
        GenerateUpdateQuery = Query

    End Function

    Function GenerateInsertQuery() As String
        Dim i As Integer = 0
        Dim tempstr As String = ""
        Dim temp_index As Integer = -1

        Dim TableName As String = Session("TableSelected")
        Dim Query As String = ""
        Query = "Insert into  " + TableName + "("

        For i = 0 To table.Columns.Count
            If (i = table.Columns.Count - 1) Then
                Query = Query + table.Columns(i).ColumnName
            Else
                Query = Query + table.Columns(i).ColumnName + ", "
            End If
        Next

        Query = Query + ")" + "Values ("
        For i = 0 To table.Columns.Count
            Select Case (table.Columns(i).DataType.Name)
                Case "Boolean"
                Case "Int32"
                Case "Byte"
                Case "Decimal"
                    If (ParameterArray(i) = "True") Then
                        ParameterArray(i) = "1"
                    ElseIf (ParameterArray(i) = "False") Then
                        ParameterArray(i) = "0"
                    End If

                    If (i = table.Columns.Count - 1) Then
                        Query = Query + ParameterArray(i)
                    Else
                        Query = Query + ParameterArray(i) + ", "
                    End If
                Case "String"
                Case "DateTime"
                    If ((ParameterArray(i)).Contains("'")) Then
                        tempstr = (ParameterArray(i))
                        ParameterArray(i) = (ParameterArray(i)).Replace("'", "''")
                        temp_index = i
                    End If
                    If (i = table.Columns.Count - 1) Then
                        Query = Query + "'" + ParameterArray(i) + "' "
                    Else
                        Query = Query + "'" + ParameterArray(i) + "', "
                    End If

            End Select
        Next
        Query = Query + ")"
        GenerateInsertQuery = Query
    End Function


    Function GenerateDeleteQuery(ByVal index As Integer) As String
        Dim TableName As String = Session("TableSelected")
        Dim query As String = ""
        If (table.Columns(0).DataType.Name = "String" Or table.Columns(0).DataType.Name = "DateTime") Then
            query = "Delete from " + TableName + " where " + table.Columns(0).ColumnName + "='" + table.Rows(index)(0).ToString() + "'"
        Else
            query = "Delete from " + TableName + " where " + table.Columns(0).ColumnName + "=" + table.Rows(index)(0).ToString()

        End If
        GenerateDeleteQuery = query
    End Function
#End Region

End Class
