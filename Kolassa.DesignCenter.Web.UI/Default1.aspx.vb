Imports Telerik.Web.UI

Public Class Default1
    Inherits Page


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        If rgMaster.SelectedIndexes.Count = 0 Then
            rgMaster.SelectedIndexes.Add(0)
        Else
            Dim ls As String = rgMaster.SelectedValue
        End If
        If RadGrid2.SelectedIndexes.Count = 0 Then
            RadGrid2.Rebind()
            RadGrid2.SelectedIndexes.Add(0)
        End If
        '    ctrlSubObjects1.Rebind()
    End Sub


    Protected Sub rgMaster_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)
        RadGrid2.SelectedIndexes.Clear()
    End Sub

    Private Sub Default1_Load(sender As Object, e As EventArgs) Handles Me.Load
        ''   Dim UserControl As ctrlContacts = ctrlSubObjects1
        'Dim uc As UserControl
        'Dim pnl As Panel
        'Dim c As Control
        'Dim lsType As String
        'For Each c In ctrlSubObjects1.Controls
        '    lsType = c.GetType.ToString
        '    If lsType = "ctrl" Then
        '        Stop

        '        pnl = uc.FindControl("Panel1")
        '        ' Dim radGrid1 As RadGrid = UserControl.FindControl("radGrid1")
        '        If pnl Is Nothing Then
        '        Else

        '            '  Dim GridView1 As GridView = UserControl.FindControl("GridView1")
        '            '  Dim panel1 As Panel = UserControl.FindControl("panel1")
        '            RadAjaxManager1.AjaxSettings.AddAjaxSetting(rgMaster, pnl, RadAjaxLoadingPanel1)
        '        End If
        '    End If
        'Next
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        System.Threading.Thread.Sleep(500)
    End Sub
End Class