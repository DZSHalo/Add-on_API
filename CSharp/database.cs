#if EXT_IDATABASE || EXT_IDATABASESTATEMENT
using System;
using System.Runtime.InteropServices;
using System.Data.Odbc;

namespace Addon_API {
#if EXT_IDATABASE
    public struct IDatabasePtr {
        public IntPtr ptr;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct IDatabase {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_connect([In, MarshalAs(UnmanagedType.LPWStr)] string ConnectionStr, [In] int Option, [In] IntPtr Param, [In] int ParamLen);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_connect_mdb([In, MarshalAs(UnmanagedType.LPWStr)] string MDBPath, [In, MarshalAs(UnmanagedType.LPWStr)] string User, [In, MarshalAs(UnmanagedType.LPWStr)] string Pass, [In, MarshalAs(UnmanagedType.I1)] bool Exclusive);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_disconnect();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int d_status();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_check();
        /// <summary>
        /// To connect a supported database.
        /// </summary>
        /// <param name="ConnectionStr">See https://msdn.microsoft.com/en-us/library/ms715433.aspx for details.</param>
        /// <param name="Option">See Attribute at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        /// <param name="Param">See ValuePtr at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        /// <param name="ParamLen">See StringLength at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        /// <returns>Return true or false.</returns>
        public d_connect m_connect;
        /// <summary>
        /// To connect local MDB file database. (Please note this function call to m_connect function.)
        /// </summary>
        /// <param name="MDBPath">Path to local MDB file database.</param>
        /// <param name="User">Username if required to connect.</param>
        /// <param name="Pass">Password if required to connect.</param>
        /// <param name="Exclusive">True to restrict access to database or false to share database.</param>
        /// <returns>Return true or false.</returns>
        public d_connect_mdb m_connect_mdb;
        /// <summary>
        /// To disconnect current active database.
        /// </summary>
        /// <returns>Does not return any value.</returns>
        public d_disconnect m_disconnect;
        /// <summary>
        /// To verify if active database is still connected.
        /// </summary>
        /// <returns>Return false if connection is dead from last attempt query or other value.</returns>
        public d_status m_status;
        /// <summary>
        /// To check an active database has up-to-date tables, if not it will automate update the tables.
        /// </summary>
        /// <returns>Does not return any value.</returns>
        public d_check m_check;

        //Simple & easier user-defined conversion + checker for null.
        public IDatabase(IDatabasePtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (IDatabase)Marshal.PtrToStructure(data.ptr, typeof(IDatabase));
            else
                this = new IDatabase();
        }
        public static implicit operator IDatabase(IDatabasePtr data) {
            return new IDatabase(data);
        }
        public bool isNotNull() {
            return m_connect != null;
        }
    }
#endif
#if EXT_IDATABASESTATEMENT
    public struct IDBStmtPtr {
        public IntPtr ptr;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct IDBStmt {

#error Due to C# is using Command class instead of Statement plus other issues, this is on hold for further review.

        //TODO: This is where I left off for IDBStmt interface.

        //Simple & easier user-defined conversion + checker for null.
        public IDBStmt(IDBStmtPtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (IDBStmt)Marshal.PtrToStructure(data.ptr, typeof(IDBStmt));
            else
                this = new IDBStmt();
        }
        public static implicit operator IDBStmt(IDBStmtPtr data) {
            return new IDBStmt(data);
        }
        public bool isNotNull() {
            return ? != null;
        }
    }

#endif

    public partial struct Interface {
#if EXT_IDATABASE
        /// <summary>
        /// Returns a IDatabase class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IDatabase class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#15", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern IDatabasePtr getIDatabase([In] uint uniqueHash);
#endif
#if EXT_IDATABASESTATEMENT
        /// <summary>
        /// Returns a IDBStmt class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IDBStmt class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#16", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern IDBStmtPtr getIDBStmt([In] uint uniqueHash);
#endif
    }
}
#endif
