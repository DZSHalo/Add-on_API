using System;
using System.Runtime.InteropServices;

//Team Color Begin
public enum e_color_team_index : sbyte {
    TEAM_NONE = -1, //Reserved for H-Ext usage ONLY!
    TEAM_RED = 0,
    TEAM_BLUE = 1
}
//Team Color End

//Color Indexes Start
public enum e_color_index {
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

public enum chatType {
    GLOBAL = 0,
    TEAM,
    VEHICLE,
    SERVER
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct chatData    {
    public chatType type;       //range of 0 - 3, sort from Global, Team, Vehicle, and Server (CE only)
    public uint     player;     //range of 0 - 15
    public IntPtr   msg_ptr;    //range of 0 - TBA
};
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct rconData {
    public IntPtr msg_ptr;
    public uint unk; // always 0
    [MarshalAs(UnmanagedType.LPStr, SizeConst = 0x50)]
    public string msg;
};

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct rconDecode {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=9)]
    public string pass;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 65)]
    public string cmd;
};

[StructLayout(LayoutKind.Sequential)]
public struct bone {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=10)]
    public float[] unknown;
    public real_vector3d World;
};

[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
public struct s_player_reserved_slot {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public string               PlayerName;     //0x00
    public short                ColorIndex;     //0x18      // See defined color indexes above.
    public short                Unknown1;       //0x1A      // 0xFFFF
    public sbyte                MachineIndex;   //0x1C      // Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
    public byte                 Unknown2;       //0x1D      //something. But, if these 4 chars are FF's, then the player isn't on.
    public e_color_team_index   Team;           //0x1E
    public sbyte                PlayerIndex;    //0x1F      // Index to their StaticPlayer
}
public struct s_player_reserved_slot_ptr {
    public IntPtr ptr;
}

[StructLayout(LayoutKind.Sequential)]
public struct S_un_b { public byte s_b1, s_b2, s_b3, s_b4; };
[StructLayout(LayoutKind.Sequential)]
public struct S_un_w { public ushort s_w1, s_w2; };
[StructLayout(LayoutKind.Explicit, Size=4)]
public struct in_addr {
    [FieldOffset(0)]
    public S_un_b un_b;
    [FieldOffset(0)]
    public S_un_w un_w;
    [FieldOffset(0)]
    public uint S_addr;
};
/* For reference only
#define s_addr  S_un.S_addr         // can be used for most tcp & ip code
#define s_host  S_un.S_un_b.s_b2    // host on imp
#define s_net   S_un.S_un_b.s_b1    // network
#define s_imp   S_un.S_un_w.s_w2    // imp
#define s_impno S_un.S_un_b.s_b4    // imp #
#define s_lh    S_un.S_un_b.s_b3    // logical host
 */

#pragma warning disable 0169
#pragma warning disable 0649
class MachineSP3 {
    in_addr IPAddress;
    ushort port;
}; //At the moment do not know complete size...
class MachineSP2 {
    MachineSP3 data3;
}; //At the moment do not know complete size...
public struct MachineSP1 {
    IntPtr data2;
    //MachineSP2 data2;
}; //At the moment do not know complete size...
#pragma warning restore 0169
#pragma warning restore 0649

[Flags]
public enum b_machine_flags : ushort {
    None = 0,
    Crouch = 1 << 0,                                 //0x0024
    Jump = 1 << 1,
    Flashlight = 1 << 2,
    Unknownbit0 = 1 << 3,
    Unknownbit1 = 1 << 4,
    Unknownbit2 = 1 << 5,
    Unknownbit3 = 1 << 6,
    Unknownbit4 = 1 << 7,
    Reload = 1 << 8,                                //0x0025
    Fire = 1 << 9,
    Swap = 1 << 10,
    Grenade = 1 << 11,
    Unknownbit5 = 1 << 12,
    Unknownbit6 = 1 << 13,
    Unknownbit7 = 1 << 14,
    Unknownbit8 = 1 << 15
}
[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Ansi)]
public struct s_machine_slot {
    public IntPtr           data1;                          //0x0000 // Following this, i found a pointer next to other useless stuff. Then, another pointer, then i found some stuff that looked like it /MIGHT/ very well be strings related to a hash. *shrug*
    public int              Unknown0;                       //0x0004
    public int              Unknown1;                       //0x0008
    public short            machineIndex;                   //0x000C
    public short            Unknown9;                       //0x000E // First is 1, then 3, then 7 and back to 0 if not in used (1 is found during the CD Hash Check, 7 if currently playing the game)
    public int              isAvailable;                    //0x0010
    public int              Unknown10;                      //0x0014
    public int              Unknown11;                      //0x0018
    public int              Unknown12;                      //0x001C    // most of the time 1, but sometimes changes to 2 for a moment.
    public int              Unknown13;                      //0x0020
    public b_machine_flags  flags;                          //0x0024 & 0x0025
    public short            Unknown14;                      //0x0026
    public float            Yaw;                            //0x0028 // player's rotation - in radians, from 0 to 2*pi, (AKA heading)
    public float            Pitch;                          //0x002C // Player's pitch - in radians, from -pi/2 to +pi/2, down to up.
    public float            Roll;                           //0x0030 // roll - unless walk-on-walls is enabled, this will always be 0.
    public int              Unknown15;                      //0x0034
    public int              Unknown16;                      //0x0038
    public float            ForwardVelocityMultiplier;      //0x003C
    public float            HorizontalVelocityMultiplier;   //0x0040
    public float            ROFVelocityMultiplier;          //0x0044
    public short            WeaponIndex;                    //0x0048
    public short            GrenadeIndex;                   //0x004A
    public short            UnknownIndex;                   //0x004C // The index is -1 if no choices are even available.
    public short            Unknown2;                       //0x004E
    public short            Unknown3;                       //0x0050 // 1
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
    public string           SessionKey;                     //0x0052 // This is used to accept the incoming player PLUS validate with gamespy server.
    public short            Unknown4;                       //0x005A
    public uint             UniqueID;                       //0x005C // increase every time a player join (notice: it is not focus on specific machine struct, it applies to all.)
}
public struct s_machine_slot_ptr {
    public IntPtr ptr;
}
[Flags]
public enum b_player_flags : ushort {
    None = 0,
    Melee = 1 << 0,
    Action = 1 << 1,
    UnknownBit = 1 << 2,
    Flashlight = 1 << 3,
    UnknownBit1 = 1 << 4,
    UnknownBit4 = 1 << 5,
    UnknownBit5 = 1 << 6,
    UnknownBit6 = 1 << 7,
    UnknownBit2 = 1 << 8,
    UnknownBit7 = 1 << 9,
    UnknownBit8 = 1 << 10,
    UnknownBit9 = 1 << 11,
    UnknownBit10 = 1 << 12,
    Reload = 1 << 13,
    UnknownBit3 = 1 << 14,
    UnknownBit11 = 1 << 15,
}
[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
public struct s_player_slot {
    public short            PlayerID;                       //0x000
    public short            IsLocal;                        //0x002            // 0=Local(no bits set), -1=Other Client(All bits set)
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public string           Name;                           //0x004            // Unicode
    public s_ident          UnknownIdent;                   //0x01C
    public int              Team;                           //0x020            // 0=Red, 1=Blue; if do client host, you will see hud's team changed instant.
    public s_ident          SwapObject;                     //0x024
    public short            SwapType;                       //0x028            // Vehicle=8, Weapon=6
    public short            SwapSeat;                       //0x02A            // Warthog-Driver=0, Passenger=1, Gunner=2, Weapon=-1
    public int              RespawnTimer;                   //0x02C            // Counts down when dead, Alive=0
    public int              Unknown;                        //0x02F
    public s_ident          CurrentBiped;                   //0x034
    public s_ident          PreviousBiped;                  //0x038
    //IMPORTANT: need to verify this.
    //   uint LocationID;                  // This is very, very interesting. BG is split into 25 location ID's. 1 - 19
    public short            LocationID;                     //0x03C            //Should be uint?
    //short                   ClusterIndex;                   //0x03C
    /*public byte             Swap : 1;                       //0x03E.0
    public byte             UnknownBits4 :7;                //0x03E.7*/
    public byte             UnknownByte1;                   //0x03E
    public byte             UnknownByte;                    //0x03F
    public s_ident          UnknownIdent1;                  //0x040            //BulletCount?
    public int              LastBulletShotTime;             //0x044            // since game start(0)
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 12)]
    public string           Name1;                          //0x048
    public short            ColorIndex;                     //0x060            // See defined color indexes above.
    public short            Unknown00;                      //0x062
    public byte             MachineIndex;                   //0x064            // Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
    public byte             Unknown0;                       //0x065            //something. But, if these 4 chars are FF's, then the player isn't on.
    public byte             iTeam;                          //0x066
    public byte             PlayerIndex;                    //0x067            // Index to their StaticPlayer
    public int              Unknown1;                       //0x068
    public float VelocityMultiplier;                        //0x06C <--- and below are correct!
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public s_ident[]        UnknownIdent3;                  //0x070
    public int              Unknown2;                       //0x080
    public int              LastDeathTime;                  //0x084        // since game start(0)
    public s_ident          killInOrderObjective;           //0x088
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[]           Unknown3;                       //0x08C
    public short            KillsCount;                     //0x09C
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[]           Unknown4;                       //0x09E
    public short            AssistsCount;                   //0x0A4
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[]           Unknown5;                       //0x0A6
    public short            BetrayedCount;                  //0x0AC
    public short            DeathsCount;                    //0x0AE
    public short            SuicideCount;                   //0x0B0
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 18)]
    public byte[]           Unknown6;                       //0x0B2
    public short            FlagStealCount;                 //0x0C4
    public short            FlagReturnCount;                //0x0C6
    public short            FlagCaptureCount;               //0x0C8
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[]           Unknown7;                       //0x0CA
    public s_ident          UnknownIdent4;                  //0x0D0
    public byte             Unknown8;                       //0x0D4
    [MarshalAs(UnmanagedType.U1)]
    public bool             HasQuit;                        //0x0D5
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public byte[]           Unknown81;                      //0x0D6
    public short            Ping;                           //0x0DC
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
    public byte[]           Unknown9;                       //0x0DE
    public s_ident          UnknownIdent5;                  //0x0EC
    public int              Unknown10;                      //0x0F0
    public int              SomeTime;                       //0x0F4
    public real_vector3d    World;                          //0x0F8
    public s_ident          UnknownIdent6;                  //0x104
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public byte[]           Unknown11;                      //0x108
    public b_player_flags   flags;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 26)]
    public byte[]           Unknown12;                      //0x11E
    public real_vector2d    Rotation;                       //0x138        // Yaw, Pitch (again, in radians.
    public float            ForwardVelocityMultiplier;      //0x140
    public float            HorizontalVelocityMultiplier;   //0x144
    public float            RateOfFireVelocityMultiplier;   //0x148
    public short            HeldWeaponIndex;                //0x14C
    public short            GrenadeIndex;                   //0x14E
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[]           Unknown13;                      //0x150
    public real_vector3d    LookVect;                       //0x154
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
    public byte[]           Unknown14;                      //0x160
    public real_vector3d    WorldDelayed;                   //0x170    // Oddly enough... it matches the world vect, but seems to lag behind (Possibly what the client reports is _its_ world coord?)
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 132)]
    public byte[]           Unknown15;                      //0x17C
}
public struct s_player_slot_ptr {
    public IntPtr ptr;
}

