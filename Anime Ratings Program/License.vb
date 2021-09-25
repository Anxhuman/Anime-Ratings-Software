Public Class License

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub License_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim yo As New rtaGlassEffectsLib.rtaGlassEffect

        yo.TopBarSize = 120
        yo.ShowEffect(Me)
        yo.BottomBarSize = 0
        yo.LeftBarSize = 0
        yo.UseHandCursorOnTitle = False
        yo.ShowEffect(Me, PictureBox1)
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
End Class