﻿Option Strict Off
Imports Microsoft.VisualBasic
Imports System.Data
Imports System
Public Class ReportCategory
    Dim mlCategoryID As Long
	Dim msDescription As String
	Dim msReportName As String
	Dim msCategoryType As String
    Dim msConnectionString As String
    Dim mdlDataLoader As New clsDataLoader
    Dim mbHideReportLists As Boolean
    Dim mcReports As New Collection
    Public Property HideReportLists() As Boolean
        '*** Report Category
        Get
            HideReportLists = mbHideReportLists
        End Get
        Set(ByVal Value As Boolean)
            mbHideReportLists = Value
        End Set
    End Property
    Public Function GetReports() As Collection
        Dim rpt As New Report

        '*** Initialize
        GetReports = New Collection

        Dim ds As Data.DataSet
        Dim dt As Data.DataTable
        Dim dr As Data.DataRow
        '*** Iterate through the dataset and add reports to Collection

        ds = mdlDataLoader.LoadReports(mlCategoryID, msReportName, 0)
        dt = ds.Tables("Reports")

        For Each dr In dt.Rows
            rpt = New Report
            rpt.ReportID = dr("reportID")
            rpt.ReportDescription = dr("reportDescription")
			rpt.ReportName = dr("Reportname")
			rpt.EntityType = dr("EntityType")
            rpt.TableName = If(IsDBNull(dr("tablename")), "", dr("tablename"))
            rpt.SelectStatement = If(IsDBNull(dr("SelectStatement")), "", dr("SelectStatement"))
            mcReports.Add(rpt)
        Next

        ds = Nothing
        dt = Nothing
        dr = Nothing

        GetReports = mcReports
    End Function

    Public Property CategoryID() As Long
        '*** Report Category
        Get
            CategoryID = mlCategoryID
        End Get
        Set(ByVal llValue As Long)
            mlCategoryID = llValue
        End Set
    End Property

    Public Property ConnectionString() As String
        '*** Connection String to get to the data
        Get
            ConnectionString = msConnectionString
        End Get
        Set(ByVal lsValue As String)
            msConnectionString = lsValue
        End Set
    End Property
	Public Property CategoryDescription() As String
		'*** Category Description
		Get
			CategoryDescription = msDescription
		End Get
		Set(ByVal lsValue As String)
			msDescription = lsValue
		End Set
	End Property
	Public Property ReportName() As String
		'*** Category Description
		Get
			ReportName = msReportName
		End Get
		Set(ByVal lsValue As String)
			msReportName = lsValue
		End Set
	End Property
	Public Property CategoryType() As String
        '*** Category Description
        Get
            CategoryType = msCategoryType
        End Get
        Set(ByVal lsValue As String)
            msCategoryType = lsValue
        End Set
    End Property

    Public Overrides Function ToString() As String
        '*** Sets Default Property for the object to The Description
        ToString = msDescription
    End Function

    Public Sub Load()
		'  If msConnectionString = "" Then
		'response.write("No Connection String")
		'Exit Sub
		'End If


	End Sub
End Class