public enum GAMETYPE {
    CTF = 1,
    SLAYER = 2,
    ODDBALL = 3,
    KOTH = 4,
    RACE = 5
}

[Flags]
public enum b_gametype_flags : byte {
    None = 0,
    showEnemyRadar = 1 << 0,                                 //0x0024
    friendlyIndicator = 1 << 1,
    infiniteGrenade = 1 << 2,
    noShield = 1 << 3,
    invisible = 1 << 4,
    isCustomEquipment = 1 << 5,
    showFriendlyRadar = 1 << 6,
    unknown = 1 << 7
}

[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
public struct s_gametype {                      //UNDONE GameType Struct is not 100% decoded.
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=24)]
    public string name;                         // 0x00
    public uint game_stage;                     // 0x30 1=CTF, 2=Slayer, 3=Oddball, 4=KOTH, 5=Race
    [MarshalAs(UnmanagedType.I1)]
    public bool isTeamPlay;                     // 0x34
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
    public byte[] NULL0;                        // 0x35
    public b_gametype_flags flags;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
    public byte[] Unknown0;                     // 0x39        //TODO need to find out what these are and 0x38 as well.
    public uint objective_indicator;            // 0x3C 0=Motion Tracker, 1=Navpoints, 2=None
    public uint is_odd_man_out;                 // 0x40
    public uint respawn_time_growth;            // 0x44
    public uint respawn_time;                   // 0x48
    public uint respawn_suicide_penalty;        // 0x4C
    public uint limit_lives;                    // 0x50
    public float health;                        // 0x54
    public uint score_limit;                    // 0x58
    public uint weapon_type;                    // 0x5C 0 = default, 1 = pistols, 2 = rifles, 3 = plasma rifles, 4 = sniper, 5 = no sniper, 6 = rocket launchers, 7 = shotguns, 8 = short range, 9 = human, 10 = covenant, 11 = classic, 12 = heavy weapons

    //Vehicles section
    public uint vehicle_red;                    // 0x60 Need some work here.
    public uint vehicle_blue;                   // 0x64 Need some work here.
    public uint vehicle_respawn_time;           // 0x68 ticks

    public uint is_friendly_fire;               // 0x6C
    public uint respawn_betrayal_penalty;       // 0x70
    public uint is_team_balance;                // 0x74
    public uint time_limit;                     // 0x78

    // ball gametype data
    public uint MovingHill;                     // 0x7C (KOTH) 0=off, 1=on; (Race) 0=Normal, 1=any order, 2=Rally; (Oddball) 0=off, 1=on;
    public byte TeamScoring;                    // 0x80 (Race) 0=minimal, 1=maximum, 2=Sum; (Oddball) 0=Slow, 1=Normal, 2=Fast
    public short Unknown2;                      // 0x81
    public byte TraitBallWith;                  // 0x83 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
    public uint Unknown3;                       // 0x84
    public uint TraitBallWithout;               // 0x88 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=14)]
    public byte[] Unknown4;                     // 0x8C - 0x99 unknown
    [MarshalAs(UnmanagedType.I1)]
    public bool Unknown5;                       // 0x9A
    [MarshalAs(UnmanagedType.I1)]
    public bool Unknown6;                       // 0x9B
    [MarshalAs(UnmanagedType.I1)]
    public bool noDeathBonus;                   // 0x9C
    [MarshalAs(UnmanagedType.I1)]
    public bool noKillPenalty;                  // 0x9D
    [MarshalAs(UnmanagedType.I1)]
    public bool isKillInOrder_flagMustReset;    // 0x9E    isKillInOrder = Slayer, FlagMustReset = CTF    //CTF and Slayer is sharing this... must be union structure?
    [MarshalAs(UnmanagedType.I1)]
    public bool isFlagAtHomeToScore;            // 0x9F    CTF usage
    public uint SingleFlagTimer;                // 0xA0 CTF usage, 0 = off, 1800 = 1 min, and so on.
};
public struct s_gametype_ptr {
    public IntPtr ptr;
}

