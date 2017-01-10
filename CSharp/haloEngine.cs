#if EXT_IHALOENGINE
using System;
using System.Runtime.InteropServices;
using System.Text;

    namespace Addon_API {
        public enum REJECT_CODE: byte {
        CANT_JOIN_SERVER = 0,       //0
        INVALID_CONNECTION_REQUEST, //1
        PASSWORD_REJECTED,          //2
        SERVER_IS_FULL,             //3
        CD_KEY_INVALID,             //4
        CD_KEY_INUSED,              //5
        OP_BANNED,                  //6
        OP_KICKED,                  //7
        VIDEO_TEST,                 //8
        CHECKPOINT_SAVED,           //9
        ADDRESS_INVALID,            //10
        PROFILE_REQUIRED,           //11
        INCOMPATIBLE_NETWORK,       //12
        OLDER_PLAYER_VERSION,       //13
        NEWER_PLAYER_VERSION,       //14
        ADMIN_REQUIRED_PATCH,       //15
        REQUEST_DELETE_SAVED,       //16
    };

    public enum HALO_VERSION: byte {
        UNKNOWN = 0,
        TRIAL,  //1,
        PC,     //2,
        CE,     //3
    };
    public struct IHaloEnginePtr {
        public IntPtr ptr;
    }
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct IHaloEngine {
        public s_server_header_ptr serverHeader;
        public s_player_reserved_slot_ptr playerReserved;
        public s_machine_slot_ptr machineHeader;
        public byte machineHeaderSize;
        public HALO_VERSION haloGameVersion;
        [MarshalAs(UnmanagedType.I1)]
        public bool isDedi;
        public byte reserved0;
        public IntPtr player_base;
        public s_gametype_ptr gameTypeLive;
        public s_cheat_header_ptr cheatHeader;
        public s_map_header_ptr mapCurrent;
        public s_console_header_ptr console;
        public UIntPtrValue gameUpTimeLive; //1 sec = 60 ticks
        public UIntPtrValue mapUpTimeLive; //1 sec = 30 ticks
        public UIntPtrValue mapTimeLimitLive;
        public UIntPtrValue mapTimeLimitPermament;
        public s_console_color_list_ptr consoleColor;
        //TODO: Need to remove these 3 pointers for client will be support later on.
        [Obsolete("Do not use DirectX9 function, will be remove any time soon.")]
        public IntPtr DirectX9;
        [Obsolete("Do not use DirectInput8 function, will be remove any time soon.")]
        public IntPtr DirectInput8;
        [Obsolete("Do not use DirectSound8 function, will be remove any time soon.")]
        public IntPtr DirectSound8;
        public BoolPtr cheatVEject;
        public s_gametype_globals_ptr gameTypeGlobals;
        public s_map_status_ptr mapStatus;


        //Functions

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_build_packet([In, Out] byte[] packet_data, [In] uint arg1, [In] uint packettype, [In] uint arg3, [In] ref IntPtr data_pointer, [In] uint arg4, [In] uint arg5, [In] uint arg6);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_add_packet_to_player_queue([In] uint machine_index, [In, MarshalAs(UnmanagedType.LPArray)] byte[] packet, [In] uint packetCode, [In] uint arg1, [In] uint arg2, [In] uint arg3, [In] uint arg4, [In] uint arg5);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_add_packet_to_global_queue([In, MarshalAs(UnmanagedType.LPArray)] byte[] packet_data, [In] uint packetCode, [In] uint arg1, [In] uint arg2, [In] uint arg3, [In] uint arg4, [In] uint arg5);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_dispatch_rcon(ref rconData data, ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_dispatch_player(ref chatData data, [In] uint len, ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_dispatch_global(ref chatData data, [In] uint len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_send_reject_code(s_machine_slot_ptr player, REJECT_CODE code);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_set_idling();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_map_next();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_exec_command([MarshalAs(UnmanagedType.LPStr)] string command);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_get_server_password([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pass);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_set_server_password([In, MarshalAs(UnmanagedType.LPWStr)] string pass);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_get_rcon_password([In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder pass);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_set_rcon_password([In, MarshalAs(UnmanagedType.LPStr)] string pass);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_ext_add_on_get_info_by_index([In] uint index, ref addon_info getInfo);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_ext_add_on_get_info_by_name([In, MarshalAs(UnmanagedType.LPWStr)] string name, ref addon_info getInfo);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_ext_add_on_reload([In, MarshalAs(UnmanagedType.LPWStr)] string name);

        //Halo Simulate Functions Begin
        /// <summary>
        /// To prepare a packet to send to player(s).
        /// </summary>
        /// <param name="packet_data">To build a buffer to send. Does not accept null or unallocate memory.</param>
        /// <param name="arg1">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="packettype">Unknown, do not have a list for this. (Use at your risk!)</param>
        /// <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="data_pointer">Any data you want send to player/server.</param>
        /// <param name="arg4">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="arg5">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg6">Unknown, usually 0 (Use at your risk!)</param>
        /// <returns>Return unique ID to be used to add in a queue functions.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_build_packet m_build_packet;
        /// <summary>
        /// To add a queue send to specific player.
        /// </summary>
        /// <param name="machine_index">Unique machine_index from s_machine structure.</param>
        /// <param name="packet">Only use packet_data buffer from m_build_packet.</param>
        /// <param name="packetCode">The return value from m_build_packet to be used.</param>
        /// <param name="arg1">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg2">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="arg4">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg5">Unknown, usually 3 (Use at your risk!)</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_add_packet_to_player_queue m_add_packet_to_player_queue;
        /// <summary>
        /// To add a queue send to all players.
        /// </summary>
        /// <param name="packet_data">To build a buffer to send. Does not accept null or unallocate memory.</param>
        /// <param name="packettype">Unknown, do not have a list for this. (Use at your risk!)</param>
        /// <param name="arg1">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg2">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="arg4">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg5">Unknown, usually 3 (Use at your risk!)</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_add_packet_to_global_queue m_add_packet_to_global_queue;
        /// <summary>
        /// Dispatch a rcon message to specific player.
        /// </summary>
        /// <param name="data">A message you would like to send.</param>
        /// <param name="plI">Specific player to receive this rcon message.</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_dispatch_rcon m_dispatch_rcon;
        /// <summary>
        /// Dispatch a chat message to specific player.
        /// </summary>
        /// <param name="data">A message you would like to send.</param>
        /// <param name="len">Length of characters from data, maximum is 80 (0x50).</param>
        /// <param name="plI">Specific player to receive this chat message.</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_dispatch_player m_dispatch_player;
        /// <summary>
        /// Dispatch a chat message to all players.
        /// </summary>
        /// <param name="data">A message you would like to send.</param>
        /// <param name="len">Length of characters from data, maximum is 80 (0x50).</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_dispatch_global m_dispatch_global;
        /// <summary>
        /// To send a rejection code reason to player and disconnect player from host.
        /// </summary>
        /// <param name="player">Pass down an active s_machine_slot pointer.</param>
        /// <param name="code">See REJECT_CODE for codes available to use.</param>
        /// <returns>Return true if successfully sent packet.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_send_reject_code m_send_reject_code;
        /// <summary>
        /// To set a server to idle state. (Only supportive for server.)
        /// </summary>
        /// <returns>Return true if will set host to idling.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_set_idling m_set_idling;
        /// <summary>
        /// To end a current game and move on to the next map. (Only supportive for server.)
        /// </summary>
        /// <returns>Return true if host is changing to next map.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_map_next m_map_next;
        /// <summary>
        /// To execute native halo command. (May will be deprecate in future.)
        /// </summary>
        /// <param name="command">Input a command.</param>
        /// <returns>Return true if execution is a success.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_exec_command m_exec_command;
        /// <summary>
        /// Get the current password for hosting un/lock.
        /// </summary>
        /// <param name="pass">Return the current password. (Must be at least 8 characters long.)</param>
        /// <returns>Does not return a value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_server_password m_get_server_password;
        /// <summary>
        /// Set the current password for hosting un/lock.
        /// </summary>
        /// <param name="pass">Set the current password. (Maximum permitted is 8 characters long.)</param>
        /// <returns>Does not return a value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_set_server_password m_set_server_password;
        /// <summary>
        /// Get the current rcon password for authorized players to execute command.
        /// </summary>
        /// <param name="pass">Return the current rcon password. (Must be at least 8 characters long.)</param>
        /// <returns>Does not return a value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_rcon_password m_get_rcon_password;
        /// <summary>
        /// Set the current rcon password for authorized players to execute command.
        /// </summary>
        /// <param name="pass">Set the current rcon password. (Maximum permitted is 8 characters long.)</param>
        /// <returns>Does not return a value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_set_rcon_password m_set_rcon_password;
        //Halo Simulate Functions End
        /// <summary>
        /// Obtain an Add-on information if able to find a match.
        /// </summary>
        /// <param name="index">Input an Add-on index slot number.</param>
        /// <param name="getInfo">Output a matched Add-on or not.</param>
        /// <returns>Return true or false if unable find a match.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_ext_add_on_get_info_by_index m_ext_add_on_get_info_by_index;
        /// <summary>
        /// Obtain an Add-on information if able to find a match.
        /// </summary>
        /// <param name="name">Input name of an Add-on. (Maximum permitted is 128 characters long.)</param>
        /// <param name="getInfo">Output a matched Add-on or not.</param>
        /// <returns>Return true or false if unable find a match.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_ext_add_on_get_info_by_name m_ext_add_on_get_info_by_name;
        /// <summary>
        /// Reload an Add-on while still running Halo.
        /// </summary>
        /// <param name="name">Input name of an Add-on. (Maximum permitted is 128 characters long.)</param>
        /// <returns>Return true or false if unable to reload Add-on.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_ext_add_on_reload m_ext_add_on_reload;
        //*/


        //[MarshalAs(UnmanagedType.FunctionPtr)]
        //public d_is_authorized m_is_authorized;

        //Simple & easier user-defined conversion + checker for null.
        public IHaloEngine(IHaloEnginePtr data) {
            if (data.ptr != IntPtr.Zero) {
                this = (IHaloEngine)Marshal.PtrToStructure(data.ptr, typeof(IHaloEngine));
                Global.s_machine_slot_size = this.machineHeaderSize;
            } else
                this = new IHaloEngine();
        }
        public static implicit operator IHaloEngine(IHaloEnginePtr data) {
            return new IHaloEngine(data);
        }
        public bool isNotNull() {
            return serverHeader.ptr != null;
        }
    }

    public partial struct Interface {
        /// <summary>
        /// Returns a IHaloEngine class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IAdmin class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#10", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern IHaloEnginePtr getIHaloEngine([In] uint uniqueHash);
    }
}
#endif
