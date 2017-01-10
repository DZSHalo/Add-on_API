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
#ifndef __cplusplus
typedef unsigned short e_decal_type;
static const e_decal_type DECAL_TYPE_SCRATCH = 0;
static const e_decal_type DECAL_TYPE_SPLATTER = 1;
static const e_decal_type DECAL_TYPE_BURN = 2;
static const e_decal_type DECAL_TYPE_PAINTED_SIGN = 3;

typedef unsigned short e_decal_layer;
static const e_decal_layer DECAL_LAYER_PRIMARY = 0;
static const e_decal_layer DECAL_LAYER_SECONDARY = 1;
static const e_decal_layer DECAL_LAYER_LIGHT = 2;
static const e_decal_layer DECAL_LAYER_ALPHA_TESTED = 3;
static const e_decal_layer DECAL_LAYER_WATER = 4;
#endif

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