Public Class Report
    Public Message As String
    Dim mlReportID As Long
    Dim msDescription As String
	Dim msReportName As String
    Public ReportType As String
    Public SelectStatement As String
    Dim mlEntityType As Integer
	Dim mbUseFilter As Boolean
    Dim rptControls As ReportControls
	Dim mdlDataLoader As New clsDataLoader
	Public TableName As String
	Public ReadOnly Property Controls() As ReportControls
        Get
            '*** We will get errors if the rptControls Object is Nothing
            '*** So set the control = to an empty ReportControls Object
            If rptControls Is Nothing Then
                '*** No Controls in this report.  Might be a problem
                Dim rptc As ReportControls
                rptc = New ReportControls
                Controls = rptc
            Else

                Controls = rptControls
            End If
        End Get
    End Property
	Public Property ReportID() As Long
		Get
			ReportID = mlReportID
		End Get
		Set(ByVal llValue As Long)
			mlReportID = llValue
		End Set
	End Property
	Public Property EntityType() As Long
		Get
			EntityType = mlENtitytype
		End Get
		Set(ByVal llValue As Long)
			mlentitytype = llValue
			Select Case mlentitytype
				Case 2
					ReportType = "Form Filter"
				Case 3
					ReportType = "Form"
				Case Else
					ReportType = "Report"
			End Select
		End Set
	End Property
	Public Property ReportDescription() As String
        Get
            ReportDescription = msDescription
        End Get
        Set(ByVal lsValue As String)
            msDescription = lsValue
        End Set
    End Property
    Public ReadOnly Property SQL() As String
        Get
            Dim rptctrl As New ReportControl
            Dim lsWhere As String = ""
            ReportName = msReportName
            For Each rptctrl In rptControls
                lsWhere = lsWhere & rptctrl.SQL
            Next
            SQL = lsWhere
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Dim rptctrl As New ReportControl
            Dim lsWhere As String = ""
            ReportName = msReportName
            For Each rptctrl In rptControls
                lsWhere = lsWhere & rptctrl.Description
            Next
            Description = lsWhere
        End Get
    End Property
    Public Property ReportName() As String
        Get
            ReportName = msReportName
        End Get
        Set(ByVal lsValue As String)
            msReportName = lsValue
        End Set
    End Property
    Public Property UseFilter() As Boolean
        Get
            UseFilter = mbUseFilter
        End Get
        Set(ByVal lbValue As Boolean)
            mbUseFilter = lbValue
        End Set
    End Property
    Public Overrides Function ToString() As String
        ToString = msDescription
    End Function
    Public Sub New()
        mbUseFilter = True
    End Sub
	Public Function LoadControls(ByVal cnStr As String) As ReportControls
        Debug.Print("<ReportControl.LoadControls>Load All Controls From Database")
        '*** Load All Controls for this Report
        Dim rptCtrl As ReportControl

		'*** Start New Control Collection
		rptControls = New ReportControls

		'*** Create Dataset and Get the list of Controls
		Dim ds As Data.DataSet = mdlDataLoader.LoadReportControls(mlReportID)
		Dim dt As Data.DataTable = ds.Tables("ReportControls")
		Dim dr As Data.DataRow
		Dim lsMsg As String = ""
		'*** Iterate through the control Records, Create the Control object
		'*** and add the controls to the Collection
		For Each dr In dt.Rows
			rptCtrl = New ReportControl
			rptCtrl.ControlFieldType = CStr(dr("ControlFieldType"))
            rptCtrl.ControlName = If(dr("FieldName") Is DBNull.Value, dr("ControlName"), If(Trim(dr("FieldName")) = "", dr("ControlName"), dr("FieldName")))
            rptCtrl.Description = If(dr("FieldTitle") Is DBNull.Value, dr("ControlDescription"), If(Trim(dr("FieldTitle")) = "", dr("ControlDescription"), dr("FieldTitle")))
            rptCtrl.FieldDescription = If(dr("FieldTitle") Is DBNull.Value, dr("ControlFieldDescription"), If(Trim(dr("FieldTitle")) = "", dr("ControlFieldDescription"), dr("FieldTitle")))
            rptCtrl.FieldName = If(dr("FieldName") Is DBNull.Value, dr("ControlFieldName"), If(Trim(dr("FieldName")) = "", dr("ControlFieldName"), dr("FieldName")))
            rptCtrl.Type = dr("ControlType")
			rptCtrl.ConnectionString = "" 'cnStr
            rptCtrl.Required = dr("Required")

            rptCtrl.ReportID = dr("ReportID")                                    'ID for the Report (Number)
            rptCtrl.ReportControlID = dr("ReportControlID").ToString             'ID for the Control (GUID)
            rptCtrl.ReportControlNumID = dr("ReportControl")                     'ID for the Control (Number)
            rptCtrl.ReportControlFieldID = dr("ReportControlFieldID").ToString   'ID for the Control FIeld Instance (GUID)




            If Not dr("ControlValidationPattern") Is DBNull.Value Then rptCtrl.ValidationPattern = Trim(dr("ControlValidationPattern"))
			If Not dr("FieldValidationPattern") Is DBNull.Value Then rptCtrl.ValidationPattern = Trim(dr("FieldValidationPattern"))
			If Not dr("ControlValidationTitle") Is DBNull.Value Then rptCtrl.ValidationTitle = Trim(dr("ControlValidationTitle"))
			If Not dr("FieldValidationTitle") Is DBNull.Value Then rptCtrl.ValidationTitle = Trim(dr("FieldValidationTitle"))
			If Not dr("FieldReadOnly") Is DBNull.Value Then rptCtrl.Read_Only = Trim(dr("FieldReadOnly"))
			If Not dr("FieldLength") Is DBNull.Value Then
				If dr("FieldLength") > 0 Then rptCtrl.FieldLength = Trim(dr("FieldLength"))
			End If
            'If Not dr("FieldName") Is DBNull.Value Then rptCtrl.FieldName = Trim(dr("FieldName"))
            'If Not dr("TableName") Is DBNull.Value Then rptCtrl.tableName = Trim(dr("TableName"))
            'Debug.Print (rptCtrl.Type & rptCtrl.FieldName)
            If Not IsDBNull(dr("ControlSQL")) Then
				rptCtrl.RowSource = dr("ControlSQL")
			Else
				rptCtrl.RowSource = ""
			End If
			'  Dim liCounter As Integer = 0
			' Dim liCols As Integer = dt.Columns.Count - 1
			'For liCounter = 0 To liCols
			'lsMsg = lsMsg & (dt.Columns(liCounter).ColumnName & dr(liCounter)) & ":      "
			'Next
			'  lsMsg = lsMsg & Chr(13) & Chr(10)

			'*** Load Other Sub Components for each control
			rptCtrl.LoadControlChildren()
			rptCtrl.LoadListItems()

			rptControls.Add(rptCtrl)
		Next
		Debug.Print("Controls Added 'Report.LoadControls'")
		System.Web.HttpContext.Current.Session("rptctrls") = rptControls ' 1/11/18
		Message = lsMsg
        '*** Return the Control Collection
        LoadControls = rptControls
        Debug.Print("</ReportControl.LoadControls>")
    End Function