[StructLayout(LayoutKind.Sequential)]
public struct s_gametype_gflag {
    public real_vector3d World; //0x00 Coordinate of where to respawn at.
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
    public byte[] UNKNOWN0;       //0x0C Nulls...
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
    public byte[] UNKNOWN1;       //0x14 Don't know...
    public float unknown;       //0x18 Possible float?
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
    public byte[] UNKNOWN2;       //0x1C Nulls...
    public uint unknown2;       //0x24 Always -1
};
public struct s_gametype_gflag_ptr {
    public IntPtr ptr;
}

#pragma warning disable 0169
#pragma warning disable 0649
public struct boolean {
    [MarshalAs(UnmanagedType.I1)]
    bool boolNative;
};
#pragma warning restore 0169
#pragma warning restore 0649
public struct s_gametype_CTFg {                     //size = 0x38
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
    public s_gametype_gflag_ptr[] flagParams;       //0x0000        // 0 = Red flag, 1 = Blue flag
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
    public s_ident[] teamFlagIds;                   //0x0008
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
    public uint[] teamScore;                        //0x0010        //0 = Red team, 1 = Blue team
    public uint scoreLimit;                         //0x0018        //Not sure what size this is atm.
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
    public boolean[] flagCapture;                      //0x001C        // 0 = Red flag, 1 = Blue flag
    public ushort Unknown1;                         //0x001E
    //Trial version end for CTF
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
    public uint[] flagNotAtBase;                    //0x0020        //Timer in ticks; 0 = Red flag, 1 = Blue flag
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x10)]
    public byte[] UNKNOWN;                          //0x0028        //Missing some other informations, need to edit more here.
};
public struct s_gametype_KOTHg {                    //size = 0x288
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] hillScoreTicks;                   //0x0000        //Scoring in ticks base on player id                                                //0x0038
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] hillScoreTicksRelativeMapTicks;   //0x0040        //Tick difference from how int in map ticks and is in hill base on player id.      //0x0078
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public boolean[] isInHill;                      //0x0080        //Player is in hill, base on player id                                              //0x00B8
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x100)]
    public byte[] UNKNOWN0;                         //0x0090        //Don't know what this is and it's relative to hill being moved around if set.      //0x00C8
    public uint Unknown2;                           //0x0190        //Goes up when someone entered                                                      //0x01C8
    public uint Unknown3;                           //0x0194        //Tick goes up when someone is in the hill.                                         //0x01CC
    public s_ident player;                          //0x0198        //Shows the player id.                                                              //0x01D0
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=0xEC)]
    public byte[] UNKNOWN1;                         //0x019C        //Dunno what the rest are.                                                          //0x01D4
};
public struct s_gametype_ODDBALLg {                 //size = 0x148
    public uint scoreLimit;                         //0x0000        //Total ticks to win.                                                               //0x02C0
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] scoreTicks;                       //0x0004                                                                                            //0x02C4
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] scoreTicks2;                      //0x0044        //Always the same with above.                                                       //0x0304
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] Unknown4;                         //0x0084        //Wizzard commented this is for juggernut...                                        //0x0344
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public s_ident[] holder;                        //0x00C4        //This is base on oddball #, not player. Also using player id                       //0x0384
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] relocateTicks;                    //0x0104        //This is base on oddball #, not player. Also using player id                       //0x03C4
    public uint Unknown5;                           //0x0144        //Null                                                                              //0x0404
};
public struct s_gametype_RACEg {                    //size = 0x148
    public uint checkpointTotal;                    //0x0000        //Total navpoints around the map to score per lap.                                  //0x0408
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] Unknown6;                         //0x0004        //Nulls                                                                             //0x040C
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] checkpointCurrent;                //0x0044        //Counting to checkpointTotal                                                       //0x044C
    public uint Unknown7;                           //0x0084                                                                                            //0x048C
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] raceLaps;                         //0x0088        //Total laps completed                                                              //0x0490
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] Unknown8;                         //0x00C8        //So far just nulls...                                                              //0x04D0
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] Unknown9;                         //0x0108        //Don't know what these are and not relative to players.                            //0x0510
};
public struct s_gametype_SLAYERg {                  //size = 0x80
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] playerScore;                      //0x0000                                                                                            //0x0550
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=16)]
    public uint[] playerScore2;                     //0x0040        //Duplicated and appear useless...                                                  //0x0590
};
#pragma warning disable 0169
#pragma warning disable 0649
public struct s_gametype_globals {
    public s_gametype_CTFg ctfGlobal;
    public s_gametype_KOTHg kothGlobal;
    public s_gametype_ODDBALLg oddballGlobal;
    public s_gametype_RACEg raceGlobal;
    public s_gametype_SLAYERg slayerGlobal;
};
#pragma warning restore 0169
#pragma warning restore 0649
public struct s_gametype_globals_ptr {
    public IntPtr ptr;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct map_name_ansi {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
    public string name;
}
[StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
public struct s_server_header {
    public IntPtr           Unknown0;       //0x000 // at least I _think_ it's a pointer since there _is_ something if i follow it.
    public ushort           state;          //0x004
    public ushort           Unknown2;       //0x006
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 66)]
    public string           server_name;    //0x008
    public map_name_ansi    map_name;       //0x08C
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 24)]
    public string           gametype_name;  //0x10C
    //IMPORTANT: DO NOT USE! Below this does not match with other Halo PC platforms, it is base on Halo CE version.
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=40)]
    public byte[]           Unknown11;      //0x13C // partial of Gametype need to break them down.
    public byte             score_max;      //0x164
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=128)]
    public byte[]           Unknown3;       //0x165
    public byte             player_max;     //0x1E5    // Note: there is another place that also says MaxPlayers - i think it's the ServerInfo socket buffer.
    public short            Unknown09;      //0x1E6
    public ushort           totalPlayers;   //0x1E8
    public short            Unknown10;      //0x1EA    // i think LastSlotFilled
};
public struct s_server_header_ptr {
    public IntPtr ptr;
}
public enum e_action_state : byte {
    IDLE = 0,
    GESTURE,
    TURN_LEFT,
    TURN_RIGHT,
    MOVE_FRONT,
    MOVE_BACK,
    MOVE_LEFT,
    MOVE_RIGHT,
    STUNNED_FRONT,
    STUNNED_BACK,
    STUNNED_LEFT,
    STUNNED_RIGHT,
    SLIDE_FRONT,
    SLIDE_BACK,
    SLIDE_LEFT,
    SLIDE_RIGHT,
    READY,
    PUT_AWAY,
    AIM_STILL,
    AIM_MOVE,
    AIRBORNE,
    LAND_SOFT,
    LAND_HARD,
    UNKNOWN0,
    AIRBORNE_DEAD,
    LAND_DEAD,
    SEAT_ENTER,
    SEAT_EXIT,
    CUSTOM_ANIMATION,
    IMPULSE,
    MELEE,
    MELEE_AIRBORNE,
    MELEE_CONTINUOUS,
    GRENADE_TOSS,
    RESURRECT_FRONT,
    RESURRECT_BACK,
    FEEDING,
    SURPRISE_FRONT,
    SURPRISE_BACK,
    LEAP_START,
    LEAP_AIRBORNE,
    LEAP_MELEE,
    UNUSED_AFAICT,
    BERSERK
}

