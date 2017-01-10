#If EXT_ICOMMAND Then
Imports System.Runtime.InteropServices

#If Not EXT_IUTIL Then
#Error EXT_IUTIL is required to be use with ICommand interface.
#End If

Namespace Addon_API
    Public Structure ICommandPtr
        Public ptr As IntPtr
    End Structure
    <StructLayoutAttribute(LayoutKind.Sequential)>    
    Public Structure ICommand
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_add(<[In]> hash As UInteger, <[In], MarshalAs(UnmanagedType.LPWStr)> command As String, <[In]> func As CmdFunc, <[In], MarshalAs(UnmanagedType.LPWStr)> section As String, <[In]> min As UShort, <[In]> max As UShort,    
            <[In], MarshalAs(UnmanagedType.I1)> allowOverride As Boolean, <[In]> mode As HEXT.GAME_MODE_S) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_delete(<[In]> hash As UInteger, <[In]> func As CmdFunc, <[In], MarshalAs(UnmanagedType.LPWStr)> command As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_reload_level(<[In]> hash As UInteger) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_alias_add(<[In], MarshalAs(UnmanagedType.LPWStr)> command As String, <[In], MarshalAs(UnmanagedType.LPWStr)> [alias] As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_alias_delete(<[In], MarshalAs(UnmanagedType.LPWStr)> command As String, <[In], MarshalAs(UnmanagedType.LPWStr)> [alias] As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_load_from_file(<[In]> hash As UInteger, <[In], MarshalAs(UnmanagedType.LPWStr)> fileName As String, plI As PlayerInfo, protocolMsg As MSG_PROTOCOL) As <MarshalAs(UnmanagedType.I1)> Boolean
        ''' <summary>
        ''' To add nonexisting <paramref name="command"/> bind to <paramref name="func"/> into command system and return true or false.
        ''' </summary>
        ''' <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        ''' <param name="command">A new command name, or override a command once if permitted, into command system.</param>
        ''' <param name="func">A new function or existing function within same add-on only.</param>
        ''' <param name="section">Section where command belongs to.</param>
        ''' <param name="min">Minimum requirement to able allow command execute.</param>
        ''' <param name="max">Maximum requirement to able allow command execute.</param>
        ''' <param name="allowOverride">An option to either allow or forbidden another add-on or current add-on override a command.</param>
        ''' <param name="mode">To permit a command executed in either single player, multiplayer, hosting a game, or all. See below GAME_MODE_S struct for pre-defined availability.</param>
        ''' <returns>Only return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_add As d_add
        ''' <summary>
        ''' To delete a <paramref name="command"/> which is binded to <paramref name="func"/> and return true or false.
        ''' </summary>
        ''' <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        ''' <param name="func">A function currently binded to a command.</param>
        ''' <param name="command">A command currently binded to a function.</param>
        ''' <returns>Only return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_delete As d_delete
        ''' <summary>
        ''' To load or reload authorized add-on's commands level from commands.ini file.
        ''' </summary>
        ''' <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        ''' <returns>Only return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_reload_level As d_reload_level
        ''' <summary>
        ''' To add an <paramref name="alias"/> from <paramref name="command"/> and return true or false.
        ''' </summary>
        ''' <param name="command">Command name currently exist in command system.</param>
        ''' <param name="alias">An alias command name which is not binded to a command.</param>
        ''' <returns>Only return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_alias_add As d_alias_add
        ''' <summary>
        ''' To delete an <paramref name="alias"/> from <paramref name="command"/> and return true or false.
        ''' </summary>
        ''' <param name="command">Command name currently exist in command system.</param>
        ''' <param name="alias">An alias command name currently bind to a command.</param>
        ''' <returns>Only return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_alias_delete As d_alias_delete
        ''' <summary>
        ''' To load a custom command(s) from <paramref name="fileName"/> and return true or false.
        ''' </summary>
        ''' <param name="hash">Valid owner Add-on hash and existing config_folder defined.</param>
        ''' <param name="fileName">Custom file name to load and execute from.</param>
        ''' <param name="plI">Bind user of this execution process.</param>
        ''' <param name="protocolMsg">For output the message to binded user.</param>
        ''' <returns>Only return true or false.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_load_from_file As d_load_from_file


        'Simple & easier user-defined conversion + checker for null.
        Public Sub New(data As ICommandPtr)
        End Sub
        Public Shared Widening Operator CType(data As ICommandPtr) As ICommand
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(ICommand)), ICommand)
            Else
                Return New ICommand
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return m_add IsNot Nothing
        End Function
    End Structure

    Public Partial Structure [Interface]
        ''' <summary>
        ''' Returns a ICommand class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IAdmin class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint := "#14", CallingConvention := CallingConvention.Cdecl)>    
        <ComVisible(True)>    
        Public Shared Function getICommand(<[In]> uniqueHash As UInteger) As ICommandPtr
        End Function
    End Structure
End Namespace
#End If
