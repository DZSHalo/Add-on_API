#ifndef structH
#define structH

#define toggle char
#define playerindex char
#define machineindex char

//Team Color Begin
#define TEAM_RED 0
#define TEAM_BLUE 1
//Team Color End

//Color Indexes Start
#define COLOR_WHITE		0
#define COLOR_BLACK		1
#define COLOR_RED		2
#define COLOR_BLUE		3
#define COLOR_GRAY		4
#define	COLOR_YELLOW	5
#define COLOR_GREEN		6
#define COLOR_PINK		7
#define COLOR_PURPLE	8
#define COLOR_CYAN		9
#define COLOR_COBALT	10
#define COLOR_ORANGE	11
#define COLOR_TEAL		12
#define COLOR_SAGE		13
#define COLOR_BROWN		14
#define COLOR_TAN		15
#define COLOR_MAROON	16
#define COLOR_SALMON	17
//Color Indexes End

#pragma pack(push,1)
struct chatData	{
		DWORD type;		//range of 0 - 3, sort from Global, Team, Vehicle, and Server (CE only)
		DWORD player;	//range of 0 - 15
		wchar_t* msg;	//range of 0 - TBA
};
struct rconData {
	char* msg_ptr;
	DWORD unk; // always 0
	char msg[0x50];
	rconData(const char text[]) {
		memset(msg, 0, 0x50);
		strcpy_s(msg, 0x50, text);
		unk = 0;
		msg_ptr = msg;
	}
};
static_assert_check(sizeof(rconData) == 0x58, "Incorrect size of rconData");
struct rconDecode {
	char pass[9];
	char cmd[65];
};
static_assert_check(sizeof(rconDecode) == 0x4A, "Incorrect size of rconDecode");

struct vect3 {
	float x;
	float y;
	float z;
	vect3() {
		this->x = this->y = this->z = (float)INFINITE;
	}
	vect3(float x, float y, float z) {
		this->x = x;
		this->y = y;
		this->z = z;
	}
	bool operator == (vect3 &v3) {
		if (v3.x == this->x && v3.y == this->y && v3.z == this->z)
			return 1;
		return 0;
	}
	bool operator != (vect3 &v3) {
		return !this->operator==(v3);
	}
};
struct vect2 {
	float x;
	float y;
	vect2(float x, float y) {
		this->x = x;
		this->y = y;
	}
};
union ident {
	int Tag; 
      struct {
         short index;
         short ID;
      };
};
static_assert_check(sizeof(ident) == 0x4, "Incorrect size of ident");
struct bone {
	float unknown[10];
	vect3 World;
};
static_assert_check(sizeof(bone) == 0x34, "Incorrect size of bone");
struct PlayerAlter {
	wchar_t PlayerName[12];		//0x00
	short ColorIndex;			//0x18		// See defined color indexes above.
	short Unknown1;				//0x1A		// FFFF
	machineindex MachineIndex;	//0x1C		// Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
	char Unknown2;				//0x1D		//something. But, if these 4 chars are FF's, then the player isn't on.
	char Team;					//0x1E
	playerindex PlayerIndex;	//0x1F	// Index to their StaticPlayer
};  //size of 0x20
static_assert_check(sizeof(PlayerAlter) == 0x20, "Incorrect size of PlayerAlter");
struct MachineHeader {
	DWORD	NetworkPointerX2;				//0x0000 // Following this, i found a pointer next to other useless stuff. Then, another pointer, then i found some stuff that looked like it /MIGHT/ very well be strings related to a hash. *shrug*
	long	Unknown0[2];					//0x0004
	short	machineIndex;					//0x000C
	short	Unknown9;						//0x000E // First is 1, then 3, then 7 and back to 0 if not in used (1 is found during the CD Hash Check, 7 if currently playing the game)
	long	isAvailable;					//0x0010
	long	Unknown10[2];					//0x0014
	long	Unknown11;						//0x001C	// most of the time 1, but sometimes changes to 2 for a moment.
	long	Unknown12;						//0x0020
	// 16 bit bitfield for action keys:
	char	Crouch	: 1;					//0x0024
	char	Jump	: 1;
	char	Flashlight : 1;
	char	Unknownbit0 :1;
	char	Unknownbit1 :1;
	char	Unknownbit2 :1;
	char	Unknownbit3 :1;
	char	Unknownbit4 :1;
	char	Reload	: 1;					//0x0025
	char	Fire	: 1;
	char	Swap	: 1;
	char	Grenade	: 1;
	char	Unknownbit5 :1;
	char	Unknownbit6 :1;
	char	Unknownbit7 :1;
	char	Unknownbit8 :1;

