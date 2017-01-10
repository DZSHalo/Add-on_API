//APPROVED
import D.cseries.cseries;
import D.tags.tag_include;

struct s_antenna_vertices_block {
    _real                spring_strength_coefficent;
    char[0x18] PADDING0;
    real_rotation2d     angles;
    _real                length_world_units;
    short               sequence_index;
    char[0x02] PADDING1;
    real_color_alpha    color;
    real_color_alpha    LOD_color;
    char[0x34] PADDING2;
};
static assert(s_antenna_vertices_block.sizeof == 0x80, "Incorrect size of s_antenna_vertices_block");

struct s_antenna_meta {
    char[0x20]      attachment_marker_name;
    s_tag_reference bitmaps;    //bitm
    s_tag_reference physics;    //pphy
    char[0x50] PADDING0;
    _real            spring_strength_coefficent;
    _real            falloff_pixels;
    _real            cutoff_pixels;
    char[0x28] PADDING1;
    //Vertices block
    s_tag_block     vertices; //Max is 20
};
static assert(s_antenna_meta.sizeof == 0xD0, "Incorrect size of s_antenna_meta");