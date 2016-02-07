//APPROVED
typedef struct s_antenna_vertices_block {
    real                spring_strength_coefficent;
    PADDING(0x18);
    real_rotation2d     angles;
    real                length_world_units;
    short               sequence_index;
    PADDING(0x02);
    real_color_alpha    color;
    real_color_alpha    LOD_color;
    PADDING(0x34);
} s_antenna_vertices_block;
static_assert_check(sizeof(s_antenna_vertices_block) == 0x80, "Incorrect size of s_antenna_vertices_block");

typedef struct s_antenna_meta {
    char            attachment_marker_name[0x20];
    s_tag_reference bitmaps;    //bitm
    s_tag_reference physics;    //pphy
    PADDING(0x50);
    real            spring_strength_coefficent;
    real            falloff_pixels;
    real            cutoff_pixels;
    PADDING(0x28);
    //Vertices block
    s_tag_block     vertices; //Max is 20
} s_antenna_meta;
static_assert_check(sizeof(s_antenna_meta) == 0xD0, "Incorrect size of s_antenna_meta");