	short	Unknown13;						//0x0026
	float	Yaw;							//0x0028 // player's rotation - in radians, from 0 to 2*pi, (AKA heading)
	float	Pitch;							//0x002C // Player's pitch - in radians, from -pi/2 to +pi/2, down to up. 
	float	Roll;							//0x0030 // roll - unless walk-on-walls is enabled, this will always be 0.
	BYTE	Unknown1[8];					//0x0034
	float	ForwardVelocityMultiplier;		//0x003C
	float	HorizontalVelocityMultiplier;	//0x0040
	float	ROFVelocityMultiplier;			//0x0044
	short	WeaponIndex;					//0x0048
	short	GrenadeIndex;					//0x004A
	short	UnknownIndex;					//0x004C // The index is -1 if no choices are even available.
	short	Unknown2;						//0x004E
	short	Unknown3;						//0x0050 // 1
	char	SessionKey[8];					//0x0052 // This is used to accept the incoming player PLUS validate with gamespy server to assuming accept the incoming player.
	short	Unknown4;						//0x005A
	long	UniqueID;						//0x005C // increase every time a player join (notice: it is not focus on specific machine struct, it applies to all.)
	//Below is used for Halo CE and Trial?, Halo PC doesn't have this extra data.
	//NOTICE: This below is disabled as it is no longer needed.
	//wchar_t	LastPlayersName[12];			//0x0060 // Odd.. this isnt the name of the player who's on, but i thinkn it's the Previous player's name.
	//long	Unknown6;						//0x0078 // these two were -1.
	//long	Unknown7;						//0x007C // but sometimes become 0.
	//char	IP[32];							//0x0080
	//char	CDhash[32];			// a solid block array, so it's not necessarily a c_str i think, but there's still usually just 0's afterwards anyways.
	//BYTE	UnknownZeros[44];	// zeros..
}; // Size: 0xEC = Halo CE & Trial, 0x60 = Halo PC
static_assert_check(sizeof(MachineHeader) == 0x60, "Incorrect size of MachineHeader");
struct PlayerHeader {//Verified offsets.
	short	PlayerID;						//0x000
	short	IsLocal;						//0x002			// 0=Local(no bits set), -1=Other Client(All bits set)
	wchar_t Name[12];						//0x004			// Unicode
	ident	UnknownIdent;					//0x01C
	long	Team;							//0x020			// 0=Red, 1=Blue
	ident	SwapObject;						//0x024
	short	SwapType;						//0x028			// Vehicle=8, Weapon=6
	short	SwapSeat;						//0x02A			// Warthog-Driver=0, Passenger=1, Gunner=2, Weapon=-1
	long	RespawnTimer;					//0x02C			// Counts down when dead, Alive=0
	long	Unknown;						//0x02F
	ident	CurrentBiped;					//0x034
	ident	PreviousBiped;					//0x038
	short	ClusterIndex;					//0x03C
	char	Swap : 1;						//0x03E.0
	char	UnknownBits4 :7;				//0x03E.7
	char	UnknownByte;					//0x03F
	ident	UnknownIdent1;					//0x040
	long	LastBulletShotTime;				//0x044			// since game start(0)
	wchar_t Name1[12];						//0x048
	short	ColorIndex;						//0x060			// See defined color indexes above.
	short	Unknown00;						//0x062
	playerindex	MachineIndex;				//0x064			// Index to the Machine List (which has their CURRENT cdhash and IP. (it also has the LAST player's name))
	char	Unknown0;						//0x065			//something. But, if these 4 chars are FF's, then the player isn't on.
	char	iTeam;							//0x066
	playerindex	PlayerIndex;				//0x067			// Index to their StaticPlayer
	long	Unknown1;						//0x068
	float	VelocityMultiplier;				//0x06C
	ident	UnknownIdent3[4];				//0x070
	long	Unknown2;						//0x080
	long	LastDeathTime;					//0x084		// since game start(0)
	char	Unknown3[18];					//0x088
	short	KillsCount;						//0x09A
	char	Unknown4[6];					//0x09C
	short	AssistsCount;					//0x0A2
	char	Unknown5[8];					//0x0A4
	short	BetrayedCount;					//0x0AC
	short	DeathsCount;					//0x0AE
	short	SuicideCount;					//0x0B0
	char	Unknown6[18];					//0x0B2
	short	FlagStealCount;					//0x0C4 <--- and below are correct!
	short	FlagReturnCount;				//0x0C6
	short	FlagCaptureCount;				//0x0C8
	char	Unknown7[6];					//0x0CA
	ident	UnknownIdent4;					//0x0D0
	char	Unknown8;						//0x0D4
	bool	HasQuit;						//0x0D5
	char	Unknown81[6];					//0x0D6
	short	Ping;							//0x0DC
	char	Unknown9[14];					//0x0DE
	ident	UnknownIdent5;					//0x0EC
	long	Unknown10;						//0x0F0
	long	SomeTime;						//0x0F4
	vect3	World;							//0x0F8
	ident	UnknownIdent6;					//0x104
	char	Unknown11[20];					//0x108
	char	Melee		:	1;				//0x11C.0
	char	Action		:	1;				//0x11C.1
	char	UnknownBit	:   1;				//0x11C.2
	char	Flashlight	:	1;				//0x11C.3
	char	UnknownBit1	:	4;				//0x11C.4
	char	UnknownBit2	:	5;				//0x11D.0
	char	Reload		:	1;				//0x11D.5
	char	UnknownBit3	:	2;				//0x11D.6
	char	Unknown12[26];					//0x11E
	vect2	Rotation;						//0x138		// Yaw, Pitch (again, in radians.
	float	ForwardVelocityMultiplier;		//0x140
	float	HorizontalVelocityMultiplier;	//0x144
	float	RateOfFireVelocityMultiplier;	//0x148
	short	HeldWeaponIndex;				//0x14C
	short	GrenadeIndex;					//0x14E
	char	Unknown13[4];					//0x150
	vect3	LookVect;						//0x154
	char	Unknown14[16];					//0x160
	vect3	WorldDelayed;					//0x170	// Oddly enough... it matches the world vect, but seems to lag behind (Possibly what the client reports is _its_ world coord?)
	char	Unknown15[132];					//0x17C
};
//template<int s> struct StructTest;
//StructTest<offsetof(PlayerHeader, World)> structTest;
static_assert_check(sizeof(PlayerHeader) == 0x200, "Incorrect size of PlayerHeader");

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
#define CTF 1
#define SLAYER 2
#define ODDBALL 3
#define KOTH 4
#define RACE 5
struct GameType {						//UNDONE GameType Struct is not 100% decoded.
	wchar_t name[24];					// 0x00
	DWORD GameStage;					// 0x30 1=CTF, 2=Slayer, 3=Oddball, 4=KOTH, 5=Race
	DWORD isTeamPlay;					// 0x34
	bool showEnemyRadar: 1;				// 0x38.0
	bool friendlyIndicator: 1;			// 0x38.1
	bool infiniteGrenade: 1;			// 0x38.2
	bool noShield: 1;					// 0x38.3
	bool invisible: 1;					// 0x38.4
	bool isCustomEquipment: 1;			// 0x38.5
	bool showFriendlyRadar: 1;			// 0x38.6
	bool unknown: 1;					// 0x38.7
	BYTE Unknown0[3];					// 0x39		//TODO need to find out what these are and 0x38 as well.
	DWORD Objectives;					// 0x3C 0=Motion Tracker, 1=Navpoints, 2=None
	DWORD isOddManOut;					// 0x40
	DWORD RespawnTimeGrowth;			// 0x44
	DWORD RespawnTime;					// 0x48
	DWORD RespawnTimeSuicide;			// 0x4C
	DWORD LimitLives;					// 0x50
	DWORD Unknown1;						// 0x54
	DWORD ScoreLimit;					// 0x58
	DWORD WeaponSet;					// 0x5C
	DWORD VehicleRed;					// 0x60 Need some work here.
	DWORD VehicleBlue;					// 0x64 Need some work here.
	DWORD VehicleRespawnTime;			// 0x68
	DWORD isFriendlyFire;				// 0x6C
	DWORD RespawnTimeTeamKill;			// 0x70 Need to verify if respawn time or a cool down.
	DWORD isTeamBalance;				// 0x74
	DWORD TimeLimit;					// 0x78
	// ball gametype data
	DWORD MovingHill;					// 0x7C (KOTH) 0=off, 1=on; (Race) 0=Normal, 1=any order, 2=Rally; (Oddball) 0=off, 1=on;
	char TeamScoring;					// 0x80 (Race) 0=minimal, 1=maximum, 2=Sum; (Oddball) 0=Slow, 1=Normal, 2=Fast
	short Unknown2;						// 0x81
	char TraitBallWith;					// 0x83 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
	DWORD Unknown3;						// 0x84
	DWORD TraitBallWithout;				// 0x88 0=None, 1=Invisible, 2=Extra Damage, 3=Damage Resistent
	BYTE Unknown4[15];					// 0x8C - 0x9B unknown
};
static_assert_check(sizeof(GameType) == 0x9B, "Incorrect size of GameType");

