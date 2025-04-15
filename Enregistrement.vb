Imports MySql.Data.MySqlClient

Public Class Enregistrement
    Public Property Id As Integer
    Public Property IdVoiture As Integer
    Public Property t As Integer
    Public Property vitesse As Single
    Public Property Acceleration As Single
    Public Property Heure As TimeSpan

    Public Sub New()
    End Sub

    Public Sub New(id As Integer, idVoiture As Integer, vitesse As Single, acceleration As Single, t As Integer, heure As TimeSpan)
        Me.Id = id
        Me.IdVoiture = idVoiture
        Me.vitesse = vitesse
        Me.Acceleration = acceleration
        Me.Heure = heure
        Me.t = t
    End Sub

    ' 🔧 Méthode statique pour insérer un enregistrement
    Public Shared Sub AjouterEnregistrement(idVoiture As Integer, vitesse As Single, acceleration As Single, t As Integer, heure As TimeSpan)
        Dim con As MySqlConnection = Connexion.getConnexion()

        Try
            con.Open()
            Dim sql As String = "INSERT INTO enregistrement (idVoiture, vitesse, acceleration, heure, t) VALUES (@idVoiture, @vitesse, @acceleration, @heure , @t)"
            Dim cmd As New MySqlCommand(sql, con)

            cmd.Parameters.AddWithValue("@idVoiture", idVoiture)
            cmd.Parameters.AddWithValue("@vitesse", vitesse)
            cmd.Parameters.AddWithValue("@acceleration", acceleration)
            cmd.Parameters.AddWithValue("@t", t)
            cmd.Parameters.AddWithValue("@heure", heure.ToString()) ' Heure formatée hh:mm:ss

            cmd.ExecuteNonQuery()
        Catch ex As Exception
            MessageBox.Show("Erreur lors de l'insertion : " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

End Class
