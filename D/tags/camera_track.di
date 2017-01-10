//APPROVED
import D.cseries.cseries;
import D.tags.tag_include;

struct s_control_point_block {
    real_vector3d position;
    real_quaternion orientation;
    char[0x20] PADDING;
};
static assert(s_control_point_block.sizeof == 0x3C, "Incorrect size of s_control_point_block");

struct s_camera_track_meta {
    //unsigned int flags; //Offset 0x00 is always zeroes... Might be for Halo 2 only?
    char[0x04] PADDING0;
    s_tag_block control_points; //Max is 16
    char[0x20] PADDING1;
};
static assert(s_camera_track_meta.sizeof == 0x30, "Incorrect size of s_camera_track_meta");