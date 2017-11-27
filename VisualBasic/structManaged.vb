Imports System.Runtime.InteropServices

Public Structure chatDataManaged
    Public data As chatData
    Public Sub New(<MarshalAs(UnmanagedType.LPWStr, SizeConst:=&H255)> text As String, player As UInteger, type As chatType)
        data.msg_ptr = Marshal.StringToHGlobalAnsi(text)
        data.player = player
        data.type = type
    End Sub
    Protected Overrides Sub Finalize()
        Marshal.FreeHGlobal(data.msg_ptr)
    End Sub
End Structure

Public Structure rconDataManaged
    Public data As rconData
    Public Sub New(<MarshalAs(UnmanagedType.LPStr, SizeConst:=&H50)> text As String)
        data.msg = text
        data.unk = 0
        data.msg_ptr = Marshal.StringToHGlobalAnsi(data.msg)
    End Sub
    Protected Overrides Sub Finalize()
        Marshal.FreeHGlobal(data.msg_ptr)
    End Sub
End Structure

Public Structure s_player_reserved_slot_managed
    Private gPtr As s_player_reserved_slot_ptr
    Private slotPtr As IntPtr
    Private slot As UInteger
    Public data As s_player_reserved_slot
    Public Function getPtr() As s_player_reserved_slot_ptr
        Return New s_player_reserved_slot_ptr() With {
            .ptr = slotPtr
        }
    End Function
    Public Function getGlobalPtr() As s_player_reserved_slot_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_player_reserved_slot_ptr)
        gPtr = dPtr
        slotPtr = dPtr.ptr
        slot = 0
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(slotPtr, GetType(s_player_reserved_slot)), s_player_reserved_slot)
        Else
            data = New s_player_reserved_slot()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_player_reserved_slot_ptr) As s_player_reserved_slot_managed
        Return New s_player_reserved_slot_managed(dPtr)
    End Operator
    Public Sub save()
        If slotPtr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, slotPtr, False)
        End If
    End Sub
    Public Sub refresh()
        If slotPtr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(slotPtr, GetType(s_player_reserved_slot)), s_player_reserved_slot)
        End If
    End Sub 'Extended version for arrays usage.
    Public Sub setSlot(slotIndex As UInteger)
        If slotIndex > 15 Then
            Throw New IndexOutOfRangeException("slotIndex is out of range.")
        End If
        slot = slotIndex
        slotPtr = New IntPtr(gPtr.ptr.ToInt32() + slotIndex * Marshal.SizeOf(GetType(s_player_reserved_slot)))
        refresh()
    End Sub
    Public Shared Operator +(pRSM As s_player_reserved_slot_managed) As s_player_reserved_slot_managed
        pRSM.setSlot(pRSM.slot + 1)
        Return pRSM
    End Operator
    Public Shared Operator -(pRSM As s_player_reserved_slot_managed) As s_player_reserved_slot_managed
        pRSM.setSlot(pRSM.slot - 1)
        Return pRSM
    End Operator

End Structure

Namespace Addon_API
    Partial Public Structure [Global]
        Public Shared s_machine_slot_size As Byte
    End Structure
End Namespace

Public Structure s_machine_slot_managed
    Private gPtr As s_machine_slot_ptr
    Private slotPtr As IntPtr
    Private slot As UInteger
    Public data As s_machine_slot
    Public Function getPtr() As s_machine_slot_ptr
        Return New s_machine_slot_ptr() With {
            .ptr = slotPtr
        }
    End Function
    Public Function getGlobalPtr() As s_machine_slot_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_machine_slot_ptr)
        gPtr = dPtr
        slotPtr = dPtr.ptr
        slot = 0
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(slotPtr, GetType(s_machine_slot)), s_machine_slot)
        Else
            data = New s_machine_slot()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_machine_slot_ptr) As s_machine_slot_managed
        Return New s_machine_slot_managed(dPtr)
    End Operator
    Public Sub save()
        If slotPtr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, slotPtr, False)
        End If
    End Sub
    Public Sub refresh()
        If slotPtr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(slotPtr, GetType(s_machine_slot)), s_machine_slot)
        End If
    End Sub
    'Extended version for arrays usage.
    Public Sub setSlot(slotIndex As UInteger)
        If slotIndex > 15 Then
            Throw New IndexOutOfRangeException("slotIndex is out of range.")
        End If
        If Addon_API.[Global].s_machine_slot_size = 0 Then
            Throw New InvalidOperationException("Global.s_machine_slot_size cannot be zero, call getIHaloEngine first before attempt to use this operation.")
        End If
        slot = slotIndex
        slotPtr = New IntPtr(gPtr.ptr.ToInt32() + slotIndex * Addon_API.[Global].s_machine_slot_size)
        refresh()
    End Sub
    Public Shared Operator +(mSM As s_machine_slot_managed, increment As Integer) As s_machine_slot_managed
        mSM.setSlot(mSM.slot + increment)
        Return mSM
    End Operator
    Public Shared Operator -(mSM As s_machine_slot_managed, decrement As Integer) As s_machine_slot_managed
        mSM.setSlot(mSM.slot - decrement)
        Return mSM
    End Operator

