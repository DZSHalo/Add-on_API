Imports System.Runtime.InteropServices

'Team Color Begin
Public Enum e_color_team_index As SByte
    TEAM_NONE = -1 'Reserved for H-Ext usage ONLY!
    TEAM_RED = 0
    TEAM_BLUE = 1
End Enum
'Team Color End

'Color Indexes Start
Public Enum e_color_index
    COLOR_WHITE = 0
    COLOR_BLACK     '1,
    COLOR_RED       '2
    COLOR_BLUE      '3
    COLOR_GRAY      '4
    COLOR_YELLOW    '5
    COLOR_GREEN     '6
    COLOR_PINK      '7
    COLOR_PURPLE    '8
    COLOR_CYAN      '9
    COLOR_COBALT    '10
    COLOR_ORANGE    '11
    COLOR_TEAL      '12
    COLOR_SAGE      '13
    COLOR_BROWN     '14
    COLOR_TAN       '15
    COLOR_MAROON    '16
    COLOR_SALMON    '17
End Enum
'Color Indexes End

Public Enum chatType
    [GLOBAL] = 0
    TEAM
    VEHICLE
    SERVER
End Enum

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure chatData
    Public type As UInteger     'range of 0 - 3, sort from Global, Team, Vehicle, and Server (CE only)
    Public player As UInteger   'range of 0 - 15
    Public msg_ptr As IntPtr    'range of 0 - TBA
End Structure
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
Public Structure rconData
    Public msg_ptr As IntPtr
    Public unk As UInteger ' always 0
    <MarshalAs(UnmanagedType.LPStr, SizeConst:=&H50)>
    Public msg As String

    Public Sub New(<MarshalAs(UnmanagedType.LPStr, SizeConst:=&H50)> text As String)
        msg = text
        unk = 0 'TODO: Need find a place to put Marshal.FreeBSTR function on destroy.
        msg_ptr = Marshal.StringToBSTR(msg)
    End Sub
End Structure

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
Public Structure rconDecode
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=9)>
    Public pass As String
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=65)>
    Public cmd As String
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure bone
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)>
    Public unknown As Single()
    Public World As real_vector3d
End Structure

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure s_player_reserved_slot
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=12)>
    Public PlayerName As String '0x00
    Public ColorIndex As Short '0x18      ' See defined color indexes above.
    Public Unknown1 As Short '0x1A      ' 0xFFFF
    Public MachineIndex As SByte '0x1C      ' Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
    Public Unknown2 As Byte '0x1D      'something. But, if these 4 chars are FF's, then the player isn't on.
    Public Team As SByte '0x1E
    Public PlayerIndex As SByte '0x1F      ' Index to their StaticPlayer
End Structure
Public Structure s_player_reserved_slot_ptr
    Public ptr As IntPtr
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure S_un_b
    Public s_b1 As Byte, s_b2 As Byte, s_b3 As Byte, s_b4 As Byte
End Structure
<StructLayout(LayoutKind.Sequential)>
Public Structure S_un_w
    Public s_w1 As UShort, s_w2 As UShort
End Structure
<StructLayout(LayoutKind.Explicit, Size:=4)>
Public Structure in_addr
    <FieldOffset(0)>
    Public un_b As S_un_b
    <FieldOffset(0)>
    Public un_w As S_un_w
    <FieldOffset(0)>
    Public S_addr As UInteger
End Structure
' For reference only
'#define s_addr  S_un.S_addr         ' can be used for most tcp & ip code
'#define s_host  S_un.S_un_b.s_b2    ' host on imp
'#define s_net   S_un.S_un_b.s_b1    ' network
'#define s_imp   S_un.S_un_w.s_w2    ' imp
'#define s_impno S_un.S_un_b.s_b4    ' imp #
'#define s_lh    S_un.S_un_b.s_b3    ' logical host
' 


'#Pragma warning disable 0169
'#Pragma warning disable 0649
Class MachineSP3
    Private IPAddress As in_addr
    Private port As UShort
End Class
'At the moment do not know complete size...
Class MachineSP2
    Private data3 As MachineSP3
End Class
'At the moment do not know complete size...
Public Structure MachineSP1
    Private data2 As IntPtr 'MachineSP2 data2;
End Structure
'At the moment do not know complete size...
'#Pragma warning restore 0169
'#Pragma warning restore 0649
<Flags>
Public Enum b_machine_flags As UShort
    None = 0
    Crouch = 1 << 0 '0x0024
    Jump = 1 << 1
    Flashlight = 1 << 2
    Unknownbit0 = 1 << 3
    Unknownbit1 = 1 << 4
    Unknownbit2 = 1 << 5
    Unknownbit3 = 1 << 6
    Unknownbit4 = 1 << 7
    Reload = 1 << 8 '0x0025
    Fire = 1 << 9
    Swap = 1 << 10
    Grenade = 1 << 11
    Unknownbit5 = 1 << 12
    Unknownbit6 = 1 << 13
    Unknownbit7 = 1 << 14
    Unknownbit8 = 1 << 15
