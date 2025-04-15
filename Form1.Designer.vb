<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Label1 = New Label()
        ComboBox1 = New ComboBox()
        Button1 = New Button()
        about = New Label()
        Label2 = New Label()
        dashboard = New PictureBox()
        Timer1 = New Timer(components)
        seconde = New Label()
        drawCarburant = New PictureBox()
        distance_parcourue = New Label()
        Timer2 = New Timer(components)
        consoMoyenne = New Label()
        Button2 = New Button()
        CType(dashboard, ComponentModel.ISupportInitialize).BeginInit()
        CType(drawCarburant, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(45, 29)
        Label1.Name = "Label1"
        Label1.Size = New Size(102, 21)
        Label1.TabIndex = 0
        Label1.Text = "Choix voiture"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(166, 29)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(158, 29)
        ComboBox1.TabIndex = 1
        ' 
        ' Button1
        ' 
        Button1.BackColor = SystemColors.ActiveCaption
        Button1.Cursor = Cursors.Hand
        Button1.Location = New Point(367, 24)
        Button1.Name = "Button1"
        Button1.Size = New Size(109, 37)
        Button1.TabIndex = 2
        Button1.Text = "Valider"
        Button1.UseVisualStyleBackColor = False
        ' 
        ' about
        ' 
        about.AutoSize = True
        about.Location = New Point(797, 64)
        about.Name = "about"
        about.Size = New Size(19, 21)
        about.TabIndex = 3
        about.Text = "..."
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Black", 9.216F, FontStyle.Bold Or FontStyle.Italic Or FontStyle.Underline, GraphicsUnit.Point, CByte(161))
        Label2.Location = New Point(797, 37)
        Label2.Name = "Label2"
        Label2.Size = New Size(175, 21)
        Label2.TabIndex = 4
        Label2.Text = "A propos du voiture :"
        ' 
        ' dashboard
        ' 
        dashboard.Location = New Point(45, 95)
        dashboard.Name = "dashboard"
        dashboard.Size = New Size(470, 314)
        dashboard.TabIndex = 5
        dashboard.TabStop = False
        ' 
        ' Timer1
        ' 
        Timer1.Interval = 1000
        ' 
        ' seconde
        ' 
        seconde.AutoSize = True
        seconde.Location = New Point(45, 448)
        seconde.Name = "seconde"
        seconde.Size = New Size(100, 21)
        seconde.TabIndex = 6
        seconde.Text = "Seconde : 0 s"
        ' 
        ' drawCarburant
        ' 
        drawCarburant.Location = New Point(588, 185)
        drawCarburant.Name = "drawCarburant"
        drawCarburant.Size = New Size(123, 239)
        drawCarburant.TabIndex = 7
        drawCarburant.TabStop = False
        ' 
        ' distance_parcourue
        ' 
        distance_parcourue.AutoSize = True
        distance_parcourue.Location = New Point(47, 485)
        distance_parcourue.Name = "distance_parcourue"
        distance_parcourue.Size = New Size(190, 21)
        distance_parcourue.TabIndex = 8
        distance_parcourue.Text = "Distance parcourue : 0 km"
        ' 
        ' Timer2
        ' 
        Timer2.Interval = 1000
        ' 
        ' consoMoyenne
        ' 
        consoMoyenne.AutoSize = True
        consoMoyenne.Location = New Point(268, 448)
        consoMoyenne.Name = "consoMoyenne"
        consoMoyenne.Size = New Size(248, 21)
        consoMoyenne.TabIndex = 9
        consoMoyenne.Text = "Consommation moyenne /100km:"
        ' 
        ' Button2
        ' 
        Button2.BackColor = Color.LimeGreen
        Button2.Location = New Point(503, 24)
        Button2.Name = "Button2"
        Button2.Size = New Size(114, 37)
        Button2.TabIndex = 10
        Button2.Text = "Pause"
        Button2.UseVisualStyleBackColor = False
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(9.0F, 21.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ButtonFace
        ClientSize = New Size(1157, 553)
        Controls.Add(Button2)
        Controls.Add(consoMoyenne)
        Controls.Add(distance_parcourue)
        Controls.Add(drawCarburant)
        Controls.Add(seconde)
        Controls.Add(dashboard)
        Controls.Add(Label2)
        Controls.Add(about)
        Controls.Add(Button1)
        Controls.Add(ComboBox1)
        Controls.Add(Label1)
        ForeColor = SystemColors.ActiveCaptionText
        Name = "Form1"
        Text = "Form1"
        CType(dashboard, ComponentModel.ISupportInitialize).EndInit()
        CType(drawCarburant, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Button1 As Button
    Friend WithEvents about As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents dashboard As PictureBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents seconde As Label
    Friend WithEvents drawCarburant As PictureBox
    Friend WithEvents distance_parcourue As Label
    Friend WithEvents Timer2 As Timer
    Friend WithEvents consoMoyenne As Label
    Friend WithEvents Button2 As Button

End Class
