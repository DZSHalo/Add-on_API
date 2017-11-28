Imports System.Runtime.InteropServices

Public Class HEXT
    Public Shared colon As Char = ":"
    Public Shared newline As Char = "\n"
    Public Shared pipe As Char = "|"
    Public Shared comma As Char = ","
    Public Shared [me] As String = "me"
    Public Shared backslash As Char = "\\"
    Public Shared dot As Char = "."

    Public Enum GAME_MODE As UShort
        [SINGLE] = 0
        MULTI
        HOSTING
    End Enum
    <StructLayout(LayoutKind.Sequential)>
    Public Structure GAME_MODE_S
        <MarshalAs(UnmanagedType.I1)>
        Public [SINGLE] As Boolean
        <MarshalAs(UnmanagedType.I1)>
        Public MULTI As Boolean
        <MarshalAs(UnmanagedType.I1)>
        Public HOSTING As Boolean
        <MarshalAs(UnmanagedType.I1)>
        Public RESERVE0 As Boolean
        Public Sub New(single__1 As Boolean, multi__2 As Boolean, hosting__3 As Boolean, reserve0__4 As Boolean)
            [SINGLE] = single__1
            MULTI = multi__2
            HOSTING = hosting__3
            RESERVE0 = reserve0__4
        End Sub
    End Structure 'TODO: is static good enough for GAME_MODE_S defined variables?
    Public Shared modeAll As New GAME_MODE_S(True, True, True, True)
    Public Shared modeSingle As New GAME_MODE_S(True, False, False, False)
    Public Shared modeSingleMulti As New GAME_MODE_S(True, True, False, False)
    Public Shared modeSingleHost As New GAME_MODE_S(True, False, True, False)
    Public Shared modeMulti As New GAME_MODE_S(False, True, False, False)
    Public Shared modeMultiHost As New GAME_MODE_S(False, True, True, False)
    Public Shared modeHost As New GAME_MODE_S(False, False, True, False)
    Public ReadOnly PI As Double = 3.1415926535897931

    Public Enum PLAYER_VALIDATE As Integer
        [DEFAULT] = 0
        BYPASS
        BANNED
        PASS_REJECT
    End Enum

    Public Enum VEHICLE_RESPAWN As Integer
        [DEFAULT] = -1
        BYPASS         ' Do not process
        FORCE          ' Force Default process
        RELOCATE       ' Relocate With given data.
        DESTROY         ' Do Not respawn, instead destroy it.
    End Enum

    Public Enum OBJECT_ATTEMPT As Integer
        [DEFAULT] = -1
        BYPASS          ' Do not process
        FORCE           ' Force Default process
        ALTERNATE       ' NOTE: Only for create attempt
    End Enum
End Class
