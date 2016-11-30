module Add_on_API.D.admin;

import Add_on_API.Add_on_API;

static if (__traits(compiles, EXT_IADMIN)) {

    enum LOGIN_VALIDATION: int {
        INVALID = -1,
        FAIL = 0,
        OK = 1
    }
    enum CMD_AUTH: int {
        NOT_FOUND = -2,
        OUT_OF_RANGE = -1,
        DENIED = 0,
        AUTHORIZED = 1
    }

    extern(C) struct IAdmin {
        /**
         * To verify if player is authorized to use command.
         * Params:
         * player = Required to input player.
         * command = Required to input command.
         * arg = Output the argument from command.
         * func = Output a function link to the command.
         * Returns: Only return true, false, and -1 if input is invalid.
         */
        CMD_AUTH function(const PlayerInfo* player, const wchar* command, ArgContainer* arg, CmdFunc* func) m_is_player_authorized;
        /**
         * To verify if username exist in database and return true, false, or -1 for database is offline.
         * Params:
         * username= Take unicode username to verify.
         * Returns: Only return true, false, and -1 if database is offline.
         */
        e_boolean function(const wchar* username) m_is_username_exist;
        /**
         * To add an admin to the database and return true, false, or -1 for database is offline.
         * Params:
         * hashW = Maximum permitted is 32 characters.
         * IP_Addr = Maximum permitted is 15 characters.
         * IP_Port = Maximum permitted is 6 characters.
         * username = Maximum permitted is 24 characters.
         * password = No limitation on password for now.
         * level = Maximum permitted is 9999.
         * remote = To permit remote administrator access without need to use Halo game.
         * pass_force = Force administrator to change their password.
         * Returns: Only return true, false, and -1 if database is offline.
         */
        e_boolean function(const wchar* hashW, const wchar* IP_Addr, const wchar* IP_Port, const wchar* username, const wchar* password, short level, bool remote, bool pass_force) m_add;
        /**
         * To remove username from database and return true, false, or -1 for database is offline.
         * Params:
         * username = Maximum permitted is 24 characters.
         * Returns: Only return true, false, and -1 if database is offline.
         */
        e_boolean function(const wchar* username) m_delete;
        /**
         * To login a player as administrator from database verfication and return true, false, or -1 for database is offline.
         * Params:
         * player = Take ingame or remote admin to verify.
         * chatRconRemote = To return a message back to player.
         * username = Maximum permitted is 24 characters.
         * password = No limitation on password for now.
         * Returns: Only return LOGIN.INVALID, LOGIN.FAIL, and LOGIN.OK.
         */
        LOGIN_VALIDATION function(ref PlayerInfo  player, MSG_PROTOCOL protocolMsg, const wchar* username, const wchar* password) m_login;
    };
    export extern(C) IAdmin* getIAdmin(uint hash);
}