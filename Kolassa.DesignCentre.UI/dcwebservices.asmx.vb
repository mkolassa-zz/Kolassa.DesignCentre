Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel
Imports AjaxControlToolkit
Imports System.Web.HttpContext

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<System.Web.Script.Services.ScriptService>
<ToolboxItem(False)>
Public Class dcwebservices
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function HelloWorld() As String
        Return "Hello World"
    End Function

    <WebMethod()>
    Public Function getObjectCode(project As String, lsObjectType As String, llNodeID As String) As String 'List(Of newcode)
        Dim qs As String = Context.Request.QueryString("L")

        Dim ProjectID As String = project '1000
        Dim ObjectType As String = lsObjectType '1000
        Dim TableName As String
        Dim errorMessage As String = ""
        Dim NodeID As Long = llNodeID
        TableName = "11112222-3333-4444-5555-666677778888"

        Dim c As New Kolassa.DesignCenter.ReportManager.clsDataLoader
        Dim newcodestring As String = c.fGetNextCode(NodeID, ObjectType, TableName, ProjectID, errorMessage)
        Return newcodestring
        ' Dim nc As New newcode
        ' nc.Code = newcodestring
        ' Dim ncl As New List(Of newcode)
        ' ncl.Add(nc)
        ' Return ncl
    End Function
    <WebMethod()>
    Public Function fchange(ByVal ID As String, lsRoomID As String, ByVal lsUnitTypeID As String, ByVal lsActive As String, lsproject As String, ByVal lsnode As String) As Boolean
        Dim clsup As New clsUnitProfile
        clsup.ProjectID = lsproject
        clsup.NodeID = Val(lsnode)
        clsup.ObjectID = lsproject
        Dim lsProjectID As String = clsup.ProjectID
        clsup.RoomID = lsRoomID
        clsup.ID = ID
        clsup.UnitTypeID = lsUnitTypeID
        clsup.Active = lsActive
        clsup.Update()
        Return True

    End Function
    <WebMethod()>
    Public Function GetCascadingValues(knownCategoryValues As String, category As String, contextKey As String) As CascadingDropDownNameValue()
        Dim kv As StringDictionary = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues)

        Dim modelId As Integer
        Dim values As List(Of CascadingDropDownNameValue) = New List(Of CascadingDropDownNameValue)
        If (kv.ContainsKey("Model") And Integer.TryParse(kv("Model"), modelId)) Then
            Return values.ToArray()
        End If
        Dim lsCat, lsLevel, lsDescription, lsModel, lsPhase, lsUnitType, lsType, lslocation, lsValue, lsWhere, lsProject As String
        Dim c As New clsSelectDataLoader
        Dim ds As DataSet

        lsUnitType = ""
        lsCat = ""
        lsLevel = ""
        lsDescription = ""
        lsModel = ""
        lsPhase = ""
        lsType = ""
        lslocation = ""
        lsProject = "" 'Session("Project")
        For Each kvalue As DictionaryEntry In kv
            Select Case kvalue.Key.ToString.ToLower
                Case "undefined"
                    lsUnitType = kvalue.Value
                Case "category"
                    lsCat = kvalue.Value
                Case "level"
                    lsLevel = kvalue.Value
                Case "description"
                    lsDescription = kvalue.Value
                Case "model"
                    lsModel = kvalue.Value
                Case "location"
                    lslocation = kvalue.Value
                Case "phase"
                    lsPhase = kvalue.Value
            End Select
        Next
        'lsPhase = "2"

        If lsModel = "" Then
            lsType = "model"
            If lsDescription = "" Then
                lsType = "description"
                If lsLevel = "" Then
                    lsType = "level"
                    If lsCat = "" Then
                        lsType = "category"
                        If lslocation = "" Then
                            lsType = "location"
                            If lsPhase = "" Then
                                lsType = "phase"
                            End If
                        End If
                    End If
                End If
            End If
        End If

        lsProject = contextKey

        lsValue = ""
        lsWhere = ""
        ds = c.LoadUpgradeOptionComponents(2, lsUnitType, lsType, lsPhase, lslocation, lsCat, lsLevel,
                                           lsDescription, lsModel, lsValue, lsWhere, 1, lsProject)
        Dim dt As DataTable
        dt = ds.Tables(0)
        If lsType <> "phase" Then
            values.Add(New CascadingDropDownNameValue("(Anything)", "(Anything)"))
        End If
        If dt.Rows.Count = 0 Then
            Else
                For Each dr As DataRow In dt.Rows
                values.Add(New CascadingDropDownNameValue(dr(1), dr(0).ToString()))
            Next
        End If
        Return values.ToArray()
    End Function
End Class
Public Class newcode
    Public Property Code As String
End Class