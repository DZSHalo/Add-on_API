//APPROVED
import D.cseries.cseries;
import D.tags.tag_include;

import std.bitmanip;

struct s_actor_flags {
    int bitfield;
    /*bool, "can_see_in_darkness", 1,
    bool, "sneak_uncovering_target", 1,
    bool, "sneak_uncovering_pursuit_position", 1,
    bool, "unused0", 1,
    bool, "shoot_at_target_last_location", 1,
    bool, "try_to_stay_still_when_crouched", 1,
    bool, "crouch_when_not_in_combat", 1,
    bool, "crouch_when_guarding", 1,
    bool, "unused1", 1,
    bool, "must_crouch_to_shoot", 1,
    bool, "panic_when_surprised", 1,
    bool, "always_charge_at_enemies", 1,
    bool, "gets_in_vehicles_with_player", 1,
    bool, "start_firing_before_aligned", 1,
    bool, "standing_must_move_forward", 1,
    bool, "crouching_must_move_forward", 1,
    bool, "defensive_crouch_while_charging", 1,
    bool, "use_stalking_behavior", 1,
    bool, "stalking_freeze_if_exposed", 1,
    bool, "always_berserk_in_attacking_mode", 1,
    bool, "berserking_uses_panicked_movement", 1,
    bool, "flying", 1,
    bool, "panicked_by_unopposable_enemy", 1,
    bool, "crouch_when_hiding_from_unopposable_enemy", 1,
    bool, "always_charge_in_attacking_mode", 1,
    bool, "dive_off_ledges", 1,
    bool, "swarm", 1,
    bool, "suicidal_melee_attack", 1,
    bool, "cannot_move_while_crouching", 1,
    bool, "fixed_crouch_facing", 1,
    bool, "crouch_when_in_line_of_file", 1,
    bool, "avoid_friends_line_of_fire", 1,*/
};
static assert(s_actor_flags.sizeof == 0x04, "Incorrect size of s_actor_flags");

struct s_actor_more_flags {
    mixin(bitfields!(
    bool, "avoid_all_enemy_attack_vectors", 1,
    bool, "must_stand_to_fire", 1,
    bool, "must_stop_to_fire", 1,
    bool, "disallow_vehicle_combat", 1,
    bool, "pathfinding_ignores_danger", 1,
    bool, "panic_in_groups", 1,
    bool, "no_corpse_shooting", 1,
    bool, "unused0", 1,
    int, "unused1", 8,
    int, "unused2", 8,
    int, "unused3", 8));
};
static assert(s_actor_more_flags.sizeof == 0x04, "Incorrect size of s_actor_more_flags");

enum e_actor_type : ushort {
    ACTOR_ELITE = 0,
    ACTOR_JACKAL,
    ACTOR_GRUNT,
    ACTOR_HUNTER,
    ACTOR_ENGINEER,
    ACTOR_ASSASSIN,
    ACTOR_PLAYER,
    ACTOR_MARINE,
    ACTOR_CREW,
    ACTOR_COMBAT_FORM,
    ACTOR_INFECTION_FORM,
    ACTOR_CARRIER_FORM,
    ACTOR_MONITOR,
    ACTOR_SENTINEL,
    ACTOR_NONE,
    ACTOR_MOUNTED_WEAPON
};

enum e_danger_trigger : ushort {
    TRIGGER_NEVER = 0,
    TRIGGER_SHOOTING,
    TRIGGER_SHOOTING_NEAR_US,
    TRIGGER_DAMAGIN_US,
    TRIGGER_UNUSED0,
    TRIGGER_UNUSED1,
    TRIGGER_UNUSED2,
    TRIGGER_UNUSED3,
    TRIGGER_UNUSED4
};

enum e_crouch_type : ushort {
    CROUCH_NEVER=0,
    CROUCH_DANGER,
    CROUCH_LOW_SHIELDS,
    CROUCH_HIGH_BEHIND_SHIELD,
    CROUCH_ANY_TARGET,
    CROUCH_FLOOD_SHAMBLE
};

