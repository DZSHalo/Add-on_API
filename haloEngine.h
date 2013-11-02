#ifndef haloEngineH
#define haloEngineH

#define CANTJOINSERVER 0
#define INVALIDCONREQUEST 1
#define PASSWORDREJECTED 2
#define SERVERFULL 3
#define CDINVALID 4
#define CDINUSED 5
#define OPBANNED 6
#define OPKICKED 7
#define VIDEOTEST 8
#define CPSAVED 9
#define ADDRESSINVALID 10
#define PROFILEREQUIRED 11
#define INCOMPATIBLENETWORK 12
#define OLDERCVER 13
#define NEWERCVER 14
#define ADMINREQUIREDPATCH 15
#define REQUESTDELETESAVED 16

#define HALO_UNKNOWN 0
#define HALO_TRIAL 1
#define HALO_PC 2
#define HALO_CE 3

#ifndef DIRECT3D_VERSION
#define DIRECTX9 DWORD
#else
#define DIRECTX9 IDirect3D9
#endif
#ifndef DIRECTINPUT_VERSION
#define DIRECTI8 DWORD
#else
#define DIRECTI8 IDirectInput8
#endif
#ifndef DIRECTSOUND_VERSION
#define DIRECTS8 DWORD
#else
#define DIRECTS8 IDirectSound
#endif

extern "C" class dllport HaloEngine { // For Add-on API interface support
public:
	GlobalServer* globalServer;
	PlayerAlter* playerAlter;
	MachineHeader* machineHeader;
	BYTE machineHeaderSize;
	DWORD* player_base;
	GameType* gameTypeLive;
	CheatHeader* cheatHeader;
	MapStruct* mapCurrent;
	ConsoleStruct* console;
	DWORD* serverUpTimeLive;
	DWORD* mapUpTimeLive;
	DWORD* mapTimeLimitLive;
	DWORD* mapTimeLimitPermament;
	BYTE haloGameVersion;
	bool isDedi;
	ConsoleColorStruct* consoleColor;
	DIRECTX9**  DirectX9;
	DIRECTI8**  DirectInput8;
	DIRECTS8**  DirectSound8;
	HaloEngine();
	//Halo Simulate Functions Begin
	DWORD WINAPIC BuildPacket(LPBYTE output, DWORD arg1, DWORD packettype, DWORD arg3, LPBYTE dataPtr, DWORD arg4, DWORD arg5, DWORD arg6);
	void WINAPIC AddPacketToPlayerQueue(DWORD player, LPBYTE packet, DWORD packetCode, DWORD arg1, DWORD arg2, DWORD arg3, DWORD arg4, DWORD arg5);
	void WINAPIC AddPacketToGlobalQueue(LPBYTE packet, DWORD packetCode, DWORD arg1, DWORD arg2, DWORD arg3, DWORD arg4, DWORD arg5);
	void WINAPIC DispatchRcon(rconData& d, Player::PlayerInfo& plI);
	void WINAPIC DispatchPlayer(chatData& d, int len, Player::PlayerInfo& plI);
	void WINAPIC DispatchGlobal(chatData& d, int len);
	bool WINAPIC sendRejectCode(MachineHeader* mH, DWORD code);
	void WINAPIC GetCDHash(MachineHeader &mH, char hashKey[33]);
	void WINAPIC SetObjectSpawnPlayerX(playerindex pl_ind);
	void WINAPIC Kill(Player::PlayerInfo plI);
	bool WINAPIC SetIdle();
	bool WINAPIC MapNext();
	bool WINAPIC Exec(const char* cmd);
	void WINAPIC GetServerPassword(wchar_t pass[8]);
	void WINAPIC SetServerPassword(wchar_t pass[8]);
	void WINAPIC GetRconPassword(char pass[8]);
	void WINAPIC SetRconPassword(char pass[8]);
	bool WINAPIC BanPlayer(Player::PlayerExtended &plEx, tm &gmtm);
	bool WINAPIC BanCDkey(wchar_t CDHash[33], tm &gmtm);
	bool WINAPIC BanIP(wchar_t IP_Addr[16], tm &gmtm);
	int WINAPIC BanIPGetId(wchar_t IP_Addr[16]);
	int WINAPIC BanCDkeyGetId(wchar_t CDHash[33]);
	bool WINAPIC UnbanID(int ID);
	//char* WINAPIC GetCDHash(Player::PlayerInfo& plI);
	void WINAPIC GetIP(MachineHeader& mH, BYTE m_ip[4]);
	void WINAPIC GetPort(MachineHeader& mH, WORD& m_port);
	//Halo Simulate Functions End
	bool WINAPIC EXTAddOnGetInfoIndex(size_t index, addon::addonInfo &getInfo);
	bool WINAPIC EXTAddOnGetInfoByName(wchar_t name[], addon::addonInfo &getInfo);
	bool WINAPIC EXTAddOnReloadDll(wchar_t name[128]);
};
extern "C" dllport HaloEngine* haloEngine;

#endif