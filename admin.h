#ifndef adminH
#define adminH

#define LOGININVALID -1
#define LOGINFAIL 0
#define LOGINPASS 1

CNATIVE class IAdmin {
public:
	virtual toggle WINAPIC isPlayerAuthorized(IPlayer::PlayerInfo* plI, const wchar_t* cmd, util::ArgContainer* arg, CmdFunc* func)=0;
	virtual toggle WINAPIC UsernameExist(wchar_t username[])=0;
	virtual toggle WINAPIC Add(wchar_t hashW[32], wchar_t IP_Addr[15], wchar_t IP_Port[6], wchar_t username[24],wchar_t password[],short level,bool remote, bool pass_force)=0;
	virtual toggle WINAPIC Del(wchar_t username[24])=0;
	virtual toggle WINAPIC Login(IPlayer::PlayerInfo& plI, char chatRconRemote, wchar_t user[], wchar_t pass[])=0;
};
CNATIVE dllport IAdmin* pIAdmin;

#endif