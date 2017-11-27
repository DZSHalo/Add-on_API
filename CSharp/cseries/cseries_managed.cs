using System;
using System.Runtime.InteropServices;

public struct s_ident_ptr {
    public IntPtr ptr;
}

public struct s_ident_managed {
    private s_ident_ptr gPtr;
    public s_ident data;
    public s_ident_ptr getPtr() { return gPtr; }
    public s_ident_managed(s_ident_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_ident)Marshal.PtrToStructure(dPtr.ptr, typeof(s_ident));
        else
            data = new s_ident();
    }
    static public implicit operator s_ident_managed(s_ident_ptr dPtr) {
        return new s_ident_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_ident)Marshal.PtrToStructure(gPtr.ptr, typeof(s_ident));
    }
    public bool isNotNull() {
        return gPtr.ptr != IntPtr.Zero;
    }
}
