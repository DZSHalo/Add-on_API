using System;
using System.Runtime.InteropServices;

public class HEXT {
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
    public readonly double PI = 3.141592653589793f;
}
