//APPROVED
typedef struct s_actor_flags {
    bool    can_see_in_darkness : 1;
    bool    sneak_uncovering_target : 1;
    bool    sneak_uncovering_pursuit_position : 1;
    bool    unused0 : 1;
    bool    shoot_at_target_last_location : 1;
    bool    try_to_stay_still_when_crouched : 1;
    bool    crouch_when_not_in_combat : 1;
    bool    crouch_when_guarding : 1;
    bool    unused1 : 1;
    bool    must_crouch_to_shoot : 1;
    bool    panic_when_surprised : 1;
    bool    always_charge_at_enemies : 1;
    bool    gets_in_vehicles_with_player : 1;
    bool    start_firing_before_aligned : 1;
    bool    standing_must_move_forward : 1;
    bool    crouching_must_move_forward : 1;
    bool    defensive_crouch_while_charging : 1;
    bool    use_stalking_behavior : 1;
    bool    stalking_freeze_if_exposed : 1;
    bool    always_berserk_in_attacking_mode : 1;
    bool    berserking_uses_panicked_movement : 1;
    bool    flying : 1;
    bool    panicked_by_unopposable_enemy : 1;
    bool    crouch_when_hiding_from_unopposable_enemy : 1;
    bool    always_charge_in_attacking_mode : 1;
    bool    dive_off_ledges : 1;
    bool    swarm : 1;
    bool    suicidal_melee_attack : 1;
    bool    cannot_move_while_crouching : 1;
    bool    fixed_crouch_facing : 1;
    bool    crouch_when_in_line_of_file : 1;
    bool    avoid_friends_line_of_fire : 1;
} s_actor_flags;
static_assert_check(sizeof(s_actor_flags) == 0x04, "Incorrect size of s_actor_flags");

typedef struct s_actor_more_flags {
    bool    avoid_all_enemy_attack_vectors : 1;
    bool    must_stand_to_fire : 1;
    bool    must_stop_to_fire : 1;
    bool    disallow_vehicle_combat : 1;
    bool    pathfinding_ignores_danger : 1;
    bool    panic_in_groups : 1;
    bool    no_corpse_shooting : 1;
    bool    unused0 : 1;
    bool    unused1 : 8;
    bool    unused2 : 8;
    bool    unused3 : 8;
} s_actor_more_flags;
static_assert_check(sizeof(s_actor_more_flags) == 0x04, "Incorrect size of s_actor_more_flags");

typedef enum e_actor_type : unsigned short {
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
} e_actor_type;

typedef enum e_danger_trigger : unsigned short {
    TRIGGER_NEVER = 0,
    TRIGGER_SHOOTING,
    TRIGGER_SHOOTING_NEAR_US,
    TRIGGER_DAMAGIN_US,
    TRIGGER_UNUSED0,
    TRIGGER_UNUSED1,
    TRIGGER_UNUSED2,
    TRIGGER_UNUSED3,
    TRIGGER_UNUSED4
} e_danger_trigger;

typedef enum e_crouch_type : unsigned short {
    CROUCH_NEVER=0,
    CROUCH_DANGER,
    CROUCH_LOW_SHIELDS,
    CROUCH_HIGH_BEHIND_SHIELD,
    CROUCH_ANY_TARGET,
    CROUCH_FLOOD_SHAMBLE
} e_crouch_type;

