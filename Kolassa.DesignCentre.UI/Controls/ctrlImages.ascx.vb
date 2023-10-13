﻿Imports System.IO
Public Class ctrlImages
	Inherits System.Web.UI.UserControl
	Dim msObjectID As String
	Public Property ObjectID() As String
		Get
			Return msObjectID
		End Get
		Set(ByVal Value As String)
			msObjectID = Value
			lblObjectID.Text = msObjectID
			With ctrlImageNewURL
				.ObjectID = msObjectID
				.Attributes.Remove("objectID")
				.Attributes.Add("objectID", msObjectID)
			End With
			With ctrlImageNew1
				.ObjectID = msObjectID
				.Attributes.Remove("objectID")
				.Attributes.Add("objectID", msObjectID)
			End With
			With ctrlImageNew2
				.ObjectID = msObjectID
				.Attributes.Remove("objectID")
				.Attributes.Add("objectID", msObjectID)
			End With
			ctrlImageNew1.ObjectID = msObjectID
			ctrlImageNew2.ObjectID = msObjectID
			gvImages.DataBind()
		End Set
	End Property

	Public Sub imghandler() Handles ctrlImageNew1.SomethingHappened, ctrlImageNew2.SomethingHappened, ctrlImageNewURL.SomethingHappened
        'Refresh
        Dim x As Integer = 9
        gvImages.DataBind()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		'lblObjectID.Text = Session("Project")
		Page.Form.Enctype = "multipart/form-data"
	End Sub
	Protected Sub ObjectDataSource1_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles odsImages.Selecting
		Dim rg As GridView
		Dim lsID As String = lblObjectID.Text
		If lsID.Length = 36 Then
			e.InputParameters("lsObjectID") = lsID
			Exit Sub
		End If

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

	Private Sub odsImages_Deleting(sender As Object, e As ObjectDataSourceMethodEventArgs) Handles odsImages.Deleting
		Dim p As IDictionary = e.InputParameters

		If p("llNodeID") Is Nothing Then
			p("llNodeID") = Session("NodeID")
		End If

		'Determine the RowIndex of the Row whose Button was clicked.
		'		Dim rowIndex As Integer = gvImages.SelectedIndex ' TryCast(TryCast(sender, Button).NamingContainer, GridViewRow).RowIndex

		'Get the value of column from the DataKeys using the RowIndex.
		'		Dim id As String = Convert.ToString(gvImages.DataKeys(rowIndex).Values(0))
		'		p("RecordID") = id
	End Sub

	Private Sub gvImages_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvImages.RowDeleting
		Dim lsID As String
		lsID = e.Keys(0).ToString

		odsImages.DeleteParameters.Clear()
		odsImages.DeleteParameters.Add("RecordID", lsID)
		odsImages.DeleteParameters.Add("llNodeID", Session("NodeID"))
	End Sub

    Private Sub lnkrefresh_Click(sender As Object, e As EventArgs) Handles lnkrefresh.Click
        gvImages.DataBind()
    End Sub

	Protected Sub lnkImageUpload_Click(sender As Object, e As EventArgs) Handles lnkImageUpload.Click
		Dim intId As Integer = 100
		' Passing intId to popup window.
		Dim strPopup As String = "<script language='javascript' ID='script1'>" + "window.open('frmImagePopOut.aspx?ObjType=" & Request.QueryString("ObjType") & "&ObjectID=" & lblObjectID.Text & "&data=" + HttpUtility.UrlEncode(intId.ToString()) + "','new window', 'top=90, left=200, width=600, height=300, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')" + "</script>"
		ScriptManager.RegisterStartupScript(DirectCast(HttpContext.Current.Handler, Page), GetType(Page), "Script1", strPopup, False)
	End Sub
    Protected Sub gvImages_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvImages.RowCommand
        '// If multiple ButtonField column fields are used, use the
        '// CommandName property to determine which button was clicked.
        Dim clsImg As New clsImage

        Dim lsID As String = e.CommandArgument
		If lsID.Length = 36 Then
			Select Case e.CommandName
				Case "Primary"
                    '               'Stop
                    clsImg.setPrimary(lsID)
				Case "Swatch"

                    '	Stop
                    clsImg.setSwatch(lsID)
				Case Else
					Stop
			End Select
		End If
	End Sub
End Class