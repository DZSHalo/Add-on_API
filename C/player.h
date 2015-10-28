#ifndef playerH
#define playerH

#ifdef __cplusplus
CNATIVE {
#endif

    enum MSG_FORMAT : unsigned int {
        MF_BLANK = 0,
        MF_SERVER = 1,
        MF_HEXT = 2,
        MF_ADMINBOT = 3,
        MF_INFO = 4,
        MF_WARNING = 5,
        MF_ERROR = 6,
        MF_ALERT = 7
    };
    enum MSG_PROTOCOL : unsigned int {
        MP_CHAT = 0,
        MP_RCON = 1,
        MP_REMOTE = 2
    };

    #pragma pack(push,1)
    struct PlayerExtended {
        bool isInServer;
        bool isRemote;
        short adminLvl;
        wchar_t user[25];
        wchar_t pass[33];
        char CDHashA[33];
        wchar_t CDHashW[33];
        wchar_t IP_Address[16];
        wchar_t IP_Port[8];
        wchar_t IP_Full[24];
        bool temp_pass;
        bool can_chat;
        bool last_team;
        bool reserved;
        int last_check;
        float handicap_ammo;
        float handicap_clip;
        float handicap_shield;
        float handicap_health;
        float handicap_speed;
        PlayerExtended() {
            isInServer=0;
            adminLvl=0;
            user[0] = 0;
            pass[0] = 0;
            CDHashW[0] = 0;
            IP_Address[0] = 0;
            IP_Port[0] = 0;
            temp_pass = 0;
            can_chat = 1;
            last_team = 0;
            isRemote = 0;
            handicap_ammo = 0;
            handicap_clip = 0;
            handicap_shield = 1;
            handicap_health = 1;
            handicap_speed = 1;
        }
    };
    static_assert_check(sizeof(PlayerExtended) == 343, "Incorrect size of PlayerExtended");
    struct PlayerInfo {
        PlayerExtended* plEx;
        s_machine_slot* mS;
        s_player_reserved_slot* plR;
        s_player_slot* plS;
        PlayerInfo() {
            plEx = 0;
            mS = 0;
            plR = 0;
            plS = 0;
        }
        PlayerInfo(PlayerInfo& plI) {
            this->plEx = plI.plEx;
            this->mS = plI.mS;
            this->plR = plI.plR;
            this->plS = plI.plS;
        }
    };
    static_assert_check(sizeof(PlayerInfo) == 16, "Incorrect size of PlayerInfo");
    struct PlayerInfoList {
        PlayerInfo* plList[32];
    };
    #pragma pack(pop)
typedef struct {
    /// <summary>
    /// Get PlayerInfo from machine index if in used.
    /// </summary>
    /// <param name="m_ind">Machine index</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <returns>Return true or false if not found.</returns>
    bool (*m_get_m_index)(machineindex m_ind, PlayerInfo& playerInfo);
    /// <summary>
    /// Get PlayerInfo from player index if in used.
    /// </summary>
    /// <param name="playerId">Player index</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <returns>Return true or false if not found.</returns>
    bool (*m_get_id)(unsigned int playerId, PlayerInfo& playerInfo);
    /// <summary>
    /// Get PlayerInfo from unique player s_ident.
    /// </summary>
    /// <param name="pl_Tag">Unique player s_ident.</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <returns>Return true or false if not found.</returns>
    bool (*m_get_ident)(s_ident pl_Tag, PlayerInfo& playerInfo);
    /// <summary>
    /// Get PlayerInfo from existing nickname.
    /// </summary>
    /// <param name="nickname">Nickname</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <returns>Return true or false if not found.</returns>
    bool (*m_get_by_nickname)(const wchar_t* nickname, PlayerInfo& playerInfo);
    /// <summary>
    /// Get PlayerInfo from existing username.
    /// </summary>
    /// <param name="username">Username</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <returns>Return true or false if not found.</returns>
    bool (*m_get_by_username)(const wchar_t* username, PlayerInfo& playerInfo);
    /// <summary>
    /// Get PlayerInfo from uniqueID from s_machine_slot.
    /// </summary>
    /// <param name="uniqueID">Can be obtain from existing s_machine_slot.</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <returns>Return true or false if not found.</returns>
    bool (*m_get_by_unique_id)(long uniqueID, PlayerInfo& playerInfo);
    /// <summary>
    /// Get ID from joined player's name.
    /// </summary>
    /// <param name="fullName">Player's full name.</param>
    /// <returns>Return ID of full name from database.</returns>
    int (*m_get_id_full_name)(wchar_t* fullName);
    /// <summary>
    /// Get ID from IP Address, excluded port number.
    /// </summary>
    /// <param name="ipAddress">Player's IP Address, excluded port number.</param>
    /// <returns>Return ID  from database.</returns>
    int (*m_get_id_ip_address)(wchar_t* ipAddress);
    /// <summary>
    /// Get ID from port, excluded IP Address.
    /// </summary>
    /// <param name="port">Port number, excluded IP Address.</param>
    /// <returns>Return ID of port from database.</returns>
    int (*m_get_id_port)(wchar_t* port);
    /// <summary>
    /// Get full name from ID.
    /// </summary>
    /// <param name="ID">ID</param>
    /// <param name="fullName">Full name</param>
    /// <returns>Does not return any value.</returns>
    void (*m_get_full_name_id)(int ID, wchar_t* fullName);
    /// <summary>
    /// Get IP Address, excluded port number, from ID.
    /// </summary>
    /// <param name="ID">ID</param>
    /// <param name="ipAddress">IP Address, excluded port number</param>
    /// <returns>Does not return any value.</returns>
    void (*m_get_ip_address_id)(int ID, wchar_t* ipAddress);
    /// <summary>
    /// Get port number, excluded IP Address, from ID.
    /// </summary>
    /// <param name="ID">ID</param>
    /// <param name="port">Port number, excluded IP Address</param>
    /// <returns>Does not return any value.</returns>
    void (*m_get_port_id)(int ID, wchar_t* port);
    /// <summary>
    /// Update PlayerInfo from database.
    /// </summary>
    /// <param name="plI">PlayerInfo</param>
    /// <returns>Return true or false if unable to update.</returns>
    bool (*m_update)(PlayerInfo& plI);
    /// <summary>
    /// Set Player's nickname.
    /// </summary>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <param name="nickname">Nickname</param>
    /// <returns>Return true or false if unable to set nickname.</returns>
    bool (*m_set_nickname)(PlayerInfo& plI, wchar_t* nickname);
    /// <summary>
    /// To send a message through chat, rcon, or remote protocol to a specific player.
    /// </summary>
    /// <param name="formatMsg">See MSG_FORMAT for detail.</param>
    /// <param name="protocolMsg">See MSG_FORMAT for detail.</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <param name="Msg">A message or predefined message.</param>
    /// <param name="...">To fill in the blank in a pre-defined message.</param>
    /// <returns>Return true or false if unable to send a message.</returns>
    bool (*m_send_custom_message)(MSG_FORMAT formatMsg, MSG_PROTOCOL protocolMsg, PlayerInfo& plI, const wchar_t *Msg, ...);
    /// <summary>
    /// To verify if a player is an admin.
    /// </summary>
    /// <param name="m_ind">Machine index</param>
    /// <returns>Return true or false if is not an admin.</returns>
    bool (*m_is_admin)(machineindex m_ind);
    /// <summary>
    /// Find player from current unique biped s_ident.
    /// </summary>
    /// <param name="bipedTag">Unique biped s_ident</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <returns>Return true or false if unable to find player using given current biped.</returns>
    bool (*m_get_by_biped_tag_current)(s_ident bipedTag, PlayerInfo& playerInfo);
    /// <summary>
    /// Find player from previous unique biped s_ident.
    /// </summary>
    /// <param name="bipedTag">Unique biped s_ident</param>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <returns>Return true or false if unable to find player using given previous biped.</returns>
    bool (*m_get_by_biped_tag_previous)(s_ident bipedTag, PlayerInfo& playerInfo);
    /// <summary>
    /// To send a message through chat procotol to all players.
    /// </summary>
    /// <param name="formatMsg">See MSG_FORMAT for detail.</param>
    /// <param name="Msg">A message or predefined message.</param>
    /// <param name="...">To fill in the blank in a pre-defined message.</param>
    /// <returns>Return true or false if unable to send a message.</returns>
    bool (*m_send_custom_message_broadcast)(MSG_FORMAT formatMsg, const wchar_t* Msg, ...);
    /// <summary>
    /// Force player to change team with optional to kill player if needed.
    /// </summary>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <param name="new_team">New team to assign.</param>
    /// <param name="forcekill">Force kill player if needed.</param>
    /// <returns>Does not return any value.</returns>
    void (*m_change_team)(PlayerInfo& playerInfo, const unsigned char new_team, bool forcekill);
    /// <summary>
    /// To apply camouflage duration on specific player.
    /// </summary>
    /// <param name="playerInfo">PlayerInfo</param>
    /// <param name="duration">In seconds format.</param>
    /// <returns>Does not return any value. (This may will be change in future.)</returns>
    void (*m_apply_camo)(PlayerInfo& playerInfo, unsigned int duration);

    /// <summary>
    /// Ban player from host server.
    /// </summary>
    /// <param name="plEx">Player to ban.</param>
    /// <param name="gmtm">Time/date to expire ban.</param>
    /// <returns>Return true or false unable to ban player.</returns>
    bool (*m_ban_player)(PlayerExtended &plEx, tm &gmtm);
    /// <summary>
    /// Ban CD hash key from host server.
    /// </summary>
    /// <param name="CDHash">CD hash key to ban. (Must have 33 characters allocate to copy, 33th is to null termate.)</param>
    /// <param name="gmtm">Time/date to expire ban.</param>
    /// <returns>Return true or false unable to ban CD hash key.</returns>
    ext_boolean (*m_ban_CD_key)(wchar_t* CDHash, tm &gmtm);
    /// <summary>
    /// Ban IP Address from host server.
    /// </summary>
    /// <param name="IP_Address">IP Address to ban.. (Must have 16 characters allocate to copy.)</param>
    /// <param name="gmtm">Time/date to expire ban.</param>
    /// <returns>Return true or false unable to ban IP Address.</returns>
    ext_boolean (*m_ban_ip)(wchar_t* IP_Address, tm &gmtm);
    /// <summary>
    /// Get ID from banned IP Address.
    /// </summary>
    /// <param name="IP_Address">Banned IP Address. (Maximum is 16 characters long.)</param>
    /// <returns>Return ID of banned IP Address.</returns>
    int (*m_ban_ip_get_id)(wchar_t* IP_Address);
    /// <summary>
    /// Get ID from banned CD hash key.
    /// </summary>
    /// <param name="CDHash">Banned CD hash key. (Must have 33 characters, 33th is to null termate.)</param>
    /// <returns>Return ID of banned IP Address.</returns>
    int (*m_ban_CD_key_get_id)(wchar_t* CDHash);
    /// <summary>
    /// To expire a ban from banned list.
    /// </summary>
    /// <param name="ID">Obtained ID from either banned IP Address or CD hash key.</param>
    /// <returns>Return true or false if unable to unban ID.</returns>
    bool (*m_unban_id)(int ID);
    /// <summary>
    /// Get IP address, excluded port number, from machine slot.
    /// </summary>
    /// <param name="mS">machine slot</param>
    /// <param name="m_ip">IP address, excluded port number</param>
    /// <returns>Return true or false if unable get IP address.</returns>
    bool (*m_get_ip)(s_machine_slot& mS, in_addr& m_ip);
    /// <summary>
    /// Get port number, excluded IP address, from machine slot.
    /// </summary>
    /// <param name="mS">machine slot</param>
    /// <param name=m_port"">Port number, excluded IP address</param>
    /// <returns>Return true or false if unable get port.</returns>
    bool (*m_get_port)(s_machine_slot& mS, unsigned short& m_port);
    /// <summary>
    /// Get CD hash from machine slot.
    /// </summary>
    /// <param name="mS">machine slot</param>
    /// <param name="CDHash">CD hash key. (Must have 33 characters allocate to copy, 33th is to null termate.)</param>
    /// <returns>Does not return any value.</returns>
    void (*m_get_CD_hash)(s_machine_slot &mS, char* CDHash);
    /// <summary>
    /// Find a match of player(s) from regex expression search.
    /// </summary>
    /// <param name="regexSearch">To find a matching player(s).</param>
    /// <param name="plMatch">List of matched players from search.</param>
    /// <param name="plOwner">Optional, owner of player execution usually.</param>
    /// <returns>Return total count of matched player(s).</returns>
    short (*StrToPlayerList)(const wchar_t* regexSearch, PlayerInfoList &plMatch, PlayerInfo* plOwner);
} IPlayer;

#ifdef __cplusplus
}
#endif

#ifdef EXT_IPLAYER
CNATIVE dllport IPlayer* getIPlayer(unsigned int hash);
#endif

#endif