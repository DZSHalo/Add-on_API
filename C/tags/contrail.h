//APPROVED
typedef struct s_contrail_flags {
    bool    first_point_unfaded : 1;
    bool    last_point_unfaded : 1;
    bool    points_start_pinned_to_media : 1;
    bool    points_start_pinned_to_ground : 1;
    bool    points_always_pinned_to_media : 1;
    bool    points_always_pinned_to_ground : 1;
    bool    edge_effect_fades_slowly : 1;
    bool    unused : 1;
    PADDING(0x01);
} s_contrail_flags;

typedef struct s_contrail_scale_flags {
    bool    point_generation_rate : 1;
    bool    point_velocity : 1;
    bool    point_velocity_delta : 1;
    bool    point_velocity_cone_angle : 1;
    bool    inherited_velocity_fraction : 1;
    bool    sequence_animation_rate : 1;
    bool    texture_scale_u : 1;
    bool    texture_scale_v : 1;
    bool    texture_animation_u : 1;
    bool    texture_animation_v : 1;
    bool    unused : 2;
} s_contrail_scale_flags;

typedef enum e_render_type : unsigned short {
    RENDER_TYPE_VERTICAL_ORIENTATION = 0,
    RENDER_TYPE_HORIZONTAL_ORIENTATION,
    RENDER_TYPE_MEDIA_MAPPED,
    RENDER_TYPE_GROUND_MAPPED,
    RENDER_TYPE_VIEWER_FACING,
    RENDER_TYPE_DOUBLE_MARKER_LINKED
} e_render_type;

typedef struct s_shader_flags {
    bool    sort_bias : 1;
    bool    nonlinear_tint : 1;
    bool    dont_overdraw_fp_weapon : 1;
    bool    unused0 : 1;
    bool    unused1 : 4;
    bool    unused2 : 8;
} s_shader_flags;

typedef enum e_blend_function : unsigned short {
    BLEND_FUNC_ALPHA_BLEND=0,
    BLEND_FUNC_MULTIPLY,
    BLEND_FUNC_DOUBLE_MULTIPLY,
    BLEND_FUNC_ADD,
    BLEND_FUNC_SUBTRACT,
    BLEND_FUNC_COMPONENT_MIN,
    BLEND_FUNC_COMPONENT_MAX,
    BLEND_FUNC_ALPHA_MULTIPLY_ADD
} e_blend_function;

typedef enum e_fade_mode : unsigned short {
    FADE_NONE=0,
    FADE_WHEN_PERPENDICULAR,
    FADE_WHEN_PARALLEL
} e_fade_mode;

typedef struct s_map_flags {
    bool    unfiltered : 1;
    bool    unused0 : 3;
    bool    unused1 : 4;
    bool    unused2 : 8;
} s_map_flags;

typedef enum e_secondary_map_anchor : unsigned short {
    ANCHOR_WITH_PRIMARY=0,
    ANCHOR_WITH_SCREEN_SPACE,
    ANCHOR_ZSPRITE
} e_secondary_map_anchor;

typedef struct s_point_scale_flags {
    bool    duration : 1;
    bool    duration_delta : 1;
    bool    transition_duration : 1;
    bool    transition_duration_delta : 1;
    bool    width : 1;
    bool    color : 1;
    bool    unused : 2;
    PADDING(0x01);
} s_point_scale_flags;

typedef struct s_point_states_block {
    //state timing
    real_range          duration_seconds;
    real_range          transition_duration_seconds;
    //point variables
    s_tag_reference     physics; //pphy
    PADDING(0x20);
    real                width_world_units;
    real_color_alpha    color_lower_bound;
    real_color_alpha    color_upper_bound;
    s_point_scale_flags scale_flags;

} s_point_states_block;
static_assert_check(sizeof(s_point_states_block) == 0x68, "Incorrect size of s_point_states_block");

typedef struct s_contrail_meta {
    s_contrail_flags        flags;
    s_contrail_scale_flags  scale_flags;
    //point creation
    real                    point_generation_rate_points_per_second;
    real_range              point_velocity_world_units_per_second;
    real                    point_velocity_cone_angle_radians;
    real                    inherited_velocity_fraction;
    //rendering
    e_render_type           render_type;
    PADDING(0x02);
    real                    texture_repeats_u;
    real                    texture_repeats_v;
    real                    texture_animation_u_repeats_per_second;
    real                    texture_animation_v_repeats_per_second;
    real                    animation_rate_frames_per_second;
    s_tag_reference         bitmap_rendering; //bitm
    short                   first_sequence_index;
    short                   sequence_count;
    PADDING(0x68);
    s_shader_flags          shader_flags;
    e_blend_function        framebuffer_blend_function;
    e_fade_mode             framebuffer_fade_mode;
    s_map_flags             map_flags;
    PADDING(0x1C);
    //Secondary Map
    s_tag_reference         bitmap_secondary_map; //bitm
    e_secondary_map_anchor  anchor;
    s_map_flags             flags_secondary_map;
    e_source_out            u_animation_source;
    e_function              u_animation_function;
    real                    u_animation_period_seconds;
    real                    u_animation_phase;
    real                    u_animation_scale_repeats;
    e_source_out            v_animation_source;
    e_function              v_animation_function;
    real                    v_animation_period_seconds;
    real                    v_animation_phase;
    real                    v_animation_scale_repeats;
    e_source_out            rotation_animation_source;
    e_function              rotation_animation_function;
    real                    rotation_animation_period_seconds;
    real                    rotation_animation_phase;
    real                    rotation_animation_scale_degrees;
    real_vector2d           rotation_animation_center;
    PADDING(0x04);
    real                    zsprite_radius_scale;
    PADDING(0x14);
    s_tag_block             point_states;
} s_contrail_meta;
static_assert_check(sizeof(s_contrail_meta) == 0x144, "Incorrect size of s_contrail_meta");