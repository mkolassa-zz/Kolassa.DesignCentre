Imports Microsoft.VisualBasic

Public Class clsQuote
    Dim msPhase1Status As String
    Dim mdPhase1TargetDate As Date
    Dim msPhase2Status As String
    Dim mdPhase2TargetDate As Date
    Dim mdPhase1CompletionDate As Date
    Dim mdPhase2CompletionDate As Date
    Dim mlQuoteID As Long
    Public Property QuoteID() As Long
        Get
            QuoteID = mlQuoteID
        End Get
        Set(ByVal value As Long)
            mlQuoteID = value
        End Set
    End Property
    Public Property Phase1TargetDate() As Date
        Get
            Phase1TargetDate = mdPhase1TargetDate
        End Get
        Set(ByVal value As Date)
            mdPhase1TargetDate = value
        End Set
    End Property
    Public Property Phase1Status() As String
        Get
            Phase1Status = msPhase1Status
        End Get
        Set(ByVal value As String)
            msPhase1Status = value
            If msPhase1Status = "Completed" Then
                If Not IsDate(mdPhase1CompletionDate) Then
                    Phase1CompletionDate = Date.Today
                End If
            End If
        End Set
    End Property

    Public Property Phase1CompletionDate() As Date
        Get
            Phase1CompletionDate = mdPhase1CompletionDate
        End Get
        Set(ByVal value As Date)
            mdPhase1CompletionDate = value
        End Set
    End Property
    Public Property Phase2TargetDate() As Date
        Get
            Phase2TargetDate = mdPhase2TargetDate
        End Get
        Set(ByVal value As Date)
            mdPhase2TargetDate = value
        End Set
    End Property
    Public Property Phase2Status() As String
        Get
            Phase2Status = msPhase2Status
        End Get
        Set(ByVal value As String)
            msPhase2Status = value
            If msPhase2Status = "Completed" Then
                If Not IsDate(mdPhase2CompletionDate) Then
                    Phase2CompletionDate = Date.Today
                End If
            End If
        End Set
    End Property

    Public Property Phase2CompletionDate() As Date
        Get
            Phase2CompletionDate = mdPhase2CompletionDate
        End Get
        Set(ByVal value As Date)
            mdPhase2CompletionDate = value
        End Set
    End Property
End Class
