using System;
using System.Runtime.InteropServices;

public struct s_tag_header_ptr {
    public IntPtr ptr;
}
public struct s_tag_header_managed {
    private s_tag_header_ptr gPtr;
    public s_tag_header data;
    public s_tag_header_ptr getPtr() { return gPtr; }
    public s_tag_header_managed(s_tag_header_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_tag_header)Marshal.PtrToStructure(dPtr.ptr, typeof(s_tag_header));
        else
            data = new s_tag_header();
    }
    static public implicit operator s_tag_header_managed(s_tag_header_ptr dPtr) {
        return new s_tag_header_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_tag_header)Marshal.PtrToStructure(gPtr.ptr, typeof(s_tag_header));
    }
};

public struct s_tag_reference_ptr {
    public IntPtr ptr;
}
public struct s_tag_reference_managed {
    private s_tag_reference_ptr gPtr;
    public s_tag_reference data;
    public s_tag_reference_ptr getPtr() { return gPtr; }
    public s_tag_reference_managed(s_tag_reference_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_tag_reference)Marshal.PtrToStructure(dPtr.ptr, typeof(s_tag_reference));
        else
            data = new s_tag_reference();
    }
    static public implicit operator s_tag_reference_managed(s_tag_reference_ptr dPtr) {
        return new s_tag_reference_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_tag_reference)Marshal.PtrToStructure(gPtr.ptr, typeof(s_tag_reference));
    }
};

public struct s_tag_block_definition_ptr {
    public IntPtr ptr;
}
public struct s_tag_block_definition_managed {
    private s_tag_block_definition_ptr gPtr;
    public s_tag_block_definition data;
    public s_tag_block_definition_ptr getPtr() { return gPtr; }
    public s_tag_block_definition_managed(s_tag_block_definition_ptr dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (s_tag_block_definition)Marshal.PtrToStructure(dPtr.ptr, typeof(s_tag_block_definition));
        else
            data = new s_tag_block_definition();
    }
    static public implicit operator s_tag_block_definition_managed(s_tag_block_definition_ptr dPtr) {
        return new s_tag_block_definition_managed(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (s_tag_block_definition)Marshal.PtrToStructure(gPtr.ptr, typeof(s_tag_block_definition));
    }
};

/*
public struct s_tag_block {
};
/*
[StructLayout(LayoutKind.Sequential)]
public struct s_tag_group {
    //TODO: Is this needed?
};*/
