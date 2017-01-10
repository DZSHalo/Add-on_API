#ifndef commandH
#define commandH

#ifdef __cplusplus
CNATIVE {
#endif

#pragma pack(push,1)
    typedef struct helpInfo {
        wchar_t info[4][255];
    } helpInfo;
#pragma pack(pop)
    typedef struct ICommand {
        /// <summary>
        /// To add nonexisting <paramref name="command"/> bind to <paramref name="func"/> into command system and return true or false.
        /// </summary>
        /// <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        /// <param name="command">A new command name, or override a command once if permitted, into command system.</param>
        /// <param name="func">A new function or existing function within same add-on only.</param>
        /// <param name="section">Section where command belongs to.</param>
        /// <param name="min">Minimum requirement to able allow command execute.</param>
        /// <param name="max">Maximum requirement to able allow command execute.</param>
        /// <param name="allowOverride">An option to either allow or forbidden another add-on or current add-on override a command.</param>
        /// <param name="mode">To permit a command executed in either single player, multiplayer, hosting a game, or all. See below GAME_MODE_S struct for pre-defined availability.</param>
        /// <returns>Only return true or false.</returns>
        bool (*m_add)(unsigned int hash, const wchar_t* command, CmdFunc func, const wchar_t* section, unsigned short min, unsigned short max, bool allowOverride, GAME_MODE_S mode);
        /// <summary>
        /// To delete a <paramref name="command"/> which is binded to <paramref name="func"/> and return true or false.
        /// </summary>
        /// <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        /// <param name="func">A function currently binded to a command.</param>
        /// <param name="command">A command currently binded to a function.</param>
        /// <returns>Only return true or false.</returns>
        bool (*m_delete)(unsigned int hash, CmdFunc func, const wchar_t* command);
        /// <summary>
        /// To load or reload authorized add-on's commands level from commands.ini file.
        /// </summary>
        /// <param name="hash">Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.</param>
        /// <returns>Only return true or false.</returns>
        bool (*m_reload_level)(unsigned int hash);
        /// <summary>
        /// To add an <paramref name="alias"/> from <paramref name="command"/> and return true or false.
        /// </summary>
        /// <param name="command">Command name currently exist in command system.</param>
        /// <param name="alias">An alias command name which is not binded to a command.</param>
        /// <returns>Only return true or false.</returns>
        bool (*m_alias_add)(const wchar_t* command, const wchar_t* alias);
        /// <summary>
        /// To delete an <paramref name="alias"/> from <paramref name="command"/> and return true or false.
        /// </summary>
        /// <param name="command">Command name currently exist in command system.</param>
        /// <param name="alias">An alias command name currently bind to a command.</param>
        /// <returns>Only return true or false.</returns>
        bool (*m_alias_delete)(const wchar_t* command, const wchar_t* alias);
        /// <summary>
        /// To load a custom command(s) from <paramref name="fileName"/> and return true or false.
        /// </summary>
        /// <param name="hash">Valid owner Add-on hash and existing config_folder defined.</param>
        /// <param name="fileName">Custom file name to load and execute from.</param>
        /// <param name="plI">Bind user of this execution process.</param>
        /// <param name="protocolMsg">For output the message to binded user.</param>
        /// <returns>Only return true or false.</returns>
        bool (*m_load_from_file)(unsigned int hash, const wchar_t* fileName, PlayerInfo plI, MSG_PROTOCOL protocolMsg);
    } ICommand;
    CNATIVE dllport ICommand* getICommand(const unsigned int hash);
#ifdef __cplusplus
}
#endif
#endif