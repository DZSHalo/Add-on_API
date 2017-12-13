module D.structs;

import Add_on_API;
import std.bitmanip;

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

*********************************************************************************/

alias playerindex = ubyte;
alias machineindex = ubyte;

//Team Color Begin
enum e_color_team_index:ubyte {
    NONE = cast(ubyte)-1,
    RED = 0,
    BLUE = 1
}
//Team Color End

//Color Indexes Start
enum e_color_index {
    WHITE = 0,  //0
    BLACK,      //1
    RED,        //2
    BLUE,       //3
    GRAY,       //4
    YELLOW,     //5
    GREEN,      //6
    PINK,       //7
    PURPLE,     //8
    CYAN,       //9
    COBALT,     //10
    ORANGE,     //11
    TEAL,       //12
    SAGE,       //13
    BROWN,      //14
    TAN,        //15
    MAROON,     //16
    SALMON      //17
}
//Color Indexes End
static assert (e_color_index.SALMON==17, "Incorrect size of COLOR_INDEX!");

enum chatType {
    GLOBAL = 0,
    TEAM,
    VEHICLE,
    SERVER
}

struct chatData {
    chatType        type;       //range of 0 - 3, sort from Global, Team, Vehicle, and Server (CE only)
    uint            player;     //range of 0 - 15
    const (wchar)*  msg_ptr;    //range of 0 - TBA
}
static assert(chatData.sizeof == 0xC, "Incorrect size of chatData");

struct rconDecode {
    char[9] pass;
    char[65] cmd;
}
static assert(rconDecode.sizeof == 0x4A, "Incorrect size of rconDecode");

struct bone {
    float[10] unknown;
    real_vector3d World;
}
static assert(bone.sizeof == 0x34, "Incorrect size of bone");

struct s_player_reserved_slot {
    align(1):
    wchar[12] PlayerName;       //0x00
    short ColorIndex;           //0x18        // See defined color indexes above.
    short Unknown1;             //0x1A        // FFFF
    machineindex MachineIndex;  //0x1C        // Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
    char Unknown2;              //0x1D        //something. But, if these 4 chars are FF's, then the player isn't on.
    e_color_team_index Team;                  //0x1E
    playerindex PlayerIndex;    //0x1F    // Index to their StaticPlayer
}
static assert(s_player_reserved_slot.sizeof == 0x20, "Incorrect size of s_player_reserved_slot");

static if (!__traits(compiles, s_addr)) {
    struct in_addr {
        union S_un {
            struct S_un_b { ubyte s_b1, s_b2, s_b3, s_b4; };
            struct S_un_w { ushort s_w1, s_w2; };
            uint S_addr;
        };
        alias s_addr  = S_un.S_addr; /* can be used for most tcp & ip code */
        alias s_host  = S_un.S_un_b.s_b2;   // host on imp
        alias s_net   = S_un.S_un_b.s_b1;   // network
        alias s_imp   = S_un.S_un_w.s_w2;   // imp
        alias s_impno = S_un.S_un_b.s_b4;   // imp #
        alias s_lh    = S_un.S_un_b.s_b3;   // logical host

    }
    alias IN_ADDR = in_addr;
    //alias PIN_ADDR = *in_addr;
}

struct MachineSP3 {
    in_addr IPAddress;
    ushort port;
} //At the moment do not know complete size...
struct MachineSP2 {
    MachineSP3* data3;
} //At the moment do not know complete size...
struct MachineSP1 {
    MachineSP2* data2;
} //At the moment do not know complete size...



struct s_machine_slot {
    align(1):
    MachineSP1* data1;                          //0x0000 // Following this, i found a pointer next to other useless stuff. Then, another pointer, then i found some stuff that looked like it /MIGHT/ very well be strings related to a hash. *shrug*
    uint[2]     Unknown0;                       //0x0004
    short       machineIndex;                   //0x000C
    short       Unknown9;                       //0x000E // First is 1, then 3, then 7 and back to 0 if not in used (1 is found during the CD Hash Check, 7 if currently playing the game)
    uint        isAvailable;                    //0x0010
    uint[2]     Unknown10;                      //0x0014
    uint        Unknown11;                      //0x001C    // most of the time 1, but sometimes changes to 2 for a moment.
    uint        Unknown12;                      //0x0020
    // 16 bit bitfield for action keys:
    char        bitFieldFlag1;
    /*char    Crouch    : 1;                    //0x0024
    char    Jump    : 1;
    char    Flashlight : 1;
    char    Unknownbit0 :1;
    char    Unknownbit1 :1;
    char    Unknownbit2 :1;
    char    Unknownbit3 :1;
    char    Unknownbit4 :1;*/
    char        bitFieldFlag2;
    /*char    Reload    : 1;                    //0x0025
    char    Fire    : 1;
    char    Swap    : 1;
    char    Grenade    : 1;
    char    Unknownbit5 :1;
    char    Unknownbit6 :1;
    char    Unknownbit7 :1;
    char    Unknownbit8 :1;*/

