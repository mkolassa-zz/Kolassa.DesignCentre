Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Public Class ctrlNotificationsFull
    Inherits System.Web.UI.UserControl
    Dim _lastdate As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '*** This must be here
        Page.Form.Enctype = "multipart/form-data"
        Dim lsID As String = "00000000-0000-0000-0000-000000000000"
        Dim s As String = Request.Form("__EVENTTARGET")
        If s Is Nothing Then Exit Sub
        If s.Length > 36 Then lsID = Right(s, 36)
        If s.ToUpper.Contains("LNK") Then

            Dim t As New clsTasks
            Dim tsk As New clstask

            Dim ds As DataSet
            t.GetRecords("", lsID, Session("Project"), Session("NodeID"), HttpContext.Current.User.Identity.GetUserId)

            If s.ToUpper.Contains("LNKCOMPLETE") Then
                tsk.ID = lsID
                tsk.TaskComplete()
            End If
            If s.ToUpper.Contains("LNKREAD") Then
                tsk.ID = lsID
                tsk.TaskRead()
            End If
        End If
    End Sub
    Public Sub BindData()
        repeaterData.DataBind()
    End Sub
    Function DaysFromDate(ByVal dt As Date) As Long
        DaysFromDate = DateDiff("d", dt, Now())
    End Function
    Protected Sub gvData_DataBinding(sender As Object, e As EventArgs) Handles repeaterData.DataBinding
        Dim t As New clsTasks()
        repeaterData.DataSource = t.GetRecords("", "", Session("Project"), Session("NodeID"), HttpContext.Current.User.Identity.GetUserId)
    End Sub
    Protected Sub cmdPostComm_Click(sender As Object, e As EventArgs) Handles cmdPostComm.Click
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

    Protected Sub repeaterData_ItemCreated(sender As Object, e As RepeaterItemEventArgs) Handles repeaterData.ItemCreated
        Try
            Dim rpt As Repeater
            rpt = sender
            Dim lbNotFound As Boolean = True
            '      Dim data As DataRowView = e.Item.DataItem
            Dim r As clstask = rpt.DataSource(e.Item.ItemIndex)
            Dim c As Control
            Dim lnk As LinkButton

            For Each c In e.Item.Controls
                If c.ClientID.ToUpper.Contains("LNKCOMPLETE") Then
                    lnk = c
                    If r.Complete Then
                        lnk.Text = "Complete"
                    Else
                        lnk.Text = "Mark as Complete"
                    End If
                    Exit For
                End If
            Next

            For Each c In e.Item.Controls
                If c.ClientID.ToUpper.Contains("LNKREAD") Then
                    lnk = c
                    If r.Read Then
                        lnk.Text = "Read"
                    Else
                        lnk.Text = "Mark as Read"
                    End If
                    Exit For
                End If
            Next

            Dim li As Literal

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
    Protected Sub ProcessTask(source As Object, e As RepeaterCommandEventArgs) Handles repeaterData.ItemCommand
        Dim t As New clstask
        t.ID = e.CommandArgument
        t.NodeID = Session("NodeID")
        Select Case e.CommandName.ToUpper
            Case "LNKREAD"
                t.TaskRead()
            Case "LNKCOMPLETE"
                t.TaskComplete()
        End Select
    End Sub
End Class