[Flags]
public enum s_object_flags : byte {
    NONE = 0,
    unkBits_1 = 1 << 0,
    unkBits_2 = 1 << 1,
    ignoreGravity = 1 << 2,
    unk1_1 = 1 << 3,
    unk1_2 = 1 << 4,
    unk1_3 = 1 << 5,
    unk2 = 1 << 6,
    noCollision = 1 << 7
}

[Flags]
public enum damageFlags : ushort {
    NONE = 0,
    unknown1_1 = 1 << 0,
    unknown1_2 = 1 << 1,
    kill1 = 1 << 2,             //0.2
    unknown2 = 1 << 3,
    kill2 = 1 << 4,             //0.4
    unknown3_1 = 1 << 5,        //0.5-0.9
    unknown3_2 = 1 << 6,
    unknown3_3 = 1 << 7,
    unknown4_1 = 1 << 8,        //1.0-1.2
    unknown4_2 = 1 << 9,
    unknown4_3 = 1 << 10,
    cannotTakeDamage = 1 << 11,
    unknown5_1 = 1 << 12,       //1.4 - 1.9
    unknown5_2 = 1 << 13,
    unknown5_3 = 1 << 14,
    unknown5_4 = 1 << 15,
}

[StructLayout(LayoutKind.Sequential)]
public struct s_object {
    public s_ident          ModelTag;               // 0x0000
    public int              Zero;                   // 0x0004
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 4)]
    public byte[]           Flags;                  // 0x0008
    public int              Timer;                  // 0x000C
    //public byte            Flags2[4];              // 0x0010
    public s_object_flags   Flags1;                 //0x0010
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 3)]
    public byte[]           unkBytes1;              // 0x0011
    public int              Timer2;                 // 0x0014
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 17)]
    public int[]            Zero2;                  // 0x0018
    public real_vector3d    World;                  // 0x005C
    public real_vector3d    Velocity;               // 0x0068
    public real_vector3d    Rotation;               // 0x0074
    public real_vector3d    Scale;                  // 0x0080
    public real_vector3d    VelocityPitchYawRoll;   // 0x008C  //current velocity for pitch, yaw, and roll
    public int              LocationID;             // 0x0098
    public int              Unknown1;               // 0x009C
    public real_vector3d    UnknownVector2d;        // 0x00A0
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
    public float[]          Unknown2;               // 0x00AC
    public short            objType;                // 0x00B4
    public short            Unknown3;               // 0x00B6
    public short            GameObject;             // 0x00B8    // 0 >= is game object, -1 = is NOT game object
    public short            Unknown4;               // 0x00BA
    public int              Unknown5;               // 0x00BD
    public s_ident          Player;                 // 0x00C0
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[]            Unknown6;               // 0x00C4
    public s_ident          AntrMeta;               // 0x00CC
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[]            Unknown7;               // 0x00D0
    public float            HealthMax;              // 0x00D8
    public float            ShieldMax;              // 0x00DC
    public float            Health;                 // 0x00E0
    public float            Shield1;                // 0x00E4
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 7)]
    public int[]            Unknown8;               // 0x00E8
    public short            Unknown9;               // 0x0104
    public damageFlags      damageFlag;             // 0x0106
    public short            Unknown10;              // 0x0108
    public short            Unknown11;              // 0x010A
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[]            Unknown12;              // 0x010C
    public s_ident          VehicleWeapon;          // 0x0114
    public s_ident          Weapon;                 // 0x0118
    public s_ident          Vehicle;                // 0x011C
    public short            SeatType;               // 0x0120
    public short            Unknown13;              // 0x0122
    public int              Unknown14;              // 0x0124
    public float            Shield2;                // 0x0128
    public float            Flashlight1;            // 0x012C
    public float            Unknown15;              // 0x0130
    public float            Flashlight2;            // 0x0134
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 5)]
    public int[]            Unknown16;              // 0x0138
    public s_ident          UnknownIdent1;          // 0x014C
    public s_ident          UnknownIdent2;          // 0x0150
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 6)]
    public int[]            Zero3;                  // 0x0154
    public s_ident          UnknownIdent3;          // 0x016C
    public s_ident          UnknownIdent4;          // 0x0170
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16)]
    public int[]            UnknownMatrix0;         //D3DXMATRIX UnknownMatrix;     // 0x0174
    [MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst = 16)]
    public int[]            UnknownMatrix1;         //D3DXMATRIX UnknownMatrix1;    // 0x01B4
    //Everything after this is 0x01F4
};
public struct s_objectPtr {
    public IntPtr ptr;
}