typedef struct s_actor_meta {
    s_actor_flags       flags;
    s_actor_more_flags  more_flags;
    PADDING(0x0C);
    e_actor_type        type;
    PADDING(0x02);
    //perception
    real                max_vision_distance_world_units;
    real                central_vision_angle_radian;
    real                max_vision_angle_radian;
    PADDING(0x04);
    real                peripheral_vision_angle_radian;
    real                peripheral_disance_world_units;
    PADDING(0x04);
    real_offset3d       standing_gun_offset;
    real_offset3d       crouching_gun_offset;
    real                hearing_distance_world_units;
    real                notice_projectile_chance;
    real                notice_vehicle_chance;
    PADDING(0x08);
    real                combat_perception_time_seconds;
    real                guard_perception_time_seconds;
    real                non_combat_perception_time_seconds;
    PADDING(0x14);
    //movement
    real                dive_into_cover_chance;
    real                emerge_from_cover_chance;
    real                dive_from_grenade_chance;
    real                pathfinding_radius_world_units;
    real                glass_ignorance_chance;
    real                stationary_movment_distance_world_units;
    real                free_flying_sidestep_world_units;
    real                begin_moving_angle_radian;
    PADDING(0x04);
    //looking
    real_rotation2d     maximum_aiming_deviation_radian;
    real_rotation2d     maximum_looking_devation_radian;
    real                noncombat_look_delta_L_radian;
    real                noncombat_look_delta_R_radian;
    real                combat_look_delta_L_radian;
    real                combat_look_delta_R_radian;
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
    PADDING(0x18);
    s_tag_reference     DO_NOT_USE_0;       //weap
    PADDING(0x10C);
    s_tag_reference     DO_NOT_USE_1;       //proj
    //unopposable
    e_danger_trigger    unreachable_danger_trigger;
    e_danger_trigger    vehicle_danger_trigger;
    e_danger_trigger    player_danger_trigger;
    PADDING(0x02);
    real_range          danger_trigger_time_seconds;
    short               friends_killed_trigger;
    short               friends_retreating_trigger;
    PADDING(0x0C);
    real_range          retreat_time_seconds;
    PADDING(0x08);
    //panic
    real_range          cowering_time_seconds;
    real                friend_killed_panic_chance;
    e_actor_type        leader_type;
    PADDING(0x02);
    real                leader_killed_panic_chance;
    real                panic_damage_threshold;
    real                surpise_distance_world_units;
    PADDING(0x1C);
    //defensive
    real_range          hide_behind_cover_time_seconds;
    real                hide_target_not_visible_time_seconds;
    real                hide_shield_fraction;
    real                attack_shield_fraction;
    real                pursue_shield_fraction;
    PADDING(0x10);
    e_crouch_type       defensive_crouch_type;
    PADDING(0x02);
    real                attacking_crouch_threshold;
    real                defending_crouch_threshold;
    real                min_stand_time_seconds;
    real                min_crouch_time_seconds;
    real                defending_hide_time_modifier;
    real                attacking_evasion_threshold;
    real                defending_evasion_threshold;
    real                evasion_seek_cover_chance;
    real                evasion_delay_time_seconds;
    real                max_seek_cover_distance_world_units;
    real                cover_damage_threshold;
    real                stalking_discovery_time_seconds;
    real                stalking_max_distance_world_units;
    real                stationary_facing_angle_radian;
    real                change_facing_stand_time_seconds;
    PADDING(0x04);
    //pursuit
    real_range          uncover_delay_time_seconds;
    real_range          target_search_time_seconds;
    real_range          pursuit_position_time_seconds;
    short               number_positions_coordination;
    short               number_positions_normal;
    PADDING(0x20);
    //berserk
    real                melee_attack_delay_seconds;
    real                melee_fudge_factor_world_units;
    real                melee_charge_time_seconds;
    real_range          melee_leap_range_world_units;
    real                melee_leap_velocity_world_units_per_tick;
    real                melee_leap_chance;
    real                melee_leap_ballistic;
    real                berserk_damage_amount;
    real                berserk_damage_threshold;
    real                berserk_proximity_world_units;
    real                suicide_sensing_dist_world_units;
    real                berserk_grenade_chance;
    PADDING(0x0C);
    //firing positions
    real_range          guard_position_time_seconds;
    real_range          combat_position_time_seconds;
    real                old_position_avoid_distance_seconds;
    real                friend_avoid_distance_world_units;
    PADDING(0x28);
    //communication;
    real_range          noncombat_idle_speech_time_seconds;
    real_range          combat_idle_speech_time_seconds;
    PADDING(0xB0);
    s_tag_reference     DO_NOT_USE_2;       //actr
    PADDING(0x30);
} s_actor_meta;
static_assert_check(sizeof(s_actor_meta) == 0x4F8, "Incorrect size of s_actor_meta");