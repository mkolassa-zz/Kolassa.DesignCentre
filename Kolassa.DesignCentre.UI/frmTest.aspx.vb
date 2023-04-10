Imports System.IO
Public Class frmTest
    Inherits System.Web.UI.Page
    Public Sub popGrid()
        Dim cn As New clsSelectDataLoader
        Dim ds As New DataSet
        Dim dtRO, dtUnit, dtRoom As DataTable
        Dim lsProject As String = Session("Project")
        Dim lsNode As String = Session("NodeID")
        ds = cn.LoadCheckGrid(Session("NodeID"), "tblUnitTypes", "tblRooms", lsProject, True, "", "")
        If ds.Tables.Count < 4 Then Exit Sub
        dtRO = ds.Tables(1)
        dtRoom = ds.Tables(3)
        dtUnit = ds.Tables(2)
        If ds.Tables.Count > 0 Then
            gvCrosstab.DataSource = ds.Tables(0)
            gvCrosstab.DataBind()

            '   For Each c As DataControlField In gvCrosstab.Columns
            '   c. = "center"
            '  Next

            gvCrosstab.HeaderRow.CssClass = "rotate"
            Dim liRow As Integer = 0
            Dim liTheCount As Integer = 0
            Dim ctrl As CheckBox
            For Each r As GridViewRow In gvCrosstab.Rows

                For li As Integer = 1 To r.Controls.Count - 1
                    r.Cells(li).Attributes("style") = "text-align: center;"

                    For Each ctrl In r.Cells(li).Controls
                        liTheCount = IIf(IsDBNull(dtRO(liRow)(li)), 0, dtRO(liRow)(li))

                        If liTheCount > 0 Then
                            r.Cells(li).Attributes("style") = "text-align: center;background-color: beige;"
                            ctrl.Enabled = False
                        Else
                            r.Cells(li).Attributes("style") = "text-align: center;"
                            ctrl.Enabled = True
                            '      ctrl.CssClass = "form-check-input"
                            ctrl.Attributes.Add("onclick", "fchange(this)")
                            ctrl.Attributes.Add("data-project", lsProject)
                            ctrl.Attributes.Add("data-node", lsNode)
                            ctrl.Attributes.Add("data-id", dtRoom(liRow)(li).ToString)
                            ctrl.Attributes.Add("data-roomid", dtRoom.Columns(li).ColumnName)
                            ctrl.Attributes.Add("data-unittypeid", dtUnit(liRow)(0).ToString)
                            'AddHandler ctrl.CheckedChanged, AddressOf checkchange
                            '  Dim b As New Button
                            ' r.Cells(li).Controls.Add(b)
                        End If
                    Next
                Next
                liRow = liRow + 1
            Next

        End If
    End Sub
    Sub checkchange(sender As Object, e As EventArgs)
        Dim chk = DirectCast(sender, CheckBox)
        System.Threading.Thread.Sleep(5000)

    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        popGrid()
    End Sub

    Private Sub frmTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        ctrlIncompatibilities1.ProjectID = Session("Project")
        litinfo.Text = MapPath("customerfiles") & "     " & ResolveUrl("customerfiles")
    End Sub

    Protected Sub cmdSendEmail_Click(sender As Object, e As EventArgs) Handles cmdSendEmail.Click
        Dim ds As DataSet = New DataSet
        Dim c As New clsSelectDataLoader
        ds = c.LoadProjects(Session("NodeID"), "", True, "")
        Dim email As New clsEmail
        email.SendNewCustomerEmail("Hey There", "Mike is Great")
        email.SendNewCustomerEmail(ds.Tables(0))
        email.SendNewCustomerEmail(Session("UserEmail"), "Test Email")
        Dim h As New Hashtable
        Dim room As New clsRoom
        room.Code = "Bath"
        room.Name = "Big Bath"
        Dim unit As New clsUnit
        unit.Code = "123"
        unit.Name = "THe Unit"

        h.Add(room, unit)
        email.SendNewCustomerEmail(h, "Test Hashtable", Session("UserEmail"), "Hash table")
        email.SendNewCustomerEmail(Session("UserEmail"), "Email Message", "Email Subject", ds)
    End Sub
    Protected Sub uploadcomplete(sender As Object, e As AjaxControlToolkit.AjaxFileUploadEventArgs) Handles fuCSV.UploadComplete
        Dim c As New clsTestCSV
        Dim lsFileName As String
        Dim fPath As String = "customerfiles"

        If Not Directory.Exists(fPath) Then Directory.CreateDirectory(fPath)

        Dim lsCust As String = Session("NodeID").ToString
        lsCust = "000" & Trim(lsCust)
        lsCust = Right(lsCust, 3)

        '  fPath = fPath & "/cust" & lsCust
        '  If Not Directory.Exists(fPath) Then Directory.CreateDirectory(fPath)
        '  Dim fileNametoupload As String = Server.MapPath(fPath) + "\" + Guid.NewGuid.ToString + e.FileName.ToString()
        '  fileNametoupload = fPath + "\" + Guid.NewGuid.ToString + e.FileName.ToString()

        lsFileName = Server.MapPath(Request.QueryString("ID"))

        lsFileName = lsFileName & "/" & Guid.NewGuid.ToString & Replace(e.FileName, " ", "")
        Try
            fuCSV.SaveAs(lsFileName)
        Catch ex As Exception
            Dim lsEx As String = ex.Message
            fuCSV.Attributes.Add("exception", lsEx)
        End Try
        '     Session("objType") = Request.QueryString("objType")
        '    c.ObjectType = Session("objType")
        '    c.csvReadTest(lsFileName)
        '    c.csvWriteTest()

    End Sub

    Protected Sub cmdfu_Click(sender As Object, e As EventArgs) Handles cmdfu.Click
        Dim s As String = Server.MapPath(txtSaveAs.Text)
        If (fu.HasFile) Then

            fu.SaveAs(s & "/" & fu.FileName)
        End If
    End Sub
End Class