    short       Unknown13;                      //0x0026
    float       Yaw;                            //0x0028 // player's rotation - in radians, from 0 to 2*pi, (AKA heading)
    float       Pitch;                          //0x002C // Player's pitch - in radians, from -pi/2 to +pi/2, down to up. 
    float       Roll;                           //0x0030 // roll - unless walk-on-walls is enabled, this will always be 0.
    ubyte[4]    Unknown1;                       //0x0034
    uint        Unknown16;                      //0x0038
    float       ForwardVelocityMultiplier;      //0x003C
    float       HorizontalVelocityMultiplier;   //0x0040
    float       ROFVelocityMultiplier;          //0x0044
    short       WeaponIndex;                    //0x0048
    short       GrenadeIndex;                   //0x004A
    short       UnknownIndex;                   //0x004C // The index is -1 if no choices are even available.
    short       Unknown2;                       //0x004E
    short       Unknown3;                       //0x0050 // 1
    char[8]     SessionKey;                     //0x0052 // This is used to accept the incoming player PLUS validate with gamespy server to assuming accept the incoming player.
    short       Unknown4;                       //0x005A
    uint        UniqueID;                       //0x005C // increase every time a player join (notice: it is not focus on specific machine struct, it applies to all.)
    //Below is used for Halo CE and Trial?, Halo PC doesn't have this extra data.
    //NOTICE: This below is disabled as it is no uinter needed.
    //wchar_t[12]    LastPlayersName;            //0x0060 // Odd.. this isnt the name of the player who's on, but i thinkn it's the Previous player's name.
    //uint    Unknown6;                        //0x0078 // these two were -1.
    //uint    Unknown7;                        //0x007C // but sometimes become 0.
    //char[32]    IP;                            //0x0080
    //char[32]    CDhash;            // a solid block array, so it's not necessarily a c_str i think, but there's still usually just 0's afterwards anyways.
    //ubyte[44]    UnknownZeros;    // zeros..
} // Size: 0xEC = Halo CE & Trial, 0x60 = Halo PC
static assert(s_machine_slot.sizeof == 0x60, "Incorrect size of s_machine_slot");

struct s_player_slot {//Verified offsets.
    align(1):
    short           PlayerID;                       //0x000
    short           IsLocal;                        //0x002            // 0=Local(no bits set), -1=Other Client(All bits set)
    wchar[12]       Name;                           //0x004            // Unicode
    s_ident         UnknownIdent;                   //0x01C
    uint            Team;                           //0x020            // 0=Red, 1=Blue; if do client host, you will see hud's team changed instant.
    s_ident         SwapObject;                     //0x024
    short           SwapType;                       //0x028            // Vehicle=8, Weapon=6
    short           SwapSeat;                       //0x02A            // Warthog-Driver=0, Passenger=1, Gunner=2, Weapon=-1
    uint            RespawnTimer;                   //0x02C            // Counts down when dead, Alive=0
    uint            Unknown;                        //0x02F
    s_ident         CurrentBiped;                   //0x034
    s_ident         PreviousBiped;                  //0x038
    //IMPORTANT: need to verify this.
    //   uuint LocationID;                  // This is very, very interesting. BG is split into 25 location ID's. 1 - 19
    short           LocationID;                     //0x03C            //Should be uuint?
    //short           ClusterIndex;                 //0x03C
    ubyte           bitFieldFlag1;
    /*char    Swap : 1;                       //0x03E.0
    char    UnknownBits4 :7;                //0x03E.7*/
    char            UnknownByte;                    //0x03F
    s_ident         UnknownIdent1;                  //0x040            //BulletCount?
    uint            LastBulletShotTime;             //0x044            // since game start(0)
    wchar[12]       Name1;                          //0x048
    short           ColorIndex;                     //0x060            // See defined color indexes above.
    short           Unknown00;                      //0x062
    playerindex     MachineIndex;                   //0x064            // Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
    char            Unknown0;                       //0x065            //something. But, if these 4 chars are FF's, then the player isn't on.
    char            iTeam;                          //0x066
    playerindex     PlayerIndex;                    //0x067            // Index to their StaticPlayer
    uint            Unknown1;                       //0x068
    float           VelocityMultiplier;             //0x06C <--- and below are correct!
    s_ident[4]      UnknownIdent3;                  //0x070
    uint            Unknown2;                       //0x080
    uint            LastDeathTime;                  //0x084        // since game start(0)
    s_ident         killInOrderObjective;           //0x088
    char[16]        Unknown3;                       //0x08C
    short           KillsCount;                     //0x09C
    char[6]         Unknown4;                       //0x09E
    short           AssistsCount;                   //0x0A4
    char[6]         Unknown5;                       //0x0A6
    short           BetrayedCount;                  //0x0AC
    short           DeathsCount;                    //0x0AE
    short           SuicideCount;                   //0x0B0
    char[18]        Unknown6;                       //0x0B2
    short           FlagStealCount;                 //0x0C4
    short           FlagReturnCount;                //0x0C6
    short           FlagCaptureCount;               //0x0C8
    char[6]         Unknown7;                       //0x0CA
    s_ident         UnknownIdent4;                  //0x0D0
    char            Unknown8;                       //0x0D4
    bool            HasQuit;                        //0x0D5
    char[6]         Unknown81;                      //0x0D6
    short           Ping;                           //0x0DC
    char[14]        Unknown9;                       //0x0DE
    s_ident         UnknownIdent5;                  //0x0EC
    uint            Unknown10;                      //0x0F0
    uint            SomeTime;                       //0x0F4
    real_vector3d   World;                          //0x0F8
    s_ident         UnknownIdent6;                  //0x104
    char[20]        Unknown11;                      //0x108
    ubyte[2]        bitFieldFlag2;
    /*char    Melee        :    1;          //0x11C.0
    char    Action        :    1;           //0x11C.1
    char    UnknownBit    :   1;            //0x11C.2
    char    Flashlight    :    1;           //0x11C.3
    char    UnknownBit1    :    4;          //0x11C.4
    char    UnknownBit2    :    5;          //0x11D.0
    char    Reload        :    1;           //0x11D.5
    char    UnknownBit3    :    2;          //0x11D.6*/
    char[26]        Unknown12;                      //0x11E
    real_vector2d   Rotation;                       //0x138        // Yaw, Pitch (again, in radians.
    float           ForwardVelocityMultiplier;      //0x140
    float           HorizontalVelocityMultiplier;   //0x144
    float           RateOfFireVelocityMultiplier;   //0x148
    short           HeldWeaponIndex;                //0x14C
    short           GrenadeIndex;                   //0x14E
    char[4]         Unknown13;                      //0x150
    real_vector3d   LookVect;                       //0x154
    char[16]        Unknown14;                      //0x160
    real_vector3d   WorldDelayed;                   //0x170    // Oddly enough... it matches the world vect, but seems to lag behind (Possibly what the client reports is _its_ world coord?)
    char[132]       Unknown15;                      //0x17C
}
//pragma(msg, PlayerS.Unknown15.offsetof);
static assert(s_player_slot.sizeof == 0x200, "Incorrect size of s_player_slot ");

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

