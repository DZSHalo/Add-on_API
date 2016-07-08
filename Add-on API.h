#ifndef addonH
#define addonH

/* IMPORTANT: Need to finish these before alpha testing start.
 * MAIN IMPORTANT FOR API!!! - Check chars length in H-Ext before allow over the limit length go through 
 * (Must do this to avoid further crash & override data.)
 *
 */


#ifndef dllport
#define dllport __declspec(dllimport)
#endif
#define dllAPI __declspec(dllexport)
#define WINAPIC __cdecl
#if _MSC_VER >= 1600
#define static_assert_check static_assert //This is needed to verify correctly and prevent to compile if something is incorrect.
#elif _MSC_VER >=1500
//TODO: Need find a way to add assert support for older than Visual Studio 2010.
#define static_assert_check(e, m) typedef char __C_ASSERT__[(e)?1:-1]
#else
#define static_assert_check(a, b) //Only necessary for Visual Studio 2008 and below to compile.
#endif


#define STRINGIZE_HELPER(x) #x
#define STRINGIZE(x) STRINGIZE_HELPER(x)
#define STRINGIZE_WIDE_HELPER(x) L##x
#define STRINGIZE_WIDE(x) STRINGIZE_WIDE_HELPER(x)
#define WARNING(desc) message(__FILE__ "(" STRINGIZE(__LINE__) ") : warning: " #desc)
#define COMPILER_ERROR(desc) message(__FILE__ "(" STRINGIZE(__LINE__) ") : error: " #desc)

#define STR_CAT(a,b)            a##b
#define STR_CAT_DELAYED(a,b)   STR_CAT(a,b)
#define UNKNOWN(size) char STR_CAT_DELAYED(_unused_,__COUNTER__)[size]
#define PADDING UNKNOWN

#ifdef __cplusplus
#define CNATIVE extern "C"
#else
#define CNATIVE
#endif

#define toggle char
#define ext_boolean int

typedef enum EAO_RETURN : signed int {
    EAO_ONE_TIME_UPDATE = 2,
    EAO_OVERRIDE = 1,
    EAO_CONTINUE = 0,
    EAO_FAIL = -1
} EAO_RETURN;

typedef enum CMD_RETURN : signed int {
    CMD_FAIL = -1,
    CMD_NO_MATCH = 0,
    CMD_SUCCESS = 1,
    CMD_SUCCESS_DELAY = 2
} CMD_RETURN;

typedef enum e_boolean : signed char {
    BOOL_FAIL = -1,
    BOOL_FALSE = 0,
    BOOL_TRUE = 1
} e_boolean;

#define CALL_MEMBER_FN(self, FUNC, ...) self->FUNC(self, ## __VA_ARGS__)

#include "C\cseries\cseries.h"
//Vanilla Tags
#include "C\tags\tag_include.h"


#include "C\struct.h"
#include "C\util.h"
#include "C\hext.h"

#ifdef EXT_ICINIFILE
#include "C\iniFile.h"
#endif

#ifdef EXT_IDATABASE
#include "C\database.h"
#endif

#include "C\object.h"

#include "C\player.h"
#ifdef EXT_IUTIL
typedef CMD_RETURN (WINAPIC *CmdFunc)(PlayerInfo plI, ArgContainer* arg, MSG_PROTOCOL protocolMsg, unsigned int idTimer, bool* showChat);
#endif
#ifdef EXT_IADMIN
#include "C\admin.h"
#endif

#ifdef EXT_ICOMMAND
#include "C\command.h"
#endif

