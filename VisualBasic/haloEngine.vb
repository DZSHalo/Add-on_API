#If EXT_IHALOENGINE Then
Imports System.Runtime.InteropServices
Imports System.Text

Namespace Addon_API
    Public Enum REJECT_CODE As Byte
        CANT_JOIN_SERVER = 0        '0
        INVALID_CONNECTION_REQUEST  '1
        PASSWORD_REJECTED           '2
        SERVER_IS_FULL              '3
        CD_KEY_INVALID              '4
        CD_KEY_INUSED               '5
        OP_BANNED                   '6
        OP_KICKED                   '7
        VIDEO_TEST                  '8
        CHECKPOINT_SAVED            '9
        ADDRESS_INVALID             '10
        PROFILE_REQUIRED            '11
        INCOMPATIBLE_NETWORK        '12
        OLDER_PLAYER_VERSION        '13
        NEWER_PLAYER_VERSION        '14
        ADMIN_REQUIRED_PATCH        '15
        REQUEST_DELETE_SAVED        '16
    End Enum

    Public Enum HALO_VERSION As Byte
        UNKNOWN = 0
        TRIAL   '1,
        PC      '2,
        CE      '3
    End Enum
    Public Structure IHaloEnginePtr
        Public ptr As IntPtr
    End Structure
    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Structure IHaloEngine
        Public serverHeader As s_server_header_ptr
        Public playerReserved As s_player_reserved_slot_ptr
        Public machineHeader As s_machine_slot_ptr
        Public machineHeaderSize As Byte
        Public haloGameVersion As HALO_VERSION
        <MarshalAs(UnmanagedType.I1)>
        Public isDedi As Boolean
        Public reserved0 As Byte
        Public player_base As IntPtr
        Public gameTypeLive As s_gametype_ptr
        Public cheatHeader As s_cheat_header_ptr
        Public mapCurrent As s_map_header_ptr
        Public console As s_console_header_ptr
        Public gameUpTimeLive As UIntPtrValue '1 sec = 60 ticks
        Public mapUpTimeLive As UIntPtrValue  '1 sec = 30 ticks
        Public mapTimeLimitLive As UIntPtrValue
        Public mapTimeLimitPermament As UIntPtrValue
        Public consoleColor As s_console_color_list_ptr 'TODO: Need to remove these 3 pointers for client will be support later on.
        <Obsolete("Do not use DirectX9 function, will be remove any time soon.")>
        Public DirectX9 As IntPtr
        <Obsolete("Do not use DirectInput8 function, will be remove any time soon.")>
        Public DirectInput8 As IntPtr
        <Obsolete("Do not use DirectSound8 function, will be remove any time soon.")>
        Public DirectSound8 As IntPtr
        Public cheatVEject As BoolPtr
        Public gameTypeGlobals As s_gametype_globals_ptr
        Public mapStatus As s_map_status_ptr


        'Functions

        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_build_packet(<[In], Out> packet_data As Byte(), <[In]> arg1 As UInteger, <[In]> packettype As UInteger, <[In]> arg3 As UInteger, <[In]> ByRef data_pointer As IntPtr, <[In]> arg4 As UInteger, <[In]> arg5 As UInteger, <[In]> arg6 As UInteger) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_add_packet_to_player_queue(<[In]> machine_index As UInteger, <[In], MarshalAs(UnmanagedType.LPArray)> packet As Byte(), <[In]> packetCode As UInteger, <[In]> arg1 As UInteger, <[In]> arg2 As UInteger, <[In]> arg3 As UInteger, <[In]> arg4 As UInteger, <[In]> arg5 As UInteger)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_add_packet_to_global_queue(<[In], MarshalAs(UnmanagedType.LPArray)> packet_data As Byte(), <[In]> packetCode As UInteger, <[In]> arg1 As UInteger, <[In]> arg2 As UInteger, <[In]> arg3 As UInteger, <[In]> arg4 As UInteger, <[In]> arg5 As UInteger)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_dispatch_rcon(ByRef data As rconData, ByRef plI As PlayerInfo)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_dispatch_player(ByRef data As chatData, <[In]> len As UInteger, ByRef plI As PlayerInfo)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_dispatch_global(ByRef data As chatData, <[In]> len As UInteger)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_send_reject_code(player As s_machine_slot_ptr, code As REJECT_CODE) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_set_idling() As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_map_next() As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_exec_command(<MarshalAs(UnmanagedType.LPStr)> command As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_get_server_password(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> pass As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_set_server_password(<[In], MarshalAs(UnmanagedType.LPWStr)> pass As String)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_get_rcon_password(<[In], Out, MarshalAs(UnmanagedType.LPStr)> pass As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_set_rcon_password(<[In], MarshalAs(UnmanagedType.LPStr)> pass As String)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_ext_add_on_get_info_by_index(<[In]> index As UInteger, ByRef getInfo As addon_info) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_ext_add_on_get_info_by_name(<[In], MarshalAs(UnmanagedType.LPWStr)> name As String, ByRef getInfo As addon_info) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_ext_add_on_reload(<[In], MarshalAs(UnmanagedType.LPWStr)> name As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        'Halo Simulate Functions Begin
        ''' <summary>
        ''' To prepare a packet to send to player(s).
        ''' </summary>
        ''' <param name="packet_data">To build a buffer to send. Does not accept null or unallocate memory.</param>
        ''' <param name="arg1">Unknown, usually 0 (Use at your risk!)</param>
        ''' <param name="packettype">Unknown, do not have a list for this. (Use at your risk!)</param>
        ''' <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        ''' <param name="data_pointer">Any data you want send to player/server.</param>
        ''' <param name="arg4">Unknown, usually 0 (Use at your risk!)</param>
        ''' <param name="arg5">Unknown, usually 1 (Use at your risk!)</param>
        ''' <param name="arg6">Unknown, usually 0 (Use at your risk!)</param>
        ''' <returns>Return unique ID to be used to add in a queue functions.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_build_packet As d_build_packet
        ''' <summary>
        ''' To add a queue send to specific player.
        ''' </summary>
        ''' <param name="machine_index">Unique machine_index from s_machine structure.</param>
        ''' <param name="packet">Only use packet_data buffer from m_build_packet.</param>
        ''' <param name="packetCode">The return value from m_build_packet to be used.</param>
        ''' <param name="arg1">Unknown, usually 1 (Use at your risk!)</param>
        ''' <param name="arg2">Unknown, usually 1 (Use at your risk!)</param>
        ''' <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        ''' <param name="arg4">Unknown, usually 1 (Use at your risk!)</param>
        ''' <param name="arg5">Unknown, usually 3 (Use at your risk!)</param>
        ''' <returns>Does not return a value. (May will be changed later on.)</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_add_packet_to_player_queue As d_add_packet_to_player_queue
        ''' <summary>
        ''' To add a queue send to all players.
        ''' </summary>
        ''' <param name="packet_data">To build a buffer to send. Does not accept null or unallocate memory.</param>
        ''' <param name="packettype">Unknown, do not have a list for this. (Use at your risk!)</param>
        ''' <param name="arg1">Unknown, usually 1 (Use at your risk!)</param>
        ''' <param name="arg2">Unknown, usually 1 (Use at your risk!)</param>
        ''' <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        ''' <param name="arg4">Unknown, usually 1 (Use at your risk!)</param>
        ''' <param name="arg5">Unknown, usually 3 (Use at your risk!)</param>
        ''' <returns>Does not return a value. (May will be changed later on.)</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_add_packet_to_global_queue As d_add_packet_to_global_queue
        ''' <summary>
        ''' Dispatch a rcon message to specific player.
        ''' </summary>
        ''' <param name="data">A message you would like to send.</param>
        ''' <param name="plI">Specific player to receive this rcon message.</param>
        ''' <returns>Does not return a value. (May will be changed later on.)</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_dispatch_rcon As d_dispatch_rcon
        ''' <summary>
        ''' Dispatch a chat message to specific player.
        ''' </summary>
        ''' <param name="data">A message you would like to send.</param>
        ''' <param name="len">Length of characters from data, maximum is 80 (0x50).</param>
        ''' <param name="plI">Specific player to receive this chat message.</param>
        ''' <returns>Does not return a value. (May will be changed later on.)</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_dispatch_player As d_dispatch_player
        ''' <summary>
        ''' Dispatch a chat message to all players.
        ''' </summary>
        ''' <param name="data">A message you would like to send.</param>
        ''' <param name="len">Length of characters from data, maximum is 80 (0x50).</param>
        ''' <returns>Does not return a value. (May will be changed later on.)</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_dispatch_global As d_dispatch_global
        ''' <summary>
        ''' To send a rejection code reason to player and disconnect player from host.
        ''' </summary>
        ''' <param name="player">Pass down an active s_machine_slot pointer.</param>
        ''' <param name="code">See REJECT_CODE for codes available to use.</param>
        ''' <returns>Return true if successfully sent packet.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_send_reject_code As d_send_reject_code
        ''' <summary>
        ''' To set a server to idle state. (Only supportive for server.)
        ''' </summary>
        ''' <returns>Return true if will set host to idling.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_set_idling As d_set_idling
        ''' <summary>
        ''' To end a current game and move on to the next map. (Only supportive for server.)
        ''' </summary>
        ''' <returns>Return true if host is changing to next map.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_map_next As d_map_next
        ''' <summary>
        ''' To execute native halo command. (May will be deprecate in future.)
        ''' </summary>
        ''' <param name="command">Input a command.</param>
        ''' <returns>Return true if execution is a success.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_exec_command As d_exec_command
        ''' <summary>
        ''' Get the current password for hosting un/lock.
        ''' </summary>
        ''' <param name="pass">Return the current password. (Must be at least 8 characters long.)</param>
        ''' <returns>Does not return a value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_server_password As d_get_server_password
        ''' <summary>
        ''' Set the current password for hosting un/lock.
        ''' </summary>
        ''' <param name="pass">Set the current password. (Maximum permitted is 8 characters long.)</param>
        ''' <returns>Does not return a value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_set_server_password As d_set_server_password
        ''' <summary>
        ''' Get the current rcon password for authorized players to execute command.
        ''' </summary>
        ''' <param name="pass">Return the current rcon password. (Must be at least 8 characters long.)</param>
        ''' <returns>Does not return a value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_rcon_password As d_get_rcon_password
        ''' <summary>
        ''' Set the current rcon password for authorized players to execute command.
        ''' </summary>
        ''' <param name="pass">Set the current rcon password. (Maximum permitted is 8 characters long.)</param>
        ''' <returns>Does not return a value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_set_rcon_password As d_set_rcon_password
        'Halo Simulate Functions End
        ''' <summary>
        ''' Obtain an Add-on information if able to find a match.
        ''' </summary>
        ''' <param name="index">Input an Add-on index slot number.</param>
        ''' <param name="getInfo">Output a matched Add-on or not.</param>
        ''' <returns>Return true or false if unable find a match.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_ext_add_on_get_info_by_index As d_ext_add_on_get_info_by_index
        ''' <summary>
        ''' Obtain an Add-on information if able to find a match.
        ''' </summary>
        ''' <param name="name">Input name of an Add-on. (Maximum permitted is 128 characters long.)</param>
        ''' <param name="getInfo">Output a matched Add-on or not.</param>
        ''' <returns>Return true or false if unable find a match.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_ext_add_on_get_info_by_name As d_ext_add_on_get_info_by_name
        ''' <summary>
        ''' Reload an Add-on while still running Halo.
        ''' </summary>
        ''' <param name="name">Input name of an Add-on. (Maximum permitted is 128 characters long.)</param>
        ''' <returns>Return true or false if unable to reload Add-on.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_ext_add_on_reload As d_ext_add_on_reload
        '*/


        '[MarshalAs(UnmanagedType.FunctionPtr)]
        'public d_is_authorized m_is_authorized;

        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As IHaloEnginePtr) As IHaloEngine
            If data.ptr <> IntPtr.Zero Then
                Dim placeHolder As IHaloEngine = CType(Marshal.PtrToStructure(data.ptr, GetType(IHaloEngine)), IHaloEngine)
                [Global].s_machine_slot_size = placeHolder.machineHeaderSize
                Return placeHolder
            Else
                Return New IHaloEngine
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return serverHeader.ptr <> Nothing
        End Function
    End Structure

    Public Partial Structure [Interface]
        ''' <summary>
        ''' Returns a IHaloEngine class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IAdmin class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint := "#10", CallingConvention := CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Function getIHaloEngine(<[In]> uniqueHash As UInteger) As IHaloEnginePtr
        End Function
    End Structure
End Namespace
#End If
