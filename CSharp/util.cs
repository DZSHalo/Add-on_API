using System;
#if EXT_IUTIL
using System.Text;
#endif
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct tm {
    public int tm_sec;     /* seconds after the minute - [0,59] */
    public int tm_min;     /* minutes after the hour - [0,59] */
    public int tm_hour;    /* hours since midnight - [0,23] */
    public int tm_mday;    /* day of the month - [1,31] */
    public int tm_mon;     /* months since January - [0,11] */
    public int tm_year;    /* years since 1900 */
    public int tm_wday;    /* days since Sunday - [0,6] */
    public int tm_yday;    /* days since January 1 - [0,365] */
    public int tm_isdst;   /* daylight savings time flag */
}
//Necessary for easier management.
public struct IntPtrValue {
    public IntPtr ptr;
    public int value { get { return Marshal.ReadInt32(ptr); } set { Marshal.WriteInt32(ptr, value); } }
}
public struct UIntPtrValue {
    public IntPtr ptr;
    public uint value { get { return (uint)Marshal.PtrToStructure(ptr, typeof(uint)); } set { Marshal.StructureToPtr(value, ptr, true); } }
}

namespace Addon_API {
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    public struct ArgContainerVars {
        [MarshalAs(UnmanagedType.LPWStr, SizeConst=256)]
        public string arg;
        uint arg_len;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
        public string[] args;
        public uint argc;
    }
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    public struct ArgContainerAPI {
        [DllImport("H-Ext.dll", EntryPoint = "#30", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern void ArgContainerVars_Constructor([In, Out] ref ArgContainerVars vars);
        [DllImport("H-Ext.dll", EntryPoint = "#31", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern void ArgContainerVars_Deconstructor([In, Out] ref ArgContainerVars vars);
        [DllImport("H-Ext.dll", EntryPoint = "#33", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern void ArgContainerVars_Set([In, Out] ref ArgContainerVars vars, [In, MarshalAs(UnmanagedType.LPWStr)] string args);
        [DllImport("H-Ext.dll", EntryPoint = "#34", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern void ArgContainerVars_Set_N([In, Out] ref ArgContainerVars vars, [In, MarshalAs(UnmanagedType.LPWStr)] string args, int numArrayLink);
        [DllImport("H-Ext.dll", EntryPoint = "#32", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern void ArgContainerVars_Copy([In, Out] ref ArgContainerVars vars, [In, Out] ref ArgContainerVars copy);
        [DllImport("H-Ext.dll", EntryPoint = "#35", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPWStr)]
        [ComVisible(true)]
        public static extern string ArgContainerVars_At([In, Out] ref ArgContainerVars vars, [In] uint i);
    }
    public class ArgContainer : IDisposable {
        public ArgContainerVars vars;
        public uint argc {
            get { return vars.argc;}
            private set {}
        }
        public ArgContainer() {
            ArgContainerAPI.ArgContainerVars_Constructor(ref vars);
        }
        public ArgContainer([In, MarshalAs(UnmanagedType.LPWStr)] string arg) {
            ArgContainerAPI.ArgContainerVars_Set(ref vars, arg);
        }
        public ArgContainer([In, MarshalAs(UnmanagedType.LPWStr)] string arg, [In] int numArrayLink) {
            ArgContainerAPI.ArgContainerVars_Set_N(ref vars, arg, numArrayLink);
        }
        public string this[uint i] {
            get {
                return ArgContainerAPI.ArgContainerVars_At(ref vars, i);
            }
            private set { }
        }
        public ArgContainer(ArgContainer copy) {
            ArgContainerAPI.ArgContainerVars_Copy(ref vars, ref copy.vars);
        }
        public ArgContainer(ArgContainerVars copy) {
            ArgContainerAPI.ArgContainerVars_Copy(ref vars, ref vars);
        }
        ~ArgContainer() {
            Dispose(false);
        }
        protected void Dispose(bool disposing) {
            if (vars.argc != 0) {
                ArgContainerAPI.ArgContainerVars_Deconstructor(ref vars);
                vars.argc = 0;
            }
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public static implicit operator ArgContainerVars(ArgContainer arg) {
            return arg.vars;
        }
        public static implicit operator ArgContainer(ArgContainerVars vars) {
            return new ArgContainer(vars);
        }
    }
#if EXT_IUTIL
    public struct IUtilPtr {
        public IntPtr ptr;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct IUtil {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate IntPtr d_allocMem([In] uint Size);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_freeMem([In] IntPtr Address);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_toCharW([In, MarshalAs(UnmanagedType.LPStr)] string charA, int len, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder charW);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_toCharA([In, MarshalAs(UnmanagedType.LPWStr)] string charW, int len, [In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder charA);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_strToBooleanA([In, MarshalAs(UnmanagedType.LPStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_strToBooleanW([In, MarshalAs(UnmanagedType.LPWStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_color_team_index d_strToTeamA([In, MarshalAs(UnmanagedType.LPStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_color_team_index d_strToTeamW([In, MarshalAs(UnmanagedType.LPWStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_replaceA([In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_replaceW([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_replaceUndoA([In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_replaceUndoW([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isNumberA([In, MarshalAs(UnmanagedType.LPStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isNumberW([In, MarshalAs(UnmanagedType.LPWStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isHashA([In, MarshalAs(UnmanagedType.LPStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isHashW([In, MarshalAs(UnmanagedType.LPWStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_shiftStrA([In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder regStr, [In] int len, [In] int pos, [In] int lenShift, [In, MarshalAs(UnmanagedType.I1)] bool leftRight);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate e_boolean d_shiftStrW([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder regStr, [In] int len, [In] int pos, [In] int lenShift, [In, MarshalAs(UnmanagedType.I1)] bool leftRight);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_regexReplaceW([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder regStr, [In, MarshalAs(UnmanagedType.I1)] bool isDB);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_regexMatchW([In, MarshalAs(UnmanagedType.LPWStr)] string srcStr, [In, MarshalAs(UnmanagedType.LPWStr)] string regex);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_regexiMatchW([In, MarshalAs(UnmanagedType.LPWStr)] string srcStr, [In, MarshalAs(UnmanagedType.LPWStr)] string regex);
        [Obsolete("Do not use FormatVarArgsListA function, will not work as expected.")]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool d_FormatVarArgsListA([In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder writeTo, [In, MarshalAs(UnmanagedType.LPStr)] string _Format, ArgIterator ArgList);
        [Obsolete("Do not use FormatVarArgsListW function, will not work as expected.")]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool d_FormatVarArgsListW([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder writeTo, [In, MarshalAs(UnmanagedType.LPWStr)] string _Format, ArgIterator ArgList);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_findSubStrFirstA([In, MarshalAs(UnmanagedType.LPStr)] string src, [In, MarshalAs(UnmanagedType.LPStr)] string find);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_findSubStrFirstW([In, MarshalAs(UnmanagedType.LPWStr)] string src, [In, MarshalAs(UnmanagedType.LPWStr)] string find);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isLettersA([In, MarshalAs(UnmanagedType.LPStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isLettersW([In, MarshalAs(UnmanagedType.LPWStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isFloatA([In, MarshalAs(UnmanagedType.LPStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isFloatW([In, MarshalAs(UnmanagedType.LPWStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isDoubleA([In, MarshalAs(UnmanagedType.LPStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isDoubleW([In, MarshalAs(UnmanagedType.LPWStr)] string str);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_strcatW([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder dest, uint len, [In, MarshalAs(UnmanagedType.LPWStr)] string src);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_strcatA([In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder dest, uint len, [In, MarshalAs(UnmanagedType.LPStr)] string src);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_str_to_wstr([In, MarshalAs(UnmanagedType.LPStr)] string str, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder wstr);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_strcmpW([In, MarshalAs(UnmanagedType.LPWStr)] string str1, [In, MarshalAs(UnmanagedType.LPWStr)] string str2);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_strcmpA([In, MarshalAs(UnmanagedType.LPStr)] string str1, [In, MarshalAs(UnmanagedType.LPStr)] string str2);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_stricmpW([In, MarshalAs(UnmanagedType.LPWStr)] string str1, [In, MarshalAs(UnmanagedType.LPWStr)] string str2);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_stricmpA([In, MarshalAs(UnmanagedType.LPStr)] string str1, [In, MarshalAs(UnmanagedType.LPStr)] string str2);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isDirExist([In, MarshalAs(UnmanagedType.LPWStr)] string str1, ref uint errorCode);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_isFileExist([In, MarshalAs(UnmanagedType.LPWStr)] string str1, ref uint errorCode);
        [Obsolete("Do not use FormatVarArgsListA function, will not work as expected.")]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool d_FormatVarArgsA([In, Out, MarshalAs(UnmanagedType.LPStr)] StringBuilder writeTo, [In, MarshalAs(UnmanagedType.LPStr)] string _Format /*, ...*/);
        [Obsolete("Do not use FormatVarArgsListW function, will not work as expected.")]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        private delegate bool d_FormatVarArgsW([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder writeTo, [In, MarshalAs(UnmanagedType.LPWStr)] string _Format /*, ...*/);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_formatVariantW([In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder outputStr, [In] uint maxOutput, [In, MarshalAs(UnmanagedType.LPWStr)] string _Format, uint argTotal, [In, MarshalAs(UnmanagedType.LPArray)] params object[] argList);

        /// <summary>
        /// Allocate memory.
        /// </summary>
        /// <param name="Size">The size of allocate memory need to be used.</param>
        /// <returns>Return allocate memory.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_allocMem m_allocMem;
        /// <summary>
        /// Free memory from allocate memory.
        /// </summary>
        /// <param name="Address">Pointer of an allocate memory to be free from.</param>
        /// <returns>No return.</returns>
        public d_freeMem m_freeMem;
        /// <summary>
        /// Convert a string to wide string.
        /// </summary>
        /// <param name="charA">String</param>
        /// <param name="len">Total length to convert from string.</param>
        /// <param name="charW">Buffered wide string</param>
        /// <returns>No return value.</returns>
        public d_toCharW m_toCharW;
        /// <summary>
        /// Convert a wide string to string.
        /// </summary>
        /// <param name="charW">Wide string</param>
        /// <param name="len">Total length to convert from wide string.</param>
        /// <param name="charA">Buffered string</param>
        /// <returns>No return value.</returns>
        public d_toCharA m_toCharA;
        /// <summary>
        /// Translate a string into boolean.
        /// </summary>
        /// <param name="str">String to translate from.</param>
        /// <returns>Return -1 if string doesn't have a translation to boolean.</returns>
        public d_strToBooleanA m_strToBooleanA;
        /// <summary>
        /// Translate a wide string into boolean.
        /// </summary>
        /// <param name="str">Wide string to translate from.</param>
        /// <returns>Return -1 if string doesn't have a translation to boolean.</returns>
        public d_strToBooleanW m_strToBooleanW;
        /// <summary>
        /// Translate a string into team index.
        /// </summary>
        /// <param name="str">String to translate from.</param>
        /// <returns>Return -1 if string doesn't have a translation to team index.</returns>
        public d_strToTeamA m_strToTeamA;
        /// <summary>
        /// Translate a wide string into team index.
        /// </summary>
        /// <param name="str">Wide string to translate from.</param>
        /// <returns>Return -1 if string doesn't have a translation to team index.</returns>
        public d_strToTeamW m_strToTeamW;
        /// <summary>
        /// Format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to format escape characters if any.</param>
        /// <returns>No return value.</returns>
        public d_replaceA m_replaceA;
        /// <summary>
        /// Format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to format escape characters if any.</param>
        /// <returns>No return value.</returns>
        public d_replaceW m_replaceW;
        /// <summary>
        /// Undo format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to undo format escape characters if any.</param>
        /// <returns>No return value.</returns>
        public d_replaceUndoA m_replaceUndoA;
        /// <summary>
        /// Undo format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to undo format escape characters if any.</param>
        /// <returns>No return value.</returns>
        public d_replaceUndoW m_replaceUndoW;
        /// <summary>
        /// Verify if whole string contain digits.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>Return true if valid.</returns>
        public d_isNumberA m_isNumberA;
        /// <summary>
        /// Verify if whole wide string contain digits.
        /// </summary>
        /// <param name="str">Wide string to check.</param>
        /// <returns>Return true if valid.</returns>
        public d_isNumberW m_isNumberW;
        /// <summary>
        /// Verify if whole string contain characters & digits.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>Return true if valid.</returns>
        public d_isHashA m_isHashA;
        /// <summary>
        /// Verify if whole wide string contain characters & digits.
        /// </summary>
        /// <param name="str">Wide string to check.</param>
        /// <returns>Return true if valid.</returns>
        public d_isHashW m_isHashW;
        /// <summary>
        /// Move partial of string to left or right.
        /// </summary>
        /// <param name="regStr">String to be shift.</param>
        /// <param name="len">Length of string to be move.</param>
        /// <param name="pos">Position of the string to be shift.</param>
        /// <param name="lenShift">Amount of length to shift left or right.</param>
        /// <param name="leftRight">True for shift to right and false for shift to left.</param>
        /// <returns>Return true for success, failed if one or more argument is invalid.</returns>
        public d_shiftStrA m_shiftStrA;
        /// <summary>
        /// Move partial of wide string to left or right.
        /// </summary>
        /// <param name="regStr">Wide string to be shift.</param>
        /// <param name="len">Length of wide string to be move.</param>
        /// <param name="pos">Position of the wide string to be shift.</param>
        /// <param name="lenShift">Amount of length to shift left or right.</param>
        /// <param name="leftRight">True for shift to right and false for shift to left.</param>
        /// <returns>Return true for success, failed if one or more argument is invalid.</returns>
        public d_shiftStrW m_shiftStrW;
        /// <summary>
        /// Format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to format escape characters if any.</param>
        /// <param name="isDB">True if goig to use escape characters in database query.</param>
        /// <returns>No return value.</returns>
        public d_regexReplaceW m_regexReplaceW;
        /// <summary>
        /// Find a regular expression string against source string to be a match.
        /// </summary>
        /// <param name="srcStr">Source string</param>
        /// <param name="regex">Regular expression string</param>
        /// <returns>Return true if is a match.</returns>
        public d_regexMatchW m_regexMatchW;
        /// <summary>
        /// Find a regular expression string against source string to be a match.
        /// </summary>
        /// <param name="srcStr">Source string</param>
        /// <param name="regex">Regular expression string</param>
        /// <returns>Return true if is a match.</returns>
        public d_regexiMatchW m_regexiMatchW;

        /// <summary>
        /// (DO NOT USE!) Format variable arguments list into given prefix string.
        /// </summary>
        /// <param name="writeTo">Output string</param>
        /// <param name="_Format">Format message string</param>
        /// <param name="ArgList">Variable arguments list</param>
        /// <returns>Return true or false for format completion.</returns>
        private d_FormatVarArgsListA m_FormatVarArgsListA;
        /// <summary>
        /// (DO NOT USE!) Format variable arguments list into given prefix string.
        /// </summary>
        /// <param name="writeTo">Output string</param>
        /// <param name="_Format">Format message string</param>
        /// <param name="ArgList">Variable arguments list</param>
        /// <returns>Return true or false for format completion.</returns>
        private d_FormatVarArgsListW m_FormatVarArgsListW;
        /// <summary>
        /// Compare beginning of case-senitive string against another string.
        /// </summary>
        /// <param name="src">Source string compare against.</param>
        /// <param name="find">Find string to use for comparison.</param>
        /// <returns>Only return true if is a match.</returns>
        public d_findSubStrFirstA m_findSubStrFirstA;
        /// <summary>
        /// Compare beginning of case-senitive string against another string.
        /// </summary>
        /// <param name="src">Source string compare against.</param>
        /// <param name="find">Find string to use for comparison.</param>
        /// <returns>Only return true if is a match.</returns>
        public d_findSubStrFirstW m_findSubStrFirstW;

        /// <summary>
        /// Test if string contains a letters or not.
        /// </summary>
        /// <param name="str">String to test if is a letters.</param>
        /// <returns>Return true if is letters.</returns>
        public d_isLettersA m_isLettersA;
        /// <summary>
        /// Test if string contains a letters or not.
        /// </summary>
        /// <param name="str">String to test if is a letters.</param>
        /// <returns>Return true if is letters.</returns>
        public d_isLettersW m_isLettersW;

        /// <summary>
        /// Test if string contains a float or not.
        /// </summary>
        /// <param name="str">String to test if is a float.</param>
        /// <returns>Return true if is a float.</returns>
        public d_isFloatA m_isFloatA;
        /// <summary>
        /// Test if string contains a float or not.
        /// </summary>
        /// <param name="str">String to test if is a float.</param>
        /// <returns>Return true if is a float.</returns>
        public d_isFloatW m_isFloatW;
        /// <summary>
        /// Test if string contains a double or not.
        /// </summary>
        /// <param name="str">String to test if is a double.</param>
        /// <returns>Return true if is a double.</returns>
        public d_isDoubleA m_isDoubleA;
        /// <summary>
        /// Test if string contains a double or not.
        /// </summary>
        /// <param name="str">String to test if is a double.</param>
        /// <returns>Return true if is a double.</returns>
        public d_isDoubleW m_isDoubleW;
        /// <summary>
        /// Append an existing string with new string.
        /// </summary>
        /// <param name="dest">Destination to write an existing string.</param>
        /// <param name="len">Maximum size of an dest string.</param>
        /// <param name="src">New string to copy from.</param>
        /// <returns>Return 1 every time.</returns>
        public d_strcatW m_strcatW;
        /// <summary>
        /// Append an existing string with new string.
        /// </summary>
        /// <param name="dest">Destination to write an existing string.</param>
        /// <param name="len">Maximum size of an dest string.</param>
        /// <param name="src">New string to copy from.</param>
        /// <returns>Return 1 every time.</returns>
        public d_strcatA m_strcatA;
        /// <summary>
        /// Convert a string to wide string.
        /// </summary>
        /// <param name="str">String</param>
        /// <param name="wstr">Buffered wide string./param>
        /// <returns>No return value.</returns>
        public d_str_to_wstr m_str_to_wstr;
        /// <summary>
        /// Case-senitive string to compare against another string..
        /// </summary>
        /// <param name="str1">String #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        public d_strcmpW m_strcmpW;
        /// <summary>
        /// Case-senitive string to compare against another string..
        /// </summary>
        /// <param name="str1">String #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        public d_strcmpA m_strcmpA;
        /// <summary>
        /// Case-insenitive string to compare against another string.
        /// </summary>
        /// <param name="str1">String #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        public d_stricmpW m_stricmpW;
        /// <summary>
        /// Case-insenitive string to compare against another string.
        /// </summary>
        /// <param name="str1">String #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        public d_stricmpA m_stricmpA;
        /// <summary>
        /// Check if a directory exist.
        /// </summary>
        /// <param name="pathStr">Must have directory name.</param>
        /// <param name="errorCode">Given error code if failed.</param>
        /// <returns>Return true if directory exist, false with given errorCode.</returns>
        public d_isDirExist m_isDirExist;
        /// <summary>
        /// Check if a file exist.
        /// </summary>
        /// <param name="pathStr">Must have directory (optional) and file name.</param>
        /// <param name="errorCode">Given error code if failed.</param>
        /// <returns>Return true if file exist, false with given errorCode.</returns>
        public d_isFileExist m_isFileExist;
        /// <summary>
        /// (DO NOT USE!) Format variable arguments into given prefix string.
        /// </summary>
        /// <param name="writeTo">Output string</param>
        /// <param name="_Format">Format message string</param>
        /// <param name="...">Variable arguments</param>
        /// <returns>Return true or false for format completion.</returns>
        private d_FormatVarArgsA m_FormatVarArgsA;
        /// <summary>
        /// (DO NOT USE!) Format variable arguments into given prefix string.
        /// </summary>
        /// <param name="writeTo">Output string</param>
        /// <param name="_Format">Format message string</param>
        /// <param name="...">Variable arguments</param>
        /// <returns>Return true or false for format completion.</returns>
        private d_FormatVarArgsW m_FormatVarArgsW;
        /// <summary>
        /// Format variant arguments into a custom prefix string.
        /// </summary>
        /// <param name="outputStr">Output string</param>
        /// <param name="maxOutput">Maximum size of output string.</param>
        /// <param name="_Format">Format custom message string</param>
        /// <param name="argTotal">Must be equivalent to argList's total of arrays.</param>
        /// <param name="argList">Variant arguments in array format.</param>
        /// <returns>Return true or false for format completion.</returns>
        public d_formatVariantW m_formatVariantW;

        //Simple & easier user-defined conversion + checker for null.
        public IUtil(IUtilPtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (IUtil)Marshal.PtrToStructure(data.ptr, typeof(IUtil));
            else
                this = new IUtil();
        }
        public static implicit operator IUtil(IUtilPtr data) {
            return new IUtil(data);
        }
        public bool isNotNull() {
            return m_allocMem != null;
        }
    }

    public partial struct Global {
        public static IUtil pIUtil;
    }

    public partial struct Interface {
        /// <summary>
        /// Returns a IUtil class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IUtil class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#19", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern IUtilPtr getIUtil([In] uint uniqueHash);
    }
#endif
}
