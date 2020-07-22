Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Specialized
Imports System.Data.SqlClient
''' <summary> 
''' Article: Dynamically Templated GridView with Edit,Insert and Delete Options 
''' Author: G. Mohyuddin 
''' Brief Notes: This class implements ITemplate which is resposible to create template fields of 
''' the GridView dynamically and also to add buttons for Edit,Delete and Insert. 
''' </summary> 
Public Class DynamicallyTemplatedGridViewHandler
    Implements ITemplate
#Region "data memebers"

    Private ItemType As ListItemType
    Private FieldName As String
    Private InfoType As String

#End Region

#Region "constructor"

    Public Sub New(ByVal item_type As ListItemType, ByVal field_name As String, ByVal info_type As String)
        ItemType = item_type
        FieldName = field_name
        InfoType = info_type
    End Sub

#End Region

#Region "Methods"

    Public Sub InstantiateIn(ByVal Container As System.Web.UI.Control) Implements ITemplate.InstantiateIn

        System.Diagnostics.Debug.WriteLine("InstantiateIn:" & ItemType.ToString)
        Select Case ItemType
            Case ListItemType.Header
                Dim header_ltrl As New Literal()
                header_ltrl.Text = "<b>" & FieldName & "</b>"
                Container.Controls.Add(header_ltrl)
                Exit Select
            Case ListItemType.Item
                Select Case InfoType
                    Case "Command"
                        Exit Select
                        Dim edit_button As ImageButton = New ImageButton()

                        edit_button.ID = "edit_button"
                        edit_button.ImageUrl = "~/images/edit.png"
                        edit_button.CommandName = "Edit"
                        AddHandler edit_button.Click, AddressOf edit_button_Click
                        edit_button.ToolTip = "Edit"
                        Container.Controls.Add(edit_button)

                        Dim delete_button As New ImageButton()
                        delete_button.ID = "delete_button"
                        delete_button.ImageUrl = "~/images/delete.png"
                        delete_button.CommandName = "Delete"
                        delete_button.ToolTip = "Delete"
                        delete_button.OnClientClick = "return confirm('Are you sure to delete the record?')"
                        Container.Controls.Add(delete_button)

                        ' Similarly add button for insert. 
                        ' * It is important to know when 'insert' button is added 
                        ' * its CommandName is set to "Edit" like that of 'edit' button 
                        ' * only because we want the GridView enter into Edit mode, 
                        ' * and this time we also want the text boxes for corresponding fields empty 

                        Dim insert_button As New ImageButton()
                        insert_button = New ImageButton
                        insert_button.ID = "insert_button"
                        insert_button.ImageUrl = "~/images/insert.png"
                        insert_button.CommandName = "Edit"
                        insert_button.ToolTip = "Insert"
                        AddHandler insert_button.Click, AddressOf insert_button_Click
                        Container.Controls.Add(insert_button)

                        Exit Select
                    Case Else

                        Dim field_lbl As New Label()
                        field_lbl.ID = FieldName
                        field_lbl.Text = [String].Empty
                        'we will bind it later through 'OnDataBinding' event 
                        AddHandler field_lbl.DataBinding, AddressOf OnDataBinding
                        Container.Controls.Add(field_lbl)
                        Exit Select

                End Select
                Exit Select
            Case ListItemType.EditItem
                If InfoType = "Command" Then
                    Dim update_button As New ImageButton()
                    update_button.ID = "update_button"
                    update_button.CommandName = "Update"
                    update_button.ImageUrl = "~/images/update.png"
                    If CInt(New Page().Session("InsertFlag")) = 1 Then
                        update_button.ToolTip = "Add"
                    Else
                        update_button.ToolTip = "Update"
                    End If
                    update_button.OnClientClick = "return confirm('Are you sure to update the record?')"
                    Container.Controls.Add(update_button)

                    Dim cancel_button As New ImageButton()
                    cancel_button.ImageUrl = "~/images/cancel.png"
                    cancel_button.ID = "cancel_button"
                    cancel_button.CommandName = "Cancel"
                    cancel_button.ToolTip = "Cancel"

                    Container.Controls.Add(cancel_button)
                Else
                    ' for other 'non-command' i.e. the key and non key fields, bind textboxes with corresponding field values 
                    Dim field_txtbox As New TextBox()
                    field_txtbox.ID = FieldName
                    field_txtbox.Text = [String].Empty
                    ' if Inert is intended no need to bind it with text..keep them empty 
                    If CInt(New Page().Session("InsertFlag")) = 0 Then
                        AddHandler field_txtbox.DataBinding, AddressOf OnDataBinding
                    End If

                    Container.Controls.Add(field_txtbox)
                End If
                Exit Select


        End Select
    End Sub

#End Region

#Region "Event Handlers"

    'just sets the insert flag ON so that we ll be able to decide in OnRowUpdating event whether to insert or update 
    Protected Sub insert_button_Click(ByVal sender As [Object], ByVal e As EventArgs)
        HttpContext.Current.Session("InsertFlag") = 1
    End Sub
    'just sets the insert flag OFF so that we ll be able to decide in OnRowUpdating event whether to insert or update 
    Protected Sub edit_button_Click(ByVal sender As [Object], ByVal e As EventArgs)
        HttpContext.Current.Session("InsertFlag") = 0
    End Sub

    Private Sub OnDataBinding(ByVal sender As Object, ByVal e As EventArgs)

        Dim bound_value_obj As Object = Nothing
        Dim ctrl As Control = DirectCast(sender, Control)
        Dim data_item_container As IDataItemContainer = DirectCast(ctrl.NamingContainer, IDataItemContainer)
        bound_value_obj = DataBinder.Eval(data_item_container.DataItem, FieldName)
        System.Diagnostics.Debug.WriteLine("OnDataBinding " & ItemType.ToString & ":" & bound_value_obj.ToString)
        Select Case ItemType
            Case ListItemType.Item
                Dim field_ltrl As Label = DirectCast(sender, Label)
                field_ltrl.Text = bound_value_obj.ToString()

                Exit Select
            Case ListItemType.EditItem
                Dim field_txtbox As TextBox = DirectCast(sender, TextBox)
                field_txtbox.Text = bound_value_obj.ToString()

                Exit Select


        End Select
    End Sub

#End Region


End Class