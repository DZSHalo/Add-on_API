#ifndef expCheckerH
#define expCheckerH

#if _MSC_VER <=1500
#pragma WARNING("Unable to perform exporting checker for Microsoft Visual Studio 2008 and below...")
#elif __cplusplus
namespace addon {
#pragma region Interface functions
    typedef bool (WINAPIC* LPReturnBOOL)();
    typedef void (WINAPIC* LPVoidFunction)();
    typedef void (WINAPIC* LPOnEndGame)(int);
    typedef toggle (WINAPIC* LPONoParamsReturn)();
    typedef EAO_RETURN (WINAPIC* LPOnEAOLoad)(unsigned int);
    typedef void (WINAPIC* LPOnNewGame)(wchar_t*);
    typedef void (WINAPIC* LPOnPlayerJoinQuitEvent)(PlayerInfo);
    typedef void (WINAPIC* LPOnPlayerUpdate)(PlayerInfo);
    typedef int (WINAPIC* LPOnPlayerJoinDefault)(s_machine_slot* mS, int);
    typedef bool (WINAPIC* LPOnVehicleUserEntryEject)(PlayerInfo, bool);
    typedef bool (WINAPIC* LPOnPlayerChangeTeamAttempt)(PlayerInfo, int, bool);
    typedef bool (WINAPIC* LPOnPlayerSpawnColor)(PlayerInfo, bool);
    typedef void (WINAPIC* LPOnPlayerDeath)(PlayerInfo, PlayerInfo, int, bool&);
    typedef void (WINAPIC* LPWeaponAssignmentC)(PlayerInfo, s_ident, s_ident, unsigned int, s_ident&);
    typedef bool (WINAPIC* LPObjectInteraction)(PlayerInfo, s_ident, s_object*, hTagHeader*);
    typedef void (WINAPIC* LPWeaponAssignmentD)(PlayerInfo, s_ident, s_tag_reference*, unsigned int, s_ident&);
    typedef bool (WINAPIC* LPOnPlayerScoreCTF)(PlayerInfo, s_ident, unsigned int, bool);
    typedef bool (WINAPIC* LPOnPlayerAttemptDropObject)(PlayerInfo, s_ident, s_biped*);
    typedef void (WINAPIC* LPOnPlayerSpawn)(PlayerInfo, s_ident, s_biped*);
    typedef toggle (WINAPIC* LPOnPlayerValidateConnect)(PlayerExtended, s_machine_slot, s_ban_check, bool, toggle, toggle);
    typedef bool (WINAPIC* LPOnWeaponReload)(s_object*, bool);
    typedef bool (WINAPIC* LPOnObjectCreation)(s_object*, hTagHeader*);
    typedef bool (WINAPIC* LPOnKillMultiplier)(PlayerInfo killer, unsigned int multiplier);
    typedef bool (WINAPIC* LPOnVehicleRespawnProcess)(s_object* cur_object, objManaged& managedObj, bool isManaged);
    typedef bool (WINAPIC* LPOnObjectDeleteManagement)(s_object* cur_object, int curTicks, bool isManaged);
    typedef bool (WINAPIC* LPOnObjectDamageLookupProcess)(objDamageInfo& damageInfo, s_ident& obj_recv, bool& allowDamage, bool isManaged);
    typedef bool (WINAPIC* LPOnObjectDamageApplyProcess)(const objDamageInfo& damageInfo, s_ident& obj_recv, objHitInfo& hitInfo, bool isBacktap, bool& allowDamage, bool isManaged);
    typedef void (WINAPIC* LPOnMapLoad)(s_ident, const wchar_t[32]);
    typedef toggle (WINAPIC* LPOnVehicleAIEntry)(s_ident, s_ident, unsigned short, toggle);
    typedef toggle (WINAPIC* LPOnEquipmentDropCurrent)(PlayerInfo, s_ident, s_biped*, s_ident, s_weapon*, toggle);
    typedef toggle (WINAPIC* LPOnServerStatus)(int, toggle);
    typedef bool (WINAPIC* LPOnObjectCreationAttempt)(PlayerInfo plOwner, objCreationInfo object_creation, objCreationInfo* change_object, bool isOverride);
    
#pragma endregion
    template<typename T, typename U>
    struct is_same 
    {
        static const bool value = false; 
    };

