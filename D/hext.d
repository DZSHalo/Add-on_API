module Add_on_API.D.hext;

import Add_on_API.Add_on_API;


static struct GAME_MODE_S {
    bool GAME_SINGLE;
    bool GAME_MULTI;
    bool GAME_HOSTING;
    bool GAME_RESERVE0;
};

static const(char) colon = ':';
static const(char) newline = '\n';
static const(char) pipe = '|';
static const wchar commaW = ',';
static const wchar me[] = "me";
static const wchar backslash = '\\';
static const wchar dotW =  '.';

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
static const(double) PI = 3.141592653589793f;