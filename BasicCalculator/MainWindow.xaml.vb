Imports System
Imports System.Windows
Imports System.Windows.Automation
Imports System.Windows.Automation.Provider
Imports System.Windows.Automation.Peers

Class MainWindow

    Dim clearDisplay As Boolean = True
    Dim Operand1 As Double
    Dim Operand2 As Double
    Dim MathOperator As String

    Private Sub DigitClick(sender As Object, e As RoutedEventArgs) Handles bttn1.Click, bttn0.Click, bttn2.Click,
                                                                        bttn3.Click, bttn4.Click, bttn5.Click, bttn6.Click,
                                                                        bttn7.Click, bttn8.Click, bttn9.Click
        If clearDisplay Then
            lblDisplay.Content = " "
            clearDisplay = False
        End If
        lblDisplay.Content = lblDisplay.Content + CType(sender, Button).Content
    End Sub

    Private Sub bttnClear_Click(sender As Object, e As RoutedEventArgs) Handles bttnClear.Click
        lblDisplay.Content = ""
    End Sub

    Private Sub bttnperiod_Click(sender As Object, e As RoutedEventArgs) Handles bttnperiod.Click
        If lblDisplay.Content.ToString.IndexOf(".") >= 0 Then
            Return
        Else
            lblDisplay.Content = lblDisplay.Content.ToString + "."
        End If
    End Sub

    Private Sub bttnPlus_Click(sender As Object, e As RoutedEventArgs) Handles bttnPlus.Click
        Operand1 = Convert.ToDouble(lblDisplay.Content)
        MathOperator = "+"
        clearDisplay = True
    End Sub

    Private Sub bttnMinus_Click(sender As Object, e As RoutedEventArgs) Handles bttnMinus.Click
        Operand1 = Convert.ToDouble(lblDisplay.Content)
        MathOperator = "-"
        clearDisplay = True
    End Sub

    Private Sub bttnMultiply_Click(sender As Object, e As RoutedEventArgs) Handles bttnMultiply.Click
        Operand1 = Convert.ToDouble(lblDisplay.Content)
        MathOperator = "*"
        clearDisplay = True
    End Sub

    Private Sub bttnDivide_Click(sender As Object, e As RoutedEventArgs) Handles bttnDivide.Click
        Operand1 = Convert.ToDouble(lblDisplay.Content)
        MathOperator = "/"
        clearDisplay = True
    End Sub

    Private Sub bttnEqual_Click(sender As Object, e As RoutedEventArgs) Handles bttnEqual.Click
        Dim result As Double
        Operand2 = Convert.ToDouble(lblDisplay.Content)
        Try
            Select Case MathOperator
                Case "+"
                    result = Operand1 + Operand2
                Case "-"
                    result = Operand1 - Operand2
                Case "*"
                    result = Operand1 * Operand2
                Case "/"
                    result = Operand1 / Operand2
            End Select
            lblDisplay.Content = result.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            result = "ERROR"
        Finally
            clearDisplay = True
        End Try
        

    End Sub

    'For the folks who don't like to use the mouse
    'WPF does not have the convenient PerformClick() so we need to dig deep into the .NET Framework...
    Private Sub Window_KeyDown(sender As Object, e As KeyEventArgs)
        Dim b_peer As ButtonAutomationPeer = Nothing
        Select Case e.Key
            Case Key.D1, Key.NumPad1
                b_peer = New ButtonAutomationPeer(bttn1)              
            Case Key.D2, Key.NumPad2
                b_peer = New ButtonAutomationPeer(bttn2)               
            Case Key.D3, Key.NumPad3
                b_peer = New ButtonAutomationPeer(bttn3)
            Case Key.D4, Key.NumPad4
                b_peer = New ButtonAutomationPeer(bttn4)
            Case Key.D5, Key.NumPad5
                b_peer = New ButtonAutomationPeer(bttn5)
            Case Key.D6, Key.NumPad6
                b_peer = New ButtonAutomationPeer(bttn6)
            Case Key.D7, Key.NumPad7
                b_peer = New ButtonAutomationPeer(bttn7)
            Case Key.D8, Key.NumPad8
                b_peer = New ButtonAutomationPeer(bttn8)
            Case Key.D9, Key.NumPad9
                b_peer = New ButtonAutomationPeer(bttn9)
            Case Key.D0, Key.NumPad0
                b_peer = New ButtonAutomationPeer(bttn0)
            Case Key.Decimal, Key.OemPeriod
                b_peer = New ButtonAutomationPeer(bttnperiod)
            Case Key.C
                b_peer = New ButtonAutomationPeer(bttnClear)
            Case Key.Add
                b_peer = New ButtonAutomationPeer(bttnPlus)
            Case Key.OemPlus
                If Keyboard.IsKeyDown(Key.LeftShift) Or Keyboard.IsKeyDown(Key.RightShift) Then
                    b_peer = New ButtonAutomationPeer(bttnPlus)
                Else
                    b_peer = New ButtonAutomationPeer(bttnEqual)
                End If
            Case Key.OemMinus, Key.Subtract
                b_peer = New ButtonAutomationPeer(bttnMinus)
            Case Key.Multiply
                b_peer = New ButtonAutomationPeer(bttnMultiply)
            Case Key.Divide, Key.OemBackslash
                b_peer = New ButtonAutomationPeer(bttnDivide)
            Case Key.Enter, Key.LineFeed
                b_peer = New ButtonAutomationPeer(bttnEqual)
        End Select
        If b_peer IsNot Nothing Then
            Dim invokeProv As IInvokeProvider = b_peer.GetPattern(PatternInterface.Invoke)
            invokeProv.Invoke()
        Else
            Return
        End If
    End Sub

    Private Sub bttnInverse_Click(sender As Object, e As RoutedEventArgs) Handles bttnInverse.Click
        Operand1 = Convert.ToDouble(lblDisplay.Content)
        Dim result As Double
        result = 1 / Operand1
        lblDisplay.Content = result
        clearDisplay = True
    End Sub

    Private Sub bttnNegative_Click(sender As Object, e As RoutedEventArgs) Handles bttnNegative.Click
        Dim value As Double = Convert.ToDouble(lblDisplay.Content.ToString)
        value = value * (-1)
        lblDisplay.Content = value
    End Sub
End Class
