﻿Imports System.ComponentModel
Imports Tundra

Public Class frmDictionary
    Dim c As TextBox
    Dim SelectedCell As DataGridViewCell

    Public Sub InsertText(ByVal Textbox As TextBox, ByVal Text As String)
        On Error Resume Next
        Dim CurrentPos As Integer = Textbox.SelectionStart
        Dim obj As Object = Clipboard.GetDataObject
        Clipboard.SetText(Text)
        Textbox.Paste()
        Clipboard.SetDataObject(obj)

        Textbox.Focus()
        Textbox.SelectionStart = CurrentPos + Text.Length
        Textbox.SelectionLength = 0
    End Sub



    Public Sub InsertIPA(sender As Object, e As EventArgs)
        On Error Resume Next
        Dim Button As Button = CType(sender, Button)
        dgvDictionary.Focus()
        dgvDictionary.BeginEdit(False)

        InsertText(c, Button.Text)
    End Sub

    Private Sub frmDictionary_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If My.Settings.ForceSansSerif = True Then
            Me.Font = New Font("Microsoft Sans Serif", "8.25")
        End If

        pnlTop.Height = pnlHome.Height


    End Sub

    Private Sub dgvDictionary_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDictionary.CellContentClick

    End Sub

    Private Sub dgvDictionary_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles dgvDictionary.EditingControlShowing
        c = e.Control
    End Sub

    Private Sub dgvDictionary_EditModeChanged(sender As Object, e As EventArgs) Handles dgvDictionary.EditModeChanged

    End Sub

    Private Sub dgvDictionary_GiveFeedback(sender As Object, e As GiveFeedbackEventArgs) Handles dgvDictionary.GiveFeedback

    End Sub

    Private Sub dgvDictionary_LostFocus(sender As Object, e As EventArgs) Handles dgvDictionary.LostFocus

    End Sub

    Private Sub dgvDictionary_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDictionary.CellEndEdit

    End Sub

    Private Sub frmDictionary_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub btnSymbols_Click(sender As Object, e As EventArgs) Handles btnSymbols.Click
        SplitContainer1.Panel1Collapsed = SplitContainer1.Panel1Collapsed Xor True
    End Sub

    Private Sub btnCustomSymbols_Click(sender As Object, e As EventArgs) Handles btnCustomSymbols.Click
        dlgCustomSymbols.ShowDialog()
    End Sub

    Private Sub btnAccentMark_Click(sender As Object, e As EventArgs) Handles btnAccentMark.Click
        On Error Resume Next
        dgvDictionary.Focus()
        dgvDictionary.BeginEdit(False)

        If c.SelectionLength > 0 Then
            dlgAccentMark.Character = c.SelectedText
        Else
            dlgAccentMark.Character = ""
        End If

        If dlgAccentMark.ShowDialog = DialogResult.OK Then
            InsertText(c, dlgAccentMark.Character)
            dlgAccentMark.Character = ""
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        dgvDictionary.Rows.Add(1)
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        dgvDictionary.Rows.RemoveAt(dgvDictionary.CurrentCell.RowIndex)
    End Sub

    Private Sub dgvDictionary_RowPostPaint(sender As Object, e As DataGridViewRowPostPaintEventArgs) Handles dgvDictionary.RowPostPaint
        Dim grid As DataGridView = CType(sender, DataGridView)
        Dim rowIdx As String = (e.RowIndex + 1).ToString()
        Dim rowFont As System.Drawing.Font = Me.Font

        Dim centerFormat = New StringFormat()
        centerFormat.Alignment = StringAlignment.Center
        centerFormat.LineAlignment = StringAlignment.Center

        Dim headerBounds As Rectangle = New Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height)
        e.Graphics.DrawString(rowIdx, rowFont, SystemBrushes.ControlText, headerBounds, centerFormat)
    End Sub

    Private Sub btnCheckExisting_Click(sender As Object, e As EventArgs) Handles btnCheckExisting.Click
        dlgCheckExisting.ShowDialog()
    End Sub
End Class