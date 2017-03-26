#If EXT_IPLAYER Then
Imports System.Text
#End If
Imports System.Runtime.InteropServices

Namespace Addon_API

    Public Enum MSG_FORMAT As UInteger
        MF_BLANK = 0
        MF_SERVER = 1
        MF_HEXT = 2
        MF_ADMINBOT = 3
        MF_INFO = 4
        MF_WARNING = 5
        MF_ERROR = 6
        MF_ALERT = 7
    End Enum
    Public Enum MSG_PROTOCOL As UInteger
        MP_CHAT = 0
        MP_RCON = 1
        MP_REMOTE = 2
    End Enum

    <StructLayout(LayoutKind.Sequential, Pack:=1, CharSet:=CharSet.Ansi)>
    Public Structure CDHashAFix
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=33)>
        Public CDHashA As String
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=1, CharSet:=CharSet.Unicode)>
    Public Structure PlayerExtended
        <MarshalAs(UnmanagedType.I1)>
        Public isInServer As Boolean
        <MarshalAs(UnmanagedType.I1)>
        Public isRemote As Boolean
        Public adminLvl As Short
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=25)>
        Public user As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=33)>
        Public pass As String
        Public FixCDHashA As CDHashAFix
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=33)>
        Public CDHashW As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=16)>
        Public IP_Addr As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)>
        Public IP_Port As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=24)>
        Public IP_Full As String
        <MarshalAs(UnmanagedType.I1)>
        Public temp_pass As Boolean
        <MarshalAs(UnmanagedType.I1)>
        Public can_chat As Boolean
        <MarshalAs(UnmanagedType.I1)>
        Public last_team As Boolean
        <MarshalAs(UnmanagedType.I1)>
        Public reserved As Boolean
        Public last_check As Integer
        Public handicap_ammo As Single
        Public handicap_clip As Single
        Public handicap_shield As Single
        Public handicap_health As Single
        Public handicap_speed As Single

        Private Sub New(ByRef plI As PlayerExtended)
            isInServer = plI.isInServer
            isRemote = plI.isRemote
            adminLvl = plI.adminLvl
            user = plI.user
            pass = plI.pass
            FixCDHashA = plI.FixCDHashA
            CDHashW = plI.CDHashW
            IP_Addr = plI.IP_Addr
            IP_Port = plI.IP_Port
            IP_Full = plI.IP_Full
            temp_pass = plI.temp_pass
            can_chat = plI.can_chat
            last_team = plI.last_team
            reserved = plI.reserved
            last_check = plI.last_check
            handicap_ammo = plI.handicap_ammo
            handicap_clip = plI.handicap_clip
            handicap_shield = plI.handicap_shield
            handicap_health = plI.handicap_health
            'handicap_shield = 1;
            '            handicap_health = 1;
            '            handicap_speed = 1;

            handicap_speed = plI.handicap_speed
        End Sub
    End Structure
    Public Structure PlayerInfo
        Public cplEx As IntPtr
        Public Property plEx() As PlayerExtended
            Get
                If cplEx = IntPtr.Zero Then
                    Return New PlayerExtended()
                Else
                    Return CType(Marshal.PtrToStructure(cplEx, GetType(PlayerExtended)), PlayerExtended)
                End If
            End Get
            Set(value As PlayerExtended)
                If cplEx = IntPtr.Zero Then
                    cplEx = Marshal.AllocHGlobal(Marshal.SizeOf(value))
                End If
                Marshal.StructureToPtr(value, cplEx, False)
            End Set
        End Property
        Public cmS As IntPtr
        Public Property mS() As s_machine_slot
            Get
                If cmS = IntPtr.Zero Then
                    Return New s_machine_slot()
                Else
                    Return CType(Marshal.PtrToStructure(cmS, GetType(s_machine_slot)), s_machine_slot)
                End If
            End Get
            Set(value As s_machine_slot)
                If cmS = IntPtr.Zero Then
                    cmS = Marshal.AllocHGlobal(Marshal.SizeOf(value))
                End If
                Marshal.StructureToPtr(value, cmS, False)
            End Set
        End Property
        Public cplR As IntPtr
        Public Property plR() As s_player_reserved_slot
            Get
                If cplR = IntPtr.Zero Then
                    Return New s_player_reserved_slot()
                Else
                    Return CType(Marshal.PtrToStructure(cplR, GetType(s_player_reserved_slot)), s_player_reserved_slot)
                End If
            End Get
            Set(value As s_player_reserved_slot)
                If cplR = IntPtr.Zero Then
                    cplR = Marshal.AllocHGlobal(Marshal.SizeOf(value))
                End If
                Marshal.StructureToPtr(value, cplR, False)
            End Set
        End Property
        Public cplS As IntPtr
        Public Property plS() As s_player_slot
            Get
                If cplS = IntPtr.Zero Then
                    Return New s_player_slot()
                Else
                    Return CType(Marshal.PtrToStructure(cplS, GetType(s_player_slot)), s_player_slot)
                End If
            End Get
            Set(value As s_player_slot)
                If cplS = IntPtr.Zero Then
                    cplS = Marshal.AllocHGlobal(Marshal.SizeOf(value))
                End If
                Marshal.StructureToPtr(value, cplS, False)
            End Set
        End Property
        Private Sub New(ByRef plI As PlayerInfo)
            cplEx = plI.cplEx
            cmS = plI.cmS
            cplR = plI.cplR
            cplS = plI.cplS
        End Sub
        Public Sub Clear()
            cplEx = IntPtr.Zero
            cmS = IntPtr.Zero
            cplR = IntPtr.Zero
            cplS = IntPtr.Zero
        End Sub
        Public Sub Free()
            If cplEx <> IntPtr.Zero Then
                Marshal.FreeHGlobal(cplEx)
                cplEx = IntPtr.Zero
            End If
            If cmS <> IntPtr.Zero Then
                Marshal.FreeHGlobal(cmS)
                cmS = IntPtr.Zero
            End If
            If cplR <> IntPtr.Zero Then
                Marshal.FreeHGlobal(cplR)
                cplR = IntPtr.Zero
            End If
            If cplS <> IntPtr.Zero Then
                Marshal.FreeHGlobal(cplS)
                cplS = IntPtr.Zero
            End If
        End Sub
    End Structure
    Public Class PlayerInfoPtr
        Public playerInfoPtr As PlayerInfo
    End Class

    <StructLayout(LayoutKind.Sequential)>
    Public Structure PlayerInfoList
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)>
        Private plList As PlayerInfo()
    End Structure