enum GAMETYPE_FLAG {
    CTF = 1,
    SLAYER,
    ODDBALL,
    KOTH,
    RACE
}

struct s_gametype {                     //UNDONE GameType Struct is not 100% decoded.
    align(1):
    wchar[24] name;                     // 0x00
    uint game_stage;                    // 0x30 1=CTF, 2=Slayer, 3=Oddball, 4=KOTH, 5=Race
    bool is_team_play;                  // 0x34
    ubyte[3] NULL0;                     // 0x35
    ubyte bitfieldFlag1;
    /*bool showEnemyRadar: 1;           // 0x38.0
    bool friendlyIndicator: 1;          // 0x38.1
    bool infiniteGrenade: 1;            // 0x38.2
    bool noShield: 1;                   // 0x38.3
    bool invisible: 1;                  // 0x38.4
    bool isCustomEquipment: 1;          // 0x38.5
    bool showFriendlyRadar: 1;          // 0x38.6
    bool unknown: 1;                    // 0x38.7*/
    ubyte[3] Unknown0;                  // 0x39        //TODO need to find out what these are and 0x38 as well.
    uint objective_indicator;           // 0x3C 0=Motion Tracker, 1=Navpoints, 2=None
    uint is_odd_man_out;                // 0x40
    uint respawn_time_growth;           // 0x44
    uint respawn_time;                  // 0x48
    uint respawn_suicide_penalty;       // 0x4C
    uint limit_lives;                   // 0x50
    float health;                       // 0x54
    uint score_limit;                   // 0x58
    uint weapon_type;                   // 0x5C  0 = default, 1 = pistols, 2 = rifles, 3 = plasma rifles, 4 = sniper, 5 = no sniper, 6 = rocket launchers, 7 = shotguns, 8 = short range, 9 = human, 10 = covenant, 11 = classic, 12 = heavy weapons

    //Vehicles section
    uint vehicle_red;                   // 0x60 Need some work here.
    uint vehicle_blue;                  // 0x64 Need some work here.
    uint vehicle_respawn_time;          // 0x68

    uint is_friendly_fire;              // 0x6C
    uint respawn_betrayal_penalty;      // 0x70 Need to verify if respawn time or a cool down.
    uint is_team_balance;               // 0x74
    uint time_limit;                    // 0x78

    // ball gametype data
    uint MovingHill;                    // 0x7C (KOTH) 0=off, 1=on; (Race) 0=Normal, 1=any order, 2=Rally; (Oddball) 0=off, 1=on;
    char TeamScoring;                   // 0x80 (Race) 0=minimal, 1=maximum, 2=Sum; (Oddball) 0=Slow, 1=Normal, 2=Fast
    short Unknown2;                     // 0x81
    char TraitBallWith;                 // 0x83 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
    uint Unknown3;                      // 0x84
    uint TraitBallWithout;              // 0x88 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
    ubyte[14] Unknown4;                 // 0x8C - 0x99 unknown
    bool Unknown5;                      // 0x9A
    bool Unknown6;                      // 0x9B
    bool noDeathBonus;                  // 0x9C
    bool noKillPenalty;                 // 0x9D
    bool isKillInOrder_flagMustReset;   // 0x9E    isKillInOrder = Slayer, FlagMustReset = CTF    //CTF and Slayer is sharing this... must be union structure?
    bool isFlagAtHomeToScore;           // 0x9F    CTF usage
    uint SingleFlagTimer;               // 0xA0 CTF usage, 0 = off, 1800 = 1 min, and so on.
}
static assert(s_gametype.sizeof == 0xA4, "Incorrect size of s_gametype");