[Flags]
public enum actionFlags : ushort {  // these are action flags, basically client button presses and these don't actually control whether or not an event occurs
    NONE = 0,
    crouching = 1 << 0,             // 0 (a few of these bit flags are thanks to halo devkit)
    jumping = 1 << 1,               // 1
    unknown0 = 1 << 2,              // 2
    unknown1 = 1 << 3,              // 3
    Flashlight = 1 << 4,            // 4
    unknown2 = 1 << 5,              // 5
    actionPress = 1 << 6,           // 6 think this is just when they initially press the action button
    melee = 1 << 7,                 // 7
    unknown3 = 1 << 8,              // 8
    unknown4 = 1 << 9,              // 9
    reload = 1 << 10,               // 10
    primaryWeaponFire = 1 << 11,    // 11 right mouse
    secondaryWeaponFire = 1 << 12,  // 12 left mouse
    secondaryWeaponFire1 = 1 << 13, // 13
    actionHold = 1 << 14,           // 14 holding action button
    UnknownBit4 = 1 << 15,          // 15
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct s_biped {
    public s_object sObject;                                //0x0000
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public int[]            Unknown;                        //0x01F4
    public short            isVisible;                      //0x0204    normal = 0x41 invis = 0x51 (bitfield) Offset 0x422 is set zero for camo to start.
    public byte             Flashlight;                     //0x0206
    public byte             Frozen;                         //0x0207
    public actionFlags      actionBits;                     //0x0208 & 0x0209
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte[]           Unknown1;                       //0x020A
    public int              UnknownCounter1;                //0x020C
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[]            UnknownLongs1;                  //0x0210
    public s_ident          PlayerOwner;                    //0x0218
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public int[]            UnknownLongs3;                  //0x021C
    public real_vector3d    RightVect;                      //0x0224
    public real_vector3d    UpVect;                         //0x0230
    public real_vector3d    LookVect;                       //0x023C
    public real_vector3d    ZeroVect;                       //0x0248
    public real_vector3d    RealLookVect;                   //0x0254
    public real_vector3d    UnknownVect3;                   //0x0260
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x34)]
    public byte[]           Unknown2;                       //0x026C
    public byte             actionVehicle_crouch_stand;     //0x02A0
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x02)]
    public byte[]           Unknown9;                       //0x02A1
    public e_action_state   animation_state;                //0x02A3
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 0x4E)]
    public byte[]           Unknown91;                      //0x02A4
    public ushort           CurWeaponIndex0;                //0x02F2    (Do not attempt to edit this, will crash Halo)
    public ushort           CurWeaponIndex1;                //0x02F4    (Read only)
    public ushort           Unknown6;                       //0x02F6
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public s_ident[]        Weapons;                        //0x02F8
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
    public uint[]           WeaponsLastUse;                 //0x0308
    public int              UnknownLongs2;                  //0x031C <-- INCORRECT OFFSET?
    public byte             grenadeIndex;                   //0x031C <-- INCORRECT OFFSET?
    public byte             grenadeIndex1;                  //0x031D
    public byte             grenade0;                       //0x031E
    public byte             grenade1;                       //0x031F
    public byte             Zoom;                           //0x0320
    public byte             Zoom1;                          //0x0321
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    public byte[]           Unknown3;                       //0x0322
    public s_ident          SlaveController;                //0x0324
    public s_ident          WeaponController;               //0x0328
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 464)]
    public byte[]           Unknown4;                       //0x032C
    public s_ident          bump_objectId;                  //0x04FC
    public byte             Unknown7;                       //0x0500
    public ushort           inAirTicks;                     //0x0501
    public byte             isWalking;                      //0x0502
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 76)]
    public byte[]           Unknown8;                       //0x0504
    public bone             LeftThigh;                      //0x0550
    public bone             RightThigh;                     //0x0584
    public bone             Pelvis;                         //0x05B8
    public bone             LeftCalf;                       //0x05EC
    public bone             RightCalf;                      //0x0620
    public bone             Spine;                          //0x0654
    public bone             LeftClavicle;                   //0x0688
    public bone             LeftFoot;                       //0x06BC
    public bone             Neck;                           //0x06F0
    public bone             RightClavicle;                  //0x0724
    public bone             RightFoot;                      //0x0758
    public bone             Head;                           //0x078C
    public bone             leftUpperArm;                   //0x07C0
    public bone             RightUpperArm;                  //0x08F4
    public bone             leftLowerArm;                   //0x0828
    public bone             RightLowerArm;                  //0x085C
    public bone             LeftHand;                       //0x0890
    public bone             RightHand;                      //0x08C4
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1216)]
    public byte[]           Unknown5;                       //0x08F8
};

