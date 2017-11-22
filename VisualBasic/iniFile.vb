Imports System.Text
Imports System.Runtime.InteropServices
Imports System.Collections.Generic

#If EXT_ICINIFILE Then

Namespace Addon_API

    Public Structure ICIniFilePtr
        Public ptr As IntPtr
    End Structure
    Public Structure ICIniFile
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_release(self As IntPtr)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_close(self As IntPtr)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_open_file(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> fileName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_create_file(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> fileName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_delete_file(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> fileName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_content(self As IntPtr, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> ByRef content As String, <[In], [Out]> ByRef len As UInteger) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_section_add(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_section_delete(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_section_exist(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_key_exist(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> keyName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_value_set(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> keyName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> valueName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_value_get(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> keyName As String, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> ByRef valueName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_save(self As IntPtr) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_load(self As IntPtr) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_clear(self As IntPtr)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_key_delete(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> keyName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_section_count(self As IntPtr) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_section_index(self As IntPtr, <[In]> index As UInteger, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> sectionName As StringBuilder) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_key_count(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_key_index(self As IntPtr, <[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In]> index As UInteger, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> keyName As StringBuilder, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> valueName As StringBuilder) As <MarshalAs(UnmanagedType.I1)> Boolean

        ''' <summary>
        ''' To release ICIniFile, cannot be re-used after release.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <returns>Does not return a value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_release As d_release
        ''' <summary>
        ''' To close a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <returns>Does not return a value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_close As d_close
        ''' <summary>
        ''' To open a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="fileName">Name of a file to open.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_open_file As d_open_file
        ''' <summary>
        ''' To create a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="fileName">Name of a file to create.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_create_file As d_create_file
        ''' <summary>
        ''' To delete a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="fileName">Name of a file to delete.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_delete_file As d_delete_file
        ''' <summary>
        ''' To obtain raw content from an opened file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="content">Raw content within a file.</param>
        ''' <param name="len">Length of the content.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_content As d_content
        ''' <summary>
        ''' To add a section in a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of a section to add. Maximum characters is INIFILESECTIONMAX.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_section_add As d_section_add
        ''' <summary>
        ''' To delete a section in a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of a section to delete. Maximum characters is INIFILESECTIONMAX. (This will delete all keys within a section!)</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_section_delete As d_section_delete
        ''' <summary>
        ''' To verify a section in a file do exist.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of a section to verify if exist or not. Maximum characters is INIFILESECTIONMAX.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_section_exist As d_section_exist
        ''' <summary>
        ''' To verify key within existing section in a file do exist.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        ''' <param name="keyName">Name of a key to verify if exist or not. Maximum characters is INIFILEKEYMAX.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_key_exist As d_key_exist
        ''' <summary>
        ''' To set key within existing section in a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX. (INFO: If a section has not been create, it will create one automatically.)</param>
        ''' <param name="keyName">Name of an existing key in a section. Maximum characters is INIFILEKEYMAX. (INFO: If a key has not been create, it will create one automatically.)</param>
        ''' <param name="valueName">Name of a value to set in a key. Maximum characters is INIFILEVALUEMAX. (NOTICE: It will overwrite existing value!)</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_value_set As d_value_set
        ''' <summary>
        ''' To get key within existing section in a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        ''' <param name="keyName">Name of a key to get from a section. Maximum characters is INIFILEKEYMAX.</param>
        ''' <param name="valueName">Name of a value to get from a key. Maximum characters is INIFILEVALUEMAX.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_value_get As d_value_get
        ''' <summary>
        ''' To save data buffer into file content.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_save As d_save
        ''' <summary>
        ''' To load content into data buffer.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_load As d_load
        ''' <summary>
        ''' To clear the content and data buffer. (NOTICE: If you want to clear everything in a file, you must save it after call m_clear.)
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <returns>Does not return a value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_clear As d_clear
        ''' <summary>
        ''' To delete a key within existing section in a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        ''' <param name="keyName">Name of a key to delete from a section. Maximum characters is INIFILEKEYMAX.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_key_delete As d_key_delete
        ''' <summary>
        ''' Get total count of sections from a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <returns>Return total count of keys.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_section_count As d_section_count
        ''' <summary>
        ''' Get a section name by index in a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionIndex">Input section index.</param>
        ''' <param name="sectionName">Get name of section. Maximum characters is INIFILESECTIONMAX.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_section_index As d_section_index
        ''' <summary>
        ''' Get total count of keys from existing section in a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        ''' <returns>Return total count of keys.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_key_count As d_key_count
        ''' <summary>
        ''' Get a key and value from section's key index in a file.
        ''' </summary>
        ''' <param name="self">Must include pointer of existing ICIniFile variable.</param>
        ''' <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        ''' <param name="keyIndex">Input key index.</param>
        ''' <param name="keyName">Name of a key to get from a section. Maximum characters is INIFILEKEYMAX.</param>
        ''' <param name="valueName">Name of a value to get from a section. Maximum characters is INIFILEVALUEMAX.</param>
        ''' <returns>Return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_key_index As d_key_index

        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As ICIniFilePtr) As ICIniFile
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(ICIniFile)), ICIniFile)
            Else
                Return New ICIniFile
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return m_release IsNot Nothing
        End Function
    End Structure
    Public Structure ICIniFileClass
        Public Const INIFILELENMAX As Integer = 512
        Public Const INIFILESECTIONMAX As Integer = 128
        Public Const INIFILEKEYMAX As Integer = 128
        Public Const INIFILEVALUEMAX As Integer = 128
        Public Const pound As Char = "#"c
        Public Const semiColon As Char = ";"c
        Private ptr As IntPtr
        Private _this As ICIniFile
        Public Sub m_release()
            _this.m_release(ptr)
        End Sub
        Public Sub m_close()
            _this.m_close(ptr)
        End Sub
        Public Function m_open_file(<[In], MarshalAs(UnmanagedType.LPWStr)> fileName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_open_file(ptr, fileName)
        End Function
        Public Function m_create_file(<[In], MarshalAs(UnmanagedType.LPWStr)> fileName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_create_file(ptr, fileName)
        End Function
        Public Function m_delete_file(<[In], MarshalAs(UnmanagedType.LPWStr)> fileName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_delete_file(ptr, fileName)
        End Function
        Public Function m_content(<[In], [Out], MarshalAs(UnmanagedType.LPWStr)> ByRef content As String, <[In], [Out]> ByRef len As UInteger) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_content(ptr, content, len)
        End Function
        Public Function m_section_add(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_section_add(ptr, sectionName)
        End Function
        Public Function m_section_delete(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_section_delete(ptr, sectionName)
        End Function
        Public Function m_section_exist(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_section_exist(ptr, sectionName)
        End Function
        Public Function m_key_exist(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> keyName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_key_exist(ptr, sectionName, keyName)
        End Function
        Public Function m_value_set(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> keyName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> valueName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_value_set(ptr, sectionName, keyName, valueName)
        End Function
        Public Function m_value_get(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> keyName As String, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> ByRef valueName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_value_get(ptr, sectionName, keyName, valueName)
        End Function
        Public Function m_save() As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_save(ptr)
        End Function
        Public Function m_load() As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_load(ptr)
        End Function
        Public Sub m_clear()
            _this.m_clear(ptr)
        End Sub
        Public Function m_key_delete(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In], MarshalAs(UnmanagedType.LPWStr)> keyName As String) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_key_delete(ptr, sectionName, keyName)
        End Function
        Public Function m_section_count() As UInteger
            Return _this.m_section_count(ptr)
        End Function
        Public Function m_section_index(<[In]> sectionIndex As UInteger, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> sectionName As StringBuilder) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_section_index(ptr, sectionIndex, sectionName)
        End Function
        Public Function m_key_count(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String) As UInteger
            Return _this.m_key_count(ptr, sectionName)
        End Function
        Public Function m_key_index(<[In], MarshalAs(UnmanagedType.LPWStr)> sectionName As String, <[In]> keyIndex As UInteger, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> keyName As StringBuilder, <[In], [Out], MarshalAs(UnmanagedType.LPWStr)> valueName As StringBuilder) As <MarshalAs(UnmanagedType.I1)> Boolean
            Return _this.m_key_index(ptr, sectionName, keyIndex, keyName, valueName)
        End Function


        'Simple & easier user-defined conversion + checker for null.
        Public Sub New(data As ICIniFilePtr)
            ptr = data.ptr
            If data.ptr <> IntPtr.Zero Then
                _this = CType(Marshal.PtrToStructure(data.ptr, GetType(ICIniFile)), ICIniFile)
            Else
                _this = New ICIniFile
            End If
        End Sub
        Public Shared Widening Operator CType(data As ICIniFilePtr) As ICIniFileClass
            Return New ICIniFileClass(data)
        End Operator
        Public Function isNotNull() As Boolean
            Return ptr <> IntPtr.Zero
        End Function
    End Structure
    Partial Public Structure [Interface]
        ''' <summary>
        ''' Returns a IPlayer class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IPlayer class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint:="#17", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Function getICIniFile(<[In]> uniqueHash As UInteger) As ICIniFilePtr
        End Function
    End Structure
End Namespace
#End If
