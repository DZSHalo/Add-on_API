#ifndef adminH
#define adminH

typedef enum LOGIN_VALIDATION {
    LOGIN_INVALID = -1,
    LOGIN_FAIL = 0,
    LOGIN_OK = 1
} LOGIN_VALIDATION;
typedef enum CMD_AUTH {
    AUTH_NOT_FOUND = -2,
    AUTH_OUT_OF_RANGE = -1,
    AUTH_DENIED = 0,
    AUTH_AUTHORIZED = 1
} CMD_AUTH;
#ifdef __cplusplus
CNATIVE {
#endif
    typedef struct IAdmin {
        /// <summary>
        /// To verify if <paramref name="player">player</paramref> is authorized to use <paramref name="command">command</paramref>.
        /// </summary>
        /// <param name="player">Required to input player.</param>
        /// <param name="command">Required to input command.</param>
        /// <param name="arg">Output the argument from command.</param>
        /// <param name="func">Output a function link to the command.</param>
        /// <returns>Only return true, false, and -1 if input is invalid.</returns>
        CMD_AUTH (*m_is_authorized)(const PlayerInfo* player, const wchar_t* command, ArgContainer* arg, CmdFunc* func);
        /// <summary>
        /// To verify if <paramref name="username"/> exist in database and return true, false, or -1 for database is offline.
        /// </summary>
        /// <param name="username">Take unicode username to verify.</param>
        /// <returns>Only return true, false, and -1 if database is offline.</returns>
        e_boolean (*m_is_username_exist)(const wchar_t* username);
        /// <summary>
        /// To add an admin to the database and return true, false, or -1 for database is offline.
        /// </summary>
        /// <param name="hashW">Maximum permitted is 32 characters.</param>
        /// <param name="IP_Addr">Maximum permitted is 15 characters.</param>
        /// <param name="IP_Port">Maximum permitted is 6 characters.</param>
        /// <param name="username">Maximum permitted is 24 characters.</param>
        /// <param name="password">No limitation on password for now.</param>
        /// <param name="level">Maximum permitted is 9999.</param>
        /// <param name="remote">To permit remote administrator access without need to use Halo game.</param>
        /// <param name="pass_force">Force administrator to change their password.</param>
        /// <returns>Only return true, false, and -1 if database is offline.</returns>
        e_boolean (*m_add)(const wchar_t* hashW, const wchar_t* IP_Addr, const wchar_t* IP_Port, const wchar_t* username, const wchar_t* password, short level, bool remote, bool pass_force);
        /// <summary>
        /// To remove <paramref name="username"/> from database and return true, false, or -1 for database is offline.
        /// </summary>
        /// <param name="username">Maximum permitted is 24 characters.</param>
        /// <returns>Only return true, false, and -1 if database is offline.</returns>
        e_boolean (*m_delete)(const wchar_t* username);
        /// <summary>
        /// To login a <paramref name="player"/> as administrator from database verfication and return LOGIN_INVALID, LOGIN_FAIL, and LOGIN_OK.
        /// </summary>
        /// <param name="player">Take ingame or remote admin to verify.</param>
        /// <param name="chatRconRemote">To return a message back to player.</param>
        /// <param name="username">Maximum permitted is 24 characters.</param>
        /// <param name="password">No limitation on password for now.</param>
        /// <returns>Only return LOGIN_INVALID, LOGIN_FAIL, and LOGIN_OK.</returns>
        LOGIN_VALIDATION (*m_login)(PlayerInfo* player, MSG_PROTOCOL protocolMsg, const wchar_t* username, const wchar_t* password);
    } IAdmin;
    dllport IAdmin* getIAdmin(unsigned int hash);
#ifdef __cplusplus
}
#endif
#endif