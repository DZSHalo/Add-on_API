#ifndef haloEngineH
#define haloEngineH

enum REJECT_CODE:unsigned char {
    REJECT_CANT_JOIN_SERVER = 0,       //0
    REJECT_INVALID_CONNECTION_REQUEST, //1
    REJECT_PASSWORD_REJECTED,          //2
    REJECT_SERVER_IS_FULL,             //3
    REJECT_CD_KEY_INVALID,             //4
    REJECT_CD_KEY_INUSED,              //5
    REJECT_OP_BANNED,                  //6
    REJECT_OP_KICKED,                  //7
    REJECT_VIDEO_TEST,                 //8
    REJECT_CHECKPOINT_SAVED,           //9
    REJECT_ADDRESS_INVALID,            //10
    REJECT_PROFILE_REQUIRED,           //11
    REJECT_INCOMPATIBLE_NETWORK,       //12
    REJECT_OLDER_player_VERSION,       //13
    REJECT_NEWER_player_VERSION,       //14
    REJECT_ADMIN_REQUIRED_PATCH,       //15
    REJECT_REQUEST_DELETE_SAVED,       //16
};

enum HALO_VERSION:unsigned char {
    HV_UNKNOWN = 0,
    HV_TRIAL,   //1,
    HV_PC,      //2,
    HV_CE,      //3
};

#ifndef DIRECT3D_VERSION
#define DIRECTX9 unsigned int
#else
#define DIRECTX9 IDirect3D9
#endif
#ifndef DIRECTINPUT_VERSION
#define DIRECTI8 unsigned int
#else
#define DIRECTI8 IDirectInput8
#endif
#ifndef DIRECTSOUND_VERSION
#define DIRECTS8 unsigned int
#else
#define DIRECTS8 IDirectSound
#endif

