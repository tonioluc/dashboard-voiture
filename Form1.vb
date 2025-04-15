Public Class Form1
    ' Variables globales pour compter le temps
    Private tempsAppuiClavier As Integer = 0 ' Temps en secondes
    Private selectedCar As Voiture
    Private vitesseActuel As Single = 0
    Dim appuiTouche As Boolean = False
    Dim facteurAcceleration As Single = 0 ' 0.1 à 1.0
    Dim resteCarburant As Single = 0
    Dim enTrainDeFreiner As Boolean = False
    Dim distanceParcourueKm As Single = 0
    Dim tempsAffichageEnSeconde As Integer = 0
    Dim pause As Boolean = False
    Dim consoMoyenne100km As Single = 0.0F

    Public Sub CalculerDistanceMRUA(vitesseInitialeKmH As Single, accelerationKmHs As Single, tempsSecondes As Integer)
        Dim v0 As Single = vitesseInitialeKmH * (1000.0F / 3600.0F) ' m/s
        Dim a As Single = accelerationKmHs * (1000.0F / 3600.0F)    ' m/s²
        Dim t As Single = tempsSecondes

        Dim distanceMetres As Single = (0.5F * a * t * t) + (v0 * t)
        Me.distanceParcourueKm += distanceMetres / 1000.0F
    End Sub

    Private Sub afficherInfoVoiture(voiture As Voiture)
        Dim infos As String = ""

        infos &= "Nom : " & voiture.Nom & vbCrLf
        infos &= "Vitesse initiale : " & voiture.Vitesse & " km/h" & vbCrLf
        infos &= "Vitesse maximale : " & voiture.VitesseMax & " km/h" & vbCrLf
        infos &= "Acceleration : " & voiture.Acceleration & " km/h" & vbCrLf
        infos &= "Deceleration : " & voiture.Deceleration & " km/h" & vbCrLf
        infos &= "Capacite du reservoir : " & voiture.CapaciteReservoir & " L" & vbCrLf
        infos &= "Consommation max : " & voiture.ConsommationMax & " L"

        about.Text = infos
    End Sub

    Private Sub dessinerCarburant(carburantActuel As Single, carburantMax As Single)
        Dim bmp As New Bitmap(drawCarburant.Width, drawCarburant.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        ' Dimensions
        Dim width As Integer = drawCarburant.Width - 20
        Dim height As Integer = drawCarburant.Height - 20
        Dim x As Integer = 10
        Dim y As Integer = 10

        ' Bord de la jauge
        g.DrawRectangle(New Pen(Color.White, 2), x, y, width, height)

        ' Calcul du niveau actuel
        Dim pourcentage As Single = carburantActuel / carburantMax
        Dim hauteurCarburant As Integer = CInt(height * pourcentage)

        ' Dessine le niveau de carburant en vert
        g.FillRectangle(Brushes.Green, x + 1, y + height - hauteurCarburant + 1, width - 1, hauteurCarburant - 1)

        ' Ajoute le texte
        Dim txt As String = Math.Round(carburantActuel, 2) & " L / " & carburantMax & " L"
        Dim font As New Font("Arial", 10, FontStyle.Bold)
        g.DrawString(txt, font, Brushes.Black, x + 5, y + 5)

        drawCarburant.Image = bmp
    End Sub

    Private Sub dessinerDashboard(vitesse As Single)
        Dim bmp As New Bitmap(dashboard.Width, dashboard.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim centreX As Integer = dashboard.Width \ 2
        Dim centreY As Integer = dashboard.Height \ 2
        Dim rayon As Integer = Math.Min(centreX, centreY) - 10

        ' Fond
        g.Clear(Color.Black)

        ' Cercle extérieur
        g.DrawEllipse(New Pen(Color.White, 3), centreX - rayon, centreY - rayon, rayon * 2, rayon * 2)

        Dim nbGraduations As Integer = selectedCar.VitesseMax / 25
        Dim fontGraduation As New Font("Arial", 10, FontStyle.Bold)
        Dim brosseTexte As Brush = Brushes.White

        For i As Integer = 0 To nbGraduations
            Dim angle As Double = (i * 180 / nbGraduations) - 180
            Dim radian As Double = angle * Math.PI / 180

            ' Lignes de graduation
            Dim x1 = centreX + Math.Cos(radian) * (rayon - 10)
            Dim y1 = centreY + Math.Sin(radian) * (rayon - 10)
            Dim x2 = centreX + Math.Cos(radian) * rayon
            Dim y2 = centreY + Math.Sin(radian) * rayon
            g.DrawLine(Pens.White, CSng(x1), CSng(y1), CSng(x2), CSng(y2))

            ' Texte des vitesses
            Dim vitesseGraduation As Integer = CInt((i / nbGraduations) * selectedCar.VitesseMax)
            Dim xText = centreX + Math.Cos(radian) * (rayon - 25)
            Dim yText = centreY + Math.Sin(radian) * (rayon - 25)

            ' Centrer le texte
            Dim taille = g.MeasureString(vitesseGraduation.ToString(), fontGraduation)
            xText -= taille.Width / 2
            yText -= taille.Height / 2

            g.DrawString(vitesseGraduation.ToString(), fontGraduation, brosseTexte, CSng(xText), CSng(yText))
        Next

        ' Aiguille selon la vitesse (angle de -90° à +90°)
        Dim maxAngle As Double = 180.0
        Dim angleAiguille As Double = ((vitesse / selectedCar.VitesseMax) * maxAngle) - 180 ' vitesse max simulée : 200 km/h
        Dim xAiguille = centreX + Math.Cos(angleAiguille * Math.PI / 180) * (rayon - 20)
        Dim yAiguille = centreY + Math.Sin(angleAiguille * Math.PI / 180) * (rayon - 20)
        g.DrawLine(New Pen(Color.Red, 3), centreX, centreY, CSng(xAiguille), CSng(yAiguille))

        ' Met à jour le PictureBox
        dashboard.Image = bmp
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer2.Start()
        Me.KeyPreview = True ' Permet de capter les touches même si un contrôle a le focus
        Dim voitures As List(Of Voiture) = Voiture.GetAllVoiture()
        ComboBox1.DataSource = voitures
        ComboBox1.DisplayMember = "Nom"       ' Propriété à afficher
        ComboBox1.ValueMember = "Id"          ' Propriété interne pour l’identifiant
        Dim voitureSelectionnee As Voiture = CType(ComboBox1.SelectedItem, Voiture)
        Me.selectedCar = voitureSelectionnee
        Me.vitesseActuel = selectedCar.GetDerniereVitesse()
        Me.resteCarburant = voitureSelectionnee.CapaciteReservoir
        afficherInfoVoiture(voitureSelectionnee)
        dessinerDashboard(vitesseActuel)
        dessinerCarburant(voitureSelectionnee.CapaciteReservoir, voitureSelectionnee.CapaciteReservoir)
    End Sub

    Private Sub validerChoixVoiture(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.SelectedItem IsNot Nothing Then
            Me.selectedCar = CType(ComboBox1.SelectedItem, Voiture)
            afficherInfoVoiture(selectedCar)
            dessinerCarburant(Me.selectedCar.CapaciteReservoir, Me.selectedCar.CapaciteReservoir)
        Else
            MessageBox.Show("Veuillez sélectionner une voiture.")
        End If
    End Sub

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If Not appuiTouche Then
            If e.KeyCode = Keys.Space OrElse (e.KeyCode >= Keys.D1 AndAlso e.KeyCode <= Keys.D9) Then
                appuiTouche = True
                enTrainDeFreiner = False ' ← Accélération
                Timer1.Start()
                Me.vitesseActuel = selectedCar.GetDerniereVitesse()
                If e.KeyCode = Keys.Space Then
                    facteurAcceleration = 1.0F
                Else
                    facteurAcceleration = (e.KeyCode - Keys.D0) / 10.0F
                End If
            ElseIf e.KeyCode >= Keys.F1 AndAlso e.KeyCode <= Keys.F9 Then
                appuiTouche = True
                enTrainDeFreiner = True ' ← Décélération
                Timer1.Start()
                facteurAcceleration = (e.KeyCode - Keys.F1 + 1) / 10.0F
            ElseIf e.KeyCode = Keys.Enter Then
                appuiTouche = True
                enTrainDeFreiner = True
                Timer1.Start()
                facteurAcceleration = 1.0F
            End If
        End If
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
        If e.KeyCode = Keys.Space OrElse
       (e.KeyCode >= Keys.D1 AndAlso e.KeyCode <= Keys.D9) OrElse
       (e.KeyCode >= Keys.F1 AndAlso e.KeyCode <= Keys.F9) OrElse
       e.KeyCode = Keys.Enter Then

            appuiTouche = False
            Timer1.Stop()
            seconde.Text = "Seconde : 0 s"

            If tempsAppuiClavier > 0 Then
                Dim vitesseFinale As Single = vitesseActuel
                Dim typeForce As Single = If(enTrainDeFreiner, -selectedCar.Deceleration, selectedCar.Acceleration)
                Enregistrement.AjouterEnregistrement(selectedCar.Id, vitesseFinale, typeForce * facteurAcceleration, tempsAppuiClavier, DateTime.Now.TimeOfDay)
            End If

            tempsAppuiClavier = 0
            facteurAcceleration = 0
            enTrainDeFreiner = False
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If appuiTouche Then
            tempsAppuiClavier += 1
            seconde.Text = "Seconde : " & tempsAppuiClavier & " s"

            If enTrainDeFreiner Then
                vitesseActuel -= selectedCar.Deceleration * facteurAcceleration
                If vitesseActuel < 0 Then vitesseActuel = 0
            Else
                vitesseActuel += selectedCar.Acceleration * facteurAcceleration
                If vitesseActuel > selectedCar.VitesseMax Then
                    vitesseActuel = selectedCar.VitesseMax
                End If
                resteCarburant -= selectedCar.ConsommationMax * facteurAcceleration
                If resteCarburant < 0 Then resteCarburant = 0
            End If

            dessinerDashboard(vitesseActuel)
            dessinerCarburant(resteCarburant, selectedCar.CapaciteReservoir)
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        If vitesseActuel > 0 AndAlso Not pause Then
            Dim typeForce As Single = If(enTrainDeFreiner, -selectedCar.Deceleration, selectedCar.Acceleration)
            Me.tempsAffichageEnSeconde += 1
            CalculerDistanceMRUA(vitesseActuel, typeForce * facteurAcceleration, 1) ' 1 seconde par tick
        End If

        ' Affiche la distance avec 2 chiffres après la virgule
        distance_parcourue.Text = "Distance parcourue : " & Math.Round((Me.distanceParcourueKm * 1000), 2) & " m avec t = " & Me.tempsAffichageEnSeconde & " s" & ", Vitesse(m/s) = " & vitesseActuel / 3.6
        consoMoyenne100km = selectedCar.calculConsoMax100km(distanceParcourueKm, Me.resteCarburant)
        consoMoyenne.Text = "Consommation moyenne/100km : " & consoMoyenne100km & " L"
    End Sub


    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Timer2.Stop()
    End Sub

    Private Sub pauseBoutton(sender As Object, e As EventArgs) Handles Button2.Click
        If Not pause Then
            pause = True
            Button2.Text = "Resume"
            Button2.BackColor = Color.Yellow
        Else
            pause = False
            Button2.Text = "Pause"
            Button2.BackColor = Color.LimeGreen
        End If
    End Sub
End Class
