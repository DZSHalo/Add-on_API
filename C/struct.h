/********************************************************************************
   This is far as we have gathered into one more reliability locations to keep
   maintaining the structures from Halo Trial through Custom Edition
   (Windows platform).


Credits:

    * Steve(Del / silentk?)
    * Abyll
    * flyingmonkey3
    * RadWolfie
    * Oxide
    * Wizard

    Your name isn't credited for finding specific offset purpose? Let us know
    and we will make sure to credit you! ;)

NOTICE:

    This file will be minimized in the future updates to remove duplicated
    structures which will be in different header file. Plus it will not be
    update unless it is necessary.

    P.S. Will not perform any more typedef in this file.

*********************************************************************************/
#ifndef structH
#define structH

#define playerindex char
#define machineindex char

//Team Color Begin
#ifdef __cplusplus
#pragma warning(push)
#pragma warning(disable:4341)
enum e_color_team_index : signed char {
    COLOR_TEAM_NONE = -1, //Reserved for H-Ext usage ONLY!
    COLOR_TEAM_RED = 0,
    COLOR_TEAM_BLUE = 1
};
#pragma warning(pop)
#else
typedef signed char e_color_team_index;
static const e_color_team_index COLOR_TEAM_NONE = -1; //Reserved for H-Ext usage ONLY!
static const e_color_team_index COLOR_TEAM_RED = 0;
static const e_color_team_index COLOR_TEAM_BLUE = 1;
#endif
//Team Color End

//Color Indexes Start
enum e_color_index {
    COLOR_WHITE     = 0,
    COLOR_BLACK,    //1,
    COLOR_RED,      //2
    COLOR_BLUE,     //3
    COLOR_GRAY,     //4
    COLOR_YELLOW,   //5
    COLOR_GREEN,    //6
    COLOR_PINK,     //7
    COLOR_PURPLE,   //8
    COLOR_CYAN,     //9
    COLOR_COBALT,   //10
    COLOR_ORANGE,   //11
    COLOR_TEAL,     //12
    COLOR_SAGE,     //13
    COLOR_BROWN,    //14
    COLOR_TAN,      //15
    COLOR_MAROON,   //16
    COLOR_SALMON    //17

};
//Color Indexes End

#pragma pack(push,1)
#define STR_CAT(a,b)            a##b
#define STR_CAT_DELAYED(a,b)   STR_CAT(a,b)
#define UNKNOWN(size) char STR_CAT_DELAYED(_unused_,__COUNTER__)[size]
#define UNKNOWN_BITFIELD(size) char STR_CAT_DELAYED(_unusedbf_, __COUNTER__) : size

typedef enum chatType {
    CHAT_GLOBAL = 0,
    CHAT_TEAM,
    CHAT_VEHICLE,
    CHAT_SERVER
} chatType;

typedef struct chatData {
    chatType        type;       //range of 0 - 3, sort from Global, Team, Vehicle, and Server (CE only)
    unsigned int    player;     //range of 0 - 15
    const wchar_t*  msg_ptr;    //range of 0 - TBA
} chatData;
typedef struct rconDecode {
    char pass[9];
    char cmd[65];
} rconDecode;
static_assert_check(sizeof(rconDecode) == 0x4A, "Incorrect size of rconDecode");

typedef struct bone {
    float unknown[10];
    real_vector3d World;
} bone;
static_assert_check(sizeof(bone) == 0x34, "Incorrect size of bone");
typedef struct s_player_reserved_slot {
    wchar_t PlayerName[12];     //0x00
    short ColorIndex;           //0x18      // See defined color indexes above.
    short Unknown1;             //0x1A      // 0xFFFF
    machineindex MachineIndex;  //0x1C      // Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
    char Unknown2;              //0x1D      //something. But, if these 4 chars are FF's, then the player isn't on.
    e_color_team_index Team;    //0x1E
    playerindex PlayerIndex;    //0x1F      // Index to their StaticPlayer
} s_player_reserved_slot;  //size of 0x20
static_assert_check(sizeof(s_player_reserved_slot) == 0x20, "Incorrect size of s_player_reserved_slot");

#ifndef s_addr
typedef struct in_addr {
    union {
        struct { unsigned char s_b1, s_b2, s_b3, s_b4; } S_un_b;
        struct { unsigned short s_w1, s_w2; } S_un_w;
        unsigned int S_addr;
    } S_un;
#define s_addr  S_un.S_addr         // can be used for most tcp & ip code
#define s_host  S_un.S_un_b.s_b2    // host on imp
#define s_net   S_un.S_un_b.s_b1    // network
#define s_imp   S_un.S_un_w.s_w2    // imp
#define s_impno S_un.S_un_b.s_b4    // imp #
#define s_lh    S_un.S_un_b.s_b3    // logical host
} IN_ADDR, *PIN_ADDR;
#endif

typedef struct MachineSP3 {
    IN_ADDR IPAddress;
    unsigned short port;
} MachineSP3; //At the moment do not know complete size...
typedef struct MachineSP2 {
    MachineSP3* data3;
} MachineSP2; //At the moment do not know complete size...
typedef struct MachineSP1 {
    MachineSP2* data2;
} MachineSP1; //At the moment do not know complete size...

