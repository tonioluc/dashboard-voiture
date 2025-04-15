Imports MySql.Data.MySqlClient
Public Class Connexion
    Private Shared connectionString As String = "Server=localhost;Database=dashboard_voiture;Uid=root;Pwd=;"
    Public Shared Function TesterConnexion() As Boolean
        Dim connexion As New MySqlConnection(connectionString)

        Try
            connexion.Open()
            MessageBox.Show("Connexion réussi !")
            Return True
        Catch ex As Exception
            MessageBox.Show("Erreur de connexion : " & ex.Message)
            Return False
        Finally
            If connexion.State = ConnectionState.Open Then
                connexion.Close()
            End If
        End Try
    End Function

    Public Shared Function getConnexion() As MySqlConnection
        Return New MySqlConnection(connectionString)
    End Function
End Class
