Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Public Class ctrlNotifications
    Inherits System.Web.UI.UserControl
    Dim _lastdate As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.Form.Enctype = "multipart/form-data"
    End Sub
    Function GetObjectID() As String
        Dim lsObjectID As String
        Dim lit As Literal
        lit = Me.Parent.FindControl("litID")
        If lit Is Nothing Then
            lit = Me.Parent.Parent.FindControl("litID")
        End If
        If lit Is Nothing Then
            lit = Me.Parent.Parent.Parent.FindControl("litID")
        End If
        If lit Is Nothing Then
            lit = Me.Parent.Parent.Parent.Parent.FindControl("litID")
        End If
        If lit Is Nothing Then
            lsObjectID = "12121212-1212-1212-1212-121212121212"
        Else
            lsObjectID = lit.Text
        End If

        GetObjectID = lsObjectID
    End Function

    Public Sub BindData()
        gvData.DataBind()
        repeaterData.DataBind()
    End Sub
    Function DaysFromDate(ByVal dt As Date) As Long
        DaysFromDate = DateDiff("d", dt, Now())
    End Function
    Protected Sub gvData_DataBinding(sender As Object, e As EventArgs) Handles gvData.DataBinding
        Dim t As New clsTasks()
        gvData.DataSource = t.GetRecords("", "", Session("Project"), Session("NodeID"), HttpContext.Current.User.Identity.GetUserId)
        repeaterData.DataSource = gvData.DataSource
    End Sub
    Protected Sub cmdPostComm_Click(sender As Object, e As EventArgs) Handles cmdPostComm.Click
        gvData.DataBind()
        repeaterData.DataBind()
    End Sub
    Protected Sub cmdBindData_Click(sender As Object, e As EventArgs) Handles cmdBindData.Click
        BindData()
    End Sub
    Protected Sub gvData_RowCreated(sender As Object, e As GridViewRowEventArgs)
        e.Row.CssClass = "table border"
    End Sub

    Protected Sub repeaterData_ItemDataBound(sender As Object, e As RepeaterItemEventArgs) Handles repeaterData.ItemDataBound
        ' Dim data As ClassType = CType(e.Item.DataItem, ClassType)
        Exit Sub
        Try
            Dim rpt As Repeater
            rpt = sender

            Dim data As DataRowView = e.Item.DataItem
            Dim r As clstask = rpt.DataSource(e.Item.ItemIndex + 1)
            Dim li As LiteralControl = e.Item.Controls(0)

            '  FormatItem(r, li)
            ' Exit Sub
            Dim lsDate As String
            Dim llDays As Long = (DateDiff("d", Now, r.CreateDate))
            Select Case System.Math.Abs(llDays)
                Case 0 To 14
                    lsDate = "Recent"
                Case 14 To 30
                    lsDate = "Earlier"
                Case Else
                    lsDate = " Older"
            End Select
            li.Text = "<div Class='box-title border-bottom p-3'><h6 Class='m-0'>" & lsDate & "</h6></div>"
            If (lsDate = _lastdate) Then
                li.Visible = True
            Else
                li.Visible = True
                _lastdate = lsDate
            End If


        Catch ex As Exception
            Dim msError As String = ex.Message
        End Try
    End Sub

    Sub FormatItem(Item As clstask, l As LiteralControl)
        Dim lbRead As Boolean = Item.Read

        Select Case lbRead
            Case True
                l.Text = "OK Man"
            Case False
                ' Item.BackColor = Color.Blue
            Case Else
                '  Item.BackColor = Color.Green
        End Select
    End Sub

    Protected Sub repeaterData_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles repeaterData.ItemCreated

        ' Dim data As ClassType = CType(e.Item.DataItem, ClassType)
        Try
            Dim rpt As Repeater
            rpt = sender
            Dim lbNotFound As Boolean = True
            '      Dim data As DataRowView = e.Item.DataItem
            Dim r As clstask = rpt.DataSource(e.Item.ItemIndex)
            Dim li As Literal
            Dim c As Control
            For Each c In e.Item.Controls
                If c.ClientID.ToUpper.Contains("LITHEADING") Then
                    li = c
                    lbNotFound = False
                    Exit For
                End If
            Next
            If lbNotFound = True Then Exit Sub
            '  FormatItem(r, li)
            ' Exit Sub
            Dim lsDate As String
            Dim llDays As Long = (DateDiff("d", Now, r.CreateDate))
            Select Case System.Math.Abs(llDays)
                Case 0 To 14
                    lsDate = "Recent"
                Case 14 To 30
                    lsDate = "Earlier"
                Case Else
                    lsDate = "Stagnant"
            End Select

            If (lsDate = _lastdate) Then
                li.Text = "" 'lsDate & r.CreateDate.ToString
            Else
                li.Text = "<div Class='box-title border-bottom p-3'><h6 Class='m-0'>" & lsDate & "</h6></div>"
                _lastdate = lsDate
            End If


        Catch ex As Exception
            Dim msError As String = ex.Message
        End Try
    End Sub
End Class