End Class

Public Class ReportControls
    Inherits System.Collections.CollectionBase
    Dim mdlDataLoader As New clsDataLoader

    Public Sub Add(ByVal ctrl As ReportControl)
        ' Invokes Add method of the List object to add a widget.
        List.Add(ctrl)
    End Sub
    Public Function FindByName(ByVal lsName As String) As ReportControl
        Dim rptctrl As ReportControl
		' response.write("ReportControl-FindByName-FindByName - " & lsName)
		For Each rptctrl In Me.List
            If rptctrl.ControlName = lsName Then
                FindByName = rptctrl
				'   response.write("ReportControl-FindByName-Found Iterator")
				Exit Function
            End If
        Next
        FindByName = New ReportControl
    End Function
    Public Sub Load()
        Dim lctrl As ReportControl
        Dim ds As Data.DataSet = mdlDataLoader.LoadAllControls
        Dim dt As Data.DataTable = ds.Tables("Controls")
        Dim dr As Data.DataRow


        For Each dr In dt.Rows
            lctrl = New ReportControl
            lctrl.Description = dr("ControlDescription")
            lctrl.ControlName = dr("ControlName")
            lctrl.FieldDescription = dr("ControlFieldDescription")
            lctrl.FieldName = dr("ControlFieldName")
            lctrl.SQL = IIf(IsDBNull(dr("ControlSQL")), "", dr("ControlSQL"))
            If lctrl.SQL <> "" Then Stop
            lctrl.Type = IIf(IsDBNull(dr("ControlType")), "", dr("ControlType"))
            lctrl.Parent = IIf(IsDBNull(dr("ControlParent")), "", dr("ControlParent"))
            lctrl.ControlFieldType = IIf(IsDBNull(dr("ControlFieldType")), "", dr("ControlFieldType"))
            lctrl.LoadControlChildren()
            lctrl.LoadListItems()
            Me.Add(lctrl)

            lctrl = Nothing
        Next
        dr = Nothing
        dt = Nothing
        ds = Nothing
    End Sub
End Class

