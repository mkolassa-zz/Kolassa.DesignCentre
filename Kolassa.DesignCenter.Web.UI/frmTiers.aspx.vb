Public Class frmTiers
    Inherits System.Web.UI.Page
    Dim msFilter As String = ""
    Dim mlRecordID As Long
    Dim mlUnitTypeID As Long
    Dim mlTierID As Long
    Dim msTierName As String
    Dim msActive As String


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        System.Threading.Thread.Sleep(500)
        Session("NodeID") = 1
        Page.Title = "Tier Management Page"
        txtTitle.Text = Page.Title
        If IsPostBack Then
            ' MsgBox(Session("msFilter"))
            Return
        Else : Session("msFilter") = ""
        End If
        'Me.cmdAddNew.Visible = True
        Me.imgAdd.Visible = True

    End Sub

    Protected Sub DataGridDisplay_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles DataGridDisplay.RowDataBound
        If DataGridDisplay.EditIndex <> -1 Then

            DataGridDisplay.ShowFooter = False

            'Me.cmdAddNew.Visible = False
            Me.imgAdd.Visible = False
        Else
            '  Me.cmdAddNew.Visible = True
            Me.imgAdd.Visible = True
        End If
    End Sub



    Protected Sub cmdAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim t As TextBox
        t = Me.DataGridDisplay.FooterRow.FindControl("TierName")
        Dim lsName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
        If lsName = "" Then
            '  MsgBox("You must enter a Unit Type Name.")
        Else
            ObjectDataSource1.Insert()
            DataGridDisplay.ShowFooter = False
        End If


    End Sub

    Protected Sub ObjectDataSource1_Inserting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Inserting
        Dim t As TextBox
        '        Dim c As CheckBox
        '  Dim d As DropDownList
        t = Me.DataGridDisplay.FooterRow.FindControl("TierName")
        Dim lsUnitName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;
      

        e.InputParameters("Tier") = lsUnitName
        e.InputParameters("llNodeID") = Session("NodeID")

    End Sub

    Protected Sub cmdAddNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddNew.Click
        DataGridDisplay.EditIndex = -1
        DataGridDisplay.ShowFooter = True
    End Sub

    Protected Sub DataGridDisplay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridDisplay.Load
        If DataGridDisplay.EditIndex <> -1 Then
            DataGridDisplay.ShowFooter = False
        End If
    End Sub

    Protected Sub DataGridDisplay_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles DataGridDisplay.RowEditing
        DataGridDisplay.ShowFooter = False
        '        Dim t As TextBox
        Dim r As GridViewRow
        Dim C As CheckBox
        '     Dim d As DropDownList
        Dim l As Literal
        Dim liRow As Integer


        liRow = e.NewEditIndex
        If liRow = -1 Then
        Else
            r = DataGridDisplay.Rows(liRow) 'DataGridDisplay.Rows(0) 'liRow) 


            l = r.FindControl("TierID")
            mlTierID = l.Text
            l = r.FindControl("TierName")
            msTierName = l.Text
            C = r.FindControl("Active")
            msActive = C.Checked



        End If
    End Sub

    Protected Sub ObjectDataSource1_Updating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs) Handles ObjectDataSource1.Updating

        Dim t As TextBox
        Dim r As GridViewRow
        Dim C As CheckBox
        '        Dim d As DropDownList
        If Session("NodeID") Is Nothing Then Session("NodeID") = 0
        r = DataGridDisplay.Rows(DataGridDisplay.EditIndex)
        t = r.FindControl("TierName")
        Dim lsTierName As String = t.Text ' ((TextBox)gvIngredient.headerRow.FindControl("tbIngredient")).Text;

        Dim l As Literal
        l = r.FindControl("TierID")
        Dim lsUnitID As String = l.Text
        C = r.FindControl("Active")
        Dim lsActive As String = IIf(C.Checked = True, "True", "False")

    
        Dim lsNodeID As String = Session("NodeID")


        e.InputParameters("TierName") = lsTierName
        e.InputParameters("llNodeID") = lsNodeID
        e.InputParameters("TierID") = lsUnitID
        e.InputParameters("lsActive") = lsActive



    End Sub



    Protected Sub cmdRunReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdRunReport.Click
        ReportContainer1.Refresh()
        Session("msFilter") = ReportContainer1.WhereClause
    End Sub


    Protected Sub ddlPagelength_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPagelength.SelectedIndexChanged
        DataGridDisplay.PageSize = ddlPagelength.SelectedValue
    End Sub

    Protected Sub DataGridDisplay_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles DataGridDisplay.PageIndexChanging
        '   Stop
    End Sub

    Public Overrides Sub VerifyRenderingInServerForm(ByVal Control As Control)
        Return
    End Sub





 



    Protected Sub imgExcel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgExcel.Click


        If Session("Export") = "Export" Then
            Session("Export") = ""
            Exit Sub
        End If
        Session("Export") = "Export"
        DataGridDisplay.AllowPaging = False
        DataGridDisplay.AllowSorting = False
        DataGridDisplay.DataBind()

        Dim strFileName As String = Trim(Left(Trim(Me.Title) & "                                    ", 31)) & ".xls"
        Dim ctrl As Control
        For Each ctrl In Me.Controls
            If ctrl.ID <> "DataGridDisplay" Then
                ctrl.Visible = False
            End If
        Next
        'Me.cboSurveyName.Visible = False

        Me.Header.Visible = False
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment; filename=""" & strFileName & """")
        Response.ContentEncoding = System.Text.Encoding.UTF7
        Response.Charset = ""
        EnableViewState = False
        Dim oStringWriter As New System.IO.StringWriter()
        Dim oHTMLTextWriter As New System.Web.UI.HtmlTextWriter(oStringWriter)
        DataGridDisplay.RenderControl(oHTMLTextWriter)
        'form1.RenderControl(oHTMLTextWriter)
        Dim lsString As String = oStringWriter.ToString()
        Response.Write(lsString)
        EnableViewState = True
        DataGridDisplay.AllowPaging = True
        DataGridDisplay.AllowSorting = True
        DataGridDisplay.DataBind()

        DataGridDisplay.Focus()

    End Sub

    Protected Sub imgAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAdd.Click
        DataGridDisplay.EditIndex = -1
        DataGridDisplay.ShowFooter = True
    End Sub


End Class
