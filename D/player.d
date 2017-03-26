module D.player;

import Add_on_API;
import core.sys.windows.oaidl;

enum MSG_FORMAT:uint {
    MF_BLANK    = 0,
    MF_SERVER   = 1,
    MF_HEXT     = 2,
    MF_ADMINBOT = 3,
    MF_INFO     = 4,
    MF_WARNING  = 5,
    MF_ERROR    = 6,
    MF_ALERT    = 7
}
enum MSG_PROTOCOL : uint {
    MP_CHAT = 0,
    MP_RCON = 1,
    MP_REMOTE = 3
}

struct rconData {
    char* msg_ptr;
    uint unk; //always 0
    char[80] msg = 0;
    this(const char[] text) {
        if (text.length > 79) {
            msg[0..79] = text[0..79];
            msg[79] = 0; //This is a requirement.
        } else {
            msg[0..text.length] = text[0..text.length];
            msg[text.length] = 0; //This is a requirement.
        }
        msg_ptr = msg.ptr;
    }
}
static assert(rconData.sizeof == 0x58, "Incorrect size of rconData");

// #pragma pack(push,1)
align (1) struct PlayerExtended {
    align (1) :
    bool isInServer;
    bool isRemote;
    short adminLvl;
    wchar[25] user;
    wchar[33] pass;
    char[33] CDHashA;
    wchar[33] CDHashW;
    wchar[16] IP_Address;
    wchar[8] IP_Port;
    wchar[24] IP_Full;
    bool temp_pass;
    bool can_chat = 1;
    bool last_team;
    bool reserved;
    int last_check;
    float handicap_ammo;
    float handicap_clip;
    float handicap_shield = 1;
    float handicap_health = 1;
    float handicap_speed = 1;
}
static assert(PlayerExtended.sizeof == 343, "Incorrect size of PlayerExtended");
struct PlayerInfo {
    //By default D Lang set them to zeroes
    PlayerExtended* plEx;
    s_machine_slot* mS;
    s_player_reserved_slot* plR;
    s_player_slot* plS;
    this(ref PlayerInfo plI) {
        this.plEx = plI.plEx;
        this.mS = plI.mS;
        this.plR = plI.plR;
        this.plS = plI.plS;
    }
}
static assert(PlayerInfo.sizeof == 16, "Incorrect size of PlayerInfo");
struct PlayerInfoList {
    PlayerInfo[32] plList;
}
// #pragma pack(pop)