    template<typename T>
    struct is_same<T,T>  //specialization
    { 
       static const bool value = true; 
    };

        //Standard verification
    static_assert_check(is_same<decltype(EAOversion), addon_version>::value, "EXTversion is incorrect, please fix this.");
    #undef EXTversion
    static_assert_check(is_same<decltype(&EXTOnEAOLoad), LPOnEAOLoad>::value, "EXTOnEAOLoad is incorrect, please fix this.");
    static_assert_check(is_same<decltype(&EXTOnEAOUnload), LPVoidFunction>::value, "EXTOnEAOUnload is incorrect, please fix this.");
    static_assert_check(is_same<decltype(EXTPluginInfo), addon_info>::value, "EXTPluginInfo is incorrect, please fix this.");


        //APIs verification
    __if_exists(EXTOnLoop) {
        static_assert_check(is_same<decltype(&EXTOnLoop), LPVoidFunction>::value, "EXTOnLoop is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerJoin) {
        static_assert_check(is_same<decltype(&EXTOnPlayerJoin), LPOnPlayerJoinQuitEvent>::value, "EXTOnPlayerJoin is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerQuit) {
        static_assert_check(is_same<decltype(&EXTOnPlayerQuit), LPOnPlayerJoinQuitEvent>::value, "EXTOnPlayerQuit is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerDeath) {
        static_assert_check(is_same<decltype(&EXTOnPlayerDeath), LPOnPlayerDeath>::value, "EXTOnPlayerDeath is incorrect, please fix this.");
    }

    __if_exists (EXTOnPlayerChangeTeamAttempt) {
        static_assert_check(is_same<decltype(&EXTOnPlayerChangeTeamAttempt), LPOnPlayerChangeTeamAttempt>::value, "EXTOnPlayerChangeTeamAttempt is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerJoinDefault) {
        static_assert_check(is_same<decltype(&EXTOnPlayerJoinDefault), LPOnPlayerJoinDefault>::value, "EXTOnPlayerJoinDefault is incorrect, please fix this.");
    }

    __if_exists(EXTOnNewGame) {
        static_assert_check(is_same<decltype(&EXTOnNewGame), LPOnNewGame>::value, "EXTOnNewGame is incorrect, please fix this.");
    }

    __if_exists(EXTOnEndGame) {
        static_assert_check(is_same<decltype(&EXTOnEndGame), LPOnEndGame>::value, "EXTOnEndGame is incorrect, please fix this.");
    }

    __if_exists (EXTOnServerIdle) {
        static_assert_check(is_same<decltype(&EXTOnServerIdle), LPVoidFunction>::value, "EXTOnServerIdle is incorrect, please fix this.");
    }

    __if_exists(EXTOnObjectInteraction) {
        static_assert_check(is_same<decltype(&EXTOnObjectInteraction), LPObjectInteraction>::value, "EXTOnObjectInteraction is incorrect, please fix this.");
    }

    __if_exists(EXTOnWeaponAssignmentDefault) {
        static_assert_check(is_same<decltype(&EXTOnWeaponAssignmentDefault), LPWeaponAssignmentD>::value, "EXTOnWeaponAssignmentDefault is incorrect, please fix this.");
    }

    __if_exists(EXTOnWeaponAssignmentCustom) {
        static_assert_check(is_same<decltype(&EXTOnWeaponAssignmentCustom), LPWeaponAssignmentC>::value, "EXTOnWeaponAssignmentCustom is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerScoreCTF) {
        static_assert_check(is_same<decltype(&EXTOnPlayerScoreCTF), LPOnPlayerScoreCTF>::value, "EXTOnPlayerScoreCTF is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerDropObject) {
        static_assert_check(0, "EXTOnPlayerDropObject need to be rename to EXTOnPlayerAttemptDropObject.");
    }
    __if_exists(EXTOnPlayerAttemptDropObject) {
        static_assert_check(is_same<decltype(&EXTOnPlayerAttemptDropObject), LPOnPlayerAttemptDropObject>::value, "EXTOnPlayerAttemptDropObject is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerSpawn) {
        static_assert_check(is_same<decltype(&EXTOnPlayerSpawn), LPOnPlayerSpawn>::value, "EXTOnPlayerSpawn is incorrect, please fix this.");
    }

    __if_exists(EXTOnVehicleUserEntry) {
        static_assert_check(is_same<decltype(&EXTOnVehicleUserEntry), LPOnVehicleUserEntryEject>::value, "EXTOnVehicleUserEntry is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerVehicleEject) {
        static_assert_check(is_same<decltype(&EXTOnPlayerVehicleEject), LPOnVehicleUserEntryEject>::value, "EXTOnPlayerVehicleEject is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerSpawnColor) {
        static_assert_check(is_same<decltype(&EXTOnPlayerSpawnColor), LPOnPlayerSpawnColor>::value, "EXTOnPlayerSpawnColor is incorrect, please fix this.");
    }

    __if_exists(EXTOnPlayerValidateConnect) {
        static_assert_check(is_same<decltype(&EXTOnPlayerValidateConnect), LPOnPlayerValidateConnect>::value, "EXTOnPlayerValidateConnect is incorrect, please fix this.");
    }

    __if_exists(EXTOnWeaponReload) {
        static_assert_check(is_same<decltype(&EXTOnWeaponReload), LPOnWeaponReload>::value, "EXTOnWeaponReload is incorrect, please fix this.");
    }

        //TODO: enable the ObjectCreation for addon support when ready.
    __if_exists(EXTOnObjectCreation) {
        static_assert_check(is_same<decltype(&EXTOnObjectCreation), LPOnObjectCreation>::value, "EXTOnObjectCreation is incorrect, please fix this.");
    }

    __if_exists(EXTOnKillMultiplier) {
        static_assert_check(is_same<decltype(&EXTOnKillMultiplier), LPOnKillMultiplier>::value, "EXTOnKillMultiplier is incorrect, please fix this.");
    }

        //TODO: enable the VehicleRespawnProcess for addon support when ready.
    __if_exists(ExtOnVehicleRespawnProcess) {
        static_assert_check(is_same<decltype(&ExtOnVehicleRespawnProcess), LPOnVehicleRespawnProcess>::value, "EXTOnVehicleRespawnProcess is incorrect, please fix this.");
    }

        //TODO: enable the ObjectDeleteManagement for addon support when ready.
    __if_exists(ExtOnObjectDeleteManagement) {
        static_assert_check(is_same<decltype(&ExtOnObjectDeleteManagement), LPOnObjectDeleteManagement>::value, "EXTOnObjectDeleteManagement is incorrect, please fix this.");
    }

    __if_exists(EXTOnObjectDamageLookupProcess) {
        static_assert_check(is_same<decltype(&EXTOnObjectDamageLookupProcess), LPOnObjectDamageLookupProcess>::value, "EXTOnObjectDamageLookupProcess is incorrect, please fix this.");
    }

    __if_exists(EXTOnObjectDamageApplyProcess) {
        static_assert_check(is_same<decltype(&EXTOnObjectDamageApplyProcess), LPOnObjectDamageApplyProcess>::value, "EXTOnObjectDamageApplyProcess is incorrect, please fix this.");
    }

    //0.5.1 listeners
    __if_exists(EXTOnMapLoad) {
        static_assert_check(is_same<decltype(&EXTOnMapLoad), LPOnMapLoad>::value, "EXTOnMapLoad is incorrect, please fix this.");
    }

    __if_exists(EXTOnVehicleAIEntry) {
        static_assert_check(is_same<decltype(&EXTOnVehicleAIEntry), LPOnVehicleAIEntry>::value, "EXTOnVehicleAIEntry is incorrect, please fix this.");
    }

    __if_exists(EXTOnEquipmentDropCurrent) {
        static_assert_check(is_same<decltype(&EXTOnEquipmentDropCurrent), LPOnEquipmentDropCurrent>::value, "EXTOnEquipmentDropCurrent is incorrect, please fix this.");
    }

    __if_exists(EXTOnServerStatus) {
        static_assert_check(is_same<decltype(&EXTOnServerStatus), LPOnServerStatus>::value, "EXTOnServerStatus is incorrect, please fix this.");
    }

    //0.5.2.3 listeners
    __if_exists(EXTOnPlayerUpdate) {
        static_assert_check(is_same<decltype(&EXTOnPlayerUpdate), LPOnPlayerUpdate>::value, "EXTOnPlayerUpdate is incorrect, please fix this.");
    }
    __if_exists(EXTOnMapReset) {
        static_assert_check(is_same<decltype(&EXTOnMapReset), LPVoidFunction>::value, "EXTOnMapReset is incorrect, please fix this.");
        //#pragma WARNING("EXTOnMapReset listener is NOT available in Halo Trial version!") //Always warn even if it's not within...
    }
	//0.5.3.0 listener
    __if_exists(EXTOnObjectCreationAttempt) {
        static_assert_check(is_same<decltype(&EXTOnObjectCreationAttempt), LPVoidFunction>::value, "EXTOnObjectCreationAttempt is incorrect, please fix this.");
    }

        //Database APIs verification
    __if_exists(EXTHookDatabase) {
        typedef void (WINAPIC* LPReturnVOID)();
        typedef SQLINTEGER (WINAPIC* LPReturnSQLINTEGER)();
        typedef bool (WINAPIC* LPIDReturnBOOL)(int StmtID);
        typedef void (WINAPIC* LPIDReturnVOID)(int StmtID);
        typedef SQLINTEGER (WINAPIC* LPIDReturnSQLINTEGER)(int StmtID);
        typedef SQLSMALLINT (WINAPIC* LPIDReturnSQLSMALLINT)(int StmtID);
        typedef SQLINTEGER (WINAPIC* LPIDReturnTypeSQLINTEGER)(int StmtID, SQLWCHAR* Name);
        typedef bool (WINAPIC* LPSTMTFetchRowAR)(int StmtID, SQLINTEGER nRow, bool Absolute);
        typedef bool (WINAPIC* LPSTMTFetchRow)(int StmtID, SQLUSMALLINT nRow);
        typedef bool (WINAPIC* LPIDReturnTypeBOOLSQLUSMALLINT)(int StmtID, SQLUSMALLINT Column);
        typedef SQLSMALLINT (WINAPIC* LPSTMTColumnTypeScale)(int StmtID, SQLUSMALLINT Column);
        typedef SQLUSMALLINT (WINAPIC* LPSTMTColumnByName)(int StmtID, SQLWCHAR* Name);
        typedef SQLUINTEGER (WINAPIC* LPSTMTColumnSize)(int StmtID, SQLUSMALLINT Column);
        typedef bool (WINAPIC* LPIDSQLWCHARReturnBOOL)(int StmtID, const SQLWCHAR* Name);
        typedef SQLUINTEGER (WINAPIC* LPIDSQLUSMALLINTReturnTypeSQLUINTEGER)(int StmtID, SQLUSMALLINT Column);
        typedef bool (WINAPIC* LPDBConnect)(SQLWCHAR* ConnectionStr, SQLINTEGER Option, SQLPOINTER Param, SQLINTEGER Param_len);
        typedef bool (WINAPIC* LPSTMTColumnName)(int StmtID, SQLUSMALLINT Column, SQLWCHAR* Name, SQLSMALLINT NameLen);
        typedef bool (WINAPIC* LPSTMTData)(int StmtID, SQLUSMALLINT Column, SQLPOINTER pBuffer, SQLINTEGER pBufLen, SQLINTEGER* dataLen, SQLSMALLINT Type);
        typedef bool (WINAPIC* LPSTMTBindColumn)(int StmtID, SQLUSMALLINT Column, SQLPOINTER pBuffer, SQLINTEGER pBufferSize, SQLINTEGER* pReturnedBufferSize, SQLUSMALLINT nType);
        static_assert_check(is_same<decltype(&EXTHookDatabase), LPReturnBOOL>::value, "EXTHookDatabase is incorrect, please fix this.");
        //static_assert_check(is_same<decltype(&EXTOnDatabaseNew), LPReturnBOOL>::value, "EXTOnDatabaseNew is incorrect, please fix this.");
        //static_assert_check(is_same<decltype(&EXTOnDatabaseDestroy), LPReturnVOID>::value, "EXTOnDatabaseDestroy is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseConnect), LPDBConnect>::value, "EXTOnDatabaseConnect is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseDisconnect), LPVoidFunction>::value, "EXTOnDatabaseDisconnect is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatus), LPReturnSQLINTEGER>::value, "EXTOnDatabaseStatus is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementNew), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementNew is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementDestroy), LPIDReturnVOID>::value, "EXTOnDatabaseStatementDestroy is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnCount), LPIDReturnSQLSMALLINT>::value, "EXTOnDatabaseStatementColumnCount is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementChangedRowCount), LPIDReturnSQLINTEGER>::value, "EXTOnDatabaseStatementChangedRowCount is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementQuery), LPIDSQLWCHARReturnBOOL>::value, "EXTOnDatabaseStatementQuery is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetch), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetch is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchRow), LPSTMTFetchRow>::value, "EXTOnDatabaseStatementFetchRow is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchPrevious), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchPrevious is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchNext), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchNext is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchRowAR), LPSTMTFetchRowAR>::value, "EXTOnDatabaseStatementFetchRowAR is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchFirst), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchFirst is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchLast), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchLast is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementCancel), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementCancel is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementBindColumn), LPSTMTBindColumn>::value, "EXTOnDatabaseStatementBindColumn is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnByName), LPSTMTColumnByName>::value, "EXTOnDatabaseStatementColumnByName is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementData), LPSTMTData>::value, "EXTOnDatabaseStatementData is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnType), LPSTMTColumnTypeScale>::value, "EXTOnDatabaseStatementColumnType is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnSize), LPSTMTColumnSize>::value, "EXTOnDatabaseStatementColumnSize is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnScale), LPSTMTColumnTypeScale>::value, "EXTOnDatabaseStatementColumnScale is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnName), LPSTMTColumnName>::value, "EXTOnDatabaseStatementColumnName is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementIsColumnNullable), LPIDReturnTypeBOOLSQLUSMALLINT>::value, "EXTOnDatabaseStatementIsColumnNullable is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementIsValid), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementIsValid is incorrect, please fix this.");
    }

        //Console API verification
    __if_exists(EXTHookConsole) {
        static_assert_check(is_same<decltype(&EXTHookConsole), LPReturnBOOL>::value, "EXTHookConsole is incorrect, please fix this.");
    }

        //Timer API verification
    #ifdef EXT_ITIMER
        typedef void (WINAPIC* LPOnTimerExecute)(unsigned int);
        typedef void (WINAPIC* LPOnTimerCancel)(unsigned int);
        static_assert_check(is_same<decltype(&EXTOnTimerExecute), LPOnTimerExecute>::value, "EXTOnTimerExecute is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnTimerCancel), LPOnTimerCancel>::value, "EXTOnTimerCancel is incorrect, please fix this.");
    #endif
}
#else
    assert("EXTOnTimerCancel is incorrect, please fix this.");
#endif
#endif