struct GlobalServer {
	DWORD* Unknown0;				//0x000 // at least I _think_ it's a pointer since there _is_ something if i follow it.
	WORD Unknown1;					//0x004
	WORD Unknown2;					//0x006
	wchar_t SV_NAME[66];			//0x008
	char MAP_NAME[128];				//0x08C
	wchar_t GAMETYPE_NAME[24];		//0x10C
	BYTE Unknown11[40];				//0x13C // partial of Gametype need to break them down.
	BYTE scoreMax;					//0x164
	BYTE Unknown3[128];				//0x165
	char SV_MAXPLAYERS;				//0x1E5	// Note: there is another place that also says MaxPlayers - i think it's the ServerInfo socket buffer.
	short Unknown09;				//0x1E6
	short totalPlayers;				//0x1E8
	short Unknown10;				//0x1EA	// i think LastSlotFilled
};

struct damageFlags {
	bool unknown1:2;			//0.0-0.1
	bool kill1:1;				//0.2
	bool unknown2:1;			//0.3
	bool kill2:1;				//0.4
	bool unknown3:3;			//0.5-0.9
	bool unknown4:3;			//1.0-1.2
	bool cannotTakeDamage:1;	//1.3
	bool unknown5:4;			//1.4 - 1.9
};
static_assert_check(sizeof(damageFlags) == 0x02, "Incorrect size of damageFlags");

