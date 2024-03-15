Public Class frmUpgradeCategoriesMap
    Inherits System.Web.UI.Page

    Dim ar1 As ArrayList = New ArrayList()
    Dim ar2 As ArrayList = New ArrayList()
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
    Shared Function fHey() As String
        Return "Whassssupppp?"
    End Function

    Protected Sub AddOneItem(sender As Object, e As EventArgs) Handles ImageButton1.Click
        Dim liItem As ListItem
        If lstCateories.SelectedIndex >= 0 Then
            For i As Integer = lstCateories.Items.Count - 1 To 0 Step -1
                liItem = lstCateories.Items(i)
                If (liItem.Selected) Then
                    If ar1.Contains(liItem) Then
                    Else
                        ar1.Add(liItem)
                    End If
                End If
            Next
            For i As Integer = ar1.Count - 1 To 0 Step -1
                liItem = ar1(i)
                If lstAdded.Items.Contains(liItem) Then
                Else
                    lstAdded.Items.Add(liItem)
                    lstCateories.Items.Remove(liItem)
                End If
            Next
            lstAdded.SelectedIndex = -1
        Else
            lblMsg.Text = "Please select atleast one listitem"
            lblMsg.ForeColor = System.Drawing.Color.Red
        End If
    End Sub

    Protected Sub RemoveOneItem(sender As Object, e As EventArgs) Handles ImageButton2.Click
        Dim liItem As ListItem
        If lstAdded.SelectedIndex >= 0 Then
            For i As Integer = lstAdded.Items.Count - 1 To 0 Step -1
                If (lstAdded.Items(i).Selected) Then
                    If ar2.Contains(lstAdded.Items(i)) Then
                    Else
                        ar2.Add(lstAdded.Items(i))
                    End If
                End If
            Next
            For i As Integer = ar2.Count - 1 To 0 Step -1
                liItem = ar2(i)
                If lstCateories.Items.Contains(liItem) Then
                Else
                    lstCateories.Items.Add(liItem)
                End If
                lstAdded.Items.Remove(liItem)
            Next
            lstCateories.SelectedIndex = -1
        Else
            lblMsg.Text = "Data removed from listbox"
            lblMsg.ForeColor = System.Drawing.Color.ForestGreen
        End If
    End Sub

    Protected Sub AddAllItems(sender As Object, e As EventArgs) Handles ImageButton3.Click
        While lstCateories.Items.Count > 0
            For i As Integer = lstCateories.Items.Count - 1 To 0 Step -1
                lstAdded.Items.Add(lstCateories.Items(i))
                lstCateories.Items.Remove(lstCateories.Items(i))
            Next
        End While
        lblMsg.Text = "All data added to listbox2"
        lblMsg.ForeColor = System.Drawing.Color.ForestGreen
    End Sub

    Protected Sub RemoveAllItems(sender As Object, e As EventArgs) Handles ImageButton4.Click
        Dim liItem As ListItem
        While lstAdded.Items.Count > 0
            For i As Integer = lstAdded.Items.Count - 1 To 0 Step -1
                liItem = lstAdded.Items(i)
                lstCateories.Items.Add(liItem)
                lstAdded.Items.Remove(liItem)
            Next
        End While
        lblMsg.Text = "All data removed and moved to listbox1"
        lblMsg.ForeColor = System.Drawing.Color.ForestGreen
    End Sub

    Private Sub frmUpgradeCategoriesMap_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            Dim lsProjectID As String = Session("Project")
            Dim llNodeID As Integer = Session("NodeID")
            If llNodeID = 0 Then Exit Sub
            If lsProjectID Is Nothing Then Exit Sub
            If lsProjectID.Length <> 36 Then Exit Sub
            Dim liItem As ListItem
            Dim cats As New clsDBObjects
            Dim ccats As New List(Of clsDBObject)
            ccats = cats.GetRecords(" ObjectID = '" & lsProjectID & "'", "", "", llNodeID, "tblUpgradecategories", True)
            lstCateories.Items.Clear()

            For Each cat As clsDBObject In ccats
                liItem = New ListItem
                liItem.Text = cat.Name
                liItem.Value = cat.ID
                lstCateories.Items.Add(liItem)
            Next
            lblMsg.Text = "Categories Loaded"
            lblMsg.ForeColor = System.Drawing.Color.Green
            Dim rooms As New clsRooms
            Dim crooms As New List(Of clsRoom)
            crooms = rooms.GetRecords("Name", "", "", lsProjectID, True, llNodeID)
            lstRoom.Items.Clear()
            For Each room As clsRoom In crooms
                liItem = New ListItem
                liItem.Text = room.Name
                liItem.Value = room.ID
                lstRoom.Items.Add(liItem)
            Next
            Dim phases As New clsPhases
            Dim cphases As New List(Of clsPhase)
            cphases = phases.GetRecords("Name", "", "") ', lsProjectID, True, llNodeID)
            lstPhase.Items.Clear()
            For Each phase As clsPhase In cphases
                liItem = New ListItem
                liItem.Text = phase.Name
                liItem.Value = phase.ID
                lstPhase.Items.Add(liItem)
            Next

            Dim UnitTypes As New clsUnitTypes
            Dim cUnitTypes As New List(Of clsUnitType)
            cUnitTypes = UnitTypes.GetRecords("", "", lsProjectID, llNodeID)
            LstUnitType.Items.Clear()
            For Each unittype As clsUnitType In cUnitTypes
                liItem = New ListItem
                liItem.Text = unittype.Name
                liItem.Value = unittype.ID
                LstUnitType.Items.Add(liItem)
            Next
        End If
    End Sub

    Protected Sub cmdSubmitCategoryDetails_Click(sender As Object, e As EventArgs) Handles cmdSubmitCategoryDetails.Click
        Dim UCD As New clsUpgradeCategoryDetials

        UCD.Phases = getSelectedValuesFromListBox(lstPhase).ToArray
        UCD.UnitTypes = getSelectedValuesFromListBox(LstUnitType).ToArray
        UCD.Rooms = getSelectedValuesFromListBox(lstRoom).ToArray
        UCD.Categories = getSelectedValuesFromListBox(lstCateories).ToArray
        UCD.Insert()
        If UCD.ErrorMessage = "" Then
            lblMsg.Text = "Categories Loaded Successfully"
        Else
            lblMsg.Text = UCD.ErrorMessage
        End If
    End Sub
    Public Shared Function getSelectedValuesFromListBox(ByVal objListBox As ListBox) As String()
        Dim listOfIndices As List(Of Integer) = objListBox.GetSelectedIndices().ToList()
        Dim values(250) As String
        Dim liCounter As Integer
        For Each indice As Integer In listOfIndices
            values(liCounter) = objListBox.Items(indice).Value
            liCounter = liCounter + 1
        Next indice

        Return values
    End Function
End Class