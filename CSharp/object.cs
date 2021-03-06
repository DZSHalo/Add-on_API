using System;
using System.Runtime.InteropServices;

namespace Addon_API {
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct hTagIndexTableHeader {
        public uint next_ptr;
        public uint starting_index;
        public uint unk;
        public uint entityCount;
        public uint unk1;
        public uint readOffset;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string unk2;
        public uint readSize;
        public uint unk3;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct hTagHeader {
        public e_tag_group group_tag;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
        public e_tag_group[] parent_tags;
        public s_ident ident;
        [MarshalAsAttribute(UnmanagedType.LPStr)]
        public string tag_name;
        public System.IntPtr group_meta_tag;
        [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
        public System.IntPtr[] parent_meta_tag;
    }

    [Flags]
    public enum objDamageFlags : uint {
        NONE = 0,
        isExplode = 1 << 0,
        Unknown0 = 2 << 1,
        Unknown1 = 3 << 1,
        isWeapon = 4 << 1,
        Unknown2 = 5 << 1,
        ignoreShield = 6 << 1,
        Unknown4 = 7 << 1,
        Unknown5 = 8 << 1,
        Unknown6 = 2,
        Unknown7 = 3,
        Unknown8 = 4
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct objDamageInfo {
        public s_ident tag_id;
        public objDamageFlags flags;
        public s_ident player_causer;
        public s_ident causer;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 48)]
        public string Unknown0;
        public float modifier;
        public float modifier1;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string Unknown1;
    }

    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct objHitInfo {
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string desc;
        [MarshalAsAttribute(UnmanagedType.ByValTStr, SizeConst = 40)]
        public string Unknown0;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct objManaged {
        public real_vector3d world;
        public real_vector3d velocity;
        public real_vector3d rotation;
        public real_vector3d scale;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct objCreationInfo {
        public s_ident map_id;
        public s_ident parent_id;
        public real_vector3d pos;
    }

#if EXT_IOBJECT

    public struct IObjectPtr {
        public IntPtr ptr;
    }

    [StructLayoutAttribute(LayoutKind.Sequential)]
    public struct IObject {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate s_objectPtr d_get_address([In] s_ident obj_id);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate hTagHeaderPtr d_lookup_tag([In] s_ident objectTag);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate hTagHeaderPtr d_lookup_tag_type_name([In] e_tag_group group_tag, [In] [MarshalAsAttribute(UnmanagedType.LPStr)] string tag_name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_destroy([In] s_ident obj_id);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_copy([In] ref s_ident model_Tag, [In] PlayerInfo plI);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_eject([In] s_ident obj_id);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_update([In] s_ident obj_id);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_kill([In] s_ident obj_id);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_create([In] s_ident model_Tag, [In] s_ident parentId, int idlingTime, [In, Out] ref s_ident out_objId, [In] ref real_vector3d location);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_equipment_assign([In] s_ident biped_id, [In] s_ident obj_id);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_move([In] s_ident obj_id, [In] objManaged obj_setup);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_equipment_drop_current([In] s_ident biped_id);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_move_and_reset([In] s_ident obj_id, [In] ref real_vector3d location);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void d_set_object_spawn_player_x([In] byte pl_ind);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_get_lookup_group_tag_list([In] e_tag_group tag_group, [In, Out] objTagGroupList tag_list);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_apply_damage_generic([In] s_ident receiver, [In] s_ident causer, [In] float multiply, [In] objDamageFlags flags);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public delegate bool d_apply_damage_custom([In] s_ident receiver, [In] s_ident causer, [In, Out] hTagHeaderPtr tag, [In] float multiply, [In] objDamageFlags flags);

        /// <summary>
        /// Get pointer of object's active structure.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Return pointer of object's active structure or null.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_address m_get_address;
        /// <summary>
        /// Lookup tag object and return object's tag header.
        /// </summary>
        /// <param name="objectTag">Unique asset tag s_ident.</param>
        /// <returns>Return pointer of tag header of an asset tag.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_lookup_tag m_lookup_tag;
        /// <summary>
        /// Lookup tag object by type and name of a tag.
        /// </summary>
        /// <param name="tagType">Type of tag.</param>
        /// <param name="tag">Name of an asset tag.</param>
        /// <returns>Return pointer of tag header of an asset tag.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_lookup_tag_type_name m_lookup_tag_type_name;
        /// <summary>
        /// To destroy an existing object.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Return true if successful destruction, false if unable to destroy.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_destroy m_destroy;
        /// <summary>
        /// To copy existing object at specific player.
        /// </summary>
        /// <param name="model_Tag">Unique asset tag s_ident.</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if unable to copy.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_copy m_copy;
        /// <summary>
        /// Eject object, usually bipeds, from enterable object. (NOTE: This does not instant eject object if there's an eject animation involved.)
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Return true or false if unable to eject.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_eject m_eject;
        /// <summary>
        /// Update an object action to players. (Currently supported for ammo count and shield.)
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_update m_update;
        /// <summary>
        /// To kill an object, usually bipeds, with existing health.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_kill m_kill;
        /// <summary>
        /// To create an object.
        /// </summary>
        /// <param name="model_Tag">Unique asset tag s_ident.</param>
        /// <param name="parentId">Owner of an object.</param>
        /// <param name="idlingTime">How much time, in ticks, idling permitted before remove from arena.</param>
        /// <param name="out_objId">Unique s_ident of an object creation.</param>
        /// <param name="location">Location to spawn at.</param>
        /// <returns>Return true or false if unable to create an object.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_create m_create;
        /// <summary>
        /// Assign equipment to biped.
        /// </summary>
        /// <param name="biped_id">Unique s_ident of an biped created.</param>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Return true or false if unable to assign equipment.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_equipment_assign m_equipment_assign;
        /// <summary>
        /// Move an object to another location.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <param name="obj_setup"></param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_move m_move;
        /// <summary>
        /// Drop current equipment from biped.
        /// </summary>
        /// <param name="biped_id">Unique s_ident of an biped created.</param>
        /// <returns>Return true or false if unable to drop current equipment.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_equipment_drop_current m_equipment_drop_current;
        /// <summary>
        /// Move and reset an object.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <param name="location">Location to move at.</param>
        /// <returns>Does not return any value.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_move_and_reset m_move_and_reset;
        /// <summary>
        /// Obtain list of specific object tags.
        /// </summary>
        /// <param name="tag_group">Find specific object tag group.</param>
        /// <param name="tag_list">Output list of specific object tags.</param>
        /// <returns>Return true or false if unable to find tag group.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_set_object_spawn_player_x m_set_object_spawn_player_x;
        /// <summary>
        /// Obtain list of specific object tags.
        /// </summary>
        /// <param name="tag_group">Find specific object tag group.</param>
        /// <param name="tag_list">Output list of specific object tags.</param>
        /// <returns>Return true or false if unable to find tag group.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_get_lookup_group_tag_list m_get_lookup_group_tag_list;
        /// <summary>
        /// Apply damage to specific object. (WARNING: May not be safe to use on certain custom maps.)
        /// </summary>
        /// <param name="receiver">An object receive the damage.</param>
        /// <param name="causer">An object cause the damage.</param>
        /// <param name="multiply">Mulitply the damage.</param>
        /// <param name="flags">Type of damage flags</param>
        /// <returns>Return true or false if unable to apply generic damage.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_apply_damage_generic m_apply_damage_generic;
        /// <summary>
        /// Apply damage to specific object.
        /// </summary>
        /// <param name="receiver">An object receive the damage.</param>
        /// <param name="causer">An object cause the damage.</param>
        /// <param name="tag">Apply type of tag damage to receiver.</param>
        /// <param name="multiply">Mulitply the damage.</param>
        /// <param name="flags">Type of damage flags</param>
        /// <returns>Return true or false if unable to apply generic damage.</returns>
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public d_apply_damage_custom m_apply_damage_custom;


        //Simple & easier user-defined conversion + checker for null.
        public IObject(IObjectPtr data) {
            if (data.ptr != IntPtr.Zero)
                this = (IObject)Marshal.PtrToStructure(data.ptr, typeof(IObject));
            else
                this = new IObject();
        }
        public static implicit operator IObject(IObjectPtr data) {
            return new IObject(data);
        }
        public bool isNotNull() {
            return m_get_address != null;
        }
    }
    public partial struct Interface {
        /// <summary>
        /// Returns a IObject class-like to add support for later execution when needed.
        /// </summary>
        /// <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        /// <returns>Pointer of IObject class-like.</returns>
        [DllImport("H-Ext.dll", EntryPoint = "#11", CallingConvention = CallingConvention.Cdecl)]
        [ComVisible(true)]
        public static extern IObjectPtr getIObject([In] uint uniqueHash);
    }
#endif
}
