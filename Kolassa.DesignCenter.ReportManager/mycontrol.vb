Imports System.ComponentModel
Imports System.Drawing

<DefaultProperty("Text"), ToolboxData("<{0}:mycontrol_ runat=server></{0}:mycontrol_>"), ToolboxBitmap(GetType(Calendar))>
Public Class mycontrol_
    Inherits CompositeControl

    Dim tb As TextBox
    Dim cal As Calendar
    Dim imgbt As ImageButton
    <Category("Appearance")>
    <Description("This is the Selected Date")>
    Public Property SelectedDate() As DateTime
        Get
            EnsureChildControls()
            If (tb.Text) Is Nothing Then
                Return DateTime.MinValue
            Else
                Return Convert.ToDateTime(tb.Text)
            End If

        End Get
        Set(ByVal value As DateTime)
            EnsureChildControls()
            If IsDBNull(value) Then
                tb.Text = ""
                cal.VisibleDate = DateTime.Today
            Else
                tb.Text = value.ToShortDateString()
                cal.SelectedDate = value
            End If
        End Set
    End Property
    <Category("Appearance")>
    <Description("Changes the way it it looks")>
    Public Property ImageButtonImageURL() As String
        Get
            EnsureChildControls()
            If (imgbt.ImageUrl) Is Nothing Then
                Return String.Empty
            Else
                Return imgbt.ImageUrl
            End If

        End Get
        Set(ByVal value As String)
            EnsureChildControls()
            imgbt.ImageUrl = value
        End Set
    End Property
    Protected Overrides Sub RecreateChildControls()
        EnsureChildControls()
    End Sub
    Protected Overrides Sub CreateChildControls()
        Controls.Clear()
        tb = New TextBox
        tb.ID = "datetextbox"
        tb.Width = Unit.Pixel(80)
        cal = New Calendar
        cal.ID = "calendarControl"
        cal.Visible = False
        imgbt = New ImageButton
        imgbt.ID = "calendarImageButton"
        AddHandler imgbt.Click, AddressOf imgbt_Click
        AddHandler cal.SelectionChanged, AddressOf cal_SelectionChanged


        Controls.Add(tb)
        Controls.Add(cal)
        Controls.Add(imgbt)
    End Sub
    Private Sub imgbt_Click(sender As Object, e As ImageClickEventArgs)
        If cal.Visible Then
            cal.Visible = False
        Else
            cal.Visible = True
            If IsDBNull(tb.Text) Then
                cal.VisibleDate = DateTime.Today
            Else
                Dim output As DateTime = DateTime.Today
                Dim isDateTimeConversionSuccessful As Boolean = DateTime.TryParse(tb.Text, output)
                cal.VisibleDate = output

            End If
        End If
    End Sub
    Private Sub cal_SelectionChanged(sender As Object, e As EventArgs)
        tb.Text = cal.SelectedDate.ToShortDateString
        Dim eventData As DateSelectedEventArgs = New DateSelectedEventArgs(SelectedDate)
        onDateSelection(eventData)
        cal.Visible = False
    End Sub

    Protected Overrides Sub Render(writer As HtmlTextWriter)
        AddAttributesToRender(writer)
        writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1")
        writer.RenderBeginTag(HtmlTextWriterTag.Table)
        writer.RenderBeginTag(HtmlTextWriterTag.Tr)
        writer.RenderBeginTag(HtmlTextWriterTag.Td)
        tb.RenderControl(writer)
        writer.RenderEndTag() ' </td>

        writer.RenderBeginTag(HtmlTextWriterTag.Td)
        imgbt.RenderControl(writer)
        writer.RenderEndTag() ' </td>
        writer.RenderEndTag() ' </tr>
        writer.RenderEndTag() ' </table>
        cal.RenderControl(writer)

    End Sub
    Public Event DateSelected As DateSelectedEventHandler
    Protected Overridable Sub onDateSelection(e As DateSelectedEventArgs)

        RaiseEvent DateSelected(Me, e)

    End Sub
End Class
Public Class DateSelectedEventArgs
    Inherits EventArgs
    Private _SelectedDate As DateTime
    Public Sub New(SelectedDate As DateTime)
        _SelectedDate = SelectedDate
    End Sub
    Public Sub DateSelectedEventArgs(SelectedDate As DateTime)
        _SelectedDate = SelectedDate
    End Sub
    Public ReadOnly Property SelectedDate As DateTime
        Get
            Return _SelectedDate
        End Get
    End Property
End Class
Public Delegate Sub DateSelectedEventHandler(Sender As Object, e As DateSelectedEventArgs)
