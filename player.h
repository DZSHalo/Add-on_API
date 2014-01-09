#ifndef playerH
#define playerH

#define MSG_BLANK		0
#define MSG_SERVER		1
#define MSG_HEXT		2
#define MSG_ADMINBOT	3
#define MSG_INFO		4
#define MSG_WARNING		5
#define MSG_ERROR		6
#define MSG_ALERT		7

extern "C" class IPlayer {
public:
	#pragma pack(push,1)
	struct PlayerExtended {
		bool isInServer;
		short adminLvl;
		wchar_t user[25];
		wchar_t pass[33];
		char CDHashA[33];
		wchar_t CDHashW[33];
		wchar_t IP_Addr[16];
		wchar_t IP_Port[8];
		wchar_t IP_Full[24];
		bool temp_pass;
		bool can_chat;
		bool last_team;
		int last_check;
		bool isRemote;
		float handicap_ammo;
		float handicap_clip;
		float handicap_shield;
		float handicap_health;
		float handicap_speed;
		PlayerExtended() {
			isInServer=0;
			adminLvl=0;
			user[0] = 0;
			pass[0] = 0;
			CDHashW[0] = 0;
			IP_Addr[0] = 0;
			IP_Port[0] = 0;
			temp_pass = 0;
			can_chat = 1;
			last_team = 0;
			isRemote = 0;
			handicap_ammo = 0;
			handicap_clip = 0;
			handicap_shield = 1;
			handicap_health = 1;
			handicap_speed = 1;
		}
	};
	struct PlayerInfo {
		PlayerExtended* plEx;
		MachineHeader* mH;
		PlayerAlter* plA;
		PlayerHeader* plH;
		PlayerInfo() {
			plEx = NULL;
			mH = NULL;
			plA = NULL;
			plH = NULL;
		}
		PlayerInfo(PlayerInfo& plI) {
			this->plEx = plI.plEx;
			this->mH = plI.mH;
			this->plA = plI.plA;
			this->plH = plI.plH;
		}
	};
	struct PlayerInfoList {
		PlayerInfo plList[32];
	};
	#pragma pack(pop)
	virtual PlayerInfo WINAPIC getPlayerMindex(machineindex m_ind)=0;
	virtual PlayerInfo WINAPIC getPlayer(DWORD playerId)=0;
	virtual PlayerInfo WINAPIC getPlayerIdent(ident pl_Tag)=0;
	virtual PlayerInfo WINAPIC getPlayerByNickname(const wchar_t nickname[])=0;
	virtual PlayerInfo WINAPIC getPlayerByUsername(const wchar_t username[])=0;
	virtual PlayerInfo WINAPIC getPlayerByUniqueID(long uniqueID)=0;
	virtual short WINAPIC StrToPlayerList(const wchar_t src[], util::dynamicStack<IPlayer::PlayerInfo> &plMatch, IPlayer::PlayerInfo* plOwner)=0;
	virtual int WINAPIC GetIDFullName(wchar_t fullName[])=0;
	virtual int WINAPIC GetIDIpAddress(wchar_t ipAddress[])=0;
	virtual int WINAPIC GetIDPort(wchar_t port[])=0;
	virtual void WINAPIC GetFullNameID(int ID, wchar_t fullName[13])=0;
	virtual void WINAPIC GetIpAddressID(int ID, wchar_t ipAddress[16])=0;
	virtual void WINAPIC GetPortID(int ID, wchar_t port[10])=0;
	virtual bool WINAPIC Update(PlayerInfo& plI)=0;
	virtual bool WINAPIC setNickname(PlayerInfo& plI, wchar_t nickname[])=0;
	virtual bool WINAPIC sendCustomMsg(char formatMsg, toggle chatRconRemote, PlayerInfo& plI, const wchar_t *Msg, ...)=0;
	virtual bool WINAPIC isAdmin(machineindex m_ind)=0;
	virtual PlayerInfo WINAPIC getPlayerByBipedTagCurrent(ident bipedTag)=0;
	virtual PlayerInfo WINAPIC getPlayerByBipedTagPrevious(ident bipedTag)=0;
};
extern "C" dllport IPlayer* pIPlayer;

#endif