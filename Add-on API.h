#ifndef addonH
#define addonH

#include <Windows.h>
#include <wchar.h>

#define dllport __declspec(dllimport)
#define dllAPI __declspec(dllexport)
#define WINAPIC __cdecl
#if _MSC_VER >= 1600
#define static_assert_check static_assert //This is needed to verify correctly and prevent to compile if something is incorrect.
/*#elif _MSC_VER >=1500
//TODO: Need find a way to add assert support for older than Visual Studio 2010.
#define static_assert_check(a, b)*/
#else
#define static_assert_check(a, b) //Only necessary for Visual Studio 2008 and below to compile.
#endif


#ifdef __cplusplus
#define CNATIVE extern "C"
#else
#define CNATIVE
#endif

#include "struct.h"
#include "util.h"

#ifdef EXT_ICINIFILE
#include "iniFile.h"
#endif

#ifdef EXT_IDATABASE
#include "database.h"
#endif

#ifdef EXT_IOBJECT
#include "object.h"
#endif

#include "player.h"
typedef toggle (WINAPIC *CmdFunc)(IPlayer::PlayerInfo plI, util::ArgContainer& arg,char chatRconRemote, DWORD idTimer, bool* showChat);

#ifdef EXT_IADMIN
#include "admin.h"
#endif

#ifdef EXT_ICOMMAND
#include "command.h"
#endif

CNATIVE dllport void WINAPI haloOutput(int r, const char *text,...);

#define EAOONETIMEUPDATE 2
#define EAOOVERRIDE 1
#define EAOCONTINUE 0
#define EAOFAIL -1

#define CMDFAIL -1
#define CMDNOMATCH 0
#define CMDSUCC 1
#define CMDSUCCDELAY 2

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
	struct versionEAO {
		WORD size;			//Used by sizeof(versionEAO);
		WORD requiredAPI;	//API requirement revision (Including command functions)
		WORD general;		//General revision specifically for events in Halo.
		WORD pICIniFile;	//CiniFile interface revision
		WORD pIDatabase;	//Database interface revision
		WORD external;		//External account revision
		WORD pIHaloEngine;	//Halo Engine interface revision
		WORD pIObject;		//Object interface revision
		WORD pIPlayer;		//Player interface revision
		WORD pICommand;		//Command interface revision
		WORD pITimer;		//Timer interface revision
		WORD pIAdmin;		//Admin interface revision
		WORD reserved2;		//reserved
		WORD reserved3;		//reserved
		WORD reserved4;		//reserved
		WORD reserved5;		//reserved
	};
	#pragma pack(pop)
#ifdef EXT_ITIMER
	CNATIVE class ITimer {
		public:
			DWORD WINAPIC EXTAddOnTimerAdd(IPlayer::PlayerInfo plI, DWORD execTime);
			void WINAPIC EXTAddOnTimerDelete(DWORD id);
	};
	CNATIVE dllport ITimer* pITimer;
#endif
}
CNATIVE dllAPI addon::versionEAO EXTversion = {
	sizeof(addon::versionEAO),	//size
				5,				//requiredAPI - API requirement revision (Including command interface)
				4,				//general - General revision specifically for events in Halo.
#ifdef EXT_ICINIFILE
				2,				//iniFile - CiniFile class revision
#else
				0,				//iniFile - excluded
#endif
#ifdef EXT_IDATABASE
				3,				//pIDatabase - Database class revision
#else
				0,				//pIDatabase - excluded
#endif
#ifdef EXT_IEXTERNAL
				0,				//external - External account revision (for Remote Control or other external possiblities)
#else
				0,				//external - excluded
#endif
#ifdef EXT_IHALOENGINE
				1,				//pIHaloEngine - Halo Engine class revision
#else
				0,				//pIHaloEngine - excluded
#endif
#ifdef EXT_IOBJECT
				2,				//pIObject - Object class revision
#else
				0,				//pIObject - excluded
#endif
#ifdef EXT_IPLAYER
				3,				//pIPlayer - Player class revision
#else
				0,				//pIPlayer - excluded
#endif
#ifdef EXT_ICOMMAND
				1,				//pICommand - Command class revision
#else
				0,				//pICommand - excluded
#endif
#ifdef EXT_ITIMER
				1,				//pITimer - Timer class revision
#else
				0,				//pITimer - excluded
#endif
#ifdef EXT_IADMIN
				1,				//pIAdmin - Admin class revision
#else
				0,				//pIAdmin - excluded
#endif
				0,				//reserved
				0,				//reserved
				0,				//reserved
				0 };			//reserved
static_assert_check(sizeof(addon::versionEAO)==32, "Error, incorrect size of versionEAO struct");

#ifdef EXT_IHALOENGINE
#include "haloEngine.h"
#endif

#endif

