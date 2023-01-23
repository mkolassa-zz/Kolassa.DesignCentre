Public Class frmUpgradeItems
    Inherits System.Web.UI.Page

    Sub checkchange(sender As Object, e As EventArgs)
        Dim chk = DirectCast(sender, CheckBox)
        System.Threading.Thread.Sleep(5000)

    End Sub


    Private Sub frmTest_Load(sender As Object, e As EventArgs) Handles Me.Load
        ucCategories.ProjectID = Session("Project")

    End Sub



    Protected Sub cmdGetUpgradeCategoryData_Click(sender As Object, e As EventArgs) Handles cmdGetUpgradeCategoryData.Click
        litMsg.Text = ucCategories.UnitType & ":" & ucCategories.UnitTypeID & ":" & ucCategories.Location & ":" &
            ucCategories.LocationID & ":" & ucCategories.Category & ":" & ucCategories.CategoryID & ":" &
            ucCategories.Level & ":" & ucCategories.Description & ":" & ucCategories.Model
    End Sub
End Class