struct ObjectS {
	ident	ModelTag;				// 0x0000
	long	Zero;					// 0x0004
	char	Flags[4];				// 0x0008
	long	Timer;					// 0x000C
	//char	Flags2[4];				// 0x0010
	char	unkBits : 2;			// 0x0010
	bool	ignoreGravity:1;
	char	unk1:4;
	bool	noCollision:1;
	char	unkBits1[3];			// 0x0011
	long	Timer2;					// 0x0014
	long	Zero2[17];				// 0x0018
	vect3	World;					// 0x005C
	vect3	Velocity;				// 0x0068
	vect3	LowerRot;				// 0x0074
	vect3	Scale;					// 0x0080
	vect3	UnknownVect1;			// 0x008C
	long	LocationID;				// 0x0098
	long	Unknown1;				// 0x009C
	vect3	UnknownVect2;			// 0x00A0
	float	Unknown2[2];			// 0x00AC		
	long	Unknown3;				// 0x00B4
	short	GameObject;				// 0x00B8	// 0 >= is game object, -1 = is NOT game object
	short	Unknown4;				// 0x00BA
	long	Unknown5;				// 0x00BD
	ident	Player;					// 0x00C0
	long	Unknown6[2];			// 0x00C4
	ident	AntrMeta;				// 0x00CC
	long	Unknown7[2];			// 0x00D0
	float	HealthMax;				// 0x00D8
	float	ShieldMax;				// 0x00DC
	float	Health;					// 0x00E0
	float	Shield1;				// 0x00E4
	long	Unknown8[7];			// 0x00E8
	short	Unknown9;				// 0x0104
	damageFlags	damageFlag;			// 0x0106
	short	Unknown10;				// 0x0108
	short	Unknown11;				// 0x010A
	long	Unknown12[2];			// 0x010C
	ident	VehicleWeapon;			// 0x0114
	ident	Weapon;					// 0x0118
	ident	Vehicle;				// 0x011C
	short	SeatType;				// 0x0120
	short	Unknown13;				// 0x0122
	long	Unknown14;				// 0x0124
	float	Shield2;				// 0x0128
	float	Flashlight1;			// 0x012C
	float	Unknown15;				// 0x0130
	float	Flashlight2;			// 0x0134
	long	Unknown16[5];			// 0x0138
	ident	UnknownIdent1;			// 0x014C
	ident	UnknownIdent2;			// 0x0150
	long	Zero3[6];				// 0x0154
	ident	UnknownIdent3;			// 0x016C
	ident	UnknownIdent4;			// 0x0170
	long UnknownMatrix0[16];		//D3DXMATRIX UnknownMatrix;	// 0x0174
	long UnknownMatrix1[16];		//D3DXMATRIX UnknownMatrix1;	// 0x01B4
	//Everything after this is 0x01F4
};
//static_assert_check(offsetof(ObjectS, damageFlag) == 0x206, "Incorrect offset of damageFlag");