typedef struct s_machine_slot {
    MachineSP1*     data1;                          //0x0000 // Following this, i found a pointer next to other useless stuff. Then, another pointer, then i found some stuff that looked like it /MIGHT/ very well be strings related to a hash. *shrug*
    int             Unknown0[2];                    //0x0004
    short           machineIndex;                   //0x000C
    short           Unknown9;                       //0x000E // First is 1, then 3, then 7 and back to 0 if not in used (1 is found during the CD Hash Check, 7 if currently playing the game)
    int             isAvailable;                    //0x0010
    int             Unknown10[2];                   //0x0014
    int             Unknown11;                      //0x001C    // most of the time 1, but sometimes changes to 2 for a moment.
    int             Unknown12;                      //0x0020
    // 16 bit bitfield for action keys:
    char            Crouch      :1;                 //0x0024
    char            Jump        :1;
    char            Flashlight  :1;
    char            Unknownbit0 :1;
    char            Unknownbit1 :1;
    char            Unknownbit2 :1;
    char            Unknownbit3 :1;
    char            Unknownbit4 :1;
    char            Reload      :1;                 //0x0025
    char            Fire        :1;
    char            Swap        :1;
    char            Grenade     :1;
    char            Unknownbit5 :1;
    char            Unknownbit6 :1;
    char            Unknownbit7 :1;
    char            Unknownbit8 :1;

    short           Unknown13;                      //0x0026
    float           Yaw;                            //0x0028 // player's rotation - in radians, from 0 to 2*pi, (AKA heading)
    float           Pitch;                          //0x002C // Player's pitch - in radians, from -pi/2 to +pi/2, down to up.
    float           Roll;                           //0x0030 // roll - unless walk-on-walls is enabled, this will always be 0.
    unsigned char   Unknown1[8];                    //0x0034
    float           ForwardVelocityMultiplier;      //0x003C
    float           HorizontalVelocityMultiplier;   //0x0040
    float           ROFVelocityMultiplier;          //0x0044
    short           WeaponIndex;                    //0x0048
    short           GrenadeIndex;                   //0x004A
    short           UnknownIndex;                   //0x004C // The index is -1 if no choices are even available.
    short           Unknown2;                       //0x004E
    short           Unknown3;                       //0x0050 // 1
    char            SessionKey[8];                  //0x0052 // This is used to accept the incoming player PLUS validate with gamespy server.
    short           Unknown4;                       //0x005A
    unsigned int    UniqueID;                       //0x005C // increase every time a player join (notice: it is not focus on specific machine struct, it applies to all.)
    //Below is used for Halo CE and Trial?, Halo PC doesn't have this extra data.
    //NOTICE: This below is disabled as it is no longer needed.
    //wchar_t         LastPlayersName[12];            //0x0060 // Odd.. this isnt the name of the player who's on, but i thinkn it's the Previous player's name.
    //int             Unknown6;                       //0x0078 // these two were -1.
    //int             Unknown7;                       //0x007C // but sometimes become 0.
    //char            IP[32];                         //0x0080
    //char            CDhash[32];                     // a solid block array, so it's not necessarily a c_str i think, but there's still usually just 0's afterwards anyways.
    //unsigned char   UnknownZeros[44];               // zeros..
} s_machine_slot; // Size: 0xEC = Halo CE & Trial, 0x60 = Halo PC
static_assert_check(sizeof(s_machine_slot) == 0x60, "Incorrect size of s_machine_slot");

typedef struct s_player_slot {//Verified offsets.
    short               PlayerID;                       //0x000
    short               IsLocal;                        //0x002            // 0=Local(no bits set), -1=Other Client(All bits set)
    wchar_t             Name[12];                       //0x004            // Unicode
    s_ident             UnknownIdent;                   //0x01C
    int                 Team;                           //0x020            // 0=Red, 1=Blue; if do client host, you will see hud's team changed instant.
    s_ident             SwapObject;                     //0x024
    short               SwapType;                       //0x028            // Vehicle=8, Weapon=6
    short               SwapSeat;                       //0x02A            // Warthog-Driver=0, Passenger=1, Gunner=2, Weapon=-1
    int                 RespawnTimer;                   //0x02C            // Counts down when dead, Alive=0
    int                 Unknown;                        //0x02F
    s_ident             CurrentBiped;                   //0x034
    s_ident             PreviousBiped;                  //0x038
    //IMPORTANT: need to verify this.
    //   uint LocationID;                   // This is very, very interesting. BG is split into 25 location ID's. 1 - 19
    short               LocationID;                     //0x03C            //Should be uint?
    //short    ClusterIndex;                //0x03C
    char                Swap            : 1;            //0x03E.0
    char                UnknownBits4    : 7;            //0x03E.1-7
    char                UnknownByte;                    //0x03F
    s_ident             UnknownIdent1;                  //0x040            //BulletCount?
    int                 LastBulletShotTime;             //0x044            // since game start(0)
    wchar_t             Name1[12];                      //0x048
    short               ColorIndex;                     //0x060            // See defined color indexes above.
    short               Unknown00;                      //0x062
    playerindex         MachineIndex;                   //0x064            // Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
    char                Unknown0;                       //0x065            //something. But, if these 4 chars are FF's, then the player isn't on.
    e_color_team_index  iTeam;                          //0x066
    playerindex         PlayerIndex;                    //0x067            // Index to their StaticPlayer
    int                 Unknown1;                       //0x068
    float               VelocityMultiplier;             //0x06C <--- and below are correct!
    s_ident             UnknownIdent3[4];               //0x070
    int                 Unknown2;                       //0x080
    int                 LastDeathTime;                  //0x084        // since game start(0)
    s_ident             killInOrderObjective;           //0x088
    char                Unknown3[16];                   //0x08C
    short               KillsCount;                     //0x09C
    char                Unknown4[6];                    //0x09E
    short               AssistsCount;                   //0x0A4
    char                Unknown5[6];                    //0x0A6
    short               BetrayedCount;                  //0x0AC
    short               DeathsCount;                    //0x0AE
    short               SuicideCount;                   //0x0B0
    char                Unknown6[18];                   //0x0B2
    short               FlagStealCount;                 //0x0C4
    short               FlagReturnCount;                //0x0C6
    short               FlagCaptureCount;               //0x0C8
    char                Unknown7[6];                    //0x0CA
    s_ident             UnknownIdent4;                  //0x0D0
    char                Unknown8;                       //0x0D4
    bool                HasQuit;                        //0x0D5
    char                Unknown81[6];                   //0x0D6
    short               Ping;                           //0x0DC
    char                Unknown9[14];                   //0x0DE
    s_ident             UnknownIdent5;                  //0x0EC
    int                 Unknown10;                      //0x0F0
    int                 SomeTime;                       //0x0F4
    real_vector3d       World;                          //0x0F8
    s_ident             UnknownIdent6;                  //0x104
    char                Unknown11[20];                  //0x108
    char                Melee       :   1;              //0x11C.0
    char                Action      :   1;              //0x11C.1
    char                UnknownBit  :   1;              //0x11C.2
    char                Flashlight  :   1;              //0x11C.3
    char                UnknownBit1 :   4;              //0x11C.4
    char                UnknownBit2 :   5;              //0x11D.0
    char                Reload      :   1;              //0x11D.5
    char                UnknownBit3 :   2;              //0x11D.6
    char                Unknown12[26];                  //0x11E
    real_vector2d       Rotation;                       //0x138        // Yaw, Pitch (again, in radians.
    float               ForwardVelocityMultiplier;      //0x140
    float               HorizontalVelocityMultiplier;   //0x144
    float               RateOfFireVelocityMultiplier;   //0x148
    short               HeldWeaponIndex;                //0x14C
    short               GrenadeIndex;                   //0x14E
    char                Unknown13[4];                   //0x150
    real_vector3d       LookVect;                       //0x154
    char                Unknown14[16];                  //0x160
    real_vector3d       WorldDelayed;                   //0x170    // Oddly enough... it matches the world vect, but seems to lag behind (Possibly what the client reports is _its_ world coord?)
    char                Unknown15[132];                 //0x17C
} s_player_slot;
static_assert_check(sizeof(s_player_slot) == 0x200, "Incorrect size of s_player_slot");