static if(__traits(compiles, EXT_IPLAYER)) {
    extern(C) struct IPlayer {
        /*
         * Get PlayerInfo from machine index if in used.
         * Params:
         * m_ind = Machine index
         * playerInfo = PlayerInfo
         * fullRequest = Get full request detail, if partial are found. Then it will reset to null and return false.
         * Returns: Return true or false if not found.
         */
        bool function(machineindex m_ind, PlayerInfo* playerInfo, bool fullRequest) m_get_m_index;
        /*
         * Get PlayerInfo from player index if in used.
         * Params:
         * playerId = Player index
         * playerInfo = PlayerInfo
         * Returns: Return true or false if not found.
         */
        bool function(uint playerId, PlayerInfo* playerInfo) m_get_id;
        /*
         * Get PlayerInfo from unique player s_ident.
         * Params:
         * pl_Tag = Unique player s_ident.
         * playerInfo = PlayerInfo
         * Returns: Return true or false if not found.
         */
        bool function(s_ident pl_Tag, PlayerInfo* playerInfo) m_get_ident;
        /*
         * Get PlayerInfo from existing nickname.
         * Params:
         * nickname = Nickname
         * playerInfo = PlayerInfo
         * Returns: Return true or false if not found.
         */
        bool function(const wchar* nickname, PlayerInfo* playerInfo) m_get_by_nickname;
        /*
         * Get PlayerInfo from existing username.
         * Params:
         * username = Username
         * playerInfo = PlayerInfo
         * Returns: Return true or false if not found.
         */
        bool function(const wchar* username, PlayerInfo* playerInfo) m_get_by_username;
        /*
         * Get PlayerInfo from uniqueID from s_machine_slot.
         * Params:
         * uniqueID = Can be obtain from existing s_machine_slot.
         * playerInfo = PlayerInfo
         * Returns: Return true or false if not found.
         */
        bool function(int uniqueID, PlayerInfo* playerInfo) m_get_by_unique_id;
        /*
         * Get ID from joined player's name.
         * Params:
         * fullName = Player's full name.
         * Returns: Return ID of full name from database.
         */
        uint function(wchar* fullName) m_get_id_full_name;
        /*
         * Get ID from IP Address, excluded port number.
         * Params:
         * ipAddress = Player's IP Address, excluded port number.
         * Returns: Return ID  from database.
         */
        uint function(wchar* ipAddress) m_get_id_ip_address;
        /*
         * Get ID from port, excluded IP Address.
         * Params:
         * port = Port number, excluded IP Address.
         * Returns: Return ID of port from database.
         */
        uint function(wchar* port) m_get_id_port;
        /*
         * Get full name from ID.
         * Params:
         * ID = ID
         * fullName = Full name
         * Returns: Return true or false if unable get full name from database.
         */
        bool function(uint ID, wchar* fullName) m_get_full_name_id;
        /*
         * Get IP Address, excluded port number, from ID.
         * Params:
         * ID = ID
         * ipAddress = IP Address, excluded port number
         * Returns: Return true or false if unable get IP Address from database.
         */
        bool function(uint ID, wchar* ipAddress) m_get_ip_address_id;
        /*
         * Get port number, excluded IP Address, from ID.
         * Params:
         * ID = ID
         * port = Port number, excluded IP Address
         * Returns: Return true or false if unable get port number from database.
         */
        bool function(uint ID, wchar* port) m_get_port_id;
        /*
         * Update PlayerInfo from database.
         * Params:
         * plI = PlayerInfo
         * Returns: Return true or false if unable to update.
         */
        bool function(PlayerInfo* plI) m_update;
        /*
         * Set Player's nickname.
         * Params:
         * playerInfo = PlayerInfo
         * nickname = Nickname
         * Returns: Return true or false if unable to set nickname.
         */
        bool function(PlayerInfo* plI, wchar* nickname) m_set_nickname;
        /*
         * To send a message through chat, rcon, or remote protocol to a specific player.
         * Params:
         * formatMsg = See MSG_FORMAT for detail.
         * protocolMsg = See MSG_FORMAT for detail.
         * playerInfo = PlayerInfo
         * Msg = A message or predefined message.
         * argTotal = Total arguments in argList.
         * argList = To fill in the blank in a pre-defined message.
         * Returns: Return true or false if unable to send a message.
         */
        bool function(MSG_FORMAT formatMsg, MSG_PROTOCOL protocolMsg, PlayerInfo* plI, const wchar* Msg, uint argTotal, VARIANT* argList) m_send_custom_message;
        /*
         * To verify if a player is an admin.
         * Params:
         * m_ind = Machine index
         * Returns: Return true or false if is not an admin.
         */
        bool function(machineindex m_ind) m_is_admin;
        /*
         * Find player from current unique biped s_ident.
         * Params:
         * bipedTag = Unique biped s_ident
         * playerInfo = PlayerInfo
         * Returns: Return true or false if unable to find player using given current biped.
         */
        bool function(s_ident bipedTag, PlayerInfo* playerInfo) m_get_by_biped_tag_current;
        /*
         * Find player from previous unique biped s_ident.
         * Params:
         * bipedTag = Unique biped s_ident
         * playerInfo = PlayerInfo
         * Returns: Return true or false if unable to find player using given previous biped.
         */
        bool function(s_ident bipedTag, PlayerInfo* playerInfo) m_get_by_biped_tag_previous;
        /*
         * To send a message through chat procotol to all players.
         * Params:
         * formatMsg = See MSG_FORMAT for detail.
         * Msg = A message or predefined message.
         * argTotal = Total arguments in argList.
         * argList = To fill in the blank in a pre-defined message.
         * <param name="...">To fill in the blank in a pre-defined message.
         * Returns: Return true or false if unable to send a message.
         */
        bool function(MSG_FORMAT formatMsg, const wchar* Msg, uint argTotal, VARIANT* argList) m_send_custom_message_broadcast;
        /*
         * Force player to change team with optional to kill player if needed.
         * Params:
         * playerInfo = PlayerInfo
         * new_team = New team to assign.
         * forcekill = Force kill player if needed.
         * Returns: Does not return any value.
         */
        void function(PlayerInfo* plI, e_color_team_index new_team, bool forcekill) m_change_team;
        /*
         * To apply camouflage duration on specific player.
         * Params:
         * playerInfo = PlayerInfo
         * duration = In seconds format.
         * Returns: Does not return any value. (This may will be change in future.)
         */
        void function(PlayerInfo* plI, uint duration) m_apply_camo;

        /*
         * Ban player from host server.
         * Params:
         * plEx = Player to ban.
         * gmtm = Time/date to expire ban.
         * Returns: Return true or false unable to ban player.
         */
        e_boolean function(PlayerExtended* plEx, tm* gmtm) m_ban_player;
        /*
         * Ban CD hash key from host server.
         * Params:
         * CDHash = CD hash key to ban. (Must have 33 characters allocate to copy, 33th is to null termate.)
         * gmtm = Time/date to expire ban.
         * Returns: Return true or false unable to ban CD hash key.
         */
        e_boolean function(const wchar* CDHash, tm* gmtm) m_ban_CD_key;
        /*
         * Ban IP Address from host server.
         * Params:
         * IP_Address = IP Address to ban.. (Must have 16 characters allocate to copy.)
         * gmtm = Time/date to expire ban.
         * Returns: Return true or false unable to ban IP Address.
         */
        e_boolean function(const wchar* IP_Addr, tm* gmtm) m_ban_ip;
        /*
         * Get ID from banned IP Address.
         * Params:
         * IP_Address = Banned IP Address. (Maximum is 16 characters long.)
         * Returns: Return ID of banned IP Address.
         */
        uint function(const wchar* IP_Addr) m_ban_ip_get_id;
        /*
         * Get ID from banned CD hash key.
         * Params:
         * CDHash = Banned CD hash key. (Must have 33 characters, 33th is to null termate.)
         * Returns: Return ID of banned IP Address.
         */
        uint function(const wchar* CDHash) m_ban_CD_key_get_id;
        /*
         * To expire a ban from banned list.
         * Params:
         * ID = Obtained ID from either banned IP Address or CD hash key.
         * Returns: Return true or false if unable to unban ID.
         */
        bool function(uint ID) m_unban_id;
        /*
         * Get IP address, excluded port number, from machine slot.
         * Params:
         * mS = machine slot
         * m_ip = IP address, excluded port number
         * Returns: Return true or false if unable get IP address.
         */
        bool function(const s_machine_slot* mH, IN_ADDR* m_ip) m_get_ip;
        /*
         * Get port number, excluded IP address, from machine slot.
         * Params:
         * mS = machine slot
         * m_port = Port number, excluded IP address
         * Returns: Return true or false if unable get port.
         */
        bool function(const s_machine_slot* mH, ushort* m_port) m_get_port;
        /*
         * Get CD hash from machine slot.
         * Params:
         * mS = machine slot
         * CDHash = CD hash key. (Must have 33 characters allocated to copy, 33th is a null termated.)
         * Returns: Return true or false if unable get CD hash.
         */
        bool function(const s_machine_slot* mH, char* hashKey) m_get_CD_hash;

        /*
         * Find a match of player(s) from regex expression search.
         * Params:
         * regexSearch = To find a matching player(s).
         * plMatch = List of matched players from search.
         * plOwner = Optional, owner of player execution usually.
         * Returns: Return total count of matched player(s).
         */
        ushort function(const wchar* src, PlayerInfoList* plMatch, PlayerInfo* plOwner) m_get_str_to_player_list;
    }
    export extern(C) IPlayer* getIPlayer(uint hash);
}
