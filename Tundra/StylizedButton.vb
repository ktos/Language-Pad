﻿Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Drawing
Imports Tundra.ZiaFile
Imports System.Drawing.Drawing2D

Public Class StylizedButton
    Inherits Button
    Private mStyle As String
    Protected Event UIStyleChanged(ByVal sender As Object, ByVal e As EventArgs)

    <Editor(GetType(MultilineStringEditor), GetType(UITypeEditor))> _
    Public Property Style As String
        Get
            Return mStyle
        End Get
        Set(ByVal value As String)
            mStyle = value
            On Error Resume Next
            GenerateBitmaps()
            Me.BackgroundImage = ActiveBitmap
            Me.ForeColor = ActiveTextColor
            RaiseEvent UIStyleChanged(Me, Nothing)
        End Set
    End Property

    Protected Radius As Integer

    Private ActiveColors As Color()
    Private ActivePositions As Single()
    Private ActiveAngle As Integer
    Private ActiveBorderColor As Color
    Private ActiveTextColor As Color
    Private ActiveHighlightColor As Color

    Private HoverColors As Color()
    Private HoverPositions As Single()
    Private HoverAngle As Integer
    Private HoverBorderColor As Color
    Private HoverTextColor As Color
    Private HoverHighlightColor As Color

    Private PressedColors As Color()
    Private PressedPositions As Single()
    Private PressedAngle As Integer
    Private PressedBorderColor As Color
    Private PressedTextColor As Color
    Private PressedHighlightColor As Color

    Private ActiveBitmap As Bitmap
    Private HoverBitmap As Bitmap
    Private PressedBitmap As Bitmap
    Private NoBottom As Boolean = False

    Private Sub ReadStyle()
        On Error Resume Next
        Radius = CInt(GetValue(Style, "Radius"))

        ActiveColors = FromCompatibleColorList(GetValue(Style, "Active Colors"))
        ActivePositions = FromCompatibleSingleList(GetValue(Style, "Active Positions"))
        ActiveAngle = CInt(GetValue(Style, "Active Angle"))
        ActiveBorderColor = FromCompatibleColor(GetValue(Style, "Active Border Color"))
        ActiveTextColor = FromCompatibleColor(GetValue(Style, "Active Text Color"))
        ActiveHighlightColor = FromCompatibleColor(GetValue(Style, "Active Highlight Color"))

        HoverColors = FromCompatibleColorList(GetValue(Style, "Hover Colors"))
        HoverPositions = FromCompatibleSingleList(GetValue(Style, "Hover Positions"))
        HoverAngle = CInt(GetValue(Style, "Hover Angle"))
        HoverBorderColor = FromCompatibleColor(GetValue(Style, "Hover Border Color"))
        HoverTextColor = FromCompatibleColor(GetValue(Style, "Hover Text Color"))
        HoverHighlightColor = FromCompatibleColor(GetValue(Style, "Hover Highlight Color"))

        PressedColors = FromCompatibleColorList(GetValue(Style, "Pressed Colors"))
        PressedPositions = FromCompatibleSingleList(GetValue(Style, "Pressed Positions"))
        PressedAngle = CInt(GetValue(Style, "Pressed Angle"))
        PressedBorderColor = FromCompatibleColor(GetValue(Style, "Pressed Border Color"))
        PressedTextColor = FromCompatibleColor(GetValue(Style, "Pressed Text Color"))
        PressedHighlightColor = FromCompatibleColor(GetValue(Style, "Pressed Highlight Color"))

        NoBottom = GetValue(Style, "No Bottom")
    End Sub

    Protected Sub GenerateBitmaps()
        On Error Resume Next
        ReadStyle()
        ActiveBitmap = New Bitmap(Me.Width, Me.Height)
        HoverBitmap = New Bitmap(Me.Width, Me.Height)
        PressedBitmap = New Bitmap(Me.Width, Me.Height)

        Dim FillRectangle As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Dim HighlightRectangle As New Rectangle(1, 1, Me.Width - 3, Me.Height - 3)

        If NoBottom = True Then
            FillRectangle = New Rectangle(0, 0, Me.Width - 1, Me.Height + 2)
            HighlightRectangle = New Rectangle(1, 1, Me.Width - 3, Me.Height + 2)
        End If

        Dim FillRoundedRectangle As GraphicsPath = RoundedRectangle(FillRectangle, Radius)
        Dim HighlightRoundedRectangle As GraphicsPath = RoundedRectangle(HighlightRectangle, Radius)

        Dim ActiveGraphics As Graphics = Graphics.FromImage(ActiveBitmap)
        ActiveGraphics.CompositingQuality = CompositingQuality.HighQuality
        ActiveGraphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim ActiveBlend As New ColorBlend()
        Dim ActiveFillBlend As New LinearGradientBrush(FillRectangle, Color.Transparent, Color.Transparent, ActiveAngle)
        ActiveBlend.Colors = ActiveColors
        ActiveBlend.Positions = ActivePositions
        ActiveFillBlend.InterpolationColors = ActiveBlend
        ActiveGraphics.FillPath(ActiveFillBlend, FillRoundedRectangle)
        ActiveGraphics.DrawPath(New Pen(New SolidBrush(ActiveBorderColor)), FillRoundedRectangle)
        ActiveGraphics.DrawPath(New Pen(New SolidBrush(ActiveHighlightColor)), HighlightRoundedRectangle)

        Dim HoverGraphics As Graphics = Graphics.FromImage(HoverBitmap)
        HoverGraphics.CompositingQuality = CompositingQuality.HighQuality
        HoverGraphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim HoverBlend As New ColorBlend()
        Dim HoverFillBlend As New LinearGradientBrush(FillRectangle, Color.Transparent, Color.Transparent, HoverAngle)
        HoverBlend.Colors = HoverColors
        HoverBlend.Positions = HoverPositions
        HoverFillBlend.InterpolationColors = HoverBlend
        HoverGraphics.FillPath(HoverFillBlend, FillRoundedRectangle)
        HoverGraphics.DrawPath(New Pen(New SolidBrush(HoverBorderColor)), FillRoundedRectangle)
        HoverGraphics.DrawPath(New Pen(New SolidBrush(HoverHighlightColor)), HighlightRoundedRectangle)

        Dim PressedGraphics As Graphics = Graphics.FromImage(PressedBitmap)
        PressedGraphics.CompositingQuality = CompositingQuality.HighQuality
        PressedGraphics.SmoothingMode = SmoothingMode.AntiAlias
        Dim PressedBlend As New ColorBlend()
        Dim PressedFillBlend As New LinearGradientBrush(FillRectangle, Color.Transparent, Color.Transparent, PressedAngle)
        PressedBlend.Colors = PressedColors
        PressedBlend.Positions = PressedPositions
        PressedFillBlend.InterpolationColors = PressedBlend
        PressedGraphics.FillPath(PressedFillBlend, FillRoundedRectangle)
        PressedGraphics.DrawPath(New Pen(New SolidBrush(PressedBorderColor)), FillRoundedRectangle)
        PressedGraphics.DrawPath(New Pen(New SolidBrush(PressedHighlightColor)), HighlightRoundedRectangle)
    End Sub

    Public Sub New()
        Me.DoubleBuffered = True
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        SetStyle(ControlStyles.ResizeRedraw, True)
        Me.BackColor = Color.Transparent
        Me.FlatStyle = Windows.Forms.FlatStyle.Flat
        Me.FlatAppearance.BorderSize = 0
        Me.FlatAppearance.MouseDownBackColor = Color.Transparent
        Me.FlatAppearance.MouseOverBackColor = Color.Transparent
        Me.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0)
        Me.Padding = New Padding(0, 1, 0, 0)
        Me.UseCompatibleTextRendering = True
    End Sub

    Private Sub StylizedButton_LostFocus(sender As Object, e As System.EventArgs) Handles Me.LostFocus
        Me.Refresh()
    End Sub

    Private Sub StylizedButton_MouseDown(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDown
        On Error Resume Next
        If MouseButtons = Windows.Forms.MouseButtons.Left Then
            Me.BackgroundImage = PressedBitmap
            Me.ForeColor = PressedTextColor
        End If
    End Sub

    Private Sub StylizedButton_MouseHover(sender As Object, e As System.EventArgs) Handles Me.MouseHover
        On Error Resume Next
        If Not MouseButtons = Windows.Forms.MouseButtons.Left Then
            Me.BackgroundImage = HoverBitmap
            Me.ForeColor = HoverTextColor
        End If
    End Sub

    Private Sub StylizedButton_MouseLeave(sender As Object, e As System.EventArgs) Handles Me.MouseLeave
        On Error Resume Next
        Me.BackgroundImage = ActiveBitmap
        Me.ForeColor = ActiveTextColor
    End Sub

    Private Sub StylizedButton_MouseMove(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        On Error Resume Next
        If Not MouseButtons = Windows.Forms.MouseButtons.Left Then
            Me.BackgroundImage = HoverBitmap
            Me.ForeColor = HoverTextColor
        End If
    End Sub

    Private Sub StylizedButton_Resize(sender As Object, e As System.EventArgs) Handles Me.Resize
        On Error Resume Next
        GenerateBitmaps()
        Me.BackgroundImage = ActiveBitmap
        Me.ForeColor = ActiveTextColor
    End Sub
End Class