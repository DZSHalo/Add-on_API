Imports System.Runtime.InteropServices

Public Class boolOption
    <MarshalAs(UnmanagedType.I1)>
    Public [boolean] As Boolean
End Class

Namespace Addon_API
    Public Enum EAO_RETURN
        'ONETIMEUPDATE = 2 'This is not compatiable with unmanaged application.
        OVERRIDE = 1 'This Is the only compatiable With unmanaged application.
        'CONTINUE = 0 'This Is Not compatiable with unmanaged application.
        FAIL = -1 'This Is Not compatiable with unmanaged application. However it will create an exception when attempt to unload it.
    End Enum
    Public Enum CMD_RETURN
        FAIL = -1
        NOMATCH = 0
        SUCC = 1
        SUCCDELAY = 2
    End Enum
    Public Enum e_boolean
        INVALID = -2
        FAIL = -1
        [FALSE] = 0
        [TRUE] = 1
    End Enum
#If EXT_IUTIL Then
    <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
    Public Delegate Function CmdFunc(<[In]> plI As PlayerInfo, <[In], Out> ByRef arg As ArgContainerVars, <[In]> protocolMsg As MSG_PROTOCOL, <[In]> idTimer As UInteger, <[In]> showChat As boolOption) As CMD_RETURN
#End If
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode, Pack:=1)>
    Public Structure addon_section_names
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=24)>
        Public sect_name1 As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=24)>
        Public sect_name2 As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=24)>
        Public sect_name3 As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=24)>
        Public sect_name4 As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=24)>
        Public sect_name5 As String
    End Structure
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode, Pack:=1)>
    Public Structure addon_info
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public name As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=15)>
        Public version As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=128)>
        Public author As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=255)>
        Public description As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=24)>
        Public config_folder As String
        Public sectors As addon_section_names
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=1)>
    Public Structure addon_version
        Private size As UShort          'Used by sizeof(versionEAO);
        Private requiredAPI As UShort   'API requirement revision (Including command functions)
        Private general As UShort       'General revision specifically for events in Halo.
        Private version As UShort       'addon_version revision
        Private pIHaloEngine As UShort  'Halo Engine interface revision
        Private pIObject As UShort      'Object interface revision
        Private pIPlayer As UShort      'Player interface revision
        Private pIAdmin As UShort       'Admin interface revision
        Private pICommand As UShort     'Command interface revision
        Private pIDatabase As UShort    'Database interface revision
        Private pIDBStmt As UShort      'Database Statement interface revision
        Private pICIniFile As UShort    'CiniFile interface revision
        Private pITimer As UShort       'Timer interface revision
        Private pIUtil As UShort        'Util interface revision
        Private pINetwork As UShort     'Network interface revision - reserved (DO NOT USE!)
        Private pISound As UShort       'Sound interface revision - reserved (DO NOT USE!)
        Private pIDirectX9 As UShort    'DirectX9 interface revision - reserved (DO NOT USE!)
        Private reserved1 As UShort     'reserved
        Private reserved2 As UShort     'reserved
        Private reserved3 As UShort     'reserved
        Private hkDatabase As UShort    'Database hook revision
        Private hkTimer As UShort       'Timer hook revision
        Private hkExternal As UShort    'External account revision - reserved (DO NOT USE!)
        Private reserved4 As UShort     'reserved
        Private reserved5 As UShort     'reserved
        Private reserved6 As UShort     'reserved
        Private reserved7 As UShort     'reserved
        Private reserved8 As UShort     'reserved
    End Structure

#If EXT_ITIMER Then
    Public Structure ITimerPtr
        Public ptr As IntPtr
    End Structure

    <StructLayout(LayoutKind.Sequential)>
    Public Structure ITimer
        <UnmanagedFunctionPointer(CallingConvention.Cdecl), System.Security.SuppressUnmanagedCodeSecurity>
        Public Delegate Function d_add(<[In]> uniqueHash As UInteger, <[In]> plI As PlayerInfoPtr, <[In]> execTime As UInteger) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl), System.Security.SuppressUnmanagedCodeSecurity>
        Public Delegate Sub d_delete(<[In]> uniqueHash As UInteger, <[In]> id As UInteger)
        ''' <summary>
        ''' Register a timer event delay.
        ''' </summary>
        ''' <param name="hash">Add-on unique ID.</param>
        ''' <param name="plI">Bind to specific player or use null for general.</param>
        ''' <param name="execTime">Amount of ticks later to execute a timer event. (1 tick = 1/30 second)</param>
        ''' <returns>Return ID of timer event.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_add As d_add
        ''' <summary>
        ''' Remove a timer event.
        ''' </summary>
        ''' <param name="hash">Add-on unique ID.</param>
        ''' <param name="id">Can be used only with registered ID number bind to specific Add-on.</param>
        ''' <returns>Return true or false if unable to reload Add-on.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_delete As d_delete

        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As ITimerPtr) As ITimer
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(ITimer)), ITimer)
            Else
                Return New ITimer
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return Me.m_add IsNot Nothing
        End Function
    End Structure
    Partial Public Structure [Interface]
        ''' <summary>
        ''' Returns a ITimer class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of ITimer class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint:="#18", CallingConvention:=CallingConvention.Cdecl), System.Security.SuppressUnmanagedCodeSecurity>
        <ComVisible(True)>
        Public Shared Function getITimer(<[In]> uniqueHash As UInteger) As ITimerPtr
        End Function
    End Structure
#End If
End Namespace