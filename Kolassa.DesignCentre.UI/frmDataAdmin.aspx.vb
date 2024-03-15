Imports System.IO
Public Class frmDataAdmin
    Inherits System.Web.UI.Page
    Dim ds As New DataSet
    Dim c As New clsSelectDataLoader
    Dim llNodeID As Long
    Dim lsProject As String
    Private Sub frmDataAdmin_Load(sender As Object, e As EventArgs) Handles Me.Load
        llNodeID = Session("NodeID")
        lsProject = Session("Project")
        If Me.IsPostBack Then
        Else
            LoadListboxes()
        End If
        Dim ls As String = Page.Request.Params.Get("__EVENTTARGET")
    End Sub
    Private Sub LoadListboxes()
        ds = c.LoadProjects(llNodeID, "", True, lsProject)
        loadlistboxItems(ds, lstProject)
        loadlistboxItems(c.LoadPhases(llNodeID, "", lsProject), lstPhase)
        loadlistboxItems(c.LoadUnitTypes(llNodeID, "", True, lsProject), lstUnitType)
        loadlistboxItems(c.LoadUnitRooms(llNodeID, "", True, lstUnitType.SelectedValue), lstRoom)
    End Sub
    Sub loadlistboxItems(ds As DataSet, llistbox As ListBox)
        llistbox.DataSource = ds
        llistbox.DataTextField = "Name"
        llistbox.DataValueField = "ID"
        llistbox.DataBind()
    End Sub

    Private Sub lstUnitType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstUnitType.SelectedIndexChanged
        loadlistboxItems(c.LoadUnitRooms(llNodeID, "", True, lstUnitType.SelectedValue), lstRoom)
    End Sub

    Private Sub cmdRefresh_Click(sender As Object, e As EventArgs) Handles cmdRefresh.Click
        Exit Sub
        loadlistboxItems(c.LoadUnitRooms(llNodeID, "", True, lstUnitType.SelectedValue), lstRoom)
    End Sub

    Private Sub lstRoom_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstRoom.SelectedIndexChanged
        loadlistboxItems(c.LoadRoomCategories2(llNodeID, lstUnitType.SelectedValue, lstPhase.SelectedValue, lstRoom.SelectedValue), lstCategoryDetail)
    End Sub

    Private Sub lstCategoryDetail_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstCategoryDetail.SelectedIndexChanged
        loadlistboxItems(c.LoadUpgradeOptionsByCategoryDetail(llNodeID, lstCategoryDetail.SelectedValue, True, "", ""), lstUpgradeOption)
    End Sub
End Class