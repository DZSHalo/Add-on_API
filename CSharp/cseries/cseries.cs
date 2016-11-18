using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct real_range {
    public float min;
    public float max;
}
[StructLayout(LayoutKind.Sequential)]
public struct real_color {
    public float red;
    public float green;
    public float blue;
}
[StructLayout(LayoutKind.Sequential)]
public struct real_color_alpha {
    public float alpha;
    public float red;
    public float green;
    public float blue;
}
[StructLayout(LayoutKind.Sequential)]
public struct real_vector2d {
    public float x;
    public float y;
}
[StructLayout(LayoutKind.Sequential)]
public struct real_rotation2d {
    public float yaw;
    public float pitch;
}
[StructLayout(LayoutKind.Sequential)]
public struct real_vector3d {
    public float x;
    public float y;
    public float z;
}
[StructLayout(LayoutKind.Sequential)]
public struct real_offset3d {
    // X-Component
    public float i;
    // Y-Component
    public float j;
    // Z-Component
    public float k;
}
[StructLayout(LayoutKind.Sequential)]
public struct real_quaternion {
    // X-Component
    public float i;
    // Y-Component
    public float j;
    // Z-Component
    public float k;
    // ?-Component
    public float w;
}
[StructLayout(LayoutKind.Sequential)]
public struct byte_color_alpha {
    public byte alpha;
    public byte red;
    public byte green;
    public byte blue;
}
[StructLayout(LayoutKind.Sequential)]
public struct short_range {
    public short min;
    public short max;
}
[StructLayout(LayoutKind.Sequential)]
public struct short_point {
    public short x;
    public short y;
}
[StructLayout(LayoutKind.Explicit, Size=4)]
public struct s_ident {
    [FieldOffset(0)]
    public int Tag;
    [FieldOffset(0)]
    public short index;
    [FieldOffset(2)]
    public short salt;
    public s_ident(int tag = -1) {
        salt = (short)tag;
        index = (short)tag;
        Tag = tag;
    }
    s_ident(ref s_ident ident) {
        index = ident.index;
        salt = ident.salt;
        Tag = ident.Tag;
    }
}