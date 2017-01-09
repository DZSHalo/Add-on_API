//APPROVED
import D.cseries.cseries;
import D.tags.tag_include;

import std.bitmanip;

struct s_actor_variant_flags {
    mixin(bitfields!(
    bool, "can_shoot_while_flying", 1,
    bool, "interpolate_color_in_HSV", 1,
    bool, "has_unlimited_grenades", 1,
    bool, "moveswitch_stay_with_friends", 1,
    bool, "active_camoflage", 1,
    bool, "super_active_camoflage", 1,
    bool, "cannot_use_ranged_weapons", 1,
    bool, "prefer_passenger_seat", 1,
    int, "unused0", 8,
    int, "unused1", 8,
    int, "unused2", 8));
};
static assert(s_actor_variant_flags.sizeof == 0x04, "Incorrect size of s_actor_variant_flags");

struct s_change_color_block {
    real_color  color_lower_bound;
    real_color  color_upper_bound;
    char[0x08] PADDING;
};
static assert(s_change_color_block.sizeof == 0x20, "Incorrect size of s_change_color_block");

enum e_movement_type : ushort {
    MOVEMENT_ALWAYS_RUN=0,
    MOVEMENT_ALWAYS_CROUCH,
    MOVEMENT_SWITCH_TYPES
};

enum e_special_fire_mode : ushort {
    SPECIAL_FIRE_MODE_NONE=0,
    SPECIAL_FIRE_MODE_OVERCHARGE,
    SPECIAL_FIRE_MODE_SECONDARY_TRIGGER
};

enum e_special_fire_situation : ushort {
    SPECIAL_FIRE_SITUATION_NEVER=0,
    SPECIAL_FIRE_SITUATION_ENEMY_VISIBLE,
    SPECIAL_FIRE_SITUATION_STRAFING
};

enum e_grenade_type : ushort {
    GRENADE_TYPE_HUMAN_FRAGMENTATION=0,
    GRENADE_TYPE_COVENANT_PLASMA
};

enum e_trajectory_type : ushort {
    TRAJECTORY_TYPE_TOSS=0,
    TRAJECTORY_TYPE_LOB,
    TRAJECTORY_TYPE_BOUNCE
};

enum e_grenade_stimulus : ushort {
    GRENADE_STIMULUS_NEVER=0,
    GRENADE_STIMULUS_VISIBLE_TARGET,
    GRENADE_STIMULUS_SEEK_COVER
};

struct s_actor_variant_meta {
    s_actor_variant_flags flags;
    s_tag_reference actor_definition;   //actr
    s_tag_reference unit;               //biped, unit, or vehicle
    s_tag_reference major_variant;      //actv
    char[0x18] PADDING0;
    //movement switching
    e_movement_type movement_type;
    char[0x02] PADDING1;
    _real    initial_crouch_chance;
    real_range  crouch_time_seconds;
    real_range  run_time_seconds;
    //ranged combat
    s_tag_reference weapon;             //weap
    _real            maximum_firing_distance_world_units;
    _real            rate_of_fire;
    _real            projectile_error_radian;
    real_range      first_burst_delay_time_seconds;
    _real            new_target_firing_pattern_time_seconds;
    _real            surprise_delay_time_seconds;
    _real            surprise_fire_wildly_time_seconds;
    _real            death_fire_wildly_chance;
    _real            death_fire_wildly_time_seconds;
    real_range      desired_combat_range_world_units;
    real_offset3d   custom_stand_gun_offset;
    real_offset3d   custom_crouch_gun_offset;
    _real            target_tracking;
    _real            target_leading;
    _real            weapon_damage_modifier;
    _real            damage_per_second;
    //burst geometry
    _real            burst_origin_radius_world_units;
    _real            burst_origin_radian;
    real_range      burst_return_length_world_units;
    _real            burst_return_radian;
    real_range      burst_duration_seconds;
    real_range      burst_separation_seconds;
    _real            burst_angular_velocity_radian_per_second;
    char[0x04] PADDING2;
    _real            special_damage_modifier;
    _real            special_projectile_error_radian;
    //firing patterns
    _real            new_target_burst_duration;
    _real            new_target_burst_separation;
    _real            new_target_rate_of_fire;
    _real            new_target_projectile_error;
    char[0x08] PADDING3;
    _real            moving_burst_duration;
    _real            moving_burst_separation;
    _real            moving_rate_of_fire;
    _real            moving_projectile_error;
    char[0x08] PADDING4;
    _real            berserk_burst_duration;
    _real            berserk_burst_separation;
    _real            berserk_rate_of_fire;
    _real            berserk_projectile_error;
    char[0x08] PADDING5;
    //special-case firing properties
    _real            super_ballistic_range;
    _real            bombardment_range;
    _real            modified_vision_range;
    e_special_fire_mode special_fire_mode;
    e_special_fire_situation special_fire_situation;
    _real            special_fire_chance;
    _real            special_fire_delay;
    //berserking and melee
    _real            melee_range_world_units;
    _real            melee_abort_range_world_units;
    real_range      berserk_firing_ranges_world_units;
    _real            berserk_melee_range_world_units;
    _real            berserk_melee_abort_range_world_units;
    char[0x08] PADDING6;
    //grenades
    e_grenade_type  grenade_type;
    e_trajectory_type   trajectory_type;
    e_grenade_stimulus  grenade_stimulus;
    short               minimum_enemy_count;
    _real                enemy_radius_world_units;
    char[0x04] PADDING7;
    _real                grenade_velocity_world_units_per_second;
    real_range          grenade_ranges_world_units;
    _real                collateral_damage_radius_world_units;
    _real                grenade_chance;
    _real                grenade_check_time_seconds;
    _real                encounter_grenade_timeout_seconds;
    char[0x14] PADDING8;
    //items
    s_tag_reference     equipment;  //eqip
    short_range         grenade_count;
    _real                dont_drop_grenades_chance;
    real_range          drop_weapon_loaded;
    short_range         drop_weapon_ammo;
    char[0x1C] PADDING9;
    //unit
    _real                body_vitality;
    _real                shield_vitality;
    _real                shield_sapping_radius_world_units;
    short               forced_shader_permutation;
    char[0x02] PADDING10;
    char[0x1C] PADDING11;
    s_tag_block change_colors;
};
static assert(s_actor_variant_meta.sizeof == 0x238, s_actor_variant_meta.sizeof);
static assert(s_actor_variant_meta.sizeof == 0x238, "Incorrect size of s_actor_variant_meta");