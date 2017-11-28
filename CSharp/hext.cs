using System;
using System.Runtime.InteropServices;

public class HEXT {
    public static char colon = ':';
    public static char newline = '\n';
    public static char pipe = '|';
    public static char comma = ',';
    public static string me = "me";
    public static char backslash = '\\';
    public static char dot = '.';

    public enum GAME_MODE : ushort {
        SINGLE = 0,
        MULTI,
        HOSTING
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct GAME_MODE_S {
        [MarshalAs(UnmanagedType.I1)]
        public bool SINGLE;
        [MarshalAs(UnmanagedType.I1)]
        public bool MULTI;
        [MarshalAs(UnmanagedType.I1)]
        public bool HOSTING;
        [MarshalAs(UnmanagedType.I1)]
        public bool RESERVE0;
        public GAME_MODE_S(bool single, bool multi, bool hosting, bool reserve0) {
            SINGLE = single;
            MULTI = multi;
            HOSTING = hosting;
            RESERVE0 = reserve0;
        }
    }
    //TODO: is static good enough for GAME_MODE_S defined variables?
    public static GAME_MODE_S modeAll = new GAME_MODE_S(true, true, true, true);
    public static GAME_MODE_S modeSingle = new GAME_MODE_S(true, false, false, false);
    public static GAME_MODE_S modeSingleMulti = new GAME_MODE_S(true, true, false, false);
    public static GAME_MODE_S modeSingleHost = new GAME_MODE_S(true, false, true, false);
    public static GAME_MODE_S modeMulti = new GAME_MODE_S(false, true, false, false);
    public static GAME_MODE_S modeMultiHost = new GAME_MODE_S(false, true, true, false);
    public static GAME_MODE_S modeHost = new GAME_MODE_S(false, false, true, false);
    public readonly double PI = 3.14159265358979323846;

    public enum PLAYER_VALIDATE {
        DEFAULT = 0,
        BYPASS,
        BANNED,
        PASS_REJECT
    }

    public enum VEHICLE_RESPAWN {
        DEFAULT = -1,
        BYPASS,         // Do not process
        FORCE,          // Force default process
        RELOCATE,       // Relocate with given data.
        DESTROY         // Do not respawn, instead destroy it.
    }

    public enum OBJECT_ATTEMPT {
        DEFAULT = -1,
        BYPASS,          // Do not process
        FORCE,           // Force default process
        ALTERNATE        // NOTE: Only for create attempt
    }
}
