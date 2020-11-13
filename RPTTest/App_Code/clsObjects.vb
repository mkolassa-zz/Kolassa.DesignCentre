Imports System.Data
Imports Kolassa.DesignCentre.Data
Public Class clsFormObjects
	Public Dude As String

	Public Sub New()

	End Sub
End Class
Public Class clsContact

	Public Property ID As String
	Public Property ParentID As String
	Public Property FirstName As String
	Public Property LastName As String
	Public Property FullAddress As String
	Public Property City As String
	Public Property StateProvince As String
	Public Property PostalCode As String
	Public Property Country As String
	Public Property Phone2 As String
	Public Property Phone1 As String
	Public Property Email1 As String
	Public Property Email2 As String
	Public Property NodeID As Integer = 0
	Public Property Type As String
	Public Property Active As Boolean
	Public Property CreateDate As Date
	Public Property createUser As String
	Public Property UpdateDate As Date
	Public Property UpdateUser As String
	Public Property UpdateUserName As String
	Public Property CreateUserName As String
	Public Property ContactType As String
	Public Property llNodeID As Long
	Public Property ImageURL As String = "images/people-jerks-shovel.gif"

	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
End Class
Public Class clsContacts
	Public Function GetContacts(ID As String, lsObjectID As String, NodeID As Integer, Active As Boolean) As DataSet

		Dim c As New clsSelectDataLoader
		GetContacts = c.LoadContacts(NodeID, "", Active, ID, lsObjectID)
	End Function
	Public Sub DeleteContacts(obj As clsContact)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteContacts(obj.ID)
    End Sub
    Public Sub InsertContacts(obj As clsContact)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.InsertContacts(obj.llNodeID, obj.FirstName, obj.LastName, obj.ParentID, obj.FullAddress, obj.City, obj.StateProvince, obj.PostalCode,
                         obj.Country, obj.Phone1, obj.Phone2, obj.Email1, obj.Email2, obj.ContactType)
    End Sub
    Public Sub UpdateContacts(obj As clsContact)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateContacts(obj.ID, obj.ParentID, obj.FirstName, obj.LastName, obj.FullAddress, obj.City, obj.StateProvince, obj.PostalCode, obj.Country, obj.Phone2, obj.Phone1, obj.Email1, obj.Email2, obj.NodeID,
                         obj.Type, obj.Active, obj.ContactType, obj.NodeID, obj.ImageURL)
    End Sub
End Class


Public Class clsCustomer
	Private _CustomerCountry As String
	Public Property ID As String
	Public Property CustomerName As String
	Public Property CustomerAddress As String
	Public Property CustomerCity As String
	Public Property StateProvince As String
	Public Property Postal_Code As String
	Public Property ImageURL As String
	Public Property CustomerCountry As String
		Get
			Return _CustomerCountry
		End Get
		Set(ByVal value As String)
			_CustomerCountry = value
		End Set
	End Property
	Public Property CustomerPhone2 As String
	Public Property CustomerPhone As String
	Public Property CustomerEmail As String
	Public Property NodeID As Integer
	Public Property CustomerType As String
	Public Property CustomerActive As Boolean
	Public Property CustomerCreateDate As Date
	Public Property CustomerCreateUser As String
	Public Property CustomerUpdateDate As Date
	Public Property CustomerUpdateUser As String
	Public Property UpdateUserName As String
	Public Property CreateUserName As String
	Private Property llNodeID As Long
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Sub Delete() 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteCustomers(ID)
	End Sub
	Public Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.InsertCustomers(llNodeID, CustomerName, CustomerAddress, CustomerCity, StateProvince, Postal_Code, CustomerPhone, CustomerEmail, CustomerCountry)
	End Sub
	Public Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateCustomers(NodeID, CustomerName, CustomerAddress, CustomerCity, CustomerActive, StateProvince, Postal_Code, CustomerEmail,
						  CustomerPhone, CustomerCountry, ID)
		Update = 1
	End Function
	Public Sub GetCustomer(lsObjectID As String)

		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cust As New clsCustomer
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID
		ds = c.LoadCustomers(llNodeID, "", 0, ID, "", "")


		For Each row As DataRow In ds.Tables(0).Rows


			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then

				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : ID = row.Item(column).ToString
						Case "CUSTOMERNAME" : CustomerName = row.Item(column)
						Case "CUSTOMERADDRESS" : CustomerAddress = row.Item(column)
						Case "CUSTOMERCITY" : CustomerCity = row.Item(column)
						Case "CUSTOMERSTATE" : StateProvince = row.Item(column)
						Case "CUSTOMERPOSTALCODE" : Postal_Code = Trim(row.Item(column))
						Case "COUNTRY" : CustomerCountry = row.Item(column)
						Case "PHONE2" : CustomerPhone2 = row.Item(column)
						Case "CUSTOMERPHONE" : CustomerPhone = row.Item(column)
						Case "CUSTOMEREMAIL" : CustomerEmail = row.Item(column)
						Case "NODEID" : NodeID = row.Item(column)
						Case "TYPE" : CustomerType = row.Item(column)
						Case "ACTIVE" : CustomerActive = row.Item(column)
						Case "IMAGEURL" : ImageURL = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then CustomerCreateDate = CustDate
						Case "CREATEUSER" : CustomerCreateUser = row.Item(column)
						Case "UPDATEDATE" : CustomerUpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then CustomerUpdateDate = CustDate
						Case "UPDATEUSER" : CustomerUpdateUser = row.Item(column)

					End Select
				End If
			Next

		Next

	End Sub
End Class

Public Class clsProject
	Inherits clsBase
	Public Property ProjectType As String

	Public Sub New()
		NodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Function GetRecord(lsObjectID As String, lbActive As Boolean) As Boolean 'IEnumerable(Of clsCustomer)
		Dim ds As New DataSet
		Dim Tempdate As DateTime
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID
		ds = c.LoadProjects(NodeID, "", Active, ID)
		'Return ds
		'Exit Function
		Dim result As New List(Of clsProject)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)

			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : ID = row.Item(column).ToString
						Case "NAME" : Name = row.Item(column)
						Case "DESCRIPTION" : Description = row.Item(column)
						Case "CODE" : Code = row.Item(column)
						Case "NODEID" : NodeID = row.Item(column)
						Case "ACTIVE" : Active = Trim(row.Item(column))
						Case "IMAGEURL" : ImageUrl = row.Item(column)
						Case "PROJECTTYPE" : ProjectType = row.Item(column)

						Case "CREATEDATE"
							Tempdate = row.Item(column)
							If Tempdate > DateAdd("d", -100000, Now) Then CreateDate = Tempdate
						Case "CREATEUSER" : CreateUser = row.Item(column)
						Case "UPDATEDATE" : UpdateDate = row.Item(column)
							Tempdate = row.Item(column)
							If Tempdate > DateAdd("d", -100000, Now) Then UpdateDate = Tempdate
						Case "UPDATEUSER" : UpdateUser = row.Item(column)

					End Select
				End If
			Next

		Next
		'      Return result
		GetRecord = True
	End Function
	Public Overrides Sub Delete() 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteProjects(ID)
	End Sub
	Public Overrides Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		'Dim lsMsg As String
		Dim lbOK As Boolean
		lbOK = c.InsertProjects(LLNodeID, Name, Description, ImageUrl, ProjectType, Code)
		'llNodeID, ObjectID, Name, Active, UnitTypeID, Availalbe, Tier, DepositTypeID)
		If lbOK = False Then
			'lsMsg = obj.ErrorMessage
		End If
	End Sub
	Public Overrides Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateProjects(LLNodeID, Name, Description, ImageUrl, ProjectType, Active, ID, Code)
		Update = 1
	End Function
