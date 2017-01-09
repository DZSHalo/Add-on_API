//APPROVED
import D.cseries.cseries;
import D.tags.tag_include;

import std.bitmanip;

enum e_side_effect : ushort {
    SIDE_EFFECT_NONE = 0,
    SIDE_EFFECT_HARMLESS,
    SIDE_EFFECT_LETHAL_TO_THE_UNSUSPECTING,
    SIDE_EFFECT_EMP
};

enum e_category : ushort {
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

struct s_damage_effect_flags {
    mixin(bitfields!(
    bool, "does_not_hurt_owner", 1,
    bool, "can_cause_headshots", 1,
    bool, "pings_resistant_units", 1,
    bool, "does_not_hurt_friends", 1,
    bool, "does_not_ping_units", 1,
    bool, "detonates_explosives", 1,
    bool, "only_hurts_shields", 1,
    bool, "cause_flaming_death", 1,
    bool, "damage_indicators_always_point_down", 1,
    bool, "skips_shields", 1,
    bool, "only_hurts_one_infection_form", 1,
    bool, "can_cause_multiplayer_headshots", 1,
    bool, "infection_form_pop", 1,
    uint, "unused0", 3,
    uint, "unused1", 8,
    uint, "unused2", 8));
};
static assert(s_damage_effect_flags.sizeof == 0x4, "Incorrect size of s_damage_effect_flags");

struct s_continuous_damage_effect_meta {
    real_range              radius_world_units;
    _real                    cutoff_scale;
    char[0x18] PADDING0;
    //viberate parameters
    _real                    low_frequency;
    _real                    high_frequency;
    char[0x18] PADDING1;
    //camera shaking
    _real                    random_translation_in_world_unit;
    int                     unknown_value;
    char[0x0C] PADDING2;
    e_function       wobble_function;
    char[0x02] PADDING3;
    _real                    wobble_function_period_in_second;
    _real                    wobble_weight;
    char[0xC0] PADDING4;
    //damage
    e_side_effect           side_effect;
    e_category              category;
    //flags;
    s_damage_effect_flags   flags;
    char[0x04] PADDING5;
    _real                    damage_lower_bound;
    real_range              damage_upper_bound;
    _real                    vehicle_passthrough_penalty;
    char[0x04] PADDING6;
    _real                    stun;
    _real                    maximum_stun;
    _real                    stun_time;
    char[0x04] PADDING7;
    _real                    instantaneous_acceleration;
    char[0x08] PADDING8;
    //damage modifiers
    _real                    dirt;
    _real                    sand;
    _real                    stone;
    _real                    snow;
    _real                    wood;
    _real                    metal_hollow;
    _real                    metal_thin;
    _real                    metal_thick;
    _real                    rubber;
    _real                    glass;
    _real                    force_field;
    _real                    grunt;
    _real                    hunter_armor;
    _real                    hunter_skin;
    _real                    elite;
    _real                    jackal;
    _real                    jackal_engery_shield;
    _real                    engineer;
    _real                    engineer_force_field;
    _real                    flood_combat_form;
    _real                    flood_carrier_form;
    _real                    cyborg;
    _real                    cyborg_engery_shield;
    _real                    armored_human;
    _real                    human;
    _real                    sentinel;
    _real                    monitor;
    _real                    plastic;
    _real                    water;
    _real                    leaves;
    _real                    elite_energy_shield;
    _real                    ice;
    _real                    hunter_shield;
};
static assert(s_continuous_damage_effect_meta.sizeof == 0x1E4, "Incorrect size of s_continuous_damage_effect_meta");