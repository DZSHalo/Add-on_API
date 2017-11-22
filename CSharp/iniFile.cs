using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if EXT_ICINIFILE

namespace Addon_API {

    public struct ICIniFilePtr {
        public IntPtr ptr;
    }
    public struct ICIniFile {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_release(IntPtr self);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_close(IntPtr self);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_open_file(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string fileName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_create_file(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string fileName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_delete_file(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string fileName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_content(IntPtr self, [In, Out, MarshalAs(UnmanagedType.LPWStr)] ref string content, [In, Out] ref uint len);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_section_add(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_section_delete(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_section_exist(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_key_exist(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In, MarshalAs(UnmanagedType.LPWStr)] string keyName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_value_set(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In, MarshalAs(UnmanagedType.LPWStr)] string keyName, [In, MarshalAs(UnmanagedType.LPWStr)] string valueName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_value_get(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In, MarshalAs(UnmanagedType.LPWStr)] string keyName, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder valueName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_save(IntPtr self);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_load(IntPtr self);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_clear(IntPtr self);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_key_delete(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In, MarshalAs(UnmanagedType.LPWStr)] string keyName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_section_count(IntPtr self);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_section_index(IntPtr self, [In] uint index, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder sectionName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint d_key_count(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_key_index(IntPtr self, [In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In] uint index, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder keyName, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder valueName);

        /// <summary>
        /// To release ICIniFile, cannot be re-used after release.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Does not return a value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_release m_release;
        /// <summary>
        /// To close a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Does not return a value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_close m_close;
        /// <summary>
        /// To open a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="fileName">Name of a file to open.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_open_file m_open_file;
        /// <summary>
        /// To create a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="fileName">Name of a file to create.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_create_file m_create_file;
        /// <summary>
        /// To delete a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="fileName">Name of a file to delete.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_delete_file m_delete_file;
        /// <summary>
        /// To obtain raw content from an opened file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="content">Raw content within a file.</param>
        /// <param name="len">Length of the content.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_content m_content;
        /// <summary>
        /// To add a section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of a section to add. Maximum characters is INIFILESECTIONMAX.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_section_add m_section_add;
        /// <summary>
        /// To delete a section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of a section to delete. Maximum characters is INIFILESECTIONMAX. (This will delete all keys within a section!)</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_section_delete m_section_delete;
        /// <summary>
        /// To verify a section in a file do exist.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of a section to verify if exist or not. Maximum characters is INIFILESECTIONMAX.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_section_exist m_section_exist;
        /// <summary>
        /// To verify key within existing section in a file do exist.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <param name="keyName">Name of a key to verify if exist or not. Maximum characters is INIFILEKEYMAX.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_key_exist m_key_exist;
        /// <summary>
        /// To set key within existing section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX. (INFO: If a section has not been create, it will create one automatically.)</param>
        /// <param name="keyName">Name of an existing key in a section. Maximum characters is INIFILEKEYMAX. (INFO: If a key has not been create, it will create one automatically.)</param>
        /// <param name="valueName">Name of a value to set in a key. Maximum characters is INIFILEVALUEMAX. (NOTICE: It will overwrite existing value!)</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_value_set m_value_set;
        /// <summary>
        /// To get key within existing section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <param name="keyName">Name of a key to get from a section. Maximum characters is INIFILEKEYMAX.</param>
        /// <param name="valueName">Name of a value to get from a key. Maximum characters is INIFILEVALUEMAX.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_value_get m_value_get;
        /// <summary>
        /// To save data buffer into file content.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_save m_save;
        /// <summary>
        /// To load content into data buffer.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_load m_load;
        /// <summary>
        /// To clear the content and data buffer. (NOTICE: If you want to clear everything in a file, you must save it after call m_clear.)
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Does not return a value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_clear m_clear;
        /// <summary>
        /// To delete a key within existing section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <param name="keyName">Name of a key to delete from a section. Maximum characters is INIFILEKEYMAX.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_key_delete m_key_delete;
        /// <summary>
        /// Get total count of sections from a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Return total count of keys.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_section_count m_section_count;
        /// <summary>
        /// Get a section name by index in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionIndex">Input section index.</param>
        /// <param name="sectionName">Get name of section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_section_index m_section_index;
        /// <summary>
        /// Get total count of keys from existing section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <returns>Return total count of keys.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_key_count m_key_count;
        /// <summary>
        /// Get a key and value from section's key index in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <param name="keyIndex">Input key index.</param>
        /// <param name="keyName">Name of a key to get from a section. Maximum characters is INIFILEKEYMAX.</param>
        /// <param name="valueName">Name of a value to get from a section. Maximum characters is INIFILEVALUEMAX.</param>
        /// <returns>Return true or false.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_key_index m_key_index;

        //Simple & easier user-defined conversion + checker for null.
        public ICIniFile(ICIniFilePtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (ICIniFile)Marshal.PtrToStructure(data.ptr, typeof(ICIniFile));
            else
                this = new ICIniFile();
        }
        public static implicit operator ICIniFile(ICIniFilePtr data) {
            return new ICIniFile(data);
        }
        public bool isNotNull() {
            return m_release != null;
        }
    }
    public struct ICIniFileClass {
        public const int INIFILELENMAX = 512;
        public const int INIFILESECTIONMAX = 128;
        public const int INIFILEKEYMAX = 128;
        public const int INIFILEVALUEMAX = 128;
        public const char pound = '#';
        public const char semiColon = ';';
        private IntPtr ptr;
        private ICIniFile _this;
        public void m_release() {
            _this.m_release(ptr);
        }
        public void m_close() {
            _this.m_close(ptr);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_open_file([In, MarshalAs(UnmanagedType.LPWStr)] string fileName) {
            return _this.m_open_file(ptr, fileName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_create_file([In, MarshalAs(UnmanagedType.LPWStr)] string fileName) {
            return _this.m_create_file(ptr, fileName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_delete_file([In, MarshalAs(UnmanagedType.LPWStr)] string fileName) {
            return _this.m_delete_file(ptr, fileName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_content([In, Out, MarshalAs(UnmanagedType.LPWStr)] ref string content, [In, Out] ref uint len) {
            return _this.m_content(ptr, ref content, ref len);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_section_add([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName) {
            return _this.m_section_add(ptr, sectionName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_section_delete([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName) {
            return _this.m_section_delete(ptr, sectionName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_section_exist([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName) {
            return _this.m_section_exist(ptr, sectionName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_key_exist([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In, MarshalAs(UnmanagedType.LPWStr)] string keyName) {
            return _this.m_key_exist(ptr, sectionName, keyName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_value_set([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In, MarshalAs(UnmanagedType.LPWStr)] string keyName, [In, MarshalAs(UnmanagedType.LPWStr)] string valueName) {
            return _this.m_value_set(ptr, sectionName, keyName, valueName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_value_get([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In, MarshalAs(UnmanagedType.LPWStr)] string keyName, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder valueName) {
            return _this.m_value_get(ptr, sectionName, keyName, valueName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_save() {
            return _this.m_save(ptr);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_load() {
            return _this.m_load(ptr);
        }
        public void m_clear() {
            _this.m_clear(ptr);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_key_delete([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In, MarshalAs(UnmanagedType.LPWStr)] string keyName) {
            return _this.m_key_delete(ptr, sectionName, keyName);
        }
        public uint m_section_count() {
            return _this.m_section_count(ptr);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_section_index([In] uint sectionIndex, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder sectionName) {
            return _this.m_section_index(ptr, sectionIndex, sectionName);
        }
        public uint m_key_count([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName) {
            return _this.m_key_count(ptr, sectionName);
        }
        [return: MarshalAs(UnmanagedType.I1)]
        public bool m_key_index([In, MarshalAs(UnmanagedType.LPWStr)] string sectionName, [In] uint keyIndex, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder keyName, [In, Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder valueName) {
            return _this.m_key_index(ptr, sectionName, keyIndex, keyName, valueName);
        }


        //Simple & easier user-defined conversion + checker for null.
        public ICIniFileClass(ICIniFilePtr data) {
            ptr = data.ptr;
            if (data.ptr != IntPtr.Zero)
                _this = (ICIniFile)Marshal.PtrToStructure(data.ptr, typeof(ICIniFile));
            else
                this = new ICIniFileClass();
        }
        public static implicit operator ICIniFileClass(ICIniFilePtr data) {
            return new ICIniFileClass(data);
        }
        public bool isNotNull() {
            return ptr != null;
        }
    }
    public partial struct Interface {
        /// <summary>
        /// Returns a IPlayer class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IPlayer class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#17", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern ICIniFilePtr getICIniFile([In] uint uniqueHash);
    }
}
#endif