End Class
Public Class clsCustomers
	Public llNodeID As Long
	Public Active As Boolean
	Public ID As String
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Function GetCustomers(SortExpression As String, SortOrder As String, lsObjectID As String, lbActive As Boolean, lsWhere As String) As DataSet 'IEnumerable(Of clsCustomer)
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cust As New clsCustomer
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID
		ds = c.LoadCustomers(llNodeID, lsWhere, Active, ID, SortExpression, SortOrder)
		Return ds
		Exit Function
		Dim result As New List(Of clsCustomer)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cust = New clsCustomer
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cust.ID = row.Item(column).ToString
						Case "CUSTOMERNAME" : cust.CustomerName = row.Item(column)
						Case "CUSTOMERADDRESS" : cust.CustomerAddress = row.Item(column)
						Case "CUSTOMERCITY" : cust.CustomerCity = row.Item(column)
						Case "CUSTOMERSTATE" : cust.StateProvince = row.Item(column)
						Case "CUSTOMERPOSTALCODE" : cust.Postal_Code = Trim(row.Item(column))
						Case "COUNTRY" : cust.CustomerCountry = row.Item(column)
						Case "PHONE2" : cust.CustomerPhone2 = row.Item(column)
						Case "CUSTOMERPHONE" : cust.CustomerPhone = row.Item(column)
						Case "CUSTOMEREMAIL" : cust.CustomerEmail = row.Item(column)
						Case "NODEID" : cust.NodeID = row.Item(column)
						Case "TYPE" : cust.CustomerType = row.Item(column)
						Case "ACTIVE" : cust.CustomerActive = row.Item(column)
						Case "IMAGEURL" : cust.ImageURL = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerCreateDate = CustDate
						Case "CREATEUSER" : cust.CustomerCreateUser = row.Item(column)
						Case "UPDATEDATE" : cust.CustomerUpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerUpdateDate = CustDate
						Case "UPDATEUSER" : cust.CustomerUpdateUser = row.Item(column)

					End Select
				End If
			Next
			result.Add(cust)
		Next
		'      Return result
	End Function
	Function GetRecords(SortExpression As String, SortOrder As String, lsObjectID As String, lbActive As Boolean, llNodeID As Long) As IEnumerable(Of clsCustomer)
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cust As New clsCustomer
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID

		ds = c.LoadCustomers(llNodeID, "", lbActive, ID, SortExpression, SortOrder)

		Dim result As New List(Of clsCustomer)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cust = New clsCustomer
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cust.ID = row.Item(column).ToString
						Case "CUSTOMERNAME" : cust.CustomerName = row.Item(column)
						Case "CUSTOMERADDRESS" : cust.CustomerAddress = row.Item(column)
						Case "CUSTOMERCITY" : cust.CustomerCity = row.Item(column)
						Case "CUSTOMERSTATE" : cust.StateProvince = row.Item(column)
						Case "CUSTOMERPOSTALCODE" : cust.Postal_Code = Trim(row.Item(column))
						Case "COUNTRY" : cust.CustomerCountry = row.Item(column)
						Case "PHONE2" : cust.CustomerPhone2 = row.Item(column)
						Case "CUSTOMERPHONE" : cust.CustomerPhone = row.Item(column)
						Case "CUSTOMEREMAIL" : cust.CustomerEmail = row.Item(column)
						Case "NODEID" : cust.NodeID = row.Item(column)
						Case "IMAGEURL" : cust.ImageURL = row.Item(column)
						Case "TYPE" : cust.CustomerType = row.Item(column)
						Case "ACTIVE" : cust.CustomerActive = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerCreateDate = CustDate
						Case "CREATEUSER" : cust.CustomerCreateUser = row.Item(column)
						Case "UPDATEDATE" : cust.CustomerUpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerUpdateDate = CustDate
						Case "UPDATEUSER" : cust.CustomerUpdateUser = row.Item(column)

					End Select
				End If
			Next
			result.Add(cust)
		Next
		Return result
	End Function
	Public Sub DeleteCustomers(obj As clsCustomer) 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteCustomers(obj.ID)
	End Sub
	Public Sub InsertCustomers(obj As clsCustomer) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.InsertCustomers(llNodeID, obj.CustomerName, obj.CustomerAddress, obj.CustomerCity, obj.StateProvince, obj.Postal_Code, obj.CustomerPhone, obj.CustomerEmail, obj.CustomerCountry)
	End Sub
	Public Function UpdateCustomers(obj As clsCustomer) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateCustomers(llNodeID, obj.CustomerName, obj.CustomerAddress, obj.CustomerCity, obj.CustomerActive, obj.StateProvince, obj.Postal_Code, obj.CustomerEmail, obj.CustomerPhone, obj.CustomerCountry, obj.ID)
		UpdateCustomers = 1
	End Function
End Class
Public Class clsRoom
	Inherits clsBase

    'Public Property ID As String
    'Public Property Name As String
    'Public Property Code As String
    'Public Property Description As String
    'Public Property Active As Boolean


    Public Property CustomerCreateDate As Date
	Public Property CustomerCreateUser As String
	Public Property CustomerUpdateDate As Date
	Public Property CustomerUpdateUser As String
    '	Public Property UpdateUserName As String
    '    Public Property CreateUserName As String
    '    Private Property llNodeID As Long
    '    Public Property NodeID As Integer
    Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
    Public Overrides Sub Delete() 'ID As String)
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        c.DeleteRooms(ID)
    End Sub
    Public Overrides Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.InsertRooms(LLNodeID, Code, Name, Description)
	End Sub
    Public Overrides Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateRooms(LLNodeID, Code, Name, Description, Active, ID)
		Update = 1
    End Function

End Class
Public Class clsRooms
	Public llNodeID As Long
	Public Active As Boolean
	Public ID As String
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Function GetRecordData(SortExpression As String, SortOrder As String, lsObjectID As String, lbActive As Boolean, lsWhere As String, ByVal sTable As String) As DataSet 'IEnumerable(Of clsCustomer)
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cust As New clsCustomer
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID
		ds = c.LoadRooms(llNodeID, lsWhere, Active, ID)
		Return ds
		Exit Function
		Dim result As New List(Of clsCustomer)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cust = New clsCustomer
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cust.ID = row.Item(column).ToString
						Case "CUSTOMERNAME" : cust.CustomerName = row.Item(column)
						Case "CUSTOMERADDRESS" : cust.CustomerAddress = row.Item(column)
						Case "CUSTOMERCITY" : cust.CustomerCity = row.Item(column)
						Case "CUSTOMERSTATE" : cust.StateProvince = row.Item(column)
						Case "CUSTOMERPOSTALCODE" : cust.Postal_Code = Trim(row.Item(column))
						Case "COUNTRY" : cust.CustomerCountry = row.Item(column)
						Case "PHONE2" : cust.CustomerPhone2 = row.Item(column)
						Case "CUSTOMERPHONE" : cust.CustomerPhone = row.Item(column)
						Case "CUSTOMEREMAIL" : cust.CustomerEmail = row.Item(column)
						Case "NODEID" : cust.NodeID = row.Item(column)
						Case "TYPE" : cust.CustomerType = row.Item(column)
						Case "ACTIVE" : cust.CustomerActive = row.Item(column)
						Case "IMAGEURL" : cust.ImageURL = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerCreateDate = CustDate
						Case "CREATEUSER" : cust.CustomerCreateUser = row.Item(column)
						Case "UPDATEDATE" : cust.CustomerUpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerUpdateDate = CustDate
						Case "UPDATEUSER" : cust.CustomerUpdateUser = row.Item(column)

					End Select
				End If
			Next
			result.Add(cust)
		Next
		'      Return result
	End Function
	Function GetRecords(SortExpression As String, SortOrder As String, lsObjectID As String, lbActive As Boolean, llNodeID As Long) As IEnumerable(Of clsRoom)
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cust As New clsCustomer
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID

		ds = c.LoadCustomers(llNodeID, "", lbActive, ID, SortExpression, SortOrder)

		Dim result As New List(Of clsCustomer)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cust = New clsCustomer
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cust.ID = row.Item(column).ToString
						Case "CUSTOMERNAME" : cust.CustomerName = row.Item(column)
						Case "CUSTOMERADDRESS" : cust.CustomerAddress = row.Item(column)
						Case "CUSTOMERCITY" : cust.CustomerCity = row.Item(column)
						Case "CUSTOMERSTATE" : cust.StateProvince = row.Item(column)
						Case "CUSTOMERPOSTALCODE" : cust.Postal_Code = Trim(row.Item(column))
						Case "COUNTRY" : cust.CustomerCountry = row.Item(column)
						Case "PHONE2" : cust.CustomerPhone2 = row.Item(column)
						Case "CUSTOMERPHONE" : cust.CustomerPhone = row.Item(column)
						Case "CUSTOMEREMAIL" : cust.CustomerEmail = row.Item(column)
						Case "NODEID" : cust.NodeID = row.Item(column)
						Case "IMAGEURL" : cust.ImageURL = row.Item(column)
						Case "TYPE" : cust.CustomerType = row.Item(column)
						Case "ACTIVE" : cust.CustomerActive = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerCreateDate = CustDate
						Case "CREATEUSER" : cust.CustomerCreateUser = row.Item(column)
						Case "UPDATEDATE" : cust.CustomerUpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerUpdateDate = CustDate
						Case "UPDATEUSER" : cust.CustomerUpdateUser = row.Item(column)

					End Select
				End If
			Next
			result.Add(cust)
		Next
		Return result
	End Function
	Public Sub Delete(obj As clsRoom) 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteRooms(obj.ID)
	End Sub
	Public Sub Insert(obj As clsRoom) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.InsertRooms(llNodeID, obj.Code, obj.Name, obj.Description)
	End Sub
	Public Function Update(obj As clsRoom) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateRooms(llNodeID, obj.Code, obj.Name, obj.Description, obj.Active, obj.ID)
		Update = 1
	End Function