static_assert_check(sizeof(ObjectS) == 0x1F4, "Incorrect size of ObjectS");

struct actionFlags { // these are action flags, basically client button presses and these don't actually control whether or not an event occurs
	bool crouching : 1; // (a few of these bit flags are thanks to halo devkit)
	bool jumping : 1; // 2
	char UnknownBit : 2; // 3
	bool Flashlight : 1; // 5
	bool UnknownBit2 : 1; // 6
	bool actionPress : 1; // 7 think this is just when they initially press the action button
	bool melee : 1; // 8
	char UnknownBit3 : 2; // 9
	bool reload : 1; // 11
	bool primaryWeaponFire : 1; // 12 right mouse
	bool secondaryWeaponFire : 1; // 13 left mouse
	bool secondaryWeaponFire1 : 1; // 14
	bool actionHold : 1; // 15 holding action button
	char UnknownBit4 : 1; // 16
};
static_assert_check(sizeof(actionFlags) == 0x02, "Incorrect size of aFlag");


struct BipedS {
	ObjectS sObject;			// 0x0000
	long	Unknown[4];			// 0x01F4
	short	IsInvisible;		// 0x0204	normal = 0x41 invis = 0x51 (bitfield?)
	char	Flashlight;			// 0x0206
	char	Frozen;				// 0x0207
	actionFlags	actionBits;		// 0x0208 & 0x0209
	char	Unknown1[2];		// 0x020A
	long	UnknownCounter1;	// 0x020C
	long	UnknownLongs1[5];	// 0x0210
	vect3	RightVect;			// 0x0224
	vect3	UpVect;				// 0x0230
	vect3	LookVect;			// 0x023C
	vect3	ZeroVect;			// 0x0248
	vect3	RealLookVect;		// 0x0254
	vect3	UnknownVect3;		// 0x0260
	char	Unknown2[140];		// 0x026C
	ident	PrimaryWeapon;		// 0x02F8
	ident	SecondaryWeapon;	// 0x02FC
	ident	ThirdWeapon;		// 0x0300
	ident	FourthWeapon;		// 0x0304
	long	UnknownLongs2[5];	// 0x0308
	char	grenadeIndex;		// 0x031C
	char	grenadeIndex1;		// 0x031D
	char	grenade0;			// 0x031E
	char	grenade1;			// 0x031F
	char	Zoom;				// 0x0320
	char	Zoom1;				// 0x0321
	char	Unknown3[2];		// 0x0322
	ident	SlaveController;	// 0x0324
	char	Unknown4[552];		// 0x0328
	bone	LeftThigh;			// 0x0550
	bone	RightThigh;			// 0x0584
	bone	Pelvis;				// 0x05B8
	bone	LeftCalf;			// 0x05EC
	bone	RightCalf;			// 0x0620
	bone	Spine;				// 0x0654
	bone	LeftClavicle;		// 0x0688
	bone	LeftFoot;			// 0x06BC
	bone	Neck;				// 0x06F0
	bone	RightClavicle;		// 0x0724
	bone	RightFoot;			// 0x0758
	bone	Head;				// 0x078C
	bone	LeftUpperArm;		// 0x07C0
	bone	RightUpperArm;		// 0x07F4
	bone	LeftLowerArm;		// 0x0828
	bone	RightLowerArm;		// 0x085C
	bone	LeftHand;			// 0x0890
	bone	RightHand;			// 0x08C4
	char	Unknown5[1216];		// 0x08F8 //Missing 0x092C?
}; // Size - 3564(0x0DEC) bytes

