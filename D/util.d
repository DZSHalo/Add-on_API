module Add_on_API.D.util;

import Add_on_API.Add_on_API;

struct tm {
    int tm_sec;     /* seconds after the minute - [0,59] */
    int tm_min;     /* minutes after the hour - [0,59] */
    int tm_hour;    /* hours since midnight - [0,23] */
    int tm_mday;    /* day of the month - [1,31] */
    int tm_mon;     /* months since January - [0,11] */
    int tm_year;    /* years since 1900 */
    int tm_wday;    /* days since Sunday - [0,6] */
    int tm_yday;    /* days since January 1 - [0,365] */
    int tm_isdst;   /* daylight savings time flag */
};
static if(__traits(compiles, EXT_IUTIL)) {

// #define _TM_DEFINED

    struct ArgContainerVars {
        wchar * args[10];
        uint argc;
        //export extern (C) this();
          /* SYNTAX ERROR: (31): expected ; instead of operator */ 
    };
    extern(C) {
        export void ArgContainerVars_Constructor(ref ArgContainerVars vars);
        export void ArgContainerVars_Deconstructor(ref ArgContainerVars vars);
        export void ArgContainerVars_Set(ref ArgContainerVars vars, const wchar* arg);
        export void ArgContainerVars_Set_N(ref ArgContainerVars vars, const wchar* arg, int numArrayLink);
        export void ArgContainerVars_Copy(ref ArgContainerVars vars, ref const ArgContainerVars copy);
        //export void ArgContainerVars_Copy(ref ArgContainerVars vars);
        export wchar* ArgContainerVars_At(ref ArgContainerVars vars, uint i);
    }
    struct ArgContainer {
        public:
        ArgContainerVars vars;
        void init() {
            ArgContainerVars_Constructor(vars);
            //econsole << L"Created1" << endline();
        }
        /*ArgContainer::ArgContainer(const ArgContainer& cpyArg) {
        argc=0;
        for (vector<wchar*>::const_iterator iCpy = cpyArg.args.begin(); iCpy < cpyArg.args.end(); iCpy++) {
        args.push_back(*iCpy);
        argc++;
        }

        }*/
        this(const wchar* arg) {
            ArgContainerVars_Set(vars, arg);
        }
        this(const wchar* arg, int numArrayLink) {
            ArgContainerVars_Set_N(vars, arg, numArrayLink);
        }
        /*ArgContainer::ArgContainer(ArgContainer const &copy) {
        //econsole << L"Copied1" << endline();
        /*if (argc!=0) {
        this.~ArgContainer();
        }* /
        //Weirdly enough on Windows XP with blank value returned differently once called the function's.
        for (size_t i = 0; i < copy.argc; i++) {
        //econsole << L"Test3: " << copy.args[i] << endline();
        int length = wcslen(copy.args[i]);
        if (length == 0)
        length = 1;
        args[i] = (wchar*)calloc(length, 4); //Oddly uses 3 or higher instead of 2 base on sizeof(wchar) = 2
        wmemcpy_s(args[i], length, copy.args[i], length);
        //econsole << args.at(i] << L" vs " << copy.args[i) << endline();
        }
        ArgCreate++;
        argc = copy.argc;
        }*/
        wchar* opIndex(uint i) {
            return ArgContainerVars_At(this.vars, i);
        }
        void opAssign(ArgContainer copy) {
            ArgContainerVars_Copy(vars, copy.vars);
        }
        ~this() {
            ArgContainerVars_Deconstructor(this.vars);
        }
    };

    extern (C) struct IUtil {
        void* function(size_t Size) AllocMem;
        void function(void* Address) FreeMem;
        void function(const char* charA, int len, wchar* charW) toCharW;
        void function(const wchar* charW, int len, char* charA) toCharA;
        toggle function(const char* str) StrToBooleanA;
        toggle function(const wchar* str) StrToBooleanW;
        e_color_team_index function(const char* str) StrToTeamA;
        e_color_team_index function(const wchar* str) StrToTeamW;
        void function(char* regStr) ReplaceA;
        void function(wchar* regStr) ReplaceW;
        void function(char* regStr) ReplaceUndoA;
        void function(wchar* regStr) ReplaceUndoW;
        bool function(const char* str) isnumberA;
        bool function(const wchar* str) isnumberW;
        bool function(const char* str) ishashA;
        bool function(const wchar* str) ishashW;
        void function(char* regStr, int len, int pos, int lenShift, bool leftRight) shiftStrA;
        void function(wchar* regStr, int len, int pos, int lenShift, bool leftRight) shiftStrW;
        void function(wchar* regStr, bool isDB) regexReplaceW;
        bool function(wchar* srcStr, wchar* regex) regexMatchW;
        bool function(wchar* srcStr, wchar* regex) regexiMatchW;


        bool function(const char* _Format, char * ArgList, char* writeTo) FormatVarArgsA;
        bool function(const wchar* _Format, char * ArgList, wchar* writeTo) FormatVarArgsW;
        bool function(const char* dest, const char* src) findSubStrFirstA;
        bool function(const wchar* dest, const wchar* src) findSubStrFirstW;

        bool function(const char* str) islettersA;
        bool function(const wchar* str) islettersW;

        bool function(const char* str) isfloatA;
        bool function(const wchar* str) isfloatW;
        bool function(const char* str) isdoubleA;
        bool function(const wchar* str) isdoubleW;
        int function(wchar* dest, size_t len, const wchar* src) strcatW;
        int function(char* dest, size_t len, const char* src) strcatA;
        void function(char* str, wchar* wstr) str_to_wstr;
        bool function(const wchar* str1, const wchar* str2) strcmpW;
        bool function(const char* str1, const char* str2) strcmpA;
        bool function(const wchar* str1, const wchar* str2) stricmpW;
        bool function(const char* str1, const char* str2) stricmpA;
    };
    extern (C) IUtil* getIUtil(uint hash);

    struct haloConsole {
        int r;
        char output[1024];
    };
// #pragma pack(pop)


}