struct GametypeGFlag {
    align(1):
    real_vector3d   World;                      //0x00 Coordinate of where to respawn at.
    ubyte[8]        UNKNOWN0;                   //0x0C Nulls...
    ubyte[4]        UNKNOWN1;                   //0x14 Don't know...
    float           unknown;                    //0x18 Possible float?
    ubyte[8]        UNKNOWN2;                   //0x1C Nulls...
    uint            unknown2;                   //0x24 Always -1
}
static assert(GametypeGFlag.sizeof == 0x28, "Incorrect size of GameTypeGFlag");

struct GameTypeCTFg {                       //size = 0x38
    align(1):
    GametypeGFlag*[2]   flagParams;             //0x0000        // 0 = Red flag, 1 = Blue flag
    s_ident[2]          teamFlagIds;            //0x0008
    uint[2]             teamScore;              //0x0010        //0 = Red team, 1 = Blue team
    uint                scoreLimit;             //0x0018        //Not sure what size this is atm.
    bool[2]             flagCaptured;           //0x001C        // 0 = Red flag, 1 = Blue flag
    ushort              Unknown1;               //0x001E
    //Trial version end for CTF
    uint[2]             flagNotAtBase;          //0x0020        //Timer in ticks; 0 = Red flag, 1 = Blue flag
    ubyte[0x10]         UNKNOWN0;               //0x0028        //Missing some other informations, need to edit more here.
}
static assert(GameTypeCTFg.sizeof == 0x38, "Incorrect size of GameTypeCTFg");
struct GameTypeKOTHg {                          //size = 0x288
    align(1):
    uint[16] hillScoreTicks;                    //0x0000        //Scoring in ticks base on player id                                                //0x0038
    uint[16] hillScoreTicksRelativeMapTicks;    //0x0040        //Tick difference from how long in map ticks and is in hill base on player id.      //0x0078
    bool[16] isInHill;                          //0x0080        //Player is in hill, base on player id                                              //0x00B8
    ubyte[0x100] UNKNOWN0;                      //0x0090        //Don't know what this is and it's relative to hill being moved around if set.      //0x00C8
    uint Unknown1;                              //0x0190        //Goes up when someone entered                                                      //0x01C8
    uint Unknown2;                              //0x0194        //Tick goes up when someone is in the hill.                                         //0x01CC
    s_ident player;                             //0x0198        //Shows the player id.                                                              //0x01D0
    ubyte[0xEC] UNKNOWN3;                       //0x019C        //Dunno what the rest are.                                                          //0x01D4
}
struct GameTypeODDBALLg {                       //size = 0x148
    uint scoreLimit;                            //0x0000        //Total ticks to win.                                                               //0x02C0
    uint[16] scoreTicks;                        //0x0004                                                                                            //0x02C4
    uint[16] scoreTicks2;                       //0x0044        //Always the same with above.                                                       //0x0304
    uint[16] Unknown4;                          //0x0084        //Wizzard commented this is for juggernut...                                        //0x0344
    s_ident[16] holder;                         //0x00C4        //This is base on oddball #, not player. Also using player id                       //0x0384
    uint[16] relocateTicks;                     //0x0104        //This is base on oddball #, not player. Also using player id                       //0x03C4
    uint Unknown5;                              //0x0144        //Null                                                                              //0x0404
}
struct GameTypeRACEg {                          //size = 0x148
    uint        checkpointTotal;                //0x0000        //Total navpoints around the map to score per lap.                                  //0x0408
    uint[16]    Unknown6;                       //0x0004        //Nulls                                                                             //0x040C
    uint[16]    checkpointCurrent;              //0x0044        //Counting to checkpointTotal                                                       //0x044C
    uint        Unknown7;                       //0x0084                                                                                            //0x048C
    uint[16]    raceLaps;                       //0x0088        //Total laps completed                                                              //0x0490
    uint[16]    Unknown8;                       //0x00C8        //So far just nulls...                                                              //0x04D0
    uint[16]    Unknown9;                       //0x0108        //Don't know what these are and not relative to players.                            //0x0510
}
struct GameTypeSLAYERg {                        //size = 0x80
    uint[16] playerScore;                       //0x0000                                                                                            //0x0550
    uint[16] playerScore2;                      //0x0040        //Duplicated and appear useless...                                                  //0x0590
}
struct GameTypeGlobals {
    GameTypeCTFg        ctfGlobal;
    GameTypeKOTHg       kothGlobal;
    GameTypeODDBALLg    oddballGlobal;
    GameTypeRACEg       raceGlobal;
    GameTypeSLAYERg     slayerGlobal;
}
static assert(GameTypeGlobals.sizeof == 0x5D0, "Incorrect size of GameTypeGlobals");