#If EXT_IPLAYER Then

    Public Structure IPlayerPtr
        Public ptr As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure IPlayer
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_m_index(<[In]> m_ind As Byte, <[In], Out> ByRef plI As PlayerInfo, <[In], MarshalAs(UnmanagedType.I1)> fullRequest As Boolean) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_id(<[In]> playerId As UInteger, <[In], Out> ByRef plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_ident(<[In]> pl_Tag As s_ident, <[In], Out> ByRef plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_by_nickname(<[In], MarshalAs(UnmanagedType.LPWStr)> nickname As String, <[In], Out> ByRef plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_by_username(<[In], MarshalAs(UnmanagedType.LPWStr)> username As String, <[In], Out> ByRef plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_by_unique_id(<[In]> uniqueID As UInteger, <[In], Out> ByRef plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_id_full_name(<[In], MarshalAs(UnmanagedType.LPWStr)> fullName As String) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_id_ip_address(<[In], MarshalAs(UnmanagedType.LPWStr)> ipAddress As String) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_id_port(<[In], MarshalAs(UnmanagedType.LPWStr)> port As String) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_full_name_id(<[In]> ID As UInteger, <[In], Out, MarshalAs(UnmanagedType.LPWStr)> fullName As StringBuilder) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_ip_address_id(<[In]> ID As UInteger, <[In], Out, MarshalAs(UnmanagedType.LPWStr)> ipAddress As StringBuilder) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_port_id(<[In]> ID As UInteger, <[In], Out, MarshalAs(UnmanagedType.LPWStr)> port As StringBuilder) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_update(<[In], Out> ByRef plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_set_nickname(<[In]> ByRef plI As PlayerInfo, <[In], MarshalAs(UnmanagedType.LPWStr)> nickname As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_send_custom_message(<[In]> formatMsg As MSG_FORMAT, <[In]> protocolMsg As MSG_PROTOCOL, <[In]> ByRef plI As PlayerInfo, <[In], MarshalAs(UnmanagedType.LPWStr)> Msg As String, argTotal As UInteger, <[In], [ParamArray]> argList As Object()) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_is_admin(<[In]> m_ind As Byte) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_by_biped_tag_current(<Out> bipedTag As s_ident, <[In]> ByRef plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_by_biped_tag_previous(<Out> bipedTag As s_ident, <[In]> ByRef plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_send_custom_message_broadcast(formatMsg As MSG_FORMAT, <[In], MarshalAs(UnmanagedType.LPWStr)> Msg As String, argTotal As UInteger, <[In], [ParamArray]> argList As Object()) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_change_team(<[In], Out> ByRef plI As PlayerInfo, <[In]> new_team As e_color_team_index, <[In], MarshalAs(UnmanagedType.I1)> forceKill As Boolean)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_apply_camo(<[In]> ByRef plI As PlayerInfo, <[In]> duration As UInteger)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_ban_player(<[In]> ByRef plEx As PlayerExtended, <[In]> ByRef gmtm As tm) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_ban_CD_key(<[In], MarshalAs(UnmanagedType.LPWStr)> CDHash As String, <[In]> ByRef gmtm As tm) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_ban_ip(<[In], MarshalAs(UnmanagedType.LPWStr)> IP_Address As String, <[In]> ByRef gmtm As tm) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_ban_ip_get_id(<[In], MarshalAs(UnmanagedType.LPWStr)> IP_Address As String) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_ban_CD_key_get_id(<[In], MarshalAs(UnmanagedType.LPWStr)> CDHash As String) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_unban_id(<[In]> ID As UInteger) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_ip(<[In]> ByRef mS As s_machine_slot, <[In], Out> ByRef m_ip As in_addr) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_port(<[In]> ByRef mS As s_machine_slot, <[In], Out> ByRef m_port As UShort) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_CD_hash(<[In]> ByRef mS As s_machine_slot, <[In], Out, MarshalAs(UnmanagedType.LPStr)> CDHash As StringBuilder) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_str_to_player_list(<[In], MarshalAs(UnmanagedType.LPWStr)> regexSearch As String, <[In], Out> ByRef plMatch As PlayerInfoList, <[In]> ByRef plOwner As PlayerInfo) As UShort
        ''' <summary>
        ''' Get PlayerInfo from machine index if in used.
        ''' </summary>
        ''' <param name="m_ind">Machine index</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <param name="fullRequest">Get full request detail, if partial are found. Then it will reset to null and return false.</param>
        ''' <returns>Return true or false if not found.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_m_index As d_get_m_index
        ''' <summary>
        ''' Get PlayerInfo from player index if in used.
        ''' </summary>
        ''' <param name="playerId">Player index</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if not found.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_id As d_get_id
        ''' <summary>
        ''' Get PlayerInfo from unique player s_ident.
        ''' </summary>
        ''' <param name="pl_Tag">Unique player s_ident.</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if not found.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_ident As d_get_ident
        ''' <summary>
        ''' Get PlayerInfo from existing nickname.
        ''' </summary>
        ''' <param name="nickname">Nickname</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if not found.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_by_nickname As d_get_by_nickname
        ''' <summary>
        ''' Get PlayerInfo from existing username.
        ''' </summary>
        ''' <param name="username">Username</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if not found.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_by_username As d_get_by_username
        ''' <summary>
        ''' Get PlayerInfo from uniqueID from s_machine_slot.
        ''' </summary>
        ''' <param name="uniqueID">Can be obtain from existing s_machine_slot.</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if not found.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_by_unique_id As d_get_by_unique_id
        ''' <summary>
        ''' Get ID from joined player's name.
        ''' </summary>
        ''' <param name="fullName">Player's full name.</param>
        ''' <returns>Return ID of full name from database.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_id_full_name As d_get_id_full_name
        ''' <summary>
        ''' Get ID from IP Address, excluded port number.
        ''' </summary>
        ''' <param name="ipAddress">Player's IP Address, excluded port number.</param>
        ''' <returns>Return ID  from database.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_id_ip_address As d_get_id_ip_address
        ''' <summary>
        ''' Get ID from port, excluded IP Address.
        ''' </summary>
        ''' <param name="port">Port number, excluded IP Address.</param>
        ''' <returns>Return ID of port from database.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_id_port As d_get_id_port
        ''' <summary>
        ''' Get full name from ID.
        ''' </summary>
        ''' <param name="ID">ID</param>
        ''' <param name="fullName">Full name</param>
        ''' <returns>Return true or false if unable get full name from database.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_full_name_id As d_get_full_name_id
        ''' <summary>
        ''' Get IP Address, excluded port number, from ID.
        ''' </summary>
        ''' <param name="ID">ID</param>
        ''' <param name="ipAddress">IP Address, excluded port number</param>
        ''' <returns>Return true or false if unable get IP Address from database.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_ip_address_id As d_get_ip_address_id
        ''' <summary>
        ''' Get port number, excluded IP Address, from ID.
        ''' </summary>
        ''' <param name="ID">ID</param>
        ''' <param name="port">Port number, excluded IP Address</param>
        ''' <returns>Return true or false if unable get port number from database.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_port_id As d_get_port_id
        ''' <summary>
        ''' Update PlayerInfo from database.
        ''' </summary>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if unable to update.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_update As d_update
        ''' <summary>
        ''' Set Player's nickname.
        ''' </summary>
        ''' <param name="plI">PlayerInfo</param>
        ''' <param name="nickname">Nickname</param>
        ''' <returns>Return true or false if unable to set nickname.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_set_nickname As d_set_nickname
        ''' <summary>
        ''' To send a message through chat, rcon, or remote protocol to a specific player.
        ''' </summary>
        ''' <param name="formatMsg">See MSG_FORMAT for detail.</param>
        ''' <param name="protocolMsg">See MSG_FORMAT for detail.</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <param name="Msg">A message or predefined message.</param>
        ''' <param name="argTotal">Total arguments in argList.</param>
        ''' <param name="argList">To fill in the blank in a pre-defined message.</param>
        ''' <returns>Return true or false if unable to send a message.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_send_custom_message As d_send_custom_message
        ''' <summary>
        ''' To verify if a player is an admin.
        ''' </summary>
        ''' <param name="m_ind">Machine index</param>
        ''' <returns>Return true or false if is not an admin.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_is_admin As d_is_admin
        ''' <summary>
        ''' Find player from current unique biped s_ident.
        ''' </summary>
        ''' <param name="bipedTag">Unique biped s_ident</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if unable to find player using given current biped.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_by_biped_tag_current As d_get_by_biped_tag_current
        ''' <summary>
        ''' Find player from current unique biped s_ident.
        ''' </summary>
        ''' <param name="bipedTag">Unique biped s_ident</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if unable to find player using given previous biped.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_by_biped_tag_previous As d_get_by_biped_tag_current
        ''' <summary>
        ''' To send a message through chat procotol to all players.
        ''' </summary>
        ''' <param name="formatMsg">See MSG_FORMAT for detail.</param>
        ''' <param name="Msg">A message or predefined message.</param>
        ''' <param name="argTotal">Total arguments in argList.</param>
        ''' <param name="argList">To fill in the blank in a pre-defined message.</param>
        ''' <returns>Return true or false if unable to send a message.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_send_custom_message_broadcast As d_send_custom_message_broadcast
        ''' <summary>
        ''' Force player to change team with optional to kill player if needed.
        ''' </summary>
        ''' <param name="plI">PlayerInfo</param>
        ''' <param name="new_team">New team to assign.</param>
        ''' <param name="forcekill">Force kill player if needed.</param>
        ''' <returns>Does not return any value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_change_team As d_change_team
        ''' <summary>
        ''' To apply camouflage duration on specific player.
        ''' </summary>
        ''' <param name="plI">PlayerInfo</param>
        ''' <param name="duration">In seconds format.</param>
        ''' <returns>Does not return any value. (This may will be change in future.)</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_apply_camo As d_apply_camo

        ''' <summary>
        ''' Ban player from host server.
        ''' </summary>
        ''' <param name="plEx">Player to ban.</param>
        ''' <param name="gmtm">Time/date to expire ban.</param>
        ''' <returns>Return true or false unable to ban player, -1 for invalid argument.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_ban_player As d_ban_player
        ''' <summary>
        ''' Ban CD hash key from host server.
        ''' </summary>
        ''' <param name="CDHash">CD hash key to ban. (Must have 33 characters allocate to copy, 33th is to null termate.)</param>
        ''' <param name="gmtm">Time/date to expire ban.</param>
        ''' <returns>Return true or false unable to ban CD hash key, -1 for invalid argument.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_ban_CD_key As d_ban_CD_key
        ''' <summary>
        ''' Ban IP Address from host server.
        ''' </summary>
        ''' <param name="IP_Address">IP Address to ban.. (Must have 16 characters allocate to copy.)</param>
        ''' <param name="gmtm">Time/date to expire ban.</param>
        ''' <returns>Return true or false unable to ban IP Address, -1 for invalid argument.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_ban_ip As d_ban_ip
        ''' <summary>
        ''' Get ID from banned IP Address.
        ''' </summary>
        ''' <param name="IP_Address">Banned IP Address. (Maximum is 16 characters long.)</param>
        ''' <returns>Return ID of banned IP Address.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_ban_ip_get_id As d_ban_ip_get_id
        ''' <summary>
        ''' Get ID from banned CD hash key.
        ''' </summary>
        ''' <param name="CDHash">Banned CD hash key. (Must have 33 characters, 33th is to null termate.)</param>
        ''' <returns>Return ID of banned IP Address.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_ban_CD_key_get_id As d_ban_CD_key_get_id
        ''' <summary>
        ''' To expire a ban from banned list.
        ''' </summary>
        ''' <param name="ID">Obtained ID from either banned IP Address or CD hash key.</param>
        ''' <returns>Return true or false if unable to unban ID.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_unban_id As d_unban_id
        ''' <summary>
        ''' Get IP address, excluded port number, from machine slot.
        ''' </summary>
        ''' <param name="mS">machine slot</param>
        ''' <param name="m_ip">IP address, excluded port number</param>
        ''' <returns>Return true or false if unable get IP address.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_ip As d_get_ip
        ''' <summary>
        ''' Get port number, excluded IP address, from machine slot.
        ''' </summary>
        ''' <param name="mS">machine slot</param>
        ''' <param name="m_port">Port number, excluded IP address</param>
        ''' <returns>Return true or false if unable get port.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_port As d_get_port
        ''' <summary>
        ''' Get CD hash from machine slot.
        ''' </summary>
        ''' <param name="mS">machine slot</param>
        ''' <param name="CDHash">CD hash key. (Must have 33 characters allocated to copy, 33th is a null termated.)</param>
        ''' <returns>Return true or false if unable get CD hash.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_CD_hash As d_get_CD_hash
        ''' <summary>
        ''' Find a match of player(s) from regex expression search.
        ''' </summary>
        ''' <param name="regexSearch">To find a matching player(s).</param>
        ''' <param name="plMatch">List of matched players from search.</param>
        ''' <param name="plOwner">Optional, owner of player execution usually.</param>
        ''' <returns>Return total count of matched player(s).</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_str_to_player_list As d_get_str_to_player_list

        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As IPlayerPtr) As IPlayer
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(IPlayer)), IPlayer)
            Else
                Return New IPlayer
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return Me.m_get_m_index IsNot Nothing
        End Function
    End Structure
    Partial Public Structure [Interface]
        ''' <summary>
        ''' Returns a IPlayer class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IPlayer class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint:="#12", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Function getIPlayer(<[In]> uniqueHash As UInteger) As IPlayerPtr
        End Function
    End Structure
#End If
End Namespace
