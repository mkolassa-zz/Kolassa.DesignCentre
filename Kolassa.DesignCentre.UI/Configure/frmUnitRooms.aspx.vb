Public Class frmUnitRooms
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
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles cmdLoad.Click
        popGrid()
    End Sub
End Class