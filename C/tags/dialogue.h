//APPROVED
typedef struct {
    PADDING(0x10);
    //idle
    s_tag_reference idle_noncombat;                 //snd!
    s_tag_reference idle_combat;                    //snd!
    s_tag_reference idle_flee;                      //snd!
    PADDING(0x30);
    //involuntary
    s_tag_reference pain_body_minor;                //snd!
    s_tag_reference pain_body_major;                //snd!
    s_tag_reference pain_shield;                    //snd!
    s_tag_reference pain_falling;                   //snd!
    s_tag_reference scream_fear;                    //snd!
    s_tag_reference scream_pain;                    //snd!
    s_tag_reference maimed_limb;                    //snd!
    s_tag_reference maimed_head;                    //snd!
    s_tag_reference death_quiet;                    //snd!
    s_tag_reference death_violent;                  //snd!
    s_tag_reference death_falling;                  //snd!
    s_tag_reference death_agonizing;                //snd!
    s_tag_reference death_instant;                  //snd!
    s_tag_reference death_flying;                   //snd!
    PADDING(0x10);
    //hurting people
    s_tag_reference damaged_friend;                 //snd!
    s_tag_reference damaged_friend_player;          //snd!
    s_tag_reference damaged_enemy;                  //snd!
    s_tag_reference damaged_enemy_cm;               //snd!
    PADDING(0x40);
    //being hurt
    s_tag_reference hurt_friend;                    //snd!
    s_tag_reference hurt_friend_re;                 //snd!
    s_tag_reference hurt_enemy_player;              //snd!
    s_tag_reference hurt_enemy;                     //snd!
    s_tag_reference hurt_enemy_re;                  //snd!
    s_tag_reference hurt_enemy_cm;                  //snd!
    s_tag_reference hurt_enemy_bullet;              //snd!
    s_tag_reference hurt_enemy_needler;             //snd!
    s_tag_reference hurt_enemy_plasma;              //snd!
    s_tag_reference hurt_enemy_sniper;              //snd!
    s_tag_reference hurt_enemy_grenade;             //snd!
    s_tag_reference hurt_enemy_explosion;           //snd!
    s_tag_reference hurt_enemy_melee;               //snd!
    s_tag_reference hurt_enemy_flame;               //snd!
    s_tag_reference hurt_enemy_shotgun;             //snd!
    s_tag_reference hurt_enemy_vehicle;             //snd!
    s_tag_reference hurt_enemy_mountedweapon;       //snd!
    PADDING(0x30);
    //killing people
    s_tag_reference killed_friend;                  //snd!
    s_tag_reference killed_friend_cm;               //snd!
    s_tag_reference killed_friend_player;           //snd!
    s_tag_reference killed_friend_player_cm;        //snd!
    s_tag_reference killed_enemy;                   //snd!
    s_tag_reference killed_enemy_cm;                //snd!
    s_tag_reference killed_enemy_player;            //snd!
    s_tag_reference killed_enemy_player_cm;         //snd!
    s_tag_reference killed_enemy_covenant;          //snd!
    s_tag_reference killed_enemy_covenant_cm;       //snd!
    s_tag_reference killed_enemy_floodcombat;       //snd!
    s_tag_reference killed_enemy_floodcombat_cm;    //snd!
    s_tag_reference killed_enemy_floodcarrier;      //snd!
    s_tag_reference killed_enemy_floodcarrier_cm;   //snd!
    s_tag_reference killed_enemy_sentinel;          //snd!
    s_tag_reference killed_enemy_sentinel_cm;       //snd!
    s_tag_reference killed_enemy_bullet;            //snd!
    s_tag_reference killed_enemy_needler;           //snd!
    s_tag_reference killed_enemy_plasma;            //snd!
    s_tag_reference killed_enemy_sniper;            //snd!
    s_tag_reference killed_enemy_grenade;           //snd!
    s_tag_reference killed_enemy_explosion;         //snd!
    s_tag_reference killed_enemy_melee;             //snd!
    s_tag_reference killed_enemy_flame;             //snd!
    s_tag_reference killed_enemy_shotgun;           //snd!
    s_tag_reference killed_enemy_vehicle;           //snd!
    s_tag_reference killed_enemy_mountedweapon;     //snd!
    s_tag_reference killed_spree;                   //snd!
    PADDING(0x30);
    //player kill responses
    s_tag_reference player_kill_cm;                 //snd!
    s_tag_reference player_kill_bullet_cm;          //snd!
    s_tag_reference player_kill_needler_cm;         //snd!
    s_tag_reference player_kill_plasma_cm;          //snd!
    s_tag_reference player_kill_sniper_cm;          //snd!
    s_tag_reference anyone_kill_grenade_cm;         //snd!
    s_tag_reference player_kill_explosion_cm;       //snd!
    s_tag_reference player_kill_melee_cm;           //snd!
    s_tag_reference player_kill_flame_cm;           //snd!
    s_tag_reference player_kill_shotgun_cm;         //snd!
    s_tag_reference player_kill_vehicle_cm;         //snd!
    s_tag_reference player_kill_mountedweapon_cm;   //snd!
    s_tag_reference player_killing_spree_cm;        //snd!
    PADDING(0x30);
    //friends dying
    s_tag_reference friend_died;                    //snd!
    s_tag_reference friend_player_died;             //snd!
    s_tag_reference friend_killed_by_friend;        //snd!
    s_tag_reference friend_killed_by_friend_player; //snd!
    s_tag_reference friend_killed_by_enemy;         //snd!
    s_tag_reference friend_killed_by_enemy_player;  //snd!
    s_tag_reference friend_killed_by_coveant;       //snd!
    s_tag_reference friend_killed_by_flood;         //snd!
    s_tag_reference friend_killed_by_sentinel;      //snd!
    s_tag_reference friend_betrayed;                //snd!
    PADDING(0x20);
    //shouting
    s_tag_reference new_combat_alone;               //snd!
    s_tag_reference new_enemy_recent_combat;        //snd!
    s_tag_reference old_enemy_sighted;              //snd!
    s_tag_reference unexpected_enemy;               //snd!
    s_tag_reference dead_friend_found;              //snd!
    s_tag_reference alliance_broken;                //snd!
    s_tag_reference alliance_reformed;              //snd!
    s_tag_reference grenade_throwing;               //snd!
    s_tag_reference grenade_sighted;                //snd!
    s_tag_reference grenade_startle;                //snd!
    s_tag_reference grenade_danager_enemy;          //snd!
    s_tag_reference grenade_danger_self;            //snd!
    s_tag_reference grenade_danger_friend;          //snd!
    PADDING(0x20);
    //group communcation
    s_tag_reference new_combat_group_re;            //snd!
    s_tag_reference new_combat_nearby_re;           //snd!
    s_tag_reference alert_friend;                   //snd!
    s_tag_reference alert_friend_re;                //snd!
    s_tag_reference alert_lost_contact;             //snd!
    s_tag_reference alert_lost_contact_re;          //snd!
    s_tag_reference blocked;                        //snd!
    s_tag_reference blocked_re;                     //snd!
    s_tag_reference search_start;                   //snd!
    s_tag_reference search_query;                   //snd!
    s_tag_reference search_query_re;                //snd!
    s_tag_reference search_report;                  //snd!
    s_tag_reference search_abandon;                 //snd!
    s_tag_reference search_group_abandon;           //snd!
    s_tag_reference group_uncover;                  //snd!
    s_tag_reference group_uncover_re;               //snd!
    s_tag_reference advance;                        //snd!
    s_tag_reference advance_re;                     //snd!
    s_tag_reference retreat;                        //snd!
    s_tag_reference retreat_re;                     //snd!
    s_tag_reference cover;                          //snd!
    PADDING(0x40);
    //actions
    s_tag_reference sighted_friend_player;          //snd!
    s_tag_reference shooting;                       //snd!
    s_tag_reference shooting_vehicle;               //snd!
    s_tag_reference shooting_berserk;               //snd!
    s_tag_reference shooting_group;                 //snd!
    s_tag_reference shooting_traitor;               //snd!
    s_tag_reference taunt;                          //snd!
    s_tag_reference taunt_re;                       //snd!
    s_tag_reference flee;                           //snd!
    s_tag_reference flee_re;                        //snd!
    s_tag_reference flee_leader_died;               //snd!
    s_tag_reference attempted_flee;                 //snd!
    s_tag_reference attempted_flee_re;              //snd!
    s_tag_reference lost_contact;                   //snd!
    s_tag_reference hiding_finished;                //snd!
    s_tag_reference vehicle_entry;                  //snd!
    s_tag_reference vehicle_exit;                   //snd!
    s_tag_reference vehicle_woohoo;                 //snd!
    s_tag_reference vehicle_scared;                 //snd!
    s_tag_reference vehicle_collision;              //snd!
    s_tag_reference partially_sighted;              //snd!
    s_tag_reference nothing_there;                  //snd!
    s_tag_reference pleading;                       //snd!
    PADDING(0x60);
    //exclamations
    s_tag_reference surprise;                       //snd!
    s_tag_reference berserk;                        //snd!
    s_tag_reference melee_attack;                   //snd!
    s_tag_reference dive;                           //snd!
    s_tag_reference uncover_exclamation;            //snd!
    s_tag_reference leap_attack;                    //snd!
    s_tag_reference resurrection;                   //snd!
    PADDING(0x40);
    //post-combat actions
    s_tag_reference celebration;                    //snd!
    s_tag_reference check_body_enemy;               //snd!
    s_tag_reference check_body_friend;              //snd!
    s_tag_reference shooting_dead_enemy;            //snd!
    s_tag_reference shooting_dead_enemy_friend;     //snd!
    PADDING(0x40);
    //post-combat chatter
    s_tag_reference alone;                          //snd!
    s_tag_reference unscathed;                      //snd!
    s_tag_reference seriously_wounded;              //snd!
    s_tag_reference seriously_wounded_re;           //snd!
    s_tag_reference massacre;                       //snd!
    s_tag_reference massacre_re;                    //snd!
    s_tag_reference rout;                           //snd!
    s_tag_reference rout_re;                        //snd!
    PADDING(0x330);
    //INFO: This area is where the location tag names are stored per used as above from saving to files.
} s_dialogue_meta;
static_assert_check(sizeof(s_dialogue_meta) == 0x1010, "Incorrect size of s_dialogue_meta");