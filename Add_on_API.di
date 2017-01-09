module Add_on_API;


//NOTE: This is required in order to function correctly for enums requirement to compile
//what's neccessary and ensure developer of Add-on include specific functionality.
public import global;

//#define dllport __declspec(dllimport)
//#define dllAPI __declspec(dllexport)
//#define WINAPIC __cdecl
/*
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
#define WARNING(desc) message(__FILE__ "(" STRINGIZE(__LINE__) ") : Warning: " #desc)

#define STR_CAT(a,b)            a##b
#define STR_CAT_DELAYED(a,b)   STR_CAT(a,b)
#define UNKNOWN(size) char STR_CAT_DELAYED(_unused_,__COUNTER__)[size]
#define PADDING UNKNOWN
*/
alias toggle = char;
alias ext_boolean = int;

enum EAO_RETURN:int {
    ONE_TIME_UPDATE = 2,
    OVERRIDE = 1,
    CONTINUE = 0,
    FAIL = -1
}
enum CMD_RETURN: int {
    FAIL = -1,
    NO_MATCH = 0,
    SUCCESS = 1,
    SUCCESS_DELAY = 2
}
enum e_boolean : byte {
    FAIL = -1,
    FALSE = 0,
    TRUE = 1
}

//#define CALL_MEMBER_FN(self, FUNC, ...) self->FUNC(self, ## __VA_ARGS__)

//This works
//pragma(msg, __traits(compiles, EXT_IHALOENGINE));

public import D.cseries.cseries;
public import D.tags.tag_include;

public import D.util;
public import D.hext;
public import D.structs;
//public import D.struct_tags;

static if (__traits(compiles, EXT_ICINIFILE)) {
public import D.iniFile;
}

static if (__traits(compiles, EXT_IDATABASE)) {
public import D.database;
}

public import D.object;

public import D.player;
static if (__traits(compiles, EXT_IUTIL)) {
alias CmdFunc = extern (C) CMD_RETURN function(PlayerInfo plI, ArgContainer* arg,MSG_PROTOCOL chatRconRemote, uint idTimer, bool* showChat);
}
//alias CmdFunc = extern (C) toggle function();

static if (__traits(compiles, EXT_IADMIN)) {
public import D.admin;
}

static if (__traits(compiles, EXT_ICOMMAND)) {
public import D.command;
}
//extern(C) void haloOutput(int r, const(char) *text,...);

    // #pragma pack(push,1)
 struct addon_section_names {
    wchar[24] sect_name1;
    wchar[24] sect_name2;
    wchar[24] sect_name3;
    wchar[24] sect_name4;
    wchar[24] sect_name5;
}
struct addon_info {
    wchar[128]  name;
    wchar[15]   ver;
    wchar[128]  author;
    wchar[255]  description;
    wchar[24]   config_folder;
    addon_section_names  sectors;
}
struct addon_version {
    ushort size = addon_version.sizeof;  //Used by sizeof(versionEAO);
    ushort requiredAPI = 6;     //API requirement revision (Including command functions)
    ushort general = 5;         //General revision specifically for events in Halo.
    static if (__traits(compiles, EXT_ICINIFILE)) {
        ushort pICIniFile = 3;      //CiniFile interface revision
    } else {
        ushort pICIniFile = 0;              //iniFile - excluded
    }
    static if (__traits(compiles, EXT_IDATABASE)) {
        ushort pIDatabase = 4;      //Database interface revision
    } else {
        ushort pIDatabase = 0;              //pIDatabase - excluded
    }
    static if (__traits(compiles, EXT_IEXTERNAL)) {
        ushort external = 0;        //External account revision (for Remote Control or other external possiblities)
    } else {
        ushort external = 0;              //external - excluded
    }
    static if (__traits(compiles, EXT_IHALOENGINE)) {
        ushort pIHaloEngine = 2;    //Halo Engine interface revision
    } else {
        ushort pIHaloEngine = 0;              //pIHaloEngine - excluded
    }
    static if (__traits(compiles, EXT_IOBJECT)) {
        ushort pIObject = 4;        //Object interface revision
    } else {
        ushort pIObject = 0;              //pIObject - excluded
    }
    static if (__traits(compiles, EXT_IPLAYER)) {
        ushort pIPlayer = 5;        //Player interface revision
    } else {
        ushort pIPlayer = 0;              //pIPlayer - excluded
    }
    static if (__traits(compiles, EXT_ICOMMAND)) {
        ushort pICommand = 2;       //Command interface revision
    } else {
        ushort pICommand = 0;              //pICommand - excluded
    }
    static if (__traits(compiles, EXT_ITIMER)) {
        ushort pITimer = 1;         //Timer interface revision
    } else {
        ushort pITimer = 0;              //pITimer - excluded
    }
    static if (__traits(compiles, EXT_IADMIN)) {
        ushort pIAdmin = 2;         //Admin interface revision
    } else {
        ushort pIAdmin = 0;              //pIAdmin - excluded
    }
    static if (__traits(compiles, EXT_IUTIL)) {
        ushort pIUtil = 1;          //Util interface revision
    } else {
        ushort pIUtil = 0;              //pIUtil - excluded
    }
    ushort reserved3 = 0;       //reserved
    ushort reserved4 = 0;       //reserved
    ushort reserved5 = 0;       //reserved
}
// #pragma pack(pop)

static if (__traits(compiles, EXT_ITIMER)) {
    extern(C) struct ITimer {
        /*
         * Register a timer event delay.
         * Params:
         * hash = Add-on unique ID.
         * plI = Bind to specific player or use null for general.
         * execTime = Amount of ticks later to execute a timer event. (1 tick = 1/30 second)
         * Returns: Return ID of timer event.
         */
        uint function(uint hash, PlayerInfo* plI, uint execTime) m_add;
        /*
         * Remove a timer event.
         * Params:
         * hash = Add-on unique ID.
         * id = Can be used only with registered ID number bind to specific Add-on.
         * Returns: Return true or false if unable to reload Add-on.
         */
        void function(uint hash, uint id) m_delete;
    }
    export extern(C) ITimer* getITimer(uint hash);
}
static addon_version EAOversion;
//static_assert_check(sizeof(addon_version)==32, "Error, incorrect size of addon_version struct");

static if (__traits(compiles, EXT_IHALOENGINE)) {
public import D.haloEngine;
}
