Imports System.Runtime.InteropServices

Public Structure s_tag_header_ptr
    Public ptr As IntPtr
End Structure
Public Structure s_tag_header_managed
    Private gPtr As s_tag_header_ptr
    Public data As s_tag_header
    Public Function getPtr() As s_tag_header_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_tag_header_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = DirectCast(Marshal.PtrToStructure(dPtr.ptr, GetType(s_tag_header)), s_tag_header)
        Else
            data = New s_tag_header()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_tag_header_ptr) As s_tag_header_managed
        Return New s_tag_header_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = DirectCast(Marshal.PtrToStructure(gPtr.ptr, GetType(s_tag_header)), s_tag_header)
        End If
    End Sub
End Structure

Public Structure s_tag_reference_ptr
    Public ptr As IntPtr
End Structure
Public Structure s_tag_reference_managed
    Private gPtr As s_tag_reference_ptr
    Public data As s_tag_reference
    Public Function getPtr() As s_tag_reference_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_tag_reference_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = DirectCast(Marshal.PtrToStructure(dPtr.ptr, GetType(s_tag_reference)), s_tag_reference)
        Else
            data = New s_tag_reference()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_tag_reference_ptr) As s_tag_reference_managed
        Return New s_tag_reference_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = DirectCast(Marshal.PtrToStructure(gPtr.ptr, GetType(s_tag_reference)), s_tag_reference)
        End If
    End Sub
End Structure

Public Structure s_tag_block_definition_ptr
    Public ptr As IntPtr
End Structure
Public Structure s_tag_block_definition_managed
    Private gPtr As s_tag_block_definition_ptr
    Public data As s_tag_block_definition
    Public Function getPtr() As s_tag_block_definition_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_tag_block_definition_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = DirectCast(Marshal.PtrToStructure(dPtr.ptr, GetType(s_tag_block_definition)), s_tag_block_definition)
        Else
            data = New s_tag_block_definition()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_tag_block_definition_ptr) As s_tag_block_definition_managed
        Return New s_tag_block_definition_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = DirectCast(Marshal.PtrToStructure(gPtr.ptr, GetType(s_tag_block_definition)), s_tag_block_definition)
        End If
    End Sub
End Structure

'
'public struct s_tag_block {
'};
'/*
'[StructLayout(LayoutKind.Sequential)]
'public struct s_tag_group {
'    //TODO: Is this needed?
'};
