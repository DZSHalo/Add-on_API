//APPROVED except for s_damage_effect_flags
//TODO: NEED VERIFY IF THIS FLAG IS 4 BYTES LONG.
typedef struct s_damage_effect_flags {
    bool    dont_scale_damage_by_distance : 1;
    bool    unused0 : 3;
    bool    unused1 : 4;
    PADDING(0x3);
} s_damage_effect_flags;
static_assert_check(sizeof(s_damage_effect_flags) == 0x4, "Incorrect size of s_damage_effect_flags");
#ifndef __cplusplus
typedef unsigned short e_screen_flash_type;
static const e_screen_flash_type SCREEN_FLASH_NONE = 0;
static const e_screen_flash_type SCREEN_FLASH_LIGHTEN = 1;
static const e_screen_flash_type SCREEN_FLASH_DARKEN = 2;
static const e_screen_flash_type SCREEN_FLASH_MAX = 3;
static const e_screen_flash_type SCREEN_FLASH_MIN = 4;
static const e_screen_flash_type SCREEN_FLASH_INVERT = 5;
static const e_screen_flash_type SCREEN_FLASH_TINT = 6;

typedef unsigned short e_priority;
static const e_priority PRIORITY_LOW = 0;
static const e_priority PRIORITY_MEDIUM = 1;
static const e_priority PRIORITY_HIGH = 2;

typedef unsigned short e_fade_function;
static const e_fade_function FADE_FUNCTION_LINEAR = 0;
static const e_fade_function FADE_FUNCTION_EARLY = 1;
static const e_fade_function FADE_FUNCTION_VERY_EARLY = 2;
static const e_fade_function FADE_FUNCTION_LATE = 3;
static const e_fade_function FADE_FUNCTION_VERY_LATE = 4;
static const e_fade_function FADE_FUNCTION_COSINE = 5;
#endif

typedef struct s_vibrate_duration_fade {
    real            duration_seconds;
    e_fade_function fade_function;
    PADDING(0x02);
} s_vibrate_duration_fade;

typedef struct s_vibrate_section {
    real                    frequency;
    s_vibrate_duration_fade duration_fade;
    PADDING(0x08);
} s_vibrate_section;
static_assert_check(sizeof(s_vibrate_section) == 0x14, "Incorrect size of s_vibrate_section");

typedef struct s_damage_effect_meta {
    real_range          radius_world_units;
    real                cutoff_scale;
    PADDING(0x14);
    //screen flash
    e_screen_flash_type type;
    e_priority          priority;
    PADDING(0x0C);
    real                duration_seconds;
    e_fade_function     fade_function;
    PADDING(0x02);
    PADDING(0x08);
    real                maximum_intensity;
    PADDING(0x04);
    real_color_alpha    color;
    //low frequency vibrate
    s_vibrate_section   low_frequency_vibrate;
    //high frequency vibrate
    s_vibrate_section   high_frequency_vibrate;
    PADDING(0x14);      //might be unused s_vibrate_section here
    //temporary camera impulse
    s_vibrate_duration_fade duration_fade;
    real                    rotation_radians;
    real                    pushback_world_units;
    real_range              jitter_world_units;
    PADDING(0x08);
    //permanent camera impulse
    real                    angle_radians;
    PADDING(0x10);
    //camera_shaking
    s_vibrate_duration_fade duration_falloff;
    real                    random_translation_world_units;
    real                    random_rotation_radians;
    PADDING(0x0C);
    e_function              wobble_function;
    PADDING(0x02);
    real                    wobble_function_period_seconds;
    real                    wobble_weight;
    PADDING(0x20);
    //sound
    s_tag_reference         sound; //snd!
    PADDING(0x70);
    //breaking effect
    real                    forward_velocity_world_units_per_second;
    real                    forward_radius_world_units;
    real                    forward_exponent;
    PADDING(0x0C);
    real                    outward_velocity_world_units_per_second;
    real                    outward_radius_world_units;
    real                    outward_exponent;
    PADDING(0x0C);
    //damage
    e_side_effect           side_effect;
    PADDING(0x02);
    e_category              category;
    PADDING(0x02);
    s_damage_flags          flags;
    real                    AOE_core_radius_world_units;
    real                    damage_lower_bound;
    real_range              damage_upper_bound;
    real                    vehicle_passthrough_penalty;
    real                    active_camouflage_damage;
    real                    stun;
    real                    maximum_stun;
    real                    stun_time_seconds;
    PADDING(0x04);
    real                    instantaneous_acceleration;
    PADDING(0x08);
    //damage modifiers
    s_damage_modifiers_section  damage_modifiers;
    PADDING(0x01C);
} s_damage_effect_meta;
static_assert_check(sizeof(s_damage_effect_meta) == 0x2A0, "Incorrect size of s_damage_effect_meta");
