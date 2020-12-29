Imports Microsoft.VisualBasic
Imports System.Web.UI.WebControls
Imports System.Web.UI
Partial Public Class clsBase
    Inherits CompositeControl

    Public mrptCtrl As ReportControl
    Public msTag As String
    Public mcSelectedItems As Collection = New Collection
    Public mcSelectedItems2 As Collection = New Collection
    Public miSelectedItem As ReportListItem = New ReportListItem
    Public miSelectedItem2 As ReportListItem = New ReportListItem
    Public mbRefreshList As Boolean = True
	Public msReportType As String = ""
	Public mbRequired As Boolean
	Public mbReadOnly As Boolean
	Public mbValid As Boolean = False
	Public msValidationReason As String = ""
	Protected msValidationPattern As String = ""
	Protected msValidationTitle As String = ""
	Public msFieldName As String
    Public msOperator As String = ""
    Public msDataType As String = ""
    Public miListItems As ReportListItems = New ReportListItems
    Public EditMode As Boolean
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
            Debug.Print("<clsBase.SelectedItems [GET] ClientID = '" & Me.ClientID & "' Count='" & mcSelectedItems.Count & "' />")
            SelectedItems = mcSelectedItems
        End Get
        Set(ByVal value As Collection)
            Debug.Print("<clsBase.SelectedItems [SET] ClientID = '" & Me.ClientID & "' Count='" & mcSelectedItems.Count & "' />")
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


	Public Property ValidationPattern() As String
		Get
			ValidationPattern = msValidationPattern
		End Get
		Set(ByVal value As String)
			msValidationPattern = value
		End Set
	End Property
	Public Property ValidationTitle() As String
		Get
			ValidationTitle = msValidationTitle
		End Get
		Set(ByVal value As String)
			msValidationTitle = value
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

    Public Overridable Sub ForceValidation()
        '   Me.Validate()
    End Sub
    Public Property FieldName() As String
        Get
            FieldName = msFieldName
        End Get
        Set(ByVal value As String)
            msFieldName = value

            ' mnuCTRL.Text = msFieldName
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
        Debug.Print("<clsBase.ctrlBase_Load>")
        Me.Tag = "ReportControl"
        Debug.Print("</clsBase.ctrlBase_Load>")
    End Sub
    Public Overridable Sub SetFields(ByVal lsOption As String, ByVal lsValue As String)
        Debug.Print("<clsBase.SetFields ClientID=" & Me.ClientID & ">")
        Dim lblfn As Label = Me.FindControl("lblFieldName")
        Dim lblf1 As Label = Me.FindControl("lblField1")
        Dim lblf2 As Label = Me.FindControl("lblField2")
        Dim ctrl2 As Control = Me.FindControl("ctrlField2")
        Dim ctrl1 As Control = Me.FindControl("ctrlField1")
        Dim ddl1 As DropDownList = Me.FindControl("DropDownList1")
        msOperator = lsOption

        If Not lblfn Is Nothing Then lblfn.Text = msFieldName
        Select Case lsOption
            Case ("Equals"), "="
                lblf1.Text = "="
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("GreaterThan"), ">"
                lblf1.Text = ">"
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("LessThan"), "<"
                lblf1.Text = "<"
                lblf2.Text = "And <="
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("GreaterThanEqual"), ">="
                lblf1.Text = ">="
                lblf2.Text = "And <="
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("LessThanEqual"), "<="
                lblf1.Text = "<="
                lblf2.Text = "And <="
                lblf2.Visible = False
                ctrl2.Visible = False
            Case ("Between")
                lblf1.Text = ">="
                lblf2.Text = "And <="
                lblf2.Visible = True
                ctrl2.Visible = True
            Case Else
                lblf1.Text = ">="
                lblf2.Text = "And <="
                lblf2.Visible = True
                lblf2.Visible = True
        End Select

        RefreshLists()
        SetVisibility()
        Debug.Print("</clsBase.SetFields>")
    End Sub
    Public Overridable Sub RefreshLists()
	End Sub
    Public Overridable Sub SetVisibility()
        Debug.Print("<clsBase.SetVisibility>")
        Dim lblfn As Label = Me.FindControl("lblFieldName")
        Dim lblf1 As Label = Me.FindControl("lblField1")
        Dim lblf2 As Label = Me.FindControl("lblField2")
        Dim ctrl2 As Control = Me.FindControl("ctrlField2")
        Dim ctrl1 As Control = Me.FindControl("ctrlField1")
        Dim ddl1 As DropDownList = Me.FindControl("DropDownList1")


        If msReportType = "Form" Or msReportType Is Nothing Then

            lblf1.Text = "="
            ddl1.SelectedValue = "="

            If Not lblfn Is Nothing Then
                lblfn.CssClass = "control-label font-weight-bold"

                lblfn.Visible = True
            End If
            ddl1.Visible = False
            lblf1.Visible = False
            lblf2.Visible = False
            ctrl2.Visible = False
        End If
        Debug.Print("</clsBase.SetVisibility>")
    End Sub
    Public Event ControlUpdated(ByVal sender As System.Object, ByVal e As System.EventArgs)
    Public Sub New()
		mrptCtrl = New ReportControl
		'SetVisibility()
	End Sub
    Public Sub New(ByRef lrptctrl As ReportControl)
        mrptCtrl = lrptctrl
    End Sub
    Public Overridable Function Validate() As Boolean
        Debug.Print("<clsBase.Validate>")
        Dim ctrl1 As Control = Me.FindControl("ctrlField1")
        Dim ctrl2 As Control = Me.FindControl("ctrlField2")
        msValidationReason = ""
        If mbRequired = True Then
            If mcSelectedItems.Count = 0 Or (ctrl2.Visible = True And mcSelectedItems2.Count = 0) Then
                msValidationReason = msFieldName & " is required."
                mbValid = False
                Validate = False
                Exit Function
            End If
        End If
        For Each Me.miSelectedItem In mcSelectedItems
            Select Case msDataType
                Case "Date"
                    If Not IsDate(miSelectedItem.Value) Then
                        msValidationReason = miSelectedItem.Value & " is NOT a valid date."
                        mbValid = False
                        Validate = False
                        Exit Function
                    End If
                Case "Numeric", "Number", "Double", "Integer", "Long", "Currency"
                    If Not IsNumeric(miSelectedItem.Value) Then
                        msValidationReason = miSelectedItem.Value & " is NOT a valid Number."
                        mbValid = False
                        Validate = False
                        Exit Function
                    End If
            End Select
        Next
        mbValid = True
        Validate = mbValid
        Debug.Print("</clsBase.Validate>")
    End Function

    Private Sub clsBase_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim t As DateTime = Now
    End Sub
End Class
