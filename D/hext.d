module D.hext;

import Add_on_API;

static const(char) colon = ':';
static const(char) newline = '\n';
static const(char) pipe = '|';
static const wchar commaW = ',';
static const wchar[] me = "me";
static const wchar backslash = '\\';
static const wchar dotW =  '.';

enum GAME_MODE: ushort {
    SINGLE = 0,
    MULTI,
    HOSTING
}

static struct GAME_MODE_S {
    bool SINGLE;
    bool MULTI;
    bool HOSTING;
    bool RESERVE0;
}

//* //This is not working due to "Symbol Undefined" for any of this variables.
//Error 42: Symbol Undefined _D1D4hext7modeAllxS1D4hext11GAME_MODE_S (const(D.hext.GAME_MODE_S) D.hext.modeAll)
static const GAME_MODE_S modeAll = { 1, 1, 1 };
static const GAME_MODE_S modeSingle = GAME_MODE_S(1, 0, 0);
static const GAME_MODE_S modeSingleMulti = GAME_MODE_S(1, 1, 0);
static const GAME_MODE_S modeSingleHost = GAME_MODE_S(1, 0, 1);
static const GAME_MODE_S modeMulti = GAME_MODE_S(0, 1, 0);
static const GAME_MODE_S modeMultiHost = GAME_MODE_S(0, 1, 1);
static const GAME_MODE_S modeHost = GAME_MODE_S(0, 0, 1);
//*/
static const(double) PI = 3.14159265358979323846;

enum PLAYER_VALIDATE {
    DEFAULT = 0,
    BYPASS,
    BANNED,
    PASS_REJECT
}

enum VEHICLE_RESPAWN {
    DEFAULT = -1,
    BYPASS,         // Do not process
    FORCE,          // Force default process
    RELOCATE,       // Relocate with given data.
    DESTROY         // Do not respawn, instead destroy it.
}

enum OBJECT_ATTEMPT {
    DEFAULT = -1,
    BYPASS,          // Do not process
    FORCE,           // Force default process
    ALTERNATE        // NOTE: Only for create attempt
}
