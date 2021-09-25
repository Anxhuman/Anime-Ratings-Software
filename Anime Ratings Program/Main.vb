Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Public Class Main
    Dim con As OleDbConnection
    Dim dAdpt As OleDbDataAdapter
    Dim ds As DataSet
    Dim i As Integer = 0
    Dim dBind As New BindingSource
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "ID" Or ComboBox1.SelectedItem = "Name" Then
            Label6.Visible = False
        End If
    End Sub

    Private Sub Label7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label7.Click
        Label7.Visible = False
        TextBox4.Focus()
    End Sub

    Private Sub TextBox1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEmpID.Leave
        If TextBox4.Text = "" Then
            Label7.Visible = True
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        BtnAdd.Enabled = False
        BtnImageBrowser.Enabled = False
        txtEmpID.Enabled = False
        Call Settings()
        Call FillEvents()
        My.Computer.Audio.Play(My.Resources.rcr, AudioPlayMode.Background)
    End Sub

    Private Sub BtnImageBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImageBrowser.Click
        Try
            Dim fopen As New OpenFileDialog
            fopen.FileName = ""
            fopen.Filter = "Image Files (*.jpg)|*.jpg|(*.jpeg)|*.JPEG|(*.gif)|*.gif|(*.png)|*.png|All Files (*.*)|*.*"
            fopen.ShowDialog()
            PictureBox1.Image = System.Drawing.Bitmap.FromFile(fopen.FileName)
            Label8.Text = fopen.FileName
        Catch ex As Exception

        End Try
    End Sub

    Private Sub BtnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNew.Click
        BtnAdd.Enabled = True
        BtnImageBrowser.Enabled = True
        PictureBox1.Image = Nothing
        txtEmpID.Text = ""
        txtEmpName.Text = ""
        txtEmpAddress.Text = ""
        Label8.Text = ""
        Dim cmd As New OleDbCommand("select max(emp_id) from emp_table", con)
        Dim n As Integer
        n = cmd.ExecuteScalar
        txtEmpID.Text = n + 1
    End Sub

    Private Sub BtnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnAdd.Click
        If BtnAdd.Text = "Add" Then
            Call AddRecord()
            BtnAdd.Text = "Update"
        ElseIf BtnAdd.Text = "Update" Then
            Call UpdateRecord()
            BtnAdd.Text = "Delete"
        ElseIf BtnAdd.Text = "Delete" Then
            Call DeleteRecord()
            BtnAdd.Text = "Add"
            BtnAdd.Enabled = False
            MsgBox("Click On New Button to Reenable it")
        End If
    End Sub
    Private Sub Settings()
        Try
            con = New OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & System.Environment.CurrentDirectory.ToString() & "\Emp.mdb")
            con.Open()
            dAdpt = New OleDbDataAdapter("select * from emp_table", con)
            ds = New DataSet
            dAdpt.Fill(ds)
            dBind.DataSource = ds
            dBind.DataMember = ds.Tables(0).ToString()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub FillEvents()
        Try
            txtEmpID.Text = ds.Tables(0).Rows(i).Item(0)
            txtEmpName.Text = ds.Tables(0).Rows(i).Item(1)
            txtEmpAddress.Text = ds.Tables(0).Rows(i).Item(2)
            Label8.Text = ds.Tables(0).Rows(i).Item(3)
            PictureBox1.Image = System.Drawing.Bitmap.FromFile(Label8.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub AddRecord()
        Try
            If Label8.Text = "" Then
                Label8.Text = System.Environment.CurrentDirectory.ToString & "\image_not_available120x120.gif"
            End If
            Dim com As New OleDbCommand
            com.Connection = con
            com.CommandText = "insert into emp_table values(" & Val(txtEmpID.Text) & ",'" & txtEmpName.Text & "','" & _
                                txtEmpAddress.Text & "','" & Label8.Text & "')"

            com.ExecuteNonQuery()
            MsgBox("Record Inserted...PleaseRestart to see the changes")
        Catch exp As Exception
            MsgBox(exp.ToString())
        End Try
    End Sub
    Private Sub DeleteRecord()
        Try
            Dim com As New OleDbCommand
            com.Connection = con
            com.CommandText = "delete from emp_table where emp_id=" & txtEmpID.Text
            com.ExecuteNonQuery()
            MsgBox("Record Deleted")
        Catch exp As Exception
            MsgBox(exp.ToString())
        End Try
    End Sub
    Private Sub UpdateRecord()
        Try
            If Label8.Text = "" Then
                Label8.Text = System.Environment.CurrentDirectory.ToString & "\image_not_available120x120.gif"
            End If
            Dim com As New OleDbCommand
            com.Connection = con
            com.CommandText = "update emp_table set emp_name='" & _
                                txtEmpName.Text & "',emp_Address='" & txtEmpAddress.Text & _
                                "',emp_image_path='" & Label8.Text & "' where emp_id=" & txtEmpID.Text
            com.ExecuteNonQuery()
            MsgBox("Record Updated")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFirst.Click
        Try
            i = 0
            Call FillEvents()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub BtnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBack.Click
        Try
            If (Not i = 0) Then
                i -= 1
                Call FillEvents()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub BtnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnNext.Click
        Try
            If (Not i = ds.Tables(0).Rows.Count - 1) Then
                i += 1
                Call FillEvents()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub NtnLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NtnLast.Click
        Try
            If (Not i = ds.Tables(0).Rows.Count - 1) Then
                i = ds.Tables(0).Rows.Count - 1
                Call FillEvents()
            End If
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        If ComboBox1.SelectedItem = "ID" Then
            Try
                ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("emp_id")}
                Dim row As DataRow
                row = ds.Tables(0).Rows.Find(TextBox4.Text)
                txtEmpID.Text = row("emp_id")
                txtEmpName.Text = row("emp_name")
                txtEmpAddress.Text = row("emp_address")
                Label8.Text = row("emp_image_path")
                PictureBox1.Image = System.Drawing.Bitmap.FromFile(Label8.Text)
                BtnAdd.Enabled = True
                BtnAdd.Text = "Update"
            Catch ex As Exception

            End Try
        End If
        If ComboBox1.SelectedItem = "Name" Then
            Try
                ds.Tables(0).PrimaryKey = New DataColumn() {ds.Tables(0).Columns("emp_name")}
                Dim row As DataRow
                row = ds.Tables(0).Rows.Find(TextBox4.Text)
                txtEmpID.Text = row("emp_id")
                txtEmpName.Text = row("emp_name")
                txtEmpAddress.Text = row("emp_address")
                Label8.Text = row("emp_image_path")
                PictureBox1.Image = System.Drawing.Bitmap.FromFile(Label8.Text)
                BtnAdd.Enabled = True
                BtnAdd.Text = "Update"
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub txtEmpName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpName.TextChanged

    End Sub

    Private Sub txtEmpAddress_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtEmpAddress.TextChanged

    End Sub


    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        About.Show()
    End Sub

    Private Sub LicenseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LicenseToolStripMenuItem.Click
        License.Show()
    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click
        Dim SAPI
        SAPI = CreateObject("SAPI.spvoice")
        SAPI.speak(txtEmpAddress.Text)

        If txtEmpAddress.Text = "" Then
            MsgBox("Please type text to Speak!", MsgBoxStyle.Information, "Input text")
            txtEmpAddress.Focus()

        End If
    End Sub
    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        Dim SAPI
        SAPI = CreateObject("SAPI.spvoice")
        SAPI.speak(txtEmpName.Text)

        If txtEmpName.Text = "" Then
            MsgBox("Please type text to Speak!", MsgBoxStyle.Information, "Input text")
            txtEmpName.Focus()

        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub
End Class
