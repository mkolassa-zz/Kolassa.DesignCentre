Imports System.Data

Public Class clsObjects
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
        Dim c As New clsSelectDataLoader
        c.DeleteContacts(obj.ID)
    End Sub
    Public Sub InsertContacts(obj As clsContact)
        Dim c As New clsSelectDataLoader
        c.InsertContacts(obj.llNodeID, obj.FirstName, obj.LastName, obj.ParentID, obj.FullAddress, obj.City, obj.StateProvince, obj.PostalCode,
                         obj.Country, obj.Phone1, obj.Phone2, obj.Email1, obj.Email2, obj.ContactType)
    End Sub
    Public Sub UpdateContacts(obj As clsContact)
        Dim c As New clsSelectDataLoader
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
End Class

Public Class clsCustomers
    Public llNodeID As Long
    Public Active As Boolean
    Public ID As String
    Public Sub New()
        llNodeID = System.Web.HttpContext.Current.Session("NodeID")
    End Sub

    Function GetCustomers(SortExpression As String, SortOrder As String, lsObjectID As String) As DataSet 'IEnumerable(Of clsCustomer)
        Dim ds As New DataSet
        Dim CustDate As Date
        Dim cust As New clsCustomer
        Dim colName As String
        Dim c As New clsSelectDataLoader
        If ID = "" Then ID = lsObjectID
        ds = c.LoadCustomers(llNodeID, "", Active, ID, SortExpression, SortOrder)
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
    Public Sub DeleteCustomers(obj As clsCustomer) 'ID As String)
        Dim c As New clsSelectDataLoader
        c.DeleteCustomers(obj.ID)
    End Sub
    Public Sub InsertCustomers(obj As clsCustomer) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
        Dim c As New clsSelectDataLoader
        c.InsertCustomers(llNodeID, obj.CustomerName, obj.CustomerAddress, obj.CustomerCity, obj.StateProvince, obj.Postal_Code, obj.CustomerPhone, obj.CustomerEmail, obj.CustomerCountry)
    End Sub
    Public Function UpdateCustomers(obj As clsCustomer) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
        Dim c As New clsSelectDataLoader
        c.UpdateCustomers(llNodeID, obj.CustomerName, obj.CustomerAddress, obj.CustomerCity, obj.CustomerActive, obj.StateProvince, obj.Postal_Code, obj.CustomerEmail, obj.CustomerPhone, obj.CustomerCountry, obj.ID)
        UpdateCustomers = 1
    End Function
End Class

Public Class clsLookup
    Public Property ID As String
    Public Property ParentID As String
    Public Property LookupID As String
    Public Property NodeID As Integer
    Public Property LookupCategory As String
    Public Property LookupDescription As String
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
        Dim c As New clsSelectDataLoader

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
                        Case "LOOKUPDESCRIPTION" : Lookup.LookupDescription = row.Item(column)
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
        Dim c As New clsSelectDataLoader
        c.DeleteLookups(obj.ID)
    End Sub
    Public Sub InsertRecords(obj As clsLookup) 'NodeID As Integer, FirstName As String, LastName As String, ParentID As String, FullAddress As String, City As String, StateProvince As String, PostalCode As String, Country As String, Phone1 As String, Phone2 As String, Email1 As String, Email2 As String, ContactType As String)
        Dim c As New clsSelectDataLoader
        c.InsertLookups(llNodeID, obj.ID, obj.ParentID, obj.LookupCategory, obj.SortOrder, obj.LookupDescription, obj.LookupValue)
    End Sub
    Public Function UpdateRecords(obj As clsLookup) As Integer 'NodeID As Integer, FirstName As String, LastName As String, City As String, ContactType As String, Active As String, ID As String)
        Dim c As New clsSelectDataLoader
        c.UpdateLookups(llNodeID, obj.ID, obj.ParentID, obj.LookupCategory, obj.LookupDescription, obj.LookupValue, obj.SortOrder, obj.Active, obj.LookupID)
        UpdateRecords = 1
    End Function
End Class