struct s_server_header {
    align(1):
    uint*       Unknown0;       //0x000     // at least I _think_ it's a pointer since there _is_ something if i follow it.
    ushort      state;          //0x004
    ushort      Unknown2;       //0x006
    wchar[66]   server_name;    //0x008
    char[128]   map_name;       //0x08C
    wchar[24]   gametype_name;  //0x10C
    //IMPORTANT: DO NOT USE! Below this does not match with other Halo PC platforms, it is base on Halo CE version.
    ubyte[40]   Unknown11;      //0x13C     // partial of Gametype need to break them down.
    ubyte       score_max;      //0x164
    ubyte[128]  Unknown3;       //0x165
    char        player_max;     //0x1E5     // Note: there is another place that also says MaxPlayers - i think it's the ServerInfo socket buffer.
    short       Unknown09;      //0x1E6
    ushort      totalPlayers;   //0x1E8
    short       Unknown10;      //0x1EA     // i think LastSlotFilled
}

enum e_action_state : ubyte {
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
}

struct damageFlags {
    align(1):
    ubyte[2] bitFieldFlag;
    /*bool unknown1:2;            //0.0-0.1
    bool kill1:1;               //0.2
    bool unknown2:1;            //0.3
    bool kill2:1;               //0.4
    bool unknown3:3;            //0.5-0.9
    bool unknown4:3;            //1.0-1.2
    bool cannotTakeDamage:1;    //1.3
    bool unknown5:4;            //1.4 - 1.9*/
}
static assert(damageFlags.sizeof == 0x02, "Incorrect size of damageFlags");


struct s_object {
    align (1):
    s_ident         ModelTag;               // 0x0000
    int             Zero;                   // 0x0004
    char[4]         Flags;                  // 0x0008
    int             Timer;                  // 0x000C
    ubyte bitFieldFlag0;
    /*char            unkBits : 2;            // 0x0010
    bool            ignoreGravity:1;
    char            unk1:3;
    bool            unk2:1;
    bool            noCollision:1;*/
    char[3]         unkBytes1;              // 0x0011
    int             Timer2;                 // 0x0014
    int[17]         Zero2;                  // 0x0018
    real_vector3d   World;                  // 0x005C
    real_vector3d   Velocity;               // 0x0068
    real_vector3d   Rotation;               // 0x0074
    real_vector3d   Scale;                  // 0x0080
    real_vector3d   VelocityPitchYawRoll;   // 0x008C  //current velocity for pitch, yaw, and roll
    int             LocationID;             // 0x0098
    int             Unknown1;               // 0x009C
    real_vector3d   UnknownVect2;           // 0x00A0
    float[2]        Unknown2;               // 0x00AC
    short           objType;                // 0x00B4
    short           Unknown3;               // 0x00B6
    short           GameObject;             // 0x00B8    // 0 >= is game object, -1 = is NOT game object
    short           Unknown4;               // 0x00BA
    int             Unknown5;               // 0x00BD
    s_ident         Player;                 // 0x00C0
    int[2]          Unknown6;               // 0x00C4
    s_ident         AntrMeta;               // 0x00CC
    int[2]          Unknown7;               // 0x00D0
    float           HealthMax;              // 0x00D8
    float           ShieldMax;              // 0x00DC
    float           Health;                 // 0x00E0
    float           Shield1;                // 0x00E4
    int[7]          Unknown8;               // 0x00E8
    short           Unknown9;               // 0x0104
    damageFlags     damageFlag;             // 0x0106
    short           Unknown10;              // 0x0108
    short           Unknown11;              // 0x010A
    int[2]          Unknown12;              // 0x010C
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
    int[5]          Unknown16;              // 0x0138
    s_ident         UnknownIdent1;          // 0x014C
    s_ident         UnknownIdent2;          // 0x0150
    int[6]          Zero3;                  // 0x0154
    s_ident         UnknownIdent3;          // 0x016C
    s_ident         UnknownIdent4;          // 0x0170
    int[16]         UnknownMatrix0;         //D3DXMATRIX UnknownMatrix;     // 0x0174
    int[16]         UnknownMatrix1;         //D3DXMATRIX UnknownMatrix1;    // 0x01B4
    //Everything after this is 0x01F4
}
static assert(s_object.sizeof == 0x1F4, "Incorrect size of s_object");

struct actionFlags {    // these are action flags, basically client button presses and these don't actually control whether or not an event occurs
    ubyte[2] bitFieldFlag;
    /*bool crouching : 1;           // 0 (a few of these bit flags are thanks to halo devkit)
    bool jumping : 1;               // 1
    char UnknownBit : 2;            // 2
    bool Flashlight : 1;            // 4
    bool UnknownBit2 : 1;           // 5
    bool actionPress : 1;           // 6 think this is just when they initially press the action button
    bool melee : 1;                 // 7
    char UnknownBit3 : 2;           // 8
    bool reload : 1;                // 10
    bool primaryWeaponFire : 1;     // 11 right mouse
    bool secondaryWeaponFire : 1;   // 12 left mouse
    bool secondaryWeaponFire1 : 1;  // 13
    bool actionHold : 1;            // 14 holding action button
    char UnknownBit4 : 1;           // 15*/
}
static assert(actionFlags.sizeof == 0x02, "Incorrect size of actionFlags");

