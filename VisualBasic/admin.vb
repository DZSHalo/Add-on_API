#If EXT_IADMIN Then
Imports System
Imports System.Runtime.InteropServices

Namespace Addon_API
    Public Enum LOGIN_VALIDATION As Integer
        INVALID = -1
        FAIL = 0
        OK = 1
    End Enum
    Public Enum CMD_AUTH As Integer
        NOT_FOUND = -2
        OUT_OF_RANGE = -1
        DENIED = 0
        AUTHORIZED = 1
    End Enum
    Public Structure IAdminPtr
        Public ptr As IntPtr
    End Structure
    <StructLayoutAttribute(LayoutKind.Sequential)>    
    Public Structure IAdmin
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_is_authorized(ByRef player As PlayerInfo, <[In], MarshalAs(UnmanagedType.LPWStr)> command As String, <[In], Out> ByRef arg As ArgContainerVars, <[In], Out> ByRef func As CmdFunc) As CMD_AUTH
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_is_username_exist(<[In], MarshalAs(UnmanagedType.LPWStr)> username As String) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_add(<[In], MarshalAs(UnmanagedType.LPWStr)> hashW As String, <[In], MarshalAs(UnmanagedType.LPWStr)> IP_Addr As String, <[In], MarshalAs(UnmanagedType.LPWStr)> IP_Port As String, <[In], MarshalAs(UnmanagedType.LPWStr)> username As String, <[In], MarshalAs(UnmanagedType.LPWStr)> password As String, <[In]> level As Short,    
            <[In], MarshalAs(UnmanagedType.I1)> remote As Boolean, <[In], MarshalAs(UnmanagedType.I1)> pass_force As Boolean) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_delete(<[In], MarshalAs(UnmanagedType.LPWStr)> username As String) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>    
        Public Delegate Function d_login(ByRef player As PlayerInfo, <[In]> protocolMsg As MSG_PROTOCOL, <[In], MarshalAs(UnmanagedType.LPWStr)> username As String, <[In], MarshalAs(UnmanagedType.LPWStr)> password As String) As LOGIN_VALIDATION
        ''' <summary>
        ''' To verify if <paramref name="player">player</paramref> is authorized to use <paramref name="command">command</paramref>.
        ''' </summary>
        ''' <param name="player">Required to input player.</param>
        ''' <param name="command">Required to input command.</param>
        ''' <param name="arg">Output the argument from command.</param>
        ''' <param name="func">Output a function link to the command.</param>
        ''' <returns>Only return true, false, and -1 if input is invalid.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_is_authorized As d_is_authorized
        ''' <summary>
        ''' To verify if <paramref name="username"/> exist in database and return true, false, or -1 for database is offline.
        ''' </summary>
        ''' <param name="username">Take unicode username to verify.</param>
        ''' <returns>Only return true, false, and -1 if database is offline.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_is_username_exist As d_is_username_exist
        ''' <summary>
        ''' To add an admin to the database and return true, false, or -1 for database is offline.
        ''' </summary>
        ''' <param name="hashW">Maximum permitted is 32 characters.</param>
        ''' <param name="IP_Addr">Maximum permitted is 15 characters.</param>
        ''' <param name="IP_Port">Maximum permitted is 6 characters.</param>
        ''' <param name="username">Maximum permitted is 24 characters.</param>
        ''' <param name="password">No limitation on password for now.</param>
        ''' <param name="level">Maximum permitted is 9999.</param>
        ''' <param name="remote">To permit remote administrator access without need to use Halo game.</param>
        ''' <param name="pass_force">Force administrator to change their password.</param>
        ''' <returns>Only return true, false, and -1 if database is offline.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_add As d_add
        ''' <summary>
        ''' To remove <paramref name="username"/> from database and return true, false, or -1 for database is offline.
        ''' </summary>
        ''' <param name="username">Maximum permitted is 24 characters.</param>
        ''' <returns>Only return true, false, and -1 if database is offline.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_delete As d_delete
        ''' <summary>
        ''' To login a <paramref name="player"/> as administrator from database verfication and return LOGIN_INVALID, LOGIN_FAIL, and LOGIN_OK.
        ''' </summary>
        ''' <param name="player">Take ingame or remote admin to verify.</param>
        ''' <param name="chatRconRemote">To return a message back to player.</param>
        ''' <param name="username">Maximum permitted is 24 characters.</param>
        ''' <param name="password">No limitation on password for now.</param>
        ''' <returns>Only return LOGIN_INVALID, LOGIN_FAIL, and LOGIN_OK.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>    
        Public m_login As d_login

        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As IAdminPtr) As IAdmin
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(IAdmin)), IAdmin)
            Else
                Return New IAdmin
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return not (m_is_authorized  is nothing) 
        End Function
    End Structure

    Public Partial Structure [Interface]
        ''' <summary>
        ''' Returns a IAdmin class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IAdmin class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint := "#13", CallingConvention := CallingConvention.Cdecl)>    
        <ComVisible(True)>    
        Public Shared Function getIAdmin(<[In]> uniqueHash As UInteger) As IAdminPtr
        End Function
    End Structure
End Namespace
#End If
