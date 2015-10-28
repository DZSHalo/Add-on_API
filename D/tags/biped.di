//APPROVED EXCEPT FOR e_unknown_enum
import Add_on_API.D.cseries.cseries;
import Add_on_API.D.tags.tag_include;

import std.bitmanip;

struct s_biped_flags {
    mixin(bitfields!(
    bool, "does_not_cast_shadow", 1,
    bool, "transparent_self_occlusion", 1,
    bool, "brighter_than_it_should_be", 1,
    bool, "not_a_pathfinder_obstacle", 1,
    uint, "unused0", 4,
    uint, "unused1", 8,
    uint, "unused2", 8,
    uint, "unused3", 8));
};
static assert(s_biped_flags.sizeof == 0x04, "Incorrect size of s_biped_flags");

enum e_export_in : ushort {
    EXPORT_IN_NONE=0,
    EXPORT_IN_BODY_VITALITY,
    EXPORT_IN_SHIELD_VITALITY,
    EXPORT_IN_RECENT_BODY_DAMAGE,
    EXPORT_IN_RECENT_SHIELD_DAMAGE,
    EXPORT_IN_RANDOM_CONSTANT,
    EXPORT_IN_UMBRELLA_SHIELD_VITALITY,
    EXPORT_IN_SHIELD_STUN,
    EXPORT_IN_RECENT_UMBRELLA_SHIELD_VITALITY,
    EXPORT_IN_UMBRELLA_SHIELD_STUN,
    EXPORT_IN_REGION_00_DAMAGE,
    EXPORT_IN_REGION_01_DAMAGE,
    EXPORT_IN_REGION_02_DAMAGE,
    EXPORT_IN_REGION_03_DAMAGE,
    EXPORT_IN_REGION_04_DAMAGE,
    EXPORT_IN_REGION_05_DAMAGE,
    EXPORT_IN_REGION_06_DAMAGE,
    EXPORT_IN_REGION_07_DAMAGE,
    EXPORT_IN_ALIVE,
    EXPORT_IN_COMPASS
};

struct s_attachments_block {
    s_tag_reference     type;   //contrail, effect, light, light_volume, particle_system, sound_looping
    char[0x20]          marker;
    e_attachment_out    primary_scale;
    e_attachment_out    secondary_scale;
    e_attachment_out    change_color;
    char[0x02] PADDING0;
    char[0x10] PADDING1;
};
static assert(s_attachments_block.sizeof == 0x48, "Incorrect size of s_attachments_block");

struct s_widgets_block {
    s_tag_reference reference;
    char[0x10] PADDING;
};
static assert(s_widgets_block.sizeof == 0x20, "Incorrect size of s_widgets_block");

struct s_function_flags {
    mixin(bitfields!(
    bool, "invert", 1,
    bool, "additive", 1,
    bool, "always_active", 1,
    uint, "unused", 5,
    uint, "unused0", 8,
    uint, "unused1", 8,
    uint, "unused2", 8));
};
static assert(s_function_flags.sizeof == 0x04, "Incorrect size of s_function_flags");

enum e_map_to :ushort {
    MAP_TO_LINEAR=0,
    MAP_TO_EARLY,
    MAP_TO_VERY_EARLY,
    MAP_TO_LATE,
    MAP_TO_VERY_LATE,
    MAP_TO_CONSINE
};

enum e_bound_mode : ushort {
    BOUND_MODE_CLIP=0,
    BOUND_MODE_CLIP_AND_NORMALIZE,
    BOUND_MODE_SCALE_TO_FIT,
};

