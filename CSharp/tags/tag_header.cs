using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]
public struct s_tag_header {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x24)]
    public byte     unknown1;
    public uint     random_number1;
    public uint     header_size;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x08)]
    public byte     unknown2;
    public short    tag_version;
    public short    engine_version;
    public uint     engine_name;
};

[StructLayout(LayoutKind.Sequential)]
public struct s_tag_reference {
    public e_tag_group  group_tag;
    [MarshalAs(UnmanagedType.LPStr)]
    public string       name;           //tag_name_reference
    public uint         name_length;    //tag_name_length, Excluding null terminate (Is not in used ingame)
    public s_ident      tag_index;      //Always -1 in Guerilla, is used ingame.
};

[StructLayout(LayoutKind.Sequential)]
public struct s_tag_block_definition {
    //TODO: Need to research again since didn't investigate this for long time.
    public uint noResearchDone; //TODO: No research done
};

[StructLayout(LayoutKind.Sequential)]
public struct s_tag_block {
    public int                          count;
    public IntPtr                       address;
    public s_tag_block_definition_ptr   definition;
};
/*
[StructLayout(LayoutKind.Sequential)]
public struct s_tag_group {
    //TODO: Is this needed?
};*/