struct s_biped {
    s_object        sObject;                    // 0x0000
    int[4]          Unknown;                    // 0x01F4
    short           isInvisible;                // 0x0204    normal = 0x41 invis = 0x51 (bitfield) Offset 0x422 is set zero for camo to start.
    char            Flashlight;                 // 0x0206
    char            Frozen;                     // 0x0207
    actionFlags     actionBits;                 // 0x0208 & 0x0209
    char[2]         Unknown1;                   // 0x020A
    int             UnknownCounter1;            // 0x020C
    int[2]          UnknownLongs1;              // 0x0210
    s_ident         PlayerOwner;                // 0x0218
    int[2]          UnknownLongs3;              // 0x021C
    real_vector3d   RightVect;                  // 0x0224
    real_vector3d   UpVect;                     // 0x0230
    real_vector3d   LookVect;                   // 0x023C
    real_vector3d   ZeroVect;                   // 0x0248
    real_vector3d   RealLookVect;               // 0x0254
    real_vector3d   UnknownVect3;               // 0x0260
    char[0x34]      Unknown2;                   // 0x026C
    ubyte           actionVehicle_crouch_stand; // 0x02A0 Is this really true? Found this from Wizard's code (Standing = 4) (Crouching = 3) (Vehicle = 0)
    char[0x02]      Unknown9;                   // 0x02A1
    e_action_state  animation_state;            // 0x02A3
    char[0x4C]      Unknown91;                  // 0x02A4
    short           vehicle_seat_index;         // 0x02F0
    ushort          CurWeaponIndex0;            // 0x02F2    (Do not attempt to edit this, will crash Halo)
    ushort          CurWeaponIndex1;            // 0x02F4    (Read only)
    ushort          Unknown6;                   // 0x02F6
    s_ident[4]      Weapons;                    // 0x02F8
    uint[4]         WeaponsLastUse;             // 0x0308
    int             UnknownLongs2;              // 0x0318
    char            grenadeIndex;               // 0x031C
    char            grenadeIndex1;              // 0x031D
    char            grenade0;                   // 0x031E
    char            grenade1;                   // 0x031F
    char            Zoom;                       // 0x0320
    char            Zoom1;                      // 0x0321
    char[2]         Unknown3;                   // 0x0322
    s_ident         SlaveController;            // 0x0324    Only effective for moving the Biped, sometimes does update the facing direction
    s_ident         WeaponController;           // 0x0328    Does update where to point, fire the weapon, and reload. Have not confirmed with other player's.
    s_ident         vehicle_eject_last;         // 0x032C
    char[460]       Unknown4;                   // 0x0330
    s_ident         bump_objectId;              // 0x04FC
    ubyte           Unknown7;                   // 0x0500    Relative to swap biped, not sure what else uses this.
    ushort          inAirticks;                 // 0x0501    Amount of time in the air?
    ubyte           isWalking;                  // 0x0503    0 = else, 1 = While on ground & is walking, 2 = rarely seen + seems to be using bit values
    char[76]        Unknown8;                   // 0x0504    0x422 (ushort) is set zero for camo to start.
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
    char[1216]      Unknown5;                   // 0x08F8 //Missing 0x092C?
}

//Major WIP Halo Structure Begin
struct s_weapon {
    s_object            sObject;
    char[12]            Unknown;                            //0x01F4
    s_ident             UnknownIdent;                       //0x0200  //Relative to assigne biped being dropped from.
    uint                NetworkTime;                        //0x0204
    char[36]            Unknown1;                           //0x0208

    mixin(bitfields!(
        uint, "Unknown16",      4,                          //0x022C.0-3
        bool, "Unknown17",      1,                          //0x022C.4
        bool, "isPickedup",     1,                          //0x022C.4-5
        bool, "isNotReturned",  1,                          //0x022C.6
        bool, "Unknown18",      1));                        //0x022C.7
    char[3]             Unknown19;                          //0x022D
    mixin(bitfields!(
        bool, "Unknown20",  1,                              //0x0230.0
        bool, "Melee",      1,                              //0x0230.1
        uint, "Unknown21",  2,                              //0x0230.2-3
        uint, "Unknown22",  4,));                           //0x0230.4-7
    char[3]             Unknown23;                          //0x0231

    uint                Unknown24;                          //0x0234

    bool                IsFiring;                           //0x0238
    char                Unknown3;                           //0x0239
    ushort              WeaponReadyWaitTime;                //0x023A
    char[36]            Unknown4;                           //0x023C
    uint                SomeCounter;                        //0x0260
    uint                IsNotFiring;                        //0x0264
    uint[5]             Unknown5;                           //0x0268
    float               Unknown6;                           //0x027C
    uint                Unknown7;                           //0x0280
    float[2]            Unknown8;                           //0x0284
    s_ident             UnknownIdent1;                      //0x028C
    uint                AutoReloadCounter;                  //0x0290
    ubyte[28]           Unknown9;                           //0x0294
    ushort              ReloadFlags;                        //0x02B0 // 0=NotReloading,1=Reloading, 2=???, 3=???  //is correct
    ushort              ReloadCountdown;                    //0x02B2    //can set to 0 to finish reload countdown
    ushort              Unknown10;                          //0x02B4
    ushort              BulletCountInRemainingClips;        //0x02B6
    ushort              BulletCountInCurrentClip;           //0x02B8
    char[18]            Unknown11;                          //0x02BA
    s_ident             UnknownIdent2;                      //0x02CC
    uint                LastBulletFiredTime;                //0x02DO
    char[16]            Unknown12;                          //0x02D4
    real_vector3d[2]    Unknown13;                          //0x02E4
    char[12]            Unknown14;
    uint                BulletCountInRemainingClips1;
    char[52]            Unknown15;
} // Size - 1644(0x066C)