//Major WIP Halo Structure Begin
struct WeaponS {									//WARNING! Offset is NOT 100% accurate!  BulletCount____ is 100% accurate!
	ObjectS			sObject;
	char			Unknown[24];					//0x01F8
	ident			UnknownIdent;					//0x0200
	unsigned long	NetworkTime;					//0x0204
	char			Unknown1[36];					//0x01F8
	unsigned long	UnknownFlags	:	32;			//0x021C
	unsigned long	UnknownBit		:	1;			//0x0220
	unsigned long	Fire			:	1;			//0x0224
	unsigned long	Grenade			:	1;			//0x0230
	unsigned long	Reload			:	1;			//0x022C
	unsigned long	Melee			:	1;			//0x0230
	unsigned long	UnknownBit1		:	1;			//0x0234
	unsigned long	Zoom			:	1;			//0x0238
	unsigned long	UnknownBit2		:	1;			//0x0238
	unsigned long	UnusedBits		:	24;			//0x023C
	float			Unknown2;						//0x0240
	bool			IsFiring;						//0x0238
	char			Unknown3;						//0x0239
	unsigned short	WeaponReadyWaitTime;			//0x023A
	char			Unknown4[36];					//0x023C
	unsigned long	SomeCounter;					//0x0260
	unsigned long	IsNotFiring;					//0x0264 //this is correct one
	unsigned long	Unknown5[2];					//0x0274
	float			Unknown6;						//0x027C
	unsigned long	Unknown7;						//0x0280
	float			Unknown8[2];					//0x0284
	ident			UnknownIdent1;					//0x028C
	unsigned long	AutoReloadCounter;				//0x0290
	unsigned char	Unknown9[28];					//0x0294
	unsigned short	ReloadFlags;					//0x02B0 // 0=NotReloading,1=Reloading, 2=???, 3=???  //is correct
	unsigned short	ReloadCountdown;				//0x02B2	//can set to 0 to finish reload countdown
	unsigned short	Unknown10;						//0x02B4
	unsigned short	BulletCountInRemainingClips;	//0x02B6
	unsigned short	BulletCountInCurrentClip;		//0x02B8
	char			Unknown11[18];					//0x02BA
	ident			UnknownIdent2;					//0x02CC
	unsigned long	LastBulletFiredTime;			//0x02DO
	char			Unknown12[16];					//0x02D4
	vect3			Unknown13[2];					//0x02E4
	char			Unknown14[12];
	unsigned long	BulletCountInRemainingClips1;
	char			Unknown15[52];
}; // Size - 1644(0x066C)

struct VehicleS {
	ident	ModelTag;		// 0x0000
	long	Zero;			// 0x0004
	char	Flags[4];		// 0x0008
	long	Timer;			// 0x000C
	char	Flags2[4];		// 0x0010
	long	Timer2;			// 0x0014
	long	Zero2[17];		// 0x0018
	vect3	World;			// 0x005C
	vect3	Velocity;		// 0x0068
	vect3	LowerRot;		// 0x0074  //incorrect
	vect3	Rotation;		// 0x0080  //incorrect
	vect3	UnknownVect1;	// 0x008C
	long	LocationID;		// 0x0098
	long	UnknownO1;		// 0x009C
	vect3	UnknownVect2;	// 0x00A0
	float	UnknownO2[2];	// 0x00AC		
	long	UnknownO3[3];	// 0x00B4
	ident	Player;			// 0x00C0
	long	UnknownO4[2];	// 0x00C4
	ident	AntrMeta;		// 0x00CC
	long	UnknownO5[4];	// 0x00D0
	float	Health;			// 0x00E0
	float	Shield1;		// 0x00E4
	long	UnknownO6[11];	// 0x00E8
	ident	VehicleWeapon;	// 0x0114
	ident	CurrentFirstPersonOn;	// 0x0118
	ident	Vehicle;		// 0x011C
	short	SeatType;		// 0x0120
	short	UnknownO7;		// 0x0122
	long	UnknownO8;		// 0x0124
	float	Shield2;		// 0x0128
	float	Flashlight1;	// 0x012C
	float	Unknown9;		// 0x0130
	float	Flashlight2;	// 0x0134
	long	UnknownO10[5];	// 0x0138
	ident	UnknownIdent1;	// 0x014C
	ident	UnknownIdent2;	// 0x0150
	long	Zero3[6];		// 0x0154
	ident	UnknownIdent3;	// 0x016C
	ident	UnknownIdent4;	// 0x0170
	vect3 UnknownMatrix;	// 0x0174
	vect3 UnknownMatrix1;	// 0x0180
//** END OBJECT part
	char	UnknownVeh0[0x7A];		// 0x018C
	bool	isNotAllowPlayerEntry;	// 0x206
	char	UnknownVeh1[0x11D];		// 0x18C
	ident	SlaveController;		// 0x324
	//Anything goes after this?
}; // Size - 3580(0xDFC)
//Major WIP Halo Structure End


