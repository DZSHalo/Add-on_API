#if EXT_ICOMMAND
using System;
using System.Runtime.InteropServices;

#if !EXT_IUTIL
#error EXT_IUTIL is required to be use with ICommand interface.
#endif

namespace Addon_API {
    public struct ICommandPtr {
        public IntPtr ptr;
    }
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct ICommand {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_add([In] uint hash, [In, MarshalAs(UnmanagedType.LPWStr)] string command, [In] CmdFunc func, [In, MarshalAs(UnmanagedType.LPWStr)] string section, [In] ushort min, [In] ushort max, [In, MarshalAs(UnmanagedType.I1)] bool allowOverride, [In] HEXT.GAME_MODE_S mode);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_delete([In] uint hash, [In] CmdFunc func, [In, MarshalAs(UnmanagedType.LPWStr)] string command);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_reload_level([In] uint hash);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_alias_add([In, MarshalAs(UnmanagedType.LPWStr)] string command, [In, MarshalAs(UnmanagedType.LPWStr)] string alias);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_alias_delete([In, MarshalAs(UnmanagedType.LPWStr)] string command, [In, MarshalAs(UnmanagedType.LPWStr)] string alias);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_load_from_file([In] uint hash, [In, MarshalAs(UnmanagedType.LPWStr)] string fileName, PlayerInfo plI, MSG_PROTOCOL protocolMsg);
        /// <summary>
        /// To add nonexisting <paramref name="command"/> bind to <paramref name="func"/> into command system and return true or false.
        /// </summary>
        /// <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        /// <param name="command">A new command name, or override a command once if permitted, into command system.</param>
        /// <param name="func">A new function or existing function within same add-on only.</param>
        /// <param name="section">Section where command belongs to.</param>
        /// <param name="min">Minimum requirement to able allow command execute.</param>
        /// <param name="max">Maximum requirement to able allow command execute.</param>
        /// <param name="allowOverride">An option to either allow or forbidden another add-on or current add-on override a command.</param>
        /// <param name="mode">To permit a command executed in either single player, multiplayer, hosting a game, or all. See below GAME_MODE_S struct for pre-defined availability.</param>
        /// <returns>Only return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_add m_add;
        /// <summary>
        /// To delete a <paramref name="command"/> which is binded to <paramref name="func"/> and return true or false.
        /// </summary>
        /// <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        /// <param name="func">A function currently binded to a command.</param>
        /// <param name="command">A command currently binded to a function.</param>
        /// <returns>Only return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_delete m_delete;
        /// <summary>
        /// To load or reload authorized add-on's commands level from commands.ini file.
        /// </summary>
        /// <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        /// <returns>Only return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_reload_level m_reload_level;
        /// <summary>
        /// To add an <paramref name="alias"/> from <paramref name="command"/> and return true or false.
        /// </summary>
        /// <param name="command">Command name currently exist in command system.</param>
        /// <param name="alias">An alias command name which is not binded to a command.</param>
        /// <returns>Only return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_alias_add m_alias_add;
        /// <summary>
        /// To delete an <paramref name="alias"/> from <paramref name="command"/> and return true or false.
        /// </summary>
        /// <param name="command">Command name currently exist in command system.</param>
        /// <param name="alias">An alias command name currently bind to a command.</param>
        /// <returns>Only return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_alias_delete m_alias_delete;
        /// <summary>
        /// To load a custom command(s) from <paramref name="fileName"/> and return true or false.
        /// </summary>
        /// <param name="hash">Valid owner Add-on hash and existing config_folder defined.</param>
        /// <param name="fileName">Custom file name to load and execute from.</param>
        /// <param name="plI">Bind user of this execution process.</param>
        /// <param name="protocolMsg">For output the message to binded user.</param>
        /// <returns>Only return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_load_from_file m_load_from_file;


        //Simple & easier user-defined conversion + checker for null.
        public ICommand(ICommandPtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (ICommand)Marshal.PtrToStructure(data.ptr, typeof(ICommand));
            else
                this = new ICommand();
        }
        public static implicit operator ICommand(ICommandPtr data) {
            return new ICommand(data);
        }
        public bool isNotNull() {
            return m_add != null;
        }
    }

    public partial struct Interface {
        /// <summary>
        /// Returns a ICommand class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IAdmin class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#14", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern ICommandPtr getICommand([In] uint uniqueHash);
    }
}
#endif
