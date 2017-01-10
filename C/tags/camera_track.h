//APPROVED
typedef struct s_control_point_block {
    real_vector3d position;
    real_quaternion orientation;
    PADDING(0x20);
} s_control_point_block;
static_assert_check(sizeof(s_control_point_block) == 0x3C, "Incorrect size of s_control_point_block");

typedef struct s_camera_track_meta {
    //unsigned int flags; //Offset 0x00 is always zeroes... Might be for Halo 2 only?
    PADDING(0x04);
    s_tag_block control_points; //Max is 16
    PADDING(0x20);
} s_camera_track_meta;
static_assert_check(sizeof(s_camera_track_meta) == 0x30, "Incorrect size of s_camera_track_meta");
