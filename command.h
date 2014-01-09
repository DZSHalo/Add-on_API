#ifndef commandH
#define commandH


#pragma pack(push,1)
struct helpInfo {
	wchar_t info[4][255];
};
#pragma pack(pop)
extern "C" class ICommand {
public:
	virtual bool WINAPIC Add(const wchar_t command[], CmdFunc func, const wchar_t section[], unsigned short min, unsigned short max, bool allowOverride, GAME_MODE_S mode)=0;
	virtual bool WINAPIC Del(CmdFunc func, const wchar_t funcName[])=0;
	virtual bool WINAPIC ReloadLevel()=0;
	virtual bool WINAPIC AliasAdd(const wchar_t* command, const wchar_t* alias)=0;
	virtual bool WINAPIC AliasDel(const wchar_t* command, const wchar_t* alias)=0;
};
extern "C" dllport ICommand* pICommand;
#endif