Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.AspNet.Identity

Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Security.Principal
Public Class clsSelectDataLoader

    Dim mdsUnits As DataSet = New DataSet
    Dim mdsProjects As DataSet = New DataSet
    Dim mdsProjectTypes As DataSet = New DataSet
    Dim mdsUnitProfiles As DataSet = New DataSet
    Dim mdsQuotes As DataSet = New DataSet
    Dim mdsFloors As DataSet = New DataSet
    Dim mdsVendors As DataSet = New DataSet
    Dim mdsUnitTypes As DataSet = New DataSet
    Dim mdsEventLogs As DataSet = New DataSet
    Dim mdsLookups As DataSet = New DataSet
    Dim mdsLogins As DataSet = New DataSet
    Dim mdsDepositConditions As DataSet = New DataSet
    Dim mdsTiers As DataSet = New DataSet
    Dim mdsCustomers As DataSet = New DataSet
    Dim mdsContacts As DataSet = New DataSet
    Dim mdsCompanyInfo As DataSet = New DataSet
    Public mdsReportCategories As DataSet
    Dim mdsReportControls As DataSet
    Public mdsAllControls As DataSet
    Dim mdsRooms As DataSet
    Dim mdsListItems As DataSet
    Dim mdsChildren As DataSet
    Dim cns As Data.SqlClient.SqlConnection
    Dim cno As Data.OleDb.OleDbConnection
    Dim mscnStr As String
    Dim mscnType As String
    Dim mscnDefault As String
    Dim mlCategoryID As Long
    Public msSQLcmd As SqlClient.SqlCommand
    Dim msSQLParameter As SqlParameter
    Dim NL As String = Chr(13) & Chr(10)
    Public Sub New()
        mscnType = "SQLConnection" '"OLEDB"
        mscnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Documents and Settings\mkolassa\My Documents\Visual Studio\Projects\WebSites\Select\App_Data\legadata.mdb;User Id=Master;Password=Cubs1;Jet OLEDB:System Database=D:\Documents and Settings\mkolassa\My Documents\Visual Studio\Projects\WebSites\Select\App_Data\Parallax.mdw;"
        mscnStr = System.Configuration.ConfigurationManager.ConnectionStrings.Item("ReportManager").ToString
        mscnDefault = System.Configuration.ConfigurationManager.ConnectionStrings.Item("DefaultConnection").ToString
        mscnStr = mscnDefault
    End Sub
    Public Function LoadTables(ByVal llTableID As Long, ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadTables = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If

        lsSQL = "SELECT SelectStatement From tblReportSQL WHERE ReportID = " & llTableID & ";"

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Tables")

        lsSQL = ds.Tables(0).Rows(0).Item(0).ToString

        mdsQuotes = ds
        lsSQL = lsSQL & " " & NL &
       " AND  (NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
             IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
          "" '   IIf(llID > 0, " OR QuoteID = " & llID & " ", "")

        ds = fGetDataset(mscnType, mscnStr, lsSQL, "TableData")


        mdsQuotes = ds
        LoadTables = mdsQuotes
    End Function

    '*******************************************************
    '*** Projects
    '*******************************************************
    Public Function LoadProjects(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        '*** Initialize
        LoadProjects = Nothing
        llNodeID = 2
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT F.NODEID, F.ID, F.Name, F.Description, F.Active, F.Image, F.ProjectType , F.Code, F.ProjectTypeName    FROM " & NL &
                "(Select P.NodeID, P.ID, P.Name, P.Description, P.Active, P.Image, P.ProjectType, T.Code, T.Name as ProjectTypeName " & NL &
                 " FROM tblProjects as P left join   tblProjectTypes as T on P.ProjectType = T.Code )  F                            " & NL &
                "WHERE  ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
                     IIf(lbActive = True, " And Active = 1 ", "") & ")" & NL & ""
        'IIf(llID > 0, " Or ID = '" & llID & "' ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Projects")
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")
        mdsProjects = ds
        LoadProjects = mdsProjects
    End Function
    Public Function DeleteProjects(ByVal RecordID As String) As Boolean
        Dim lscnStr As String = mscnDefault
        Dim lsSQL As String = "Update tblProjects Set Active=0  WHERE  ID='" & RecordID & "'"
        DeleteProjects = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    Public Function InsertProjects(ByVal llNodeID As Long, ByVal lsName As String, lsDescription As String, ByVal lsImage As String, ByVal lsProjectType As String) As Boolean
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertProjects = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblProjects ( Name, Description,  ProjectType, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( '" & fTakeOutQuotes(lsName) & "', " & NL &
                          "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
                          "'" & fTakeOutQuotes(lsProjectType) & "', " & NL &
                          "'" & Now.ToString & "', " & NL &
                          "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                          "'" & Now.ToString & "', " & NL &
                          "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                           "1, '" & llNodeID & "'); "
        '*** Run The SQL.
        InsertProjects = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    Public Function UpdateProjects(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsImage As String, lsProjectType As String, ByVal lsActive As String, ByVal ID As String) As Boolean
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateProjects = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblProjects  " &
                "Set Name = '" & fTakeOutQuotes(lsName) & "', Description = " & NL &
                          "'" & fTakeOutQuotes(lsDescription) & "', image= " & NL &
                          "'" & fTakeOutQuotes(lsImage) & "', ProjectType= " & NL &
                          "'" & fTakeOutQuotes(lsProjectType) & "', UpdateDate=" & NL &
                          "'" & Now.ToString & "', UpdateUser=" & NL &
                          "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL &
                 " Where ID='" & ID & "';"
        '*** Run The SQL.
        UpdateProjects = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    '*******************************************************
    '*** Project Types
    '*******************************************************
    Public Function LoadProjectTypes(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        '*** Initialize
        LoadProjectTypes = Nothing
        llNodeID = 2
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT * " & NL &
                "FROM tblProjectTypes                                   " & NL &
                "WHERE ( tblProjectTypes.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
                     IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                     IIf(llID > 0, " OR QuoteID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Projects")
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")
        mdsProjectTypes = ds
        LoadProjectTypes = mdsProjectTypes
    End Function
    Public Function DeleteProjectTypes(ByVal RecordID As Long) As Boolean
        Dim lscnStr As String = mscnDefault
        Dim lsSQL As String = "Update tblProjects Set Active=False  WHERE  ID=" & RecordID
        DeleteProjectTypes = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    Public Function InsertProjectTypes(ByVal llNodeID As Long, ByVal lsName As String, lsDescription As String, ByVal lsImage As String, ByVal lsProjectType As String, lsCode As String) As Boolean
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertProjectTypes = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblProjectTypes ( Name, Code, Description, Image, ProjectType " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
                          """" & fTakeOutQuotes(lsCode) & """, " & NL &
                            """" & fTakeOutQuotes(lsDescription) & """, " & NL &
                          """" & fTakeOutQuotes(lsImage) & """, " & NL &
                          """" & fTakeOutQuotes(lsProjectType) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "1, " & llNodeID & "); "
        '*** Run The SQL.
        InsertProjectTypes = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function

    Public Function UpdateProjectTypes(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsImage As String, lsCode As String, ByVal lsActive As String, ByVal ID As String) As Boolean
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateProjectTypes = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblProjectTypes  " &
                "Set Name = """ & fTakeOutQuotes(lsName) & """, Description = " & NL &
                          """" & fTakeOutQuotes(lsDescription) & """, image= " & NL &
                          """" & fTakeOutQuotes(lsImage) & """, Code= " & NL &
                          """" & fTakeOutQuotes(lsCode) & """, UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where ID=" & ID & ";"
        '*** Run The SQL.
        UpdateProjectTypes = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function

    '*******************************************************
    '*** Quotes
    '*******************************************************
    Public Function LoadQuotes(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadQuotes = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT * " & NL &
                "FROM v_QuoteLookup                                   " & NL &
               "WHERE ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
                     IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                     IIf(llID > 0, " OR QuoteID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Units")

        mdsQuotes = ds
        LoadQuotes = mdsQuotes
    End Function
    Public Function DeleteQuotes(ByVal RecordID As Long) As Boolean
        Dim lsSQL As String = "Update tblQuotes Set Active=False  WHERE  QuoteID=" & RecordID
        DeleteQuotes = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertQuotes(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertQuotes = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertQuotes = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateQuotes(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateQuotes = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblVendors  " &
                "Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where VendorID=" & VendorID & ";"
        '*** Run The SQL.
        UpdateQuotes = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function




    '****************************************************
    '*** Requested Upgrades
    Public Function LoadRequestedUpgrades(ByVal llNodeID As Long, ByVal lsRoom As String, ByVal lsPhase As String, ByVal lsQuoteID As String, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long, Optional ByVal lsCat As String = "") As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadRequestedUpgrades = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT RequestedUpgradeID, " &
                "       UpgradeCategory as Category, " &
                "       UpgradeClass as UpgradeLevel, " &
                "       UpgradeDescription as Description, " &
                "       StyleDescription as Style, " &
                "       CustomerPrice + Adjustments AS Cost " &
                "FROM   tblRequestedUpgrades " &
                "WHERE  RoomDescription = '" & lsRoom & "'" &
                "        AND BuildingPhase = " & lsPhase & " " &
                "        AND quoteid = " & lsQuoteID & " " &
                " " & IIf(lsWhere.Length > 4, " AND " & lsWhere, "") &
                    IIf(lbActive = True, " and Active = 1 ", "") & NL &
                        IIf(lsCat = "", "", " and UpgradeCategory =  """ & lsCat & """") & NL &
                    IIf(llID > 0, " OR RequestedUpgradeID = " & llID & " ", "") & NL &
                "ORDER BY UpgradeCategory"


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Customers")

        mdsCustomers = ds
        LoadRequestedUpgrades = mdsCustomers
    End Function
    Public Function DeleteRequestedUpgrades(ByVal RecordID As Long) As Boolean
        Dim lsSQL As String = "Delete From tblRequestedUpgrades   WHERE  RequestedUpgradeID =" & RecordID
        DeleteRequestedUpgrades = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertRequestedUpgrades(ByVal llNodeID As Long, ByVal llQuoteID As Long, ByVal lsRoomDesc As String, ByVal lsUpgradeDesc As String,
                                            ByVal lsUpgradeCategory As String, ByVal lsPhase As String,
                                            ByVal lsUpgradeClass As String, ByVal lsStyle As String,
                                            ByVal lsPrice As String,
                                            ByVal lsTypeHold As String, ByVal llUpgradeOptionID As Long) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertRequestedUpgrades = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If

        lsSQL = "INSERT INTO tblRequestedUpgrades " &
              "    ( QuoteID, RoomDescription, UpgradeDescription, " &
              "      UpgradeCategory, UpgradeClass, StyleDescription, CustomerPrice, Adjustments, " &
              "      PricingRevNumber, CreateUser, UpdateDate, BuildingPhase, RequestType, UpgradeOptionID) " &
              "values (" & llQuoteID & ", '" &
               lsRoomDesc & "',""" &
               fReplaceQuotes(lsUpgradeDesc) & """, """ &
               fReplaceQuotes(lsUpgradeCategory) & """, """ &
               fReplaceQuotes(lsUpgradeClass) & """, """ &
               fReplaceQuotes(lsStyle) & """," &
               lsPrice & " , 0, 1, '" & lsCurrentUser & "' , #" & Now & "#," & lsPhase & ",'" &
               lsTypeHold & "'," & llUpgradeOptionID & ");"

        '*** Run The SQL.
        InsertRequestedUpgrades = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateRequestedUpgrades(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateRequestedUpgrades = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblVendors  " &
                "Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where VendorID=" & VendorID & ";"
        '*** Run The SQL.
        UpdateRequestedUpgrades = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function




    '****************************************************
    '*** Customers
    Public Function LoadCustomers(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String, SortExpression As String, SortOrder As String) As DataSet
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        '*** Initialize
        LoadCustomers = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "Select convert(nvarchar(50), f.ID) as ID, f.CustomerName, f.CustomerAddress, f.CustomerCity, f.CustomerState as StateProvince, f.CustomerPostalCode as Postal_Code , 
                   f.CustomerCountry, f.CustomerPhone, f.CustomerEmail, f.NodeID, f.CustomerType, f.Active as CustomerActive, 
                   f.CreateDate As CustomerCreateDate, f.CreateUser As CustomerCreateUser, f.UpdateDate As CustomerUpdateDate, f.UpdateUser As CustomerUpdateUser, 
                   uc.username as CreateUserName, uu.username as UpdateUserName , ci.imageurl 
                FROM   tblCustomers f    Left Join aspnetUsers as uc On f.createuser = convert(varchar(40), uc.ID )
                                         Left Join aspnetUsers as uu on f.UpdateUser = convert(varchar(40), uu.ID )
                                         Left Join (Select * from tblImages where type='Primary' and nodeID = '" & llNodeID & "' ) as ci on f.ID = ci.ObjectID " & NL &
                "WHERE  f.NodeID=" & llNodeID & " " & NL &
                    IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
                    IIf(lbActive = True, " And Active = 1 ", "") & NL &
                    IIf(lsID <> "", " and f.ID = '" & lsID & "' ", "") & NL &
                IIf(SortExpression = "", "", " Order By " & SortExpression & " " & SortOrder)


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Customers")

        '*** Debugging Start
        Dim dt As DataTable = ds.Tables.Item("Customers")
        Dim rowCustomer As DataRow

        For Each rowCustomer In dt.Rows
            Dim ls As String = rowCustomer.Item(0).ToString
            Console.WriteLine("Ya" & ls & "Ya")
            System.Diagnostics.Debug.Write(ls)
        Next
        '*** Debugging End

        mdsCustomers = ds
        LoadCustomers = mdsCustomers
    End Function
    Public Function DeleteCustomers(ByVal RecordID As String) As Boolean
        Dim lscnStr As String = mscnDefault
        Dim lsSQL As String = "Update tblCustomers Set Active=0  WHERE  ID ='" & RecordID & "'"
        DeleteCustomers = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    Public Function InsertCustomers(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsAddress As String,
                                    ByVal lsCity As String, ByVal lsState As String, ByVal lsPostalCode As String,
                                    ByVal lsPhone As String, ByVal lsemail As String,
                                    ByVal lsCustomerCountry As String) As Boolean
        Dim lgID As New Guid
        lgID = Guid.NewGuid
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertCustomers = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblCustomers (ID, CustomerName, CustomerAddress, CustomerCity, CustomerState, CustomerPostalCode, CustomerCountry,  CustomerEmail, CustomerPhone, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( @CustomerGuid, @CustomerName, @CustomerAddress, @CustomerCity, @CustomerState, @CustomerPostalCode, @CustomerCountry, @CustomerEmail, @CustomerPhone, " & NL &
                "         GetDate(), @CurrentUser, GetDate(), @CurrentUser, 1, @CustomerNode );"
        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL
        msSQLcmd.Parameters.Clear()
        msSQLcmd.Parameters.AddWithValue("@CustomerGuid", fTakeOutQuotes(lgID.ToString))
        msSQLcmd.Parameters.AddWithValue("@CustomerName", fTakeOutQuotes(lsName))
        msSQLcmd.Parameters.AddWithValue("@CustomerAddress", fTakeOutQuotes(lsAddress))
        msSQLcmd.Parameters.AddWithValue("@CustomerCity", fTakeOutQuotes(lsCity))
        msSQLcmd.Parameters.AddWithValue("@CustomerState", fTakeOutQuotes(lsState))
        msSQLcmd.Parameters.AddWithValue("@CustomerPostalCode", fTakeOutQuotes(lsPostalCode))
        msSQLcmd.Parameters.AddWithValue("@CustomerEmail", fTakeOutQuotes(lsemail))
        msSQLcmd.Parameters.AddWithValue("@CustomerPhone", fTakeOutQuotes(lsPhone))
        msSQLcmd.Parameters.AddWithValue("@CurrentUser", fTakeOutQuotes(lsCurrentUser))
        msSQLcmd.Parameters.AddWithValue("@CustomerNode", fTakeOutQuotes(llNodeID))
        msSQLcmd.Parameters.AddWithValue("@CustomerCountry", fTakeOutQuotes(lsCustomerCountry))
        '*** Run The SQL.
        InsertCustomers = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
    End Function

    Public Function UpdateCustomers(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsAddress As String,
                                    ByVal lsCity As String, ByVal lsActive As String,
                                    lsState As String, lsPostalCode As String, lsEmail As String,
                                    lsPhone As String, lsCustomerCountry As String,
                                    ByVal ObjectID As String) As Boolean
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        If UCase(lsActive) = "TRUE" Then
            lsActive = "1"
        Else
            lsActive = "0"
        End If
        '*** Initialize
        UpdateCustomers = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblCustomers  " &
                "Set CustomerName = @CustomerName, CustomerAddress = @CustomerAddress , CustomerCity= @CustomerCity , " & NL &
                        " CustomerState= @CustomerState , CustomerPostalCode= @CustomerPostalCode ," & NL &
                        " CustomerEmail= @CustomerEmail , CustomerPhone= @CustomerPhone , CustomerCountry= @CustomerCountry , " & NL &
                        " UpdateDate= GetDate() , UpdateUser= @CurrentUser , " & NL &
                        " Active = @CustomerActive " & NL &
                 " Where ID= @ObjectID ;"
        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL
        msSQLcmd.Parameters.Clear()
        msSQLcmd.Parameters.AddWithValue("@ObjectID", fTakeOutQuotes(ObjectID.ToString))
        msSQLcmd.Parameters.AddWithValue("@CustomerName", fTakeOutQuotes(lsName))
        msSQLcmd.Parameters.AddWithValue("@CustomerAddress", fTakeOutQuotes(lsAddress))
        msSQLcmd.Parameters.AddWithValue("@CustomerCity", fTakeOutQuotes(lsCity))
        msSQLcmd.Parameters.AddWithValue("@CustomerState", fTakeOutQuotes(lsState))
        msSQLcmd.Parameters.AddWithValue("@CustomerPostalCode", fTakeOutQuotes(lsPostalCode))
        msSQLcmd.Parameters.AddWithValue("@CustomerEmail", fTakeOutQuotes(lsEmail))
        msSQLcmd.Parameters.AddWithValue("@CustomerPhone", fTakeOutQuotes(lsPhone))
        msSQLcmd.Parameters.AddWithValue("@CurrentUser", fTakeOutQuotes(lsCurrentUser))
        msSQLcmd.Parameters.AddWithValue("@CustomerNode", fTakeOutQuotes(llNodeID))
        msSQLcmd.Parameters.AddWithValue("@CustomerActive", fTakeOutQuotes(lsActive))
        msSQLcmd.Parameters.AddWithValue("@CustomerCountry", fTakeOutQuotes(lsCustomerCountry))
        '*** Run The SQL.
        UpdateCustomers = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
        'MyCommand = New SqlCommand("UPDATE SeansMessage SET Message = @TicBoxText WHERE Number = 1", dbConn)
        'MyCommand.Parameters.AddWithValue("@TicBoxText", TicBoxText.Text)
    End Function
    '*******************************************************
    '*** Contacts
    '*******************************************************
    Public Function LoadContacts(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String, lsObjectID As String) As DataSet
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        '*** Initialize
        LoadContacts = Nothing
        llNodeID = 2
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "Select (convert(nvarchar(50), f.ID)) as ID , f.ObjectID as ParentID 
                , F.FirstName, f.LastName, f.FullAddress, f.CIty, f.StateProvince, f.PostalCode, f.Country
                , isnull( f.Phone1,'') as Phone1, isnull(f.Phone2,'') as Phone2, f.Email1, f.Email2, f.NodeID, f.Type, f.Active, f.CreateDate, f.CreateUser, f.UpdateDate, f.UpdateUser
                , I.imageURL  , uc.username CreateUserName, uu.username UpdateUserName  " & NL &
                "FROM tblContacts F     Left Join tblImages I on f.ID = I.OBJECTID 
                                        Left Join aspnetUsers as uc On f.createuser = convert(varchar(40), uc.ID )
                                        Left Join aspnetUsers as uu on f.UpdateUser = convert(varchar(40), uu.ID ) " & NL &
                "WHERE  ( f.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " And " & lsWhere, "") &
                     IIf(lbActive = True, " And f.Active = 1 ", "") & NL &
        IIf(lsObjectID = "", "", " And f.ObjectID ='" & lsObjectID & "'") & ")" & NL & ""
        'IIf(llID > 0, " Or ID = '" & llID & "' ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Contacts")
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Contacts")
        mdsContacts = ds
        LoadContacts = mdsContacts
    End Function
    Public Function DeleteContacts(ByVal RecordID As String) As Boolean
        Dim lscnStr As String = mscnDefault
        Dim lsSQL As String = "Update tblContacts Set Active=0  WHERE  ID='" & RecordID & "'"
        DeleteContacts = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    Public Function InsertContacts(ByVal llNodeID As Long, ByVal lsFirstName As String, ByVal lsLastName As String, lsObjectID As String,
                                   ByVal lsAddress As String, ByVal lsCity As String, ByVal lsState As String, ByVal lsPostalCode As String,
                                   ByVal lsCountry As String,
                                   ByVal lsPhone1 As String, ByVal lsPhone2 As String, ByVal lsEmail1 As String, ByVal lsEmail2 As String,
                                   ByVal lsContactType As String) As Boolean
        'InsertContacts' that has parameters: llNodeID, lsObjectID, lsFirstName, lsLastName, lsAddress, lsCity, lsState, lsPostalCode,
        'lsCountry, lsPhone1, 
        'lsPhone2, lsEmail1, lsEmail2, lsContactType, Phone, City, email, ID, LastName, FirstName, State.
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertContacts = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblContacts ( FirstName, LastName,  Type, ObjectID, " &
                 "                        FullAddress, City, StateProvince, PostalCode, Country, Phone1, Phone2, Email1, Email2, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( '" & fTakeOutQuotes(lsFirstName) & "', " & NL &
                          "'" & fTakeOutQuotes(lsLastName) & "', " & NL &
                          "'" & fTakeOutQuotes(lsContactType) & "', " & NL &
                          "'" & fTakeOutQuotes(lsObjectID) & "', " & NL &
                          "'" & fTakeOutQuotes(lsAddress) & "', " & NL &
                          "'" & fTakeOutQuotes(lsCity) & "', " & NL &
                          "'" & fTakeOutQuotes(lsState) & "', " & NL &
                          "'" & fTakeOutQuotes(lsPostalCode) & "', " & NL &
                          "'" & fTakeOutQuotes(lsCountry) & "', " & NL &
                          "'" & fTakeOutQuotes(lsPhone1) & "', " & NL &
                          "'" & fTakeOutQuotes(lsPhone2) & "', " & NL &
                          "'" & fTakeOutQuotes(lsEmail1) & "', " & NL &
                          "'" & fTakeOutQuotes(lsEmail2) & "', " & NL &
                          "'" & Now.ToString & "', " & NL &
                          "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                          "'" & Now.ToString & "', " & NL &
                          "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                           "1, '" & llNodeID & "'); "
        '*** Run The SQL.
        InsertContacts = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    Public Function UpdateContacts(ID As String, ParentID As String, FirstName As String, LastName As String, FullAddress As String, City As String, StateProvince As String,
     PostalCode As String, Country As String, Phone2 As String, Phone1 As String, Email1 As String, Email2 As String, NodeID As Integer, Type As String, Active As Boolean,
    ContactType As String, llNodeID As Long, ImageURL As String) As Boolean
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateContacts = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblContacts  " &
                "Set FirstName = @FirstName, LastName=@LastName, FullAddress = @FullAddress , City= @City , " & NL &
                        " StateProvince = @StateProvince , PostalCode= @PostalCode ," & NL &
                        " Email1= @Email1 , Phone1= @Phone1 , Email2= @Email2 , Phone2= @Phone2 , Country= @Country , " & NL &
                        " UpdateDate= GetDate() , UpdateUser= @CurrentUser , " & NL &
                        " Active = @Active , Type=@ContactType  , NodeID=@NodeID " & NL &
                 " Where ID= @ObjectID ;"
        msSQLcmd = New SqlCommand
        msSQLcmd.CommandText = lsSQL
        msSQLcmd.Parameters.Clear()
        msSQLcmd.Parameters.AddWithValue("@ObjectID", fTakeOutQuotes(ID.ToString))
        msSQLcmd.Parameters.AddWithValue("@FirstName", fTakeOutQuotes(FirstName))
        msSQLcmd.Parameters.AddWithValue("@LastName", fTakeOutQuotes(LastName))
        msSQLcmd.Parameters.AddWithValue("@FullAddress", fTakeOutQuotes(FullAddress))
        msSQLcmd.Parameters.AddWithValue("@City", fTakeOutQuotes(City))
        msSQLcmd.Parameters.AddWithValue("@StateProvince", fTakeOutQuotes(StateProvince))
        msSQLcmd.Parameters.AddWithValue("@PostalCode", fTakeOutQuotes(PostalCode))
        msSQLcmd.Parameters.AddWithValue("@Email1", fTakeOutQuotes(Email1))
        msSQLcmd.Parameters.AddWithValue("@Phone1", fTakeOutQuotes(Phone1))
        msSQLcmd.Parameters.AddWithValue("@Email2", fTakeOutQuotes(Email2))
        msSQLcmd.Parameters.AddWithValue("@Phone2", fTakeOutQuotes(Phone2))
        msSQLcmd.Parameters.AddWithValue("@CurrentUser", fTakeOutQuotes(lsCurrentUser))
        msSQLcmd.Parameters.AddWithValue("@NodeID", fTakeOutQuotes(NodeID))
        msSQLcmd.Parameters.AddWithValue("@ContactType", fTakeOutQuotes(ContactType))
        msSQLcmd.Parameters.AddWithValue("@Active", fTakeOutQuotes(Active))
        msSQLcmd.Parameters.AddWithValue("@Country", fTakeOutQuotes(Country))
        '*** Run The SQL.
        UpdateContacts = fExecuteSQLCmd("SQLConnection", lscnStr, msSQLcmd)
    End Function
    '****************************************************
    '*** Rooms
    Public Function LoadRooms(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadRooms = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT * " & NL &
                "FROM tblRooms as R                                 " & NL &
                  "WHERE  NodeID=" & llNodeID & " " &
                    IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
                    IIf(lbActive = True, " and Active = 1 ", "") & NL &
                    IIf(llID > 0, " and RoomID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Rooms")

        mdsRooms = ds
        LoadRooms = mdsRooms
    End Function

    '****************************************************
    '*** Unit Rooms
    Public Function LoadUnitRooms(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As String) As DataSet
        Dim lsSQL As String
        If isGUID(lsID) Then
        Else
            lsID = "00000000-0000-0000-000000000000"
        End If
        '*** Initialize
        LoadUnitRooms = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT tblUnitProfiles.ID UnitProfileID, tblRooms.ID roomid, tblUnitProfiles.ID UnitTypeID, " &
                "tblRooms.RoomName, tblRooms.RoomDescription, tblUnits.UnitCode, tblUnitProfiles.Active " &
                "FROM (tblUnitTypes " &
                "   INNER JOIN (tblUnitProfiles " &
                "   INNER JOIN tblRooms ON tblUnitProfiles.RoomID = tblRooms.ID) " &
                "   ON tblUnitTypes.ID = tblUnitProfiles.UnitTypeID) " &
                "   INNER JOIN tblUnits ON tblUnitTypes.ID = tblUnits.UnitTypeID " &
                "WHERE tblUnits.ID='" & lsID & "' " &
                    IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
                    IIf(lbActive = True, " and tblUnitProfiles.Active = 1 ", "") & NL &
                    " Order By RoomName"


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Rooms")

        mdsRooms = ds
        LoadUnitRooms = mdsRooms
    End Function

    '****************************************************
    '*** Room Categories
    Public Function LoadRoomCategories(ByVal llNodeID As Long, ByVal lsUnitType As String, ByVal llPhase As Long, ByVal lsRoom As String) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadRoomCategories = Nothing
        If llNodeID = 0 Or lsUnitType = "" Then Exit Function


        lsSQL = "SELECT O.UnitType, O.UpgradeCategory, O.Location " &
                "FROM tblUpgradeOptions O " &
                "WHERE (((O.Active) = 1)) " &
                "GROUP BY O.UnitType, O.Location, O.UpgradeCategory, O.BuildingPhase " &
                "HAVING O.UnitType='" & lsUnitType & "' AND " &
                "O.BuildingPhase=" & llPhase & " AND " &
                "O.Location='" & lsRoom & "';"
        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")

        mdsRooms = ds
        LoadRoomCategories = mdsRooms
    End Function

    '****************************************************
    '*** Room Category Levels
    Public Function LoadRoomCategoryLevels(ByVal llNodeID As Long, ByVal lsUnitType As String, ByVal llPhase As Long, ByVal lsRoom As String, ByVal lsCategory As String) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadRoomCategoryLevels = Nothing
        If llNodeID = 0 Or lsUnitType = "" Then Exit Function


        lsSQL = "SELECT DISTINCT  O.UpgradeLevel " &
                "FROM     tblUpgradeOptions as O " &
                "WHERE (((O.UnitType)='" & lsUnitType & "') AND " &
                "((O.Location)='" & lsRoom & "') and " &
                "O.BuildingPhase=" & llPhase & " AND " &
                "((O.UpgradeCategory) = '" & lsCategory & "'))" &
                "ORDER BY O.UpgradeLevel;"



        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")

        mdsRooms = ds
        LoadRoomCategoryLevels = mdsRooms
    End Function

    '****************************************************
    '*** Room Category Level Styles
    Public Function LoadRoomCategoryLevelStyles(ByVal llNodeID As Long, ByVal lsUnitType As String, ByVal llPhase As Long, ByVal lsRoom As String, ByVal lsCategory As String, ByVal lsLevel As String, Optional ByVal llID As Long = 0) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadRoomCategoryLevelStyles = Nothing
        If llID = 0 And (llNodeID = 0 Or lsUnitType = "") Then Exit Function

        lsSQL = "SELECT Distinct Description, ModelorStyle as Style, " &
                   "CustomerPrice, UnitType, UpgradeCategory, " &
                    "Location, UpgradeLevel, UpgradeOptionID " &
                    "FROM tblUpgradeOptions " &
                    "Where "
        If llID > 0 Then
            lsSQL = lsSQL & " UpgradeOptionID = " & llID & ";"
        Else
            lsSQL = lsSQL & "( active=1 " &
                    "    AND UnitType='" & lsUnitType & "'  " &
                    "    AND Location='" & lsRoom & "' " &
                    "    AND UpgradeLevel='" & fReplaceQuotes(lsLevel) & "' " &
                    "    AND UpgradeCategory = '" & fReplaceQuotes(lsCategory) & "');"

        End If




        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")

        mdsRooms = ds
        LoadRoomCategoryLevelStyles = mdsRooms
    End Function

    Public Function DeleteRooms(ByVal RecordID As Long) As Boolean
        Dim lsSQL As String = "Update tblRooms Set Active=False  WHERE  RoomID=" & RecordID
        DeleteRooms = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertRooms(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertRooms = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selected")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( RoomName, RoomDescription, VendorAbbreviation, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( '" & fTakeOutQuotes(lsName) & "', " & NL &
                          "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
                          "'" & Now.ToString & "', " & NL &
                          "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                        " '" & Now.ToString & "', " & NL &
                         " '" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertRooms = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateRooms(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsActive As String, ByVal RoomID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateRooms = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblVendors  " &
                "Set RoomName = """ & fTakeOutQuotes(lsName) & """, RoomDescription = " & NL &
                          """" & fTakeOutQuotes(lsDescription) & """, UpdateDate= " & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where VendorID=" & RoomID & ";"
        '*** Run The SQL.
        UpdateRooms = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function


    '****************************************************
    '*** Unit Profiles
    Public Function LoadUnitProfiles(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadUnitProfiles = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT U.*, T.UnitTypeName, R.RoomName " & NL &
                "FROM (tblUnitProfiles as U INNER JOIN tblUnitTypes as T on U.UnitTypeID = T.ID )  " & NL &
                "           INNER JOIN tblRooms as R on U.RoomID = R.RoomID   " & NL &
                "WHERE  U.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
                    IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                    IIf(llID > 0, " OR UnitProfileID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "UnitProfiles")

        mdsUnitProfiles = ds
        LoadUnitProfiles = mdsUnitProfiles
    End Function
    Public Function DeleteUnitProfiles(ByVal UnitProfileID As Long) As Boolean
        Dim lsSQL As String = "Update tblUnitProfiles Set Active=False  WHERE  UnitProfileID=" & UnitProfileID
        DeleteUnitProfiles = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertUnitProfiles(ByVal llNodeID As Long, ByVal lsUnitTypeID As String, ByVal lsRoomID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertUnitProfiles = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblUnitProfiles ( UnitTypeID, RoomID,  " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( " & lsUnitTypeID & ", " & NL &
                          "" & lsRoomID & ", " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertUnitProfiles = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateUnitProfiles(ByVal llNodeID As Long, ByVal UnitProfileID As String, ByVal lsUnitTypeID As String,
                                       ByVal lsActive As String, ByVal lsRoomID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateUnitProfiles = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblUnitProfiles  " &
                "Set RoomID = """ & fTakeOutQuotes(lsRoomID) & """, UnitTypeID = " & NL &
                          """" & fTakeOutQuotes(lsUnitTypeID) & """,   UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where UnitProfileID=" & UnitProfileID & ";"
        '*** Run The SQL.
        UpdateUnitProfiles = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function





    '****************************************************
    '*** Floors

    Public Function LoadFloors(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadFloors = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        If lsWhere = "" Then
            lbActive = True
        Else : lbActive = False

        End If
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'llNodeID = 1
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT F.*, L.LookupDescription AS BuildingLevelText " &
                "FROM tblFloors AS F LEFT JOIN (Select lookupid,  lookupdescription from tblLookups) AS L ON F.BuildingLevel = L.LookupID   " & NL &
                "WHERE ( F.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
                    IIf(lbActive = True, " and F.Active = 1 ", "") & ")" & NL &
                    IIf(llID > 0, " OR FloorID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Floors")

        mdsFloors = ds
        LoadFloors = mdsFloors
    End Function
    Public Function DeleteFloors(ByVal FloorID As Long) As Boolean
        Dim lsSQL As String = "Update tblFloors Set Active=False  WHERE  FloorID=" & FloorID
        DeleteFloors = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertFloors(ByVal llNodeID As Long, ByVal lsFloorName As String, ByVal lsFloorDescription As String, ByVal lsBuildinglevel As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertFloors = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblFloors ( FloorName, FloorDescription, Buildinglevel, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsFloorName) & """, " & NL &
                          """" & fTakeOutQuotes(lsFloorDescription) & """, " & NL &
                          """" & fTakeOutQuotes(lsBuildinglevel) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertFloors = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateFloors(ByVal llNodeID As Long, ByVal lsFloorName As String,
                                 ByVal lsFloorDescription As String, ByVal lsBuildingLevel As String,
                                ByVal lsActive As String, ByVal FloorID As String) As Boolean
        '*** DataKey from the table must be listed in the Parameters exactly as it appears in the Properties
        '*** of the GridView 
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateFloors = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblFloors  " &
               "Set FloorName = """ & fTakeOutQuotes(lsFloorName) & """, FloorDescription = " & NL &
                        """" & fTakeOutQuotes(lsFloorDescription) & """, BuildingLevel= " & NL &
                         fTakeOutQuotes(lsBuildingLevel) & ", UpdateDate=" & NL &
                      """" & Now.ToString & """, UpdateUser=" & NL &
                     """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
           " Where FloorID=" & FloorID & ";"
        '*** Run The SQL.
        UpdateFloors = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

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


    '****************************************************
    '*** Lookups
    Public Function LoadLookups(ByVal llNodeID As Long, lsParentID As String, lsLookupID As String, ByVal lsWhere As String, ByVal lsLookupType As String,
                                ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        '  llNodeID = 1
        '*** Initialize
        LoadLookups = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            ' Exit Function
        End If
        lsSQL = "With LookupList as (
                Select a.NodeID, convert(nvarchar(50), a.ID) as ID , convert(nvarchar(50),  a.ParentID) as ParentID , a.LookupID, a.LookupValue, a.LookupDescription
                      , 1 as LookupLevel,active from tbllookups a where a.Parentid is null
	                  and lookupCategory = '" & fTakeOutQuotes(lsLookupType) & "'
                union All
                Select  b.NodeID, convert(nvarchar(50), b.ID) as ID, convert(nvarchar(50), b.ParentID) as ParentID, b.LookupID, b.LookupValue, b.LookupDescription
                      , a.LookupLevel + 1 as LookupLevel, a.active from LookupList a
                        Inner Join tbllookups b on a.id = b.parentid
                Where b.ParentID is not null
               ) Select * from LookupList " & NL &
                "WHERE (NodeID is null or NodeID=" & llNodeID & " ) " &
                        IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
                    IIf(isGUID(lsLookupID), " and ID=" & lsLookupID, "") & NL &
                    IIf(lbActive = True, " and Active = 1 ", "") & NL &
                    IIf(llID > 0, " and lookupID = " & llID & " ", "")

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Lookups")

        mdsLookups = ds
        LoadLookups = mdsLookups
    End Function
    Public Function DeleteLookups(ByVal RecordID As Long) As Boolean
        Dim lsSQL As String = "Update tblLookups Set Active=False  WHERE  LookupID=" & RecordID
        DeleteLookups = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertLookups(ByVal llNodeID As Long, ByVal lsID As String, ByVal lsParentID As String, ByVal lsLookupCategory As String,
                                 liSortOrder As Integer, lsLookupDescription As String, lsLookupValue As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertLookups = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( ParentID, LookupID, LookupCategory, LookupDecsription, LookupValue, SortOrder " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsParentID) & """, " & NL &
                          "" & fTakeOutQuotes(lsID) & ", " & NL &
                          """" & fTakeOutQuotes(lsLookupCategory) & """, " & NL &
                          """" & fTakeOutQuotes(lsLookupDescription) & """, " & NL &
                          """" & fTakeOutQuotes(lsLookupValue) & """, " & NL &
                            "" & fTakeOutQuotes(liSortOrder) & ", " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertLookups = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateLookups(ByVal llNodeID As Long, ByVal lsID As String, ByVal lsParentID As String, ByVal lsLookupCategory As String,
                                  ByVal lsLookupDescription As String, ByVal lsLookupValue As String, ByVal liSortOrder As Integer,
                                  ByVal lsActive As String, ByVal liLookupID As Integer) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateLookups = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblLookups  " &
                "Set LookupID = " & fTakeOutQuotes(liLookupID) & ", ID = " & NL &
                        """" & fTakeOutQuotes(lsID) & """, LookupCategory= " & NL &
                        """" & fTakeOutQuotes(lsLookupCategory) & """, LookupDescription= " & NL &
                        """" & fTakeOutQuotes(lsLookupDescription) & """, LookupValue= " & NL &
                        """" & fTakeOutQuotes(lsLookupValue) & """, ParentID= " & NL &
                        """" & fTakeOutQuotes(lsParentID) & """, NodeID= " & NL &
                        """" & fTakeOutQuotes(llNodeID) & """,  UpdateDate=" & NL &
                        """" & Now.ToString & """, UpdateUser=" & NL &
                        """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where ID=""" & lsID & """;"
        '*** Run The SQL.
        UpdateLookups = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function




    '****************************************************
    '*** Logins 
    Public Function LoadLogins(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadLogins = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT * " & NL &
                "FROM tblLogins                                   " & NL &
                "WHERE  NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
                    IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                    IIf(llID > 0, " OR LoginID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "frmLogins")

        mdsLogins = ds
        LoadLogins = mdsLogins
    End Function
    Public Function DeleteLogins(ByVal RecordID As Long) As Boolean
        Dim lsSQL As String = "Update tblLogins Set Active=False  WHERE  LoginID=" & RecordID
        DeleteLogins = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertLogins(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertLogins = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertLogins = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateLogins(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateLogins = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblVendors  " &
                "Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where VendorID=" & VendorID & ";"
        '*** Run The SQL.
        UpdateLogins = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function




    '****************************************************
    '***
    Public Function LoadEventLogs(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadEventLogs = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT * " & NL &
                "FROM tblEventLog                                   " & NL &
               "WHERE  NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
         IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                    IIf(llID > 0, " OR EventID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "EventLogs")

        mdsEventLogs = ds
        LoadEventLogs = mdsEventLogs
    End Function
    Public Function DeleteEventLogs(ByVal RecordID As Long) As Boolean
        Dim lsSQL As String = "Update tblEventLog Set Active=False  WHERE  EventID=" & RecordID
        DeleteEventLogs = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertEventLogs(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertEventLogs = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertEventLogs = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateEventLogs(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateEventLogs = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblVendors  " &
                "Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where VendorID=" & VendorID & ";"
        '*** Run The SQL.
        UpdateEventLogs = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

    '****************************************************
    '*** Deposit Conditions
    Public Function LoadDepositConditions(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String
        llNodeID = 1
        '*** Initialize
        LoadDepositConditions = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT * " & NL &
                "FROM  tblDepositConditions   " & NL &
                "WHERE ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
IIf(llID > 0, " OR DepositTypeID = " & llID & " ", "")

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "DepositConditions")

        mdsDepositConditions = ds
        LoadDepositConditions = mdsDepositConditions
    End Function
    Public Function DeleteDepositConditions(ByVal DepositTypeID As Long) As Boolean
        Dim lsSQL As String = "Update tblDepositConditions Set Active=False  WHERE  DepositTypeID=" & DepositTypeID
        DeleteDepositConditions = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertDepositConditions(ByVal llNodeID As Long, ByVal lsUnitName As String, ByVal lsFloorID As String,
                                ByVal lsUnitTypeID As String, ByVal lsAvailable As String,
                                ByVal lsTier As String, ByVal lsDepositType As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertDepositConditions = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( UnitName, FloorID, UnitTypeID, Available, Tier, DepositType, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsUnitName) & """, " & NL &
                          "" & fTakeOutQuotes(lsFloorID) & ", " & NL &
                          """" & fTakeOutQuotes(lsUnitTypeID) & """, " & NL &
                          """" & fTakeOutQuotes(lsAvailable) & """, " & NL &
                          """" & fTakeOutQuotes(lsTier) & """, " & NL &
                          """" & fTakeOutQuotes(lsDepositType) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertDepositConditions = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateDepositConditions(ByVal UnitID As String, ByVal lsNodeID As String, ByVal lsUnitName As String, ByVal lsFloorID As String,
                                ByVal lsUnitTypeID As String, ByVal lsActive As String, ByVal lsAvailable As String,
                                ByVal lsTier As String, ByVal lsDepositType As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateDepositConditions = False
        '*** Check for No Selected Category
        If lsNodeID = 0 Then
            Exit Function
        End If

        If lsActive.ToUpper = "TRUE" Then lsActive = "1"
        If lsAvailable.ToUpper = "TRUE" Then lsAvailable = "1"

        lsSQL = "Update tblUnits  "
        lsSQL = lsSQL & "Set UnitName = '" & fTakeOutQuotes(lsUnitName) & "', Tier = " & NL
        lsSQL = lsSQL & """" & fTakeOutQuotes(lsTier) & """, FloorID = " & NL
        lsSQL = lsSQL & fTakeOutQuotes(lsFloorID) & ", UnitTypeID= " & NL
        lsSQL = lsSQL & """" & fTakeOutQuotes(lsUnitTypeID) & """, Available= " & NL
        lsSQL = lsSQL & fTakeOutQuotes(lsAvailable) & ", DepositType= " & NL
        lsSQL = lsSQL & """" & fTakeOutQuotes(lsDepositType) & """, UpdateDate=" & NL
        lsSQL = lsSQL & """" & Now.ToString & """, UpdateUser=" & NL
        lsSQL = lsSQL & """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL
        lsSQL = lsSQL & " , NodeID = " & lsNodeID & " " & NL
        lsSQL = lsSQL & " Where UnitID=" & UnitID & ";"
        '*** Run The SQL.
        UpdateDepositConditions = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

    '****************************************************
    '*** Units
    Public Function LoadUnits(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long, lsID As String) As DataSet
        Dim lsSQL As String
        llNodeID = 1
        '*** Initialize
        LoadUnits = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT U.ID, D.DepositTypeName, F.FloorName, Tier.TierName, T.UnitTypeName, U.* " & NL &
                "FROM (((( tblUnits  U                                 " & NL &
                "  INNER JOIN tblUnitTypes  T         ON U.UnitTypeID = T.ID        )   " & NL &
                "  INNER JOIN tblDepositConditions  D ON U.DepositTypeID = D.DepositTypeID  )   " & NL &
                "  INNER JOIN tblFloors  F            ON U.FloorID = F.FloorID              )   " & NL &
                "  INNER JOIN tblUnitTiers  Tier          ON U.TierID = Tier.TierID             )   " & NL &
                "WHERE  U.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
                IIf(lbActive = True, " and U.Active = True ", "") & NL &
                IIf(llID > 0, " and UnitID = " & llID & " ", "") &
                IIf(lsID = "", "", " and u.ID = '" & lsID & "' ")

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Units")

        mdsUnits = ds
        LoadUnits = mdsUnits
    End Function
    Public Function DeleteUnits(ByVal lsID As String) As Boolean
        Dim lsSQL As String = "Update tblUnits Set Active=0  WHERE  ID=" & lsID
        DeleteUnits = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertUnits(ByVal llNodeID As Long, lsUnitID As String, ByVal lsUnitName As String, ByVal lsFloorID As String,
                                ByVal lsUnitTypeID As String, ByVal lsAvailable As String,
                                ByVal lsTier As String, ByVal lsDepositType As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertUnits = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        If lsAvailable.ToUpper = "TRUE" Then lsAvailable = 1
        lsSQL = "INSERT INTO tblUnits ( UnitID, UnitName, FloorID, UnitTypeID, Available, TierID, DepositTypeID, 
                                        UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) 
                Values ( '" & fTakeOutQuotes(lsUnitID) & "', " &
                 "     '" & fTakeOutQuotes(lsUnitName) & "', " &
                           lsFloorID & ", " &
                           lsUnitTypeID & ", " &
                           lsAvailable & ", " &
                           lsTier & ", " &
                           lsDepositType & ", " &
                           "'" & Now.ToString & "', " &
                           "'" & fTakeOutQuotes(lsCurrentUser) & "', " &
                           "'" & Now.ToString & "', " &
                           "'" & fTakeOutQuotes(lsCurrentUser) & "', " & NL &
                           "1, " & llNodeID & "); "
        '*** Run The SQL.
        InsertUnits = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

    Public Function UpdateUnits(ByVal llNodeID As Long, ByVal lsUnitName As String, ByVal lsFloorID As String,
                              ByVal lsUnitTypeID As String, ByVal lsAvailable As String, ByVal UnitID As String,
                              ByVal lsTier As String, ByVal lsDepositType As String, ByVal lsActive As String _
, lsID As String) As Boolean


        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateUnits = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        If lsAvailable.ToUpper = "TRUE" Then
            lsAvailable = 1
        Else
            lsAvailable = "0"
        End If
        If lsActive.ToUpper = "TRUE" Then
            lsActive = 1
        Else
            lsActive = "0"
        End If
        lsSQL = "Update tblUnits  "
        lsSQL = lsSQL & "Set UnitName = '" & fTakeOutQuotes(lsUnitName) & "', TierID = " & NL
        lsSQL = lsSQL & lsTier & ", FloorID = " & NL
        lsSQL = lsSQL & lsFloorID & ", UnitTypeID= " & NL
        lsSQL = lsSQL & lsUnitTypeID & ", Available= " & NL
        lsSQL = lsSQL & lsAvailable & ", DepositTypeID= " & NL
        lsSQL = lsSQL & lsDepositType & ", UpdateDate=" & NL
        lsSQL = lsSQL & "'" & Now.ToString & "', UpdateUser=" & NL
        lsSQL = lsSQL & "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL
        lsSQL = lsSQL & " , NodeID = " & llNodeID & " " & NL
        lsSQL = lsSQL & " Where ID='" & lsID & "';"
        '*** Run The SQL.
        UpdateUnits = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function


    '****************************************************
    '*** Missing Upgrades
    Public Function LoadMissingUpgrades(ByVal lsQuoteID As String) As DataSet
        Dim lsSQL As String
        LoadMissingUpgrades = Nothing
        lsSQL = "    SELECT tempselections.BuildingPhase, tempselections.Location, tempselections.UpgradeCategory, " & NL &
                "           tempselections.Required, * " & NL &
                "    FROM(tempselections) " & NL &
                "    WHERE(((tempselections.Required) = True) And ((tempselections.ValueExists) = False)) " & NL &
                "           AND QuoteID = " & lsQuoteID & " " & NL &
                "    ORDER BY tempselections.BuildingPhase, tempselections.Location, tempselections.UpgradeCategory"


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Tiers")

        mdsTiers = ds
        LoadMissingUpgrades = mdsTiers
    End Function


    '****************************************************
    '*** Tiers
    Public Function LoadTiers(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String
        llNodeID = 1
        '*** Initialize
        LoadTiers = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT * " & NL &
                "FROM tblUnitTiers       " & NL &
                "WHERE ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") & NL &
                    IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                    IIf(llID > 0, " OR TierID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Tiers")

        mdsTiers = ds
        LoadTiers = mdsTiers
    End Function
    Public Function DeleteTiers(ByVal TierID As Long) As Boolean
        Dim lsSQL As String = "Update tblUnitTiers Set Active=False  WHERE  TierID=" & TierID & ";"
        DeleteTiers = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertTiers(ByVal Tier As String, ByVal llNodeID As Long) As Boolean

        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertTiers = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblUnitTiers ( TierName, UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(Tier) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertTiers = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

    Public Function UpdateTiers(ByVal TierID As Long, ByVal TierName As String, ByVal llNodeID As Long, ByVal lsActive As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateTiers = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblUnitTiers  "
        lsSQL = lsSQL & "Set TierName = """ & fTakeOutQuotes(TierName) & """,  Active = " & lsActive & NL
        lsSQL = lsSQL & " , NodeID = " & llNodeID & " " & NL
        lsSQL = lsSQL & " Where TierID=" & TierID & " AND NodeID = " & llNodeID & ";"
        '*** Run The SQL.
        UpdateTiers = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function



    '*************************************************
    '**** Vendors
    Public Function DeleteVendors(ByVal VendorID As Long) As Boolean
        Dim lsSQL As String = "Update tblVendors Set Active=False  WHERE  VendorID=" & VendorID
        DeleteVendors = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

    Public Function LoadVendors(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadVendors = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT SelectStatement From tblReportSQL " & NL &
                "FROM tblVendors                                   " & NL &
               "WHERE  NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
                    IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                    IIf(llID > 0, " OR VendorID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Vendors")

        mdsVendors = ds
        LoadVendors = mdsVendors
    End Function
    Public Function InsertVendors(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertVendors = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Load a data set.
        InsertVendors = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateVendors(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateVendors = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblVendors  " &
                "Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where VendorID=" & VendorID & ";"
        '*** Load a data set.
        UpdateVendors = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function




    '****************************************************
    '***UnitTypes
    Public Function LoadUnitTypes(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadUnitTypes = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT tblUnitTypes.*, ID as UnitTypeID " & NL &
                "FROM tblUnitTypes                                   " & NL &
                "WHERE ( NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
                IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                IIf(llID > 0, " OR ID = " & llID & " ", "")

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "UnitTypes")

        mdsUnitTypes = ds
        LoadUnitTypes = mdsUnitTypes
    End Function
    Public Function DeleteUnitTypes(ByVal UnitTypeID As Long) As Boolean
        Dim lsSQL As String = "Update tblUnitTypes Set Active=False  WHERE  UnitTypeID=" & UnitTypeID
        DeleteUnitTypes = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertUnitTypes(ByVal llNodeID As Long, ByVal lsUnitTypeName As String, ByVal lsUnitTypeDescription As String, ByVal lsSubType As String, ByVal lsLevel As String, ByVal lsModelUnit As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertUnitTypes = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If


        lsSQL = "INSERT INTO tblUnitTypes (  UnitTypeName, UnitTypeDescription, SubType, [Level], ModelUnit, UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsUnitTypeName) & """, " & NL &
                          """" & fTakeOutQuotes(lsUnitTypeDescription) & """, " & NL &
                          """" & fTakeOutQuotes(lsSubType) & """, " & NL &
                          """" & fTakeOutQuotes(lsLevel) & """, " & NL &
                          """" & fTakeOutQuotes(lsModelUnit) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertUnitTypes = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateUnitTypes(ByVal llNodeID As Long, ByVal UnitTypeID As Long, ByVal UnitTypeName As String,
                                    ByVal UnitTypeDescription As String, ByVal SubType As String,
                                    ByVal lsLevel As String, ByVal ModelUnit As String,
                                    ByVal lsActive As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateUnitTypes = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If



        lsSQL = "Update tblUnitTypes  " &
                "Set UnitTypeName = """ & fTakeOutQuotes(UnitTypeName) & """, UnitTypeDescription = " & NL &
                          """" & fTakeOutQuotes(UnitTypeDescription) & """, SubType= " & NL &
                          """" & fTakeOutQuotes(SubType) & """, [Level]=" & NL &
                          """" & fTakeOutQuotes(lsLevel) & """, ModelUnit=" & NL &
                          """" & fTakeOutQuotes(ModelUnit) & """, UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where UnitTypeID=" & UnitTypeID & ";"
        '*** Run The SQL.
        UpdateUnitTypes = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function


    '****************************************************
    '*** Company Information
    Public Function LoadCompanyInfo(ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal llID As Long) As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadCompanyInfo = Nothing
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT *                         " & NL &
                "FROM   tblCompanyInfo            " & NL &
                "WHERE  NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") *
                    IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                    IIf(llID > 0, " OR CompanyID = " & llID & " ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "CompanyInfo")

        mdsCompanyInfo = ds
        LoadCompanyInfo = mdsCompanyInfo
    End Function
    Public Function DeleteCompanyInfo(ByVal RecordID As Long) As Boolean
        Dim lsSQL As String = "Update tblCompanyInfo Set Active=False  WHERE  CompanyInfoID=" & RecordID
        DeleteCompanyInfo = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function InsertCompanyInfo(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertCompanyInfo = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "INSERT INTO tblVendors ( VendorName, VendorContact, VendorAbbreviation, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( """ & fTakeOutQuotes(lsName) & """, " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                          """" & Now.ToString & """, " & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, " & NL &
                           "true, " & llNodeID & "); "
        '*** Run The SQL.
        InsertCompanyInfo = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function
    Public Function UpdateCompanyInfo(ByVal llNodeID As Long, ByVal lsName As String, ByVal lsContact As String, ByVal lsAbbreviation As String, ByVal lsActive As String, ByVal VendorID As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        UpdateCompanyInfo = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblVendors  " &
                "Set VendorName = """ & fTakeOutQuotes(lsName) & """, VendorContact = " & NL &
                          """" & fTakeOutQuotes(lsContact) & """, VendorAbbreviation= " & NL &
                          """" & fTakeOutQuotes(lsAbbreviation) & """, UpdateDate=" & NL &
                          """" & Now.ToString & """, UpdateUser=" & NL &
                          """" & fTakeOutQuotes(lsCurrentUser) & """, Active = " & lsActive & NL &
                 " Where VendorID=" & VendorID & ";"
        '*** Run The SQL.
        UpdateCompanyInfo = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

    Function fGetUser() As String

        fGetUser = System.Web.HttpContext.Current.User.Identity.GetUserId()
    End Function


    '***************************************
    Public Function LoadReportCategories() As DataSet
        Dim lsSQL As String

        '*** Initialize
        LoadReportCategories = Nothing

        lsSQL = "Select * from tblReportCategories"

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Categories")

        mdsReportCategories = ds
        LoadReportCategories = mdsReportCategories
    End Function
    Public Function LoadAllControls() As DataSet
        Dim NL As String = Chr(13) & Chr(10)
        Dim lsSQL As String

        '*** Initialize
        LoadAllControls = Nothing

        lsSQL = "Select * from tblReportControls"

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Controls")

        mdsAllControls = ds
        LoadAllControls = mdsAllControls
    End Function
    Public Function LoadReportControls(ByVal llReportID As Long) As DataSet
        Dim lscnType As String = "OLEDB"
        Dim lsSQL As String

        '*** Initialize
        LoadReportControls = Nothing

        lsSQL = "SELECT tblReportControls.*, " &
                "       tblReportFields.* " &
                "From   tblReportFields inner join tblReportControls on " &
                "       tblreportfields.reportcontrol = tblreportcontrols.controlid " &
                "WHERE  ReportID= " & llReportID

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "ReportControls")

        mdsReportControls = ds
        LoadReportControls = mdsReportControls
    End Function

    Public Function LoadListItems(ByVal lsCnStr As String, ByVal lsSQL As String) As DataSet
        Dim lscnType As String = "OLEDB"
        On Error GoTo LoadListItemsError

        LoadListItems = New DataSet
        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(lscnType, lsCnStr, lsSQL, "ListItems")

        mdsListItems = ds
        LoadListItems = mdsReportControls

LoadListItemsExit:
        Exit Function
LoadListItemsError:
        MsgBox(Err.Description)
        Resume LoadListItemsExit
    End Function

    Public Function LoadChildren(ByVal lsCnStr As String, ByVal lsSQL As String) As DataSet
        Dim lscnType As String = "OLEDB"

        On Error GoTo LoadChildrenError
        LoadChildren = New DataSet

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(lscnType, lsCnStr, lsSQL, "Children")

        mdsChildren = ds
        LoadChildren = mdsReportControls

LoadChildrenExit:
        Exit Function
LoadChildrenError:
        MsgBox(Err.Description)
        Resume LoadChildrenExit
    End Function
    '*******************************************************
    '*** Images
    '*******************************************************
    Public Function LoadImages(lsObjectID As String) As DataSet
        'ByVal llNodeID As Long, ByVal lsWhere As String, ByVal lbActive As Boolean, ByVal lsID As Long) As DataSet
        Dim llNodeID As Long = 2
        Dim lsWhere As String = ""
        Dim lbActive As Boolean = True
        Dim lsID As String = ""

        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        '*** Initialize
        LoadImages = Nothing
        llNodeID = 2
        If lsWhere = Nothing Then lsWhere = ""
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If
        lsSQL = "SELECT * " & NL &
                "FROM tblImages                                   " & NL &
                "WHERE ( tblImages.NodeID=" & llNodeID & " " & IIf(lsWhere.Length > 4, " and " & lsWhere, "") &
                     IIf(lbActive = True, " and Active = 1 ", "") & ")" & NL &
                               IIf(lsObjectID <> "", " and ( ObjectID = '" & lsObjectID & "'", "") & ")" & NL &
                     IIf(lsID <> "", " OR ID = '" & lsID & "' ", "")


        '*** Load a data set.
        Dim ds As New DataSet()
        ' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Projects")
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Projects")
        mdsProjectTypes = ds
        LoadImages = mdsProjectTypes
    End Function
    Public Function DeleteImages(ByVal RecordID As String) As Boolean
        Dim lscnStr As String = mscnDefault
        Dim lsSQL As String = "Update tblImage Set Active=False  WHERE  ID='" & RecordID & "'"
        DeleteImages = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    Public Function InsertImages(ByVal lsObjectID As String, ByVal llNodeID As Long, ByVal lsName As String,
                                 lsDescription As String, ByVal liOrder As Integer, ByVal lsImage As Byte(),
                                 ByVal lsType As String, lsURL As String) As Boolean
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        InsertImages = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If

        msSQLParameter = New SqlParameter("@pImage", SqlDbType.Image)
        msSQLParameter.Value = lsImage

        lsSQL = "INSERT INTO tblImages ( ObjectID, Name,  Description, Image, Type, ImageOrder, " &
                "                         UpdateDate, UpdateUser, CreateDate, CreateUser, Active, NodeID ) " &
                "Values ( '" & fTakeOutQuotes(lsObjectID) & "', " & NL &
                          "'" & fTakeOutQuotes(lsName) & "', " & NL &
                          "'" & fTakeOutQuotes(lsDescription) & "', " & NL &
                          "@pImage " & fTakeOutQuotes("") & ", " & NL &
                          "'" & fTakeOutQuotes(lsType) & "', " & NL &
                           "" & fTakeOutQuotes(liOrder) & ", " & NL &
                          "'" & Now.ToString & "', " & NL &
                          " convert(uniqueidentifier,'" & fTakeOutQuotes(lsCurrentUser) & "'), " & NL &
                          "'" & Now.ToString & "', " & NL &
                          " convert(uniqueidentifier,'" & fTakeOutQuotes(lsCurrentUser) & "'), " & NL &
                           "1, " & llNodeID & "); "
        '*** Run The SQL.
        InsertImages = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function


    Public Function Updateimages(ByVal lsObjectID As String, ByVal llNodeID As Long, ByVal lsName As String, ByVal lsDescription As String, ByVal lsImage As String, lsImageURL As String, ByVal lsActive As String, ByVal ID As String, ByVal liOrder As Integer) As Boolean
        Dim lsSQL As String
        Dim lscnStr As String = mscnDefault
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString
        '*** Initialize
        Updateimages = False
        '*** Check for No Selected Category
        If llNodeID = 0 Then
            Exit Function
        End If
        lsSQL = "Update tblImages  " &
                "Set Name = '" & fTakeOutQuotes(lsName) & "', Description = " & NL &
                          "'" & fTakeOutQuotes(lsDescription) & "', image= " & NL &
                          "'" & fTakeOutQuotes(lsImage) & "', ImageOrder= " & NL &
                           "" & fTakeOutQuotes(liOrder) & ", ImageURL= " & NL &
                          "'" & fTakeOutQuotes(lsImageURL) & "', UpdateDate=" & NL &
                          "'" & Now.ToString & "', UpdateUser=" & NL &
                          "'" & fTakeOutQuotes(lsCurrentUser) & "', Active = " & lsActive & NL &
                 " Where ID='" & ID & "';"
        '*** Run The SQL.
        Updateimages = fRunSQL("SQLConnection", lscnStr, lsSQL)
    End Function
    '  Private Function CheckdbConnection(ByVal lsConnectionType, ByVal lsConnectionString) As Boolean
    '      If cn Is Nothing Then
    '          cn = New Data.SqlClient.SqlConnection
    '      End If
    '      If cn.State = ConnectionState.Closed Then
    '  Dim lscn As String
    '          lscn = "DRIVER={Microsoft Access Driver (*.mdb)};DBQ=c:\data\parallax\pinnacle\pinnacle data.mdb"
    '          lscn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=c:\data\parallax\pinnacle\pinnacle data.mdb;User Id=admin;Password=;"
    '          cn.ConnectionString = lscn
    '  '"Server=azshdsd17;user id=scored;password=scored;Database=Quality;Pooling=false;"
    '          cn.Open()
    '      End If
    '      If cn.State <> ConnectionState.Open Then
    '          CheckdbConnection = False
    '      Else
    '         CheckdbConnection = True
    '     End If
    ' End Function
    '*******************************************************
    '*** Adhoc
    '*******************************************************
    Public Function LoadAdhoc(ByVal llNodeID As Long, ByVal lsSQL As String, ByVal lbActive As Boolean, ByVal lsID As String, lsObjectID As String) As DataSet

        Dim lscnStr As String = mscnDefault
        '*** Initialize
        LoadAdhoc = Nothing
        llNodeID = 2

        If llNodeID = 0 Then
            'MsgBox("No Project Selectedd")
            Exit Function
        End If

        '*** Load a data set.
        Dim ds As New DataSet()
        ' ds = fGetDataset(mscnType, lscnStr, lsSQL, "Contacts")
        ds = fGetDataset("SQLConnection", lscnStr, lsSQL, "Contacts")
        mdsContacts = ds
        LoadAdhoc = mdsContacts
    End Function
    Private Function fGetDataset(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal lsSQL As String, ByVal lsTableName As String) As DataSet
        Dim ds As DataSet = New DataSet
        '    Threading.Thread.Sleep(5000)
        Select Case lsConnectionType
            Case "SQLConnection"
                cns = New SqlClient.SqlConnection
                cns.ConnectionString = lsCn
                cns.Open()
                ' CheckdbConnection("SQLServer", lsCn)
                '*** Set up a data set command object.
                lsSQL = lsSQL.Replace("False", "0")
                lsSQL = lsSQL.Replace("True", "1")
                Dim dscmd As New SqlDataAdapter(lsSQL, cns)

                '*** Load a data set.
                dscmd.Fill(ds, lsTableName)
                cns.Close()
            Case "OLEDB"
                cno = New OleDb.OleDbConnection
                cno.ConnectionString = lsCn
                cno.Open()
                ' CheckdbConnection("OLEDB", lsCn)
                '*** Set up a data set command object.
                Dim dscmdO As New OleDb.OleDbDataAdapter(lsSQL, cno)

                '*** Load a data set.
                dscmdO.Fill(ds, lsTableName)
                cno.Close()
        End Select
        fGetDataset = ds
    End Function
    Public Function fExecuteSQLCmd(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal cms As SqlCommand) As Boolean

        Select Case lsConnectionType
            Case "SQLConnection"
                cns = New SqlClient.SqlConnection

                cns.ConnectionString = lsCn
                cns.Open()
                cms.Connection = cns


                cms.ExecuteNonQuery()
                msSQLParameter = Nothing
                cns.Close()
            Case "OLEDB"
                cno = New OleDb.OleDbConnection
                cno.ConnectionString = lsCn
                cno.Open()
                ' CheckdbConnection("OLEDB", lsCn)
                '*** Set up a data set command object.

                Dim cmo As New OleDb.OleDbCommand
                cmo.Connection = cno
                cmo.CommandText = "" 'lsSQL
                cmo.ExecuteNonQuery()
                cno.Close()
        End Select
        fExecuteSQLCmd = True
    End Function
    Public Function fRunSQL(ByVal lsConnectionType As String, ByVal lsCn As String, ByVal lsSQL As String) As Boolean

        Select Case lsConnectionType
            Case "SQLConnection"
                cns = New SqlClient.SqlConnection
                Dim cms As New SqlClient.SqlCommand
                cns.ConnectionString = lsCn
                cns.Open()
                cms.Connection = cns
                cms.CommandText = lsSQL
                If msSQLParameter IsNot Nothing Then
                    cms.Parameters.Add(msSQLParameter)
                End If
                cms.ExecuteNonQuery()
                msSQLParameter = Nothing
                cns.Close()
            Case "OLEDB"
                cno = New OleDb.OleDbConnection
                cno.ConnectionString = lsCn
                cno.Open()
                ' CheckdbConnection("OLEDB", lsCn)
                '*** Set up a data set command object.

                Dim cmo As New OleDb.OleDbCommand
                cmo.Connection = cno
                cmo.CommandText = lsSQL
                cmo.ExecuteNonQuery()
                cno.Close()
        End Select
        fRunSQL = True
    End Function
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    Function fTakeOutQuotes(ByVal lsStr As String) As String
        lsStr = Replace(lsStr, "script", "scri_pt")
        lsStr = Replace(lsStr, """", """""")
        lsStr = Replace(lsStr, "'", "''")
        fTakeOutQuotes = Trim(lsStr)
    End Function

    Public Function TrimAll(ByVal TextIn As String, Optional ByVal TrimChar As String = " ", Optional ByVal CtrlChar As String = Chr(0)) As String

        Try ' Replace ALL Duplicate Characters in String with a Single Instance
            TrimAll = Replace(TextIn, TrimChar, CtrlChar)   ' In case of CrLf etc.

            While InStr(TrimAll, CtrlChar + CtrlChar) > 0
                TrimAll = TrimAll.Replace(CtrlChar + CtrlChar, CtrlChar)
            End While

            TrimAll = TrimAll.Trim(CtrlChar)                ' Trim Begining and End
            TrimAll = TrimAll.Replace(CtrlChar, TrimChar)   ' Replace with Original Trim Character(s)
        Catch Exp As Exception
            TrimAll = TextIn    ' Justin Case
        End Try

        Return TrimAll

    End Function
    Public Function fReplaceQuotes(ByVal ls As String) As String
        fReplaceQuotes = Replace(ls, "'", "''")
    End Function
    Function fcheckforIncompatibilities(ByVal llQuoteID As Long,
            ByVal lsRoomDesc As String,
            ByVal lsUpgradeDesc As String,
            ByVal lsUpgradeCategory As String,
            ByVal lsUpgradeClass As String,
            ByVal lsStyle As String,
            ByVal llUpgradeOptionID As Long, ByRef lsMsg As String) As Boolean

        fcheckforIncompatibilities = False
        Dim lsUnit As String
        Dim lsUnitType As String
        Dim lsSQL As String
        Dim d As DataSet

        Dim rs As DataTable
        Dim rs2 As DataTable
        Dim r As DataRow
        Dim r2 As DataRow
        lsSQL = "SELECT tblUnits.UnitName, tblUnitTypes.UnitTypeName, tblQuote.QuoteID " &
                "FROM tblUnitTypes LEFT JOIN (tblUnits LEFT JOIN tblQuote ON tblUnits.UnitID = tblQuote.UnitID) ON " &
                "tblUnitTypes.UnitTypeID = tblUnits.UnitTypeID " &
                "WHERE (((tblQuote.QuoteID)=" & llQuoteID & "));"

        d = fGetDataset(mscnType, mscnStr, lsSQL, "Comapt")
        rs = d.Tables(0)
        lsUnit = rs.Rows(0).Item(0).ToString
        lsUnitType = rs.Rows(0).Item(1).ToString


        Dim liUnit As Integer
        Dim liSection As Integer
        Dim lsUnitText As String
        Dim lsRoomText As String
        Dim liRoom As Integer
        Dim liSectionOther As Integer
        '*** Loop for Unit, Then Unittype
        For liUnit = 1 To 2
            If liUnit = 1 Then
                lsUnitText = " EntityType = 2 and EntityID = '" & lsUnit & "' "
            Else
                lsUnitText = " EntityType = 1 and EntityID = '" & lsUnitType & "' "
            End If
            '*** Loop for first Section, Then Second
            For liSection = 1 To 2
                If liSection = 1 Then
                    liSectionOther = 2
                Else
                    liSectionOther = 1
                End If
                '*** Loop For Room
                For liRoom = 1 To 2
                    If liRoom = 1 Then
                        lsRoomText = " Location" & liSection & " = '" & lsRoomDesc & "'  "
                    Else
                        lsRoomText = " Location" & liSection & " = '(Anywhere)'  "
                    End If
                    lsSQL = "Select * from tblIncompatibilities where " & lsUnitText & " AND " & lsRoomText
                    lsSQL = lsSQL & " AND (Category" & liSection & "=""" & fReplaceQuotes(lsUpgradeCategory) & """ OR Category" & liSection & "='(Anything)' ) "
                    lsSQL = lsSQL & " AND (Class" & liSection & "=""" & fReplaceQuotes(lsUpgradeClass) & """ OR Class" & liSection & "='(Anything)' )  "
                    lsSQL = lsSQL & " AND (Description" & liSection & "=""" & fReplaceQuotes(lsUpgradeDesc) & """ OR Description" & liSection & "='(Anything)' )  "
                    lsSQL = lsSQL & " AND (ModelOrStyle" & liSection & "=""" & fReplaceQuotes(lsStyle) & """ OR ModelOrStyle" & liSection & "='(Anything)' )"
                    lsSQL = lsSQL & " AND (severity = 1 or severity = 2 )"
                    'Debug.Print(lsSQL)

                    d = fGetDataset(mscnType, mscnStr, lsSQL, "compat")
                    rs = d.Tables("compat")


                    If rs.Rows.Count > 0 Then
                        r = rs.Rows(0)
                        ' Debug.Print("Found First: " & lsSQL)
                        '*** Found THIS item in Inc List, Let's check for another in the requested Upgrades
                        lsSQL = "Select * from tblRequestedUpgrades where quoteid = " & llQuoteID & " "
                        If r("Location" & liSectionOther) <> "(Anywhere)" Then
                            lsSQL = lsSQL & " AND RoomDescription = '" & r("Location" & liSectionOther) & "' "
                        End If

                        If r("Category" & liSectionOther) <> "(Anything)" Then
                            lsSQL = lsSQL & " AND UpgradeCategory = """ & fReplaceQuotes(r("Category" & liSectionOther)) & """ "
                        End If

                        If r("Description" & liSectionOther) <> "(Anything)" Then
                            lsSQL = lsSQL & " AND UpgradeDescription = """ & fReplaceQuotes(r("Description" & liSectionOther)) & """ "
                        End If

                        If r("Class" & liSectionOther) <> "(Anything)" Then
                            lsSQL = lsSQL & " AND UpgradeClass = """ & fReplaceQuotes(r("Class" & liSectionOther)) & """ "
                        End If

                        If r("ModelOrStyle" & liSectionOther) <> "(Anything)" Then
                            lsSQL = lsSQL & " AND StyleDescription" & liSectionOther & " = """ & fReplaceQuotes(r("ModelOrStyle" & liSectionOther)) & """ "
                        End If
                        '   Debug.Print(lsSQL)

                        d = fGetDataset(mscnType, mscnStr, lsSQL, "rs2")
                        rs2 = d.Tables("rs2")
                        If rs2.Rows.Count > 0 Then
                            r2 = rs2.Rows(0)
                            lsMsg = lsMsg & ("The item you are trying to add is incompatible with the following Rule.")
                            lsMsg = lsMsg & r2.Item("IncompatibilityID").ToString '  .DoCmd.OpenForm("frmIncompatMSG", acNormal, , "IncompatibilityID=" & rs("IncompatibilityID"), acFormReadOnly, acDialog)
                            '*** gbContinue comes back from the frmIncompatMSG form and indicates whether the
                            '*** item was only a warning and the user said Continue or whether is was denied
                            '    If Session("gbContinue") = True Then
                            fcheckforIncompatibilities = False
                            'Else
                            '   fcheckforIncompatibilities = True
                            'End If
                        End If
                        rs2.Dispose()
                        ' rs2.Close()
                        ' rs.MoveNext()
                    End If
                    rs.Dispose()
                    'rs.Close()
                Next liRoom
                '*** End Room Loop
            Next liSection
            '*** End Section Loop
        Next liUnit
        '*** End Unit Loop
        rs = Nothing
        rs2 = Nothing
    End Function
    '*************************************
    '*** Data Validation Functions
    '*************************************


    Dim gbContinue As Boolean
    Dim gbMissingSelections As Boolean
    Sub test()
        Dim dt As DataTable = New DataTable
        fRequired(1, 2, dt)
    End Sub
    Function fRequired(ByVal llQuoteID As Long, ByVal llPhaseID As Long, ByVal dt As DataTable) As Boolean
        fRequired = fCheckForRequiredItems(llQuoteID, llPhaseID, dt)

        If fRequired = True Then
            Exit Function
        Else
            fRequired = fMissingSelections(llQuoteID, "Form", llPhaseID)
        End If
    End Function
    Sub testreq()
        Dim dt As New DataTable
        MsgBox(fRequired(1, 2, dt))
    End Sub

    Function fCheckForRequiredItems(ByVal llQuoteID As Long, ByVal llPhaseID As Long, ByVal dtIncompat As DataTable) As Boolean
        '*** THIS IS FROM THE INCOMPATIBILITIES LIST
        Dim d As DataSet
        Dim r As DataRow
        Dim rsRule As DataTable 'ADODB.Recordset
        Dim rs As DataTable 'ADODB.Recordset
        Dim rs2 As DataTable 'ADODB.Recordset
        Dim lsUnit As String
        Dim lsUnitType As String
        Dim lsSQL As String
        Dim liUnit As Integer
        Dim liSection As Integer
        Dim lsUnitText As String
        Dim lsRoomText As String
        Dim liRoom As Integer
        Dim liSectionOther As Integer


        Dim lsRoomDesc As String
        Dim lsUpgradeDesc As String
        Dim lsUpgradeCategory As String
        Dim lsUpgradeClass As String
        Dim lsStyle As String
        Dim llUpgradeOptionID As Long


        If llPhaseID = 1 Then
            fCheckForRequiredItems = False
            Exit Function
        End If
        On Error GoTo fCheckForRequiredItems_Error

        fCheckForRequiredItems = False
        rsRule = New DataTable ' ADODB.Recordset
        rs2 = New DataTable 'ADODB.Recordset
        rs = New DataTable 'ADODB.Recordset

        '*** Get Unit and Unit Type for this Quote
        lsSQL = "SELECT tblUnits.UnitName, tblUnitTypes.UnitTypeName, tblQuote.QuoteID " &
           "FROM tblUnitTypes LEFT JOIN (tblUnits LEFT JOIN tblQuote ON tblUnits.UnitID = tblQuote.UnitID) ON " &
           "tblUnitTypes.UnitTypeID = tblUnits.UnitTypeID " &
           "WHERE (((tblQuote.QuoteID)=" & llQuoteID & "));"

        d = fGetDataset(mscnType, mscnStr, lsSQL, "Required")
        rs = d.Tables("Required")


        If rs.Rows.Count > 0 Then
            r = rs.Rows(0)

            lsUnit = r(0)
            lsUnitType = r(1)
            rs.Dispose()


            '*** Loop for Unit, Then UnitType
            For liUnit = 1 To 2
                If liUnit = 1 Then
                    lsUnitText = " EntityType = 2 and EntityID = '" & lsUnit & "' "
                Else
                    lsUnitText = " EntityType = 1 and EntityID = '" & lsUnitType & "' "
                End If

                '*** Loop for first Section, Then Second
                For liSection = 1 To 1 '2 *** Do not want to go backward at this juncture  Only Check First Item against Second, Not Second Against First
                    If liSection = 1 Then
                        liSectionOther = 2
                    Else
                        liSectionOther = 1
                    End If

                    '*** Get all required Items for that Unit / Unit Type
                    lsSQL = "Select * from tblIncompatibilities where " & lsUnitText
                    lsSQL = lsSQL & " AND (Severity = 3 )  "

                    ' Debug.Print(lsSQL)
                    d = fGetDataset(mscnType, mscnStr, lsSQL, "Rules")
                    rsRule = d.Tables("Rules")

                    Dim liRowCount As Integer
                    If rsRule.Rows.Count > 0 Then
                        For liRowCount = 0 To rsRule.Rows.Count - 1
                            r = rsRule.Rows(liRowCount)
                            'rsRule.Open(lsSQL, CurrentProject.Connection)


                            lsRoomDesc = r("Location" & liSection)
                            lsUpgradeDesc = r("Description" & liSection)
                            lsUpgradeCategory = r("Category" & liSection)
                            lsUpgradeClass = r("Class" & liSection)
                            lsStyle = r("ModelOrStyle" & liSection)


                            '*** Was one of the Base Items Selected?
                            lsSQL = "Select * from tblRequestedUpgrades where quoteid = " & llQuoteID & " "
                            If lsRoomDesc <> "(Anywhere)" Then lsSQL = lsSQL & " AND RoomDescription = '" & lsRoomDesc & "' "
                            If lsUpgradeCategory <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeCategory = '" & lsUpgradeCategory & "' "
                            If lsUpgradeDesc <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeDescription = '" & lsUpgradeDesc & "' "
                            If lsUpgradeClass <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeClass = '" & lsUpgradeClass & "' "
                            If lsStyle <> "(Anything)" Then lsSQL = lsSQL & " AND StyleDescription" & liSectionOther & " = '" & lsStyle & "' "
                            ' Debug.Print(lsSQL)

                            ' rs.Open(lsSQL, CurrentProject.Connection)
                            d = fGetDataset(mscnType, mscnStr, lsSQL, "Found")
                            rs = d.Tables("Found")

                            Dim liRowCount2 As Integer
                            If rs.Rows.Count > 0 Then
                                '*** We Found Something
                                '*** Found THIS item in Inc List, Let's check for another in the requested Upgrades
                                lsSQL = "Select * from tblRequestedUpgrades where quoteid = " & llQuoteID & " "
                                If r("Location" & liSectionOther) <> "(Anywhere)" Then lsSQL = lsSQL & " AND RoomDescription = '" & r("Location" & liSectionOther) & "' "
                                If r("Category" & liSectionOther) <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeCategory = '" & r("Category" & liSectionOther) & "' "
                                If r("Description" & liSectionOther) <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeDescription = '" & r("Description" & liSectionOther) & "' "
                                If r("Class" & liSectionOther) <> "(Anything)" Then lsSQL = lsSQL & " AND UpgradeClass = '" & r("Class" & liSectionOther) & "' "
                                If r("ModelOrStyle" & liSectionOther) <> "(Anything)" Then lsSQL = lsSQL & " AND StyleDescription" & liSectionOther & " = '" & r("ModelOrStyle" & liSectionOther) & "' "

                                '  Debug.Print(lsSQL)

                                ' rs2.Open(lsSQL, CurrentProject.Connection)
                                d = fGetDataset(mscnType, mscnStr, lsSQL, "Found2")
                                rs2 = d.Tables("Found2")
                                Dim r2 As DataRow

                                Dim liRowCount3 As Integer
                                If rs2.Rows.Count = 0 Then

                                    MsgBox("An item you selected requires you to select another item compatible with the following Rule.")

                                    dtIncompat = New DataTable ' DoCmd.OpenForm("frmIncompatMSG", acNormal, , "IncompatibilityID=" & rsRule("IncompatibilityID"), acFormReadOnly, acDialog)
                                    '*** gbContinue comes back from the frmIncompatMSG form and indicates whether the
                                    '*** item was only a warning and the user said Continue or whether is was denied

                                    fCheckForRequiredItems = True

                                End If
                                rs2.Dispose()

                            End If
                            rs.Dispose()


                            ''*** End Room Loop

                        Next
                    End If
                    rsRule.Dispose()
                    '*** End Rule Loop

                Next liSection
                '*** End Section Loop
            Next liUnit
            '*** End Unit Loop

            rs = Nothing
            rs2 = Nothing
            rsRule = Nothing

        End If

fCheckForRequiredItems_Exit:
        Exit Function

fCheckForRequiredItems_Error:
        MsgBox(Err.Description & " " & Err.Number)
        Resume fCheckForRequiredItems_Exit
    End Function
    Sub testmissing()
        fMissingSelections(1, "", True)
    End Sub
    Function fMissingSelections(ByVal llQuoteID As Long, ByVal lsOutput As String, ByVal llPhase As Long) As Boolean
        Dim lsSQL As String

        lsSQL = "Delete * from tempselections Where quoteid = " & llQuoteID
        fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)

        lsSQL = "Insert into TempSelections (   UnitType, Location, UpgradeCategory, " &
            "         QuoteID, BuildingPhase, UnitName, ValueExists ) " &
            "SELECT   tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeCategory, " &
            "         tblQuote.QuoteID, tblUpgradeOptions.BuildingPhase, tblUnits.UnitName, False AS ValueExists " &
            "FROM     (tblUnitTypes LEFT JOIN (tblUnits LEFT JOIN tblQuote ON tblUnits.UnitID = tblQuote.UnitID)  " &
            "         ON tblUnitTypes.UnitTypeID = tblUnits.UnitTypeID) INNER JOIN tblUpgradeOptions ON  " &
            "         tblUnitTypes.UnitTypeName = tblUpgradeOptions.UnitType " &
            "GROUP BY tblUpgradeOptions.UnitType, tblUpgradeOptions.Location, tblUpgradeOptions.UpgradeCategory,  " &
            "         tblQuote.QuoteID, tblUpgradeOptions.BuildingPhase, tblUnits.UnitName, False " &
            "HAVING   tblUpgradeOptions.BuildingPhase = " & llPhase & " And tblQuote.QuoteID=" & llQuoteID & ";"

        'Debug.Print(lsSQL)

        fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)

        '*** Take out Electrical
        lsSQL = "Delete * from tempselections where ucase(left(upgradeCategory,10)) = 'ELECTRICAL'"
        fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)

        '*** Update record if Value has been selected
        lsSQL = "UPDATE tblRequestedUpgrades INNER JOIN tempSelections ON " &
                "      (tblRequestedUpgrades.UpgradeCategory = tempSelections.UpgradeCategory) AND  " &
                "      (tblRequestedUpgrades.RoomDescription = tempSelections.Location) AND  " &
                "      (tblRequestedUpgrades.QuoteID = tempSelections.QuoteID)  " &
                "      SET tempSelections.ValueExists = True;"

        'Debug.Print(lsSQL)
        fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)

        '*** Update the Required Field Based on the CategorySummary Table
        lsSQL = "UPDATE tempSelections INNER JOIN tblUpgradeCategorySummary ON " &
                "       tempSelections.UpgradeCategory = tblUpgradeCategorySummary.UpgradeCategory " &
                "SET    tempSelections.Required = True " &
                "WHERE  tblUpgradeCategorySummary.Required=True;"

        'Debug.Print(lsSQL)
        fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)


        '*** Delete Rows where Upgrade Record Exists for the unit but Room is not defined in the
        '*** Unit Profile
        lsSQL = "SELECT tblUnitTypes.UnitTypeName, tblUnitTypes.UnitTypeDescription, " &
                "       tblUnitTypes.UnitTypeID, tblUnitProfiles.RoomName,  " &
                "       tblUnitProfiles.RoomDescription, tblQuote.QuoteID " &
                "FROM (tblUnitTypes LEFT JOIN tblUnitProfiles ON  " &
                "       tblUnitTypes.UnitTypeID = tblUnitProfiles.UnitTypeID)  " &
                "       LEFT JOIN (tblUnits LEFT JOIN tblQuote ON  " &
                "       tblUnits.UnitID = tblQuote.UnitID) ON  " &
                "       tblUnitTypes.UnitTypeID = tblUnits.UnitTypeID " &
                "WHERE  tblQuote.QuoteID = " & llQuoteID & ";"

        '*** Load a data set.
        Dim ds As New DataSet()
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "Rooms")
        Dim dt As New DataTable
        dt = ds.Tables(0)
        Dim dr As DataRow

        lsSQL = "Delete * from TempSelections Where 1=1 "
        For Each dr In dt.Rows
            lsSQL = lsSQL & " and location <> """ & fReplaceQuotes(dr("RoomName")) & """ "
        Next
        dt.Dispose()

        'Debug.Print(lsSQL)
        fMissingSelections = fRunSQL(mscnType, mscnStr, lsSQL)


        lsSQL = "select * from tempselections where valueexists = false"
        ds = fGetDataset(mscnType, mscnStr, lsSQL, "TempSelections")

        If ds.Tables(0).Rows.Count > 0 Then
            '*** There are No Values missing
            fMissingSelections = True

        Else
            If lsOutput = "Report" Then
                '*** Put this back DoCmd.OpenReport("rptSelectedItems", acViewPreview, , , acWindowNormal)
            Else
                '*** Put this back DoCmd.OpenForm("frmmissingselections", acNormal, , , acFormReadOnly, acDialog, "Visible")
            End If

            If gbMissingSelections = True Then
                fMissingSelections = False
            Else
                fMissingSelections = True
            End If
        End If
        dt = Nothing
        ds = Nothing

    End Function

    Public Function UpdateQuoteStatus(ByVal llQuoteID As Long, ByVal lsPhase As String, ByVal lsStatus As String, ByVal lsPhaseDate As String, ByVal lsDate As String) As Boolean
        Dim lsSQL As String
        Dim lsCurrentUser As String = fGetUser() ' Membership.GetUser.ToString

        '*** Initialize
        UpdateQuoteStatus = False
        '*** Check for No Selected Category
        If llQuoteID = 0 Or lsStatus = "" Then
            Exit Function
        End If

        lsSQL = "Update tblQuote  " &
                "Set " & lsPhase & " = """ & fTakeOutQuotes(lsStatus) & """,  " & NL &
                          lsPhaseDate & " = " & lsDate & " " & NL &
                 " Where QuoteID=" & llQuoteID & ";"
        '*** Run The SQL.
        UpdateQuoteStatus = fRunSQL(mscnType, mscnStr, lsSQL)
    End Function

End Class
