Imports System.Globalization

Public Class Form1

    Private firstNumber As Double = 0
    Private operation As String = ""
    Private isOperationPerformed As Boolean = False

    ' Format angka ribuan otomatis (Indonesia)
    Private Function FormatNumberWithThousands(numberText As String) As String
        Dim number As Double
        If Double.TryParse(numberText.Replace(".", "").Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, number) Then
            If number = Math.Truncate(number) Then
                Return number.ToString("N0", New CultureInfo("id-ID"))
            Else
                Return number.ToString("N2", New CultureInfo("id-ID"))
            End If
        End If
        Return numberText
    End Function

    ' Klik tombol angka 0–9
    Private Sub btnNumber_Click(sender As Object, e As EventArgs) Handles _
        btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click,
        btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

        Dim btn As Button = CType(sender, Button)

        If txtDisplay.Text = "0" Or isOperationPerformed Then
            txtDisplay.Text = ""
            isOperationPerformed = False
        End If

        Dim currentText As String = txtDisplay.Text.Replace(".", "").Replace(",", ".")
        currentText &= btn.Text

        txtDisplay.Text = FormatNumberWithThousands(currentText)
    End Sub

    ' Tombol titik (desimal)
    Private Sub btnDot_Click(sender As Object, e As EventArgs) Handles btnDot.Click
        If isOperationPerformed Then
            txtDisplay.Text = "0"
            isOperationPerformed = False
        End If

        If Not txtDisplay.Text.Contains(",") Then
            txtDisplay.Text &= ","
        End If
    End Sub

    ' Tombol operasi (+ - * /)
    Private Sub btnOperation_Click(sender As Object, e As EventArgs) Handles _
        btnAdd.Click, btnSubtract.Click, btnMultiply.Click, btnDivide.Click

        Dim btn As Button = CType(sender, Button)

        Try
            firstNumber = Double.Parse(txtDisplay.Text.Replace(".", "").Replace(",", "."), CultureInfo.InvariantCulture)
            operation = btn.Text
            isOperationPerformed = True
        Catch ex As Exception
            MessageBox.Show("Format angka tidak valid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Sama dengan (=)
    Private Sub btnEquals_Click(sender As Object, e As EventArgs) Handles btnEquals.Click
        Try
            Dim secondNumber As Double = Double.Parse(txtDisplay.Text.Replace(".", "").Replace(",", "."), CultureInfo.InvariantCulture)
            Dim result As Double

            Select Case operation
                Case "+"
                    result = firstNumber + secondNumber
                Case "-"
                    result = firstNumber - secondNumber
                Case "*"
                    result = firstNumber * secondNumber
                Case "/"
                    If secondNumber = 0 Then
                        MessageBox.Show("Tidak bisa dibagi dengan nol!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End If
                    result = firstNumber / secondNumber
                Case Else
                    Exit Sub
            End Select

            If result = Math.Truncate(result) Then
                txtDisplay.Text = result.ToString("N0", New CultureInfo("id-ID"))
            Else
                txtDisplay.Text = result.ToString("N2", New CultureInfo("id-ID"))
            End If

            operation = ""
            isOperationPerformed = True
        Catch ex As Exception
            MessageBox.Show("Terjadi kesalahan saat perhitungan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Clear (C)
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtDisplay.Text = "0"
        firstNumber = 0
        operation = ""
        isOperationPerformed = False
    End Sub

    ' Blok input manual, kita tangani pakai KeyDown
    Private Sub txtDisplay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDisplay.KeyPress
        e.Handled = True
    End Sub

    ' Input keyboard: angka, operasi, enter, backspace
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9
                Dim num As String = (e.KeyCode Mod 48).ToString()
                btnNumber_Click(Controls.Find("btn" & num, True).FirstOrDefault(), EventArgs.Empty)

            Case Keys.Add, Keys.Oemplus
                btnOperation_Click(btnAdd, EventArgs.Empty)
            Case Keys.Subtract, Keys.OemMinus
                btnOperation_Click(btnSubtract, EventArgs.Empty)
            Case Keys.Multiply
                btnOperation_Click(btnMultiply, EventArgs.Empty)
            Case Keys.Divide, Keys.OemQuestion
                btnOperation_Click(btnDivide, EventArgs.Empty)

            Case Keys.Decimal, Keys.Oemcomma, Keys.OemPeriod
                btnDot_Click(btnDot, EventArgs.Empty)

            Case Keys.Enter, Keys.Return
                btnEquals_Click(btnEquals, EventArgs.Empty)

            Case Keys.Back
                If txtDisplay.Text.Length > 1 Then
                    Dim rawText As String = txtDisplay.Text.Replace(".", "").Replace(",", ".")
                    rawText = rawText.Substring(0, rawText.Length - 1)
                    txtDisplay.Text = FormatNumberWithThousands(rawText)
                Else
                    txtDisplay.Text = "0"
                End If
        End Select
    End Sub

    ' Form Load
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtDisplay.Text = "0"
        Me.KeyPreview = True ' Agar Form bisa menangkap input keyboard
    End Sub

End Class
