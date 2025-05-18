Public Class Form1

    ' Variabel untuk menyimpan angka pertama, operasi yang dipilih, dan status operasi
    Private firstNumber As Double = 0
    Private operation As String = ""
    Private isOperationPerformed As Boolean = False

    ' Event handler untuk tombol angka (0-9)
    Private Sub btnNumber_Click(sender As Object, e As EventArgs) Handles _
        btn0.Click, btn1.Click, btn2.Click, btn3.Click, btn4.Click,
        btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click

        Dim btn As Button = CType(sender, Button)

        ' Jika sedang setelah operasi, reset display
        If txtDisplay.Text = "0" Or isOperationPerformed Then
            txtDisplay.Text = ""
            isOperationPerformed = False
        End If

        ' Tambah angka ke display
        txtDisplay.Text &= btn.Text
    End Sub

    ' Event handler untuk tombol operasi (+, -, *, /)
    Private Sub btnOperation_Click(sender As Object, e As EventArgs) Handles btnAdd.Click, btnSubtract.Click, btnMultiply.Click, btnDivide.Click

        Dim btn As Button = CType(sender, Button)

        ' Simpan angka pertama dan operasi
        firstNumber = Double.Parse(txtDisplay.Text)
        operation = btn.Text
        isOperationPerformed = True
    End Sub

    ' Event handler untuk tombol sama dengan (=)
    Private Sub btnEquals_Click(sender As Object, e As EventArgs) Handles btnEquals.Click
        Dim secondNumber As Double = Double.Parse(txtDisplay.Text)
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

        ' Tampilkan hasil
        txtDisplay.Text = result.ToString()
        operation = ""
        isOperationPerformed = True
    End Sub

    ' Event handler tombol clear (C)
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtDisplay.Text = "0"
        firstNumber = 0
        operation = ""
        isOperationPerformed = False
    End Sub

    ' Event handler agar txtDisplay hanya terima angka dan satu titik desimal
    Private Sub txtDisplay_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDisplay.KeyPress
        ' Cek input, hanya angka, backspace, dan titik desimal
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back AndAlso e.KeyChar <> "."c Then
            e.Handled = True
        End If

        ' Batasi hanya satu titik desimal
        If e.KeyChar = "."c AndAlso txtDisplay.Text.Contains(".") Then
            e.Handled = True
        End If
    End Sub

End Class