End Enum
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
Public Structure s_machine_slot
    Public data1 As IntPtr '0x0000 ' Following this, i found a pointer next to other useless stuff. Then, another pointer, then i found some stuff that looked like it /MIGHT/ very well be strings related to a hash. *shrug*
    Public Unknown0 As Integer '0x0004
    Public Unknown1 As Integer '0x0008
    Public machineIndex As Short '0x000C
    Public Unknown9 As Short '0x000E ' First is 1, then 3, then 7 and back to 0 if not in used (1 is found during the CD Hash Check, 7 if currently playing the game)
    Public isAvailable As Integer '0x0010
    Public Unknown10 As Integer '0x0014
    Public Unknown11 As Integer '0x0018
    Public Unknown12 As Integer '0x001C    ' most of the time 1, but sometimes changes to 2 for a moment.
    Public Unknown13 As Integer '0x0020
    Public flags As b_machine_flags '0x0024 & 0x0025
    Public Unknown14 As Short '0x0026
    Public Yaw As Single '0x0028 ' player's rotation - in radians, from 0 to 2*pi, (AKA heading)
    Public Pitch As Single '0x002C ' Player's pitch - in radians, from -pi/2 to +pi/2, down to up.
    Public Roll As Single '0x0030 ' roll - unless walk-on-walls is enabled, this will always be 0.
    Public Unknown15 As Integer '0x0034
    Public Unknown16 As Integer '0x0038
    Public ForwardVelocityMultiplier As Single '0x003C
    Public HorizontalVelocityMultiplier As Single '0x0040
    Public ROFVelocityMultiplier As Single '0x0044
    Public WeaponIndex As Short '0x0048
    Public GrenadeIndex As Short '0x004A
    Public UnknownIndex As Short '0x004C ' The index is -1 if no choices are even available.
    Public Unknown2 As Short '0x004E
    Public Unknown3 As Short '0x0050 ' 1
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=8)>
    Public SessionKey As String '0x0052 ' This is used to accept the incoming player PLUS validate with gamespy server.
    Public Unknown4 As Short '0x005A
    Public UniqueID As Integer '0x005C ' increase every time a player join (notice: it is not focus on specific machine struct, it applies to all.)
End Structure
Public Structure s_machine_slot_ptr
    Public ptr As IntPtr
End Structure
<Flags>
Public Enum b_player_flags As UShort
    None = 0
    Melee = 1 << 0
    Action = 1 << 1
    UnknownBit = 1 << 2
    Flashlight = 1 << 3
    UnknownBit1 = 1 << 4
    UnknownBit4 = 1 << 5
    UnknownBit5 = 1 << 6
    UnknownBit6 = 1 << 7
    UnknownBit2 = 1 << 8
    UnknownBit7 = 1 << 9
    UnknownBit8 = 1 << 10
    UnknownBit9 = 1 << 11
    UnknownBit10 = 1 << 12
    Reload = 1 << 13
    UnknownBit3 = 1 << 14
    UnknownBit11 = 1 << 15
