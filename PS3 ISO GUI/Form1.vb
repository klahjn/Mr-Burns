Imports System.ComponentModel
Imports System.IO
Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.DeleteISO = "YES" Then CheckBox1.Checked = True
        If My.Settings.Newpath.ToString <> "" Then ComboBox1.Text = My.Settings.Newpath.ToString
        NumericUpDown1.Value = My.Settings.Copies
        For Each drive In DriveInfo.GetDrives()
            If drive.DriveType = DriveType.CDRom Then ComboBox1.Items.Add(drive.ToString())
        Next
        If File.Exists("C:\Program Files (x86)\ImgBurn\ImgBurn.exe") = True Then My.Settings.ImgburnLoc = "C:\Program Files (x86)\ImgBurn\ImgBurn.exe" : My.Settings.Save()
        If File.Exists("C:\Program Files\ImgBurn\ImgBurn.exe") = True Then My.Settings.ImgburnLoc = "C:\Program Files\ImgBurn\ImgBurn.exe" : My.Settings.Save()
    End Sub
    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then My.Settings.DeleteISO = "YES" : CheckBox1.Checked = True : Exit Sub
        If CheckBox1.Checked = False Then My.Settings.DeleteISO = "NO" : CheckBox1.Checked = False : Exit Sub
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        FolderBrowserDialog1.ShowDialog()
        If Directory.Exists(FolderBrowserDialog1.SelectedPath.ToString + "\PS3_GAME") = False Then MsgBox("Not a valid game! No PS3_GAME Folder detected") : Exit Sub
        If FolderBrowserDialog1.SelectedPath.ToString <> "" Then My.Settings.Oldpath = FolderBrowserDialog1.SelectedPath.ToString
        My.Settings.Save()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        My.Settings.Newpath = ComboBox1.SelectedItem.ToString
        My.Settings.Save()
    End Sub
    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        My.Settings.Save()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If IO.Directory.Exists("C:\ISOs") = False Then Directory.CreateDirectory("C:\ISOs")
        If IO.File.Exists("C:\ISOs\temp.iso") Then File.Delete("C:\ISOs\temp.iso")
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = Application.StartupPath + "\tools\mk.exe"
        startInfo.Arguments = " " + Chr(34) + My.Settings.Oldpath.ToString + Chr(34) + " " + Chr(34) + "C:\ISOs\temp.iso" + Chr(34)
        Process.Start(startInfo)
    End Sub

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        My.Settings.Copies = NumericUpDown1.Value.ToString
        My.Settings.Save()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = My.Settings.ImgburnLoc.ToString
        startInfo.Arguments = " /MODE WRITE /SRC C:\ISOs\temp.iso /DEST " + My.Settings.Newpath.ToString + " /START /COPIES " + My.Settings.Copies.ToString + " /DELETEIMAGE " + My.Settings.DeleteISO.ToString
        Process.Start(startInfo)
    End Sub
End Class
