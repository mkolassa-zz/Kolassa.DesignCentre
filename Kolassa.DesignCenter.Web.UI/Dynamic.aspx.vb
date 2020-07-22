Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OLEdb
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls


Partial Public Class DynamicTemplates : Inherits System.Web.UI.Page

    '    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) ' Handles Button1.Click
    '        Dim PS As New SPRJ.ProjectsService
    '        Dim Log As New SPRJ.Login
    '        Dim Auth As New SPRJ.Auth

    '        Dim SoapXML As New System.Xml.XmlDocument
    '        '  Dim DataBus As System.Xml.XmlElement
    '        Dim XOGOutput As System.Xml.XmlElement
    '        Dim sessionId As String = "Not Set"
    '        ' Me.txtQuery.Text = sessionId
    '        Log.Password = "pmoadmin"
    '        Log.Username = "Illini"
    '        sessionId = PS.Login(Log)
    '        '     ' sessionId = PS.Login("pmoadmin", "Illini")
    '        txtXOG.Text = PS.ConnectionGroupName & PS.Site.ToString & PS.ToString
    '        If Not sessionId Is Nothing Then
    '            txtXOG.Text = txtXOG.Text & sessionId
    '        Else
    '            txtXOG.Text = txtXOG.Text & "Nothing"
    '        End If
    '        Auth.SessionID = sessionId
    '        PS.AuthValue = Auth

    '        SoapXML.Load("c:\Program Files\Clarity\xog\custom\prj_projects_read.xml")
    '        ' txtDebug2.Text = SoapXML.InnerXml
    '        XOGOutput = PS.ReadProject(SoapXML.DocumentElement)
    '        If Not XOGOutput Is Nothing Then
    '            MsgBox(XOGOutput.InnerXml)
    '        Else

    '        End If


    '        PS.Logout(sessionId)
    '        ' For Each wsRecord In 
    '        ' Debug.Print(wsRecord.ice_name)
    '        ' Next

    '    End Sub
    '    Protected Sub Button1_c2(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '        On Error GoTo XOG_Error
    '        Dim QS As New Q.res_managerQueryService
    '        Dim Qu As New Q.res_managerQuery
    '        Dim Auth As New Q.Auth

    '        Dim Log As New Q.Login
    '        Dim ClaritySessionID As String

    '        Log.Username = "pmoadmin"
    '        Log.Password = "illini"

    '        ClaritySessionID = QS.Login(Log)
    '        Auth.SessionID = ClaritySessionID

    '        Dim F As New Q.res_managerFilter
    '        Dim QueryResult As New Q.res_managerQueryResult
    '        Dim Rec As New Q.res_managerRecord()
    '        QS.AuthValue = Auth
    '        Qu.Code = "res_manager"
    '        F.rsrc = txtXOG.Text
    '        Qu.Filter = F
    '        QueryResult = QS.Query(Qu)

    '        If Not QueryResult.Records.Length = 0 Then
    '            txtXOG.Text = QueryResult.Records(0).manager & QueryResult.Records.Length.ToString
    '        Else
    '            txtXOG.Text = "Not Found"
    '        End If
    '        QS.Logout(ClaritySessionID)
    'XOG_Exit:
    '        Exit Sub
    'XOG_Error:
    '        txtXOG.Text = txtXOG.Text & Err.Description & Err.Number
    '        Resume Next
    '    End Sub
    Protected Sub cmdLoadGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLoadGrid.Click
        Dim dl As New clsSelectDataLoader
        Dim ds As New DataSet
        dl = New clsSelectDataLoader
        ds = dl.LoadTables(CLng(ddlTable.SelectedValue), 1, "", True, 0)

        ctrlDynamicGrid1.GetDataSet(ds)
        ctrlDynamicDataGrid1.GetDataSet(ds)
    End Sub
End Class
