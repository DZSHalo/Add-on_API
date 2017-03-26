Imports System.Runtime.InteropServices

Namespace Addon_API
    <StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Structure hTagIndexTableHeader
        Public next_ptr As UInteger
        Public starting_index As UInteger
        Public unk As UInteger
        Public entityCount As UInteger
        Public unk1 As UInteger
        Public readOffset As UInteger
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=8)>
        Public unk2 As String
        Public readSize As UInteger
        Public unk3 As UInteger
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Structure hTagHeader
        Public group_tag As e_tag_group
        <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=2)>
        Public parent_tags As e_tag_group()
        Public ident As s_ident
        <MarshalAsAttribute(UnmanagedType.LPStr)>
        Public tag_name As String
        Public group_meta_tag As System.IntPtr
        <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=2)>
        Public parent_meta_tag As System.IntPtr()
    End Structure
    Public Structure hTagHeaderPtr
        Public ptr As IntPtr
        Public Sub New(data As IntPtr)
            ptr = data
        End Sub
    End Structure
    Public Structure hTagHeader_managed
        Private gPtr As hTagHeaderPtr
        Public hTagHeader_n As hTagHeader
        Public Property nPtr() As hTagHeaderPtr
            Private Get
                Return gPtr
            End Get
            Set(value As hTagHeaderPtr)
                gPtr = value
                If value.ptr <> IntPtr.Zero Then
                    hTagHeader_n = CType(Marshal.PtrToStructure(value.ptr, GetType(hTagHeader)), hTagHeader)
                Else
                    hTagHeader_n = New hTagHeader()
                End If
            End Set
        End Property
        Public Sub New(ByRef data As hTagHeader_managed)
            gPtr = data.gPtr
            hTagHeader_n = data.hTagHeader_n
        End Sub
        Public Sub New(dPtr As hTagHeaderPtr)
            gPtr = dPtr
            If dPtr.ptr <> IntPtr.Zero Then
                hTagHeader_n = CType(Marshal.PtrToStructure(dPtr.ptr, GetType(hTagHeader)), hTagHeader)
            Else
                hTagHeader_n = New hTagHeader()
            End If
        End Sub
        Public Shared Widening Operator CType(dPtr As hTagHeaderPtr) As hTagHeader_managed
            Return New hTagHeader_managed(dPtr)
        End Operator
        Public Sub Save()
            If gPtr.ptr <> IntPtr.Zero Then
                Marshal.StructureToPtr(hTagHeader_n, gPtr.ptr, False)
            End If
        End Sub
        Public Sub Refresh()
            If gPtr.ptr <> IntPtr.Zero Then
                hTagHeader_n = CType(Marshal.PtrToStructure(gPtr.ptr, GetType(s_object)), hTagHeader)
            End If
        End Sub
        Public Function isNull() As Boolean
            Return gPtr.ptr = IntPtr.Zero
        End Function
    End Structure

    <Flags>
    Public Enum objDamageFlags As UInteger
        NONE = 0
        isExplode = 1 << 0
        Unknown0 = 2 << 1
        Unknown1 = 3 << 1
        isWeapon = 4 << 1
        Unknown2 = 5 << 1
        Unknown3 = 6 << 1
        Unknown4 = 7 << 1
        Unknown5 = 8 << 1
        Unknown6 = 2
        Unknown7 = 3
        Unknown8 = 4
    End Enum

    <StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Structure objDamageInfo
        Public tag_id As s_ident
        Public flags As objDamageFlags
        Public player_causer As s_ident
        Public causer As s_ident
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=48)>
        Public Unknown0 As String
        Public modifier As Single
        Public modifier1 As Single
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=8)>
        Public Unknown1 As String
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
    Public Structure objHitInfo
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=32)>
        Public desc As String
        <MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst:=40)>
        Public Unknown0 As String
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Structure objManaged
        Public world As real_vector3d
        Public velocity As real_vector3d
        Public rotation As real_vector3d
        Public scale As real_vector3d
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Structure objCreationInfo
        Public map_id As s_ident
        Public parent_id As s_ident
        Public pos As real_vector3d
    End Structure

