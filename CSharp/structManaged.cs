using System;
using System.Runtime.InteropServices;

public struct s_player_reserved_slot_managed {
    private s_player_reserved_slot_ptr gPtr;
    private IntPtr slotPtr;
    private uint slot;
    public s_player_reserved_slot data;
    public s_player_reserved_slot_ptr getPtr() { return new s_player_reserved_slot_ptr() { ptr = slotPtr }; }
    public s_player_reserved_slot_ptr getGlobalPtr() { return gPtr; }
    public s_player_reserved_slot_managed(s_player_reserved_slot_ptr dPtr) {
        gPtr = dPtr;
        slotPtr = dPtr.ptr;
        slot = 0;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_player_reserved_slot)Marshal.PtrToStructure(slotPtr, typeof(s_player_reserved_slot));
        else
            data = new s_player_reserved_slot();
    }
    static public implicit operator s_player_reserved_slot_managed(s_player_reserved_slot_ptr dPtr) {
        return new s_player_reserved_slot_managed(dPtr);
    }
    public void save() {
        if (slotPtr != IntPtr.Zero)
            Marshal.StructureToPtr(data, slotPtr, false);
    }
    public void refresh() {
        if (slotPtr != IntPtr.Zero)
            data = (s_player_reserved_slot)Marshal.PtrToStructure(slotPtr, typeof(s_player_reserved_slot));
    }
    //Extended version for arrays usage.
    public void setSlot(uint slotIndex) {
        if (slotIndex > 15)
            throw new IndexOutOfRangeException("slotIndex is out of range.");
        slot = slotIndex;
        slotPtr = new IntPtr(gPtr.ptr.ToInt32() + slotIndex * Marshal.SizeOf(typeof(s_player_reserved_slot)));
        refresh();
    }
    public static s_player_reserved_slot_managed operator ++(s_player_reserved_slot_managed pRSM) {
        pRSM.setSlot(pRSM.slot + 1);
        return pRSM;
    }
    public static s_player_reserved_slot_managed operator --(s_player_reserved_slot_managed pRSM) {
        pRSM.setSlot(pRSM.slot - 1);
        return pRSM;
    }

}

public struct s_machine_slot_managed {
    private s_machine_slot_ptr gPtr;
    private IntPtr slotPtr;
    private uint slot;
    public s_machine_slot data;
    public s_machine_slot_ptr getPtr() { return new s_machine_slot_ptr() {ptr =slotPtr}; }
    public s_machine_slot_ptr getGlobalPtr() { return gPtr; }
    public s_machine_slot_managed(s_machine_slot_ptr dPtr) {
        gPtr = dPtr;
        slotPtr = dPtr.ptr;
        slot = 0;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_machine_slot)Marshal.PtrToStructure(slotPtr, typeof(s_machine_slot));
        else
            data = new s_machine_slot();
    }
    static public implicit operator s_machine_slot_managed(s_machine_slot_ptr dPtr) {
        return new s_machine_slot_managed(dPtr);
    }
    public void save() {
        if (slotPtr != IntPtr.Zero)
            Marshal.StructureToPtr(data, slotPtr, false);
    }
    public void refresh() {
        if (slotPtr != IntPtr.Zero)
            data = (s_machine_slot)Marshal.PtrToStructure(slotPtr, typeof(s_machine_slot));
    }
    //Extended version for arrays usage.
    public void setSlot(uint slotIndex) {
        if (slotIndex > 15)
            throw new IndexOutOfRangeException("slotIndex is out of range.");
        slot = slotIndex;
        slotPtr = new IntPtr(gPtr.ptr.ToInt32() + slotIndex * Marshal.SizeOf(typeof(s_machine_slot)));
        refresh();
    }
    public static s_machine_slot_managed operator ++(s_machine_slot_managed mSM) {
        mSM.setSlot(mSM.slot + 1);
        return mSM;
    }
    public static s_machine_slot_managed operator --(s_machine_slot_managed mSM) {
        mSM.setSlot(mSM.slot - 1);
        return mSM;
    }

}