End Enum
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure s_player_slot
    Public PlayerID As Short '0x000
    Public IsLocal As Short '0x002            ' 0=Local(no bits set), -1=Other Client(All bits set)
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=12)>
    Public Name As String '0x004            ' Unicode
    Public UnknownIdent As s_ident '0x01C
    Public Team As Integer '0x020            ' 0=Red, 1=Blue; if do client host, you will see hud's team changed instant.
    Public SwapObject As s_ident '0x024
    Public SwapType As Short '0x028            ' Vehicle=8, Weapon=6
    Public SwapSeat As Short '0x02A            ' Warthog-Driver=0, Passenger=1, Gunner=2, Weapon=-1
    Public RespawnTimer As Integer '0x02C            ' Counts down when dead, Alive=0
    Public Unknown As Integer '0x02F
    Public CurrentBiped As s_ident '0x034
    Public PreviousBiped As s_ident '0x038 'IMPORTANT: need to verify this. '   uint LocationID;                  ' This is very, very interesting. BG is split into 25 location ID's. 1 - 19
    Public LocationID As Short '0x03C            'Should be uint? 'short                   ClusterIndex;                   '0x03C 'public byte             Swap : 1;                       '0x03E.0 '    public byte             UnknownBits4 :7;                '0x03E.7
    Public UnknownByte1 As Byte '0x03E
    Public UnknownByte As Byte '0x03F
    Public UnknownIdent1 As s_ident '0x040            'BulletCount?
    Public LastBulletShotTime As Integer '0x044            ' since game start(0)
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=12)>
    Public Name1 As String '0x048
    Public ColorIndex As Short '0x060            ' See defined color indexes above.
    Public Unknown00 As Short '0x062
    Public MachineIndex As Byte '0x064            ' Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
    Public Unknown0 As Byte '0x065            'something. But, if these 4 chars are FF's, then the player isn't on.
    Public iTeam As Byte '0x066
    Public PlayerIndex As Byte '0x067            ' Index to their StaticPlayer
    Public Unknown1 As Integer '0x068
    Public VelocityMultiplier As Single '0x06C <--- and below are correct!
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)>
    Public UnknownIdent3 As s_ident() '0x070
    Public Unknown2 As Integer '0x080
    Public LastDeathTime As Integer '0x084        ' since game start(0)
    Public killInOrderObjective As s_ident '0x088
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public Unknown3 As Byte() '0x08C
    Public KillsCount As Short '0x09C
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)>
    Public Unknown4 As Byte() '0x09E
    Public AssistsCount As Short '0x0A4
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)>
    Public Unknown5 As Byte() '0x0A6
    Public BetrayedCount As Short '0x0AC
    Public DeathsCount As Short '0x0AE
    Public SuicideCount As Short '0x0B0
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=18)>
    Public Unknown6 As Byte() '0x0B2
    Public FlagStealCount As Short '0x0C4
    Public FlagReturnCount As Short '0x0C6
    Public FlagCaptureCount As Short '0x0C8
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)>
    Public Unknown7 As Byte() '0x0CA
    Public UnknownIdent4 As s_ident '0x0D0
    Public Unknown8 As Byte '0x0D4
    <MarshalAs(UnmanagedType.U1)>
    Public HasQuit As Boolean '0x0D5
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=6)>
    Public Unknown81 As Byte() '0x0D6
    Public Ping As Short '0x0DC
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=14)>
    Public Unknown9 As Byte() '0x0DE
    Public UnknownIdent5 As s_ident '0x0EC
    Public Unknown10 As Integer '0x0F0
    Public SomeTime As Integer '0x0F4
    Public World As real_vector3d '0x0F8
    Public UnknownIdent6 As s_ident '0x104
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=20)>
    Public Unknown11 As Byte() '0x108
    Public flags As b_player_flags
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=26)>
    Public Unknown12 As Byte() '0x11E
    Public Rotation As real_vector2d '0x138        ' Yaw, Pitch (again, in radians.
    Public ForwardVelocityMultiplier As Single '0x140
    Public HorizontalVelocityMultiplier As Single '0x144
    Public RateOfFireVelocityMultiplier As Single '0x148
    Public HeldWeaponIndex As Short '0x14C
    Public GrenadeIndex As Short '0x14E
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)>
    Public Unknown13 As Byte() '0x150
    Public LookVect As real_vector3d '0x154
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public Unknown14 As Byte() '0x160
    Public WorldDelayed As real_vector3d '0x170    ' Oddly enough... it matches the world vect, but seems to lag behind (Possibly what the client reports is _its_ world coord?)
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=132)>
    Public Unknown15 As Byte() '0x17C
End Structure
Public Structure s_player_slot_ptr
    Public ptr As IntPtr
End Structure

Public Enum GAMETYPE
    CTF = 1
    SLAYER = 2
    ODDBALL = 3
    KOTH = 4
    RACE = 5
End Enum

<Flags>
Public Enum b_gametype_flags As Byte
    None = 0
    showEnemyRadar = 1 << 0 '0x0024
    friendlyIndicator = 1 << 1
    infiniteGrenade = 1 << 2
    noShield = 1 << 3
    invisible = 1 << 4
    isCustomEquipment = 1 << 5
    showFriendlyRadar = 1 << 6
    unknown = 1 << 7
End Enum

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure s_gametype 'UNDONE GameType Struct is not 100% decoded.
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=24)>
    Public name As String ' 0x00
    Public game_stage As UInteger ' 0x30 1=CTF, 2=Slayer, 3=Oddball, 4=KOTH, 5=Race
    <MarshalAs(UnmanagedType.I1)>
    Public isTeamPlay As Boolean ' 0x34
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)>
    Public NULL0 As Byte() ' 0x35
    Public flags As b_gametype_flags
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=3)>
    Public Unknown0 As Byte() ' 0x39        'TODO need to find out what these are and 0x38 as well.
    Public objective_indicator As UInteger ' 0x3C 0=Motion Tracker, 1=Navpoints, 2=None
    Public is_odd_man_out As UInteger ' 0x40
    Public respawn_time_growth As UInteger ' 0x44
    Public respawn_time As UInteger ' 0x48
    Public respawn_suicide_penalty As UInteger ' 0x4C
    Public limit_lives As UInteger ' 0x50
    Public health As Single ' 0x54
    Public score_limit As UInteger ' 0x58
    Public weapon_type As UInteger ' 0x5C 0 = default, 1 = pistols, 2 = rifles, 3 = plasma rifles, 4 = sniper, 5 = no sniper, 6 = rocket launchers, 7 = shotguns, 8 = short range, 9 = human, 10 = covenant, 11 = classic, 12 = heavy weapons 'Vehicles section
    Public vehicle_red As UInteger ' 0x60 Need some work here.
    Public vehicle_blue As UInteger ' 0x64 Need some work here.
    Public vehicle_respawn_time As UInteger ' 0x68 ticks
    Public is_friendly_fire As UInteger ' 0x6C
    Public respawn_betrayal_penalty As UInteger ' 0x70
    Public is_team_balance As UInteger ' 0x74
    Public time_limit As UInteger ' 0x78 ' ball gametype data
    Public MovingHill As UInteger ' 0x7C (KOTH) 0=off, 1=on; (Race) 0=Normal, 1=any order, 2=Rally; (Oddball) 0=off, 1=on;
    Public TeamScoring As Byte ' 0x80 (Race) 0=minimal, 1=maximum, 2=Sum; (Oddball) 0=Slow, 1=Normal, 2=Fast
    Public Unknown2 As Short ' 0x81
    Public TraitBallWith As Byte ' 0x83 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
    Public Unknown3 As UInteger ' 0x84
    Public TraitBallWithout As UInteger ' 0x88 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=14)>
    Public Unknown4 As Byte() ' 0x8C - 0x99 unknown
    <MarshalAs(UnmanagedType.I1)>
    Public Unknown5 As Boolean ' 0x9A
    <MarshalAs(UnmanagedType.I1)>
    Public Unknown6 As Boolean ' 0x9B
    <MarshalAs(UnmanagedType.I1)>
    Public noDeathBonus As Boolean ' 0x9C
    <MarshalAs(UnmanagedType.I1)>
    Public noKillPenalty As Boolean ' 0x9D
    <MarshalAs(UnmanagedType.I1)>
    Public isKillInOrder_flagMustReset As Boolean ' 0x9E    isKillInOrder = Slayer, FlagMustReset = CTF    'CTF and Slayer is sharing this... must be union structure?
    <MarshalAs(UnmanagedType.I1)>
    Public isFlagAtHomeToScore As Boolean ' 0x9F    CTF usage
    Public SingleFlagTimer As UInteger ' 0xA0 CTF usage, 0 = off, 1800 = 1 min, and so on.