/*

*** Note: Vehicle settings ***
Using the following notation:
t turret
b banshee
r rocket hog
s scorpion
g ghost
h warthog
The bits are: 00000000 00tttbbb rrrsssgg ghhh1000 
where the final 1000 denotes a custom vehicle set. 
Then group it into bytes in reverse order:
ghhh1000  rrrsssgg  00tttbbb 00000000 
(reverse the bytes and not the bits) and plug it into 
hex 60-63 and 64-67 using the hex editor. The maximum 
number is 7 but only up to 4 is allowed and you only 
get one banshee no matter how many you want. All max 
is 48, 92, 24, 00 hex.

*/
#define GAMETYPE_CTF 1
#define GAMETYPE_SLAYER 2
#define GAMETYPE_ODDBALL 3
#define GAMETYPE_KOTH 4
#define GAMETYPE_RACE 5
typedef struct s_gametype {                        //UNDONE GameType Struct is not 100% decoded.
    wchar_t name[24];                           // 0x00
    unsigned int game_stage;                    // 0x30 1=CTF, 2=Slayer, 3=Oddball, 4=KOTH, 5=Race
    bool    isTeamPlay;                         // 0x34
    unsigned char NULL0[3];                     // 0x35
    bool    showEnemyRadar: 1;                  // 0x38.0
    bool    friendlyIndicator: 1;               // 0x38.1
    bool    infiniteGrenade: 1;                 // 0x38.2
    bool    noShield: 1;                        // 0x38.3
    bool    invisible: 1;                       // 0x38.4
    bool    isCustomEquipment: 1;               // 0x38.5
    bool    showFriendlyRadar: 1;               // 0x38.6
    bool    unknown: 1;                         // 0x38.7
    unsigned char Unknown0[3];                  // 0x39        //TODO need to find out what these are and 0x38 as well.
    unsigned int objective_indicator;           // 0x3C 0=Motion Tracker, 1=Navpoints, 2=None
    unsigned int is_odd_man_out;                // 0x40
    unsigned int respawn_time_growth;           // 0x44
    unsigned int respawn_time;                  // 0x48
    unsigned int respawn_suicide_penalty;       // 0x4C
    unsigned int limit_lives;                   // 0x50
    float health;                               // 0x54
    unsigned int score_limit;                   // 0x58
    unsigned int weapon_type;                   // 0x5C 0 = default, 1 = pistols, 2 = rifles, 3 = plasma rifles, 4 = sniper, 5 = no sniper, 6 = rocket launchers, 7 = shotguns, 8 = short range, 9 = human, 10 = covenant, 11 = classic, 12 = heavy weapons

    //Vehicles section
    unsigned int vehicle_red;                   // 0x60 Need some work here.
    unsigned int vehicle_blue;                  // 0x64 Need some work here.
    unsigned int vehicle_respawn_time;          // 0x68 ticks

    unsigned int is_friendly_fire;              // 0x6C
    unsigned int respawn_betrayal_penalty;      // 0x70
    unsigned int is_team_balance;               // 0x74
    unsigned int time_limit;                    // 0x78

    // ball gametype data
    unsigned int MovingHill;                    // 0x7C (KOTH) 0=off, 1=on; (Race) 0=Normal, 1=any order, 2=Rally; (Oddball) 0=off, 1=on;
    char TeamScoring;                           // 0x80 (Race) 0=minimal, 1=maximum, 2=Sum; (Oddball) 0=Slow, 1=Normal, 2=Fast
    short Unknown2;                             // 0x81
    char TraitBallWith;                         // 0x83 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
    unsigned int Unknown3;                      // 0x84
    unsigned int TraitBallWithout;              // 0x88 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
    unsigned char Unknown4[14];                 // 0x8C - 0x99 unknown
    bool    Unknown5;                           // 0x9A
    bool    Unknown6;                           // 0x9B
    bool    noDeathBonus;                       // 0x9C
    bool    noKillPenalty;                      // 0x9D
    bool    isKillInOrder_flagMustReset;        // 0x9E    isKillInOrder = Slayer, FlagMustReset = CTF    //CTF and Slayer is sharing this... must be union structure?
    bool    isFlagAtHomeToScore;                // 0x9F    CTF usage
    unsigned int SingleFlagTimer;               // 0xA0 CTF usage, 0 = off, 1800 = 1 min, and so on.
} s_gametype;
static_assert_check(sizeof(s_gametype) == 0xA4, "Incorrect size of s_gametype");


typedef struct GametypeGFlag {
    real_vector3d World;        //0x00 Coordinate of where to respawn at.
    UNKNOWN(8);                 //0x0C Nulls...
    UNKNOWN(4);                 //0x14 Don't know...
    float unknown;              //0x18 Possible float?
    UNKNOWN(8);                 //0x1C Nulls...
    unsigned int unknown2;      //0x24 Always -1
} GametypeGFlag;
static_assert_check(sizeof(GametypeGFlag) == 0x28, "Incorrect size of GametypeGFlag");

