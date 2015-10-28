//APPROVED EXCEPT FOR e_unknown_enum
typedef struct {
    bool    does_not_cast_shadow : 1;
    bool    transparent_self_occlusion : 1;
    bool    brighter_than_it_should_be : 1;
    bool    not_a_pathfinder_obstacle : 1;
    bool    unused0 : 4;
    bool    unused1 : 8;
    bool    unused2 : 8;
    bool    unused3 : 8;
} s_biped_flags;
static_assert_check(sizeof(s_biped_flags) == 0x04, "Incorrect size of s_biped_flags");

enum e_export_in : unsigned short {
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

typedef struct {
    s_tag_reference     type;   //contrail, effect, light, light_volume, particle_system, sound_looping
    char                marker[0x20];
    e_attachment_out    primary_scale;
    e_attachment_out    secondary_scale;
    e_attachment_out    change_color;
    PADDING(0x02);
    PADDING(0x10);
} s_attachments_block;
static_assert_check(sizeof(s_attachments_block) == 0x48, "Incorrect size of s_attachments_block");

typedef struct {
    s_tag_reference reference;
    PADDING(0x10);
} s_widgets_block;
static_assert_check(sizeof(s_widgets_block) == 0x20, "Incorrect size of s_widgets_block");

typedef struct {
    bool    invert : 1;
    bool    additive : 1;
    bool    always_active : 1;
    bool    unused : 5;
    bool    unused0 : 8;
    bool    unused1 : 8;
    bool    unused2 : 8;
} s_function_flags;
static_assert_check(sizeof(s_function_flags) == 0x04, "Incorrect size of s_function_flags");

enum e_map_to :unsigned short {
    MAP_TO_LINEAR=0,
    MAP_TO_EARLY,
    MAP_TO_VERY_EARLY,
    MAP_TO_LATE,
    MAP_TO_VERY_LATE,
    MAP_TO_CONSINE
};

enum e_bound_mode : unsigned short {
    BOUND_MODE_CLIP=0,
    BOUND_MODE_CLIP_AND_NORMALIZE,
    BOUND_MODE_SCALE_TO_FIT,
};

typedef struct {
    s_function_flags    flags;
    real                period_seconds;
    e_attachment_in_out scale_period_by;
    e_function          function;
    e_attachment_in_out scale_function_by;
    e_function          wobble_function;
    real                wobble_period;
    real                wobble_magnitude;
    real                square_wave_threshold;
    short               step_count;
    e_map_to            map_to;
    short               sawtooth_count;
    e_attachment_in_out add;
    e_attachment_in_out scale_result_by;
    e_bound_mode        bounds_mode;
    real_range          bounds;
    PADDING(0x04);
    PADDING(0x02);
    short               turn_off_with;  //-1 = NONE, function index
    real                scale_by;
    PADDING(0x10C);
    char                usage[0x20];    //Title of the function

} s_function_block;
static_assert_check(sizeof(s_function_block) == 0x168, "Incorrect size of s_function_block");

typedef struct {
    bool blend_in_hsv : 1;
    bool more_colors : 1;
    bool unused0 : 6;
    bool unused1 : 8;
    bool unused2 : 8;
    bool unused3 : 8;
} s_scale_flags;
static_assert_check(sizeof(s_scale_flags) == 0x04, "Incorrect size of s_scale_flags");

typedef struct {
    e_attachment_in_out darken_by;
    e_attachment_in_out scale_by;
    s_scale_flags       scale_flags;
    real_color          color_lower_bound;
    real_color          color_upper_bound;
    s_tag_block         permutations;
} s_biped_change_color_block;
static_assert_check(sizeof(s_biped_change_color_block) == 0x2C, "Incorrect size of s_biped_change_color_block");

typedef struct {
    real        weight;
    real_color  color_lower_bound;
    real_color  color_upper_bound;
} s_color_permutation_block;
static_assert_check(sizeof(s_color_permutation_block) == 0x1C, "Incorrect size of s_color_permutation_block");

enum e_predicted_resource_type :unsigned short {
    PREDICTED_RESOURCE_TYPE_BITMAP=0,
    PREDICTED_RESOURCE_TYPE_SOUND
};

typedef struct {
    e_predicted_resource_type   type;
    short                       resource_index;
    int                         tag_index;
} s_predicted_resource_block;
static_assert_check(sizeof(s_predicted_resource_block) == 0x08, "Incorrect size of s_predicted_resource_block");

typedef struct {
    bool circular_aiming : 1;
    bool destroyed_after_dying : 1;
    bool half_speed_interpolation : 1;
    bool fires_from_camera : 1;
    bool entrance_inside_bounding_sphere : 1;
    bool unused0 : 1;
    bool causes_passenger_dialogue : 1;
    bool resists_pings : 1;
    bool melee_attack_is_fatal : 1;
    bool dont_reface_during_pings : 1;
    bool has_no_aiming : 1;
    bool simple_creature : 1;
    bool impact_melee_attaches_to_unit : 1;
    bool impact_melee_dies_on_shields : 1;
    bool cannot_open_doors_automatically : 1;
    bool melee_attackers_cannot_attach : 1;
    bool not_instantly_killed_by_melee : 1;
    bool shield_sapping : 1;
    bool runs_around_flaming : 1;
    bool inconsequential : 1;
    bool special_cinematic_unit : 1;
    bool ignored_by_autoaiming : 1;
    bool shields_fry_infection_form : 1;
    bool integrated_light_controls_weapon : 1;
    bool intergrated_lights_last_forever : 1;
    bool unused1 : 7;
} s_unit_flags;
static_assert_check(sizeof(s_unit_flags) == 0x4, "Incorrect size of s_unit_flags");

typedef struct {
    s_tag_reference track;
    PADDING(0x0C);
} s_camera_track_block;
static_assert_check(sizeof(s_camera_track_block) == 0x1C, "Incorrect size of s_camera_track_block");

typedef struct {
    s_tag_reference unit_hud_interface;
    PADDING(0x20);
} s_hud_interface_block;
static_assert_check(sizeof(s_hud_interface_block) == 0x30, "Incorrect size of s_hud_interface_block");

typedef struct {
    unsigned short variant_block;
    PADDING(0x02);
    PADDING(0x04);
    s_tag_reference dialogue;
} s_dialog_variant_block;
static_assert_check(sizeof(s_dialog_variant_block) == 0x18, "Incorrect size of s_dialog_variant_block");

typedef struct {
    PADDING(0x04);
    real    driver_powerup_time_seconds;
    real    driver_powerdown_time_seconds;
    PADDING(0x38);
} s_powered_seat_block;
static_assert_check(sizeof(s_powered_seat_block) == 0x44, "Incorrect size of s_powered_seat_block");

typedef struct {
    bool invisible : 1;
    bool locked : 1;
    bool driver : 1;
    bool gunner : 1;
    bool third_person_camera : 1;
    bool allows_weapons : 1;
    bool third_person_on_enter : 1;
    bool first_person_camera_slaved_to_gun : 1;
    bool allow_vehicle_communication_animation : 1;
    bool not_valid_without_driver : 1;
    bool allow_AI_noncombatants : 1;
    bool unused0 : 5;
    bool unused1 : 8;
    bool unused2 : 8;
} s_seat_flags;
static_assert_check(sizeof(s_seat_flags) == 0x04, "Incorrect size of s_seat_flags");

typedef struct {
    s_tag_reference weapon;
    PADDING(0x14);
} s_weapon_block;
static_assert_check(sizeof(s_weapon_block) == 0x24, "Incorrect size of s_weapon_block");

typedef struct {
    s_seat_flags    flags;
    char            label[0x20];
    char            marker_name[0x20];
    PADDING(0x20);
    real_offset3d   acceration_scale;
    PADDING(0x0C);
    real            yaw_rate_degrees_per_second;
    real            pitch_rate_degrees_per_second;
    char            camera_marker_name[0x20];
    char            camera_submerged_marker_name[0x20];
    real            pitch_auto_level;       //Unknown conversion;
    real_range      pitch_range;            //Unknown conversion;
    s_tag_block     camera_tracks;          //Max is 2; loose / tight
    s_tag_block     unit_hud_interface;     //Max is 2; default/solo_player_hud / multiplayer_hud
    PADDING(0x04);
    short           hud_text_message_index;
    PADDING(0x02);
    real            yaw_minimum_radian;
    real            yaw_maximum_radian;
    s_tag_reference built_in_gunner;
    PADDING(0x14);

} s_seat_block;
static_assert_check(sizeof(s_seat_block) == 0x11C, "Incorrect size of s_seat_block");

typedef struct {
    PADDING(0x20);
    char    marker_name[0x20];
} s_contact_point_block;
static_assert_check(sizeof(s_contact_point_block) == 0x40, "Incorrect size of s_contact_point_block");

typedef struct {
    bool turns_without_animating : 1;
    bool uses_player_physics : 1;
    bool flying : 1;
    bool phsyics_pill_centered_at_origin : 1;
    bool spherical : 1;
    bool passes_through_other_bipeds : 1;
    bool can_climb_any_surface : 1;
    bool immune_to_falling_damage : 1;
    bool rotate_while_airborne : 1;
    bool uses_limp_body_physics : 1;
    bool has_no_dying_airbone : 1;
    bool random_speed_increase : 1;
    bool unit_uses_old_NTSC_player_physics : 1;
    bool unused0 : 3;
    bool unused1 : 8;
    bool unused2 : 8;
} s_biped__flags;
static_assert_check(sizeof(s_biped__flags) == 0x04, "Incorrect size of s_biped__flags");

enum e_default_team : unsigned short {
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

enum e_sound_volume : unsigned short {
    SOUND_VOLUME_SILENT=0,
    SOUND_VOLUME_MEDIUM,
    SOUND_VOLUME_LOUD,
    SOUND_VOLUME_SHOUT,
    SOUND_VOLUME_QUIET
};

enum e_unit_action : unsigned short {
    UNIT_ACTION_NONE=0,
    UNIT_ACTION_DRIVER_SEAT_POWER,
    UNIT_ACTION_GUNNER_SEAT_POWER,
    UNIT_ACTION_AIMING_CHANGE,
    UNIT_ACTION_MOUTH_APERTURE,
    UNIT_ACTION_INTEGRATED_LIGHT_POWER,
    UNIT_ACTION_CAN_BLINK,
    UNIT_ACTION_SHIELD_SAPPING
};

enum e_motion_sensor_blip_size : unsigned short {
    BLIP_SIZE_MEDIUM=0,
    BLIP_SIZE_SMALL,
    BLIP_SIZE_LARGE,
};

enum e_unknown_enum : unsigned short {
    UNKNOWN_ENUM_NONE=0,
    UNKNOWN_ENUM_FLYING_VELOCITY
};

typedef struct {
    s_biped_flags   flags;
    real            bounding_radius_world_units;
    real_offset3d   bounding_offset;
    real_offset3d   origin_offset;
    real            acceleration_scale;
    PADDING(0x04);
    s_tag_reference model;              //mod2
    s_tag_reference animation_graph;    //antr
    PADDING(0x28);
    s_tag_reference collision_model;    //coll
    s_tag_reference physics;            //phys
    s_tag_reference modifier_shader;    /* shader, shader_enviornment, shader_model, shader_transparent_chicago,
                                            shader_transparent_extended, shader_transparent_generic, shader_transparent_glass, 
                                            shader_transparent_meter, shader_transparent_plasma, or shader_transparent_water */
    s_tag_reference creation_effect;    //effe
    PADDING(0x54);
    real            render_bounding_radius_world_units;
    //export to functions
    e_export_in     export_A_in;
    e_export_in     export_B_in;
    e_export_in     export_C_in;
    e_export_in     export_D_in;
    PADDING(0x2C);
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
    real            rider_damage_fraction;
    s_tag_reference integrated_light_toggle;    //effe
    e_unit_action   unit_A_in;
    e_unit_action   unit_B_in;
    e_unit_action   unit_C_in;
    e_unit_action   unit_D_in;
    real            camera_field_of_view_radian;
    real            camera_stiffness;
    char            camera_marker_name[0x20];
    char            camera_submerged_marker_name[0x20];
    real            pitch_auto_level;   //Unknown conversion
    real_range      pitch_range;        //Unknown conversion
    s_tag_block     camera_tracks;      //Max is 2; loose & tight
    real_offset3d   seat_acceleration_scale;
    PADDING(0x0C);
    real            soft_ping_threshold;
    real            soft_ping_interruption_time_seconds;
    real            hard_ping_threshold;
    real            hard_ping_interruption_time_seconds;
    real            hard_death_threshold;
    real            feign_death_threshold;
    real            feign_death_time_seconds;
    real            distance_of_evade_anim_world_units;
    real            distance_of_dive_anim_world_units;
    PADDING(0x04);
    real            stunned_movement_threshold;
    real            feign_death_chance;
    real            feign_repeat_chance;
    s_tag_reference spawned_actor;  //actv
    short_range     spawned_actor_count;
    real            spawned_velocity;
    real            aiming_velocity_maximum_radians_per_second;
    real            aiming_acceleration_maximum_radians_per_second;
    real            casual_aiming_modifier;
    real            looking_velocity_maximum_radians_per_second;
    real            looking_acceleration_maximum_radians_per_second;
    PADDING(0x08);
    real            AI_vehicle_radius;
    real            AI_danger_radius;
    s_tag_reference melee_damage;   //jpt!
    e_motion_sensor_blip_size    motion_sensor_blip_size;
    PADDING(0x02);
    PADDING(0x0C);
    s_tag_block     new_hud_interfaces; //Max is 2; default/solo_player_hud / multiplayer_hud
    s_tag_block     dialogue_variants;  //Max is 16
    real            grenade_velocity_world_units_per_second;
    e_grenade_type  grenade_type;
    unsigned short  grenade_count;
    PADDING(0x04);
    s_tag_block     powered_seats;      //Max is 2; driver / gunner
    s_tag_block     weapons;            //Max is 4
    s_tag_block     seats;              //Max is 16
    //biped
    real            moving_turning_speed_radians_per_second;
    s_biped__flags  biped_flags;
    real            stationary_turning_threshold;   //Unknown conversion
    PADDING(0x10);
    e_unknown_enum  biped_A_in;
    e_unknown_enum  biped_B_in;
    e_unknown_enum  biped_C_in;
    e_unknown_enum  biped_D_in;
    s_tag_reference DONT_USE;       //jpt!
    //flying
    real            bank_angle_radian;
    real            bank_apply_time_seconds;
    real            bank_decay_time_seconds;
    real            pitch_ratio;
    real            max_velocity_world_units_per_second;
    real            max_sidestep_velocity_world_units_per_second;
    real            max_acceleration_world_units_per_second_squared;
    real            max_deceleration_world_units_per_second_squared;
    real            anglular_velocity_maximum_radians_per_second;
    real            anglular_acceleration_maximum_radians_per_second;
    real            crouch_velocity_modifier;
    PADDING(0x08);
    //movement
    real            maximum_slop_angle_radian;
    real            downhill_falloff_angle_radian;
    real            downhill_cutoff_angle_radian;
    real            downhill_velocity_scale;
    real            uphill_falloff_angle_radian;
    real            uphill_cutoff_angle_radian;
    real            uphill_velocity_scale;
    PADDING(0x18);
    s_tag_reference footsteps;      //foot
    PADDING(0x18);
    //jumping and landing
    real            jump_velocity_world_units_per_second;
    PADDING(0x1C);
    real            maximum_soft_land_time_seconds;
    real            maximum_hard_land_time_seconds;
    real            minimum_soft_landing_velocity_world_units_per_second;
    real            minimum_hard_landing_velocity_world_units_per_second;
    real            maximum_hard_landing_velocity_world_units_per_second;
    real            death_hard_landing_velocity_world_units_per_second;
    PADDING(0x14);
    //camera, collision, and autoaim
    real            standing_camera_height_world_units;
    real            crouching_camera_height_world_units;
    real            crouch_transition_time_seconds;
    PADDING(0x18);
    real            standing_collision_height_world_units;
    real            crouching_collision_height_world_units;
    real            collision_radius_world_units;
    PADDING(0x28);
    real            autoaim_width_world_units;
    PADDING(0x6C);
    UNKNOWN(0x20);
    s_tag_block     contact_points; //Max is 2
} s_biped_meta;
static_assert_check(sizeof(s_biped_meta) == 0x4F4, "Incorrect size of s_biped_meta");