Public Class ReportControl
    Dim mcListItems As ReportListItems
    Dim mcControlChildren As ReportControlChildControls
    Dim msSQL As String
    Dim msDescription As String
    Dim msType As String
    Dim msValue As String = ""
    Dim mcSelectedItems As Collection = New Collection
    Dim mcSelectedItems2 As Collection = New Collection
    Dim moSelectedItem As ReportListItem = New ReportListItem
	Dim msTableName As String
	Dim msFieldName As String
    Dim msFieldDescription As String
    Dim mbEnabled As Boolean = False
    Dim msParent As String
    Dim msControlName As String = ""
    Dim msControlFieldType As String = ""

    Public ReportControlFieldID As String = ""  'Unique Identifer for the Record that Holds the Report/Control/Field combo
    Public ReportControlID As String = ""       'Unique Identifier for the record that holds the Control Definition
    Public ReportID As Long = 0
    Public ReportControlFieldNumID As Long = 0      'Nemeric Field for the ReportControl to Field COmbo
    Public ReportControlNumID As Long = 0       'Numeric Value for Report Control Instance

    Dim msControlConnectionString As String = ""
    Dim msControlRowSource As String = "" '*** SQL used to fill the Control
    Dim msOperator As String = ""
    Dim msDataType As String = ""
	Dim mbRequired As Boolean = False
	Dim mbReadOnly As Boolean = False
	Dim mbFieldLength As Integer
	Protected msValidationPattern As String = ""
	Protected msValidationTitle As String = ""

    Public Sub New()
        'Stop
    End Sub

    Public Sub LoadListItems()
        If mcListItems Is Nothing Then
            mcListItems = New ReportListItems
        End If
        mcListItems.Clear()
        If msControlRowSource = "" Then
            '*** No Items
            Exit Sub
        End If
        mcListItems.ConnectionString = msControlConnectionString
        mcListItems.RowSource = msControlRowSource
        mcListItems.LoadListItems()
    End Sub
    Public Sub LoadControlChildren()
        If mcControlChildren Is Nothing Then
            mcControlChildren = New ReportControlChildControls
        End If
        mcControlChildren.Clear()
        If msControlConnectionString = "" Or msControlRowSource = "" Then
            '*** No Items
            Exit Sub
        End If
        mcControlChildren.ConnectionString = msControlConnectionString
        mcControlChildren.ControlName = msControlName
        mcControlChildren.LoadChildren()
    End Sub
    Public ReadOnly Property ControlChildren() As ReportControlChildControls
        Get
            ControlChildren = mcControlChildren
        End Get
    End Property
    Public Property ListItems() As ReportListItems
        Get
            ListItems = mcListItems
        End Get
        Set(ByVal value As ReportListItems)
            mcListItems = value
        End Set
    End Property
    Public Property ConnectionString() As String
        Get
            ConnectionString = msControlConnectionString
        End Get
        Set(ByVal value As String)
            msControlConnectionString = value
        End Set
    End Property
    Public Property RowSource() As String
        Get
            RowSource = msControlRowSource
        End Get
        Set(ByVal value As String)
            msControlRowSource = value
        End Set
    End Property
    Public Property DataOperator() As String
        Get
            If msOperator Is Nothing Then
                DataOperator = ""
            Else
                DataOperator = msOperator
            End If

        End Get
        Set(ByVal value As String)
            msOperator = value
        End Set
    End Property
    Public Property DataType() As String
        Get
            DataType = msDataType
        End Get
        Set(ByVal value As String)
            msDataType = value
        End Set
    End Property
    Public Property SelectedItems() As Collection
        Get
            SelectedItems = mcSelectedItems
        End Get
        Set(ByVal value As Collection)
            '     If msFieldName = "UnitTypeID" Then Stop
            Debug.Print("<" & msFieldName & " Value=" & value(1).ToString & " />")
            mcSelectedItems = value
            'response.write(value.ToString)
        End Set
    End Property
    Public Property SelectedItems2() As Collection
        Get
            SelectedItems2 = mcSelectedItems2
        End Get
        Set(ByVal value As Collection)
            mcSelectedItems2 = value
        End Set
    End Property
	Public Property ValidationPattern() As String
		Get
			ValidationPattern = msValidationPattern
		End Get
		Set(ByVal value As String)
			msValidationPattern = value
		End Set
	End Property
	Public Property ValidationTitle() As String
		Get
			ValidationTitle = msValidationTitle
		End Get
		Set(ByVal value As String)
			msValidationTitle = value
		End Set
	End Property
	Public Function fGetOperator(ByVal lsOp As String) As String
		Select Case lsOp
			Case ("Equals"), "="
				fGetOperator = "="
			Case ("GreaterThan"), ">"
				fGetOperator = ">"
			Case ("LessThan"), "<"
				fGetOperator = "<"
			Case ("GreaterThanEqual"), ">="
				fGetOperator = ">="
			Case ("LessThanEqual"), "<="
				fGetOperator = "<="
			Case Else
				fGetOperator = "="
		End Select
	End Function
	Public Property FormValue() As List(Of KeyValuePair(Of String, String))

		Get
			Dim llist As New List(Of KeyValuePair(Of String, String))
			Dim lsValue As String = ""
			Dim lsValue2 As String = ""


			'*** Check for Selected Items
			'*** If No Items then Quit
			If mcSelectedItems.Count = 0 Then
				FormValue = New List(Of KeyValuePair(Of String, String))
				Exit Property
			ElseIf msDataType = "" Then
				FormValue = New List(Of KeyValuePair(Of String, String))
				Exit Property
			End If


			'	llist.Add(New KeyValuePair(Of String, String)("", ""))

			'*** Iterate Through all Selected Items
			For Each Me.moSelectedItem In mcSelectedItems
				lsValue = moSelectedItem.Value
				llist.Add(New KeyValuePair(Of String, String)(msFieldName, lsValue))
			Next

			FormValue = llist
		End Get
		Set(ByVal lsValue As List(Of KeyValuePair(Of String, String)))

		End Set
	End Property
	Public Property SQL() As String

		Get

			Dim lsValue As String = ""
			Dim lsValue2 As String = ""
			Dim loSelectedItem2 As ReportListItem
			SQL = ""

			'*** Check for Selected Items
			'*** If No Items then Quit
			If mcSelectedItems.Count = 0 Then
				Exit Property
			ElseIf msDataType = "" Then
				Exit Property
			End If

			'*** If Range, Only Use First Value From each Selected Items List
			If mcSelectedItems2.Count > 0 Then
				loSelectedItem2 = mcSelectedItems2.Item(1)
				lsValue2 = loSelectedItem2.Value
			End If

			SQL = SQL & " ( "
			'*** Iterate Through all Selected Items
			For Each Me.moSelectedItem In mcSelectedItems
				lsValue = moSelectedItem.Value
				FormValue.Add(New KeyValuePair(Of String, String)(msFieldName, lsValue))

                If lsValue <> "" And lsValue <> "--" Then
                    Select Case msControlFieldType
                        Case "DateBox", "Date"
                            If lsValue2 = "" Then
                                SQL = SQL & "[" & msFieldName & "] " & fGetOperator(msOperator) & " #" & CDate(lsValue).ToString & "# Or "
                            Else
                                SQL = SQL & "([" & msFieldName & "] >= #" & CDate(lsValue).ToString & "# And [" & msFieldName & "] <= #" & CDate(lsValue2).ToString & "#) Or "
                            End If

                        Case "Text"
                            If lsValue2 = "" Then
                                SQL = SQL & "[" & msFieldName & "] " & fGetOperator(msOperator) & " '" & lsValue & "' OR "
                            Else
                                SQL = SQL & "([" & msFieldName & "] >= '" & lsValue & "' and [" & msFieldName & "] <= '" & lsValue2 & "') OR "
                            End If
                        Case "Number", "Integer", "Double", "Currency", "Boolean"
                            If lsValue2 = "" Then
                                SQL = SQL & "[" & msFieldName & "] " & fGetOperator(msOperator) & " " & lsValue & " OR "
                            Else
                                SQL = SQL & "([" & msFieldName & "] >= " & lsValue & " and [" & msFieldName & "] <= " & lsValue2 & ") OR "
                            End If

                        Case Else
                            If lsValue2 = "" Then
                                SQL = SQL & "[" & msFieldName & "] " & fGetOperator(msOperator) & " '" & lsValue & "' OR "
                            Else
                                SQL = SQL & "([" & msFieldName & "] >= '" & lsValue & "' and [" & msFieldName & "] <= '" & lsValue2 & "') OR "
                            End If
                    End Select
                End If
            Next
			If SQL.Length > 3 Then
				SQL = SQL.Substring(0, SQL.Length - 3)
			End If
            SQL = SQL & " ) and "
            If SQL.Length < 12 Then
                '*** There is no Selection
                SQL = " (1=1) AND "
            End If
        End Get
		Set(ByVal lsValue As String)
            msSQL = lsValue
        End Set
    End Property
    Public Property Description() As String
        Get
            Dim lsValue As String = ""
            Dim lsValue2 As String = ""

            Description = ""
            If mcSelectedItems.Count = 0 Then
                Exit Property
            End If
            If msDataType = Nothing Then
                Exit Property
            End If
            If mcSelectedItems2.Count > 0 Then
                lsValue2 = CStr(mcSelectedItems2.Item(1).Description)
            End If

            Description = Description & " ( "
            For Each Me.moSelectedItem In mcSelectedItems
                lsValue = moSelectedItem.Description
                If lsValue <> "" Then
                    Select Case msType
                        Case "Date", "Text", "Number", "Integer", "Double", "Currency"
                            If lsValue2 = "" Then
                                Description = Description & msFieldDescription & " " & fGetOperator(msOperator) & " " & lsValue & " or "
                            Else
                                Description = Description & "( " & msFieldDescription & " >= " & lsValue & " and " & " <= " & lsValue2 & " ) or "
                            End If

                        Case Else
                            If lsValue2 = "" Then
                                Description = Description & msFieldDescription & " " & fGetOperator(msOperator) & " " & lsValue & " or "
                            Else
                                Description = Description & "( " & msFieldDescription & " >= " & lsValue & " and " & " <= " & lsValue2 & " ) or "
                            End If
                    End Select
                End If
            Next

            '*** Cut off the last Or
            If Description.Length > 3 Then
                Description = Description.Substring(0, Description.Length - 3)
            End If
            '*** Add the 'and '
            Description = Description & " ) and "
        End Get
        Set(ByVal lsValue As String)
            msDescription = lsValue
        End Set
    End Property
    Public ReadOnly Property Value() As String
        Get
            Value = mcSelectedItems(0).ToString
        End Get

    End Property
	Public Property Type() As String
		Get
			Type = msType
		End Get
		Set(ByVal lsValue As String)
			msType = lsValue
		End Set
	End Property
	Public Property TableName() As String
		Get
			TableName = msTableName
		End Get
		Set(ByVal lsValue As String)
			msTableName = lsValue
		End Set
	End Property
	Public Property FieldName() As String
        Get
            FieldName = msFieldName
        End Get
        Set(ByVal lsValue As String)
            msFieldName = lsValue
        End Set
    End Property
    Public Property FieldDescription() As String
        Get
            FieldDescription = msFieldDescription
        End Get
        Set(ByVal lsValue As String)
            msFieldDescription = lsValue
        End Set
    End Property
    Public Property Parent() As String
        Get
            Parent = msParent
        End Get
        Set(ByVal lsValue As String)
            msParent = lsValue
        End Set
    End Property
    Public Property ControlName() As String
        Get
            ControlName = msControlName
        End Get
        Set(ByVal lsValue As String)
            msControlName = lsValue
        End Set
    End Property
    Public Property ControlFieldType() As String
        Get
            ControlFieldType = msControlFieldType
        End Get
        Set(ByVal lsValue As String)
            msControlFieldType = lsValue
        End Set
    End Property

    Public Property Enabled() As Boolean
        Get
            Enabled = mbEnabled
        End Get
        Set(ByVal lbValue As Boolean)
            mbEnabled = lbValue
        End Set
    End Property
    Public Property Required() As Boolean
        Get
            Required = mbRequired
        End Get
        Set(ByVal lbValue As Boolean)
            mbRequired = lbValue
        End Set
    End Property
	Public Property Read_Only() As Boolean
		Get
			Read_Only = mbReadOnly
		End Get
		Set(ByVal lbValue As Boolean)
			mbReadOnly = lbValue
		End Set
	End Property
	Public Property FieldLength() As Integer
		Get
			FieldLength = mbFieldLength
		End Get
		Set(ByVal lbValue As Integer)
			mbFieldLength = lbValue
		End Set
	End Property