align (1) struct s_vehicle {
    s_ident         ModelTag;               // 0x0000
    long            Zero;                   // 0x0004
    char[4]         Flags;                  // 0x0008
    long            Timer;                  // 0x000C
    char[4]         Flags2;                 // 0x0010
    long            Timer2;                 // 0x0014
    long[17]        Zero2;                  // 0x0018
    real_vector3d   World;                  // 0x005C
    real_vector3d   Velocity;               // 0x0068
    real_vector3d   LowerRot;               // 0x0074  //incorrect
    real_vector3d   Rotation;               // 0x0080  //incorrect
    real_vector3d   UnknownVect1;           // 0x008C
    long            LocationID;             // 0x0098
    long            UnknownO1;              // 0x009C
    real_vector3d   UnknownVect2;           // 0x00A0
    float[2]        UnknownO2;              // 0x00AC
    long[3]         UnknownO3;              // 0x00B4
    s_ident         Player;                 // 0x00C0
    long[2]         UnknownO4;              // 0x00C4
    s_ident         AntrMeta;               // 0x00CC
    long[4]         UnknownO5;              // 0x00D0
    float           Health;                 // 0x00E0
    float           Shield1;                // 0x00E4
    long[11]        UnknownO6;              // 0x00E8
    s_ident         VehicleWeapon;          // 0x0114
    s_ident         CurrentFirstPersonOn;   // 0x0118
    s_ident         Vehicle;                // 0x011C
    short           SeatType;               // 0x0120
    short           UnknownO7;              // 0x0122
    long            UnknownO8;              // 0x0124
    float           Shield2;                // 0x0128
    float           Headlight1;             // 0x012C
    float           Unknown9;               // 0x0130
    float           Headlight2;             // 0x0134
    long[5]         UnknownO10;             // 0x0138
    s_ident         UnknownIdent1;          // 0x014C
    s_ident         UnknownIdent2;          // 0x0150
    long[6]         Zero3;                  // 0x0154
    s_ident         UnknownIdent3;          // 0x016C
    s_ident         UnknownIdent4;          // 0x0170
    real_vector3d   UnknownMatrix;          // 0x0174
    real_vector3d   UnknownMatrix1;         // 0x0180
    //** END OBJECT part
    char[0x7A]      UnknownVeh0;            // 0x018C
    bool            isNotAllowPlayerEntry;  // 0x206
    char[0x11D]     UnknownVeh1;            // 0x18C
    s_ident         SlaveController;        // 0x324
    int[0xA1]       UnknownInt;             // 0x328
    int             respawn_idle;           // 0x5AC
    short           respawn_id;             // 0x5B0
    //Anything goes after this?
} // Size - 3580(0xDFC)
//Major WIP Halo Structure End

//TODO: Variable of offset seems to have some sort of data usage base from SDMHaloMapLoader.c/h Need to do some research.
align (1) struct s_map_header {
    uint        head;           //0x00 //enum 'head' string type
    uint        haloVersion;    //0x04
    uint        length;         //0x08
    ubyte[4]    PADDING0;       //0x0C //Nulls
    uint        offset;         //0x10
    uint        metaSize;       //0x14
    ubyte[8]    PADDING1;       //0x18 //Nulls
    char[32]    mapName;        //0x20
    char[32]    builddate;      //0x40
    uint        type;           //0x060 // 0 = Campaign, 1 = Multi-player, 2 = Menu
    uint        unknown07;      //0x064
}
static assert(s_map_header.sizeof == 0x68, "Incorrect size of s_map_header");

align (1) struct s_map_status {
    bool   Unknown1;    //0x00
    bool   Unknown2;    //0x01
    ushort Unknown3;    //0x02 NULLs
    uint   Unknown4;    //0x04
    uint   Unknown5;    //0x08
    uint   upTime;      //0x0C 1 sec = 30 ticks <-- use this as recommended upTime
    uint   Unknown6;    //0x10
    uint   upTime1;     //0x14 1 sec = 30 ticks
    float  Unknown7;    //0x18
    uint   Unknown8;    //0x1C Don't know what this is and it's increasing rapidly...
}
static assert(s_map_status.sizeof == 0x20, "Incorrect size of s_map_status");

