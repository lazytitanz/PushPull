﻿Imports MySql.Data.MySqlClient
Public Class PushPull
    Dim command As MySqlCommand
    Dim mysqlconn As MySqlConnection

    Public Function Hasher(ByVal Content As String) As String
        Dim sha1 As New Security.Cryptography.SHA1CryptoServiceProvider
        Dim bytestring() As Byte = System.Text.Encoding.ASCII.GetBytes(Content)
        bytestring = sha1.ComputeHash(bytestring)

        Dim FinalString As String = Nothing
        For Each bt As Byte In bytestring
            FinalString &= bt.ToString("x2")
        Next
        Return FinalString
    End Function

    Private Sub PushPull_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        mysqlconn = New MySqlConnection
        mysqlconn.ConnectionString = "server=localhost;userid=root;password=;database=push;"
        Try
            mysqlconn.Open()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            mysqlconn.Dispose()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        mysqlconn = New MySqlConnection
        mysqlconn.ConnectionString = "server=localhost;userid=root;password=;database=push;"

        Dim reader As MySqlDataReader
        Dim username As String
        Dim password As String
        Dim combination As String
        Dim hashedcombination As String


        username = TextBox1.Text
        password = TextBox2.Text
        combination = TextBox1.Text.ToUpper + TextBox2.Text.ToUpper

        combination = Hasher(combination)

        hashedcombination = combination.ToUpper

        Try
            mysqlconn.Open()
            Dim query As String
            query = "SELECT * FROM users WHERE username='" & TextBox1.Text & "' and password='" & TextBox2.Text & "' "
            command = New MySqlCommand(query, mysqlconn)
            reader = command.ExecuteReader
            Dim count As Integer
            count = 0
            While reader.Read
                count = count + 1
            End While
        Catch ex As Exception

        End Try







    End Sub

    Private Sub TextBox2_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyDown
        If e.KeyValue = Keys.Enter Then

        End If
    End Sub
End Class