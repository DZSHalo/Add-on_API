#ifndef commandH
#define commandH


#pragma pack(push,1)
struct helpInfo {
	wchar_t info[4][255];
};
#pragma pack(pop)
extern "C" class Command {
public:
	Command();
	~Command();
	dllport bool WINAPIC Add(const wchar_t command[], CmdFunc func, const wchar_t section[], unsigned short min, unsigned short max, bool allowOverride, GAME_MODE_S mode);
	dllport bool WINAPIC Del(CmdFunc func, const wchar_t funcName[]);
	dllport bool WINAPIC ReloadLevel();
	dllport bool WINAPIC AliasAdd(const wchar_t* command, const wchar_t* alias);
	dllport bool WINAPIC AliasDel(const wchar_t* command, const wchar_t* alias);
};
extern "C" dllport Command* command;
#endif