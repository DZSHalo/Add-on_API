//APPROVED
enum e_trailing_edge_shape :unsigned short {
    TRAILING_EDGE_SHAPE_FLAT=0,
    TRAILING_EDGE_SHAPE_CONCAVE_TRIANGULAR,
    TRAILING_EDGE_SHAPE_CONVEX_TRIANGULAR,
    TRAILING_EDGE_SHAPE_TRAPEZOID_SHORT_TOP,
    TRAILING_EDGE_SHAPE_TRAPEZOID_SHORT_BOTTOM
};

enum e_attached_edge_shape :unsigned short {
    ATTACHED_EDGE_SHAPE_FLAT=0,
    ATTACHED_EDGE_SHAPE_CONCAVE_TRIANGULAR
};

typedef struct s_attachment_point {
    signed short    height_to_next_attachment_vertices;
    PADDING(0x02);
    PADDING(0x010);
    char            name[0x20];
} s_attachment_point;
static_assert_check(sizeof(s_attachment_point) == 0x34, "Incorrect size of s_flag_meta");

typedef struct s_flag_meta {
    PADDING(0x04);
    e_trailing_edge_shape   trailing_edge_shape;
    signed short            trailing_edge_shape_offset;
    e_attached_edge_shape   attached_edge_shape;
    PADDING(0x02);
    signed short            width_vertices;
    signed short            height_vertices;
    real                    cell_width_world_units;
    real                    cell_height_world_units;
    s_tag_reference         red_flag_shader; //shader, shader_environment, shader_model, shader_transparent_chicago, shader_transparent_chicago_extended, shader_transparent_generic, shader_transparent_glass, shader_transparent_meter, shader_transparent_plasma, shader_transparent_water
    s_tag_reference         physics; //pphy
    real                    wind_noise_world_units_per_second;
    PADDING(0x08);
    s_tag_reference         blue_flag_shader; //shader, shader_environment, shader_model, shader_transparent_chicago, shader_transparent_chicago_extended, shader_transparent_generic, shader_transparent_glass, shader_transparent_meter, shader_transparent_plasma, shader_transparent_water
    s_tag_block             attachment_points;
} s_flag_meta;
static_assert_check(sizeof(s_flag_meta) == 0x60, "Incorrect size of s_flag_meta");