End Structure
Public Structure s_gametype_ptr
    Public ptr As IntPtr
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure s_gametype_gflag
    Public World As real_vector3d '0x00 Coordinate of where to respawn at.
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)>
    Public UNKNOWN0 As Byte() '0x0C Nulls...
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)>
    Public UNKNOWN1 As Byte() '0x14 Don't know...
    Public unknown As Single '0x18 Possible float?
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)>
    Public UNKNOWN2 As Byte() '0x1C Nulls...
    Public unknown2_ As UInteger '0x24 Always -1
End Structure
Public Structure s_gametype_gflag_ptr
    Public ptr As IntPtr
End Structure

'#Pragma warning disable 0169
'#Pragma warning disable 0649
Public Structure [boolean]
    <MarshalAs(UnmanagedType.I1)>
    Private boolNative As Boolean
End Structure
'#Pragma warning restore 0169
'#Pragma warning restore 0649
Public Structure s_gametype_CTFg 'size = 0x38
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public flagParams As s_gametype_gflag_ptr() '0x0000        ' 0 = Red flag, 1 = Blue flag
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public teamFlagIds As s_ident() '0x0008
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public teamScore As UInteger() '0x0010        '0 = Red team, 1 = Blue team
    Public scoreLimit As UInteger '0x0018        'Not sure what size this is atm.
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public flagCapture As [boolean]() '0x001C        ' 0 = Red flag, 1 = Blue flag
    Public Unknown1 As UShort '0x001E 'Trial version end for CTF
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public flagNotAtBase As UInteger() '0x0020        'Timer in ticks; 0 = Red flag, 1 = Blue flag
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H10)>
    Public UNKNOWN As Byte() '0x0028        'Missing some other informations, need to edit more here.
End Structure
Public Structure s_gametype_KOTHg 'size = 0x288
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public hillScoreTicks As UInteger() '0x0000        'Scoring in ticks base on player id                                                '0x0038
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public hillScoreTicksRelativeMapTicks As UInteger() '0x0040        'Tick difference from how int in map ticks and is in hill base on player id.      '0x0078
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public isInHill As [boolean]() '0x0080        'Player is in hill, base on player id                                              '0x00B8
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H100)>
    Public UNKNOWN0 As Byte() '0x0090        'Don't know what this is and it's relative to hill being moved around if set.      '0x00C8
    Public Unknown2 As UInteger '0x0190        'Goes up when someone entered                                                      '0x01C8
    Public Unknown3 As UInteger '0x0194        'Tick goes up when someone is in the hill.                                         '0x01CC
    Public player As s_ident '0x0198        'Shows the player id.                                                              '0x01D0
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&HEC)>
    Public UNKNOWN1 As Byte() '0x019C        'Dunno what the rest are.                                                          '0x01D4
End Structure
Public Structure s_gametype_ODDBALLg 'size = 0x148
    Public scoreLimit As UInteger '0x0000        'Total ticks to win.                                                               '0x02C0
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public scoreTicks As UInteger() '0x0004                                                                                            '0x02C4
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public scoreTicks2 As UInteger() '0x0044        'Always the same with above.                                                       '0x0304
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public Unknown4 As UInteger() '0x0084        'Wizzard commented this is for juggernut...                                        '0x0344
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public holder As s_ident() '0x00C4        'This is base on oddball #, not player. Also using player id                       '0x0384
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public relocateTicks As UInteger() '0x0104        'This is base on oddball #, not player. Also using player id                       '0x03C4
    Public Unknown5 As UInteger '0x0144        'Null                                                                              '0x0404
