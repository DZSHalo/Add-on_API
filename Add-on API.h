#ifndef addonH
#define addonH

#include <Windows.h>
#include <wchar.h>

#define dllport __declspec(dllimport)
#define dllAPI __declspec(dllexport)
#define WINAPIC __cdecl
#if _ATL_VER >= 0xA00
#define static_assert_check static_assert //This is needed to verify correctly and prevent to compile if something is incorrect.
#else
#define static_assert_check(a, b) //Only nessary for Visual Studio 2008 and below to compile.
#endif

#include "struct.h"
#include "util.h"
#include "iniFile.h"
#include "database.h"
#include "object.h"
#include "player.h"
typedef toggle (WINAPIC *CmdFunc)(Player::PlayerInfo plI, util::ArgContainer arg,char chatRconRemote, short stage, LPVOID stack, bool* showChat);
#include "admin.h"
#include "command.h"

extern "C" dllport void WINAPI haloOutput(int r, const char *text,...);

#define EAOONETIMEUPDATE 2
#define EAOOVERRIDE 1
#define EAOCONTINUE 0
#define EAOFAIL -1

#define CMDSUCC 1
#define CMDFAIL -1
#define CMDNOMATCH 0
typedef void (WINAPIC *addonTimerFunc)(DWORD id, void* param);

namespace addon {

	#pragma pack(push,1)
	struct sectNames {
		wchar_t sect_name1[24];
		wchar_t sect_name2[24];
		wchar_t sect_name3[24];
		wchar_t sect_name4[24];
		wchar_t sect_name5[24];
	};
	struct addonInfo {
		wchar_t	name[128];
		wchar_t	version[15];
		wchar_t	author[128];
		wchar_t	description[255];
		wchar_t	config_folder[24];
		sectNames sectors;
	};
	struct addonTimer {
		DWORD id;
		DWORD execTime;
		void* param;
		addonTimerFunc func;
	};
	struct versionEAO {
		WORD size;			//Used by sizeof(versionEAO);
		WORD requiredAPI;	//API requirement revision (Including command interface)
		WORD general;		//General revision specifically for events in Halo.
		WORD iniFile;		//CiniFile revision
		WORD database;		//Database revision
		WORD external;		//External account revision
		WORD reserved1;		//Reserved
		WORD reserved2;		//Reserved
	};
	#pragma pack(pop)

	extern "C" dllport DWORD WINAPIC EXTAddOnTimerAdd(addonTimer params);
	extern "C" dllport void WINAPIC EXTAddOnTimerDelete(DWORD id);
}
extern "C" dllAPI addon::versionEAO EXTversion = {
	sizeof(addon::versionEAO),	//size	
				3,				//requiredAPI (required)
				3,				//general (optional, set to 0 if not using)
				1,				//iniFile (optional, set to 0 if not using)
				2,				//database (optional, set to 0 if not using)
				0,				//external (optional, set to 0 if not using)
				0,				//reserved
				0 };			//reserved
#include "haloEngine.h"
#endif

