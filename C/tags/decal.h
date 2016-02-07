//APPROVED
typedef struct s_decal_flags {
    bool    geometry_inherited_by_next_decal_in_chain : 1;
    bool    interpolate_color_in_hsv : 1;
    bool    more_colors : 1;
    bool    no_random_rotation : 1;
    bool    water_effect : 1;
    bool    SAPIEN_snap_to_axis : 1;
    bool    SAPIEN_incremental_counter : 1;
    bool    animation_loop : 1;
    bool    preserve_aspect : 1;
    bool    unused0 : 3;
    bool    unused1 : 4;
} s_decal_flags;
static_assert_check(sizeof(s_decal_flags) == 0x02, "Incorrect size of s_decal_flags");

enum e_decal_type :unsigned short {
    DECAL_TYPE_SCRATCH=0,
    DECAL_TYPE_SPLATTER,
    DECAL_TYPE_BURN,
    DECAL_TYPE_PAINTED_SIGN
};

enum e_decal_layer :unsigned short {
    DECAL_LAYER_PRIMARY=0,
    DECAL_LAYER_SECONDARY,
    DECAL_LAYER_LIGHT,
    DECAL_LAYER_ALPHA_TESTED,
    DECAL_LAYER_WATER
};

typedef struct s_decal_meta {
    //decal
    //Contains description
    s_decal_flags   flags;
    e_decal_type    type;
    e_decal_layer   layer;
    PADDING(0x02);
    s_tag_reference next_decal_in_chain; //deca
    //radius and color
    real_range      radius_world_units;
    PADDING(0x0C);
    real_range      intensity;
    real_color      color_lower_bounds;
    real_color      color_upper_bounds;
    PADDING(0x0C);
    //animation
    short           animation_loop_frame;
    unsigned short  animation_speed;
    PADDING(0x1C);
    real_range      lifetime_seconds;
    real_range      decay_time_seconds;
    PADDING(0x38);
    //shader
    e_blend_function    framebuffer_blend_function;
    PADDING(0x02);
    PADDING(0x14);
    s_tag_reference     map;
    PADDING(0x14);
    //sprite info
    real                maximum_sprite_extent;
    PADDING(0x0C);
} s_decal_meta;
static_assert_check(sizeof(s_decal_meta) == 0x10C, "Incorrect size of s_decal_meta");