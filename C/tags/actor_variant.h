//APPROVED
typedef struct s_actor_variant_flags {
    bool    can_shoot_while_flying : 1;
    bool    interpolate_color_in_HSV : 1;
    bool    has_unlimited_grenades : 1;
    bool    moveswitch_stay_with_friends : 1;
    bool    active_camoflage : 1;
    bool    super_active_camoflage : 1;
    bool    cannot_use_ranged_weapons : 1;
    bool    prefer_passenger_seat : 1;
    bool    unused0 : 8;
    bool    unused1 : 8;
    bool    unused2 : 8;
} s_actor_variant_flags;
static_assert_check(sizeof(s_actor_variant_flags) == 0x04, "Incorrect size of s_actor_variant_flags");

typedef struct s_change_color_block {
    real_color  color_lower_bound;
    real_color  color_upper_bound;
    PADDING(0x08);
} s_change_color_block;
static_assert_check(sizeof(s_change_color_block) == 0x20, "Incorrect size of s_change_color_block");

typedef enum e_movement_type : unsigned short {
    MOVEMENT_ALWAYS_RUN=0,
    MOVEMENT_ALWAYS_CROUCH,
    MOVEMENT_SWITCH_TYPES
} e_movement_type;

typedef enum e_special_fire_mode : unsigned short {
    SPECIAL_FIRE_MODE_NONE=0,
    SPECIAL_FIRE_MODE_OVERCHARGE,
    SPECIAL_FIRE_MODE_SECONDARY_TRIGGER
} e_special_fire_mode;

typedef enum e_special_fire_situation : unsigned short {
    SPECIAL_FIRE_SITUATION_NEVER=0,
    SPECIAL_FIRE_SITUATION_ENEMY_VISIBLE,
    SPECIAL_FIRE_SITUATION_STRAFING
} e_special_fire_situation;

typedef enum e_grenade_type : unsigned short {
    GRENADE_TYPE_HUMAN_FRAGMENTATION=0,
    GRENADE_TYPE_COVENANT_PLASMA
} e_grenade_type;

typedef enum e_trajectory_type : unsigned short {
    TRAJECTORY_TYPE_TOSS=0,
    TRAJECTORY_TYPE_LOB,
    TRAJECTORY_TYPE_BOUNCE
} e_trajectory_type;

typedef enum e_grenade_stimulus : unsigned short {
    GRENADE_STIMULUS_NEVER=0,
    GRENADE_STIMULUS_VISIBLE_TARGET,
    GRENADE_STIMULUS_SEEK_COVER
} e_grenade_stimulus;

typedef struct s_actor_variant_meta {
    s_actor_variant_flags flags;
    s_tag_reference actor_definition;   //actr
    s_tag_reference unit;               //biped, unit, or vehicle
    s_tag_reference major_variant;      //actv
    PADDING(0x18);
    //movement switching
    e_movement_type movement_type;
    PADDING(0x02);
    real    initial_crouch_chance;
    real_range  crouch_time_seconds;
    real_range  run_time_seconds;
    //ranged combat
    s_tag_reference weapon;             //weap
    real            maximum_firing_distance_world_units;
    real            rate_of_fire;
    real            projectile_error_radian;
    real_range      first_burst_delay_time_seconds;
    real            new_target_firing_pattern_time_seconds;
    real            surprise_delay_time_seconds;
    real            surprise_fire_wildly_time_seconds;
    real            death_fire_wildly_chance;
    real            death_fire_wildly_time_seconds;
    real_range      desired_combat_range_world_units;
    real_offset3d   custom_stand_gun_offset;
    real_offset3d   custom_crouch_gun_offset;
    real            target_tracking;
    real            target_leading;
    real            weapon_damage_modifier;
    real            damage_per_second;
    //burst geometry
    real            burst_origin_radius_world_units;
    real            burst_origin_radian;
    real_range      burst_return_length_world_units;
    real            burst_return_radian;
    real_range      burst_duration_seconds;
    real_range      burst_separation_seconds;
    real            burst_angular_velocity_radian_per_second;
    PADDING(0x04);
    real            special_damage_modifier;
    real            special_projectile_error_radian;
    //firing patterns
    real            new_target_burst_duration;
    real            new_target_burst_separation;
    real            new_target_rate_of_fire;
    real            new_target_projectile_error;
    PADDING(0x08);
    real            moving_burst_duration;
    real            moving_burst_separation;
    real            moving_rate_of_fire;
    real            moving_projectile_error;
    PADDING(0x08);
    real            berserk_burst_duration;
    real            berserk_burst_separation;
    real            berserk_rate_of_fire;
    real            berserk_projectile_error;
    PADDING(0x08);
    //special-case firing properties
    real            super_ballistic_range;
    real            bombardment_range;
    real            modified_vision_range;
    e_special_fire_mode special_fire_mode;
    e_special_fire_situation special_fire_situation;
    real            special_fire_chance;
    real            special_fire_delay;
    //berserking and melee
    real            melee_range_world_units;
    real            melee_abort_range_world_units;
    real_range      berserk_firing_ranges_world_units;
    real            berserk_melee_range_world_units;
    real            berserk_melee_abort_range_world_units;
    PADDING(0x08);
    //grenades
    e_grenade_type  grenade_type;
    e_trajectory_type   trajectory_type;
    e_grenade_stimulus  grenade_stimulus;
    short               minimum_enemy_count;
    real                enemy_radius_world_units;
    PADDING(0x04);
    real                grenade_velocity_world_units_per_second;
    real_range          grenade_ranges_world_units;
    real                collateral_damage_radius_world_units;
    real                grenade_chance;
    real                grenade_check_time_seconds;
    real                encounter_grenade_timeout_seconds;
    PADDING(0x14);
    //items
    s_tag_reference     equipment;  //eqip
    short_range         grenade_count;
    real                dont_drop_grenades_chance;
    real_range          drop_weapon_loaded;
    short_range         drop_weapon_ammo;
    PADDING(0x1C);
    //unit
    real                body_vitality;
    real                shield_vitality;
    real                shield_sapping_radius_world_units;
    short               forced_shader_permutation;
    PADDING(0x02);
    PADDING(0x1C);
    s_tag_block change_colors;
} s_actor_variant_meta;
static_assert_check(sizeof(s_actor_variant_meta) == 0x238, "Incorrect size of s_actor_variant_meta");