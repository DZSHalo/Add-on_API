#ifndef hextH
#define hextH


#ifdef __cplusplus
CNATIVE {
#endif

    static const char colon = ':';
    static const char newline = '\n';
    static const char pipe = '|';
    static const wchar_t commaW = L',';
    static const wchar_t me[] = L"me";
    static const wchar_t backslash = L'\\';
    static const wchar_t dotW = L'.';
#ifdef __cplusplus
    typedef enum GAME_MODE : unsigned short {
        GAME_SINGLE = 0,
        GAME_MULTI,
        GAME_HOSTING
    } GAME_MODE;
#else
    typedef unsigned short GAME_MODE;
    static const GAME_MODE GAME_SINGLE = 0;
    static const GAME_MODE GAME_MULTI = 1;
    static const GAME_MODE GAME_HOSTING = 2;
#endif
    typedef struct GAME_MODE_S {
        bool SINGLE;
        bool MULTI;
        bool HOSTING;
        bool RESERVE0;
    } GAME_MODE_S;

    static GAME_MODE_S modeAll = { 1, 1, 1 };
    static GAME_MODE_S modeSingle = { 1, 0, 0 };
    static GAME_MODE_S modeSingleMulti = { 1, 1, 0 };
    static GAME_MODE_S modeSingleHost = { 1, 0, 1 };
    static GAME_MODE_S modeMulti = { 0, 1, 0 };
    static GAME_MODE_S modeMultiHost = { 0, 1, 1 };
    static GAME_MODE_S modeHost = { 0, 0, 1 };
    static const double PI = 3.14159265358979323846;

    typedef enum PLAYER_VALIDATE {
        PLAYER_VALIDATE_DEFAULT = 0,
        PLAYER_VALIDATE_BYPASS,
        PLAYER_VALIDATE_BANNED,
        PLAYER_VALIDATE_PASS_REJECT
    } PLAYER_VALIDATE;
#ifdef __cplusplus
}
#endif
#endif
