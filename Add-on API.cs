using System;
using System.Runtime.InteropServices;


public class boolOption {
    [MarshalAs(UnmanagedType.I1)]
    public bool boolean;
}

namespace Addon_API {

    public enum EAO_RETURN: int {
        //ONETIMEUPDATE = 2 //This is not compatiable with unmanaged application.
        OVERRIDE = 1, //This is the only compatiable with unmanaged application.
        //CONTINUE = 0, //This is not compatiable with unmanaged application.
        FAIL = -1, //This is not compatiable with unmanaged application. However it will create an exception when attempt to unload it.
    }
    public enum CMD_RETURN: int {
        FAIL = -1,
        NOMATCH = 0,
        SUCC = 1,
        SUCCDELAY = 2
    }
    public enum e_boolean : int {
        INVALID = -2,
        FAIL = -1,
        FALSE = 0,
        TRUE = 1
    }

#if EXT_IUTIL
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate CMD_RETURN CmdFunc([In] PlayerInfo plI, [In, Out] ref ArgContainerVars arg, [In] MSG_PROTOCOL protocolMsg, [In] uint idTimer, [In] boolOption showChat);
#endif
    [StructLayout(LayoutKind.Sequential, CharSet =CharSet.Unicode, Pack = 1)]
    public struct addon_section_names {
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst=24)]
        public string sect_name1;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst=24)]
        public string sect_name2;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst=24)]
        public string sect_name3;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst=24)]
        public string sect_name4;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst=24)]
        public string sect_name5;
    };
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct addon_info {
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string name;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string version;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string author;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 255)]
        public string description;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 24)]
        public string config_folder;
        public addon_section_names sectors;
    };
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct addon_version {
        ushort size;            //Used by sizeof(versionEAO);
        ushort requiredAPI;     //API requirement revision (Including command functions)
        ushort general;         //General revision specifically for events in Halo.
        ushort version;         //addon_version revision
        ushort pIHaloEngine;    //Halo Engine interface revision
        ushort pIObject;        //Object interface revision
        ushort pIPlayer;        //Player interface revision
        ushort pIAdmin;         //Admin interface revision
        ushort pICommand;       //Command interface revision
        ushort pIDatabase;      //Database interface revision
        ushort pIDBStmt;        //Database Statement interface revision
        ushort pICIniFile;      //CiniFile interface revision
        ushort pITimer;         //Timer interface revision
        ushort pIUtil;          //Util interface revision
        ushort pINetwork;       //Network interface revision - reserved (DO NOT USE!)
        ushort pISound;         //Sound interface revision - reserved (DO NOT USE!)
        ushort pIDirectX9;      //DirectX9 interface revision - reserved (DO NOT USE!)
        ushort reserved1;       //reserved
        ushort reserved2;       //reserved
        ushort reserved3;       //reserved
        ushort hkDatabase;      //Database hook revision
        ushort hkTimer;         //Timer hook revision
        ushort hkExternal;      //External account revision - reserved (DO NOT USE!)
        ushort reserved4;       //reserved
        ushort reserved5;       //reserved
        ushort reserved6;       //reserved
        ushort reserved7;       //reserved
        ushort reserved8;       //reserved
    };

#if EXT_ITIMER
    public struct ITimerPtr {
        public IntPtr ptr;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ITimer {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl), System.Security.SuppressUnmanagedCodeSecurity]
        public delegate uint d_add([In] uint uniqueHash, [In] PlayerInfoPtr plI, [In] uint execTime);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl), System.Security.SuppressUnmanagedCodeSecurity]
        public delegate void d_delete([In] uint uniqueHash, [In] uint id);
        /// <summary>
        /// Register a timer event delay.
        /// </summary>
        /// <param name="hash">Add-on unique ID.</param>
        /// <param name="plI">Bind to specific player or use null for general.</param>
        /// <param name="execTime">Amount of ticks later to execute a timer event. (1 tick = 1/30 second)</param>
        /// <returns>Return ID of timer event.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_add m_add;
        /// <summary>
        /// Remove a timer event.
        /// </summary>
        /// <param name="hash">Add-on unique ID.</param>
        /// <param name="id">Can be used only with registered ID number bind to specific Add-on.</param>
        /// <returns>Return true or false if unable to reload Add-on.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_delete m_delete;

        //Simple & easier user-defined conversion + checker for null.
        public ITimer(ITimerPtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (ITimer)Marshal.PtrToStructure(data.ptr, typeof(ITimer));
            else
                this = new ITimer();
        }
        public static implicit operator ITimer(ITimerPtr data) {
            return new ITimer(data);
        }
        public bool isNotNull() {
            return this.m_add != null;
        }
    }
    public partial struct Interface {
        /// <summary>
        /// Returns a ITimer class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of ITimer class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#18", CallingConvention = CallingConvention.Cdecl), System.Security.SuppressUnmanagedCodeSecurity]
        [ComVisible(true)]
        public static extern ITimerPtr getITimer([In] uint uniqueHash);
    }
#endif
}