struct s_function_block {
    s_function_flags    flags;
    _real                period_seconds;
    e_attachment_in_out scale_period_by;
    e_function          function_;
    e_attachment_in_out scale_function_by;
    e_function          wobble_function;
    _real                wobble_period;
    _real                wobble_magnitude;
    _real                square_wave_threshold;
    short               step_count;
    e_map_to            map_to;
    short               sawtooth_count;
    e_attachment_in_out add;
    e_attachment_in_out scale_result_by;
    e_bound_mode        bounds_mode;
    real_range          bounds;
    char[0x04] PADDING0;
    char[0x02] PADDING1;
    short               turn_off_with;  //-1 = NONE, function index
    _real                scale_by;
    char[0x10C] PADDING2;
    char[0x20]          usage;    //Title of the function

};
static assert(s_function_block.sizeof == 0x168, "Incorrect size of s_function_block");

struct s_scale_flags {
    mixin(bitfields!(
    bool, "blend_in_hsv", 1,
    bool, "more_colors", 1,
    uint, "unused0", 6,
    uint, "unused1", 8,
    uint, "unused2", 8,
    uint, "unused3", 8));
};
static assert(s_scale_flags.sizeof == 0x04, "Incorrect size of s_scale_flags");

struct s_biped_change_color_block {
    e_attachment_in_out darken_by;
    e_attachment_in_out scale_by;
    s_scale_flags       scale_flags;
    real_color          color_lower_bound;
    real_color          color_upper_bound;
    s_tag_block         permutations;
};
static assert(s_biped_change_color_block.sizeof == 0x2C, "Incorrect size of s_biped_change_color_block");

struct s_color_permutation_block {
    _real        weight;
    real_color  color_lower_bound;
    real_color  color_upper_bound;
};
static assert(s_color_permutation_block.sizeof == 0x1C, "Incorrect size of s_color_permutation_block");

enum e_predicted_resource_type :ushort {
    PREDICTED_RESOURCE_TYPE_BITMAP=0,
    PREDICTED_RESOURCE_TYPE_SOUND
};

struct s_predicted_resource_block {
    e_predicted_resource_type   type;
    short                       resource_index;
    int                         tag_index;
};
static assert(s_predicted_resource_block.sizeof == 0x08, "Incorrect size of s_predicted_resource_block");

struct s_unit_flags {
    mixin(bitfields!(
    bool, "circular_aiming", 1,
    bool, "destroyed_after_dying", 1,
    bool, "half_speed_interpolation", 1,
    bool, "fires_from_camera", 1,
    bool, "entrance_inside_bounding_sphere", 1,
    bool, "unused0", 1,
    bool, "causes_passenger_dialogue", 1,
    bool, "resists_pings", 1,
    bool, "melee_attack_is_fatal", 1,
    bool, "dont_reface_during_pings", 1,
    bool, "has_no_aiming", 1,
    bool, "simple_creature", 1,
    bool, "impact_melee_attaches_to_unit", 1,
    bool, "impact_melee_dies_on_shields", 1,
    bool, "cannot_open_doors_automatically", 1,
    bool, "melee_attackers_cannot_attach", 1,
    bool, "not_instantly_killed_by_melee", 1,
    bool, "shield_sapping", 1,
    bool, "runs_around_flaming", 1,
    bool, "inconsequential", 1,
    bool, "special_cinematic_unit", 1,
    bool, "ignored_by_autoaiming", 1,
    bool, "shields_fry_infection_form", 1,
    bool, "integrated_light_controls_weapon", 1,
    bool, "intergrated_lights_last_forever", 1,
    uint, "unused1", 7));
};
static assert(s_unit_flags.sizeof == 0x4, "Incorrect size of s_unit_flags");

struct s_camera_track_block {
    s_tag_reference track;
    char[0x0C] PADDING;
};
static assert(s_camera_track_block.sizeof == 0x1C, "Incorrect size of s_camera_track_block");

struct s_hud_interface_block {
    s_tag_reference unit_hud_interface;
    char[0x20] PADDING;
};
static assert(s_hud_interface_block.sizeof == 0x30, "Incorrect size of s_hud_interface_block");

struct s_dialog_variant_block {
    ushort variant_block;
    char[0x02] PADDING0;
    char[0x04] PADDING1;
    s_tag_reference dialogue;
};
static assert(s_dialog_variant_block.sizeof == 0x18, "Incorrect size of s_dialog_variant_block");

