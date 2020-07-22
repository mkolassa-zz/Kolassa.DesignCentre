Imports Microsoft.VisualBasic
Partial Class ctrlBase
    Inherits System.Web.UI.UserControl



    Public mbRefreshList As Boolean = True

    Public mrptCtrl As ReportControl
    Public msTag As String
    Public mcSelectedItems As Collection = New Collection
    Public mcSelectedItems2 As Collection = New Collection
    Public miSelectedItem As ReportListItem = New ReportListItem
    Public miSelectedItem2 As ReportListItem = New ReportListItem

    Public mbValid As Boolean = False
    Public msFieldName As String
    Public msOperator As String = ""
    Public msDataType As String = ""
    Public miListItems As ReportListItems = New ReportListItems


    Public Property ListItems() As ReportListItems
        Get
            ListItems = miListItems
        End Get
        Set(ByVal value As ReportListItems)
            miListItems = value
        End Set
    End Property
    Public Property SelectedItems() As Collection
        Get
            SelectedItems = mcSelectedItems
        End Get
        Set(ByVal value As Collection)
            mcSelectedItems = value
        End Set
    End Property
    Public Property SelectedItems2() As Collection
        Get
            SelectedItems2 = mcSelectedItems2
        End Get
        Set(ByVal value As Collection)
            mcSelectedItems2 = value
        End Set
    End Property
    Public Property DataOperator() As String
        Get
            DataOperator = msOperator
        End Get
        Set(ByVal value As String)
            msOperator = value
        End Set
    End Property
    Public Property DataType() As String
        Get
            DataType = msDataType
        End Get
        Set(ByVal value As String)
            msDataType = value
        End Set
    End Property

    Public Property Valid() As Boolean
        Get
            Valid = mbValid
        End Get
        Set(ByVal value As Boolean)

        End Set
    End Property

    '  Private Sub ctrlBase_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles me.
    '      UpdateParent()
    ' End Sub


    Public Sub UpdateParent()

        '  mrptCtrl.SelectedItems = mcSelectedItems
        '  mrptCtrl.SelectedItems2 = mcSelectedItems2

        'mrptCtrl.FieldName = msFieldName
        'mrptCtrl.DataOperator = msOperator
        'mrptCtrl.DataType = msDataType

    End Sub
    Public Overridable Sub ForceValidation()
        '   Me.Validate()
        UpdateParent()
    End Sub
    Public Property FieldName() As String
        Get
            FieldName = msFieldName
        End Get
        Set(ByVal value As String)
            msFieldName = value

            mnuCTRL.Text = msFieldName
        End Set
    End Property
    Public Property Tag() As String
        Get
            Tag = msFieldName
        End Get
        Set(ByVal value As String)
            msTag = value
        End Set
    End Property
    Private Sub ctrlBase_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Tag = "ReportControl"

        UpdateParent()
    End Sub
    '  Private Sub mnuEqual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEqual.Click
    '     SetFields("Equals", "")
    'End Sub
    'Private Sub mnuGreaterThan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGreaterThan.Click
    '    SetFields("GreaterThan", "")
    'End Sub
    'Private Sub mnuLessThan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLessThan.Click
    '    SetFields("LessThan", "")
    'End Sub
    'Private Sub mnuGreaterThanEqual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGreaterThanEqual.Click
    '    SetFields("GreaterThanEqual", "")
    'End Sub
    'Private Sub mnuLessThanEqual_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuLessThanEqual.Click
    '    SetFields("LessThanEqual", "")
    'End Sub
    'Private Sub mnuBetween_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuBetween.Click
    '    SetFields("Between", "")
    'End Sub
    Public Overridable Sub SetFields(ByVal lsOption As String, ByVal lsValue As String)
    End Sub
    Public Overridable Sub RefreshLists()
    End Sub
    Public Event ControlUpdated(ByVal sender As System.Object, ByVal e As System.EventArgs)

    Public Sub New()
        mrptCtrl = New ReportControl
    End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
End Class