//APPROVED
typedef struct {
    bool is_water : 1;
    bool atmosphere_dominant : 1;
    bool fog_screen_only : 1;
    bool unused0 : 5;
    bool unused1 : 8;
    bool unused2 : 8;
    bool unused3 : 8;
} s_fog_flags;
static_assert_check(sizeof(s_fog_flags) == 0x04, "Incorrect size of s_fog_flags");

typedef struct {
    bool no_environment_multipass : 1;
    bool no_model_multipass : 1;
    bool no_texture_based_falloff : 1;
    bool unused0 : 5;
    bool unused1 : 8;
} s_screen_layer_flags;
static_assert_check(sizeof(s_screen_layer_flags) == 0x02, "Incorrect size of s_screen_layer_flags");

typedef struct {
    //flags
    s_fog_flags             flags;
    PADDING(0x54);
    //density
    real                    maximum_density;
    PADDING(0x04);
    real                    opaque_distance_world_units;
    PADDING(0x04);
    real                    opaque_depth_world_units;
    PADDING(0x08);
    real                    distance_to_water_plane_world_units;
    //color
    real_color              color;
    //screen layers;
    s_screen_layer_flags    screen_layer_flags;
    short                   layer_count;
    real_range              distance_gradient;
    real_range              density_gradient;
    real                    start_distance_from_fog_plane;
    PADDING(0x04);
    byte_color              screen_layers_color;
    real                    rotation_multiplier;
    real                    strafing_multiplier;
    real                    zoom_multiplier;
    PADDING(0x08);
    real                    map_scale;
    s_tag_reference         map;                //bitm
    //screen layer animation
    real                    animation_period;
    PADDING(0x04);
    real_range              wind_velocity_world_units_per_second;
    real_range              wind_period_seconds;
    real                    wind_acceration_weight;
    real                    wind_perpendicular_weight;
    PADDING(0x08);
    //sound
    s_tag_reference         background_sound;   //lsnd
    s_tag_reference         sound_environment;  //snde
    PADDING(0x78);
} s_fog_meta;
static_assert_check(sizeof(s_fog_meta) == 0x18C, "Incorrect size of s_fog_meta");