typedef struct GameTypeCTFg {               //size = 0x38
    GametypeGFlag* flagParams[2];   //0x0000        // 0 = Red flag, 1 = Blue flag
    s_ident teamFlagIds[2];         //0x0008
    unsigned int teamScore[2];      //0x0010        //0 = Red team, 1 = Blue team
    unsigned int scoreLimit;        //0x0018        //Not sure what size this is atm.
    bool    flagCaptured[2];           //0x001C        // 0 = Red flag, 1 = Blue flag
    unsigned short Unknown1;        //0x001E
    //Trial version end for CTF
    unsigned int flagNotAtBase[2];  //0x0020        //Timer in ticks; 0 = Red flag, 1 = Blue flag
    UNKNOWN(0x10);                  //0x0028        //Missing some other informations, need to edit more here.
} GameTypeCTFg;
typedef struct GameTypeKOTHg {                                  //size = 0x288
    unsigned int hillScoreTicks[16];                    //0x0000        //Scoring in ticks base on player id                                                //0x0038
    unsigned int hillScoreTicksRelativeMapTicks[16];    //0x0040        //Tick difference from how int in map ticks and is in hill base on player id.      //0x0078
    bool    isInHill[16];                                  //0x0080        //Player is in hill, base on player id                                              //0x00B8
    UNKNOWN(0x100);                                     //0x0090        //Don't know what this is and it's relative to hill being moved around if set.      //0x00C8
    unsigned int Unknown2;                              //0x0190        //Goes up when someone entered                                                      //0x01C8
    unsigned int Unknown3;                              //0x0194        //Tick goes up when someone is in the hill.                                         //0x01CC
    s_ident player;                                     //0x0198        //Shows the player id.                                                              //0x01D0
    UNKNOWN(0xEC);                                      //0x019C        //Dunno what the rest are.                                                          //0x01D4
} GameTypeKOTHg;
typedef struct GameTypeODDBALLg {               //size = 0x148
    unsigned int scoreLimit;            //0x0000        //Total ticks to win.                                                               //0x02C0
    unsigned int scoreTicks[16];        //0x0004                                                                                            //0x02C4
    unsigned int scoreTicks2[16];       //0x0044        //Always the same with above.                                                       //0x0304
    unsigned int Unknown4[16];          //0x0084        //Wizzard commented this is for juggernut...                                        //0x0344
    s_ident holder[16];                 //0x00C4        //This is base on oddball #, not player. Also using player id                       //0x0384
    unsigned int relocateTicks[16];     //0x0104        //This is base on oddball #, not player. Also using player id                       //0x03C4
    unsigned int Unknown5;              //0x0144        //Null                                                                              //0x0404
} GameTypeODDBALLg;
typedef struct GameTypeRACEg {                  //size = 0x148
    unsigned int checkpointTotal;       //0x0000        //Total navpoints around the map to score per lap.                                  //0x0408
    unsigned int Unknown6[16];          //0x0004        //Nulls                                                                             //0x040C
    unsigned int checkpointCurrent[16]; //0x0044        //Counting to checkpointTotal                                                       //0x044C
    unsigned int Unknown7;              //0x0084                                                                                            //0x048C
    unsigned int raceLaps[16];          //0x0088        //Total laps completed                                                              //0x0490
    unsigned int Unknown8[16];          //0x00C8        //So far just nulls...                                                              //0x04D0
    unsigned int Unknown9[16];          //0x0108        //Don't know what these are and not relative to players.                            //0x0510
} GameTypeRACEg;
typedef struct GameTypeSLAYERg {            //size = 0x80
    unsigned int playerScore[16];   //0x0000                                                                                            //0x0550
    unsigned int playerScore2[16];  //0x0040        //Duplicated and appear useless...                                                  //0x0590
} GameTypeSLAYERg;
typedef struct GameTypeGlobals {
    GameTypeCTFg        ctfGlobal;
    GameTypeKOTHg       kothGlobal;
    GameTypeODDBALLg    oddballGlobal;
    GameTypeRACEg       raceGlobal;
    GameTypeSLAYERg     slayerGlobal;
} GameTypeGlobals;
static_assert_check(sizeof(GameTypeGlobals) == 0x5D0, "Incorrect size of GameTypeGlobals");

typedef struct s_server_header {
    unsigned int*   Unknown0;           //0x000 // at least I _think_ it's a pointer since there _is_ something if i follow it.
    unsigned short  state;              //0x004
    unsigned short  Unknown2;           //0x006
    wchar_t         server_name[66];    //0x008
    char            map_name[128];      //0x08C
    wchar_t         gametype_name[24];  //0x10C
    //IMPORTANT: DO NOT USE! Below this does not match with other Halo PC platforms, it is base on Halo CE version.
    unsigned char   Unknown11[40];      //0x13C // partial of Gametype need to break them down.
    unsigned char   score_max;          //0x164
    unsigned char   Unknown3[128];      //0x165
    unsigned char   player_max;         //0x1E5    // Note: there is another place that also says MaxPlayers - i think it's the ServerInfo socket buffer.
    short           Unknown09;          //0x1E6
    unsigned short  totalPlayers;       //0x1E8
    short           Unknown10;          //0x1EA    // i think LastSlotFilled
} s_server_header;

