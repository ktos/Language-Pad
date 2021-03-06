﻿Imports Tundra
Imports System.IO
Imports System.Linq
Namespace My

    ' The following events are available for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As ApplicationServices.StartupEventArgs) Handles Me.Startup
            On Error Resume Next
            'Dim stpw As New Stopwatch
            'stpw.Reset()
            'stpw.Start()

            ssLoading.pbLoading.SetProgress(0)
            ssLoading.lblLoading.Text = "Loading..."

            If My.Settings.AccentMarks = True Then
                ssLoading.lblLoading.Text = "Loading Accent Marks..."
                For Each IPA As KeyValuePair(Of String, String) In ZiaFile.Read(My.Resources.Accents)
                    Dim SymbolButton As New SymbolButton
                    SymbolButton.Text = IPA.Value
                    AddHandler SymbolButton.Click, AddressOf frmMain.InsertIPA
                    frmMain.AccentsLayoutPanel.Controls.Add(SymbolButton)

                    Dim SymbolButtonDictionary As New SymbolButton
                    SymbolButtonDictionary.Text = IPA.Value
                    AddHandler SymbolButtonDictionary.Click, AddressOf frmDictionary.InsertIPA
                    frmDictionary.AccentsLayoutPanel.Controls.Add(SymbolButtonDictionary)
                Next
                ssLoading.pbLoading.SetProgress(10)
                ssLoading.Refresh()
            Else
                frmMain.tcSymbols.TabPages.Remove(frmMain.AccentsTabPage)
                frmDictionary.tcSymbols.TabPages.Remove(frmMain.AccentsTabPage)
            End If

            If My.Settings.IPAAffricates = True Then
                ssLoading.lblLoading.Text = "Loading IPA Affricates..."
                For Each IPA As KeyValuePair(Of String, String) In ZiaFile.Read(My.Resources.Affricates)
                    Dim SymbolButton As New SymbolButton
                    SymbolButton.Text = IPA.Value
                    AddHandler SymbolButton.Click, AddressOf frmMain.InsertIPA
                    frmMain.AffricatesLayoutPanel.Controls.Add(SymbolButton)

                    Dim SymbolButtonDictionary As New SymbolButton
                    SymbolButtonDictionary.Text = IPA.Value
                    AddHandler SymbolButtonDictionary.Click, AddressOf frmDictionary.InsertIPA
                    frmDictionary.AffricatesLayoutPanel.Controls.Add(SymbolButtonDictionary)
                Next
                ssLoading.pbLoading.SetProgress(20)
                ssLoading.Refresh()
            Else
                frmMain.tcSymbols.TabPages.Remove(frmMain.AffricatesTabPage)
                frmDictionary.tcSymbols.TabPages.Remove(frmMain.AffricatesTabPage)
            End If

            If My.Settings.IPAConsonants = True Then
                ssLoading.lblLoading.Text = "Loading IPA Consonants..."
                For Each IPA As KeyValuePair(Of String, String) In ZiaFile.Read(My.Resources.Consonants)
                    Dim SymbolButton As New SymbolButton
                    SymbolButton.Text = IPA.Value
                    AddHandler SymbolButton.Click, AddressOf frmMain.InsertIPA
                    frmMain.ConsonantsLayoutPanel.Controls.Add(SymbolButton)

                    Dim SymbolButtonDictionary As New SymbolButton
                    SymbolButtonDictionary.Text = IPA.Value
                    AddHandler SymbolButtonDictionary.Click, AddressOf frmDictionary.InsertIPA
                    frmDictionary.ConsonantsLayoutPanel.Controls.Add(SymbolButtonDictionary)
                Next
                ssLoading.pbLoading.SetProgress(30)
                ssLoading.Refresh()
            Else
                frmMain.tcSymbols.TabPages.Remove(frmMain.ConsonantsTabPage)
                frmDictionary.tcSymbols.TabPages.Remove(frmMain.ConsonantsTabPage)
            End If

            If My.Settings.IPATones = True Then
                ssLoading.lblLoading.Text = "Loading IPA Tones..."
                For Each IPA As KeyValuePair(Of String, String) In ZiaFile.Read(My.Resources.ToneIntonation)
                    Dim SymbolButton As New SymbolButton
                    SymbolButton.Text = IPA.Value
                    AddHandler SymbolButton.Click, AddressOf frmMain.InsertIPA
                    frmMain.ToneIntonationLayoutPanel.Controls.Add(SymbolButton)

                    Dim SymbolButtonDictionary As New SymbolButton
                    SymbolButtonDictionary.Text = IPA.Value
                    AddHandler SymbolButtonDictionary.Click, AddressOf frmDictionary.InsertIPA
                    frmDictionary.ToneIntonationLayoutPanel.Controls.Add(SymbolButtonDictionary)
                Next
                ssLoading.pbLoading.SetProgress(40)
                ssLoading.Refresh()
            Else
                frmMain.tcSymbols.TabPages.Remove(frmMain.ToneIntonationTabPage)
                frmDictionary.tcSymbols.TabPages.Remove(frmMain.ToneIntonationTabPage)
            End If

            If My.Settings.IPAVowels = True Then
                ssLoading.lblLoading.Text = "Loading IPA Vowels..."
                For Each IPA As KeyValuePair(Of String, String) In ZiaFile.Read(My.Resources.Vowels)
                    Dim SymbolButton As New SymbolButton
                    SymbolButton.Text = IPA.Value
                    AddHandler SymbolButton.Click, AddressOf frmMain.InsertIPA
                    frmMain.VowelsLayoutPanel.Controls.Add(SymbolButton)

                    Dim SymbolButtonDictionary As New SymbolButton
                    SymbolButtonDictionary.Text = IPA.Value
                    AddHandler SymbolButtonDictionary.Click, AddressOf frmDictionary.InsertIPA
                    frmDictionary.VowelsLayoutPanel.Controls.Add(SymbolButtonDictionary)
                Next
                ssLoading.pbLoading.SetProgress(50)
                ssLoading.Refresh()
            Else
                frmMain.tcSymbols.TabPages.Remove(frmMain.VowelsTabPage)
                frmDictionary.tcSymbols.TabPages.Remove(frmMain.VowelsTabPage)
            End If

            If My.Settings.OtherIPA = True Then
                ssLoading.lblLoading.Text = "Loading Other IPA Characters..."
                For Each IPA As KeyValuePair(Of String, String) In ZiaFile.Read(My.Resources.Other)
                    Dim SymbolButton As New SymbolButton
                    SymbolButton.Text = IPA.Value
                    AddHandler SymbolButton.Click, AddressOf frmMain.InsertIPA
                    frmMain.OtherLayoutPanel.Controls.Add(SymbolButton)

                    Dim SymbolButtonDictionary As New SymbolButton
                    SymbolButtonDictionary.Text = IPA.Value
                    AddHandler SymbolButtonDictionary.Click, AddressOf frmDictionary.InsertIPA
                    frmDictionary.OtherLayoutPanel.Controls.Add(SymbolButtonDictionary)
                Next
                ssLoading.pbLoading.SetProgress(60)
                ssLoading.Refresh()
            Else
                frmMain.tcSymbols.TabPages.Remove(frmMain.OtherTabPage)
                frmDictionary.tcSymbols.TabPages.Remove(frmMain.OtherTabPage)
            End If

            If My.Settings.CommonChar = True Then
                ssLoading.lblLoading.Text = "Loading Common Characters..."
                For Each IPA As String In My.Resources.Common.Split({"|"}, StringSplitOptions.RemoveEmptyEntries)
                    Dim SymbolButton As New SymbolButton
                    SymbolButton.Text = IPA
                    AddHandler SymbolButton.Click, AddressOf frmMain.InsertIPA
                    frmMain.CommonLayoutPanel.Controls.Add(SymbolButton)

                    Dim SymbolButtonDictionary As New SymbolButton
                    SymbolButtonDictionary.Text = IPA
                    AddHandler SymbolButtonDictionary.Click, AddressOf frmDictionary.InsertIPA
                    frmDictionary.CommonLayoutPanel.Controls.Add(SymbolButtonDictionary)
                Next
                ssLoading.pbLoading.SetProgress(70)
                ssLoading.Refresh()
            Else
                frmMain.tcSymbols.TabPages.Remove(frmMain.CommonTabPage)
                frmDictionary.tcSymbols.TabPages.Remove(frmMain.CommonTabPage)
            End If

            ssLoading.lblLoading.Text = "Loading Custom Characters..."
            Dim LineList As String() = My.Settings.CustomSymbols.Split({Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries)
            For Each IPA As String In LineList
                Dim SymbolButton As New SymbolButton
                SymbolButton.Text = IPA
                AddHandler SymbolButton.Click, AddressOf frmMain.InsertIPA
                frmMain.CustomLayoutPanel.Controls.Add(SymbolButton)

                Dim SymbolButtonDictionary As New SymbolButton
                SymbolButtonDictionary.Text = IPA
                AddHandler SymbolButtonDictionary.Click, AddressOf frmDictionary.InsertIPA
                frmDictionary.CustomLayoutPanel.Controls.Add(SymbolButtonDictionary)
            Next
            ssLoading.pbLoading.SetProgress(80)
            ssLoading.Refresh()

            ssLoading.pbLoading.SetProgress(90)
            ssLoading.Refresh()

            If My.Settings.Updates = True Then
                ssLoading.lblLoading.Text = "Checking for updates..."
                dlgUpdate.FetchUpdateData()
            End If

            ssLoading.pbLoading.SetProgress(100)
            ssLoading.Refresh()
            'stpw.Stop()
            'MessageBox.Show(stpw.Elapsed.TotalSeconds)

        End Sub
    End Class
End Namespace

