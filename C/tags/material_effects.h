//Approved
/*
 * dirt,
 * sand,
 * stone,
 * snow,
 * wood,
 * metal (hollow),
 * metal (thin),
 * metal (thick),
 * rubber,
 * glass,
 * force_field,
 * grunt,
 * hunter_armor,
 * hunter_skin,
 * elite,
 * jackal,
 * jackal_engery_shield,
 * engineer_skin,
 * engineer_force_field,
 * flood_combat_form,
 * flood_carrier_form,
 * cyborg_armor,
 * cyborg_energy_shield,
 * human_armor,
 * human_skin,
 * sentinel,
 * monitor,
 * plastic,
 * water,
 * leaves,
 * elite_engery_shield,
 * ice,
 * hunter_energy_shield
 */
typedef struct s_matrials_block {
    s_tag_reference effect; //effe
    s_tag_reference sound;  //snd!
    PADDING(0x10);
} s_matrials_block;
static_assert_check(sizeof(s_matrials_block) == 0x30, "Incorrect size of s_matrials_block");

/*
 * walk,
 * run,
 * sliding,
 * shuffle,
 * jump,
 * jump_land,
 * biped_unused1,
 * biped_unused2,
 * impact,
 * vehicle_tire_slip,
 * vehicle_chassis_slip,
 * vehicle_unused1,
 * vehicle_unused2
 */
typedef struct s_effects_block {
    //Materials block
    s_tag_block materials; //Max is 33
    PADDING(0x10);
} s_effects_block;
static_assert_check(sizeof(s_effects_block) == 0x1C, "Incorrect size of s_effects_block");


typedef struct s_material_effects_meta {
    //Effects block
    s_tag_block effects; //Max is 13
    PADDING(0x80);
} s_material_effects_meta;
static_assert_check(sizeof(s_material_effects_meta) == 0x8C, "Incorrect size of s_material_effects_meta");