public struct s_player_slot_managed {
    private s_player_slot_ptr gPtr;
    private IntPtr slotPtr;
    private uint slot;
    public s_player_slot data;
    public s_player_slot_ptr getPtr() { return new s_player_slot_ptr() { ptr = slotPtr }; }
    public s_player_slot_ptr getGlobalPtr() { return gPtr; }
    public s_player_slot_managed(s_player_slot_ptr dPtr) {
        gPtr = dPtr;
        slotPtr = dPtr.ptr;
        slot = 0;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_player_slot)Marshal.PtrToStructure(slotPtr, typeof(s_player_slot));
        else
            data = new s_player_slot();
    }
    static public implicit operator s_player_slot_managed(s_player_slot_ptr dPtr) {
        return new s_player_slot_managed(dPtr);
    }
    public void save() {
        if (slotPtr != IntPtr.Zero)
            Marshal.StructureToPtr(data, slotPtr, false);
    }
    public void refresh() {
        if (slotPtr != IntPtr.Zero)
            data = (s_player_slot)Marshal.PtrToStructure(slotPtr, typeof(s_player_slot));
    }
    //Extended version for arrays usage.
    public void setSlot(uint slotIndex) {
        if (slotIndex > 15)
            throw new IndexOutOfRangeException("slotIndex is out of range.");
        slot = slotIndex;
        slotPtr = new IntPtr(gPtr.ptr.ToInt32() + slotIndex * Marshal.SizeOf(typeof(s_player_slot)));
        refresh();
    }
    public static s_player_slot_managed operator ++(s_player_slot_managed pSM) {
        pSM.setSlot(pSM.slot + 1);
        return pSM;
    }
    public static s_player_slot_managed operator --(s_player_slot_managed pSM) {
        pSM.setSlot(pSM.slot - 1);
        return pSM;
    }

}

public struct s_gametype_managed {
    private s_gametype_ptr gPtr;
    public s_gametype data;
    public s_gametype_ptr getPtr() { return gPtr; }
    public s_gametype_managed(s_gametype_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_gametype)Marshal.PtrToStructure(dPtr.ptr, typeof(s_gametype));
        else
            data = new s_gametype();
    }
    static public implicit operator s_gametype_managed(s_gametype_ptr dPtr) {
        return new s_gametype_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_gametype)Marshal.PtrToStructure(gPtr.ptr, typeof(s_gametype));
    }
}

public struct s_gametype_gflag_managed {
    private s_gametype_gflag_ptr gPtr;
    public s_gametype_gflag data;
    public s_gametype_gflag_ptr getPtr() { return gPtr; }
    public s_gametype_gflag_managed(s_gametype_gflag_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_gametype_gflag)Marshal.PtrToStructure(dPtr.ptr, typeof(s_gametype_gflag));
        else
            data = new s_gametype_gflag();
    }
    static public implicit operator s_gametype_gflag_managed(s_gametype_gflag_ptr dPtr) {
        return new s_gametype_gflag_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_gametype_gflag)Marshal.PtrToStructure(gPtr.ptr, typeof(s_gametype_gflag));
    }
}

public struct s_gametype_globals_managed {
    private s_gametype_globals_ptr gPtr;
    public s_gametype_globals data;
    public s_gametype_globals_ptr getPtr() { return gPtr; }
    public s_gametype_globals_managed(s_gametype_globals_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_gametype_globals)Marshal.PtrToStructure(dPtr.ptr, typeof(s_gametype_globals));
        else
            data = new s_gametype_globals();
    }
    static public implicit operator s_gametype_globals_managed(s_gametype_globals_ptr dPtr) {
        return new s_gametype_globals_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_gametype_globals)Marshal.PtrToStructure(gPtr.ptr, typeof(s_gametype_globals));
    }
}

public struct s_server_header_managed {
    private s_server_header_ptr gPtr;
    public s_server_header data;
    public s_server_header_ptr getPtr() { return gPtr; }
    public s_server_header_managed(s_server_header_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_server_header)Marshal.PtrToStructure(dPtr.ptr, typeof(s_server_header));
        else
            data = new s_server_header();
    }
    static public implicit operator s_server_header_managed(s_server_header_ptr dPtr) {
        return new s_server_header_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_server_header)Marshal.PtrToStructure(gPtr.ptr, typeof(s_server_header));
    }
}

public struct s_object_managed {
    private s_objectPtr gPtr;
    public s_object s_object_n;
    public s_objectPtr getPtr() { return gPtr; }
    public s_object_managed(s_objectPtr data) {
        gPtr = data;
        if (data.ptr != IntPtr.Zero)
            s_object_n = (s_object)Marshal.PtrToStructure(data.ptr, typeof(s_object));
        else
            s_object_n = new s_object();
    }
    static public implicit operator s_object_managed(s_objectPtr data) {
        return new s_object_managed(data);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(s_object_n, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            s_object_n = (s_object)Marshal.PtrToStructure(gPtr.ptr, typeof(s_object));
    }
}

public struct s_biped_managed {
    private s_objectPtr gPtr;
    public s_biped s_object_n;
    public s_objectPtr getPtr() { return gPtr; }
    public s_biped_managed(s_objectPtr data) {
        gPtr = data;
        if (data.ptr != IntPtr.Zero)
            s_object_n = (s_biped)Marshal.PtrToStructure(data.ptr, typeof(s_biped));
        else
            s_object_n = new s_biped();
    }
    static public implicit operator s_biped_managed(s_objectPtr data) {
        return new s_biped_managed(data);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(s_object_n, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            s_object_n = (s_biped)Marshal.PtrToStructure(gPtr.ptr, typeof(s_biped));
    }
}
//TODO: Need to put s_biped, s_weapon, and s_vehicle in here before production release

public struct s_map_header_managed {
    private s_map_header_ptr gPtr;
    public s_map_header data;
    public s_map_header_ptr getPtr() { return gPtr; }
    public s_map_header_managed(s_map_header_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_map_header)Marshal.PtrToStructure(dPtr.ptr, typeof(s_map_header));
        else
            data = new s_map_header();
    }
    static public implicit operator s_map_header_managed(s_map_header_ptr dPtr) {
        return new s_map_header_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_map_header)Marshal.PtrToStructure(gPtr.ptr, typeof(s_map_header));
    }
}

