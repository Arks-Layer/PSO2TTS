'------------------------------------------------------------------------------
' PSO2 TTS - A PSO2 Text To Speech system
' (As seen here: https://www.youtube.com/watch?v=53yo5cLkezE)
'
' Thanks for taking a look at this code.
' Feel free to submit bugfixes/improvements to 
' https://github.com/Arks-Layer/PSO2TTS
' 
' Take care, and have fun in everything you do. - AIDA
' Program uses the MIT license
'
'------------------------------------------------------------------------------
Imports System.IO
Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class Form1
    Dim ChatTextOld As String = "qwegowieghiwg"
    Dim Voice As New SpeechLib.SpVoice
    Dim VoiceNumber As Integer = 0

    Public Sub ReadLog()
        'Call this from a timer maybe?
        Dim dir As String
        dir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim fi As New System.IO.DirectoryInfo(dir & "\SEGA\PHANTASYSTARONLINE2\log\")
        Dim files = fi.GetFiles("Chat*").ToList
        Dim last = (From file In files Select file Order By file.LastWriteTime Descending).ToArray.FirstOrDefault
        Dim fs As New FileStream(last.FullName.ToString, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)

        Dim sr As New StreamReader(fs)
        Dim ChatText As String = ""
        Do Until sr.EndOfStream
            ' replace "previous" last line with "current" read line ,
            ' hence at end of loop lastLine will contain the "real" last line.
            ChatText = sr.ReadLine
        Loop
        sr.Close()
        fs.Close()
        If ChatText = ChatTextOld Then Exit Sub
        ChatTextOld = ChatText
        'Dim ChatType As String = "UNKNOWN"
        'If ChatText.Contains("	PUBLIC	") Then ChatType = "PUBLIC"
        'If ChatText.Contains("	GUILD	") Then ChatType = "GUILD"
        'If ChatText.Contains("	PARTY	") Then ChatType = "PARTY"
        'If ChatText.Contains("	REPLY	") Then ChatType = "REPLY"
        'Dim LogColor As Color
        Dim ChatText2() As String = ChatText.Split("	")
        ChatText2(5) = ChatText2(5).Replace("[G-", "<G-").Replace("] ", "> ")

        'Dim ChatType2 As String = "UNKNOWN"
        'If ChatType = "PARTY" Then ChatType2 = "PARTY"
        'If ChatType = "PUBLIC" Then ChatType2 = "PUBLIC"
        'If ChatType = "GUILD" Then ChatType2 = "TEAM"
        'If ChatType = "REPLY" Then ChatType2 = "WHISPER"
        If ChatText2(5).Contains("/la") Then
            Dim ChatText3() As String = ChatText2(5).Split(" ")
            If ChatText3.Count < 4 Then Exit Sub
            'If ChatText3.Count > 2 Then
            ' ChatText = "<" & ChatType2 & "> " & ChatText2(4) & ": " & ChatText3(2)
            ' GoTo TYPEOFCHAT
            'End If
        End If
        If ChatText2(5).Contains("/cla") Then
            Dim ChatText3() As String = ChatText2(5).Split(" ")
            If ChatText3.Count < 4 Then Exit Sub
            'If ChatText3.Count > 2 Then
            ' ChatText = "<" & ChatType2 & "> " & ChatText2(4) & ": " & ChatText3(2)
            ' GoTo TYPEOFCHAT
            'End If
        End If
        If ChatText2(5).Contains("/mla") Then
            Dim ChatText3() As String = ChatText2(5).Split(" ")
            If ChatText3.Count < 4 Then Exit Sub
            'If ChatText3.Count > 2 Then
            'ChatText = "<" & ChatType2 & "> " & ChatText2(4) & ": " & ChatText3(2)
            'GoTo TYPEOFCHAT
            'End If
        End If
        If ChatText2(5) = "" Then Exit Sub
        If ChatText2(5) = " " Then Exit Sub
        Dim ChatSender As String = ChatText2(4)
        Dim ChatMessage As String = ChatText2(5)
        If ChatSender.Contains("[GIRC] AIDA") = True Or ChatSender = "AIDA" Or ChatSender = "TheCrimsonDevil" Then
            Voice.Voice = Voice.GetVoices().Item(5)
        Else
            Voice.Voice = Voice.GetVoices().Item(0)
        End If
        If Regex.IsMatch(ChatMessage.Replace(ChatSender, ""), "\p{IsHiragana}") Then Voice.Voice = Voice.GetVoices().Item(6)
        If Regex.IsMatch(ChatMessage.Replace(ChatSender, ""), "\p{IsKatakana}") Then Voice.Voice = Voice.GetVoices().Item(6)
        Voice.Speak(ChatMessage.ToLower.Replace(" aida", " ayduh").Replace("aida ", "ayduh ").Replace("*", ""), SpeechLib.SpeechVoiceSpeakFlags.SVSFlagsAsync)

        'Comment out this code cause it was a bit buggy [AIDA]

        'If ChatText2.Count > 10 Then
        'Dim chatlogdate As String = DateTime.Now.ToString("yyyy-MM-dd")
        'chatlogdate = chatlogdate & "T"
        'Dim ChatText4() As String = ChatText2(5).Split(chatlogdate)
        'ChatText = "<" & ChatType2 & "> " & ChatText2(4) & ": " & Microsoft.VisualBasic.Left(ChatText4(0), ChatText4(0).Length - 1)
        'If ChatType = "PARTY" Then LogColor = PARTY_COLOR
        'If ChatType = "PUBLIC" Then LogColor = PUBLIC_COLOR
        'If ChatType = "GUILD" Then LogColor = TEAM_COLOR
        'If ChatType = "REPLY" Then LogColor = WHISPER_COLOR
        'Me.Refresh()
        'ChatText = ChatText.Replace(vbLf, vbCrLf)
        'ChatType = ChatText2(7)
        'MsgBox("Chat text")
        'If ChatType = "PARTY" Then ChatType2 = "PARTY"
        'If ChatType = "PUBLIC" Then ChatType2 = "PUBLIC"
        'If ChatType = "GUILD" Then ChatType2 = "TEAM"
        'If ChatType = "REPLY" Then ChatType2 = "WHISPER"
        'ChatText = "<" & ChatType2 & "> " & ChatText2(9) & ": " & ChatText2(10)
        'If ChatType = "PARTY" Then LogColor = PARTY_COLOR
        'If ChatType = "PUBLIC" Then LogColor = PUBLIC_COLOR
        'If ChatType = "GUILD" Then LogColor = TEAM_COLOR
        'If ChatType = "REPLY" Then LogColor = WHISPER_COLOR
        'Me.Refresh()
        'rtbLog.SelectionStart = rtbLog.Text.Length
        'rtbLog.SelectionColor = LogColor
        'rtbLog.SelectionFont = New Font("Microsoft Sans Serif", 9.75, FontStyle.Bold)
        'ChatText = ChatText.Replace(vbLf, vbCrLf)
        'rtbLog.AppendText(vbCrLf & ChatText)
        'Notify(ChatText)
        'Exit Sub
        'Split by the date, and post the first chat, then the second (oh boy), then exit the sub [AIDA]
        'ChatText2(5).Split(
        'End If
        'ChatText = "<" & ChatType2 & "> " & ChatText2(4) & ": " & ChatText2(5)
        'TYPEOFCHAT:
        'If ChatType = "PARTY" Then LogColor = PARTY_COLOR
        'If ChatType = "PUBLIC" Then LogColor = PUBLIC_COLOR
        'If ChatType = "GUILD" Then LogColor = TEAM_COLOR
        'If ChatType = "REPLY" Then LogColor = WHISPER_COLOR
        'Me.Refresh()
        'rtbLog.SelectionStart = rtbLog.Text.Length
        'rtbLog.SelectionColor = LogColor
        'rtbLog.SelectionFont = New Font("Microsoft Sans Serif", 9.75, FontStyle.Bold)
        'ChatText = ChatText.Replace(vbLf, vbCrLf)
        'Notify(ChatText)
        'rtbLog.AppendText(vbCrLf & ChatText)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim t1 As New Threading.Thread(AddressOf ReadLog)

        t1.Start()
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'For Each T In Voice.GetVoices
        'MsgBox(T.GetDescription & " is voice number " & VoiceNumber) 'The token's name
        'VoiceNumber += 1
        'Next
        Voice.Voice = Voice.GetVoices().Item(0)
        'MessageBox.Show(Voice.GetVoices.Count)
        'Voice.Voice = Voice.GetVoices().Item(0)
    End Sub
End Class