End Class
Public Class clsQuote
	Public Property ID As String
	Public NodeID As Long
	Public Property UnitTypeID As String
	Public Property QuoteStatus As String
	Public Property Phase1Status As String
	Public Property Phase2Status As String
	Public Property CustomerID As String
	Public Property QuoteID As String
	Public Property Phase1TargetDate As Date
	Public Property Phase1CompleteDate As Date
	Public Property Phase2TargetDate As Date
	Public Property Phase2CompleteDate As Date
	Public Property UnitID As String
	Public Property UnitCode As String
	Public Property UnitName As String
	Public Property UnitTypeCode As String
	Public Property UnitTypeName As String
	Public Property UnitTypeDescription As String
	Public Property CustomerName As String
	Public Property ProjectID As String
	Public Sub New()
		ID = System.Web.HttpContext.Current.Session("QuoteID")
		NodeID = System.Web.HttpContext.Current.Session("NodeID")

		Dim ds As New DataSet
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader

		ds = c.LoadQuotes(NodeID, "", True, ID)

		For Each row As DataRow In ds.Tables(0).Rows

			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "UNITTYPEID" : UnitTypeID = row.Item(column).ToString
						Case "QUOTESTATUS" : QuoteStatus = row.Item(column).ToString
						Case "PHASE1STATUS" : Phase1Status = row.Item(column).ToString
						Case "PHASE2STATUS" : Phase2Status = row.Item(column).ToString
						Case "CUSTOMERID" : CustomerID = row.Item(column).ToString = row.Item(column).ToString
						Case "QUOTEID" : QUOTEID = row.Item(column).ToString
						Case "PHASE1TARGETDATE" : PHASE1TARGETDATE = row.Item(column).ToString
						Case "PHASE1COMPLETEDATE" : PHASE1COMPLETEDATE = row.Item(column).ToString
						Case "PHASE2TARGETDATE" : PHASE2TARGETDATE = row.Item(column).ToString
						Case "PHASE2COMPLETEDATE" : PHASE2COMPLETEDATE = row.Item(column).ToString
						Case "UnitTypeID" : UnitTypeID = row.Item(column).ToString
						Case "UNITCODE" : UnitCode = row.Item(column).ToString
						Case "UNITID" : UnitID = row.Item(column).ToString
						Case "UNITNAME" : UNITNAME = row.Item(column).ToString
						Case "UNITTYPEID" : UnitTypeID = row.Item(column).ToString
						Case "UNITTYPECODE" : UNITTYPECODE = row.Item(column).ToString
						Case "UNITTYPENAME" : UNITTYPENAME = row.Item(column).ToString
						Case "UNITTYPEDESCRIPTION" : UNITTYPEDESCRIPTION = row.Item(column).ToString
						Case "CUSTOMERNAME" : CUSTOMERNAME = row.Item(column).ToString
						Case "OBJECTID" : ProjectID = row.Item(column).ToString

					End Select
				End If
			Next
		Next
	End Sub
	Function ToHTML() As String
		ToHTML = "ID: " & ID & "<br/>" &
	" UnitTypeID: " & UnitTypeID & "<br/>" &
	" QuoteStatus: " & QuoteStatus & "<br/>" &
	" Phase1Status: " & Phase1Status & "<br/>" &
	" Phase2Status: " & Phase2Status & "<br/>" &
	" CustomerID: " & CustomerID & "<br/>" &
	" QuoteID: " & QuoteID & "<br/>" &
	" Phase1TargetDate: " & Phase1TargetDate & "<br/>" &
	" Phase1CompleteDate: " & Phase1CompleteDate & "<br/>" &
	" Phase2TargetDate: " & Phase2TargetDate & "<br/>" &
	" Phase2CompleteDate: " & Phase2CompleteDate & "<br/>" &
	" UnitCode: " & UnitCode & "<br/>" &
	" UnitName: " & UnitName & "<br/>" &
	" UnitTypeCode: " & UnitTypeCode & "<br/>" &
	" UnitTypeName: " & UnitTypeName & "<br/>" &
	" UnitTypeDescription: " & UnitTypeDescription & "<br/>" &
	" CustomerName: " & CustomerName
	End Function
	Public Function InsertQuote() As Boolean
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lbSuccess As Boolean = c.InsertQuotes(NodeID, ProjectID, CustomerID, UnitID, QuoteID)
		InsertQuote = True
	End Function
End Class






Public Class clsLookup
    Public Property ID As String
    Public Property ParentID As String
    Public Property LookupID As String
    Public Property NodeID As Integer
    Public Property LookupCategory As String
    Public Property DESCRIPTION As String
    Public Property LookupValue As String
    Public Property SortOrder As Integer

    Public Property Active As Boolean
    Public Property CreateDate As Date
    Public Property CreateUser As String
    Public Property UpdateDate As Date
    Public Property UpdateUser As String
    Private llNodeID As Long
    Public Sub New()
        llNodeID = System.Web.HttpContext.Current.Session("NodeID")
        NodeID = llNodeID
    End Sub
End Class

Public Class clsLookups
	Public llNodeID As Long
	Public Active As Boolean
	Public ID As String
	Public lsCategory As String
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Function GetRecords(LookupID As String, ParentID As String, lookupType As String, id As Long, llNodeID As Long) As DataSet 'IEnumerable(Of clsCustomer)
		Dim ds As New DataSet
		Dim CreateDate As DateTime
		Dim Lookup As New clsLookup
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader

		ds = c.LoadLookups(llNodeID, ParentID, LookupID, "", lookupType, Active, id)
		Return ds
		Exit Function
		Dim result As New List(Of clsLookup)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			Lookup = New clsLookup
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : Lookup.ID = row.Item(column).ToString
						Case "PARENTID" : Lookup.ParentID = row.Item(column)
						Case "LOOKUPID" : Lookup.LookupID = row.Item(column)
						Case "LOOKUPCATEGORY" : Lookup.LookupCategory = row.Item(column)
                        Case "DESCRIPTION" : Lookup.DESCRIPTION = row.Item(column)
                        Case "LOOKUPVALUE" : Lookup.LookupValue = row.Item(column)
						Case "SORTORDER" : Lookup.SortOrder = row.Item(column)
						Case "NODEID" : Lookup.NodeID = row.Item(column)
						Case "ACTIVE" : Lookup.Active = row.Item(column)
						Case "CREATEDATE"
							CreateDate = row.Item(column)
							If CreateDate > DateAdd("d", -100000, Now) Then Lookup.CreateDate = CreateDate
						Case "CREATEUSER" : Lookup.CreateUser = row.Item(column)
						Case "UPDATEDATE" : Lookup.UpdateDate = row.Item(column)
							CreateDate = row.Item(column)
							If CreateDate > DateAdd("d", -100000, Now) Then Lookup.UpdateDate = CreateDate
						Case "UPDATEUSER" : Lookup.UpdateUser = row.Item(column)

					End Select
				End If
			Next
			result.Add(Lookup)
		Next
		'      Return result
	End Function
	Public Sub DeleteRecords(obj As clsCustomer) 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteLookups(obj.ID)
	End Sub
	Public Sub InsertRecords(obj As clsLookup) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        c.InsertLookups(llNodeID, obj.ID, obj.ParentID, obj.LookupCategory, obj.SortOrder, obj.DESCRIPTION, obj.LookupValue)
    End Sub
	Public Function UpdateRecords(obj As clsLookup) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        c.UpdateLookups(llNodeID, obj.ID, obj.ParentID, obj.LookupCategory, obj.DESCRIPTION, obj.LookupValue, obj.SortOrder, obj.Active, obj.LookupID)
        UpdateRecords = 1
	End Function
End Class