End Structure
Public Structure s_gametype_RACEg 'size = 0x148
    Public checkpointTotal As UInteger '0x0000        'Total navpoints around the map to score per lap.                                  '0x0408
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public Unknown6 As UInteger() '0x0004        'Nulls                                                                             '0x040C
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public checkpointCurrent As UInteger() '0x0044        'Counting to checkpointTotal                                                       '0x044C
    Public Unknown7 As UInteger '0x0084                                                                                            '0x048C
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public raceLaps As UInteger() '0x0088        'Total laps completed                                                              '0x0490
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public Unknown8 As UInteger() '0x00C8        'So far just nulls...                                                              '0x04D0
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public Unknown9 As UInteger() '0x0108        'Don't know what these are and not relative to players.                            '0x0510
End Structure
Public Structure s_gametype_SLAYERg 'size = 0x80
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public playerScore As UInteger() '0x0000                                                                                            '0x0550
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=16)>
    Public playerScore2 As UInteger() '0x0040        'Duplicated and appear useless...                                                  '0x0590
End Structure
'#Pragma warning disable 0169
'#Pragma warning disable 0649
Public Structure s_gametype_globals
    Public ctfGlobal As s_gametype_CTFg
    Public kothGlobal As s_gametype_KOTHg
    Public oddballGlobal As s_gametype_ODDBALLg
    Public raceGlobal As s_gametype_RACEg
    Public slayerGlobal As s_gametype_SLAYERg
End Structure
'#Pragma warning restore 0169
'#Pragma warning restore 0649
Public Structure s_gametype_globals_ptr
    Public ptr As IntPtr
End Structure

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
Public Structure map_name_ansi
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
    Public name As String
End Structure
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure s_server_header
    Public Unknown0 As IntPtr           '0x000 ' at least I _think_ it's a pointer since there _is_ something if i follow it.
    Public state As UShort              '0x004
    Public Unknown2 As UShort           '0x006
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=66)>
    Public server_name As String        '0x008
    Public map_name As map_name_ansi    '0x08C
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=96)>
    Public Unknown12 As Byte            '0x0AC
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=24)>
    Public gametype_name As String      '0x10C
    'IMPORTANT: DO NOT USE! Below this does Not match with other Halo PC platforms, it Is base on Halo CE version.
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=40)>
    Public Unknown11 As Byte()          '0x13C ' partial of Gametype need to break them down.
    Public score_max As Byte            '0x164
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=128)>
    Public Unknown3 As Byte()           '0x165
    Public player_max As Byte           '0x1E5    ' Note: there is another place that also says MaxPlayers - i think it's the ServerInfo socket buffer.
    Public Unknown09 As Short           '0x1E6
    Public totalPlayers As UShort       '0x1E8
    Public Unknown10 As Short           '0x1EA    ' i think LastSlotFilled
End Structure
Public Structure s_server_header_ptr
    Public ptr As IntPtr
End Structure

Public Enum e_action_state As Byte
    IDLE = 0
    GESTURE
    TURN_LEFT
    TURN_RIGHT
    MOVE_FRONT
    MOVE_BACK
    MOVE_LEFT
    MOVE_RIGHT
    STUNNED_FRONT
    STUNNED_BACK
    STUNNED_LEFT
    STUNNED_RIGHT
    SLIDE_FRONT
    SLIDE_BACK
    SLIDE_LEFT
    SLIDE_RIGHT
    READY
    PUT_AWAY
    AIM_STILL
    AIM_MOVE
    AIRBORNE
    LAND_SOFT
    LAND_HARD
    UNKNOWN0
    AIRBORNE_DEAD
    LAND_DEAD
    SEAT_ENTER
    SEAT_EXIT
    CUSTOM_ANIMATION
    IMPULSE
    MELEE
    MELEE_AIRBORNE
    MELEE_CONTINUOUS
    GRENADE_TOSS
    RESURRECT_FRONT
    RESURRECT_BACK
    FEEDING
    SURPRISE_FRONT
    SURPRISE_BACK
    LEAP_START
    LEAP_AIRBORNE
    LEAP_MELEE
    UNUSED_AFAICT
    BERSERK
End Enum

<Flags>
Public Enum s_object_flags As Byte
    NONE = 0
    unkBits_1 = 1 << 0
    unkBits_2 = 1 << 1
    ignoreGravity = 1 << 2
    unk1_1 = 1 << 3
    unk1_2 = 1 << 4
    unk1_3 = 1 << 5
    unk2 = 1 << 6
    noCollision = 1 << 7
End Enum

<Flags>
Public Enum damageFlags As UShort
    NONE = 0
    unknown1_1 = 1 << 0
    unknown1_2 = 1 << 1
    kill1 = 1 << 2 '0.2
    unknown2 = 1 << 3
    kill2 = 1 << 4 '0.4
    unknown3_1 = 1 << 5 '0.5-0.9
    unknown3_2 = 1 << 6
    unknown3_3 = 1 << 7
    unknown4_1 = 1 << 8 '1.0-1.2
    unknown4_2 = 1 << 9
    unknown4_3 = 1 << 10
    cannotTakeDamage = 1 << 11
    unknown5_1 = 1 << 12 '1.4 - 1.9
    unknown5_2 = 1 << 13
    unknown5_3 = 1 << 14
    unknown5_4 = 1 << 15
End Enum

