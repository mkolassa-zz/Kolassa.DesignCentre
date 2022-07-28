Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

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
        Dim ObjectTYpe As String = lsObjectType '1000
        Dim TableName As String
        Dim errorMessage As String = ""
        Dim NodeID As Long = llNodeID
        TableName = "11112222-3333-4444-5555-666677778888"

        Dim c As New Kolassa.DesignCenter.ReportManager.clsDataLoader
        Dim newcodestring As String = c.fGetNextCode(NodeID, ObjectTYpe, TableName, ProjectID, errorMessage)
        Return newcodestring
        ' Dim nc As New newcode
        ' nc.Code = newcodestring
        ' Dim ncl As New List(Of newcode)
        ' ncl.Add(nc)
        ' Return ncl
    End Function

End Class
Public Class newcode
    Public Property Code As String
End Class