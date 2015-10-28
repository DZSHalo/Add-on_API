//APPROVED
enum e_side_effect : unsigned short {
    SIDE_EFFECT_NONE = 0,
    SIDE_EFFECT_HARMLESS,
    SIDE_EFFECT_LETHAL_TO_THE_UNSUSPECTING,
    SIDE_EFFECT_EMP
};

enum e_category : unsigned short {
    CATEGORY_NONE = 0,
    CATEGORY_FALLING,
    CATEGORY_BULLET,
    CATEGORY_GRENADE,
    CATEGORY_HIGH_EXPLOSIVE,
    CATEGORY_SNIPER,
    CATEGORY_MELEE,
    CATEGORY_FLAME,
    CATEGORY_MOUNTED_WEAPON,
    CATEGORY_VEHICLE,
    CATEGORY_PLASMA,
    CATEGORY_NEEDLE,
    CATEGORY_SHOTGUN
};

typedef struct {
    bool does_not_hurt_owner : 1;
    bool can_cause_headshots : 1;
    bool pings_resistant_units : 1;
    bool does_not_hurt_friends : 1;
    bool does_not_ping_units : 1;
    bool detonates_explosives : 1;
    bool only_hurts_shields : 1;
    bool cause_flaming_death : 1;
    bool damage_indicators_always_point_down : 1;
    bool skips_shields : 1;
    bool only_hurts_one_infection_form : 1;
    bool can_cause_multiplayer_headshots : 1;
    bool infection_form_pop : 1;
    bool unused0 : 3;
    bool unused1 : 8;
    bool unused2 : 8;
} s_damage_effect_flags;
static_assert_check(sizeof(s_damage_effect_flags) == 0x4, "Incorrect size of s_damage_effect_flags");

typedef struct {
    real_range              radius_world_units;
    real                    cutoff_scale;
    PADDING(0x18);
    //viberate parameters
    real                    low_frequency;
    real                    high_frequency;
    PADDING(0x18);
    //camera shaking
    real                    random_translation_in_world_unit;
    int                     unknown_value;
    PADDING(0x0C);
    e_function       wobble_function;
    PADDING(0x02);
    real                    wobble_function_period_in_second;
    real                    wobble_weight;
    PADDING(0xC0);
    //damage
    e_side_effect           side_effect;
    e_category              category;
    //flags;
    s_damage_effect_flags   flags;
    PADDING(0x04);
    real                    damage_lower_bound;
    real_range              damage_upper_bound;
    real                    vehicle_passthrough_penalty;
    PADDING(0x04);
    real                    stun;
    real                    maximum_stun;
    real                    stun_time;
    PADDING(0x04);
    real                    instantaneous_acceleration;
    PADDING(0x08);
    //damage modifiers
    real                    dirt;
    real                    sand;
    real                    stone;
    real                    snow;
    real                    wood;
    real                    metal_hollow;
    real                    metal_thin;
    real                    metal_thick;
    real                    rubber;
    real                    glass;
    real                    force_field;
    real                    grunt;
    real                    hunter_armor;
    real                    hunter_skin;
    real                    elite;
    real                    jackal;
    real                    jackal_engery_shield;
    real                    engineer;
    real                    engineer_force_field;
    real                    flood_combat_form;
    real                    flood_carrier_form;
    real                    cyborg;
    real                    cyborg_engery_shield;
    real                    armored_human;
    real                    human;
    real                    sentinel;
    real                    monitor;
    real                    plastic;
    real                    water;
    real                    leaves;
    real                    elite_energy_shield;
    real                    ice;
    real                    hunter_shield;
} s_continuous_damage_effect_meta;
static_assert_check(sizeof(s_continuous_damage_effect_meta) == 0x1E4, "Incorrect size of s_continuous_damage_effect_meta");