#ifdef __cplusplus
typedef enum e_action_state : unsigned char {
    ACTION_IDLE = 0,
    ACTION_GESTURE,
    ACTION_TURN_LEFT,
    ACTION_TURN_RIGHT,
    ACTION_MOVE_FRONT,
    ACTION_MOVE_BACK,
    ACTION_MOVE_LEFT,
    ACTION_MOVE_RIGHT,
    ACTION_STUNNED_FRONT,
    ACTION_STUNNED_BACK,
    ACTION_STUNNED_LEFT,
    ACTION_STUNNED_RIGHT,
    ACTION_SLIDE_FRONT,
    ACTION_SLIDE_BACK,
    ACTION_SLIDE_LEFT,
    ACTION_SLIDE_RIGHT,
    ACTION_READY,
    ACTION_PUT_AWAY,
    ACTION_AIM_STILL,
    ACTION_AIM_MOVE,
    ACTION_AIRBORNE,
    ACTION_LAND_SOFT,
    ACTION_LAND_HARD,
    ACTION_UNKNOWN0,
    ACTION_AIRBORNE_DEAD,
    ACTION_LAND_DEAD,
    ACTION_SEAT_ENTER,
    ACTION_SEAT_EXIT,
    ACTION_CUSTOM_ANIMATION,
    ACTION_IMPULSE,
    ACTION_MELEE,
    ACTION_MELEE_AIRBORNE,
    ACTION_MELEE_CONTINUOUS,
    ACTION_GRENADE_TOSS,
    ACTION_RESURRECT_FRONT,
    ACTION_RESURRECT_BACK,
    ACTION_FEEDING,
    ACTION_SURPRISE_FRONT,
    ACTION_SURPRISE_BACK,
    ACTION_LEAP_START,
    ACTION_LEAP_AIRBORNE,
    ACTION_LEAP_MELEE,
    ACTION_UNUSED_AFAICT,
    ACTION_BERSERK
} e_action_state;
#else
#define e_action_state signed char
#define ACTION_IDLE                 0
#define ACTION_GESTURE              1
#define ACTION_TURN_LEFT            2
#define ACTION_TURN_RIGHT           3
#define ACTION_MOVE_FRONT           4
#define ACTION_MOVE_BACK            5
#define ACTION_MOVE_LEFT            6
#define ACTION_MOVE_RIGHT           7
#define ACTION_STUNNED_FRONT        8
#define ACTION_STUNNED_BACK         9
#define ACTION_STUNNED_LEFT         10
#define ACTION_STUNNED_RIGHT        11
#define ACTION_SLIDE_FRONT          12
#define ACTION_SLIDE_BACK           13
#define ACTION_SLIDE_LEFT           14
#define ACTION_SLIDE_RIGHT          15
#define ACTION_READY                16
#define ACTION_PUT_AWAY             17
#define ACTION_AIM_STILL            18
#define ACTION_AIM_MOVE             19
#define ACTION_AIRBORNE             20
#define ACTION_LAND_SOFT            21
#define ACTION_LAND_HARD            22
#define ACTION_UNKNOWN0             23
#define ACTION_AIRBORNE_DEAD        24
#define ACTION_LAND_DEAD            25
#define ACTION_SEAT_ENTER           26
#define ACTION_SEAT_EXIT            27
#define ACTION_CUSTOM_ANIMATION     28
#define ACTION_IMPULSE              29
#define ACTION_MELEE                30
#define ACTION_MELEE_AIRBORNE       31
#define ACTION_MELEE_CONTINUOUS     32
#define ACTION_GRENADE_TOSS         33
#define ACTION_RESURRECT_FRONT      34
#define ACTION_RESURRECT_BACK       35
#define ACTION_FEEDING              36
#define ACTION_SURPRISE_FRONT       37
#define ACTION_SURPRISE_BACK        38
#define ACTION_LEAP_START           39
#define ACTION_LEAP_AIRBORNE        40
#define ACTION_LEAP_MELEE           41
#define ACTION_UNUSED_AFAICT        42
#define ACTION_BERSERK              43
#endif

typedef struct damageFlags {
    bool    unknown1:2;            //0.0-0.1
    bool    kill1:1;               //0.2
    bool    unknown2:1;            //0.3
    bool    kill2:1;               //0.4
    bool    unknown3:3;            //0.5-0.9
    bool    unknown4:3;            //1.0-1.2
    bool    cannotTakeDamage:1;    //1.3
    bool    unknown5:4;            //1.4 - 1.9
} damageFlags;
static_assert_check(sizeof(damageFlags) == 0x02, "Incorrect size of damageFlags");

typedef struct s_object {
    s_ident         ModelTag;               // 0x0000
    int             Zero;                   // 0x0004
    char            Flags[4];               // 0x0008
    int             Timer;                  // 0x000C
    //char            Flags2[4];              // 0x0010
    char            unkBits:2;              // 0x0010
    bool            ignoreGravity:1;
    char            unk1:3;
    bool            unk2:1;
    bool            noCollision:1;
    char            unkBytes1[3];           // 0x0011
    int             Timer2;                 // 0x0014
    int             Zero2[17];              // 0x0018
    real_vector3d   World;                  // 0x005C
    real_vector3d   Velocity;               // 0x0068
    real_vector3d   Rotation;               // 0x0074
    real_vector3d   Scale;                  // 0x0080
    real_vector3d   VelocityPitchYawRoll;   // 0x008C  //current velocity for pitch, yaw, and roll
    int             LocationID;             // 0x0098
    int             Unknown1;               // 0x009C
    real_vector3d   UnknownVector2d;        // 0x00A0
    float           Unknown2[2];            // 0x00AC
    short           objType;                // 0x00B4
    short           Unknown3;               // 0x00B6
    short           GameObject;             // 0x00B8    // 0 >= is game object, -1 = is NOT game object
    short           Unknown4;               // 0x00BA
    int             Unknown5;               // 0x00BD
    s_ident         Player;                 // 0x00C0
    int             Unknown6[2];            // 0x00C4
    s_ident         AntrMeta;               // 0x00CC
    int             Unknown7[2];            // 0x00D0
    float           HealthMax;              // 0x00D8
    float           ShieldMax;              // 0x00DC
    float           Health;                 // 0x00E0
    float           Shield1;                // 0x00E4
    int             Unknown8[7];            // 0x00E8
    short           Unknown9;               // 0x0104
    damageFlags     damageFlag;             // 0x0106
    short           Unknown10;              // 0x0108
    short           Unknown11;              // 0x010A
    int             Unknown12[2];           // 0x010C
    s_ident         VehicleWeapon;          // 0x0114
    s_ident         Weapon;                 // 0x0118
    s_ident         Vehicle;                // 0x011C
    short           SeatType;               // 0x0120
    short           Unknown13;              // 0x0122
    int             Unknown14;              // 0x0124
    float           Shield2;                // 0x0128
    float           Flashlight1;            // 0x012C
    float           Unknown15;              // 0x0130
    float           Flashlight2;            // 0x0134
    int             Unknown16[5];           // 0x0138
    s_ident         UnknownIdent1;          // 0x014C
    s_ident         UnknownIdent2;          // 0x0150
    int             Zero3[6];               // 0x0154
    s_ident         UnknownIdent3;          // 0x016C
    s_ident         UnknownIdent4;          // 0x0170
    int             UnknownMatrix0[16];     //D3DXMATRIX UnknownMatrix;     // 0x0174
    int             UnknownMatrix1[16];     //D3DXMATRIX UnknownMatrix1;    // 0x01B4
    //Everything after this is 0x01F4
} s_object;
static_assert_check(sizeof(s_object) == 0x1F4, "Incorrect size of s_object");