struct s_powered_seat_block {
    char[0x04] PADDING0;
    _real    driver_powerup_time_seconds;
    _real    driver_powerdown_time_seconds;
    char[0x38] PADDING1;
};
static assert(s_powered_seat_block.sizeof == 0x44, "Incorrect size of s_powered_seat_block");

struct s_seat_flags {
    mixin(bitfields!(
    bool, "invisible", 1,
    bool, "locked", 1,
    bool, "driver", 1,
    bool, "gunner", 1,
    bool, "third_person_camera", 1,
    bool, "allows_weapons", 1,
    bool, "third_person_on_enter", 1,
    bool, "first_person_camera_slaved_to_gun", 1,
    bool, "allow_vehicle_communication_animation", 1,
    bool, "not_valid_without_driver", 1,
    bool, "allow_AI_noncombatants", 1,
    uint, "unused0", 5,
    uint, "unused1", 8,
    uint, "unused2", 8));
};
static assert(s_seat_flags.sizeof == 0x04, "Incorrect size of s_seat_flags");

struct s_weapon_block {
    s_tag_reference weapon;
    char[0x14] PADDING;
};
static assert(s_weapon_block.sizeof == 0x24, "Incorrect size of s_weapon_block");

struct s_seat_block {
    s_seat_flags    flags;
    char[0x20]      label;
    char[0x20]      marker_name;
    char[0x20] PADDING0;
    real_offset3d   acceration_scale;
    char[0x0C] PADDING1;
    _real            yaw_rate_degrees_per_second;
    _real            pitch_rate_degrees_per_second;
    char[0x20]      camera_marker_name;
    char[0x20]      camera_submerged_marker_name;
    _real            pitch_auto_level;       //Unknown conversion;
    real_range      pitch_range;            //Unknown conversion;
    s_tag_block     camera_tracks;          //Max is 2; loose / tight
    s_tag_block     unit_hud_interface;     //Max is 2; default/solo_player_hud / multiplayer_hud
    char[0x04] PADDING2;
    short           hud_text_message_index;
    char[0x02] PADDING3;
    _real            yaw_minimum_radian;
    _real            yaw_maximum_radian;
    s_tag_reference built_in_gunner;
    char[0x14] PADDING4;

};
static assert(s_seat_block.sizeof == 0x11C, "Incorrect size of s_seat_block");

struct s_contact_point_block {
    char[0x20] PADDING;
    char[0x20]  marker_name;
};
static assert(s_contact_point_block.sizeof == 0x40, "Incorrect size of s_contact_point_block");

struct s_biped__flags {
    mixin(bitfields!(
    bool, "turns_without_animating", 1,
    bool, "uses_player_physics", 1,
    bool, "flying", 1,
    bool, "phsyics_pill_centered_at_origin", 1,
    bool, "spherical", 1,
    bool, "passes_through_other_bipeds", 1,
    bool, "can_climb_any_surface", 1,
    bool, "immune_to_falling_damage", 1,
    bool, "rotate_while_airborne", 1,
    bool, "uses_limp_body_physics", 1,
    bool, "has_no_dying_airbone", 1,
    bool, "random_speed_increase", 1,
    bool, "unit_uses_old_NTSC_player_physics", 1,
    uint, "unused0", 3,
    uint, "unused1", 8,
    uint, "unused2", 8));
};
static assert(s_biped__flags.sizeof == 0x04, "Incorrect size of s_biped__flags");

enum e_default_team : ushort {
    DEFAULT_TEAM_NONE=0,
    DEFAULT_TEAM_PLAYER,
    DEFAULT_TEAM_HUMAN,
    DEFAULT_TEAM_COVENANT,
    DEFAULT_TEAM_FLOOD,
    DEFAULT_TEAM_SENTINEL,
    DEFAULT_TEAM_UNUSED6,
    DEFAULT_TEAM_UNUSED7,
    DEFAULT_TEAM_UNUSED8,
    DEFAULT_TEAM_UNUSED9
};

