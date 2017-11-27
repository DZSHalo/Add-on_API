using System;
using System.Runtime.InteropServices;

// TODO: Need to find a way to make generic suppport for ptr access and functions.
/*
public class ptr_managed<P, S> {
    private P gPtr;
    public S data;
    public P getPtr() { return gPtr; }
    public ptr_managed(P dPtr) {
        gPtr = dPtr;
        if (dPtr.ptr != IntPtr.Zero)
            data = (S)Marshal.PtrToStructure(dPtr.ptr, typeof(S));
        else
            data = new S();
    }
    static public implicit operator ptr_managed<P, S>(P dPtr) {
        return new ptr_managed<P, S>(dPtr);
    }
    public void save() {
        if (gPtr.ptr != IntPtr.Zero)
            Marshal.StructureToPtr(data, gPtr.ptr, false);
    }
    public void refresh() {
        if (gPtr.ptr != IntPtr.Zero)
            data = (S)Marshal.PtrToStructure(gPtr.ptr, typeof(S));
    }
}*/