align (1) struct s_console_header {
    bool        gamePause;      //0x00
    bool        allowConsole;   //0x01
    ushort      unknown01;      //0x02 //Nulls
    bool        isNotConsole;   //0x04
    ubyte       unknown02;      //0x05
    ubyte       unknown03;      //0x06
    ubyte       keyPress;       //0x07
    ushort      unknown04;      //0x08
    ushort      unknown05;      //0x0A
    ushort      unknown06;      //0x0C
    ushort[61]  unknown07;      //0x10 //Nulls
    uint        unknown08;      //0x88
    uint        unknown09;      //0x8C
    uint        unknown10;      //0x90
    uint        unknown11;      //0x94
    char[32]    inputName;      //0x98
    char[255]   input;          //0xB8
}
static assert(s_console_header.sizeof == 0x1B7, "Incorrect size of s_console_header");

align (1) struct s_ban_check {
    wchar[9]   password;            //0x00
    char[32]   cdKeyHash;           //0x12
    char[40]   unknown0;            //0x32
    char[4]    unknown1;            //0x5A
    wchar[12]  requestPlayerName;   //0x5E
} // Size - 100 (0x64) bytes
static assert(s_ban_check.sizeof == 0x76, "Incorrect size of s_ban_check");

//Extras for Add-on API usage.

struct s_cheat_header {
    align (1):
    bool deathlessPlayer;       //0x00
    bool jetPack;               //0x01
    bool infiniteAmmo;          //0x02
    bool bumpPossession;        //0x03
    bool superJump;             //0x04
    bool reflexDamage;          //0x05
    bool medUsa;                //0x06
    bool omnipotent;            //0x07
    bool controller;            //0x08
    bool bottomlessClip;        //0x09
}
static assert(s_cheat_header.sizeof == 0xA, "Incorrect size of s_cheat_header");

struct D3DCOLOR_COLORVALUE_ARGB {
    float a = 1.0f;
    float r = 1.0f;
    float g = 1.0f;
    float b = 1.0f;
}
struct s_console_color_list {
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

}
static assert(s_console_color_list.sizeof == 0x38, "Incorrect size of s_console_color_list");

struct GlobalVars {
    uint                    ptrAcceptableASCIICharsArray;   //Unknown if this is correct.
    uint                    ptrExceptionHandlerFunc;        //Halo CE 0x006207AC
    s_console_color_list    consoleColorStruct;             //Halo CE 0x006207B0 - 0x006207E4
    uint                    ptrStrAsleep;                   //Halo CE 0x006207E8
    uint                    ptrStrAlert;                    //Halo CE 0x006207EC
    uint                    ptrStrCombat;                   //Halo CE 0x006207F0
    uint                    UnknownNull;
    uint                    ptrStrFlood_Carrier;            //Halo CE 0x006207F8,  A pointer plus seems to be a structure for it.
    uint[4]                 ptrFlood_Carrier_Parms0;        //Part 1 - Parameters?
    uint                    ptrFlood_CarrierFunc;           //Part 2 - Is a pointer for a Flood Carrier Func?
    uint[4]                 ptrFlood_Carrier_Parms1;        //Part 3 - Parameters?
    //Etc, etc, etc. Don't have time to figure out the rest.
}

struct SoundPlay {
    short inUsed;       //0x00 //If is -1, then not in used. If is 0 or above, then in used.
    short Priority;     //0x02 //0 = general action, 1 = Unknown, 2 = UI sounds, 3 = possible loop or background sounds.
}
struct SoundVars {
    align (1):
    bool soundEnabled;              //0x00    //Not entirely sure...
    bool globalSoundOn;             //0x01
    bool muteSoundAsNotInWindow;    //0x02
    bool UNKNOWN0;                  //0x03
    uint UNKNOWN1;                  //0x04    Full of nulls
    uint UNKNOWNPTR0;               //0x08    Some kind of pointer to a struct?
    uint time;                      //0x0C    counting up
    float UNKNOWN2;                 //0x10    some kind of sound volume? (Constantly changing)
    uint UNKNOWN3;                  //0x14    Unknown 1/0 value, constantly changing.
    uint UNKNOWN4;                  //0x18    Always 1?
    float UNKNOWN5;                 //0x1C    Always 1?
    real_vector3d Vector0;          //0x20
    real_vector3d Vector1;          //0x2C
    real_vector3d Vector2;          //0x38
    real_vector3d World;            //0x44
    real_vector3d UNKNOWN6;         //0x50    0 and 0x80000000 per float (not sure why)
    real_vector3d[6] Vector4;       //0x5C
    float vol_slider;               //0xA4
    float vol_Music;                //0xA8
    float vol_Master;               //0xAC
    float vol_Effects;              //0xB0
    uint Max_Channels;              //0xB4
    uint UNKNOWN7;                  //0xB8
    uint UNKNOWN8;                  //0xBC    Null
    uint soundHeader;               //0xC0
    ubyte[0x1C] UNKNOWN9;           //0xC4    //Full of nulls
    short UNKNOWN10;                //0xE0
    short UNKNOWN11;                //0xE2
    SoundPlay[38] sPlay;            //0xE4
}
static assert(SoundVars.sizeof == 0x17C, "Incorrect size of SoundVars");

struct validationCheck {
    uint UniqueID;
    uint isValid;
    char *message;
}
static assert(validationCheck.sizeof == 0xC, "Incorrect size of validationCheck");