#If EXT_IOBJECT Then
    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Class objTagGroupList
        Implements IDisposable
        Public count As UInteger
        Private tag_list As System.IntPtr
        Public Function list(iterator As UInteger) As hTagHeader_managed
            Dim i As New hTagHeader_managed()
            If iterator < count Then
                i.nPtr = New hTagHeaderPtr(CType(Marshal.ReadInt32(New IntPtr(iterator * 4 + tag_list.ToInt32())), IntPtr))
            End If
            Return i
        End Function
        Protected Overrides Sub Finalize()
            Try
                Dispose(False)
            Finally
                MyBase.Finalize()
            End Try
        End Sub
        Protected Sub Dispose(disposing As Boolean)
#If EXT_IUTIL Then
            If tag_list <> IntPtr.Zero Then
                [Global].pIUtil.m_freeMem(tag_list)
                tag_list = IntPtr.Zero
            End If
#Else
            #Error EXT_IUTIL is a requirement for using objTagGroupList structure.
#End If
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
    End Class

    Public Structure IObjectPtr
        Public ptr As IntPtr
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Structure IObject
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_address(<[In]> obj_id As s_ident) As s_objectPtr
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_lookup_tag(<[In]> objectTag As s_ident) As hTagHeaderPtr
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_lookup_tag_type_name(<[In]> group_tag As e_tag_group, <[In]> <MarshalAsAttribute(UnmanagedType.LPStr)> tag_name As String) As hTagHeaderPtr
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_destroy(<[In]> obj_id As s_ident) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_copy(<[In]> ByRef model_Tag As s_ident, <[In]> plI As PlayerInfo) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_eject(<[In]> obj_id As s_ident) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_update(<[In]> obj_id As s_ident)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_kill(<[In]> obj_id As s_ident)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_create(<[In]> model_Tag As s_ident, <[In]> parentId As s_ident, idlingTime As Integer, <[In], Out> ByRef out_objId As s_ident, <[In]> ByRef location As real_vector3d) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_equipment_assign(<[In]> biped_id As s_ident, <[In]> obj_id As s_ident) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_move(<[In]> obj_id As s_ident, <[In]> obj_setup As objManaged)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_equipment_drop_current(<[In]> biped_id As s_ident) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_move_and_reset(<[In]> obj_id As s_ident, <[In]> ByRef location As real_vector3d)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_set_object_spawn_player_x(<[In]> pl_ind As Byte)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_get_lookup_group_tag_list(<[In]> tag_group As e_tag_group, <[In], Out> tag_list As objTagGroupList) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_apply_damage_generic(<[In]> receiver As s_ident, <[In]> causer As s_ident, <[In]> multiply As Single, <[In]> flags As objDamageFlags) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_apply_damage_custom(<[In]> receiver As s_ident, <[In]> causer As s_ident, <[In], Out> tag As hTagHeaderPtr, <[In]> multiply As Single, <[In]> flags As objDamageFlags) As <MarshalAs(UnmanagedType.I1)> Boolean

        ''' <summary>
        ''' Get pointer of object's active structure.
        ''' </summary>
        ''' <param name="obj_id">Unique s_ident of an object created.</param>
        ''' <returns>Return pointer of object's active structure or null.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_address As d_get_address
        ''' <summary>
        ''' Lookup tag object and return object's tag header.
        ''' </summary>
        ''' <param name="objectTag">Unique asset tag s_ident.</param>
        ''' <returns>Return pointer of tag header of an asset tag.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_lookup_tag As d_lookup_tag
        ''' <summary>
        ''' Lookup tag object by type and name of a tag.
        ''' </summary>
        ''' <param name="tagType">Type of tag.</param>
        ''' <param name="tag">Name of an asset tag.</param>
        ''' <returns>Return pointer of tag header of an asset tag.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_lookup_tag_type_name As d_lookup_tag_type_name
        ''' <summary>
        ''' To destroy an existing object.
        ''' </summary>
        ''' <param name="obj_id">Unique s_ident of an object created.</param>
        ''' <returns>Return true if successful destruction, false if unable to destroy.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_destroy As d_destroy
        ''' <summary>
        ''' To copy existing object at specific player.
        ''' </summary>
        ''' <param name="model_Tag">Unique asset tag s_ident.</param>
        ''' <param name="plI">PlayerInfo</param>
        ''' <returns>Return true or false if unable to copy.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_copy As d_copy
        ''' <summary>
        ''' Eject object, usually bipeds, from enterable object. (NOTE: This does not instant eject object if there's an eject animation involved.)
        ''' </summary>
        ''' <param name="obj_id">Unique s_ident of an object created.</param>
        ''' <returns>Return true or false if unable to eject.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_eject As d_eject
        ''' <summary>
        ''' Update an object action to players. (Currently supported for ammo count and shield.)
        ''' </summary>
        ''' <param name="obj_id">Unique s_ident of an object created.</param>
        ''' <returns>Does not return any value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_update As d_update
        ''' <summary>
        ''' To kill an object, usually bipeds, with existing health.
        ''' </summary>
        ''' <param name="obj_id">Unique s_ident of an object created.</param>
        ''' <returns>Does not return any value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_kill As d_kill
        ''' <summary>
        ''' To create an object.
        ''' </summary>
        ''' <param name="model_Tag">Unique asset tag s_ident.</param>
        ''' <param name="parentId">Owner of an object.</param>
        ''' <param name="idlingTime">How much time, in ticks, idling permitted before remove from arena.</param>
        ''' <param name="out_objId">Unique s_ident of an object creation.</param>
        ''' <param name="location">Location to spawn at.</param>
        ''' <returns>Return true or false if unable to create an object.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_create As d_create
        ''' <summary>
        ''' Assign equipment to biped.
        ''' </summary>
        ''' <param name="biped_id">Unique s_ident of an biped created.</param>
        ''' <param name="obj_id">Unique s_ident of an object created.</param>
        ''' <returns>Return true or false if unable to assign equipment.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_equipment_assign As d_equipment_assign
        ''' <summary>
        ''' Move an object to another location.
        ''' </summary>
        ''' <param name="obj_id">Unique s_ident of an object created.</param>
        ''' <param name="obj_setup"></param>
        ''' <returns>Does not return any value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_move As d_move
        ''' <summary>
        ''' Drop current equipment from biped.
        ''' </summary>
        ''' <param name="biped_id">Unique s_ident of an biped created.</param>
        ''' <returns>Return true or false if unable to drop current equipment.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_equipment_drop_current As d_equipment_drop_current
        ''' <summary>
        ''' Move and reset an object.
        ''' </summary>
        ''' <param name="obj_id">Unique s_ident of an object created.</param>
        ''' <param name="location">Location to move at.</param>
        ''' <returns>Does not return any value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_move_and_reset As d_move_and_reset
        ''' <summary>
        ''' Set object, usually cheats, to specific player. NOTE: Make sure you set it back to zero after you're done using it!
        ''' </summary>
        ''' <param name="pl_ind">Player index</param>
        ''' <returns>Does not return any value.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_set_object_spawn_player_x As d_set_object_spawn_player_x
        ''' <summary>
        ''' Obtain list of specific object tags.
        ''' </summary>
        ''' <param name="tag_group">Find specific object tag group.</param>
        ''' <param name="tag_list">Output list of specific object tags.</param>
        ''' <returns>Return true or false if unable to find tag group.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_get_lookup_group_tag_list As d_get_lookup_group_tag_list
        ''' <summary>
        ''' Apply damage to specific object. (WARNING: May Not be safe to use on certain custom maps.)
        ''' </summary>
        ''' <param name="receiver">An object receive the damage.</param>
        ''' <param name="causer">An object cause the damage.</param>
        ''' <param name="multiply">Mulitply the damage.</param>
        ''' <param name="flags">Type of damage flags</param>
        ''' <returns>Return true Or false if unable to apply generic damage.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_apply_damage_generic As d_apply_damage_generic
        ''' <summary>
        ''' Apply damage to specific object.
        ''' </summary>
        ''' <param name="receiver">An object receive the damage.</param>
        ''' <param name="causer">An object cause the damage.</param>
        ''' <param name="tag">Apply type of tag damage to receiver.</param>
        ''' <param name="multiply">Mulitply the damage.</param>
        ''' <param name="flags">Type of damage flags</param>
        ''' <returns>Return true or false if unable to apply generic damage.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_apply_damage_custom As d_apply_damage_custom


        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As IObjectPtr) As IObject
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(IObject)), IObject) 'New IObject(data)
            Else
                Return New IObject
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return m_get_address IsNot Nothing
        End Function
    End Structure
    Partial Public Structure [Interface]
        ''' <summary>
        ''' Returns a IObject class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IObject class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint:="#11", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Function getIObject(<[In]> uniqueHash As UInteger) As IObjectPtr
        End Function
    End Structure
#End If
End Namespace
