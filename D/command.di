module Add_on_API.D.command;

import Add_on_API.Add_on_API;

static if(__traits(compiles, EXT_ICOMMAND)) {
// #define commandH

// #pragma pack(push,1)
    struct helpInfo {
        wchar info[4][255];
    };
// #pragma pack(pop)
    extern(C) struct ICommand {
        /*
         * To add nonexisting command bind to func into command system and return true or false.
         * Params:
         * hash = Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.
         * command = A new command name, or override a command once if permitted, into command system.
         * func = A new function or existing function within same add-on only.
         * section = Section where command belongs to.
         * min = Minimum requirement to able allow command execute.
         * max = Maximum requirement to able allow command execute.
         * allowOverride = An option to either allow or forbidden another add-on or current add-on override a command.
         * mode = To permit a command executed in either single player, multiplayer, hosting a game, or all. See below GAME_MODE_S struct for pre-defined availability.
         * Returns: Only return true or false.
         */
        bool function(uint hash, const wchar* command, CmdFunc func, const wchar* section, ushort min, ushort max, bool allowOverride, GAME_MODE_S mode) m_add;
        /*
         * To delete a command which is binded to func and return true or false.
         * Params:
         * hash = Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.
         * func = A function currently binded to a command.
         * command = A command currently binded to a function.
         * Returns: Only return true or false.
         */
        bool function(uint hash, CmdFunc func, const wchar* command) m_delete;
        /*
         * To load or reload authorized add-on's commands level from commands.ini file.
         * Params:
         * hash = Authorized add-on usage only. Can be obtained from EXTOnEAOLoad's parameter.
         * Returns: Only return true or false.
         */
        bool function(uint hash) m_reload_level;
        /*
         * To add an alias from command and return true or false.
         * Params:
         * command = Command name currently exist in command system.
         * alias = An alias command name which is not binded to a command.
         * Returns: Only return true or false.
         */
        bool function(const wchar* command, const wchar* _alias) m_alias_add;
        /*
         * To delete an alias from command and return true or false.
         * Params:
         * command = Command name currently exist in command system.
         * alias = An alias command name currently bind to a command.
         * Returns: Only return true or false.
         */
        bool function(const wchar* command, const wchar* _alias) m_alias_delete;
        /*
         * To load a custom command(s) from <paramref name="fileName"/> and return true or false.
         * Params:
         * hash = Valid owner Add-on hash and existing config_folder defined.
         * fileName = Custom file name to load and execute from.
         * plI = Bind user of this execution process.
         * protocolMsg = For output the message to binded user.
         * Returns: Only return true or false.
         */
        bool function(uint hash, const wchar* fileName, PlayerInfo plI, MSG_PROTOCOL protocolMsg) m_load_from_file;
    };
    export extern(C) ICommand* getICommand(uint hash);

}