End Structure

Public Structure s_player_slot_managed
    Private gPtr As s_player_slot_ptr
    Private slotPtr As IntPtr
    Private slot As UInteger
    Public data As s_player_slot
    Public Function getPtr() As s_player_slot_ptr
        Return New s_player_slot_ptr() With {
            .ptr = slotPtr
        }
    End Function
    Public Function getGlobalPtr() As s_player_slot_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_player_slot_ptr)
        gPtr = dPtr
        slotPtr = dPtr.ptr
        slot = 0
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(slotPtr, GetType(s_player_slot)), s_player_slot)
        Else
            data = New s_player_slot()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_player_slot_ptr) As s_player_slot_managed
        Return New s_player_slot_managed(dPtr)
    End Operator
    Public Sub save()
        If slotPtr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, slotPtr, False)
        End If
    End Sub
    Public Sub refresh()
        If slotPtr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(slotPtr, GetType(s_player_slot)), s_player_slot)
        End If
    End Sub 'Extended version for arrays usage.
    Public Sub setSlot(slotIndex As UInteger)
        If slotIndex > 15 Then
            Throw New IndexOutOfRangeException("slotIndex is out of range.")
        End If
        slot = slotIndex
        slotPtr = New IntPtr(gPtr.ptr.ToInt32() + slotIndex * Marshal.SizeOf(GetType(s_player_slot)))
        refresh()
    End Sub
    Public Shared Operator +(pSM As s_player_slot_managed) As s_player_slot_managed
        pSM.setSlot(pSM.slot + 1)
        Return pSM
    End Operator
    Public Shared Operator -(pSM As s_player_slot_managed) As s_player_slot_managed
        pSM.setSlot(pSM.slot - 1)
        Return pSM
    End Operator

End Structure

Public Structure s_gametype_managed
    Private gPtr As s_gametype_ptr
    Public data As s_gametype
    Public Function getPtr() As s_gametype_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_gametype_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(s_gametype)), s_gametype)
        Else
            data = New s_gametype()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_gametype_ptr) As s_gametype_managed
        Return New s_gametype_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_gametype)), s_gametype)
        End If
    End Sub
End Structure

Public Structure s_gametype_gflag_managed
    Private gPtr As s_gametype_gflag_ptr
    Public data As s_gametype_gflag
    Public Function getPtr() As s_gametype_gflag_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_gametype_gflag_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(s_gametype_gflag)), s_gametype_gflag)
        Else
            data = New s_gametype_gflag()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_gametype_gflag_ptr) As s_gametype_gflag_managed
        Return New s_gametype_gflag_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_gametype_gflag)), s_gametype_gflag)
        End If
    End Sub
End Structure

Public Structure s_gametype_globals_managed
    Private gPtr As s_gametype_globals_ptr
    Public data As s_gametype_globals
    Public Function getPtr() As s_gametype_globals_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_gametype_globals_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(s_gametype_globals)), s_gametype_globals)
        Else
            data = New s_gametype_globals()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_gametype_globals_ptr) As s_gametype_globals_managed
        Return New s_gametype_globals_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_gametype_globals)), s_gametype_globals)
        End If
    End Sub
End Structure

Public Structure s_server_header_managed
    Private gPtr As s_server_header_ptr
    Public data As s_server_header
    Public Function getPtr() As s_server_header_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_server_header_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(s_server_header)), s_server_header)
        Else
            data = New s_server_header()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_server_header_ptr) As s_server_header_managed
        Return New s_server_header_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_server_header)), s_server_header)
        End If
    End Sub
End Structure

Public Structure s_object_managed
    Private gPtr As s_object_ptr
    Public s_object_n As s_object
    Public Function getPtr() As s_object_ptr
        Return gPtr
    End Function
    Public Sub New(data As s_object_ptr)
        gPtr = data
        If data.ptr <> IntPtr.Zero Then
            s_object_n = CType(Marshal.PtrToStructure(data.ptr, GetType(s_object)), s_object)
        Else
            s_object_n = New s_object()
        End If
    End Sub
    Public Shared Widening Operator CType(data As s_object_ptr) As s_object_managed
        Return New s_object_managed(data)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(s_object_n, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            s_object_n = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_object)), s_object)
        End If
    End Sub
End Structure

Public Structure s_biped_managed
    Private gPtr As s_object_ptr
    Public s_object_n As s_biped
    Public Function getPtr() As s_object_ptr
        Return gPtr
    End Function
    Public Sub New(data As s_object_ptr)
        gPtr = data
        If data.ptr <> IntPtr.Zero Then
            s_object_n = CType(Marshal.PtrToStructure(data.ptr, GetType(s_biped)), s_biped)
        Else
            s_object_n = New s_biped()
        End If
    End Sub
    Public Shared Widening Operator CType(data As s_object_ptr) As s_biped_managed
        Return New s_biped_managed(data)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(s_object_n, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            s_object_n = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_biped)), s_biped)
        End If
    End Sub
