Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential)>
Public Structure s_tag_header
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H24)>
    Public unknown1 As Byte
    Public random_number1 As UInteger
    Public header_size As UInteger
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H8)>
    Public unknown2 As Byte
    Public tag_version As Short
    Public engine_version As Short
    Public engine_name As UInteger
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure s_tag_reference
    Public group_tag As e_tag_group
    <MarshalAs(UnmanagedType.LPStr)>
    Public name As String 'tag_name_reference
    Public name_length As UInteger 'tag_name_length, Excluding null terminate (Is not in used ingame)
    Public tag_index As s_ident 'Always -1 in Guerilla, is used ingame.
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure s_tag_block_definition
    'TODO: Need to research again since didn't investigate this for long time.
    Public noResearchDone As UInteger 'TODO: No research done
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure s_tag_block
    Public count As Integer
    Public address As IntPtr
    Public definition As s_tag_block_definition_ptr
End Structure
'
'[StructLayout(LayoutKind.Sequential)]
'public struct s_tag_group {
'    //TODO: Is this needed?
'};
