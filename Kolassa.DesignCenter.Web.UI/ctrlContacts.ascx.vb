Imports System.IO
Imports Telerik.Web.UI
Public Class ctrlContacts
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Exit Sub
        Dim rl As Telerik.Web.UI.RadAjaxLoadingPanel
        rl = Me.Parent.FindControl("RadAjaxLoadingPanel1")
        If rl Is Nothing Then
            rl = Me.Parent.Parent.FindControl("RadAjaxLoadingPanel1")
        End If
        If rl Is Nothing Then
            rl = Me.Parent.Parent.Parent.FindControl("RadAjaxLoadingPanel1")
        End If
        If rl Is Nothing Then
            rl = Me.Parent.Parent.Parent.Parent.FindControl("RadAjaxLoadingPanel1")
        End If
        If rl Is Nothing Then
            rl = Me.Parent.Parent.Parent.Parent.Parent.FindControl("RadAjaxLoadingPanel1")
        End If
        If rl Is Nothing Then
            Exit Sub 'Never Found rgMaster
        End If

        Dim rg As Telerik.Web.UI.RadGrid
        rg = Me.Parent.FindControl("rgMaster")
        If rg Is Nothing Then
            rg = Me.Parent.Parent.FindControl("rgMaster")
        End If

        If rg Is Nothing Then
            rg = Me.Parent.Parent.Parent.FindControl("rgMaster")
        End If
        If rg Is Nothing Then
            rg = Me.Parent.Parent.Parent.Parent.FindControl("rgMaster")
        End If
        If rg Is Nothing Then
            Exit Sub 'Never Found rgMaster
        End If

        Dim rm As Telerik.Web.UI.RadAjaxManager
        rm = Me.Parent.FindControl("RadAjaxManager1")
        If rm Is Nothing Then
            rm = Me.Parent.Parent.FindControl("RadAjaxManager1")
        End If
        If rm Is Nothing Then
            rm = Me.Parent.Parent.Parent.FindControl("RadAjaxManager1")
        End If
        If rm Is Nothing Then
            rm = Me.Parent.Parent.Parent.Parent.FindControl("RadAjaxManager1")
        End If
        If rm Is Nothing Then
            'Do Nothing
        Else
            rm.AjaxSettings.AddAjaxSetting(rgContact, rg, rl)
        End If


    End Sub


    Protected Sub odsContact_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsContact.Selecting
        Dim rg As Telerik.Web.UI.RadGrid
        rg = Me.Parent.FindControl("rgMaster")
        If rg Is Nothing Then
            rg = Me.Parent.Parent.FindControl("rgMaster")
        End If
        If rg Is Nothing Then
            rg = Me.Parent.Parent.Parent.FindControl("rgMaster")
        End If
        If rg Is Nothing Then
            rg = Me.Parent.Parent.Parent.Parent.FindControl("rgMaster")
        End If
        If rg Is Nothing Then
            e.InputParameters("lsObjectID") = "12121212-1212-1212-1212-121212121212"
        Else
            e.InputParameters("lsObjectID") = rg.SelectedValue
        End If


    End Sub

    Protected Sub ddlStateProvince_DataBinding(sender As Object, e As EventArgs)
        Dim ddList As DropDownList = CType(sender, DropDownList)
        RemoveHandler ddList.DataBinding, AddressOf ddlStateProvince_DataBinding
        Try
            ddList.DataBind()
        Catch ex As ArgumentOutOfRangeException
            ddList.SelectedValue = ""  ' or whatever value you have for the page dropdownlist
        End Try
    End Sub

    ' The id parameter name should match the DataKeyNames value set on the control
    Private Sub _Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' base.OnLoad(e)
        ErrorMessage.Visible = False
    End Sub







    '   Protected void SearchButton_Click(Object sender, EventArgs e)
    '{
    '	RadGrid1.MasterTableView.FilterExpression = string.Empty;
    '	If (!string.IsNullOrEmpty(this.SearchBox.Text))
    '	{
    '		foreach (GridColumn column in RadGrid1.MasterTableView.RenderColumns)
    '		{
    '			If (column Is GridBoundColumn)
    '			{
    '				If (!string.IsNullOrEmpty(RadGrid1.MasterTableView.FilterExpression))
    '				{
    '					RadGrid1.MasterTableView.FilterExpression += " OR ";
    '				}
    '				RadGrid1.MasterTableView.FilterExpression += @"(it[""" + (column as GridBoundColumn).DataField + @"""].ToString().Contains(""" + this.SearchBox.Text + @"""))";
    '			}
    '		}
    '	}

    '	RadGrid1.MasterTableView.Rebind();
    '}



    Protected Sub rgContact_ItemDeleted(sender As Object, e As GridDeletedEventArgs) Handles rgContact.ItemDeleted
        If (Not e.Exception Is Nothing) Then
            SetMessage("Failed: " + e.Exception.Message)
            e.ExceptionHandled = True
        End If
    End Sub




    Protected Sub rgContact_ItemInserted(sender As Object, e As GridInsertedEventArgs) Handles rgContact.ItemInserted
        If (Not e.Exception Is Nothing) Then
            SetMessage("Failed: " + e.Exception.Message)
            e.ExceptionHandled = True
        End If
    End Sub


    Protected Sub rgContact_ItemUpdated(sender As Object, e As GridUpdatedEventArgs) Handles rgContact.ItemUpdated
        If (Not e.Exception Is Nothing) Then
            SetMessage("Failed: " + e.Exception.Message)
            e.ExceptionHandled = True
        End If
    End Sub



    Private Sub SetMessage(text As String)
        ErrorMessage.Text = text
        ErrorMessage.Visible = True
    End Sub




End Class