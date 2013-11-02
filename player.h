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

extern "C" class Player {
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
	Player();
	~Player();
	Player& operator=(Player const &);
	PlayerInfo WINAPIC getPlayerMindex(machineindex m_ind);
	PlayerInfo WINAPIC getPlayer(DWORD playerId);
	PlayerInfo WINAPIC getPlayerIdent(ident pl_Tag);
	PlayerInfo WINAPIC getPlayerByNickname(const wchar_t nickname[]);
	PlayerInfo WINAPIC getPlayerByUsername(const wchar_t username[]);
	PlayerInfo WINAPIC getPlayerByUniqueID(long uniqueID);
	short WINAPIC StrToPlayerList(const wchar_t src[], util::dynamicStack<Player::PlayerInfo> &plMatch, Player::PlayerInfo* plOwner);
	int WINAPIC GetIDFullName(wchar_t fullName[]);
	int WINAPIC GetIDIpAddress(wchar_t ipAddress[]);
	int WINAPIC GetIDPort(wchar_t port[]);
	void WINAPIC GetFullNameID(int ID, wchar_t fullName[13]);
	void WINAPIC GetIpAddressID(int ID, wchar_t ipAddress[16]);
	void WINAPIC GetPortID(int ID, wchar_t port[10]);
	bool WINAPIC Update(PlayerInfo& plI);
	bool WINAPIC setNickname(PlayerInfo& plI, wchar_t nickname[]);
	bool WINAPIC sendCustomMsg(char formatMsg, toggle chatRconRemote, PlayerInfo& plI, const wchar_t *Msg, ...);
	bool WINAPIC isAdmin(machineindex m_ind);
};
extern "C" dllport Player* player;

#endif