<StructLayout(LayoutKind.Sequential)>
Public Structure s_object
    Public ModelTag As s_ident                      ' 0x0000
    Public Zero As Integer                          ' 0x0004
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=4)>
    Public Flags As Byte()                          ' 0x0008
    Public Timer As Integer                         ' 0x000C
    '<MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=4)>
    'Public Flags2 As Byte()                         ' 0x0010
    Public Flags1 As s_object_flags                 ' 0x0010
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=3)>
    Public unkBytes1 As Byte()                      ' 0x0011
    Public Timer2 As Integer                        ' 0x0014
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=17)>
    Public Zero2 As Integer()                       ' 0x0018
    Public World As real_vector3d                   ' 0x005C
    Public Velocity As real_vector3d                ' 0x0068
    Public Rotation As real_vector3d                ' 0x0074
    Public Scale As real_vector3d                   ' 0x0080
    Public VelocityPitchYawRoll As real_vector3d    ' 0x008C  'current velocity for pitch, yaw, and roll
    Public LocationID As Integer                    ' 0x0098
    Public Unknown1 As Integer                      ' 0x009C
    Public UnknownVector2d As real_vector3d         ' 0x00A0
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Unknown2 As Single()                     ' 0x00AC
    Public objType As Short                         ' 0x00B4
    Public Unknown3 As Short                        ' 0x00B6
    Public GameObject As Short                      ' 0x00B8    ' 0 >= is game object, -1 = is NOT game object
    Public Unknown4 As Short                        ' 0x00BA
    Public Unknown5 As Integer                      ' 0x00BD
    Public Player As s_ident                        ' 0x00C0
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Unknown6 As Integer()                    ' 0x00C4
    Public AntrMeta As s_ident                      ' 0x00CC
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Unknown7 As Integer()                    ' 0x00D0
    Public HealthMax As Single                      ' 0x00D8
    Public ShieldMax As Single                      ' 0x00DC
    Public Health As Single                         ' 0x00E0
    Public Shield1 As Single                        ' 0x00E4
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=7)>
    Public Unknown8 As Integer()                    ' 0x00E8
    Public Unknown9 As Short                        ' 0x0104
    Public damageFlag As damageFlags                ' 0x0106
    Public Unknown10 As Short                       ' 0x0108
    Public Unknown11 As Short                       ' 0x010A
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Unknown12 As Integer()                   ' 0x010C
    Public VehicleWeapon As s_ident                 ' 0x0114
    Public Weapon As s_ident                        ' 0x0118
    Public Vehicle As s_ident                       ' 0x011C
    Public SeatType As Short                        ' 0x0120
    Public Unknown13 As Short                       ' 0x0122
    Public Unknown14 As Integer                     ' 0x0124
    Public Shield2 As Single                        ' 0x0128
    Public Flashlight1 As Single                    ' 0x012C
    Public Unknown15 As Single                      ' 0x0130
    Public Flashlight2 As Single                    ' 0x0134
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=5)>
    Public Unknown16 As Integer()                   ' 0x0138
    Public UnknownIdent1 As s_ident                 ' 0x014C
    Public UnknownIdent2 As s_ident                 ' 0x0150
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=6)>
    Public Zero3 As Integer()                       ' 0x0154
    Public UnknownIdent3 As s_ident                 ' 0x016C
    Public UnknownIdent4 As s_ident                 ' 0x0170
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=16)>
    Public UnknownMatrix0 As Integer()              'D3DXMATRIX UnknownMatrix;     ' 0x0174
    <MarshalAsAttribute(UnmanagedType.ByValArray, SizeConst:=16)>
    Public UnknownMatrix1 As Integer()              'D3DXMATRIX UnknownMatrix1;    ' 0x01B4 'Everything after this is 0x01F4
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure s_object_ptr
    Public ptr As IntPtr
End Structure

<Flags>
Public Enum actionFlags As UShort
    NONE = 0
    crouching = 1 << 0              ' 0 (a few Of these bit flags are thanks To halo devkit)
    jumping = 1 << 1                ' 1
    unknown0 = 1 << 2               ' 2
    unknown1 = 1 << 3               ' 3
    Flashlight = 1 << 4             ' 4
    unknown2 = 1 << 5               ' 5
    actionPress = 1 << 6            ' 6 think this Is just When they initially press the action button
    melee = 1 << 7                  ' 7
    unknown3 = 1 << 8               ' 8
    unknown4 = 1 << 9               ' 9
    reload = 1 << 10                ' 10
    primaryWeaponFire = 1 << 11     ' 11 right mouse
    secondaryWeaponFire = 1 << 12   ' 12 left mouse
    secondaryWeaponFire1 = 1 << 13  ' 13
    actionHold = 1 << 14            ' 14 holding action button
    UnknownBit4 = 1 << 15           ' 15
End Enum