typedef struct actionFlags {    // these are action flags, basically client button presses and these don't actually control whether or not an event occurs
    bool    crouching : 1;              // 0 (a few of these bit flags are thanks to halo devkit)
    bool    jumping : 1;                // 1
    char UnknownBit : 2;                // 2
    bool    Flashlight : 1;             // 4
    bool    UnknownBit2 : 1;            // 5
    bool    actionPress : 1;            // 6 think this is just when they initially press the action button
    bool    melee : 1;                  // 7
    char UnknownBit3 : 2;               // 8
    bool    reload : 1;                 // 10
    bool    primaryWeaponFire : 1;      // 11 right mouse
    bool    secondaryWeaponFire : 1;    // 12 left mouse
    bool    secondaryWeaponFire1 : 1;   // 13
    bool    actionHold : 1;             // 14 holding action button
    char UnknownBit4 : 1;               // 15
} actionFlags;
static_assert_check(sizeof(actionFlags) == 0x02, "Incorrect size of actionFlags");

typedef struct s_biped {
    s_object        sObject;                    // 0x0000
    int             Unknown[4];                 // 0x01F4
    short           isInvisible;                // 0x0204    normal = 0x41 invis = 0x51 (bitfield) Offset 0x422 is set zero for camo to start.
    char            Flashlight;                 // 0x0206
    char            Frozen;                     // 0x0207
    actionFlags     actionBits;                 // 0x0208 & 0x0209
    char            Unknown1[2];                // 0x020A
    int             UnknownCounter1;            // 0x020C
    int             UnknownLongs1[2];           // 0x0210
    s_ident         PlayerOwner;                // 0x0218
    int             UnknownLongs3[2];           // 0x021C
    real_vector3d   RightVect;                  // 0x0224
    real_vector3d   UpVect;                     // 0x0230
    real_vector3d   LookVect;                   // 0x023C
    real_vector3d   ZeroVect;                   // 0x0248
    real_vector3d   RealLookVect;               // 0x0254
    real_vector3d   UnknownVect3;               // 0x0260
    char            Unknown2[0x34];             // 0x026C
    unsigned char   actionVehicle_crouch_stand; // 0x02A0 Is this really true? Found this from Wizard's code (Standing = 4) (Crouching = 3) (Vehicle = 0)
    char            Unknown9[0x02];             // 0x02A1
    e_action_state  animation_state;            // 0x02A3
    char            Unknown91[0x4E];            // 0x02A4
    unsigned short  CurWeaponIndex0;            // 0x02F2    (Do not attempt to edit this, will crash Halo)
    unsigned short  CurWeaponIndex1;            // 0x02F4    (Read only)
    unsigned short  Unknown6;                   // 0x02F6
    s_ident         Weapons[4];                 // 0x02F8
    unsigned int    WeaponsLastUse[4];          // 0x0308
    int             UnknownLongs2;              // 0x031C <-- INCORRECT OFFSET?
    char            grenadeIndex;               // 0x031C <-- INCORRECT OFFSET?
    char            grenadeIndex1;              // 0x031D
    char            grenade0;                   // 0x031E
    char            grenade1;                   // 0x031F
    char            Zoom;                       // 0x0320
    char            Zoom1;                      // 0x0321
    char            Unknown3[2];                // 0x0322
    s_ident         SlaveController;            // 0x0324    Only effective for moving the Biped, sometimes does update the facing direction
    s_ident         WeaponController;           // 0x0328    Does update where to point, fire the weapon, and reload. Have not confirmed with other player's.
    char            Unknown4[464];              // 0x032C
    s_ident         bump_objectId;              // 0x04FC
    unsigned char   Unknown7;                   // 0x0500    Relative to swap biped, not sure what else uses this.
    unsigned short  inAirticks;                 // 0x0501    Amount of time in the air?
    unsigned char   isWalking;                  // 0x0503    0 = else, 1 = While on ground & is walking, 2 = rarely seen + seems to be using bit values
    char            Unknown8[76];               // 0x0504    0x422 (unsigned short) is set zero for camo to start.
    bone            LeftThigh;                  // 0x0550
    bone            RightThigh;                 // 0x0584
    bone            Pelvis;                     // 0x05B8
    bone            LeftCalf;                   // 0x05EC
    bone            RightCalf;                  // 0x0620
    bone            Spine;                      // 0x0654
    bone            LeftClavicle;               // 0x0688
    bone            LeftFoot;                   // 0x06BC
    bone            Neck;                       // 0x06F0
    bone            RightClavicle;              // 0x0724
    bone            RightFoot;                  // 0x0758
    bone            Head;                       // 0x078C
    bone            LeftUpperArm;               // 0x07C0
    bone            RightUpperArm;              // 0x07F4
    bone            LeftLowerArm;               // 0x0828
    bone            RightLowerArm;              // 0x085C
    bone            LeftHand;                   // 0x0890
    bone            RightHand;                  // 0x08C4
    char            Unknown5[1216];             // 0x08F8 //Missing 0x092C?
} s_biped; // Size - 3564(0x0DEC) bytes

//Major WIP Halo Structure Begin
typedef struct s_weapon {
    s_object        sObject;
    char            Unknown[12];                        //0x01F4
    s_ident         UnknownIdent;                       //0x0200  //Relative to assigne biped being dropped from.
    unsigned int    NetworkTime;                        //0x0204
    char            Unknown1[36];                       //0x0208

    bool            Unknown16:4;                        //0x022C.0-3
    bool            Unknown17:1;                        //0x022C.4
    bool            isPickedup:1;                       //0x022C.5
    bool            isNotReturned:1;                    //0x022C.6
    bool            Unknown18:1;                        //0x022C.7
    char            Unknown19[3];                       //0x022D
    bool            Unknown20:1;                        //0x0230.0
    bool            Melee:1;                            //0x0230.1
    bool            Unknown21:2;                        //0x0230.2-3
    bool            Unknown22:4;                        //0x0230.4-7
    char            Unknown23[3];                       //0x0231

    unsigned int    Unknown24;                          //0x0234

    bool            IsFiring;                           //0x0238
    char            Unknown3;                           //0x0239
    unsigned short  WeaponReadyWaitTime;                //0x023A
    char            Unknown4[4];                        //0x023C
    float           ammoBattery;                        //0x0240
    char            Unknown30[28];                      //0x023C
    unsigned int    SomeCounter;                        //0x0260
    unsigned int    IsNotFiring;                        //0x0264
    unsigned int    Unknown5[5];                        //0x0268
    float           Unknown6;                           //0x027C
    unsigned int    Unknown7;                           //0x0280
    float           Unknown8[2];                        //0x0284
    s_ident         UnknownIdent1;                      //0x028C
    unsigned int    AutoReloadCounter;                  //0x0290
    unsigned char   Unknown9[28];                       //0x0294
    unsigned short  ReloadFlags;                        //0x02B0 // 0=NotReloading,1=Reloading, 2=???, 3=???  //is correct
    unsigned short  ReloadCountdown;                    //0x02B2    //can set to 0 to finish reload countdown
    unsigned short  Unknown10;                          //0x02B4
    unsigned short  BulletCountInRemainingClips;        //0x02B6
    unsigned short  BulletCountInCurrentClip;           //0x02B8
    char            Unknown11[18];                      //0x02BA
    s_ident         UnknownIdent2;                      //0x02CC
    unsigned int    LastBulletFiredTime;                //0x02DO
    char            Unknown12[16];                      //0x02D4
    real_vector3d   Unknown13[2];                       //0x02E4
    char            Unknown14[12];
    unsigned int    BulletCountInRemainingClips1;
    char            Unknown15[52];
} s_weapon; // Size - 1644(0x066C)

