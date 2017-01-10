#if EXT_IADMIN
using System;
using System.Runtime.InteropServices;

namespace Addon_API {
    public enum LOGIN_VALIDATION : int {
        INVALID = -1,
        FAIL = 0,
        OK = 1
    }
    public enum CMD_AUTH: int {
        NOT_FOUND = -2,
        OUT_OF_RANGE = -1,
        DENIED = 0,
        AUTHORIZED = 1
    }
    public struct IAdminPtr {
        public IntPtr ptr;
    }
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct IAdmin {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate CMD_AUTH d_is_authorized(ref PlayerInfo player, [In, MarshalAs(UnmanagedType.LPWStr)] string command, [In, Out] ref ArgContainerVars arg, [In, Out] ref CmdFunc func);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_is_username_exist([In, MarshalAs(UnmanagedType.LPWStr)] string username);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_add([In, MarshalAs(UnmanagedType.LPWStr)] string hashW, [In, MarshalAs(UnmanagedType.LPWStr)] string IP_Addr, [In, MarshalAs(UnmanagedType.LPWStr)] string IP_Port, [In, MarshalAs(UnmanagedType.LPWStr)] string username, [In, MarshalAs(UnmanagedType.LPWStr)] string password, [In] short level, [In, MarshalAs(UnmanagedType.I1)] bool remote, [In, MarshalAs(UnmanagedType.I1)] bool pass_force);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_delete([In, MarshalAs(UnmanagedType.LPWStr)] string username);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate LOGIN_VALIDATION d_login(ref PlayerInfo player, [In] MSG_PROTOCOL protocolMsg, [In, MarshalAs(UnmanagedType.LPWStr)] string username, [In, MarshalAs(UnmanagedType.LPWStr)] string password);
        /// <summary>
        /// To verify if <paramref name="player">player</paramref> is authorized to use <paramref name="command">command</paramref>.
        /// </summary>
        /// <param name="player">Required to input player.</param>
        /// <param name="command">Required to input command.</param>
        /// <param name="arg">Output the argument from command.</param>
        /// <param name="func">Output a function link to the command.</param>
        /// <returns>Only return true, false, and -1 if input is invalid.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_is_authorized m_is_authorized;
        /// <summary>
        /// To verify if <paramref name="username"/> exist in database and return true, false, or -1 for database is offline.
        /// </summary>
        /// <param name="username">Take unicode username to verify.</param>
        /// <returns>Only return true, false, and -1 if database is offline.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_is_username_exist m_is_username_exist;
        /// <summary>
        /// To add an admin to the database and return true, false, or -1 for database is offline.
        /// </summary>
        /// <param name="hashW">Maximum permitted is 32 characters.</param>
        /// <param name="IP_Addr">Maximum permitted is 15 characters.</param>
        /// <param name="IP_Port">Maximum permitted is 6 characters.</param>
        /// <param name="username">Maximum permitted is 24 characters.</param>
        /// <param name="password">No limitation on password for now.</param>
        /// <param name="level">Maximum permitted is 9999.</param>
        /// <param name="remote">To permit remote administrator access without need to use Halo game.</param>
        /// <param name="pass_force">Force administrator to change their password.</param>
        /// <returns>Only return true, false, and -1 if database is offline.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_add m_add;
        /// <summary>
        /// To remove <paramref name="username"/> from database and return true, false, or -1 for database is offline.
        /// </summary>
        /// <param name="username">Maximum permitted is 24 characters.</param>
        /// <returns>Only return true, false, and -1 if database is offline.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_delete m_delete;
        /// <summary>
        /// To login a <paramref name="player"/> as administrator from database verfication and return LOGIN_INVALID, LOGIN_FAIL, and LOGIN_OK.
        /// </summary>
        /// <param name="player">Take ingame or remote admin to verify.</param>
        /// <param name="chatRconRemote">To return a message back to player.</param>
        /// <param name="username">Maximum permitted is 24 characters.</param>
        /// <param name="password">No limitation on password for now.</param>
        /// <returns>Only return LOGIN_INVALID, LOGIN_FAIL, and LOGIN_OK.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_login m_login;

        //Simple & easier user-defined conversion + checker for null.
        public IAdmin(IAdminPtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (IAdmin)Marshal.PtrToStructure(data.ptr, typeof(IAdmin));
            else
                this = new IAdmin();
        }
        public static implicit operator IAdmin(IAdminPtr data) {
            return new IAdmin(data);
        }
        public bool isNotNull() {
            return m_is_authorized != null;
        }
    }

    public partial struct Interface {
        /// <summary>
        /// Returns a IAdmin class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IAdmin class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#13", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern IAdminPtr getIAdmin([In] uint uniqueHash);
    }
}
#endif