//TODO: Need to put s_weapon, and s_vehicle in here before production release

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct s_map_header {
    public uint head;           //0x00 //enum 'head' string type
    public uint haloVersion;    //0x04
    public uint length;         //0x08
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
    public byte[] PADDING0;     //0x0C //Nulls
    public uint index_offset;   //0x10
    public uint metadata_size;  //0x14
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
    public byte[] PADDING1;     //0x18 //Nulls
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
    public string name;         //0x20
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
    public string build;        //0x40
    public uint type;           //0x060 // 0 = Campaign, 1 = Multi-player, 2 = Menu
    public uint unknown07;      //0x064
};
public struct s_map_header_ptr {
    public IntPtr ptr;
}

[StructLayout(LayoutKind.Sequential)]
public struct s_map_status {
    [MarshalAs(UnmanagedType.I1)]
    public bool Unknown1;      //0x00
    [MarshalAs(UnmanagedType.I1)]
    public bool Unknown2;      //0x01
    public ushort Unknown3;    //0x02 NULLs
    public uint Unknown4;      //0x04
    public uint Unknown5;      //0x08
    public uint upTime;        //0x0C 1 sec = 30 ticks <-- use this as recommended upTime
    public uint Unknown6;      //0x10
    public uint upTime1;       //0x14 1 sec = 30 ticks
    public float Unknown7;     //0x18
    public uint Unknown8;      //0x1C Don't know what this is and it's increasing rapidly...
}
public struct s_map_status_ptr {
    public IntPtr ptr;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct s_console_header {
    [MarshalAs(UnmanagedType.I1)]
    public bool gamePause;          //0x00
    [MarshalAs(UnmanagedType.I1)]
    public bool allowConsole;       //0x01
    public ushort unknown01;        //0x02 //Nulls
    [MarshalAs(UnmanagedType.I1)]
    public bool isNotConsole;       //0x04
    public byte unknown02;          //0x05
    public byte unknown03;          //0x06
    public byte keyPress;           //0x07
    public ushort unknown04;        //0x08
    public ushort unknown05;        //0x0A
    public ushort unknown06;        //0x0C
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=61)]
    public ushort[] unknown07;      //0x10 //Nulls
    public uint unknown08;          //0x88
    public uint unknown09;          //0x8C
    public uint unknown10;          //0x90
    public uint unknown11;          //0x94
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
    public string inputName;        //0x98
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=255)]
    public string input;            //0xB8
};
public struct s_console_header_ptr {
    public IntPtr ptr;
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
public struct char32 {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
    public string str;
}
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct s_ban_check {
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=9)]
    public string password;             //0x00
    public char32 cdKeyHash;            //0x12
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=40)]
    public byte[] unknown0;             //0x32
    [MarshalAs(UnmanagedType.ByValArray, SizeConst=4)]
    public byte[] unknown1;             //0x5A
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst=12)]
    public string requestPlayerName;    //0x5E
}
public struct s_ban_check_ptr {
    public IntPtr ptr;
}

