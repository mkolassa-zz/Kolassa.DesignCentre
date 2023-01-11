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
Public Class dcConfigure
    Inherits System.Web.Services.WebService

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

End Class