#ifndef haloEngineH
#define haloEngineH

#define REJECT_CANTJOINSERVER 0
#define REJECT_INVALIDCONREQUEST 1
#define REJECT_PASSWORDREJECTED 2
#define REJECT_SERVERFULL 3
#define REJECT_CDINVALID 4
#define REJECT_CDINUSED 5
#define REJECT_OPBANNED 6
#define REJECT_OPKICKED 7
#define REJECT_VIDEOTEST 8
#define REJECT_CPSAVED 9
#define REJECT_ADDRESSINVALID 10
#define REJECT_PROFILEREQUIRED 11
#define REJECT_INCOMPATIBLENETWORK 12
#define REJECT_OLDERCVER 13
#define REJECT_NEWERCVER 14
#define REJECT_ADMINREQUIREDPATCH 15
#define REJECT_REQUESTDELETESAVED 16

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


extern "C" class IHaloEngine { // For Add-on API interface support
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
	DIRECTX9*  DirectX9;
	DIRECTI8*  DirectInput8;
	DIRECTS8*  DirectSound8;
	//Halo Simulate Functions Begin
	virtual DWORD WINAPIC BuildPacket(LPBYTE output, DWORD arg1, DWORD packettype, DWORD arg3, LPBYTE dataPtr, DWORD arg4, DWORD arg5, DWORD arg6)=0;
	virtual void WINAPIC AddPacketToPlayerQueue(DWORD player, LPBYTE packet, DWORD packetCode, DWORD arg1, DWORD arg2, DWORD arg3, DWORD arg4, DWORD arg5)=0;
	virtual void WINAPIC AddPacketToGlobalQueue(LPBYTE packet, DWORD packetCode, DWORD arg1, DWORD arg2, DWORD arg3, DWORD arg4, DWORD arg5)=0;
	virtual void WINAPIC DispatchRcon(rconData& d, IPlayer::PlayerInfo& plI)=0;
	virtual void WINAPIC DispatchPlayer(chatData& d, int len, IPlayer::PlayerInfo& plI)=0;
	virtual void WINAPIC DispatchGlobal(chatData& d, int len)=0;
	virtual bool WINAPIC sendRejectCode(MachineHeader* mH, DWORD code)=0;
	virtual void WINAPIC GetCDHash(MachineHeader &mH, char hashKey[33])=0;
	virtual void WINAPIC SetObjectSpawnPlayerX(playerindex pl_ind)=0;
	virtual void WINAPIC Kill(IPlayer::PlayerInfo plI)=0;
	virtual bool WINAPIC SetIdle()=0;
	virtual bool WINAPIC MapNext()=0;
	virtual bool WINAPIC Exec(const char* cmd)=0;
	virtual void WINAPIC GetServerPassword(wchar_t pass[8])=0;
	virtual void WINAPIC SetServerPassword(wchar_t pass[8])=0;
	virtual void WINAPIC GetRconPassword(char pass[8])=0;
	virtual void WINAPIC SetRconPassword(char pass[8])=0;
	virtual bool WINAPIC BanPlayer(IPlayer::PlayerExtended &plEx, tm &gmtm)=0;
	virtual bool WINAPIC BanCDkey(wchar_t CDHash[33], tm &gmtm)=0;
	virtual bool WINAPIC BanIP(wchar_t IP_Addr[16], tm &gmtm)=0;
	virtual int WINAPIC BanIPGetId(wchar_t IP_Addr[16])=0;
	virtual int WINAPIC BanCDkeyGetId(wchar_t CDHash[33])=0;
	virtual bool WINAPIC UnbanID(int ID)=0;
	//char* WINAPIC GetCDHash(Player::PlayerInfo& plI);
	virtual void WINAPIC GetIP(MachineHeader& mH, BYTE m_ip[4])=0;
	virtual void WINAPIC GetPort(MachineHeader& mH, WORD& m_port)=0;
	//Halo Simulate Functions End
	virtual bool WINAPIC EXTAddOnGetInfoIndex(size_t index, addon::addonInfo &getInfo)=0;
	virtual bool WINAPIC EXTAddOnGetInfoByName(wchar_t name[], addon::addonInfo &getInfo)=0;
	virtual bool WINAPIC EXTAddOnReloadDll(wchar_t name[128])=0;
};
extern "C" dllport IHaloEngine* pIHaloEngine;

#endif