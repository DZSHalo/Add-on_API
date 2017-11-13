module D.util;

import Add_on_API;
public import core.sys.windows.oaidl;
public import core.sys.windows.wtypes : VARENUM;
import core.stdc.config: c_long, c_ulong;
public import core.stdc.time : tm;

static if(!__traits(compiles, tm)) {
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
}
}
static if(__traits(compiles, EXT_IUTIL)) {

// #define _TM_DEFINED

pragma(inline) void VARIANTstr(VARIANT* var, char* val) {
    var.vt = VARENUM.VT_LPSTR;
    var.pcVal = val;
}
pragma(inline) void VARIANTwstr(VARIANT* var, wchar* val) {
    var.vt = VARENUM.VT_LPWSTR;
    var.bstrVal = val;
}
pragma(inline) void VARIANTbool(VARIANT* var, const bool val) {
    var.vt = VARENUM.VT_BOOL;
    if (val)
        var.boolVal = -1;
    else
        var.boolVal = 0;
}
pragma(inline) void VARIANTshort(VARIANT* var, const short val) {
    var.vt = VARENUM.VT_I2;
    var.iVal = val;
}
pragma(inline) void VARIANTushort(VARIANT* var, const ushort val) {
    var.vt = VARENUM.VT_UI2;
    var.uiVal = val;
}
pragma(inline) void VARIANTint(VARIANT* var, const int val) {
    var.vt = VARENUM.VT_I4;
    var.intVal = val;
}
pragma(inline) void VARIANTuint(VARIANT* var, const uint val) {
    var.vt = VARENUM.VT_UI4;
    var.uintVal = val;
}
pragma(inline) void VARIANTlong(VARIANT* var, const c_long val) {
    var.vt = VARENUM.VT_I8;
    var.lVal = val;
}
pragma(inline) void VARIANTulong(VARIANT* var, const c_ulong val) {
    var.vt = VARENUM.VT_UI8;
    var.ulVal = val;
}
pragma(inline) void VARIANTfloat(VARIANT* var, const float val) {
    var.vt = VARENUM.VT_R4;
    var.fltVal = val;
}
pragma(inline) void VARIANTdouble(VARIANT* var, const double val) {
    var.vt = VARENUM.VT_R8;
    var.dblVal = val;
}

struct ArgContainerVars {
    wchar[256] arg;
    uint arg_len;
    wchar*[10] args;
    uint argc;
}
extern(C) {
    export void ArgContainerVars_Constructor(ref ArgContainerVars vars);
    export void ArgContainerVars_Deconstructor(ref ArgContainerVars vars);
    export void ArgContainerVars_Set(ref ArgContainerVars vars, const wchar* arg);
    export void ArgContainerVars_Set_N(ref ArgContainerVars vars, const wchar* arg, int numArrayLink);
    export void ArgContainerVars_Copy(ref ArgContainerVars vars, ref const ArgContainerVars copy);
    export wchar* ArgContainerVars_At(ref ArgContainerVars vars, uint i);
}
struct ArgContainer {
    public:
    ArgContainerVars vars;
    void init() {
        ArgContainerVars_Constructor(vars);
    }
    this(const wchar* arg) {
        ArgContainerVars_Set(vars, arg);
    }
    this(const wchar* arg, int numArrayLink) {
        ArgContainerVars_Set_N(vars, arg, numArrayLink);
    }
    wchar* opIndex(uint i) {
        return ArgContainerVars_At(this.vars, i);
    }
    void opAssign(ArgContainer copy) {
        ArgContainerVars_Copy(vars, copy.vars);
    }
    ~this() {
        ArgContainerVars_Deconstructor(this.vars);
    }
}
extern (C) struct IUtil {
    /*
        * Allocate memory.
        * Params:
        * Size = The size of allocate memory need to be used.
        * Returns: Return allocate memory.
        */
    void* function(uint Size) m_allocMem;
    /*
        * Free memory from allocate memory.
        * Params:
        * Address = Pointer of an allocate memory to be free from.
        * Returns: No return value.
        */
    void function(void* Address) m_freeMem;
    /*
        * Convert a string to wide string.
        * Params:
        * charA = String
        * len = Total length to convert from string.
        * charW = Buffered wide string
        * Returns: No return value.
        */
    void function(const char* charA, int len, wchar* charW) m_toCharW;
    /*
        * Convert a wide string to string.
        * Params:
        * charW = Wide string
        * len = Total length to convert from wide string.
        * charA = Buffered string
        * Returns: No return value.
        */
    void function(const wchar* charW, int len, char* charA) m_toCharA;
    /*
        * Translate a string into boolean.
        * Params:
        * str = String to translate from.
        * Returns: Return -1 if string doesn't have a translation to boolean.
        */
    e_boolean function(const char* str) m_strToBooleanA;
    /*
        * Translate a wide string into boolean.
        * Params:
        * str = Wide string to translate from.
        * Returns: Return -1 if string doesn't have a translation to boolean.
        */
    e_boolean function(const wchar* str) m_strToBooleanW;
    /*
        * Translate a string into team index.
        * Params:
        * str = String to translate from.
        * Returns: Return -1 if string doesn't have a translation to team index.
        */
    e_color_team_index function(const char* str) m_strToTeamA;
    /*
        * Translate a wide string into team index.
        * Params:
        * str = Wide string to translate from.
        * Returns: Return -1 if string doesn't have a translation to team index.
        */
    e_color_team_index function(const wchar* str) m_strToTeamW;
    /*
        * Format a current string to support escape characters if any.
        * Params:
        * regStr = String to format escape characters if any.
        * Returns: No return value.
        */
    void function(char* regStr) m_replaceA;
    /*
        * Format a current string to support escape characters if any.
        * Params:
        * regStr = String to format escape characters if any.
        * Returns: No return value.
        */
    void function(wchar* regStr) m_replaceW;
    /*
        * Undo format a current string to support escape characters if any.
        * Params:
        * regStr = String to undo format escape characters if any.
        * Returns: No return value.
        */
    void function(char* regStr) m_replaceUndoA;
    /*
        * Undo format a current string to support escape characters if any.
        * Params:
        * regStr = String to undo format escape characters if any.
        * Returns: No return value.
        */
    void function(wchar* regStr) m_replaceUndoW;
    /*
        * Verify if whole string contain digits.
        * Params:
        * str = String to check.
        * Returns: Return true if valid.
        */
    bool function(const char* str) m_isNumbersA;
    /*
        * Verify if whole wide string contain digits.
        * Params:
        * str = Wide string to check.
        * Returns: Return true if valid.
        */
    bool function(const wchar* str) m_isNumbersW;
    /*
        * Verify if whole string contain characters & digits.
        * Params:
        * str = String to check.
        * Returns: Return true if valid.
        */
    bool function(const char* str) m_isHashA;
    /*
        * Verify if whole wide string contain characters & digits.
        * Params:
        * str = Wide string to check.
        * Returns: Return true if valid.
        */
    bool function(const wchar* str) m_isHashW;
    /*
        * Move partial of string to left or right.
        * Params:
        * regStr = String to be shift.
        * len = Length of string to be move.
        * pos = Position of the string to be shift.
        * lenShift = Amount of length to shift left or right.
        * leftRight = True for shift to right and false for shift to left.
        * Returns: Return true for success, failed if one or more argument is invalid.
        */
    e_boolean function(char* regStr, int len, int pos, int lenShift, bool leftRight) m_shiftStrA;
    /*
        * Move partial of wide string to left or right.
        * Params:
        * regStr = Wide string to be shift.
        * len = Length of wide string to be move.
        * pos = Position of the wide string to be shift.
        * lenShift = Amount of length to shift left or right.
        * leftRight = True for shift to right and false for shift to left.
        * Returns: Return true for success, failed if one or more argument is invalid.
        */
    e_boolean function(wchar* regStr, int len, int pos, int lenShift, bool leftRight) m_shiftStrW;
    /*
        * Format a current string to support escape characters if any.
        * Params:
        * regStr = String to format escape characters if any.
        * isDB = True if goig to use escape characters in database query.
        * Returns: No return value.
        */
    void function(wchar* regStr, bool isDB) m_regexReplaceW;
    /*
        * Find a regular expression string against source string to be a match.
        * Params:
        * srcStr = Source string
        * regex = Regular expression string
        * Returns: Return true if is a match.
        */
    bool function(const wchar* srcStr, const wchar* regex) m_regexMatchW;
    /*
        * Find a regular expression string against source string to be a match.
        * Params:
        * srcStr = Source string
        * regex = Regular expression string
        * Returns: Return true if is a match.
        */
    bool function(const wchar* srcStr, const wchar* regex) m_regexiMatchW;

    /*
        * Format variable arguments list into given prefix string.
        * Params:
        * writeTo = Output string
        * _Format = Format message string
        * ArgList = Variable arguments list
        * Returns: Return true or false for format completion.
        */
    bool function(char* writeTo, const char* _Format, char * ArgList) m_formatVarArgsListA;
    /*
        * Format variable arguments list into given prefix string.
        * Params:
        * writeTo = Output string
        * _Format = Format message string
        * ArgList = Variable arguments list
        * Returns: Return true or false for format completion.
        */
    bool function(wchar* writeTo, const wchar* _Format, char * ArgList) m_formatVarArgsListW;
    /*
        * Compare beginning of case-senitive string against another string.
        * Params:
        * src = Source string compare against.
        * find = Find string to use for comparison.
        * Returns: Only return true if is a match.
        */
    bool function(const char* src, const char* find) m_findSubStrFirstA;
    /*
        * Compare beginning of case-senitive string against another string.
        * Params:
        * src = Source string compare against.
        * find = Find string to use for comparison.
        * Returns: Only return true if is a match.
        */
    bool function(const wchar* src, const wchar* find) m_findSubStrFirstW;

    /*
        * Test if string contains a letters or not.
        * Params:
        * str = String to test if is a letters.
        * Returns: Return true if is letters.
        */
    bool function(const char* str) m_isLettersA;
    /*
        * Test if string contains a letters or not.
        * Params:
        * str = String to test if is a letters.
        * Returns: Return true if is letters.
        */
    bool function(const wchar* str) m_isLettersW;

    /*
        * Test if string contains a float or not.
        * Params:
        * str = String to test if is a float.
        * Returns: Return true if is a float.
        */
    bool function(const char* str) m_isFloatA;
    /*
        * Test if string contains a float or not.
        * Params:
        * str = String to test if is a float.
        * Returns: Return true if is a float.
        */
    bool function(const wchar* str) m_isFloatW;
    /*
        * Test if string contains a double or not.
        * Params:
        * str = String to test if is a double.
        * Returns: Return true if is a double.
        */
    bool function(const char* str) m_isDoubleA;
    /*
        * Test if string contains a double or not.
        * Params:
        * str = String to test if is a double.
        * Returns: Return true if is a double.
        */
    bool function(const wchar* str) m_isDoubleW;
    /*
        * Append an existing string with new string.
        * Params:
        * dest = Destination to write an existing string.
        * len = Maximum size of an dest string.
        * src = New string to copy from.
        * Returns: Return 1 every time.
        */
    uint function(wchar* dest, size_t len, const wchar* src) m_strcatW;
    /*
        * Append an existing string with new string.
        * Params:
        * dest = Destination to write an existing string.
        * len = Maximum size of an dest string.
        * src = New string to copy from.
        * Returns: Return 1 every time.
        */
    uint function(char* dest, size_t len, const char* src) m_strcatA;
    /*
        * Convert a string to wide string.
        * Params:
        * str = String
        * wstr = Buffered wide string./param>
        * Returns: No return value.
        */
    void function(char* str, wchar* wstr) m_str_to_wstr;
    /*
        * Case-senitive string to compare against another string..
        * Params:
        * str1 = String #1 to compare against.
        * str2 = String #2 to compare against.
        * Returns: Only return true if is a match.
        */
    bool function(const wchar* str1, const wchar* str2) m_strcmpW;
    /*
        * Case-senitive string to compare against another string..
        * Params:
        * str1 = String #1 to compare against.
        * str2 = String #2 to compare against.
        * Returns: Only return true if is a match.
        */
    bool function(const char* str1, const char* str2) m_strcmpA;
    /*
        * Case-insenitive string to compare against another string.
        * Params:
        * str1 = String #1 to compare against.
        * str2 = String #2 to compare against.
        * Returns: Only return true if is a match.
        */
    bool function(const wchar* str1, const wchar* str2) m_stricmpW;
    /*
        * Case-insenitive string to compare against another string.
        * Params:
        * str1 = String #1 to compare against.
        * str2 = String #2 to compare against.
        * Returns: Only return true if is a match.
        */
    bool function(const char* str1, const char* str2) m_stricmpA;
    /*
        * Check if a directory exist.
        * Params:
        * pathStr = Must have directory name.
        * errorCode = Given error code if failed.
        * Returns: Return true if directory exist, false with given errorCode.
        */
    bool function(const wchar* str, uint* errorCode) m_isDirExist;
    /*
        * Check if a file exist.
        * Params:
        * pathStr = Must have directory (optional) and file name.
        * errorCode = Given error code if failed.
        * Returns: Return true if file exist, false with given errorCode.
        */
    bool function(const wchar* str, uint* errorCode) m_isFileExist;
    /*
        * Format variable arguments into given prefix string.
        * Params:
        * writeTo = Output string
        * _Format = Format message string
        * ... = Variable arguments
        * Returns: Return true or false for format completion.
    */
    bool function(char* writeTo, const char* _Format, ...) m_formatVarArgsA;
    /*
        * Format variable arguments into given prefix string.
        * Params:
        * writeTo = Output string
        * _Format = Format message string
        * ... = Variable arguments
        * Returns: Return true or false for format completion.
        */
    bool function(wchar* writeTo, const wchar* _Format, ...) m_formatVarArgsW;
    /*
        * Format variant arguments into a custom prefix string.
        * Params:
        * outputStr = Output string
        * maxOutput = Maximum size of output string.
        * _Format = Format custom message string
        * argTotal = Must be equivalent to argList's total of arrays.
        * argList = Variant arguments in array format.
        * Returns: Return true or false for format completion.
    */
    bool function(wchar* outputStr, uint maxOutput, const wchar* _Format, uint argTotal, VARIANT* argList) m_formatVariantW;
}
extern (C) IUtil* getIUtil(uint hash);
__gshared IUtil* pIUtil = null;

struct haloConsole {
    int r;
    char[1024] output;
}
//TODO: Untested dynamicStack...
class d_stack(T) {
    T* d_type;
    d_stack!(T)* next_d_type;
    void init(T) {
        d_type = null;
        next_d_type = null;
    }
}
class dynamicStack(T) : d_stack!(T) {
    private:
    d_stack!(T)* stack = null;
    public:
    /*void init(T)() {
        stack = 0;
    }*/
    ~this() {
        this.clear();
    }
    void push_back(T)(T type) {
        if (!stack) {
            stack = cast(d_stack!(T)*)pIUtil.m_AllocMem(d_stack!(T).sizeof);
            stack.d_type = cast(T*)pIUtil.m_AllocMem(T.sizeof);
            *stack.d_type = type;
            stack.next_d_type = null;
        } else {
            d_stack!(T)* src = stack;
            while (src.next_d_type) {
                src = src.next_d_type;
            }
            d_stack!(T)* newSrc = cast(d_stack!(T)*)pIUtil.m_AllocMem(d_stack!(T).sizeof);
            newSrc.d_type = cast(T*)pIUtil.m_AllocMem(T.sizeof);
            *newSrc.d_type = type;
            newSrc.next_d_type = null;
            src.next_d_type = newSrc;
        }
    }
    void pop_back() {
        if (stack) {
            d_stack!(T)* src = stack;
            d_stack!(T)* nextSrc = stack.next_d_type;
            pIUtil.m_FreeMem(stack.d_type);
            pIUtil.m_FreeMem(cast(void*)stack);
            stack = nextSrc;
        }
    }
    bool remove(uint i) {
        bool isDel = 1;
        d_stack!(T)* src = stack;
        d_stack!(T)* prevSrc = null;
        while (i>0) {
            if (!is(src ==typeof(null)) && !is(src.next_d_type ==typeof(null))) {
                prevSrc = src;
                src = src.next_d_type;
            }
            else {
                isDel = 0;
                break;
            }
            i--;
        }
        if (isDel) {
            if (!is(prevSrc ==typeof(null))) {
                prevSrc.next_d_type = src.next_d_type;
            }
            else
                stack = src.next_d_type;
            pIUtil.m_FreeMem(cast(void*)src.d_type);
            pIUtil.m_FreeMem(cast(void*)src);
        }
        return isDel;
    }
    T* opIndex(size_t i) {
        d_stack!(T)* src = stack;
        while (i>0) {
            if (src && src.next_d_type ) {
                src = src.next_d_type;
            }
            else {
                src = null;
                break;
            }
            i--;
        }
        if (!is(src ==typeof(null)))
            return src.d_type;
        else
            return null;
    }
    void clear() {
        if (stack) {
            d_stack!(T)* src = stack;
            d_stack!(T)* holder = null;
            while (src) {
                if (src.d_type)
                    pIUtil.m_FreeMem(src.d_type);
                holder = src;
                src = src.next_d_type;
                pIUtil.m_FreeMem(holder);
            }
            stack = null;
        }
    }
}
}