struct s_actor_meta {
    s_actor_flags       flags;
    s_actor_more_flags  more_flags;
    char[0x0C] PADDING0;
    e_actor_type        type;
    char[0x02] PADDING1;
    //perception
    _real                max_vision_distance_world_units;
    _real                central_vision_angle_radian;
    _real                max_vision_angle_radian;
    char[0x04] PADDING2;
    _real                peripheral_vision_angle_radian;
    _real                peripheral_disance_world_units;
    char[0x04] PADDING3;
    real_offset3d       standing_gun_offset;
    real_offset3d       crouching_gun_offset;
    _real                hearing_distance_world_units;
    _real                notice_projectile_chance;
    _real                notice_vehicle_chance;
    char[0x08] PADDING4;
    _real                combat_perception_time_seconds;
    _real                guard_perception_time_seconds;
    _real                non_combat_perception_time_seconds;
    char[0x14] PADDING5;
    //movement
    _real                dive_into_cover_chance;
    _real                emerge_from_cover_chance;
    _real                dive_from_grenade_chance;
    _real                pathfinding_radius_world_units;
    _real                glass_ignorance_chance;
    _real                stationary_movment_distance_world_units;
    _real                free_flying_sidestep_world_units;
    _real                begin_moving_angle_radian;
    char[0x04] PADDING6;
    //looking
    real_rotation2d     maximum_aiming_deviation_radian;
    real_rotation2d     maximum_looking_devation_radian;
    _real                noncombat_look_delta_L_radian;
    _real                noncombat_look_delta_R_radian;
    _real                combat_look_delta_L_radian;
    _real                combat_look_delta_R_radian;
    real_rotation2d     idle_aiming_range_radian;
    real_rotation2d     idle_looking_ragne_radian;
    real_range          event_look_time_modifier;
    real_range          noncombat_idle_facing_seconds;
    real_range          noncombat_idle_aiming_seconds;
    real_range          noncombat_idle_looking_seconds;
    real_range          guard_idle_facing_seconds;
    real_range          guard_idle_aiming_seconds;
    real_range          guard_idle_looking_seconds;
    real_range          combat_idle_facing_seconds;
    real_range          combat_idle_aiming_seconds;
    real_range          combat_idle_looking_seconds;
    char[0x18] PADDING7;
    s_tag_reference     DO_NOT_USE_0;       //weap
    char[0x10C] PADDING8;
    s_tag_reference     DO_NOT_USE_1;       //proj
    //unopposable
    e_danger_trigger    unreachable_danger_trigger;
    e_danger_trigger    vehicle_danger_trigger;
    e_danger_trigger    player_danger_trigger;
    char[0x02] PADDING9;
    real_range          danger_trigger_time_seconds;
    short               friends_killed_trigger;
    short               friends_retreating_trigger;
    char[0x0C] PADDING10;
    real_range          retreat_time_seconds;
    char[0x08] PADDING11;
    //panic
    real_range          cowering_time_seconds;
    _real                friend_killed_panic_chance;
    e_actor_type        leader_type;
    char[0x02] PADDING12;
    _real                leader_killed_panic_chance;
    _real                panic_damage_threshold;
    _real                surpise_distance_world_units;
    char[0x1C] PADDING13;
    //defensive
    real_range          hide_behind_cover_time_seconds;
    _real                hide_target_not_visible_time_seconds;
    _real                hide_shield_fraction;
    _real                attack_shield_fraction;
    _real                pursue_shield_fraction;
    char[0x10] PADDING14;
    e_crouch_type       defensive_crouch_type;
    char[0x02] PADDING15;
    _real                attacking_crouch_threshold;
    _real                defending_crouch_threshold;
    _real                min_stand_time_seconds;
    _real                min_crouch_time_seconds;
    _real                defending_hide_time_modifier;
    _real                attacking_evasion_threshold;
    _real                defending_evasion_threshold;
    _real                evasion_seek_cover_chance;
    _real                evasion_delay_time_seconds;
    _real                max_seek_cover_distance_world_units;
    _real                cover_damage_threshold;
    _real                stalking_discovery_time_seconds;
    _real                stalking_max_distance_world_units;
    _real                stationary_facing_angle_radian;
    _real                change_facing_stand_time_seconds;
    char[0x04] PADDING16;
    //pursuit
    real_range          uncover_delay_time_seconds;
    real_range          target_search_time_seconds;
    real_range          pursuit_position_time_seconds;
    short               number_positions_coordination;
    short               number_positions_normal;
    char[0x20] PADDING17;
    //berserk
    _real                melee_attack_delay_seconds;
    _real                melee_fudge_factor_world_units;
    _real                melee_charge_time_seconds;
    real_range          melee_leap_range_world_units;
    _real                melee_leap_velocity_world_units_per_tick;
    _real                melee_leap_chance;
    _real                melee_leap_ballistic;
    _real                berserk_damage_amount;
    _real                berserk_damage_threshold;
    _real                berserk_proximity_world_units;
    _real                suicide_sensing_dist_world_units;
    _real                berserk_grenade_chance;
    char[0x0C] PADDING18;
    //firing positions
    real_range          guard_position_time_seconds;
    real_range          combat_position_time_seconds;
    _real                old_position_avoid_distance_seconds;
    _real                friend_avoid_distance_world_units;
    char[0x28] PADDING19;
    //communication;
    real_range          noncombat_idle_speech_time_seconds;
    real_range          combat_idle_speech_time_seconds;
    char[0xB0] PADDING20;
    s_tag_reference     DO_NOT_USE_2;       //actr
    char[0x30] PADDING21;
};
static assert(s_actor_meta.sizeof == 0x4F8, "Incorrect size of s_actor_meta");