<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
Public Structure s_biped
    Public sObject As s_object                  '0x0000
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)>
    Public Unknown As Integer()                 '0x01F4
    Public isVisible As Short                   '0x0204    normal = 0x41 invis = 0x51 (bitfield) Offset 0x422 is set zero for camo to start.
    Public Flashlight As Byte                   '0x0206
    Public Frozen As Byte                       '0x0207
    Public actionBits As actionFlags            '0x0208 & 0x0209
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Unknown1 As Byte()                   '0x020A
    Public UnknownCounter1 As Integer           '0x020C
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public UnknownLongs1 As Integer()           '0x0210
    Public PlayerOwner As s_ident               '0x0218
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public UnknownLongs3 As Integer()           '0x021C
    Public RightVect As real_vector3d           '0x0224
    Public UpVect As real_vector3d              '0x0230
    Public LookVect As real_vector3d            '0x023C
    Public ZeroVect As real_vector3d            '0x0248
    Public RealLookVect As real_vector3d        '0x0254
    Public UnknownVect3 As real_vector3d        '0x0260
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H34)>
    Public Unknown2 As Byte()                   '0x026C
    Public actionVehicle_crouch_stand As Byte   '0x02A0
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H2)>
    Public Unknown9 As Byte()                   '0x02A1
    Public animation_state As e_action_state    '0x02A3
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=&H4C)>
    Public Unknown91 As Byte()                  '0x02A4
    Public vehicle_seat_index As Short          '0x02F0
    Public CurWeaponIndex0 As UShort            '0x02F2    (Do not attempt to edit this, will crash Halo)
    Public CurWeaponIndex1 As UShort            '0x02F4    (Read only)
    Public Unknown6 As UShort                   '0x02F6
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)>
    Public Weapons As s_ident()                 '0x02F8
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)>
    Public WeaponsLastUse As UInteger()         '0x0308
    Public UnknownLongs2 As Integer             '0x0318
    Public grenadeIndex As Byte                 '0x031C
    Public grenadeIndex1 As Byte                '0x031D
    Public grenade0 As Byte                     '0x031E
    Public grenade1 As Byte                     '0x031F
    Public Zoom As Byte                         '0x0320
    Public Zoom1 As Byte                        '0x0321
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=2)>
    Public Unknown3 As Byte()                   '0x0322
    Public SlaveController As s_ident           '0x0324
    Public WeaponController As s_ident          '0x0328
    Public vehicle_eject_last As s_ident        '0x032C
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=460)>
    Public Unknown4 As Byte()                   '0x0330
    Public bump_objectId As s_ident             '0x04FC
    Public Unknown7 As Byte                     '0x0500
    Public inAirTicks As UShort                 '0x0501
    Public isWalking As Byte                    '0x0502
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=76)>
    Public Unknown8 As Byte()                   '0x0504
    Public LeftThigh As bone                    '0x0550
    Public RightThigh As bone                   '0x0584
    Public Pelvis As bone                       '0x05B8
    Public LeftCalf As bone                     '0x05EC
    Public RightCalf As bone                    '0x0620
    Public Spine As bone                        '0x0654
    Public LeftClavicle As bone                 '0x0688
    Public LeftFoot As bone                     '0x06BC
    Public Neck As bone                         '0x06F0
    Public RightClavicle As bone                '0x0724
    Public RightFoot As bone                    '0x0758
    Public Head As bone                         '0x078C
    Public leftUpperArm As bone                 '0x07C0
    Public RightUpperArm As bone                '0x08F4
    Public leftLowerArm As bone                 '0x0828
    Public RightLowerArm As bone                '0x085C
    Public LeftHand As bone                     '0x0890
    Public RightHand As bone                    '0x08C4
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=1216)>
    Public Unknown5 As Byte()                   '0x08F8
End Structure
'TODO: Need to put s_weapon, and s_vehicle in here before production release

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
Public Structure s_map_header
    Public head As UInteger             '0x00 'enum 'head' string type
    Public haloVersion As UInteger      '0x04
    Public length As UInteger           '0x08
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)>
    Public PADDING0 As Byte()           '0x0C 'Nulls
    Public index_offset As UInteger     '0x10
    Public metadata_size As UInteger    '0x14
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=8)>
    Public PADDING1 As Byte()           '0x18 'Nulls
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
    Public name As String               '0x20
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
    Public build As String              '0x40
    Public type As UInteger             '0x060 ' 0 = Campaign, 1 = Multi-player, 2 = Menu
    Public unknown07 As UInteger        '0x064
End Structure
Public Structure s_map_header_ptr
    Public ptr As IntPtr
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure s_map_status
    <MarshalAs(UnmanagedType.I1)>
    Public Unknown1 As Boolean  '0x00
    <MarshalAs(UnmanagedType.I1)>
    Public Unknown2 As Boolean  '0x01
    Public Unknown3 As UShort   '0x02 NULLs
    Public Unknown4 As UInteger '0x04
    Public Unknown5 As UInteger '0x08
    Public upTime As UInteger   '0x0C 1 sec = 30 ticks <-- use this as recommended upTime
    Public Unknown6 As UInteger '0x10
    Public upTime1 As UInteger  '0x14 1 sec = 30 ticks
    Public Unknown7 As Single   '0x18
    Public Unknown8 As UInteger '0x1C Don't know what this is and it's increasing rapidly...
End Structure
Public Structure s_map_status_ptr
    Public ptr As IntPtr