Public Class clsPersonalData
	Public Property UserID As String
	Public Property GuidValue As String
	Public Property TextValue As String
	Public Property UsageType As String

	Public Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lbOK As Boolean
		lbOK = c.InsertPersonalData(UsageType, GuidValue, TextValue)
	End Sub
	Public Sub getlastValue(ByVal lsUsageValue As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim ds As DataSet
		Dim colName As String

		ds = c.LoadPersonalData(lsUsageValue, 1, "")
		If ds Is Nothing Then Exit Sub
		If ds.Tables(0).Rows.Count = 0 Then
			Exit Sub
		Else
			For Each column As DataColumn In ds.Tables(0).Columns
				colName = UCase(column.ColumnName)
				Select Case colName
					Case "GUIDVALUE" : GuidValue = ds.Tables(0).Rows(0).Item(column).ToString.Trim
					Case "TEXTVALUE" : TextValue = ds.Tables(0).Rows(0).Item(column).ToString.Trim
					Case "USAGETYPE" : UsageType = ds.Tables(0).Rows(0).Item(column).ToString.Trim
				End Select
			Next
		End If
	End Sub
End Class
Public Class clsPersonalDatas

	Function GetRecords(SortExpression As String, SortOrder As String, lsObjectID As String) As IEnumerable(Of clsPersonalData) 'DataSet
		Dim ds As New DataSet
		Dim cpd As New clsPersonalData
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		ds = c.LoadPersonalData("", 5, "")
		'Return ds
		'Exit Function
		Dim result As New List(Of clsPersonalData)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cpd = New clsPersonalData
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "GUIDVALUE" : cpd.GuidValue = ds.Tables(0).Rows(0).Item(column).ToString.Trim
						Case "TEXTVALUE" : cpd.TextValue = ds.Tables(0).Rows(0).Item(column).ToString.Trim
						Case "USAGETYPE" : cpd.UsageType = ds.Tables(0).Rows(0).Item(column).ToString.Trim
					End Select
				End If
			Next
			result.Add(cpd)
		Next
		Return result
	End Function

End Class

Public Class clsSelectedItem
    Public Property ID As String
    Public Property ObjectID As String
    Public Property Code As String
    Public Property Name As String
    Public Property UpgradeDescription As String
    Public Property ProjectID As String
    Public Property OptionID As String
    Public Property Quantity As Integer
    Public Property Adjustments As Double
    Public Property Comments As String
    Public Property Image As String
    Public Property SortOrder As Int16
    Public Property Active As Boolean
    Public Property CreateDate As Date
    Public Property CreateUser As String
    Public Property UpdateDate As Date
    Public Property UpdateUser As String
    Public Property UpdateUserName As String
    Public Property CreateUserName As String
    Public Property ErrorMessage As String
    Public Property RequestType As String
    Public Property RoomDescription As String
    Public Property UpgradeCategory As String
    Public Property UpgradeClass As String
    Public Property StyleDescription As String
    Public Property CustomerPrice As Double
    Public Property BuildingPhase As String
    Public Property PricingRevNumber As Integer
    Public Property LeadVendor As String
    Public Property Standard As String
    Public Property RequestedUpgradeFlexText1 As String
    Public Property RequestedUpgradeFlexText2 As String
    Public Property RequestedUpgradeFlexText3 As String
    Public Property RequestedUpgradeFlexText4 As String
    Public Property AdditionalFileToPrint1 As String
    Public Property AdditionalFileToPrint2 As String

    Public Property Level As String = "" ' Tells whether this is coming from the Project (Setting up Phases) or Quote
    ' Levels is important because at the Project Level we do not modify the Quote Level Fields
    Private Property llNodeID As Long

    Public Sub New()
        llNodeID = System.Web.HttpContext.Current.Session("NodeID")
    End Sub
    Public Sub Delete() 'ID As String)
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        c.DeleteRequestedUpgrades(ID)
    End Sub
    Public Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader

        Dim lbOK As Boolean
        If OptionID = "" Then
            '	lbOK = c.InsertRequestedUpgradesCustom( llNodeID, "", ObjectID, OptionID, Quantity)
        Else
            lbOK = c.InsertRequestedUpgrades(llNodeID, "", ObjectID, OptionID, Quantity)
        End If
    End Sub
    Public Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        c.UpdateRequestedUpgrades(ID, llNodeID, Adjustments, Comments, Quantity)
        Update = 1
    End Function
End Class
Public Class clsSelectedItems

    Public UnitType As String, Quantity As Double, Adjustments As Double,
     Location As String, UpgradeCategory As String, UpgradeLevel As String,
     Description As String, ModelOrStyle As String, Comments As String,
     LeadVendor As String, CustomerPrice As Double, DeveloperPrice As Double,
     ToVendorPrice As Double, BuildingPhase As Integer, PricingRevNumber As Integer,
     OptionStatus As String, Standard As String, AdditionalFileToPrint1 As String,
     AdditionalFileToPrint2 As String, lbActive As Boolean, UnitTypeID As String,
     ID As String
    Private Property llNodeID As Long
    Function GetRecords(lsWhere As String, lsID As String, lsObjectID As String, llNodeID As Long) As IEnumerable(Of clsUpgradeOption) 'DataSet
        Dim ds As New DataSet
        Dim CustDate As Date
        Dim cObject As New clsSelectedItem
        Dim colName As String
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        ID = lsID
        If ID = "" Then ID = lsObjectID
        ds = c.LoadRequestedUpgrades(llNodeID, "", "", lsObjectID, lsWhere, True, lsID)
        'Return ds
        'Exit Function
        Dim result As New List(Of clsUpgradeOption)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim values As New List(Of Object)
            cObject = New clsSelectedItem
            For Each column As DataColumn In ds.Tables(0).Columns
                If row.IsNull(column) Then
                    values.Add(Nothing)
                Else
                    colName = UCase(column.ColumnName)
                    Select Case colName
                        'Case "UNITTYPE": cObject.unit = row.Item(column)
                        Case "LOCATION" : cObject.RoomDescription = row.Item(column)
                        Case "UPGRADECATEGORY" : cObject.UpgradeCategory = row.Item(column).ToString.Trim
                        Case "UPGRADELEVEL" : cObject.UpgradeClass = row.Item(column).ToString.Trim
                        Case "DESCRIPTION" : cObject.UpgradeDescription = row.Item(column).ToString.Trim
                        Case "MODELORSTYLE" : cObject.StyleDescription = row.Item(column).ToString.Trim

                        Case "LEADVENDOR" : cObject.LeadVendor = row.Item(column).ToString.Trim
                        Case "CUSTOMERPRICE" : cObject.CustomerPrice = row.Item(column)
                        'Case "DEVELOPERPRICE": cObject. = row.Item(column)
                    '	Case "TOVENDORPRICE": cObject.v = row.Item(column)
                        Case "BUILDINGPHASE" : cObject.BuildingPhase = row.Item(column)
                        Case "PRICINGREVNUMBER" : cObject.PricingRevNumber = row.Item(column)
                        'Case "OPTIONSTATUS": cObject.s = row.Item(column)
                        Case "STANDARD" : cObject.Standard = row.Item(column).ToString.Trim
                        Case "ADDITIONALFILETOPRINT1" : cObject.AdditionalFileToPrint1 = row.Item(column).ToString.Trim
                        Case "ADDITIONALFILETOPRINT2" : cObject.AdditionalFileToPrint2 = row.Item(column).ToString.Trim

                        'Case "UNITTYPEID" : cObject.u = row.Item(column)

                        Case "ID" : cObject.ID = row.Item(column).ToString.Trim
                        Case "NAME" : cObject.Name = row.Item(column).ToString.Trim
                        Case "OBJECTID" : cObject.ObjectID = row.Item(column).ToString.Trim
                        Case "CODE" : cObject.Code = row.Item(column).ToString.Trim
                        Case "COMMENTS" : cObject.Comments = row.Item(column).ToString.Trim

                        'Case "NODEID" : cObject.NodeID = row.Item(column)
                        Case "SORTORDER" : cObject.SortOrder = row.Item(column)
                        Case "ACTIVE" : cObject.Active = row.Item(column)
                        Case "CREATEDATE"
                            CustDate = row.Item(column)
                            If CustDate > DateAdd("d", -100000, Now) Then cObject.CreateDate = CustDate
                        Case "CREATEUSER" : cObject.CreateUser = row.Item(column).ToString.Trim
                        Case "UPDATEDATE" : cObject.UpdateDate = row.Item(column)
                            CustDate = row.Item(column)
                            If CustDate > DateAdd("d", -100000, Now) Then cObject.UpdateDate = CustDate
                        Case "UPDATEUSER" : cObject.UpdateUser = row.Item(column).ToString.Trim

                    End Select
                End If
            Next
            result.Add(New clsUpgradeOption) 'cObject)
        Next
        Return result
    End Function
    Public Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        c.UpdateRequestedUpgrades(ID, llNodeID, Adjustments, Comments, Quantity)
        Update = 1
    End Function
End Class
Public Class clsUpgradeOption
	Public UnitType As String,
	 Location As String, UpgradeCategory As String, UpgradeLevel As String,
	 Description As String, ModelOrStyle As String, Comments As String,
	 LeadVendor As String, CustomerPrice As Double, DeveloperPrice As Double,
	 ToVendorPrice As Double, BuildingPhase As Integer, PricingRevNumber As Integer,
	 OptionStatus As String, Standard As String, AdditionalFileToPrint1 As String,
	 AdditionalFileToPrint2 As String, lbActive As Boolean, UnitTypeID As String,
	 ID As String
End Class
Public Class clsUpgradeOptions
	Public UnitType As String,
	 Location As String, UpgradeCategory As String, UpgradeLevel As String,
	 Description As String, ModelOrStyle As String, Comments As String,
	 LeadVendor As String, CustomerPrice As Double, DeveloperPrice As Double,
	 ToVendorPrice As Double, BuildingPhase As Integer, PricingRevNumber As Integer,
	 OptionStatus As String, Standard As String, AdditionalFileToPrint1 As String,
	 AdditionalFileToPrint2 As String, lbActive As Boolean, UnitTypeID As String,
	 ID As String
	Private Property llNodeID As Long

    Function GetRecords(lsWhere As String, lsID As String, lsObjectID As String, llNodeID As Long) As IEnumerable(Of clsUpgradeOption) 'DataSet
        Dim ds As New DataSet
        Dim CustDate As Date
        Dim cObject As New clsSelectedItem
        Dim colName As String
        Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        ID = lsID
        If ID = "" Then ID = lsObjectID
        ds = c.LoadRequestedUpgrades(llNodeID, "", "", lsObjectID, lsWhere, True, lsID)
        'Return ds
        'Exit Function
        Dim result As New List(Of clsUpgradeOption)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim values As New List(Of Object)
            cObject = New clsSelectedItem
            For Each column As DataColumn In ds.Tables(0).Columns
                If row.IsNull(column) Then
                    values.Add(Nothing)
                Else
                    colName = UCase(column.ColumnName)
                    Select Case colName
                        'Case "UNITTYPE": cObject.unit = row.Item(column)
                        Case "LOCATION" : cObject.RoomDescription = row.Item(column)
                        Case "UPGRADECATEGORY" : cObject.UpgradeCategory = row.Item(column).ToString.Trim
                        Case "UPGRADELEVEL" : cObject.UpgradeClass = row.Item(column).ToString.Trim
                        Case "DESCRIPTION" : cObject.UpgradeDescription = row.Item(column).ToString.Trim
                        Case "MODELORSTYLE" : cObject.StyleDescription = row.Item(column).ToString.Trim

                        Case "LEADVENDOR" : cObject.LeadVendor = row.Item(column).ToString.Trim
                        Case "CUSTOMERPRICE" : cObject.CustomerPrice = row.Item(column)
                        'Case "DEVELOPERPRICE": cObject. = row.Item(column)
                    '	Case "TOVENDORPRICE": cObject.v = row.Item(column)
                        Case "BUILDINGPHASE" : cObject.BuildingPhase = row.Item(column)
                        Case "PRICINGREVNUMBER" : cObject.PricingRevNumber = row.Item(column)
                        'Case "OPTIONSTATUS": cObject.s = row.Item(column)
                        Case "STANDARD" : cObject.Standard = row.Item(column).ToString.Trim
                        Case "ADDITIONALFILETOPRINT1" : cObject.AdditionalFileToPrint1 = row.Item(column).ToString.Trim
                        Case "ADDITIONALFILETOPRINT2" : cObject.AdditionalFileToPrint2 = row.Item(column).ToString.Trim

                        'Case "UNITTYPEID" : cObject.u = row.Item(column)

                        Case "ID" : cObject.ID = row.Item(column).ToString.Trim
                        Case "NAME" : cObject.Name = row.Item(column).ToString.Trim
                        Case "OBJECTID" : cObject.ObjectID = row.Item(column).ToString.Trim
                        Case "CODE" : cObject.Code = row.Item(column).ToString.Trim
                        Case "COMMENTS" : cObject.Comments = row.Item(column).ToString.Trim

                        'Case "NODEID" : cObject.NodeID = row.Item(column)
                        Case "SORTORDER" : cObject.SortOrder = row.Item(column)
                        Case "ACTIVE" : cObject.Active = row.Item(column)
                        Case "CREATEDATE"
                            CustDate = row.Item(column)
                            If CustDate > DateAdd("d", -100000, Now) Then cObject.CreateDate = CustDate
                        Case "CREATEUSER" : cObject.CreateUser = row.Item(column).ToString.Trim
                        Case "UPDATEDATE" : cObject.UpdateDate = row.Item(column)
                            CustDate = row.Item(column)
                            If CustDate > DateAdd("d", -100000, Now) Then cObject.UpdateDate = CustDate
                        Case "UPDATEUSER" : cObject.UpdateUser = row.Item(column).ToString.Trim

                    End Select
                End If
            Next
            result.Add(New clsUpgradeOption) 'cObject)
        Next
        Return result
    End Function

    Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Sub Delete() 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
        c.DeleteUpgradeOptions(ID, llNodeID)
    End Sub
	Public Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader

		Dim lbOK As Boolean

		lbOK = c.InsertUpgradeOptions(llNodeID, UnitType,
	 Location, UpgradeCategory, UpgradeLevel,
	 Description, ModelOrStyle, Comments,
	 LeadVendor, CustomerPrice, DeveloperPrice,
	 ToVendorPrice, BuildingPhase, PricingRevNumber,
	 OptionStatus, Standard, AdditionalFileToPrint1,
	 AdditionalFileToPrint2, lbActive, UnitTypeID,
	 ID)

	End Sub
	Public Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateUpgradeOptions(llNodeID, UnitType,
	 Location, UpgradeCategory, UpgradeLevel,
	 Description, ModelOrStyle, Comments,
	 LeadVendor, CustomerPrice, DeveloperPrice,
	 ToVendorPrice, BuildingPhase, PricingRevNumber,
	 OptionStatus, Standard, AdditionalFileToPrint1,
	 AdditionalFileToPrint2, lbActive, UnitTypeID,
	 ID)
		Update = 1
	End Function
End Class
Public Class clsPhase
	Private _ProjectType As String
	Public Property ID As String
	Public Property ObjectID As String
	Public Property Code As String
	Public Property Name As String
	Public Property Description As String
	Public Property PhaseStatus As String

	Public Property ProjectType As String
		Get
			Return _ProjectType
		End Get
		Set(ByVal value As String)
			_ProjectType = value
		End Set
	End Property
	Public Property PhaseTargetDate As Date
	Public Property PhaseTargetDateString As String
	Public Property PhaseCompleteDate As Date
	Public Property PhaseCompleteDateString As String
	Public Property CustomerEmail As String
	Public Property NodeID As Integer
	Public Property Image As String
	Public Property SortOrder As Int16
	Public Property Active As Boolean
	Public Property CreateDate As Date
	Public Property CreateUser As String
	Public Property UpdateDate As Date
	Public Property UpdateUser As String
	Public Property UpdateUserName As String
	Public Property CreateUserName As String
	Public Property ErrorMessage As String
	Public Property Level As String = "" ' Tells whether this is coming from the Project (Setting up Phases) or Quote
	' Levels is important because at the Project Level we do not modify the Quote Level Fields
	Private Property llNodeID As Long
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Sub Delete() 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeletePhases(ID)
	End Sub
	Public Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader

		Dim lbOK As Boolean
		lbOK = c.InsertPhases(ObjectID, llNodeID, Name, Description, Image, ProjectType, Code, ErrorMessage, SortOrder)

	End Sub
	Public Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdatePhases(llNodeID, Name, Description, Image, ProjectType, Active, ID, Code, PhaseStatus, PhaseTargetDate, PhaseCompleteDate, Level, SortOrder)
		Update = 1
	End Function
End Class

Public Class clsPhases
	Public llNodeID As Long
	Public Active As Boolean
	Public ID As String
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Function GetRecords(SortExpression As String, SortOrder As String, lsObjectID As String) As IEnumerable(Of clsPhase) 'DataSet
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cPhase As New clsPhase
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID
		ds = c.LoadPhases(ID)
		'Return ds
		'Exit Function
		Dim result As New List(Of clsPhase)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cPhase = New clsPhase
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cPhase.ID = row.Item(column).ToString.Trim
						Case "NAME" : cPhase.Name = row.Item(column).ToString.Trim
						Case "OBJECTID" : cPhase.ObjectID = row.Item(column).ToString.Trim
						Case "CODE" : cPhase.Code = row.Item(column).ToString.Trim
						Case "DESCRIPTION" : cPhase.Description = row.Item(column).ToString.Trim
						Case "PHASETARGETDATE"
							cPhase.PhaseTargetDate = Trim(row.Item(column))
							cPhase.PhaseTargetDateString = Format(row.Item(column), "yyyy-MM-dd").ToString.Trim
						Case "PHASESTATUS" : cPhase.PhaseStatus = row.Item(column).ToString.Trim
						Case "PHASECOMPLETEDATE"
							cPhase.PhaseCompleteDate = row.Item(column)
							cPhase.PhaseCompleteDateString = Format(row.Item(column), "yyyy-MM-dd").ToString.Trim
						Case "NODEID" : cPhase.NodeID = row.Item(column)
						Case "SORTORDER" : cPhase.SortOrder = row.Item(column)
						Case "ACTIVE" : cPhase.Active = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cPhase.CreateDate = CustDate
						Case "CREATEUSER" : cPhase.CreateUser = row.Item(column).ToString.Trim
						Case "UPDATEDATE" : cPhase.UpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cPhase.UpdateDate = CustDate
						Case "UPDATEUSER" : cPhase.UpdateUser = row.Item(column).ToString.Trim

					End Select
				End If
			Next
			result.Add(cPhase)
		Next
		Return result
	End Function

End Class

Public Class clsUnit
	Inherits clsBase
	Private _UnitTypeID As String
	Public Property ObjectID As String
	Public Property Tier As String
	Public Property TierID As String
	Public Property Availalbe As Boolean
	Public Property UnitTypeName As String
	Public Property UnitTypeID As String
		Get
			Return _UnitTypeID
		End Get
		Set(ByVal value As String)
			_UnitTypeID = value
		End Set
	End Property
	Public Property FloorID As String
	Public Property PhaseCompleteDate As Date
	Public Property DepositTypeID As String

	Public Property Image As String

	Public Property Level As String = "" ' Tells whether this is coming from the Project (Setting up Phases) or Quote
    ' Levels is important because at the Project Level we do not modify the Quote Level Fields
    'Private Property llNodeID As Long
    Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Overrides Sub Delete() 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteUnits(ID)
	End Sub
	Public Overrides Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		'Dim lsMsg As String
		Dim lbOK As Boolean
		lbOK = c.InsertUnits(llNodeID, ObjectID, Name, Active, UnitTypeID, Availalbe, Tier, DepositTypeID)
		If lbOK = False Then
			'lsMsg = obj.ErrorMessage
		End If
	End Sub
	Public Overrides Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateUnits(llNodeID, Name, FloorID, UnitTypeID, Availalbe, 0, Tier, DepositTypeID, Active, ID)
		Update = 1
	End Function
End Class

Public Class clsUnits
	Public llNodeID As Long
	Public Active As Boolean
	Public ID As String
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Function GetRecords(lsWhere As String, ByVal lsID As String, lsObjectID As String, llNodeID As Long) As IEnumerable(Of clsUnit) 'DataSet
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cObject As New clsUnit
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If lsObjectID = "" Then lsObjectID = "11112222-3333-4444-5555-666677778888"
		lsWhere = " q.unitid Is null "
		ds = c.LoadUnits(llNodeID, lsWhere, True, lsObjectID, lsID)
		'Return ds
		'Exit Function

		Dim result As New List(Of clsUnit)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cObject = New clsUnit
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cObject.ID = row.Item(column).ToString
						Case "UNITNAME" : cObject.Name = row.Item(column)
						Case "OBJECTID" : cObject.ObjectID = row.Item(column).ToString
						Case "UNITCODE" : cObject.Code = row.Item(column)
						Case "TIERID" : cObject.Tier = row.Item(column)
						Case "FLOORID" : cObject.FloorID = Trim(row.Item(column).ToString)
						Case "UNITTYPEID" : cObject.UnitTypeID = row.Item(column).ToString
						Case "AVAILABLE" : cObject.Availalbe = row.Item(column)
						Case "DEPOSITTYPEID" : cObject.DepositTypeID = row.Item(column)
						Case "CREATEUSERNAME" : cObject.CreateUserName = row.Item(column)
						Case "UPDATEUSERNAME" : cObject.UpdateUserName = row.Item(column)
						Case "ACTIVE" : cObject.Active = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.CreateDate = CustDate
						Case "CREATEUSER" : cObject.CreateUser = row.Item(column).ToString
						Case "UPDATEDATE" : cObject.UpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.UpdateDate = CustDate
						Case "UPDATEUSER" : cObject.UpdateUser = row.Item(column).ToString

					End Select
				End If
			Next
			result.Add(cObject)
		Next
		Return result
	End Function
	Public Sub Delete(obj As clsUnit) 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteUnits(obj.ID)
	End Sub
	Public Sub Insert(obj As clsUnit) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String
		Dim lbOK As Boolean
		lbOK = c.InsertUnits(llNodeID, obj.ObjectID, obj.Name, obj.Active, obj.UnitTypeID, obj.Availalbe, obj.Tier, obj.DepositTypeID)
		If lbOK = False Then
			lsMsg = obj.ErrorMessage
		End If
	End Sub
	Public Function Update(obj As clsUnit) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateUnits(llNodeID, obj.Name, obj.FloorID, obj.UnitTypeID, obj.Availalbe, 0, obj.Tier, obj.DepositTypeID, obj.Active, obj.ID)
		Update = 1
	End Function
End Class
Public Class clsPayment

	Public Property ID As String
	Public Property PaymentDueDate As Date
	Public Property ActualPaymentDate as Date
	Public Property CheckNumber As String
	Public Property BuildingPhase As String
	Public Property ActualPaymentAmount As Double
	Public Property PaymentDueAmount As Double
	Public Property PaymentComment As String
	Public Property PaymentType As String
	Public Property ObjectID As String
	Public Property Code As String
	Public Property Name As String

	Public Property NodeID As Integer
	Public Property Image As String
	Public Property Active As Boolean
	Public Property CreateDate As Date
	Public Property CreateUser As String
	Public Property UpdateDate As Date
	Public Property UpdateUser As String
	Public Property UpdateUserName As String
	Public Property CreateUserName As String
	Public Property ErrorMessage As String
	Public Property Level As String = "" ' Tells whether this is coming from the Project (Setting up Phases) or Quote
	' Levels is important because at the Project Level we do not modify the Quote Level Fields
	Private Property llNodeID As Long
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String = ""
		Dim lbOK As Boolean
		lbOK = c.InsertPayments(llNodeID, ObjectID, PaymentDueDate, PaymentDueAmount, ActualPaymentDate, ActualPaymentAmount, CheckNumber, PaymentComment, BuildingPhase, PaymentType)
		If lbOK = False Then
			ErrorMessage = lsMsg
		End If
	End Sub
End Class

Public Class clsPayments
	Public llNodeID As Long
	Public Active As Boolean
	Public ID As String
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Function GetRecords(lsWhere As String, ByVal lsID As String, lsObjectID As String, llNodeID As Long) As IEnumerable(Of clsPayment) 'DataSet
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cObject As New clsPayment
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If lsObjectID = "" Then lsObjectID = "11112222-3333-4444-5555-666677778888"
		lsWhere = " "
		ds = c.LoadPayments(llNodeID, lsWhere, lsObjectID, lsID, True)
		'Return ds
		'Exit Function

		Dim result As New List(Of clsPayment)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cObject = New clsPayment
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cObject.ID = row.Item(column).ToString
						Case "NAME" : cObject.Name = row.Item(column)
						Case "OBJECTID" : cObject.ObjectID = row.Item(column).ToString
						Case "CODE" : cObject.Code = row.Item(column)
						Case "PAYMENTCOMMENT" : cObject.PaymentComment = row.Item(column)
						Case "PAYMENTTYPE" : cObject.PaymentType = row.Item(column)
						Case "PAYMENTDUEAMOUNT" : cObject.PaymentDueAmount = row.Item(column)
						Case "PAYMENTDUEDATE" : cObject.PaymentDueDate = (row.Item(column))
						Case "ACTUALPAYMENTAMOUNT" : cObject.ActualPaymentAmount = row.Item(column).ToString
						Case "ACTUALPAYMENTDATE" : cObject.ActualPaymentDate = row.Item(column)
						Case "CHECKNUMBER" : cObject.CheckNumber = row.Item(column)
						Case "CREATEUSERNAME" : cObject.CreateUserName = row.Item(column)
						Case "UPDATEUSERNAME" : cObject.UpdateUserName = row.Item(column)
						Case "ACTIVE" : cObject.Active = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.CreateDate = CustDate
						Case "CREATEUSER" : cObject.CreateUser = row.Item(column).ToString
						Case "UPDATEDATE" : cObject.UpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.UpdateDate = CustDate
						Case "UPDATEUSER" : cObject.UpdateUser = row.Item(column).ToString

					End Select
				End If
			Next
			result.Add(cObject)
		Next
		Return result
	End Function
	Public Sub Delete(obj As clsPayment) 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeletePayments(obj.ID)
	End Sub
	Public Sub Insert(obj As clsPayment) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String = ""
		Dim lbOK As Boolean
		lbOK = c.InsertPayments(llNodeID, obj.ObjectID, obj.PaymentDueDate, obj.PaymentDueAmount, obj.ActualPaymentDate, obj.ActualPaymentAmount, obj.CheckNumber, obj.PaymentComment, obj.BuildingPhase, obj.paymentType)
		If lbOK = False Then
			obj.ErrorMessage = lsMsg
		End If
	End Sub
	Public Function Update(obj As clsPayment) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdatePayments(obj.ID, llNodeID, obj.ObjectID, obj.PaymentDueDate,
						 obj.PaymentDueAmount, obj.ActualPaymentDate, obj.ActualPaymentAmount,
						 obj.CheckNumber, obj.PaymentComment, obj.BuildingPhase, obj.PaymentType, obj.Active)
		Update = 1
	End Function
End Class

Public Class clsBase

	Public Property ID As String
	Public Property Name As String
	Public Property Code As String
	Public Property Description As String
	Public Property Active As Boolean
	Public Property CreateDate As Date
	Public Property CreateUser As String
	Public Property UpdateDate As Date
	Public Property UpdateUser As String
	Public Property UpdateUserName As String
	Public Property CreateUserName As String
	Public Property llNodeID As Long
	Public Property NodeID As Integer
	Public Property ImageUrl As String
	Public FormValue As List(Of KeyValuePair(Of String, String))
	Public Property ErrorMessage As String
	Public Property TableName As String
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Overridable Sub processFormValues()
		Dim lsString As String = ""
		For Each kvp As KeyValuePair(Of String, String) In FormValue
			lsString = lsString & Chr(13) & Chr(10) & kvp.Key & kvp.Value
			Select Case kvp.Key.ToUpper
				Case "ID"
					ID = kvp.Value
				Case "CODE"
					Code = kvp.Value
				Case "NAME"
					Name = kvp.Value
				Case "Description".ToUpper
					Description = kvp.Value
				Case "Active".ToUpper
					Active = kvp.Value
				Case "CreateDate.toupper"
					CreateDate = kvp.Value
				Case "CreateUser".ToUpper
					CreateUser = kvp.Value
				Case "UpdateDate".ToUpper
					UpdateDate = kvp.Value
				Case "UpdateUser.toupper"
					UpdateUser = kvp.Value
				Case "UpdateUserName".ToUpper
					UpdateUserName = kvp.Value
				Case "CreateUserName".ToUpper
					CreateUserName = kvp.Value
				Case "llNodeID".ToUpper
					llNodeID = kvp.Value
				Case "NodeID".ToUpper
					NodeID = kvp.Value
				Case "ImageUrl".ToUpper
					ImageUrl = kvp.Value
			End Select

		Next
		If ID = "" Then
			Insert()
		Else
			Update()
		End If
		lsString = lsString
	End Sub
	Public Overridable Sub Delete() 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteBase(ID)
	End Sub
	Public Overridable Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.InsertBase(llNodeID, Name, Description, ImageUrl, Code)
	End Sub
	Public Overridable Function Update() As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsActive As String = "0"
		If Active Then
			lsActive = "1"
		End If
		c.UpdateBase(llNodeID, Name, Description, ImageUrl, "", lsActive, ID, Code)

		Update = 1
	End Function

End Class
Public Class clsBases
	Public objType As String
	Public llNodeID As Long
	Public Active As Boolean
	Public ID As String

	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
		objType = System.Web.HttpContext.Current.Session("objType")
		If objType Is Nothing Or objType = "" Then objType = "Units"
	End Sub
	Public Function GetObject(lsObjType As String) As clsBase

		Select Case lsObjType.ToUpper
			Case "ROOMS" : GetObject = New clsRoom
			Case "UNITS" : GetObject = New clsDBObject 'clsUnit
			'Case "UNITTYPES" : GetObject = New clsunittype
			'Case "FLOORS" : GetObject = New clsfloor
			'Case "VENDORS" : GetObject = New clsvendor
			'Case "UNITPROFILES" : GetObject = New clsun
			'Case "UNITTIERS" : GetObject = New clsti
			Case "PROJECTS" : GetObject = New clsProject
			'Case "LOGINS" : GetObject = New clsReportCategor
			'Case "DEPOSITCONDITIONS" : GetObject = New clsD
			'Case "COMPANYINFO" : GetObject = New clscompany
			Case "REPORTCATEGORIES", "REPORTDESCRIPTIONS", "REPORTCATEGORYMAP" : GetObject = New clsDBObject

			Case "BASE" : GetObject = New clsBase
			Case Else : GetObject = New clsDBObject
		End Select
	End Function
	Overridable Function GetRecordData(SortExpression As String, SortOrder As String, lsObjectID As String, lsObjType As String, lbActive As Boolean, lsWhere As String, ByVal sTable As String) As DataSet 'IEnumerable(Of clsCustomer)
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cust As New clsCustomer
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID
		Select Case objType.ToUpper
			Case "ROOMS" : ds = c.LoadRooms(llNodeID, lsWhere, Active, ID)
			Case "UNITS" : ds = c.LoadUnits(llNodeID, lsWhere, Active, "", ID) 'FIX THIS
			Case "UNITTYPES" : ds = c.LoadUnitTypes(llNodeID, lsWhere, Active, ID)
			Case "FLOORS" : ds = c.LoadFloors(llNodeID, lsWhere, Active, ID)
			Case "VENDORS" : ds = c.LoadVendors(llNodeID, lsWhere, Active, ID)
			Case "UNITPROFILES" : ds = c.LoadUnitProfiles(llNodeID, lsWhere, Active, ID)
			Case "UNITTIERS" : ds = c.LoadTiers(llNodeID, lsWhere, Active, ID)
			Case "PROJECTS" : ds = c.LoadProjects(llNodeID, lsWhere, Active, ID)
			Case "LOGINS" : ds = c.LoadLogins(llNodeID, lsWhere, Active, ID)
			Case "DEPOSITCONDITIONS" : ds = c.LoadDepositConditions(llNodeID, lsWhere, Active, ID)
			Case "COMPANYINFO" : ds = c.LoadCompanyInfo(llNodeID, lsWhere, Active, ID)
			Case "REPORTCATEGORIES" : ds = c.LoadReportCategories(llNodeID, lsWhere, Active, ID)
			Case "BASE" : ds = c.LoadBase(llNodeID, lsWhere, Active, ID)
		End Select

		Return ds
		Exit Function
		Dim result As New List(Of clsCustomer)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cust = New clsCustomer
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cust.ID = row.Item(column).ToString
						Case "CUSTOMERNAME" : cust.CustomerName = row.Item(column)
						Case "CUSTOMERADDRESS" : cust.CustomerAddress = row.Item(column)
						Case "CUSTOMERCITY" : cust.CustomerCity = row.Item(column)
						Case "CUSTOMERSTATE" : cust.StateProvince = row.Item(column)
						Case "CUSTOMERPOSTALCODE" : cust.Postal_Code = Trim(row.Item(column))
						Case "COUNTRY" : cust.CustomerCountry = row.Item(column)
						Case "PHONE2" : cust.CustomerPhone2 = row.Item(column)
						Case "CUSTOMERPHONE" : cust.CustomerPhone = row.Item(column)
						Case "CUSTOMEREMAIL" : cust.CustomerEmail = row.Item(column)
						Case "NODEID" : cust.NodeID = row.Item(column)
						Case "TYPE" : cust.CustomerType = row.Item(column)
						Case "ACTIVE" : cust.CustomerActive = row.Item(column)
						Case "IMAGEURL" : cust.ImageURL = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerCreateDate = CustDate
						Case "CREATEUSER" : cust.CustomerCreateUser = row.Item(column)
						Case "UPDATEDATE" : cust.CustomerUpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerUpdateDate = CustDate
						Case "UPDATEUSER" : cust.CustomerUpdateUser = row.Item(column)

					End Select
				End If
			Next
			result.Add(cust)
		Next
		'      Return result
	End Function
	Public Overridable Function GetRecords(SortExpression As String, SortOrder As String, lsObjectID As String, lbActive As Boolean, llNodeID As Long) As IEnumerable(Of clsRoom)
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cust As New clsCustomer
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If ID = "" Then ID = lsObjectID

		ds = c.LoadCustomers(llNodeID, "", lbActive, ID, SortExpression, SortOrder)

		Dim result As New List(Of clsCustomer)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cust = New clsCustomer
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cust.ID = row.Item(column).ToString
						Case "CUSTOMERNAME" : cust.CustomerName = row.Item(column)
						Case "CUSTOMERADDRESS" : cust.CustomerAddress = row.Item(column)
						Case "CUSTOMERCITY" : cust.CustomerCity = row.Item(column)
						Case "CUSTOMERSTATE" : cust.StateProvince = row.Item(column)
						Case "CUSTOMERPOSTALCODE" : cust.Postal_Code = Trim(row.Item(column))
						Case "COUNTRY" : cust.CustomerCountry = row.Item(column)
						Case "PHONE2" : cust.CustomerPhone2 = row.Item(column)
						Case "CUSTOMERPHONE" : cust.CustomerPhone = row.Item(column)
						Case "CUSTOMEREMAIL" : cust.CustomerEmail = row.Item(column)
						Case "NODEID" : cust.NodeID = row.Item(column)
						Case "IMAGEURL" : cust.ImageURL = row.Item(column)
						Case "TYPE" : cust.CustomerType = row.Item(column)
						Case "ACTIVE" : cust.CustomerActive = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerCreateDate = CustDate
						Case "CREATEUSER" : cust.CustomerCreateUser = row.Item(column)
						Case "UPDATEDATE" : cust.CustomerUpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cust.CustomerUpdateDate = CustDate
						Case "UPDATEUSER" : cust.CustomerUpdateUser = row.Item(column)

					End Select
				End If
			Next
			result.Add(cust)
		Next
		Return result
	End Function
	Public Overridable Sub Delete(obj As clsBase) 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteBase(obj.ID)
	End Sub
	Public Overridable Sub Insert(obj As clsBase) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.InsertBase(llNodeID, obj.Name, obj.Description, obj.ImageUrl, obj.Code)
	End Sub
	Public Overridable Function Update(obj As clsBase) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateBase(llNodeID, obj.Name, obj.Description, obj.ImageUrl, "", obj.Active, obj.ID, obj.Code)
		Update = 1
	End Function
End Class





'*********************************************************
'**** Adjustments
'*********************************************************
Public Class clsAdjustment

	Public Property ID As String
	Public Property AdjustmentDate As Date
	Public Property AdjustmentReason As String
	Public Property CheckNumber As String
	Public Property BuildingPhase As String
	Public Property AdjustmentAmount As Double

	Public Property ObjectID As String
	Public Property Code As String
	Public Property Name As String

	Public Property NodeID As Integer
	Public Property Image As String
	Public Property Active As Boolean
	Public Property CreateDate As Date
	Public Property CreateUser As String
	Public Property UpdateDate As Date
	Public Property UpdateUser As String
	Public Property UpdateUserName As String
	Public Property CreateUserName As String
	Public Property ErrorMessage As String
	Public Property Level As String = "" ' Tells whether this is coming from the Project (Setting up Phases) or Quote
	' Levels is important because at the Project Level we do not modify the Quote Level Fields
	Private Property llNodeID As Long
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String = ""
		Dim lbOK As Boolean
		lbOK = c.InsertAdjustments(llNodeID, ObjectID, AdjustmentAmount, AdjustmentReason, BuildingPhase, AdjustmentDate)
		If lbOK = False Then
			ErrorMessage = lsMsg
		End If
	End Sub
End Class

Public Class clsAdjustments
	Public llNodeID As Long
	Public Active As Boolean
	Public ID As String
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Function GetRecords(lsWhere As String, ByVal lsID As String, lsObjectID As String, llNodeID As Long) As IEnumerable(Of clsAdjustment) 'DataSet
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cObject As New clsAdjustment
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If lsObjectID = "" Then lsObjectID = "11112222-3333-4444-5555-666677778888"
		lsWhere = " "
		ds = c.LoadAdjustments(llNodeID, lsWhere, lsObjectID, lsID, True)
		'Return ds
		'Exit Function

		Dim result As New List(Of clsAdjustment)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cObject = New clsAdjustment
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cObject.ID = row.Item(column).ToString
						Case "NAME" : cObject.Name = row.Item(column)
						Case "OBJECTID" : cObject.ObjectID = row.Item(column).ToString
						Case "CODE" : cObject.Code = row.Item(column)
						Case "ADJUSTMENTREASON" : cObject.AdjustmentReason = row.Item(column)
						Case "ADJUSTMENTAMOUNT" : cObject.AdjustmentAmount = row.Item(column)
						Case "ADJUSTMENTDATE" : cObject.AdjustmentDate = row.Item(column)

						Case "CHECKNUMBER" : cObject.CheckNumber = row.Item(column)
						Case "CREATEUSERNAME" : cObject.CreateUserName = row.Item(column)
						Case "UPDATEUSERNAME" : cObject.UpdateUserName = row.Item(column)
						Case "ACTIVE" : cObject.Active = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.CreateDate = CustDate
						Case "CREATEUSER" : cObject.CreateUser = row.Item(column).ToString
						Case "UPDATEDATE" : cObject.UpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.UpdateDate = CustDate
						Case "UPDATEUSER" : cObject.UpdateUser = row.Item(column).ToString

					End Select
				End If
			Next
			result.Add(cObject)
		Next
		Return result
	End Function
	Public Sub Delete(obj As clsAdjustment) 'ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteAdjustments(obj.ID)
	End Sub
	Public Sub Insert(obj As clsAdjustment) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String = ""
		Dim lbOK As Boolean
		lbOK = c.InsertAdjustments(llNodeID, obj.ObjectID, obj.AdjustmentAmount, obj.AdjustmentReason, obj.BuildingPhase, obj.AdjustmentDate)
		If lbOK = False Then
			obj.ErrorMessage = lsMsg
		End If
	End Sub
	Public Function Update(obj As clsAdjustment) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateAdjustments(llNodeID, obj.ID, obj.ObjectID, obj.AdjustmentDate,
						 obj.AdjustmentReason, obj.AdjustmentAmount, 1,
						  obj.Active, 1)
		Update = 1
	End Function
End Class
'**** End Adjustments


Public Class BootstrapAlerts

	Public Enum BootstrapAlertType
		Plain
		Success
		Information
		Warning
		Danger
		Primary
	End Enum

	Public Shared Sub BootstrapAlert(MsgLabel As Label, Message As String, Optional MessageType As BootstrapAlertType = BootstrapAlertType.Plain,
									 Optional Dismissable As Boolean = False)
		Dim style As String = ""
		Dim icon As String = ""

		Select Case MessageType
			Case BootstrapAlertType.Plain
				style = "Default"
				icon = ""
			Case BootstrapAlertType.Success
				style = "success"
				icon = "check"
			Case BootstrapAlertType.Information
				style = "info"
				icon = "info-circle"
			Case BootstrapAlertType.Warning
				style = "warning"
				icon = "warning"
			Case BootstrapAlertType.Danger
				style = "danger"
				icon = "remove"
			Case BootstrapAlertType.Primary
				style = "primary"
				icon = "info"
		End Select

		If (Not MsgLabel.Page.IsPostBack Or MsgLabel.Page.IsPostBack) And Message = Nothing Then
			MsgLabel.Visible = False
		Else
			MsgLabel.Visible = True
			MsgLabel.CssClass = "alert alert-" & style & If(Dismissable = True, " alert-dismissible fade In font2", "")
			MsgLabel.Text = "<i Class='fa fa-" & icon & "'></i>" & Message
			If Dismissable = True Then
				MsgLabel.Text &= "<button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button>"
			End If
			MsgLabel.Focus()
			Message = ""
		End If
	End Sub
End Class
Public Class googleChartData
	Public Property ProductCategory As String
	Public Property RevenueAmount As Double
End Class
Public Class clsReportCategories
	Inherits clsBases

	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Public Overloads Function GetRecords(lsWhere As String, ByVal lsID As String, lsObjectID As String, llNodeID As Long) As IEnumerable(Of clsReportCategory) 'DataSet
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cObject As New clsReportCategory
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If lsObjectID = "" Then lsObjectID = "11112222-3333-4444-5555-666677778888"
		lsWhere = " "
		ds = c.LoadReportCategories("", "", True, lsID)
		'Return ds
		'Exit Function

		Dim result As New List(Of clsReportCategory)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cObject = New clsReportCategory
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cObject.ID = row.Item(column).ToString
						Case "NAME" : cObject.Name = row.Item(column)
						'Case "OBJECTID" : cObject.ObjectID = row.Item(column).ToString
						Case "CODE" : cObject.Code = row.Item(column)
						Case "REPORTCATEGORYID" : cObject.ReportCategoryID = row.Item(column)
						Case "REPORTCATEGORYTYPE" : cObject.ReportCategoryType = row.Item(column)
						Case "REPORTCATEGORYHIDELISTS" : cObject.HideLists = row.Item(column)
						Case "CREATEUSERNAME" : cObject.CreateUserName = row.Item(column)
						Case "UPDATEUSERNAME" : cObject.UpdateUserName = row.Item(column)
						Case "ACTIVE" : cObject.Active = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.CreateDate = CustDate
						Case "CREATEUSER" : cObject.CreateUser = row.Item(column).ToString
						Case "UPDATEDATE" : cObject.UpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.UpdateDate = CustDate
						Case "UPDATEUSER" : cObject.UpdateUser = row.Item(column).ToString

					End Select
				End If
			Next
			result.Add(cObject)
		Next
		Return result
	End Function
	Public Overloads Sub Delete(obj As clsReportCategory)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteReportCategory(obj.ID)

	End Sub

	Public Overloads Sub Insert(obj As clsReportCategory) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String = ""
		Dim lbOK As Boolean
		lbOK = c.InsertReportCategory(obj.ReportCategoryID, obj.Name, obj.ReportCategoryType, obj.HideLists)
		If lbOK = False Then
			obj.ErrorMessage = lsMsg
		End If
	End Sub
	Public Overloads Function Update(obj As clsReportCategory) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateReportCategory(obj.NodeID, obj.Name, obj.ReportCategoryType, obj.Active, obj.ID)
		Update = 1
	End Function
End Class
'*********************************************************
'**** ReportCategory
'*********************************************************
Public Class clsReportCategory
	Inherits clsBase
	Public Property ReportCategoryType As String
	Public Property ReportCategoryID As Long
	Public Property HideLists As Boolean
	'Private Property llNodeID As Long
	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Overrides Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String = ""
		Dim lbOK As Boolean
		lbOK = c.insertReportCategory(ID, Name, ReportCategoryType, HideLists)
		If lbOK = False Then
			ErrorMessage = lsMsg
		End If
	End Sub
	Public Overloads Function Update(obj As clsReportCategory) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateReportCategory(obj.NodeID, obj.Name, obj.ReportCategoryType, obj.Active, obj.ID)
		Update = 1
	End Function
End Class

'*********************************************************
'**** DB Objects
'*********************************************************
Public Class clsDBObjects
	Inherits clsBases

	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub

	Public Overloads Function GetRecords(lsWhere As String, ByVal lsID As String, lsObjectID As String, llNodeID As Long, lsTableName As String) As IEnumerable(Of clsDBObject) 'DataSet
		Dim ds As New DataSet
		Dim CustDate As Date
		Dim cObject As New clsDBObject
		Dim colName As String
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		If lsObjectID = "" Then lsObjectID = "11112222-3333-4444-5555-666677778888"
		lsWhere = " "
		ds = c.LoadThings(llNodeID, lsWhere, True, lsID, lsTableName)
		'Return ds
		'Exit Function

		Dim result As New List(Of clsDBObject)
		For Each row As DataRow In ds.Tables(0).Rows
			Dim values As New List(Of Object)
			cObject = New clsDBObject
			For Each column As DataColumn In ds.Tables(0).Columns
				If row.IsNull(column) Then
					values.Add(Nothing)
				Else
					colName = UCase(column.ColumnName)
					Select Case colName
						Case "ID" : cObject.ID = row.Item(column).ToString
						Case "NAME" : cObject.Name = row.Item(column)
						'Case "OBJECTID" : cObject.ObjectID = row.Item(column).ToString
						Case "CODE" : cObject.Code = row.Item(column)
						Case "REPORTDESCRIPTION" : cObject.Description = row.Item(column)
						Case "REPORTCATEGORYHIDELISTS" : cObject.HideLists = row.Item(column)
						Case "CREATEUSERNAME" : cObject.CreateUserName = row.Item(column)
						Case "UPDATEUSERNAME" : cObject.UpdateUserName = row.Item(column)
						Case "ACTIVE" : cObject.Active = row.Item(column)
						Case "CREATEDATE"
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.CreateDate = CustDate
						Case "CREATEUSER" : cObject.CreateUser = row.Item(column).ToString
						Case "UPDATEDATE" : cObject.UpdateDate = row.Item(column)
							CustDate = row.Item(column)
							If CustDate > DateAdd("d", -100000, Now) Then cObject.UpdateDate = CustDate
						Case "UPDATEUSER" : cObject.UpdateUser = row.Item(column).ToString

					End Select
				End If
			Next
			result.Add(cObject)
		Next
		Return result
	End Function
	Public Overloads Sub Delete(obj As clsDBObject)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.DeleteThings(obj.ID, obj.TableName)
		If c.msErrorMsg = "" Then

		Else
			obj.ErrorMessage = c.msErrorMsg
		End If
	End Sub

	Public Overloads Sub Insert(obj As clsDBObject) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String = ""
		Dim lbOK As Boolean
		lbOK = c.InsertThings(obj.FormValue, obj.TableName, obj.ID, obj.llNodeID, obj.Name, obj.Description, obj.Code)
		If lbOK = False Then
			obj.ErrorMessage = lsMsg
		End If
	End Sub
	Public Overloads Function Update(obj As clsDBObject) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateThings(obj.FormValue, obj.NodeID, obj.Name, obj.Description, obj.Code, obj.ID, obj.TableName, obj.Active)
		If c.msErrorMsg = "" Then
			Update = 1
		Else
			Update = 0
			obj.ErrorMessage = c.msErrorMsg
		End If

	End Function
End Class
'*********************************************************
'**** DB Object
'*********************************************************
Public Class clsDBObject
	Inherits clsBase
    'Public Property Description As String
    Public Property HideLists As Boolean
	'Private Property llNodeID As Long

	Public Sub New()
		llNodeID = System.Web.HttpContext.Current.Session("NodeID")
	End Sub
	Public Overrides Sub Insert() 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		Dim lsMsg As String = ""
		Dim lbOK As Boolean
		lbOK = c.InsertThings(FormValue, TableName, ID, llNodeID, Name, Description, Code)
		If lbOK = False Then
			ErrorMessage = lsMsg

		End If
	End Sub
	Public Overloads Function Update(obj As clsDBObject) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
		Dim c As New Kolassa.DesignCentre.Data.clsSelectDataLoader
		c.UpdateThings(FormValue, llNodeID, obj.Name, obj.Description, obj.Code, obj.ID, obj.TableName, obj.Active)
		If c.msErrorMsg <> "" Then
			Update = 0
			ErrorMessage = c.msErrorMsg
			obj.ErrorMessage = c.msErrorMsg
		Else
			Update = 1
		End If

	End Function

	Public Overrides Sub processFormValues()
		Dim lsString As String = ""
		For Each kvp As KeyValuePair(Of String, String) In FormValue
			lsString = lsString & Chr(13) & Chr(10) & kvp.Key & kvp.Value
			Select Case kvp.Key.ToUpper
				Case "ID"
					ID = kvp.Value

				Case "CODE"
					Code = kvp.Value
				Case "NAME"
					Name = kvp.Value
				Case "Description".ToUpper
					Description = kvp.Value
				Case "Active".ToUpper
					Active = kvp.Value
				Case "CreateDate.toupper"
					CreateDate = kvp.Value
				Case "CreateUser".ToUpper
					CreateUser = kvp.Value
				Case "UpdateDate".ToUpper
					UpdateDate = kvp.Value
				Case "UpdateUser.toupper"
					UpdateUser = kvp.Value
				Case "UpdateUserName".ToUpper
					UpdateUserName = kvp.Value
				Case "CreateUserName".ToUpper
					CreateUserName = kvp.Value
				Case "llNodeID".ToUpper
					llNodeID = kvp.Value
				Case "NodeID".ToUpper
					NodeID = kvp.Value
				Case "ImageUrl".ToUpper
					ImageUrl = kvp.Value
			End Select

		Next
		If ID = "" Then
			Insert()
		Else
			Update(Me)
		End If
		lsString = lsString
	End Sub
End Class