End Class

Public Class ReportListItems
    Inherits System.Collections.CollectionBase
    Dim mdlDataLoader As New clsDataLoader
    Dim msRowSource As String = ""
    Dim msConnectionString As String = ""

    Public Property RowSource() As String
        Get
            RowSource = msRowSource
        End Get
        Set(ByVal value As String)
            msRowSource = value
        End Set
    End Property
    Public Property ConnectionString() As String
        Get
            ConnectionString = msConnectionString
        End Get
        Set(ByVal value As String)
            msConnectionString = value
        End Set
    End Property
    Public Sub Add(ByVal ctrl As ReportListItem)
        ' Invokes Add method of the List object to add a widget.
        List.Add(ctrl)
    End Sub
    Public Sub LoadListItems()
        Dim liListItem As ReportListItem
        Dim lsSQL As String = msRowSource
        Dim lsDesc As String = ""
        Dim lsValue As String = ""
        Me.Clear()

        '*** If there is no Connection String or Row Source, Exit out
        If msRowSource = "" Then
            Exit Sub
        End If

        '*** Open Connection and Recordset Objects
        Dim ds As Data.DataSet = mdlDataLoader.LoadListItems(msConnectionString, lsSQL)
        Dim dt As Data.DataTable = ds.Tables("ListItems")
        Dim dr As Data.DataRow
        Dim lsDescription As String

        '*** Check to see if the Recordset actually has records
        If dt Is Nothing Then Exit Sub
        For Each dr In dt.Rows
            liListItem = New ReportListItem
            If IsDBNull(dr(1)) Then
                lsDescription = ""
            Else
                lsDescription = dr(1)
            End If
            lsDesc = lsDescription
            lsValue = dr(0).ToString
            liListItem.Description = lsDesc
            liListItem.Value = dr(0).ToString
            Me.Add(liListItem)
            liListItem = Nothing
        Next

        '*** Perform Garbage Collection
        dr = Nothing
        dt = Nothing
        ds = Nothing

    End Sub