End Structure

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
Public Structure s_console_header
    <MarshalAs(UnmanagedType.I1)>
    Public gamePause As Boolean     '0x00
    <MarshalAs(UnmanagedType.I1)>
    Public allowConsole As Boolean  '0x01
    Public unknown01 As UShort      '0x02 'Nulls
    <MarshalAs(UnmanagedType.I1)>
    Public isNotConsole As Boolean  '0x04
    Public unknown02 As Byte        '0x05
    Public unknown03 As Byte        '0x06
    Public keyPress As Byte         '0x07
    Public unknown04 As UShort      '0x08
    Public unknown05 As UShort      '0x0A
    Public unknown06 As UShort      '0x0C
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=61)>
    Public unknown07 As UShort()    '0x10 'Nulls
    Public unknown08 As UInteger    '0x88
    Public unknown09 As UInteger    '0x8C
    Public unknown10 As UInteger    '0x90
    Public unknown11 As UInteger    '0x94
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)>
    Public inputName As String      '0x98
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=255)>
    Public input As String          '0xB8
End Structure
Public Structure s_console_header_ptr
    Public ptr As IntPtr
End Structure

<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Ansi)>
Public Structure char32
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=32)>
    Public str As Byte()
End Structure
<StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
Public Structure s_ban_check
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=9)>
    Public password As String           '0x00
    Public cdKeyHash As char32          '0x12
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=40)>
    Public unknown0 As Byte()           '0x32
    <MarshalAs(UnmanagedType.ByValArray, SizeConst:=4)>
    Public unknown1 As Byte()           '0x5A
    <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=12)>
    Public requestPlayerName As String  '0x5E
End Structure
Public Structure s_ban_check_ptr
    Public ptr As IntPtr
End Structure

'Extras for Add-on API usage.

<StructLayout(LayoutKind.Sequential)>
Public Structure s_cheat_header
    <MarshalAs(UnmanagedType.I1)>
    Public deathlessPlayer As Boolean   '0x00
    <MarshalAs(UnmanagedType.I1)>
    Public jetPack As Boolean           '0x01
    <MarshalAs(UnmanagedType.I1)>
    Public infiniteAmmo As Boolean      '0x02
    <MarshalAs(UnmanagedType.I1)>
    Public bumpPossession As Boolean    '0x03
    <MarshalAs(UnmanagedType.I1)>
    Public superJump As Boolean         '0x04
    <MarshalAs(UnmanagedType.I1)>
    Public reflexDamage As Boolean      '0x05
    <MarshalAs(UnmanagedType.I1)>
    Public medUsa As Boolean            '0x06
    <MarshalAs(UnmanagedType.I1)>
    Public omnipotent As Boolean        '0x07
    <MarshalAs(UnmanagedType.I1)>
    Public controller As Boolean        '0x08
    <MarshalAs(UnmanagedType.I1)>
    Public bottomlessClip As Boolean    '0x09
End Structure
Public Structure s_cheat_header_ptr
    Public ptr As IntPtr
End Structure

<StructLayout(LayoutKind.Sequential)>
Public Structure D3DCOLOR_COLORVALUE_ARGB
    Public a As Single
    Public r As Single
    Public g As Single
    Public b As Single 'public D3DCOLOR_COLORVALUE_ARGB() { '        a = 1.0f; '        r = 1.0f; '        g = 1.0f; '        b = 1.0f; '    }

End Structure
Public Structure D3DCOLOR_COLORVALUE_ARGB_ptr
    Public ptr As IntPtr
End Structure
'#Pragma warning disable 0169
'#Pragma warning disable 0649
Public Structure s_console_color_list
    Private x0_Black As D3DCOLOR_COLORVALUE_ARGB_ptr        '0x00
    Private x1_DodgerBlue As D3DCOLOR_COLORVALUE_ARGB_ptr   '0x04
    Private x2_Cyan As D3DCOLOR_COLORVALUE_ARGB_ptr         '0x08
    Private x3_White As D3DCOLOR_COLORVALUE_ARGB_ptr        '0x0C
    Private x4_Yellow As D3DCOLOR_COLORVALUE_ARGB_ptr       '0x10
    Private x5_Blue As D3DCOLOR_COLORVALUE_ARGB_ptr         '0x14
    Private x6_Coral As D3DCOLOR_COLORVALUE_ARGB_ptr        '0x18    'Light Orange
    Private x7_Aquamarine As D3DCOLOR_COLORVALUE_ARGB_ptr   '0x1C
    Private x8_Purple As D3DCOLOR_COLORVALUE_ARGB_ptr       '0x20
    Private x9_DarkGreen As D3DCOLOR_COLORVALUE_ARGB_ptr    '0x24
    Private x10_Red As D3DCOLOR_COLORVALUE_ARGB_ptr         '0x28
    Private x11_Indigo As D3DCOLOR_COLORVALUE_ARGB_ptr      '0x2C    'Dark Purple
    Private x12_Orange As D3DCOLOR_COLORVALUE_ARGB_ptr      '0x30
    Private x13_Gray As D3DCOLOR_COLORVALUE_ARGB_ptr        '0x34
End Structure
'#Pragma warning restore 0169
'#Pragma warning restore 0649
Public Structure s_console_color_list_ptr
    Public ptr As IntPtr
End Structure
