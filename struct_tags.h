#ifndef struct_tagsH
#define struct_tagsH

#pragma pack(push, 1)
struct h_tag_weap {
    UNKNOWN(0x308);
    bool vertical_heat_display:1;                           //0x308.0
    bool mutually_exclusive_triggers:1;                     //0x308.1
    bool attack_automatically_on_bump:1;                    //0x308.2
    bool must_be_readied:1;                                 //0x308.3
    bool doesnt_count_toward_maximum:1;                     //0x308.4
    bool aim_assist_only_when_zoomed:1;                     //0x308.5
    bool prevents_grenade_throwing:1;                       //0x308.6
    bool must_be_picked_up:1;                               //0x308.7
    bool hold_trigger_when_dropped:1;                       //0x309.0
    bool prevent_melee_attack:1;                            //0x309.1
    bool detonate_when_dropped:1;                           //0x309.2
    bool cannot_fire_at_maximum_age:1;                      //0x309.3
    bool secondary_trigger_override_grenade:1;              //0x309.4
    bool obsolete_does_not_depower_active_camo_in_mp:1;     //0x309.5
    bool enable_integrated_night_vision:1;                  //0x309.6
    bool ai_use_weapon_melee_damage:1;                      //0x309.7
};
//static_assert_check(sizeof(h_tag_weap) == 0x236, "Incorrect size of h_tag_weap");

#pragma pack(pop)

#endif