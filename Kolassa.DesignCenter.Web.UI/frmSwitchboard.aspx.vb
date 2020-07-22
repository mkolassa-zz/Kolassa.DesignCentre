Imports Google.GData.Calendar
Imports Google.GData.Extensions
Imports Google.GData.Client
Public Class frmSwitchboard
    Inherits System.Web.UI.Page

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    ' Protected  WithEvents btnOption1 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel1 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents btnOption2 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel2 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents btnOption3 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel3 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents btnOption4 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel4 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents btnOption5 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel5 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents btnOption6 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel6 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents btnOption7 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel7 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents btnOption8 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel8 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents txtText33 As System.Web.UI.WebControls.TextBox
    ' Protected  WithEvents txtText35 As System.Web.UI.WebControls.TextBox
    ' Protected  WithEvents pnlBox36 As System.Web.UI.WebControls.Panel
    ' Protected  WithEvents lblLabel37 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents txtText0 As System.Web.UI.WebControls.TextBox
    ' Protected  WithEvents imgImage38 As System.Web.UI.WebControls.Image
    ' Protected  WithEvents btnOption9 As System.Web.UI.WebControls.Button
    ' Protected  WithEvents lblOptionLabel9 As System.Web.UI.WebControls.Label
    ' Protected  WithEvents lblGenerated As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If IsPostBack Then
            Return
        End If
    End Sub

    Protected Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Dim entry As New EventEntry
        Dim lsDescription As String = ""
        Dim u As MembershipUser
        u = Membership.GetUser
        Dim lsUser As String
        If u Is Nothing Then
            lsUser = ""
        Else
            lsUser = " (" & u.UserName & ")"
        End If
      
        ' Set the title and content of the entry.

        Dim MyProperty As New ExtendedProperty

        entry.Title.Text = txtSubject.Text & lsUser
        If chkExclusive.Checked Then
            lsDescription = lsDescription & "** Exclusive ** - "
            MyProperty = New ExtendedProperty
            MyProperty.Name = "Exclusive"
            MyProperty.Value = "True"
            entry.ExtensionElements.Add(MyProperty)
            Dim wc As New WebContentLink
            wc.Title = "Exclusive"
            wc.Type = "image/gif"
            wc.Url = "http://www.google.com" '/logos/worldcup06.gif"
            wc.Icon = "http://www.parallaxsolutions.com/images/e.gif"
            wc.Width = 276
            wc.Height = 120
            entry.WebContentLink = wc
            'wc.

        End If
        If chkMaster.Checked Then
            lsDescription = lsDescription & chkMaster.Text & " - "
            MyProperty = New ExtendedProperty
            MyProperty.Name = "Master"
            MyProperty.Value = "True"
            entry.ExtensionElements.Add(MyProperty)
            Dim wc As New WebContentLink
            wc.Title = "Master Bedroom"
            wc.Type = "image/gif"
            wc.Url = "http://www.google.com" '/logos/worldcup06.gif"
            wc.Icon = "http://www.parallaxsolutions.com/images/m.gif"
            wc.Width = 276
            wc.Height = 120
            entry.WebContentLink = wc
        End If
        If chkBed1.Checked Then
            lsDescription = lsDescription & chkBed1.Text & " - "
            MyProperty = New ExtendedProperty
            MyProperty.Name = "Bed2"
            MyProperty.Value = "True"
            entry.ExtensionElements.Add(MyProperty)
            Dim wc As New WebContentLink
            wc.Title = "Bed 2 (2 Twins)"
            wc.Type = "image/gif"
            wc.Url = "http://www.google.com" '/logos/worldcup06.gif"
            wc.Icon = "http://www.parallaxsolutions.com/images/2.gif"
            wc.Width = 276
            wc.Height = 120
            entry.WebContentLink = wc
        End If
        If ChkBed2.Checked Then
            lsDescription = lsDescription & ChkBed2.Text & " - "
            MyProperty = New ExtendedProperty
            MyProperty.Name = "Bed3"
            MyProperty.Value = "True"
            entry.ExtensionElements.Add(MyProperty)
            Dim wc As New WebContentLink
            wc.Title = "Bed3 Queen"
            wc.Type = "image/gif"
            wc.Url = "http://www.google.com" '/logos/worldcup06.gif"
            wc.Icon = "http://www.parallaxsolutions.com/images/3.gif"
            wc.Width = 276
            wc.Height = 120
            entry.WebContentLink = wc
        End If
        entry.Content.Content = lsDescription

        Dim lsCalendarID As String = "mkolassa@gmail.com"
        lsCalendarID = "2hvquuk1fhdk5phr0v3v0rk43k@group.calendar.google.com"

        Dim postUri As New Uri("http://www.google.com/calendar/feeds/" & lsCalendarID & "/private/full") '"http://www.google.com/calendar/feeds/2hvquuk1fhdk5phr0v3v0rk43k/private/full")

        Dim myService As New CalendarService("exampleCo-exampleApp-1")
        myService.setUserCredentials("mkolassa@gmail.com", "IPsum238")


        ' Set a location for the event.
        Dim eventLocation As New Google.GData.Extensions.Where

        eventLocation.ValueString = "Galena, IL"
        entry.Locations.Add(eventLocation)

        Dim eventTime As New Google.GData.Extensions.When(CDate(ctrlField1.Text), CDate(ctrlField2.Text)) ' DateTime.Now.AddHours(2))
        entry.Times.Add(eventTime)

        ' Send the request and receive the response:
        Dim insertedEntry As New Google.GData.Client.AtomEntry


        insertedEntry = myService.Insert(postUri, entry)

    End Sub

    Sub InsertThisEntry(ByVal lsTitle As String, ByVal lsEmail As String, ByVal lsAuthor As String, ByVal lsContent As String)
        Dim entry As New AtomEntry
        Dim author As New AtomPerson
        author = New AtomPerson(AtomPersonType.Author)
        author.Name = lsAuthor
        author.Email = lsEmail
        entry.Authors.Add(author)
        entry.Title.Text = lsTitle

        entry.Content.Content = lsContent
        Dim postUri As Uri
        postUri = New Uri("http://www.google.com/calendar/feeds/mkolassa@gmail.com/private/full")

        '/ Send the request and receive the response:
        Dim insertedEntry As AtomEntry

        Dim myService As New CalendarService("exampleCo-exampleApp-1")
        myService.setUserCredentials("mkolassa@gmail.com", "IPsum238")

        insertedEntry = myService.Insert(postUri, entry)

    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'InsertThisEntry()
    End Sub


    Public Sub PickDate()
        Me.ctrlField1.Text = Calendar1.SelectedDate.ToShortDateString()
        ToggleCalendar()
    End Sub
    Private Sub ToggleCalendar()
        If Panel1.Visible Then
            Panel1.Visible = False
            Panel1.CssClass = "calendarHide"
        Else
            Panel1.Visible = True
            Panel1.CssClass = "calendarShow"
        End If
    End Sub
    Public Sub PickDate2()
        Me.ctrlField2.Text = Calendar2.SelectedDate.ToShortDateString()
        ToggleCalendar2()
    End Sub
    Private Sub ToggleCalendar2()
        If Panel2.Visible Then
            Panel2.Visible = False
            Panel2.CssClass = "calendarHide"
        Else
            Panel2.Visible = True
            Panel2.CssClass = "calendarShow"
        End If
    End Sub
    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ToggleCalendar()
    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PickDate()
    End Sub

    Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        ToggleCalendar2()
    End Sub

    Protected Sub Calendar2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        PickDate2()
    End Sub
    Public Sub sGetEvents()
        Dim lsCalendarID As String = "mkolassa@gmail.com"
        lsCalendarID = "2hvquuk1fhdk5phr0v3v0rk43k@group.calendar.google.com"

        Dim postUri As New Uri("http://www.google.com/calendar/feeds/" & lsCalendarID & "/private/full") '"http://www.google.com/calendar/feeds/2hvquuk1fhdk5phr0v3v0rk43k/private/full")

        Dim myService As New CalendarService("exampleCo-exampleApp-1")
        myService.setUserCredentials("mkolassa@gmail.com", "IPsum238")

        Dim ds As New System.Data.DataSet
        Dim query As CalendarQuery = New CalendarQuery()

        query.Uri = postUri
        'Dim resultfeed As EventFeed = (myService.Query(query))
        '  Dim resultfeed As CalendarFeed = (myService.Query(query).Entries)
        Dim lsMsg As String = ""

        Dim calEntry As EventEntry 'CalendarEntry
        ds.Tables.Add("Events")
        Dim dt As System.Data.DataTable
        dt = ds.Tables("Events")
        dt.Columns.Add("Event")
        dt.Columns.Add("PropertyName")
        dt.Columns.Add("PropertyValue")
        Dim myProperty As ExtendedProperty
        query.StartDate = #1/1/2008#
        query.EndDate = #1/1/2009#
        Dim cQuery = New EventQuery
        cQuery.Uri = postUri
        Dim resultfeed As EventFeed = (myService.Query(cQuery))
        Dim myExt As Object
        For Each calEntry In resultfeed.Entries
            'On Error Resume Next
            For Each myExt In calEntry.ExtensionElements
                If myExt.GetType.FullName = "Google.GData.Extensions.ExtendedProperty" Then
                    myProperty = myExt
                    dt.Rows.Add(calEntry.Title.Text, myProperty.Name.ToString, myProperty.Value.ToString)
                End If
            Next
        Next
        'litmsg.Text = lsMsg
        ' fGetEvents = ds
        GridView1.DataSource = ds
        GridView1.DataBind()
        Exit Sub
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        sGetEvents()
    End Sub

End Class
