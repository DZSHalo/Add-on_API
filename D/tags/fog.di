//APPROVED
import D.cseries.cseries;
import D.tags.tag_include;

import std.bitmanip;

struct s_fog_flags {
    mixin(bitfields!(
    bool, "is_water", 1,
    bool, "atmosphere_dominant", 1,
    bool, "fog_screen_only", 1,
    uint, "unused0", 5,
    uint, "unused1", 8,
    uint, "unused2", 8,
    uint, "unused3", 8));
};
static assert(s_fog_flags.sizeof == 0x04, "Incorrect size of s_fog_flags");

struct s_screen_layer_flags {
    mixin(bitfields!(
    bool, "no_environment_multipass", 1,
    bool, "no_model_multipass", 1,
    bool, "no_texture_based_falloff", 1,
    uint, "unused0", 5,
    uint, "unused1", 8));
} ;
static assert(s_screen_layer_flags.sizeof == 0x02, "Incorrect size of s_screen_layer_flags");

struct s_fog_meta {
    //flags
    s_fog_flags             flags;
    char[0x54] PADDING0;
    //density
    _real                    maximum_density;
    char[0x04] PADDING1;
    _real                    opaque_distance_world_units;
    char[0x04] PADDING2;
    _real                    opaque_depth_world_units;
    char[0x08] PADDING3;
    _real                    distance_to_water_plane_world_units;
    //color
    real_color              color;
    //screen layers;
    s_screen_layer_flags    screen_layer_flags;
    short                   layer_count;
    real_range              distance_gradient;
    real_range              density_gradient;
    _real                    start_distance_from_fog_plane;
    char[0x04] PADDING4;
    byte_color              screen_layers_color;
    _real                    rotation_multiplier;
    _real                    strafing_multiplier;
    _real                    zoom_multiplier;
    char[0x08] PADDING5;
    _real                    map_scale;
    s_tag_reference         map;                //bitm
    //screen layer animation
    _real                    animation_period;
    char[0x04] PADDING6;
    real_range              wind_velocity_world_units_per_second;
    real_range              wind_period_seconds;
    _real                    wind_acceration_weight;
    _real                    wind_perpendicular_weight;
    char[0x08] PADDING7;
    //sound
    s_tag_reference         background_sound;   //lsnd
    s_tag_reference         sound_environment;  //snde
    char[0x78] PADDING8;
};
static assert(s_fog_meta.sizeof == 0x18C, "Incorrect size of s_fog_meta");