End Class

Public Class ReportListItem
    Dim msDescription As String
    Dim msValue As String = ""

    Public Property Description() As String
        Get
            Description = CStr(msDescription)
        End Get
        Set(ByVal lsValue As String)
            msDescription = lsValue
        End Set
    End Property

    Public Property Value() As String
        Get
            Value = CStr(msValue)
        End Get
        Set(ByVal lsValue As String)
            msValue = lsValue
        End Set
    End Property

    Public Overrides Function ToString() As String
        ToString = msDescription
    End Function
End Class

Public Class ReportControlChildControls
    Inherits System.Collections.CollectionBase
    Dim msControlName As String = ""
    Dim msConnectionString As String = ""

    Public Property ControlName() As String
        Get
            ControlName = msControlName
        End Get
        Set(ByVal value As String)
            msControlName = value
        End Set
    End Property
    Public Property ConnectionString() As String
        Get
            ConnectionString = msConnectionString
        End Get
        Set(ByVal value As String)
            msConnectionString = value
        End Set
    End Property
    Public Sub Add(ByVal ctrl As ReportControlChildControl)
        ' Invokes Add method of the List object to add a widget.
        List.Add(ctrl)
    End Sub
    Public Sub LoadChildren()
        Dim mdlDataLoader As New clsDataLoader
        Dim liChild As ReportControlChildControl
        Dim lsSQL As String = "Select ControlName From tblReportControls Where ControlParent ='" & msControlName & "'"

        Me.Clear()

        '*** If there is no Connection String or Row Source, Exit out
        If msConnectionString = "" Or msControlName = "" Then
            Exit Sub
        End If

        '*** Open Connection and Recordset Objects
        Dim ds As Data.DataSet = mdlDataLoader.LoadChildren(msConnectionString, lsSQL)
        Dim dt As Data.DataTable = ds.Tables("Children")
        Dim dr As Data.DataRow


        '*** Iterate through Records Adding
        For Each dr In dt.Rows
            liChild = New ReportControlChildControl
            liChild.Description = dr(0)
			liChild.Value = dr(0)

			Me.Add(liChild)
            liChild = Nothing
        Next

        '*** Perform Garbage Collection
        dr = Nothing
        dt = Nothing
        ds = Nothing

    End Sub

End Class

Public Class ReportControlChildControl
    Dim msDescription As String
    Dim msValue As String = ""

    Public Property Description() As String
        Get
            Description = CStr(msDescription)
        End Get
        Set(ByVal lsValue As String)
            msDescription = lsValue
        End Set
    End Property

    Public Property Value() As String
        Get
            Value = CStr(msValue)
        End Get
        Set(ByVal lsValue As String)
			msValue = lsValue
		End Set
    End Property

    Public Overrides Function ToString() As String
        ToString = msDescription
    End Function
End Class