End Structure

'TODO: Need to put s_weapon, and s_vehicle in here before production release

Public Structure s_map_header_managed
    Private gPtr As s_map_header_ptr
    Public data As s_map_header
    Public Function getPtr() As s_map_header_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_map_header_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(s_map_header)), s_map_header)
        Else
            data = New s_map_header()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_map_header_ptr) As s_map_header_managed
        Return New s_map_header_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_map_header)), s_map_header)
        End If
    End Sub
End Structure

Public Structure s_map_status_managed
    Private gPtr As s_map_status_ptr
    Public data As s_map_status
    Public Function getPtr() As s_map_status_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_map_status_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(Marshal.ReadIntPtr(gPtr.ptr), GetType(s_map_status)), s_map_status)
        Else
            data = New s_map_status()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_map_status_ptr) As s_map_status_managed
        Return New s_map_status_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, Marshal.ReadIntPtr(gPtr.ptr), False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(Marshal.ReadIntPtr(gPtr.ptr), GetType(s_map_status)), s_map_status)
        End If
    End Sub
End Structure

Public Structure s_console_header_managed
    Private gPtr As s_console_header_ptr
    Public data As s_console_header
    Public Function getPtr() As s_console_header_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_console_header_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(s_console_header)), s_console_header)
        Else
            data = New s_console_header()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_console_header_ptr) As s_console_header_managed
        Return New s_console_header_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_console_header)), s_console_header)
        End If
    End Sub
End Structure

'Extras for Add-on API usage.

Public Structure s_cheat_header_managed
    Private gPtr As s_cheat_header_ptr
    Public data As s_cheat_header
    Public Function getPtr() As s_cheat_header_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_cheat_header_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(s_cheat_header)), s_cheat_header)
        Else
            data = New s_cheat_header()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_cheat_header_ptr) As s_cheat_header_managed
        Return New s_cheat_header_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_cheat_header)), s_cheat_header)
        End If
    End Sub
End Structure

Public Structure D3DCOLOR_COLORVALUE_ARGB_managed
    Private gPtr As D3DCOLOR_COLORVALUE_ARGB_ptr
    Public data As D3DCOLOR_COLORVALUE_ARGB
    Public Function getPtr() As D3DCOLOR_COLORVALUE_ARGB_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As D3DCOLOR_COLORVALUE_ARGB_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(D3DCOLOR_COLORVALUE_ARGB)), D3DCOLOR_COLORVALUE_ARGB)
        Else
            data = New D3DCOLOR_COLORVALUE_ARGB()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As D3DCOLOR_COLORVALUE_ARGB_ptr) As D3DCOLOR_COLORVALUE_ARGB_managed
        Return New D3DCOLOR_COLORVALUE_ARGB_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(D3DCOLOR_COLORVALUE_ARGB)), D3DCOLOR_COLORVALUE_ARGB)
        End If
    End Sub
End Structure
Public Structure s_console_color_list_managed
    Private gPtr As s_console_color_list_ptr
    Public data As s_console_color_list
    Public Function getPtr() As s_console_color_list_ptr
        Return gPtr
    End Function
    Public Sub New(dPtr As s_console_color_list_ptr)
        gPtr = dPtr
        If dPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(s_console_color_list)), s_console_color_list)
        Else
            data = New s_console_color_list()
        End If
    End Sub
    Public Shared Widening Operator CType(dPtr As s_console_color_list_ptr) As s_console_color_list_managed
        Return New s_console_color_list_managed(dPtr)
    End Operator
    Public Sub save()
        If gPtr.ptr <> IntPtr.Zero Then
            Marshal.StructureToPtr(data, gPtr.ptr, False)
        End If
    End Sub
    Public Sub refresh()
        If gPtr.ptr <> IntPtr.Zero Then
            data = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_console_color_list)), s_console_color_list)
        End If
    End Sub
End Structure

'Extras for managed code usage.
'#Pragma warning disable 0169
'#Pragma warning disable 0649
Public Structure UIntPtr
    Private ptr As IntPtr
    Public Property data() As UInteger
        Get
            Return CUInt(Marshal.ReadInt32(ptr))
        End Get
        Set(value As UInteger)
            Marshal.WriteInt32(ptr, CInt(value))
        End Set
    End Property
End Structure
Public Structure BoolPtr
    Private ptr As IntPtr
    Public Property data() As Boolean
        Get
            Return Convert.ToBoolean(Marshal.ReadByte(ptr))
        End Get
        Set(value As Boolean)
            Marshal.WriteByte(ptr, Convert.ToByte(value))
        End Set
    End Property
End Structure
'#Pragma warning restore 0169
'#Pragma warning restore 0649
