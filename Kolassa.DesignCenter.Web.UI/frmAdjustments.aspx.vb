Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Partial Class _frmAdjustments
    Inherits System.Web.UI.Page
    Private cnn As New SqlConnection("Data Source=.;Initial Catalog=pubs;User ID=sa;Password=*****")


    Protected Sub cmdTestContacts_Click(sender As Object, e As EventArgs) Handles cmdTestContacts.Click
        Dim c As New clsContacts
        Dim contact As New clsContact
        contact.FirstName = "Babe"
        contact.LastName = "Ruth"
        contact.FullAddress = "1401 Dogwood Lane"
        contact.City = "Mt Prospect"
        contact.StateProvince = "IL"
        contact.PostalCode = "60056"
        contact.NodeID = 2
        contact.ImageURL = "https://www.flickr.com/gp/kolassa/66Gc28"
        contact.Phone1 = "3216549872"
        contact.Phone2 = "1234567890"
        contact.Email1 = "a@a.com"
        contact.Email2 = "m@m.com"
        contact.Country = "US"
        contact.Type = "Primary"
        contact.ParentID = "b65337fc-ce7a-4454-a8d9-9f8394b3a049"
        ' c.InsertContacts(contact)
        '    Stop
        contact.ID = ("12341234-1234-1234-1234-123412341234")
        '  c.DeleteContacts(contact)
        '  Stop
        c.UpdateContacts(contact)
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

    Private Sub _frmAdjustments_Load(sender As Object, e As EventArgs) Handles Me.Load
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