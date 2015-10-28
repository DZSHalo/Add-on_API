module Add_on_API.D.haloEngine;

import Add_on_API.Add_on_API;

static if(__traits(compiles, EXT_IHALOENGINE)) {
// #define haloEngineH

    enum REJECT_CODE:ubyte {
        CANT_JOIN_SERVER = 0,       //0
        INVALID_CONNECTION_REQUEST, //1
        PASSWORD_REJECTED,          //2
        SERVER_IS_FULL,             //3
        CD_KEY_INVALID,             //4
        CD_KEY_INUSED,              //5
        OP_BANNED,                  //6
        OP_KICKED,                  //7
        VIDEO_TEST,                 //8
        CHECKPOINT_SAVED,           //9
        ADDRESS_INVALID,            //10
        PROFILE_REQUIRED,           //11
        INCOMPATIBLE_NETWORK,       //12
        OLDER_CLIENT_VERSION,       //13
        NEWER_CLIENT_VERSION,       //14
        ADMIN_REQUIRED_PATCH,       //15
        REQUEST_DELETE_SAVED,       //16
    }
    enum HALO_VERSION:ubyte {
        HV_UNKNOWN = 0,
        HV_TRIAL,   //1,
        HV_PC,      //2,
        HV_CE,      //3
    }

    version(DIRECT3D_VERSION) {
        alias DIRECTX9 = IDirect3D9;
    } else {
        alias DIRECTX9 = uint;
    }
    version(DIRECTINPUT_VERSION) {
        alias DIRECTI8 = IDirectInput8;
    } else {
        alias DIRECTI8 = uint;
    }
    version(DIRECTSOUND_VERSION) {
        alias DIRECTS8 = IDirectSound;
    } else {
        alias DIRECTS8 = uint;
    }

    extern (C) struct IHaloEngine { // For Add-on API interface support
        s_server_header* serverHeader;
        s_player_reserved_slot* playerReserved;
        s_machine_slot* machineHeader;
        ubyte machineHeaderSize;
        HALO_VERSION haloGameVersion;
        bool isDedi;
        ubyte reserved0;
        uint * player_base;
        s_gametype* gameTypeLive;
        s_cheat_header* cheatHeader;
        s_map_header* mapCurrent;
        s_console_header* console;
        uint* gameUpTimeLive; //1 sec = 60 ticks
        uint* mapUpTimeLive; //1 sec = 30 ticks
        uint* mapTimeLimitLive;
        uint* mapTimeLimitPermament;
        s_console_color_list* consoleColor;
        DIRECTX9*  DirectX9;
        DIRECTI8*  DirectInput8;
        DIRECTS8*  DirectSound8;
        bool* cheatVEject;
        GameTypeGlobals* gameTypeGlobals;
        s_map_status** mapStatus;
        //Halo Simulate Functions Begin
        /*
         * To prepare a packet to send to player(s).
         * Params:
         * packet_data = To build a buffer to send. Does not accept null or unallocate memory.
         * arg1 = Unknown, usually 0 (Use at your risk!)
         * packettype = Unknown, do not have a list for this. (Use at your risk!)
         * arg3 = Unknown, usually 0 (Use at your risk!)
         * data_pointer = Any data you want send to player/server.
         * arg4 = Unknown, usually 0 (Use at your risk!)
         * arg5 = Unknown, usually 1 (Use at your risk!)
         * arg6 = Unknown, usually 0 (Use at your risk!)
         * Returns: Return unique ID to be used to add in a queue functions.*/
        uint function(ubyte* output, uint arg1, uint packettype, uint arg3, ubyte* dataPtr, uint arg4, uint arg5, uint arg6) m_build_packet;
        /*
         * To add a queue send to specific player.
         * Params:
         * machine_index = Unique machine_index from s_machine structure.
         * packet = Only use packet_data buffer from m_build_packet.
         * packetCode = The return value from m_build_packet to be used.
         * arg1 = Unknown, usually 1 (Use at your risk!)
         * arg2 = Unknown, usually 1 (Use at your risk!)
         * arg3 = Unknown, usually 0 (Use at your risk!)
         * arg4 = Unknown, usually 1 (Use at your risk!)
         * arg5 = Unknown, usually 3 (Use at your risk!)
         * Returns: Does not return a value. (May will be changed later on.)*/
        void function(uint player, ubyte* packet, uint packetCode, uint arg1, uint arg2, uint arg3, uint arg4, uint arg5) m_add_packet_to_player_queue;
        /*
         * To add a queue send to all players.
         * Params:
         * packet_data = To build a buffer to send. Does not accept null or unallocate memory.
         * packettype = Unknown, do not have a list for this. (Use at your risk!)
         * arg1 = Unknown, usually 1 (Use at your risk!)
         * arg2 = Unknown, usually 1 (Use at your risk!)
         * arg3 = Unknown, usually 0 (Use at your risk!)
         * arg4 = Unknown, usually 1 (Use at your risk!)
         * arg5 = Unknown, usually 3 (Use at your risk!)
         * Returns: Does not return a value. (May will be changed later on.)*/
        void function(ubyte * packet, uint packetCode, uint arg1, uint arg2, uint arg3, uint arg4, uint arg5) m_add_packet_to_global_queue;
        /*
         * Dispatch a rcon message to specific player.
         * Params:
         * data = A message you would like to send.
         * plI = Specific player to receive this rcon message.
         * Returns: Does not return a value. (May will be changed later on.)*/
        void function(ref rconData  d, ref PlayerInfo  plI) m_dispatch_rcon;
        /*
         * Dispatch a chat message to specific player.
         * Params:
         * data = A message you would like to send.
         * len = Length of characters from data, maximum is 80 (0x50).
         * plI = Specific player to receive this chat message.
         * Returns: Does not return a value. (May will be changed later on.)*/
        void function(ref chatData  d, int len, ref PlayerInfo  plI) m_dispatch_player;
        /*
         * Dispatch a chat message to all players.
         * Params:
         * data = A message you would like to send.
         * len = Length of characters from data, maximum is 80 (0x50).
         * Returns: Does not return a value. (May will be changed later on.)*/
        void function(ref chatData  d, int len) m_dispatch_global;
        /*
         * To send a rejection code reason to player and disconnect player from host.
         * Params:
         * player = Pass down an active s_machine_slot pointer.
         * code = See REJECT_CODE for codes available to use.
         * Returns: Return true if successfully sent packet.*/
        bool function(s_machine_slot* mH, REJECT_CODE code) m_send_reject_code;
        /*
         * To set a server to idle state. (Only supportive for server.)
         * Returns: Return true if will set host to idling.*/
        bool function() m_set_idling;
        /*
         * To end a current game and move on to the next map.
         * Returns: Return true if host is changing to next map.*/
        bool function() m_map_next;
        /*
         * To execute native halo command. (May will be deprecate in future.)
         * Params:
         * command = Input a command.
         * Returns: Return true if execution is a success.*/
        bool function(const(char*) cmd) m_exec_command;
        /*
         * Get the current password for hosting un/lock.
         * Params:
         * pass = Return the current password. (Must be at least 8 characters long.)
         * Returns: Does not return a value.*/
        void function(wchar* pass) m_get_server_password;
        /*
         * Set the current password for hosting un/lock.
         * Params:
         * pass = Set the current password. (Maximum permitted is 8 characters long.)
         * Returns: Does not return a value.*/
        void function(wchar* pass) m_set_server_password;
        /*
         * Get the current rcon password for authorized players to execute command.
         * Params:
         * pass = Return the current rcon password. (Must be at least 8 characters long.)
         * Returns: Does not return a value.*/
        void function(char* pass) m_get_rcon_password;
        /*
         * Set the current rcon password for authorized players to execute command.
         * Params:
         * pass = Set the current rcon password. (Maximum permitted is 8 characters long.)
         * Returns: Does not return a value.*/
        void function(char* pass) m_set_rcon_password;
        //Halo Simulate Functions End
        /*
         * Obtain an Add-on information if able to find a match.
         * Params:
         * index = Input an Add-on index slot number.
         * getInfo = Output a matched Add-on or not.
         * Returns: Return true or false if unable find a match.*/
        bool function(uint index, ref addon_info  getInfo) m_ext_add_on_get_info_index;
        /*
         * Obtain an Add-on information if able to find a match.
         * Params:
         * name = Input name of an Add-on. (Maximum permitted is 128 characters long.)
         * getInfo = Output a matched Add-on or not.
         * Returns: Return true or false if unable find a match.*/
        bool function(wchar* name, ref addon_info  getInfo) m_ext_add_on_get_info_by_name;
        /*
         * Reload an Add-on while still running Halo.
         * Params:
         * name = Input name of an Add-on. (Maximum permitted is 128 characters long.)
         * Returns: Return true or false if unable to reload Add-on.*/
        bool function(wchar* name) m_ext_add_on_reload;
    };
    export extern(C) IHaloEngine* getIHaloEngine(uint hash);

}