enum e_sound_volume : ushort {
    SOUND_VOLUME_SILENT=0,
    SOUND_VOLUME_MEDIUM,
    SOUND_VOLUME_LOUD,
    SOUND_VOLUME_SHOUT,
    SOUND_VOLUME_QUIET
};

enum e_unit_action : ushort {
    UNIT_ACTION_NONE=0,
    UNIT_ACTION_DRIVER_SEAT_POWER,
    UNIT_ACTION_GUNNER_SEAT_POWER,
    UNIT_ACTION_AIMING_CHANGE,
    UNIT_ACTION_MOUTH_APERTURE,
    UNIT_ACTION_INTEGRATED_LIGHT_POWER,
    UNIT_ACTION_CAN_BLINK,
    UNIT_ACTION_SHIELD_SAPPING
};

enum e_motion_sensor_blip_size : ushort {
    BLIP_SIZE_MEDIUM=0,
    BLIP_SIZE_SMALL,
    BLIP_SIZE_LARGE,
};

enum e_unknown_enum : ushort {
    UNKNOWN_ENUM_NONE=0,
    UNKNOWN_ENUM_FLYING_VELOCITY
};

struct s_biped_meta {
    s_biped_flags   flags;
    _real            bounding_radius_world_units;
    real_offset3d   bounding_offset;
    real_offset3d   origin_offset;
    _real            acceleration_scale;
    char[0x04] PADDING0;
    s_tag_reference model;              //mod2
    s_tag_reference animation_graph;    //antr
    char[0x28] PADDING1;
    s_tag_reference collision_model;    //coll
    s_tag_reference physics;            //phys
    s_tag_reference modifier_shader;    /* shader, shader_enviornment, shader_model, shader_transparent_chicago,
                                            shader_transparent_extended, shader_transparent_generic, shader_transparent_glass, 
                                            shader_transparent_meter, shader_transparent_plasma, or shader_transparent_water */
    s_tag_reference creation_effect;    //effe
    char[0x54] PADDING2;
    _real            render_bounding_radius_world_units;
    //export to functions
    e_export_in     export_A_in;
    e_export_in     export_B_in;
    e_export_in     export_C_in;
    e_export_in     export_D_in;
    char[0x2C] PADDING3;
    short           hud_text_message_index;
    short           forced_shader_permuation_index;
    s_tag_block     attachments;        //Max is 8
    s_tag_block     widgets;            //Max is 4
    s_tag_block     functions;
    s_tag_block     change_colors;      //Max is 4 (A, B, C, D)
    s_tag_block     predicted_resource; //disabled by HEK, OS bypass Max is 500+ (unknown max limitation)
    //unit
    s_unit_flags    unit_flags;
    e_default_team  default_team;
    e_sound_volume  constant_sound_volume;
    _real            rider_damage_fraction;
    s_tag_reference integrated_light_toggle;    //effe
    e_unit_action   unit_A_in;
    e_unit_action   unit_B_in;
    e_unit_action   unit_C_in;
    e_unit_action   unit_D_in;
    _real            camera_field_of_view_radian;
    _real            camera_stiffness;
    char[0x20]      camera_marker_name;
    char[0x20]      camera_submerged_marker_name;
    _real            pitch_auto_level;   //Unknown conversion
    real_range      pitch_range;        //Unknown conversion
    s_tag_block     camera_tracks;      //Max is 2; loose & tight
    real_offset3d   seat_acceleration_scale;
    char[0x0C] PADDING4;
    _real            soft_ping_threshold;
    _real            soft_ping_interruption_time_seconds;
    _real            hard_ping_threshold;
    _real            hard_ping_interruption_time_seconds;
    _real            hard_death_threshold;
    _real            feign_death_threshold;
    _real            feign_death_time_seconds;
    _real            distance_of_evade_anim_world_units;
    _real            distance_of_dive_anim_world_units;
    char[0x04] PADDING5;
    _real            stunned_movement_threshold;
    _real            feign_death_chance;
    _real            feign_repeat_chance;
    s_tag_reference spawned_actor;  //actv
    short_range     spawned_actor_count;
    _real            spawned_velocity;
    _real            aiming_velocity_maximum_radians_per_second;
    _real            aiming_acceleration_maximum_radians_per_second;
    _real            casual_aiming_modifier;
    _real            looking_velocity_maximum_radians_per_second;
    _real            looking_acceleration_maximum_radians_per_second;
    char[0x08] PADDING6;
    _real            AI_vehicle_radius;
    _real            AI_danger_radius;
    s_tag_reference melee_damage;   //jpt!
    e_motion_sensor_blip_size    motion_sensor_blip_size;
    char[0x02] PADDING7;
    char[0x0C] PADDING8;
    s_tag_block     new_hud_interfaces; //Max is 2; default/solo_player_hud / multiplayer_hud
    s_tag_block     dialogue_variants;  //Max is 16
    _real            grenade_velocity_world_units_per_second;
    e_grenade_type  grenade_type;
    ushort  grenade_count;
    char[0x04] PADDING;
    s_tag_block     powered_seats;      //Max is 2; driver / gunner
    s_tag_block     weapons;            //Max is 4
    s_tag_block     seats;              //Max is 16
    //biped
    _real            moving_turning_speed_radians_per_second;
    s_biped__flags  biped_flags;
    _real            stationary_turning_threshold;   //Unknown conversion
    char[0x10] PADDING9;
    e_unknown_enum  biped_A_in;
    e_unknown_enum  biped_B_in;
    e_unknown_enum  biped_C_in;
    e_unknown_enum  biped_D_in;
    s_tag_reference DONT_USE;       //jpt!
    //flying
    _real            bank_angle_radian;
    _real            bank_apply_time_seconds;
    _real            bank_decay_time_seconds;
    _real            pitch_ratio;
    _real            max_velocity_world_units_per_second;
    _real            max_sidestep_velocity_world_units_per_second;
    _real            max_acceleration_world_units_per_second_squared;
    _real            max_deceleration_world_units_per_second_squared;
    _real            anglular_velocity_maximum_radians_per_second;
    _real            anglular_acceleration_maximum_radians_per_second;
    _real            crouch_velocity_modifier;
    char[0x08] PADDING10;
    //movement
    _real            maximum_slop_angle_radian;
    _real            downhill_falloff_angle_radian;
    _real            downhill_cutoff_angle_radian;
    _real            downhill_velocity_scale;
    _real            uphill_falloff_angle_radian;
    _real            uphill_cutoff_angle_radian;
    _real            uphill_velocity_scale;
    char[0x18] PADDING11;
    s_tag_reference footsteps;      //foot
    char[0x18] PADDING12;
    //jumping and landing
    _real            jump_velocity_world_units_per_second;
    char[0x1C] PADDING13;
    _real            maximum_soft_land_time_seconds;
    _real            maximum_hard_land_time_seconds;
    _real            minimum_soft_landing_velocity_world_units_per_second;
    _real            minimum_hard_landing_velocity_world_units_per_second;
    _real            maximum_hard_landing_velocity_world_units_per_second;
    _real            death_hard_landing_velocity_world_units_per_second;
    char[0x14] PADDING14;
    //camera, collision, and autoaim
    _real            standing_camera_height_world_units;
    _real            crouching_camera_height_world_units;
    _real            crouch_transition_time_seconds;
    char[0x18] PADDING15;
    _real            standing_collision_height_world_units;
    _real            crouching_collision_height_world_units;
    _real            collision_radius_world_units;
    char[0x28] PADDING16;
    _real            autoaim_width_world_units;
    char[0x6C] PADDING17;
    char[0x20] UNKNOWN;
    s_tag_block     contact_points; //Max is 2
};
static assert(s_biped_meta.sizeof == 0x4F4, "Incorrect size of s_biped_meta");