struct MapStruct {
	char head[4];			//0x00
	DWORD haloVersion;		//0x04
	DWORD unknown02;		//0x08
	DWORD unknown03;		//0x0C //Nulls
	DWORD unknown04;		//0x10
	DWORD unknown05;		//0x14
	DWORD unknown06[2];		//0x18 //Nulls
	char mapName[32];		//0x20
	char version[32];		//0x40
	DWORD cam_multi_menu;	//0x060 // 0 = Campaign, 1 = Multi-player, 2 = Menu
	DWORD unknown07;		//0x064
};
static_assert_check(sizeof(MapStruct) == 0x68, "Incorrect size of MapStruct");

struct ConsoleStruct {
	bool gamePause;			//0x00
	bool allowConsole;		//0x01
	WORD unknown01;			//0x02 //Nulls
	bool isNotConsole;		//0x04
	BYTE unknown02;			//0x05
	BYTE unknown03;			//0x06
	BYTE keyPress;			//0x07
	WORD unknown04;			//0x08
	WORD unknown05;			//0x0A
	WORD unknown06;			//0x0C
	WORD unknown07[61];		//0x10 //Nulls
	DWORD unknown08;		//0x88
	DWORD unknown09;		//0x8C
	DWORD unknown10;		//0x90
	DWORD unknown11;		//0x94
	char inputName[32];		//0x98
	char input[255];		//0xB8
};
static_assert_check(sizeof(ConsoleStruct) == 0x1B7, "Incorrect size of MapStruct");

struct banCheckStruct {
	wchar_t password[9];				//0x00
	char cdKeyHash[32];					//0x12
	char unknown0[40];					//0x32
	char unknown1[4];					//0x5A
	wchar_t requestPlayerName[12];		//0x5E
}; // Size - 100 (0x64) bytes
static_assert_check(sizeof(banCheckStruct) == 0x76, "Incorrect size of banCheckStruct");

//Extras for Add-on API usage.

struct CheatHeader {
	bool deathlessPlayer;	//0x00
	bool jetPack;			//0x01
	bool infiniteAmmo;		//0x02
	bool bumpPossession;	//0x03
	bool superJump;			//0x04
	bool reflexDamage;		//0x05
	bool medUsa;			//0x06
	bool omnipotent;		//0x07
	bool controller;		//0x08
	bool bottomlessClip;	//0x09
};
static_assert_check(sizeof(CheatHeader) == 0xA, "Incorrect size of CheatHeader");

struct D3DCOLOR_COLORVALUE_ARGB {
	float a;
	float r;
	float g;
	float b;
	D3DCOLOR_COLORVALUE_ARGB() {
		a=1.0f;
		r=1.0f;
		g=1.0f;
		b=1.0f;
	}
};
struct ConsoleColorStruct {
	D3DCOLOR_COLORVALUE_ARGB* x0_Black;		//0x00
	D3DCOLOR_COLORVALUE_ARGB* x1_DodgerBlue;	//0x04
	D3DCOLOR_COLORVALUE_ARGB* x2_Cyan;			//0x08
	D3DCOLOR_COLORVALUE_ARGB* x3_White;		//0x0C
	D3DCOLOR_COLORVALUE_ARGB* x4_Yellow;		//0x10
	D3DCOLOR_COLORVALUE_ARGB* x5_Blue;			//0x14
	D3DCOLOR_COLORVALUE_ARGB* x6_Coral;		//0x18	//Light Orange
	D3DCOLOR_COLORVALUE_ARGB* x7_Aquamarine;	//0x1C
	D3DCOLOR_COLORVALUE_ARGB* x8_Purple;		//0x20
	D3DCOLOR_COLORVALUE_ARGB* x9_DarkGreen;	//0x24
	D3DCOLOR_COLORVALUE_ARGB* x10_Red;			//0x28
	D3DCOLOR_COLORVALUE_ARGB* x11_Indigo;		//0x2C	//Dark Purple
	D3DCOLOR_COLORVALUE_ARGB* x12_Orange;		//0x30
	D3DCOLOR_COLORVALUE_ARGB* x13_Gray;		//0x34

};
static_assert_check(sizeof(ConsoleColorStruct) == 0x38, "Incorrect size of ConsoleColorStruct");

