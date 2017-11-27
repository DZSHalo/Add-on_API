Imports System.Runtime.InteropServices

' tag_name_length = uint;

' tag_name_reference = UnmanagedType.LPStr;

<StructLayout(LayoutKind.Sequential)>
Public Structure real_range
    Public min As Single
    Public max As Single
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure real_color
    Public red As Single
    Public green As Single
    Public blue As Single
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure real_color_alpha
    Public alpha As Single
    Public red As Single
    Public green As Single
    Public blue As Single
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure real_vector2d
    Public x As Single
    Public y As Single
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure real_rotation2d
    Public yaw As Single
    Public pitch As Single
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure real_vector3d
    Public x As Single
    Public y As Single
    Public z As Single
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure real_offset3d
    ' X-Component
    Public i As Single
    ' Y-Component
    Public j As Single
    ' Z-Component
    Public k As Single
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure real_quaternion
    ' X-Component
    Public i As Single
    ' Y-Component
    Public j As Single
    ' Z-Component
    Public k As Single
    ' ?-Component
    Public w As Single
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure byte_color_alpha
    Public alpha As Byte
    Public red As Byte
    Public green As Byte
    Public blue As Byte
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure short_range
    Public min As Short
    Public max As Short
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure short_point
    Public x As Short
    Public y As Short
End Structure
<StructLayout(LayoutKind.Explicit, Size:=4)>
Public Structure s_ident
    <FieldOffset(0)>
    Public Tag As Integer
    <FieldOffset(0)>
    Public index As Short
    <FieldOffset(2)>
    Public salt As Short
    Public Sub New(Optional tag__1 As Integer = -1)
        salt = CShort(tag__1)
        index = CShort(tag__1)
        Tag = tag__1
    End Sub
    Private Sub New(ByRef ident As s_ident)
        index = ident.index
        salt = ident.salt
        Tag = ident.Tag
    End Sub
End Structure