typedef struct s_vehicle {
    s_ident         ModelTag;               // 0x0000
    int             Zero;                   // 0x0004
    char            Flags[4];               // 0x0008
    int             Timer;                  // 0x000C
    char            Flags2[4];              // 0x0010
    int             Timer2;                 // 0x0014
    int             Zero2[17];              // 0x0018
    real_vector3d   World;                  // 0x005C
    real_vector3d   Velocity;               // 0x0068
    real_vector3d   LowerRot;               // 0x0074  //incorrect
    real_vector3d   Rotation;               // 0x0080  //incorrect
    real_vector3d   UnknownVect1;           // 0x008C
    int             LocationID;             // 0x0098
    int             UnknownO1;              // 0x009C
    real_vector3d   Unknownreal_vector2d;   // 0x00A0
    float           UnknownO2[2];           // 0x00AC
    int             UnknownO3[3];           // 0x00B4
    s_ident         Player;                 // 0x00C0
    int             UnknownO4[2];           // 0x00C4
    s_ident         AntrMeta;               // 0x00CC
    int             UnknownO5[4];           // 0x00D0
    float           Health;                 // 0x00E0
    float           Shield1;                // 0x00E4
    int             UnknownO6[11];          // 0x00E8
    s_ident         VehicleWeapon;          // 0x0114
    s_ident         CurrentFirstPersonOn;   // 0x0118
    s_ident         Vehicle;                // 0x011C
    short           SeatType;               // 0x0120
    short           UnknownO7;              // 0x0122
    int             UnknownO8;              // 0x0124
    float           Shield2;                // 0x0128
    float           Headlight1;             // 0x012C
    float           Unknown9;               // 0x0130
    float           Headlight2;             // 0x0134
    int             UnknownO10[5];          // 0x0138
    s_ident         UnknownIdent1;          // 0x014C
    s_ident         UnknownIdent2;          // 0x0150
    int             Zero3[6];               // 0x0154
    s_ident         UnknownIdent3;          // 0x016C
    s_ident         UnknownIdent4;          // 0x0170
    real_vector3d   UnknownMatrix;          // 0x0174
    real_vector3d   UnknownMatrix1;         // 0x0180
//** END OBJECT part
    char            UnknownVeh0[0x7A];      // 0x018C
    bool            isNotAllowPlayerEntry;  // 0x206
    char            UnknownVeh1[0x11D];     // 0x18C
    s_ident         SlaveController;        // 0x324
    int             UnknownInt[0xA1];       // 0x328
    int             LastInUsedTick;         // 0x5AC
    //Anything goes after this?
} s_vehicle; // Size - 3580(0xDFC)
//Major WIP Halo Structure End

//TODO: Variable of offset seems to have some sort of data usage base from SDMHaloMapLoader.c/h Need to do some research.
typedef struct s_map_header {
    unsigned int    head;           //0x00 //enum 'head' string type
    unsigned int    haloVersion;    //0x04
    unsigned int    length;         //0x08
    PADDING(4);                     //0x0C //Nulls
    unsigned int    index_offset;   //0x10
    unsigned int    metadata_size;  //0x14
    PADDING(8);                     //0x18 //Nulls
    char            name[32];       //0x20
    char            build[32];      //0x40
    unsigned int    type;           //0x060 // 0 = Campaign, 1 = Multi-player, 2 = Menu
    unsigned int    unknown07;      //0x064
} s_map_header;
static_assert_check(sizeof(s_map_header) == 0x68, "Incorrect size of s_map_header");

typedef struct s_map_status {
    bool            Unknown1;       //0x00
    bool            Unknown2;       //0x01
    unsigned short  Unknown3;       //0x02 NULLs
    unsigned int    Unknown4;       //0x04
    unsigned int    Unknown5;       //0x08
    unsigned int    upTime;         //0x0C 1 sec = 30 ticks <-- use this as recommended upTime
    unsigned int    Unknown6;       //0x10
    unsigned int    upTime1;        //0x14 1 sec = 30 ticks
    float           Unknown7;       //0x18
    unsigned int    Unknown8;       //0x1C Don't know what this is and it's increasing rapidly...
} s_map_status;
static_assert_check(sizeof(s_map_status) == 0x20, "Incorrect size of s_map_status");

typedef struct s_console_header {
    bool            gamePause;          //0x00
    bool            allowConsole;       //0x01
    unsigned short  unknown01;          //0x02 //Nulls
    bool            isNotConsole;       //0x04
    unsigned char   unknown02;          //0x05
    unsigned char   unknown03;          //0x06
    unsigned char   keyPress;           //0x07
    unsigned short  unknown04;          //0x08
    unsigned short  unknown05;          //0x0A
    unsigned short  unknown06;          //0x0C
    unsigned short  unknown07[61];      //0x10 //Nulls
    unsigned int    unknown08;          //0x88
    unsigned int    unknown09;          //0x8C
    unsigned int    unknown10;          //0x90
    unsigned int    unknown11;          //0x94
    char            inputName[32];      //0x98
    char            input[255];         //0xB8
} s_console_header;
static_assert_check(sizeof(s_console_header) == 0x1B7, "Incorrect size of s_console_header");