struct GlobalVars {
	DWORD ptrAcceptableASCIICharsArray;			//Unknown if this is correct.
	DWORD ptrExceptionHandlerFunc;				//Halo CE 0x006207AC
	ConsoleColorStruct consoleColorStruct;		//Halo CE 0x006207B0 - 0x006207E4
	DWORD ptrStrAsleep;							//Halo CE 0x006207E8
	DWORD ptrStrAlert;							//Halo CE 0x006207EC
	DWORD ptrStrCombat;							//Halo CE 0x006207F0
	DWORD UnknownNull;
	DWORD ptrStrFlood_Carrier;					//Halo CE 0x006207F8,  A pointer plus seems to be a structure for it.
	DWORD ptrFlood_Carrier_Parms0[4];			//Part 1 - Parameters?
	DWORD ptrFlood_CarrierFunc;					//Part 2 - Is a pointer for a Flood Carrier Func?
	DWORD ptrFlood_Carrier_Parms1[4];			//Part 3 - Parameters?
	//Etc, etc, etc. Don't have time to figure out the rest.
};

struct SoundPlay {
	short inUsed;	//0x00 //If is -1, then not in used. If is 0 or above, then in used.
	short Priority;	//0x02 //0 = general action, 1 = Unknown, 2 = UI sounds, 3 = possible loop or background sounds.
};
struct SoundVars {
	bool soundEnabled;				//0x00	//Not entirely sure...
	bool globalSoundOn;				//0x01
	bool muteSoundAsNotInWindow;	//0x02
	bool UNKNOWN0;					//0x03
	DWORD UNKNOWN1;					//0x04	Full of nulls
	DWORD UNKNOWNPTR0;				//0x08	Some kind of pointer to a struct?
	DWORD time;						//0x0C	counting up
	float UNKNOWN2;					//0x10	some kind of sound volume? (Constantly changing)
	DWORD UNKNOWN3;					//0x14	Unknown 1/0 value, constantly changing.
	DWORD UNKNOWN4;					//0x18	Always 1?
	float UNKNOWN5;					//0x1C	Always 1?
	vect3 Vector0;					//0x20
	vect3 Vector1;					//0x2C
	vect3 Vector2;					//0x38
	vect3 World;					//0x44
	vect3 UNKNOWN6;					//0x50	0 and 0x80000000 per float (not sure why)
	vect3 Vector4[6];				//0x5C
	float vol_slider;				//0xA4
	float vol_Music;				//0xA8
	float vol_Master;				//0xAC
	float vol_Effects;				//0xB0
	DWORD Max_Channels;				//0xB4
	DWORD UNKNOWN7;					//0xB8
	DWORD UNKNOWN8;					//0xBC	Null
	DWORD soundHeader;				//0xC0
	BYTE UNKNOWN9[0x1C];			//0xC4	//Full of nulls
	short UNKNOWN10;				//0xE0
	short UNKNOWN11;				//0xE2
	SoundPlay sPlay[38];			//0xE4
};
static_assert_check(sizeof(SoundVars) == 0x17C, "Incorrect size of SoundVars");

enum GAME_MODE: unsigned short {
	GAME_SINGLE,
	GAME_MULTI,
	GAME_HOSTING
};
struct GAME_MODE_S {
	bool GAME_SINGLE;
	bool GAME_MULTI;
	bool GAME_HOSTING;
};

struct validationCheck {
	long UniqueID;
	DWORD isValid;
	char *message;
};
static_assert_check(sizeof(validationCheck) == 0xC, "Incorrect size of validationCheck");
#pragma pack(pop)

#endif