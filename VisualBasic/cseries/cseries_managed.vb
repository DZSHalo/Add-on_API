
Imports System.Runtime.InteropServices

Public Structure s_ident_ptr
    Public ptr As IntPtr
End Structure

Public Structure s_ident_managed
    Private gPtr As s_ident_ptr
    Public data As s_ident
    Public Function getPtr() As s_ident_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_ident_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = DirectCast(Marshal.PtrToStructure(dPtr.ptr, GetType(s_ident)), s_ident)
        Else
            data = New s_ident()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_ident_ptr) As s_ident_managed
        Return New s_ident_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = DirectCast(Marshal.PtrToStructure(gPtr.ptr, GetType(s_ident)), s_ident)
        End If
    End Sub
    Public Function isNotNull() As Boolean
        Return gPtr.ptr <> IntPtr.Zero
    End Function
End Structure
