Imports MySql.Data.MySqlClient

Public Class Voiture
    Public Property Id As Integer
    Public Property Nom As String
    Public Property Acceleration As Single
    Public Property Deceleration As Single
    Public Property Vitesse As Single
    Public Property CapaciteReservoir As Single?
    Public Property VitesseMax As Single?
    Public Property ConsommationMax As Single? 'L/s (100% an'ny acceleration)
    Public Shared Function GetAllVoiture() As List(Of Voiture)
        Dim listeVoitures As New List(Of Voiture)()

        Try
            Using con As MySqlConnection = Connexion.getConnexion()
                con.Open()
                Dim req As String = "SELECT * FROM voiture"
                Using cmd As New MySqlCommand(req, con)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        While reader.Read()
                            Dim v As New Voiture() With {
                                .Id = reader.GetInt32("id"),
                                .Nom = reader.GetString("nom"),
                                .Acceleration = reader.GetFloat("acceleration"),
                                .Deceleration = reader.GetFloat("deceleration"),
                                .Vitesse = reader.GetFloat("vitesse"),
                                .CapaciteReservoir = If(reader.IsDBNull(reader.GetOrdinal("capaciteReservoir")), CType(Nothing, Single?), reader.GetFloat("capaciteReservoir")),
                                .VitesseMax = If(reader.IsDBNull(reader.GetOrdinal("vitesseMax")), CType(Nothing, Single?), reader.GetFloat("vitesseMax")),
                                .ConsommationMax = If(reader.IsDBNull(reader.GetOrdinal("consommationMax")), CType(Nothing, Single?), reader.GetFloat("consommationMax"))
                            }
                            listeVoitures.Add(v)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Erreur lors du chargement des voitures : " & ex.Message)
        End Try

        Return listeVoitures
    End Function

    Public Function GetDerniereVitesse() As Single
        Dim derniereVitesse As Single = 0

        Using conn As MySqlConnection = Connexion.getConnexion()
            conn.Open()

            ' 1re requête : chercher la dernière vitesse dans enregistrement
            Dim query1 As String = "SELECT vitesse FROM enregistrement WHERE idVoiture = @idVoiture ORDER BY id DESC LIMIT 1;"
            Using cmd1 As New MySqlCommand(query1, conn)
                cmd1.Parameters.AddWithValue("@idVoiture", Me.Id)

                Using reader1 As MySqlDataReader = cmd1.ExecuteReader()
                    If reader1.Read() AndAlso Not reader1.IsDBNull(0) Then
                        derniereVitesse = Convert.ToSingle(reader1("vitesse"))
                        Return derniereVitesse ' On retourne directement si trouvé
                    End If
                End Using
            End Using

            ' 2e requête : sinon, récupérer la vitesse de base dans voiture
            Dim query2 As String = "SELECT vitesse FROM voiture WHERE id = @id;"
            Using cmd2 As New MySqlCommand(query2, conn)
                cmd2.Parameters.AddWithValue("@id", Me.Id)

                Using reader2 As MySqlDataReader = cmd2.ExecuteReader()
                    If reader2.Read() AndAlso Not reader2.IsDBNull(0) Then
                        derniereVitesse = Convert.ToSingle(reader2("vitesse"))
                    End If
                End Using
            End Using
        End Using

        Return derniereVitesse
    End Function

    Public Function calculConsoMax100km(distanceParcourueKm As Single, resteCarburant As Single) As Single
        Dim result As Single = 0.0F
        Dim essenceConsommee As Double = Me.CapaciteReservoir - resteCarburant
        If distanceParcourueKm <= 0 Then
            Return 0.0F
        End If
        result = (essenceConsommee / distanceParcourueKm) * 100
        Return result
    End Function

End Class
