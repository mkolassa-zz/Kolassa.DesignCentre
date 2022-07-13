Public Class ctrlContactEntry
    Inherits System.Web.UI.UserControl

    Dim msObjectID As String
    Public Property ObjectID() As String
        Get
            Return msObjectID
        End Get
        Set(ByVal Value As String)
            msObjectID = Value
            lnkNewContact.Attributes.Add("data-parentid", Value)
            'lblObjectID.Text = msObjectID
            Dim c As clsContacts
            c = New clsContacts
            Dim ds As DataSet = c.GetContacts(0, msObjectID, Session("NodeID"), True)
            'gvContacts.DataSource = ds
            'gvContacts.DataBind()
            rptContact.DataSource = ds
            rptContact.DataBind()
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack Then


        End If
    End Sub

    Protected Sub btn_Submit_Click1(sender As Object, e As EventArgs)
        '   Dim txt As TextBox = 
        '   Dim t As String = txt.Text

        '   lblmsg.Text = "Item successfullyy added to category dropdown "
        '  lblmsg.CssClass = "alert alert-success pull-right"
        Dim strJsSuccess As String = New StringBuilder("$('#contact_Modal').modal('hide');").ToString()
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "Hide", strJsSuccess, True)
    End Sub





    Private Sub rptContact_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles rptContact.ItemDataBound


    End Sub

    Protected Sub btn_save_Click(sender As Object, e As EventArgs) Handles btn_save.Click
        Dim c As clsContact
        c = New clsContact
        c.ID = txtContactID.Text
        c.ParentID = txtContactParentID.Text
        c.FirstName = txtContactFirstName.Text
        c.LastName = txtContactLastName.Text
        c.FullAddress = txtContactAddress.Text
        c.City = txtContactCity.Text
        c.StateProvince = txtContactState.Text
        c.PostalCode = txtContactPostalCode.Text
        c.Country = txtContactCountry.Text
        c.Phone1 = txtContactPhone1.Text
        c.Phone2 = txtContactPhone2.Text
        c.Email1 = txtContactEmail1.Text
        c.Email2 = txtContactEmail2.Text
        c.Active = True
        Dim cs As New clsContacts
        If txtContactID.Text.Length = 36 Then
            cs.UpdateContacts(c)
        Else
            cs.InsertContacts(c)
        End If
        If cs.ErrorMessage <> "" Then
            ShowMessage(cs.ErrorMessage, "Error")
        Else
            ShowMessage("Success", "Success")
        End If
        ObjectID = txtContactParentID.Text
    End Sub
    Private Sub ShowMessage(Message As String, type As String)
        Dim m As String = fTakeOutQuotes(Message).Replace(Environment.NewLine, String.Empty)
        Dim t As String = fTakeOutQuotes(type)
        Dim s As String = "$( document ).ready(function() {ShowContactMessage('" & m & "','" & t & "');});"
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script", s, True)

        'ScriptManager.RegisterStartupScript(Page, Page.GetType(), "script1", "$( document ).ready(function() {aalert('Stop');});", True)
    End Sub
    Function fTakeOutQuotes(ByVal lsStr As String) As String
        lsStr = Replace(lsStr, "script", "scri_pt")
        lsStr = Replace(lsStr, """", "*")
        lsStr = Replace(lsStr, "'", "*")
        fTakeOutQuotes = Trim(lsStr)
    End Function

    Protected Sub lnkDeleteContact_Click(sender As Object, e As EventArgs) Handles lnkDeleteContact.Click
        Dim c As New clsSelectDataLoader
        c.DeleteContacts(txtContactID.Text, Session("NodeID"))
        If c.ErrorMessage <> "" Then
            ShowMessage(c.ErrorMessage, "Error")
        Else
            ShowMessage("Success", "Success")
        End If
        ObjectID = txtContactParentID.Text
    End Sub
End Class