public struct s_map_status_managed {
    private s_map_status_ptr gPtr;
    public s_map_status data;
    public s_map_status_ptr getPtr() { return gPtr; }
    public s_map_status_managed(s_map_status_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_map_status)Marshal.PtrToStructure(Marshal.ReadIntPtr(gPtr.ptr), typeof(s_map_status));
        else
            data = new s_map_status();
    }
    static public implicit operator s_map_status_managed(s_map_status_ptr dPtr) {
        return new s_map_status_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, Marshal.ReadIntPtr(gPtr.ptr), false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_map_status)Marshal.PtrToStructure(Marshal.ReadIntPtr(gPtr.ptr), typeof(s_map_status));
    }
}

public struct s_console_header_managed {
    private s_console_header_ptr gPtr;
    public s_console_header data;
    public s_console_header_ptr getPtr() { return gPtr; }
    public s_console_header_managed(s_console_header_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_console_header)Marshal.PtrToStructure(dPtr.ptr, typeof(s_console_header));
        else
            data = new s_console_header();
    }
    static public implicit operator s_console_header_managed(s_console_header_ptr dPtr) {
        return new s_console_header_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_console_header)Marshal.PtrToStructure(gPtr.ptr, typeof(s_console_header));
    }
}

//Extras for Add-on API usage.

public struct s_cheat_header_managed {
    private s_cheat_header_ptr gPtr;
    public s_cheat_header data;
    public s_cheat_header_ptr getPtr() { return gPtr; }
    public s_cheat_header_managed(s_cheat_header_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_cheat_header)Marshal.PtrToStructure(dPtr.ptr, typeof(s_cheat_header));
        else
            data = new s_cheat_header();
    }
    static public implicit operator s_cheat_header_managed(s_cheat_header_ptr dPtr) {
        return new s_cheat_header_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_cheat_header)Marshal.PtrToStructure(gPtr.ptr, typeof(s_cheat_header));
    }
}

public struct D3DCOLOR_COLORVALUE_ARGB_managed {
    private D3DCOLOR_COLORVALUE_ARGB_ptr gPtr;
    public D3DCOLOR_COLORVALUE_ARGB data;
    public D3DCOLOR_COLORVALUE_ARGB_ptr getPtr() { return gPtr; }
    public D3DCOLOR_COLORVALUE_ARGB_managed(D3DCOLOR_COLORVALUE_ARGB_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (D3DCOLOR_COLORVALUE_ARGB)Marshal.PtrToStructure(dPtr.ptr, typeof(D3DCOLOR_COLORVALUE_ARGB));
        else
            data = new D3DCOLOR_COLORVALUE_ARGB();
    }
    static public implicit operator D3DCOLOR_COLORVALUE_ARGB_managed(D3DCOLOR_COLORVALUE_ARGB_ptr dPtr) {
        return new D3DCOLOR_COLORVALUE_ARGB_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (D3DCOLOR_COLORVALUE_ARGB)Marshal.PtrToStructure(gPtr.ptr, typeof(D3DCOLOR_COLORVALUE_ARGB));
    }
}
public struct s_console_color_list_managed {
    private s_console_color_list_ptr gPtr;
    public s_console_color_list data;
    public s_console_color_list_ptr getPtr() { return gPtr; }
    public s_console_color_list_managed(s_console_color_list_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_console_color_list)Marshal.PtrToStructure(dPtr.ptr, typeof(s_console_color_list));
        else
            data = new s_console_color_list();
    }
    static public implicit operator s_console_color_list_managed(s_console_color_list_ptr dPtr) {
        return new s_console_color_list_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_console_color_list)Marshal.PtrToStructure(gPtr.ptr, typeof(s_console_color_list));
    }
}

//Extras for managed code usage.
#pragma warning disable 0169
#pragma warning disable 0649
public struct UIntPtr {
    private IntPtr ptr;
    public uint data {
        get { return (uint)Marshal.ReadInt32(ptr); }
        set { Marshal.WriteInt32(ptr, (int)value); }
    }
}
public struct BoolPtr {
    private IntPtr ptr;
    public bool data {
        get { return Convert.ToBoolean(Marshal.ReadByte(ptr)); }
        set { Marshal.WriteByte(ptr, Convert.ToByte(value)); }
    }
}
#pragma warning restore 0169
#pragma warning restore 0649
