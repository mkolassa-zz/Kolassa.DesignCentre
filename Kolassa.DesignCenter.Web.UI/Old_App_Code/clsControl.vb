Option Explicit On
Option Strict On

Imports System
Imports System.Web.UI
Imports System.Web.UI.WebControls

Namespace CustomControls
    Public Class FirstControl
        Inherits Control
        Private _message As String = "Hello"

        Public Overridable Property Message() As String
            Get
                Return _message
            End Get
            Set(ByVal value As String)
                _message = value
            End Set
        End Property

        Protected Overrides Sub Render(ByVal writer As HtmlTextWriter)
            writer.Write(("<font> " + Me.Message + "<br>" + "The time on the server is " + System.DateTime.Now.ToLongTimeString() + "</font>"))
        End Sub
    End Class
End Namespace