#ifdef __cplusplus
CNATIVE {
#endif
    typedef struct IHaloEngine { // For Add-on API interface support
        s_server_header* serverHeader;
        s_player_reserved_slot* playerReserved;
        s_machine_slot* machineHeader;
        unsigned char machineHeaderSize;
        HALO_VERSION haloGameVersion;
        bool isDedi;
        unsigned char reserved0;
        unsigned int* player_base;
        s_gametype* gameTypeLive;
        s_cheat_header* cheatHeader;
        s_map_header* mapCurrent;
        s_console_header* console;
        unsigned int* gameUpTimeLive; //1 sec = 60 ticks
        unsigned int* mapUpTimeLive; //1 sec = 30 ticks
        unsigned int* mapTimeLimitLive;
        unsigned int* mapTimeLimitPermament;
        s_console_color_list* consoleColor;
        DIRECTX9*  DirectX9;
        DIRECTI8*  DirectInput8;
        DIRECTS8*  DirectSound8;
        bool* cheatVEject;
        GameTypeGlobals* gameTypeGlobals;
        s_map_status** mapStatus;
        //Halo Simulate Functions Begin
        /// <summary>
        /// To prepare a packet to send to player(s).
        /// </summary>
        /// <param name="packet_data">To build a buffer to send. Does not accept null or unallocate memory.</param>
        /// <param name="arg1">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="packettype">Unknown, do not have a list for this. (Use at your risk!)</param>
        /// <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="data_pointer">Any data you want send to player/server.</param>
        /// <param name="arg4">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="arg5">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg6">Unknown, usually 0 (Use at your risk!)</param>
        /// <returns>Return unique ID to be used to add in a queue functions.</returns>
        unsigned int (*m_build_packet)(unsigned char* packet_data, unsigned int arg1, unsigned int packettype, unsigned int arg3, unsigned char* data_pointer, unsigned int arg4, unsigned int arg5, unsigned int arg6);
        /// <summary>
        /// To add a queue send to specific player.
        /// </summary>
        /// <param name="machine_index">Unique machine_index from s_machine structure.</param>
        /// <param name="packet">Only use packet_data buffer from m_build_packet.</param>
        /// <param name="packetCode">The return value from m_build_packet to be used.</param>
        /// <param name="arg1">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg2">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="arg4">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg5">Unknown, usually 3 (Use at your risk!)</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        void (*m_add_packet_to_player_queue)(unsigned int machine_index, unsigned char* packet, unsigned int packetCode, unsigned int arg1, unsigned int arg2, unsigned int arg3, unsigned int arg4, unsigned int arg5);
        /// <summary>
        /// To add a queue send to all players.
        /// </summary>
        /// <param name="packet_data">To build a buffer to send. Does not accept null or unallocate memory.</param>
        /// <param name="packettype">Unknown, do not have a list for this. (Use at your risk!)</param>
        /// <param name="arg1">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg2">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg3">Unknown, usually 0 (Use at your risk!)</param>
        /// <param name="arg4">Unknown, usually 1 (Use at your risk!)</param>
        /// <param name="arg5">Unknown, usually 3 (Use at your risk!)</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        void (*m_add_packet_to_global_queue)(unsigned char* packet_data, unsigned int packetCode, unsigned int arg1, unsigned int arg2, unsigned int arg3, unsigned int arg4, unsigned int arg5);
        /// <summary>
        /// Dispatch a rcon message to specific player.
        /// </summary>
        /// <param name="data">A message you would like to send.</param>
        /// <param name="plI">Specific player to receive this rcon message.</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        void (*m_dispatch_rcon)(rconData& data, PlayerInfo& plI);
        /// <summary>
        /// Dispatch a chat message to specific player.
        /// </summary>
        /// <param name="data">A message you would like to send.</param>
        /// <param name="len">Length of characters from data, maximum is 80 (0x50).</param>
        /// <param name="plI">Specific player to receive this chat message.</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        void (*m_dispatch_player)(chatData& data, int len, PlayerInfo& plI);
        /// <summary>
        /// Dispatch a chat message to all players.
        /// </summary>
        /// <param name="data">A message you would like to send.</param>
        /// <param name="len">Length of characters from data, maximum is 80 (0x50).</param>
        /// <returns>Does not return a value. (May will be changed later on.)</returns>
        void (*m_dispatch_global)(chatData& data, int len);
        /// <summary>
        /// To send a rejection code reason to player and disconnect player from host.
        /// </summary>
        /// <param name="player">Pass down an active s_machine_slot pointer.</param>
        /// <param name="code">See REJECT_CODE for codes available to use.</param>
        /// <returns>Return true if successfully sent packet.</returns>
        bool (*m_send_reject_code)(s_machine_slot* player, REJECT_CODE code);
        /// <summary>
        /// To set a server to idle state. (Only supportive for server.)
        /// </summary>
        /// <returns>Return true if will set host to idling.</returns>
        bool (*m_set_idling)();
        /// <summary>
        /// To end a current game and move on to the next map. (Only supportive for server.)
        /// </summary>
        /// <returns>Return true if host is changing to next map.</returns>
        bool (*m_map_next)();
        /// <summary>
        /// To execute native halo command. (May will be deprecate in future.)
        /// </summary>
        /// <param name="command">Input a command.</param>
        /// <returns>Return true if execution is a success.</returns>
        bool (*m_exec_command)(const char* command);
        /// <summary>
        /// Get the current password for hosting un/lock.
        /// </summary>
        /// <param name="pass">Return the current password. (Must be at least 8 characters long.)</param>
        /// <returns>Does not return a value.</returns>
        void (*m_get_server_password)(wchar_t* pass);
        /// <summary>
        /// Set the current password for hosting un/lock.
        /// </summary>
        /// <param name="pass">Set the current password. (Maximum permitted is 8 characters long.)</param>
        /// <returns>Does not return a value.</returns>
        void (*m_set_server_password)(wchar_t* pass);
        /// <summary>
        /// Get the current rcon password for authorized players to execute command.
        /// </summary>
        /// <param name="pass">Return the current rcon password. (Must be at least 8 characters long.)</param>
        /// <returns>Does not return a value.</returns>
        void (*m_get_rcon_password)(char* pass);
        /// <summary>
        /// Set the current rcon password for authorized players to execute command.
        /// </summary>
        /// <param name="pass">Set the current rcon password. (Maximum permitted is 8 characters long.)</param>
        /// <returns>Does not return a value.</returns>
        void (*m_set_rcon_password)(char* pass);
        //Halo Simulate Functions End
        /// <summary>
        /// Obtain an Add-on information if able to find a match.
        /// </summary>
        /// <param name="index">Input an Add-on index slot number.</param>
        /// <param name="getInfo">Output a matched Add-on or not.</param>
        /// <returns>Return true or false if unable find a match.</returns>
        bool(*m_ext_add_on_get_info_index)(unsigned int index, addon_info& getInfo);
        /// <summary>
        /// Obtain an Add-on information if able to find a match.
        /// </summary>
        /// <param name="name">Input name of an Add-on. (Maximum permitted is 128 characters long.)</param>
        /// <param name="getInfo">Output a matched Add-on or not.</param>
        /// <returns>Return true or false if unable find a match.</returns>
        bool(*m_ext_add_on_get_info_by_name)(wchar_t* name, addon_info& getInfo);
        /// <summary>
        /// Reload an Add-on while still running Halo.
        /// </summary>
        /// <param name="name">Input name of an Add-on. (Maximum permitted is 128 characters long.)</param>
        /// <returns>Return true or false if unable to reload Add-on.</returns>
        bool (*m_ext_add_on_reload)(wchar_t* name);
    } IHaloEngine;
    //static_assert_check(sizeof(IHaloEngine) == 0x20, sizeof(IHaloEngine));// "Incorrect size of IHaloEngine!");
    dllport IHaloEngine* WINAPIC getIHaloEngine(unsigned int hash);

#ifdef __cplusplus
}
#endif
#endif