Imports System
Imports System.ComponentModel
Imports System.Net
Imports System.Net.Mail
Imports System.Runtime.CompilerServices
Imports System.Windows
Imports System.Windows.Input

Public Class DelegateCommand(Of TValue)
    Implements ICommand

    Private _command As Action(Of TValue)
    Private _predicate As Action(Of TValue, Boolean)

    Public Sub New(ByVal command As Action(Of TValue), ByVal Optional predicate As Func(Of TValue, Boolean) = Nothing)
        _command = command
    End Sub

    Public Event CanExecuteChanged As EventHandler Implements ICommand.CanExecuteChanged



    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements ICommand.CanExecute
        Return True
    End Function

    Public Sub Execute(ByVal parameter As Object) Implements ICommand.Execute
        _command?.Invoke(CType(parameter, TValue))
    End Sub
End Class
Public Class RichTextMailViewmodel
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Private Sub SetProperty(Of TValue)(ByRef propref As TValue, ByVal value As TValue,
<CallerMemberName> ByVal Optional [property] As String = "")
        propref = value
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs([property]))
    End Sub

    Private smtpClient As SmtpClient

    Public Sub New()
        Dim credentials As NetworkCredential = New NetworkCredential("your-account@gmail.com", "P@55w0rd")
        smtpClient = New SmtpClient("smtp.gmail.com", 587)
        smtpClient.Credentials = credentials
        smtpClient.EnableSsl = True
    End Sub

    Private _from As String

    Public Property From As String
        Get
            Return _from
        End Get
        Set(ByVal value As String)
            SetProperty(_from, value)
        End Set
    End Property

    Private _to As String

    Public Property [To] As String
        Get
            Return _to
        End Get
        Set(ByVal value As String)
            SetProperty(_to, value)
        End Set
    End Property

    Private _cc As String

    Public Property CC As String
        Get
            Return _cc
        End Get
        Set(ByVal value As String)
            SetProperty(_cc, value)
        End Set
    End Property

    Private _subject As String

    Public Property Subject As String
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            SetProperty(_subject, value)
        End Set
    End Property

    Private _message As String

    Public Property Message As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            SetProperty(_message, value)
        End Set
    End Property

    Public ReadOnly Property SendCommand As ICommand
        Get
            Return New DelegateCommand(Of String)(Function(s)
                                                      smtpClient.Send(New MailMessage(_from, _to, _subject, _message))
                                                  End Function)
        End Get
    End Property
End Class
Class MainWindow

End Class
