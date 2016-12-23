using System;
#if EXT_IPLAYER
using System.Text;
#endif
using System.Runtime.InteropServices;

namespace Addon_API {

    public enum MSG_FORMAT : uint {
        MF_BLANK = 0,
        MF_SERVER = 1,
        MF_HEXT = 2,
        MF_ADMINBOT = 3,
        MF_INFO = 4,
        MF_WARNING = 5,
        MF_ERROR = 6,
        MF_ALERT = 7
    };
    public enum MSG_PROTOCOL : uint {
        MP_CHAT = 0,
        MP_RCON = 1,
        MP_REMOTE = 2
    };

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct CDHashAFix {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=33)]
        public string CDHashA;
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Unicode)]
    public struct PlayerExtended {
        [MarshalAs(UnmanagedType.I1)]
        public bool isInServer;
        [MarshalAs(UnmanagedType.I1)]
        public bool isRemote;
        public short adminLvl;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=25)]
        public string user;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=33)]
        public string pass;
        public CDHashAFix FixCDHashA;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=33)]
        public string CDHashW;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=16)]
        public string IP_Addr;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=8)]
        public string IP_Port;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=24)]
        public string IP_Full;
        [MarshalAs(UnmanagedType.I1)]
        public bool temp_pass;
        [MarshalAs(UnmanagedType.I1)]
        public bool can_chat;
        [MarshalAs(UnmanagedType.I1)]
        public bool last_team;
        [MarshalAs(UnmanagedType.I1)]
        public bool reserved;
        public int last_check;
        public float handicap_ammo;
        public float handicap_clip;
        public float handicap_shield;
        public float handicap_health;
        public float handicap_speed;

        PlayerExtended(ref PlayerExtended plI) {
            isInServer = plI.isInServer;
            isRemote = plI.isRemote;
            adminLvl = plI.adminLvl;
            user = plI.user;
            pass = plI.pass;
            FixCDHashA = plI.FixCDHashA;
            CDHashW = plI.CDHashW;
            IP_Addr = plI.IP_Addr;
            IP_Port = plI.IP_Port;
            IP_Full = plI.IP_Full;
            temp_pass = plI.temp_pass;
            can_chat = plI.can_chat;
            last_team = plI.last_team;
            reserved = plI.reserved;
            last_check = plI.last_check;
            handicap_ammo = plI.handicap_ammo;
            handicap_clip = plI.handicap_clip;
            handicap_shield = plI.handicap_shield;
            handicap_health = plI.handicap_health;
            handicap_speed = plI.handicap_speed;
            /*handicap_shield = 1;
            handicap_health = 1;
            handicap_speed = 1;*/
        }
    }
    public struct PlayerInfo {
        public IntPtr cplEx;
        public PlayerExtended plEx {
            get { if (cplEx == IntPtr.Zero) return new PlayerExtended();
                  else return (PlayerExtended)Marshal.PtrToStructure(cplEx, typeof(PlayerExtended)); }
            set { if (cplEx == IntPtr.Zero) cplEx = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                  Marshal.StructureToPtr(value, cplEx, false); }
        }
        public IntPtr cmS;
        public s_machine_slot mS {
            get { if (cmS == IntPtr.Zero) return new s_machine_slot();
                  else return (s_machine_slot)Marshal.PtrToStructure(cmS, typeof(s_machine_slot)); }
            set { if (cmS == IntPtr.Zero) cmS = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                  Marshal.StructureToPtr(value, cmS, false); }
        }
        public IntPtr cplR;
        public s_player_reserved_slot plR {
            get { if (cplR == IntPtr.Zero) return new s_player_reserved_slot();
                  else return (s_player_reserved_slot)Marshal.PtrToStructure(cplR, typeof(s_player_reserved_slot)); }
            set { if (cplR == IntPtr.Zero) cplR = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                  Marshal.StructureToPtr(value, cplR, false); }
        }
        public IntPtr cplS;
        public s_player_slot plS {
            get { if (cplS == IntPtr.Zero) return new s_player_slot();
                  else return (s_player_slot)Marshal.PtrToStructure(cplS, typeof(s_player_slot)); }
            set { if (cplS == IntPtr.Zero) cplS = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                  Marshal.StructureToPtr(value, cplS, false); }
        }
        PlayerInfo(ref PlayerInfo plI) {
            cplEx = plI.cplEx;
            cmS = plI.cmS;
            cplR = plI.cplR;
            cplS = plI.cplS;
        }
        public void Clear() {
            cplEx = IntPtr.Zero;
            cmS = IntPtr.Zero;
            cplR = IntPtr.Zero;
            cplS = IntPtr.Zero;
        }
        public void Free() {
            if (cplEx != IntPtr.Zero) {
                Marshal.FreeHGlobal(cplEx);
                cplEx = IntPtr.Zero;
            }
            if (cmS != IntPtr.Zero) {
                Marshal.FreeHGlobal(cmS);
                cmS = IntPtr.Zero;
            }
            if (cplR != IntPtr.Zero) {
                Marshal.FreeHGlobal(cplR);
                cplR = IntPtr.Zero;
            }
            if (cplS != IntPtr.Zero) {
                Marshal.FreeHGlobal(cplS);
                cplS = IntPtr.Zero;
            }
        }
    }
    public class PlayerInfoPtr {
        public PlayerInfo playerInfoPtr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PlayerInfoList {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
        PlayerInfo[] plList;
    };

#if EXT_IPLAYER

    public struct IPlayerPtr {
        public IntPtr ptr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IPlayer {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_m_index([In] byte m_ind, [In,Out] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_id([In] uint playerId, [In, Out] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_ident([In] s_ident pl_Tag, [In, Out] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_by_nickname([In, MarshalAs(UnmanagedType.LPWStr)] string nickname, [In, Out] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_by_username([In, MarshalAs(UnmanagedType.LPWStr)] string username, [In, Out] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_by_unique_id([In] uint uniqueID, [In, Out] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_get_id_full_name([In, MarshalAs(UnmanagedType.LPWStr)] string fullName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_get_id_ip_address([In, MarshalAs(UnmanagedType.LPWStr)] string ipAddress);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_get_id_port([In, MarshalAs(UnmanagedType.LPWStr)] string port);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool d_get_full_name_id([In] uint ID, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder fullName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool d_get_ip_address_id([In] uint ID, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder ipAddress);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate bool d_get_port_id([In] uint ID, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder port);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_update([In, Out] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_set_nickname([In] ref PlayerInfo plI, [In, MarshalAs(UnmanagedType.LPWStr)] string nickname);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_send_custom_message([In] MSG_FORMAT formatMsg, [In] MSG_PROTOCOL protocolMsg, [In] ref PlayerInfo plI, [In, MarshalAs(UnmanagedType.LPWStr)] string Msg, uint argTotal, [In] params object[] argList);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_is_admin([In] byte m_ind);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_by_biped_tag_current([Out] s_ident bipedTag, [In] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_by_biped_tag_previous([Out] s_ident bipedTag, [In] ref PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_send_custom_message_broadcast(MSG_FORMAT formatMsg, [In, MarshalAs(UnmanagedType.LPWStr)] string Msg, uint argTotal, [In] params object[] argList);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_change_team([In, Out] ref PlayerInfo plI, [In] e_color_team_index new_team, [In, MarshalAs(UnmanagedType.I1)] bool forceKill);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_apply_camo([In] ref PlayerInfo plI, [In] uint duration);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_ban_player([In] ref PlayerExtended plEx, [In] ref tm gmtm);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_ban_CD_key([In, MarshalAs(UnmanagedType.LPWStr)] string CDHash, [In] ref tm gmtm);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_ban_ip([In, MarshalAs(UnmanagedType.LPWStr)] string IP_Address, [In] ref tm gmtm);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_ban_ip_get_id([In, MarshalAs(UnmanagedType.LPWStr)] string IP_Address);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_ban_CD_key_get_id([In, MarshalAs(UnmanagedType.LPWStr)] string CDHash);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_unban_id([In] uint ID);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_ip([In] ref s_machine_slot mS, [In, Out] ref in_addr m_ip);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_port([In] ref s_machine_slot mS, [In, Out] ref ushort m_port);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_CD_hash([In] ref s_machine_slot mS, [In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder CDHash);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate short d_get_str_to_player_list([In, MarshalAs(UnmanagedType.LPWStr)] string regexSearch, [In, Out] ref PlayerInfoList plMatch, [In] PlayerInfoPtr plOwner);
        /// <summary>
        /// Get PlayerInfo from machine index if in used.
        /// </summary>
        /// <param name="m_ind">Machine index</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if not found.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_m_index m_get_m_index;
        /// <summary>
        /// Get PlayerInfo from player index if in used.
        /// </summary>
        /// <param name="playerId">Player index</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if not found.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_id m_get_id;
        /// <summary>
        /// Get PlayerInfo from unique player s_ident.
        /// </summary>
        /// <param name="pl_Tag">Unique player s_ident.</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if not found.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_ident m_get_ident;
        /// <summary>
        /// Get PlayerInfo from existing nickname.
        /// </summary>
        /// <param name="nickname">Nickname</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if not found.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_by_nickname m_get_by_nickname;
        /// <summary>
        /// Get PlayerInfo from existing username.
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if not found.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_by_username m_get_by_username;
        /// <summary>
        /// Get PlayerInfo from uniqueID from s_machine_slot.
        /// </summary>
        /// <param name="uniqueID">Can be obtain from existing s_machine_slot.</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if not found.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_by_unique_id m_get_by_unique_id;
        /// <summary>
        /// Get ID from joined player's name.
        /// </summary>
        /// <param name="fullName">Player's full name.</param>
        /// <returns>Return ID of full name from database.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_id_full_name m_get_id_full_name;
        /// <summary>
        /// Get ID from IP Address, excluded port number.
        /// </summary>
        /// <param name="ipAddress">Player's IP Address, excluded port number.</param>
        /// <returns>Return ID  from database.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_id_ip_address m_get_id_ip_address;
        /// <summary>
        /// Get ID from port, excluded IP Address.
        /// </summary>
        /// <param name="port">Port number, excluded IP Address.</param>
        /// <returns>Return ID of port from database.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_id_port m_get_id_port;
        /// <summary>
        /// Get full name from ID.
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="fullName">Full name</param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_full_name_id m_get_full_name_id;
        /// <summary>
        /// Get IP Address, excluded port number, from ID.
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="ipAddress">IP Address, excluded port number</param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_ip_address_id m_get_ip_address_id;
        /// <summary>
        /// Get port number, excluded IP Address, from ID.
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="port">Port number, excluded IP Address</param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_port_id m_get_port_id;
        /// <summary>
        /// Update PlayerInfo from database.
        /// </summary>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if unable to update.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_update m_update;
        /// <summary>
        /// Set Player's nickname.
        /// </summary>
        /// <param name="plI">PlayerInfo</param>
        /// <param name="nickname">Nickname</param>
        /// <returns>Return true or false if unable to set nickname.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_set_nickname m_set_nickname;
        /// <summary>
        /// To send a message through chat, rcon, or remote protocol to a specific player.
        /// </summary>
        /// <param name="formatMsg">See MSG_FORMAT for detail.</param>
        /// <param name="protocolMsg">See MSG_FORMAT for detail.</param>
        /// <param name="plI">PlayerInfo</param>
        /// <param name="Msg">A message or predefined message.</param>
        /// <param name="argTotal">Total arguments in argList.</param>
        /// <param name="argList">To fill in the blank in a pre-defined message.</param>
        /// <returns>Return true or false if unable to send a message.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_send_custom_message m_send_custom_message;
        /// <summary>
        /// To verify if a player is an admin.
        /// </summary>
        /// <param name="m_ind">Machine index</param>
        /// <returns>Return true or false if is not an admin.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_is_admin m_is_admin;
        /// <summary>
        /// Find player from current unique biped s_ident.
        /// </summary>
        /// <param name="bipedTag">Unique biped s_ident</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if unable to find player using given current biped.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_by_biped_tag_current m_get_by_biped_tag_current;
        /// <summary>
        /// Find player from current unique biped s_ident.
        /// </summary>
        /// <param name="bipedTag">Unique biped s_ident</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if unable to find player using given previous biped.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_by_biped_tag_current m_get_by_biped_tag_previous;
        /// <summary>
        /// To send a message through chat procotol to all players.
        /// </summary>
        /// <param name="formatMsg">See MSG_FORMAT for detail.</param>
        /// <param name="Msg">A message or predefined message.</param>
        /// <param name="argTotal">Total arguments in argList.</param>
        /// <param name="argList">To fill in the blank in a pre-defined message.</param>
        /// <returns>Return true or false if unable to send a message.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_send_custom_message_broadcast m_send_custom_message_broadcast;
        /// <summary>
        /// Force player to change team with optional to kill player if needed.
        /// </summary>
        /// <param name="plI">PlayerInfo</param>
        /// <param name="new_team">New team to assign.</param>
        /// <param name="forcekill">Force kill player if needed.</param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_change_team m_change_team;
        /// <summary>
        /// To apply camouflage duration on specific player.
        /// </summary>
        /// <param name="plI">PlayerInfo</param>
        /// <param name="duration">In seconds format.</param>
        /// <returns>Does not return any value. (This may will be change in future.)</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_apply_camo m_apply_camo;

        /// <summary>
        /// Ban player from host server.
        /// </summary>
        /// <param name="plEx">Player to ban.</param>
        /// <param name="gmtm">Time/date to expire ban.</param>
        /// <returns>Return true or false unable to ban player, -1 for invalid argument.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_ban_player m_ban_player;
        /// <summary>
        /// Ban CD hash key from host server.
        /// </summary>
        /// <param name="CDHash">CD hash key to ban. (Must have 33 characters allocate to copy, 33th is to null termate.)</param>
        /// <param name="gmtm">Time/date to expire ban.</param>
        /// <returns>Return true or false unable to ban CD hash key, -1 for invalid argument.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_ban_CD_key m_ban_CD_key;
        /// <summary>
        /// Ban IP Address from host server.
        /// </summary>
        /// <param name="IP_Address">IP Address to ban.. (Must have 16 characters allocate to copy.)</param>
        /// <param name="gmtm">Time/date to expire ban.</param>
        /// <returns>Return true or false unable to ban IP Address, -1 for invalid argument.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_ban_ip m_ban_ip;
        /// <summary>
        /// Get ID from banned IP Address.
        /// </summary>
        /// <param name="IP_Address">Banned IP Address. (Maximum is 16 characters long.)</param>
        /// <returns>Return ID of banned IP Address.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_ban_ip_get_id m_ban_ip_get_id;
        /// <summary>
        /// Get ID from banned CD hash key.
        /// </summary>
        /// <param name="CDHash">Banned CD hash key. (Must have 33 characters, 33th is to null termate.)</param>
        /// <returns>Return ID of banned IP Address.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_ban_CD_key_get_id m_ban_CD_key_get_id;
        /// <summary>
        /// To expire a ban from banned list.
        /// </summary>
        /// <param name="ID">Obtained ID from either banned IP Address or CD hash key.</param>
        /// <returns>Return true or false if unable to unban ID.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_unban_id m_unban_id;
        /// <summary>
        /// Get IP address, excluded port number, from machine slot.
        /// </summary>
        /// <param name="mS">machine slot</param>
        /// <param name="m_ip">IP address, excluded port number</param>
        /// <returns>Return true or false if unable get IP address.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_ip m_get_ip;
        /// <summary>
        /// Get port number, excluded IP address, from machine slot.
        /// </summary>
        /// <param name="mS">machine slot</param>
        /// <param name="m_port">Port number, excluded IP address</param>
        /// <returns>Return true or false if unable get port.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_port m_get_port;
        /// <summary>
        /// Get CD hash from machine slot.
        /// </summary>
        /// <param name="mS">machine slot</param>
        /// <param name="CDHash">CD hash key. (Must have 33 characters allocated to copy, 33th is a null termated.)</param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_CD_hash m_get_CD_hash;
        /// <summary>
        /// Find a match of player(s) from regex expression search.
        /// </summary>
        /// <param name="regexSearch">To find a matching player(s).</param>
        /// <param name="plMatch">List of matched players from search.</param>
        /// <param name="plOwner">Optional, owner of player execution usually.</param>
        /// <returns>Return total count of matched player(s).</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_str_to_player_list m_get_str_to_player_list;

        //Simple & easier user-defined conversion + checker for null.
        public IPlayer(IPlayerPtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (IPlayer)Marshal.PtrToStructure(data.ptr, typeof(IPlayer));
            else
                this = new IPlayer();
        }
        public static implicit operator IPlayer(IPlayerPtr data) {
            return new IPlayer(data);
        }
        public bool isNotNull() {
            return this.m_get_m_index != null;
        }
    }
    public partial struct Interface {
        /// <summary>
        /// Returns a IPlayer class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IPlayer class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#12", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern IPlayerPtr getIPlayer([In] uint uniqueHash);
    }
#endif
}
