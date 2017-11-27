#If EXT_IDATABASE OrElse EXT_IDATABASESTATEMENT Then
Imports System.Runtime.InteropServices
Imports System.Data.Odbc

Namespace Addon_API
    #If EXT_IDATABASE Then
    Public Structure IDatabasePtr
        Public ptr As IntPtr
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Structure IDatabase
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_connect(<[In], MarshalAs(UnmanagedType.LPWStr)> ConnectionStr As String, <[In]> [Option] As Integer, <[In]> Param As IntPtr, <[In]> ParamLen As Integer) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_connect_mdb(<[In], MarshalAs(UnmanagedType.LPWStr)> MDBPath As String, <[In], MarshalAs(UnmanagedType.LPWStr)> User As String, <[In], MarshalAs(UnmanagedType.LPWStr)> Pass As String, <[In], MarshalAs(UnmanagedType.I1)> Exclusive As Boolean) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_disconnect()
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_status() As Integer
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_check()
        ''' <summary>
        ''' To connect a supported database.
        ''' </summary>
        ''' <param name="ConnectionStr">See https://msdn.microsoft.com/en-us/library/ms715433.aspx for details.</param>
        ''' <param name="Option">See Attribute at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        ''' <param name="Param">See ValuePtr at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        ''' <param name="ParamLen">See StringLength at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        ''' <returns>Return true or false.</returns>
        Public m_connect As d_connect
        ''' <summary>
        ''' To connect local MDB file database. (Please note this function call to m_connect function.)
        ''' </summary>
        ''' <param name="MDBPath">Path to local MDB file database.</param>
        ''' <param name="User">Username if required to connect.</param>
        ''' <param name="Pass">Password if required to connect.</param>
        ''' <param name="Exclusive">True to restrict access to database or false to share database.</param>
        ''' <returns>Return true or false.</returns>
        Public m_connect_mdb As d_connect_mdb
        ''' <summary>
        ''' To disconnect current active database.
        ''' </summary>
        ''' <returns>Does not return any value.</returns>
        Public m_disconnect As d_disconnect
        ''' <summary>
        ''' To verify if active database is still connected.
        ''' </summary>
        ''' <returns>Return false if connection is dead from last attempt query or other value.</returns>
        Public m_status As d_status
        ''' <summary>
        ''' To check an active database has up-to-date tables, if not it will automate update the tables.
        ''' </summary>
        ''' <returns>Does not return any value.</returns>
        Public m_check As d_check

        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As IDatabasePtr) As IDatabase
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(IDatabase)), IDatabase)
            Else
                Return New IDatabase
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return m_connect IsNot Nothing
        End Function
    End Structure
    #End If
    #If EXT_IDATABASESTATEMENT Then
    Public Structure IDBStmtPtr
        Public ptr As IntPtr
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Structure IDBStmt

        #Error Due to C# is using Command class instead of Statement plus other issues, this is on hold for further review.

        'TODO: This is where I left off for IDBStmt interface.

        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As IDBStmtPtr) As IDBStmt
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(IDBStmt)), IDBStmt)
            Else
                Return New IDBStmt
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return data IsNot Nothing
        End Function
    End Structure

    #End If

    Public Partial Structure [Interface]
        #If EXT_IDATABASE Then
        ''' <summary>
        ''' Returns a IDatabase class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IDatabase class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint := "#15", CallingConvention := CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Function getIDatabase(<[In]> uniqueHash As UInteger) As IDatabasePtr
        End Function
        #End If
        #If EXT_IDATABASESTATEMENT Then
        ''' <summary>
        ''' Returns a IDBStmt class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IDBStmt class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint := "#16", CallingConvention := CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Function getIDBStmt(<[In]> uniqueHash As UInteger) As IDBStmtPtr
        End Function
        #End If
    End Structure
End Namespace
#End If