//Extras for Add-on API usage.

[StructLayout(LayoutKind.Sequential)]
public struct s_cheat_header {
    [MarshalAs(UnmanagedType.I1)]
    public bool deathlessPlayer;    //0x00
    [MarshalAs(UnmanagedType.I1)]
    public bool jetPack;            //0x01
    [MarshalAs(UnmanagedType.I1)]
    public bool infiniteAmmo;       //0x02
    [MarshalAs(UnmanagedType.I1)]
    public bool bumpPossession;     //0x03
    [MarshalAs(UnmanagedType.I1)]
    public bool superJump;          //0x04
    [MarshalAs(UnmanagedType.I1)]
    public bool reflexDamage;       //0x05
    [MarshalAs(UnmanagedType.I1)]
    public bool medUsa;             //0x06
    [MarshalAs(UnmanagedType.I1)]
    public bool omnipotent;         //0x07
    [MarshalAs(UnmanagedType.I1)]
    public bool controller;         //0x08
    [MarshalAs(UnmanagedType.I1)]
    public bool bottomlessClip;     //0x09
}
public struct s_cheat_header_ptr {
    public IntPtr ptr;
}

[StructLayout(LayoutKind.Sequential)]
public struct D3DCOLOR_COLORVALUE_ARGB {
    public float a;
    public float r;
    public float g;
    public float b;
    /*public D3DCOLOR_COLORVALUE_ARGB() {
        a = 1.0f;
        r = 1.0f;
        g = 1.0f;
        b = 1.0f;
    }*/
}
public struct D3DCOLOR_COLORVALUE_ARGB_ptr {
    public IntPtr ptr;
}
#pragma warning disable 0169
#pragma warning disable 0649
public struct s_console_color_list {
    D3DCOLOR_COLORVALUE_ARGB_ptr x0_Black;         //0x00
    D3DCOLOR_COLORVALUE_ARGB_ptr x1_DodgerBlue;    //0x04
    D3DCOLOR_COLORVALUE_ARGB_ptr x2_Cyan;          //0x08
    D3DCOLOR_COLORVALUE_ARGB_ptr x3_White;         //0x0C
    D3DCOLOR_COLORVALUE_ARGB_ptr x4_Yellow;        //0x10
    D3DCOLOR_COLORVALUE_ARGB_ptr x5_Blue;          //0x14
    D3DCOLOR_COLORVALUE_ARGB_ptr x6_Coral;         //0x18    //Light Orange
    D3DCOLOR_COLORVALUE_ARGB_ptr x7_Aquamarine;    //0x1C
    D3DCOLOR_COLORVALUE_ARGB_ptr x8_Purple;        //0x20
    D3DCOLOR_COLORVALUE_ARGB_ptr x9_DarkGreen;     //0x24
    D3DCOLOR_COLORVALUE_ARGB_ptr x10_Red;          //0x28
    D3DCOLOR_COLORVALUE_ARGB_ptr x11_Indigo;       //0x2C    //Dark Purple
    D3DCOLOR_COLORVALUE_ARGB_ptr x12_Orange;       //0x30
    D3DCOLOR_COLORVALUE_ARGB_ptr x13_Gray;         //0x34
}
#pragma warning restore 0169
#pragma warning restore 0649
public struct s_console_color_list_ptr {
    public IntPtr ptr;
}