typedef struct s_ban_check {
    wchar_t password[9];                //0x00
    char    cdKeyHash[32];              //0x12
    char    unknown0[40];               //0x32
    char    unknown1[4];                //0x5A
    wchar_t requestPlayerName[12];      //0x5E
} s_ban_check; // Size - 100 (0x64) bytes
static_assert_check(sizeof(s_ban_check) == 0x76, "Incorrect size of s_ban_check");

//Extras for Add-on API usage.

typedef struct s_cheat_header {
    bool    deathlessPlayer;       //0x00
    bool    jetPack;               //0x01
    bool    infiniteAmmo;          //0x02
    bool    bumpPossession;        //0x03
    bool    superJump;             //0x04
    bool    reflexDamage;          //0x05
    bool    medUsa;                //0x06
    bool    omnipotent;            //0x07
    bool    controller;            //0x08
    bool    bottomlessClip;        //0x09
} s_cheat_header;
static_assert_check(sizeof(s_cheat_header) == 0xA, "Incorrect size of s_cheat_header");

typedef struct D3DCOLOR_COLORVALUE_ARGB {
    float a;
    float r;
    float g;
    float b;
#ifdef __cplusplus
    D3DCOLOR_COLORVALUE_ARGB() {
        a=1.0f;
        r=1.0f;
        g=1.0f;
        b=1.0f;
    }
#endif
} D3DCOLOR_COLORVALUE_ARGB;
typedef struct s_console_color_list {
    D3DCOLOR_COLORVALUE_ARGB* x0_Black;         //0x00
    D3DCOLOR_COLORVALUE_ARGB* x1_DodgerBlue;    //0x04
    D3DCOLOR_COLORVALUE_ARGB* x2_Cyan;          //0x08
    D3DCOLOR_COLORVALUE_ARGB* x3_White;         //0x0C
    D3DCOLOR_COLORVALUE_ARGB* x4_Yellow;        //0x10
    D3DCOLOR_COLORVALUE_ARGB* x5_Blue;          //0x14
    D3DCOLOR_COLORVALUE_ARGB* x6_Coral;         //0x18    //Light Orange
    D3DCOLOR_COLORVALUE_ARGB* x7_Aquamarine;    //0x1C
    D3DCOLOR_COLORVALUE_ARGB* x8_Purple;        //0x20
    D3DCOLOR_COLORVALUE_ARGB* x9_DarkGreen;     //0x24
    D3DCOLOR_COLORVALUE_ARGB* x10_Red;          //0x28
    D3DCOLOR_COLORVALUE_ARGB* x11_Indigo;       //0x2C    //Dark Purple
    D3DCOLOR_COLORVALUE_ARGB* x12_Orange;       //0x30
    D3DCOLOR_COLORVALUE_ARGB* x13_Gray;         //0x34

} s_console_color_list;
static_assert_check(sizeof(s_console_color_list) == 0x38, "Incorrect size of s_console_color_list");

typedef struct GlobalVars {
    unsigned int ptrAcceptableASCIICharsArray;      //Unknown if this is correct.
    unsigned int ptrExceptionHandlerFunc;           //Halo CE 0x006207AC
    s_console_color_list s_console_color_list;      //Halo CE 0x006207B0 - 0x006207E4
    unsigned int ptrStrAsleep;                      //Halo CE 0x006207E8
    unsigned int ptrStrAlert;                       //Halo CE 0x006207EC
    unsigned int ptrStrCombat;                      //Halo CE 0x006207F0
    unsigned int UnknownNull;
    unsigned int ptrStrFlood_Carrier;               //Halo CE 0x006207F8,  A pointer plus seems to be a structure for it.
    unsigned int ptrFlood_Carrier_Parms0[4];        //Part 1 - Parameters?
    unsigned int ptrFlood_CarrierFunc;              //Part 2 - Is a pointer for a Flood Carrier Func?
    unsigned int ptrFlood_Carrier_Parms1[4];        //Part 3 - Parameters?
    //Etc, etc, etc. Don't have time to figure out the rest.
} GlobalVars;

typedef struct SoundPlay {
    short inUsed;       //0x00 //If is -1, then not in used. If is 0 or above, then in used.
    short Priority;     //0x02 //0 = general action, 1 = Unknown, 2 = UI sounds, 3 = possible loop or background sounds.
} SoundPlay;
typedef struct SoundVars {
    bool soundEnabled;              //0x00    //Not entirely sure...
    bool globalSoundOn;             //0x01
    bool muteSoundAsNotInWindow;    //0x02
    bool UNKNOWN0;                  //0x03
    unsigned int UNKNOWN1;          //0x04    Full of nulls
    unsigned int UNKNOWNPTR0;       //0x08    Some kind of pointer to a struct?
    unsigned int time;              //0x0C    counting up
    float UNKNOWN2;                 //0x10    some kind of sound volume? (Constantly changing)
    unsigned int UNKNOWN3;          //0x14    Unknown 1/0 value, constantly changing.
    unsigned int UNKNOWN4;          //0x18    Always 1?
    float UNKNOWN5;                 //0x1C    Always 1?
    real_vector3d Vector0;          //0x20
    real_vector3d Vector1;          //0x2C
    real_vector3d Vector2;          //0x38
    real_vector3d World;            //0x44
    real_vector3d UNKNOWN6;         //0x50    0 and 0x80000000 per float (not sure why)
    real_vector3d Vector4[6];       //0x5C
    float vol_slider;               //0xA4
    float vol_Music;                //0xA8
    float vol_Master;               //0xAC
    float vol_Effects;              //0xB0
    unsigned int Max_Channels;      //0xB4
    unsigned int UNKNOWN7;          //0xB8
    unsigned int UNKNOWN8;          //0xBC    Null
    unsigned int soundHeader;       //0xC0
    unsigned char UNKNOWN9[0x1C];   //0xC4    //Full of nulls
    short UNKNOWN10;                //0xE0
    short UNKNOWN11;                //0xE2
    SoundPlay sPlay[38];            //0xE4
} SoundVars;
static_assert_check(sizeof(SoundVars) == 0x17C, "Incorrect size of SoundVars");

typedef struct validationCheck {
    unsigned int UniqueID;
    unsigned int isValid;
    char *message;
} validationCheck;
static_assert_check(sizeof(validationCheck) == 0xC, "Incorrect size of validationCheck");
#pragma pack(pop)

#endif
