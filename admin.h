#ifndef adminH
#define adminH

#define LOGININVALID -1
#define LOGINFAIL 0
#define LOGINPASS 1

extern "C" class dllport Admin {
public:
	Admin();
	~Admin();
	Admin(Admin const &);
	short unlimit;
	bool reqUser;
	bool reqAddr;
	bool reqPort;
	bool reqLoginAddr;
	bool reqLoginPort;
	bool reqRemoteAddr;
	bool reqRemotePort;
	toggle WINAPIC isPlayerAuthorized(Player::PlayerInfo* plI, wchar_t* cmd, util::ArgContainer* arg, CmdFunc* func);
	toggle WINAPIC UsernameExist(wchar_t username[]);
	toggle WINAPIC Add(wchar_t hashW[], wchar_t IP_Addr[], wchar_t IP_Port[], wchar_t username[],wchar_t password[],short level,bool remote, bool pass_force);
	toggle WINAPIC Del(wchar_t username[]);
	toggle WINAPIC Login(Player::PlayerInfo& plI, char chatRconRemote, wchar_t user[], wchar_t pass[]);
};
extern "C" dllport Admin* admin;

#endif