#ifdef __cplusplus
CNATIVE {
#endif
    
    #pragma pack(push,1)
    typedef struct addon_section_names {
        wchar_t sect_name1[24];
        wchar_t sect_name2[24];
        wchar_t sect_name3[24];
        wchar_t sect_name4[24];
        wchar_t sect_name5[24];
    } addon_section_names;
    typedef struct addon_info {
        wchar_t    name[128];
        wchar_t    version[15];
        wchar_t    author[128];
        wchar_t    description[255];
        wchar_t    config_folder[24];
        addon_section_names sectors;
    } addon_info;
    typedef struct addon_version {
        unsigned short size;            //Used by sizeof(versionEAO);
        unsigned short requiredAPI;     //API requirement revision (Including command functions)
        unsigned short general;         //General revision specifically for events in Halo.
        unsigned short version;         //addon_version revision
        unsigned short pIHaloEngine;    //Halo Engine interface revision
        unsigned short pIObject;        //Object interface revision
        unsigned short pIPlayer;        //Player interface revision
        unsigned short pIAdmin;         //Admin interface revision
        unsigned short pICommand;       //Command interface revision
        unsigned short pIDatabase;      //Database interface revision
        unsigned short pIDBStmt;        //Database Statement interface revision
        unsigned short pICIniFile;      //CiniFile interface revision
        unsigned short pITimer;         //Timer interface revision
        unsigned short pIUtil;          //Util interface revision
        unsigned short pINetwork;       //Network interface revision - reserved (DO NOT USE!)
        unsigned short pISound;         //Sound interface revision - reserved (DO NOT USE!)
        unsigned short pIDirectX9;      //DirectX9 interface revision - reserved (DO NOT USE!)
        unsigned short reserved1;       //reserved
        unsigned short reserved2;       //reserved
        unsigned short reserved3;       //reserved
        unsigned short hkDatabase;      //Database hook revision
        unsigned short hkTimer;         //Timer hook revision
        unsigned short hkExternal;      //External account revision - reserved (DO NOT USE!)
        unsigned short reserved4;       //reserved
        unsigned short reserved5;       //reserved
        unsigned short reserved6;       //reserved
        unsigned short reserved7;       //reserved
        unsigned short reserved8;       //reserved
    } addon_version;
    #pragma pack(pop)

#ifdef EXT_ITIMER
#ifndef EXT_HKTIMER
#pragma WARNING("If you're using addon_version structure, recommended to include EXT_HKTIMER since EXT_ITIMER required hooks to be include.")
#endif
    typedef struct ITimer {
        /// <summary>
        /// Register a timer event delay.
        /// </summary>
        /// <param name="hash">Add-on unique ID.</param>
        /// <param name="plI">Bind to specific player or use null for general.</param>
        /// <param name="execTime">Amount of ticks later to execute a timer event. (1 tick = 1/30 second)</param>
        /// <returns>Return ID of timer event.</returns>
        unsigned long (*m_add)(unsigned int hash, PlayerInfo* plI, unsigned int execTime);
        /// <summary>
        /// Remove a timer event.
        /// </summary>
        /// <param name="hash">Add-on unique ID.</param>
        /// <param name="id">Can be used only with registered ID number bind to specific Add-on.</param>
        /// <returns>Return true or false if unable to reload Add-on.</returns>
        void (*m_delete)(unsigned int hash, unsigned int id);
    } ITimer;
    CNATIVE dllport ITimer* getITimer(unsigned int hash);
#endif
static addon_version EAOversion = {
    sizeof(addon_version),  //size
                6,              //requiredAPI - API requirement revision (Including command interface)
                5,              //general - General revision specifically for events in Halo.
                4,              //addon_version - format revision requirement enforced if needed
#ifdef EXT_IHALOENGINE
                2,              //pIHaloEngine - Halo Engine class revision
#else
                0,              //pIHaloEngine - excluded
#endif
#ifdef EXT_IOBJECT
                4,              //pIObject - Object class revision
#else
                0,              //pIObject - excluded
#endif
#ifdef EXT_IPLAYER
                5,              //pIPlayer - Player class revision
#else
                0,              //pIPlayer - excluded
#endif
#ifdef EXT_IADMIN
                2,              //pIAdmin - Admin class revision
#else
                0,              //pIAdmin - excluded
#endif
#ifdef EXT_ICOMMAND
                2,              //pICommand - Command class revision
#else
                0,              //pICommand - excluded
#endif
#ifdef EXT_IDATABASE
                4,              //pIDatabase - Database class revision
#else
                0,              //pIDatabase - excluded
#endif
#ifdef EXT_IDATABASESTATEMENT
                4,              //pIDBStmt - Database Statement class revision
#else
                0,              //pIDBStmt - excluded
#endif
#ifdef EXT_ICINIFILE
                3,              //iniFile - CiniFile class revision
#else
                0,              //iniFile - excluded
#endif
#ifdef EXT_ITIMER
                1,              //pITimer - Timer class revision
#else
                0,              //pITimer - excluded
#endif
#ifdef EXT_IUTIL
                1,              //pIUtil - IUtil class revision
#else
                0,              //pIUtil - excluded
#endif
#ifdef EXT_INETWORK
                0,              //pINetwork - INetwork class revision
#else
                0,              //pINetwork - excluded
#endif
#ifdef EXT_ISOUND
                0,              //pISound - ISound class revision
#else
                0,              //pISound - excluded
#endif
#ifdef EXT_IDIRECTX9
                0,              //pIDirectX9 - IDirectX9 class revision
#else
                0,              //pIDirectX9 - excluded
#endif
#ifdef EXT_RESERVED
                0,              //reserved1 - reserved1 class revision
#else
                0,              //reserved1 - excluded
#endif
#ifdef EXT_RESERVED
                0,              //reserved2 - reserved2 class revision
#else
                0,              //reserved2 - excluded
#endif
#ifdef EXT_RESERVED
                0,              //reserved3 - reserved3 class revision
#else
                0,              //reserved3 - excluded
#endif
#ifdef EXT_HKDATABASE
                4,              //hkDatabase - Database hook revision
#else
                0,              //hkDatabase - excluded
#endif
#ifdef EXT_HKTIMER
                1,              //hkTimer - Timer hook revision
#else
                0,              //hkTimer - excluded
#endif
#ifdef EXT_HKEXTERNAL
                0,              //hkExternal - External account revision (for Remote Control or other external possiblities)
#else
                0,              //hkExternal - excluded
#endif
                0,              //reserved5
                0,              //reserved6
                0,              //reserved7
                0,              //reserved8
                0 };            //reserved9
static_assert_check(sizeof(addon_version)==0x38, "Error, incorrect size of addon_version struct");

#ifdef __cplusplus
}
#endif

#ifdef EXT_IHALOENGINE
#include "C\haloEngine.h"
#endif

#endif