#ifndef expCheckerH
#define expCheckerH

#if _MSC_VER <=1500
#pragma WARNING("Unable to perform exporting checker for Microsoft Visual Studio 2008 and below...")
#else
namespace addon {
#pragma region Interface functions
    typedef bool (WINAPIC* LPReturnBOOL)();
    typedef void (WINAPIC* LPVoidFunction)();
    typedef void (WINAPIC* LPOnEndGame)(int);
    typedef toggle (WINAPIC* LPONoParamsReturn)();
    typedef void (WINAPIC* LPOnNewGame)(wchar_t*);
    typedef void (WINAPIC* LPOnPlayerJoinQuitEvent)(IPlayer::PlayerInfo);
    typedef void (WINAPIC* LPOnPlayerUpdate)(IPlayer::PlayerInfo);
    typedef int (WINAPIC* LPOnPlayerJoinDefault)(MachineS* mS, int);
    typedef bool (WINAPIC* LPOnVehicleUserEntryEject)(IPlayer::PlayerInfo, bool);
    typedef bool (WINAPIC* LPOnPlayerChangeTeamAttempt)(IPlayer::PlayerInfo, int, bool);
    typedef bool (WINAPIC* LPOnPlayerSpawnColor)(IPlayer::PlayerInfo, bool);
    typedef void (WINAPIC* LPOnPlayerDeath)(IPlayer::PlayerInfo, IPlayer::PlayerInfo, int, bool&);
    typedef void (WINAPIC* LPWeaponAssignmentC)(IPlayer::PlayerInfo, ident, ident, DWORD, ident&);
    typedef bool (WINAPIC* LPObjectInteraction)(IPlayer::PlayerInfo, ident, ObjectS*, IObject::hTagHeader*);
    typedef void (WINAPIC* LPWeaponAssignmentD)(IPlayer::PlayerInfo, ident, IObject::objInfo*, DWORD, ident&);
    typedef bool (WINAPIC* LPOnPlayerScoreCTF)(IPlayer::PlayerInfo, ident, DWORD, bool);
    typedef bool (WINAPIC* LPOnPlayerAttemptDropObject)(IPlayer::PlayerInfo, ident, BipedS*);
    typedef void (WINAPIC* LPOnPlayerSpawn)(IPlayer::PlayerInfo, ident, BipedS*);
    typedef toggle (WINAPIC* LPOnPlayerValidateConnect)(IPlayer::PlayerExtended, MachineS, banCheckStruct, bool, toggle, toggle);
    typedef bool (WINAPIC* LPOnWeaponReload)(ObjectS*, bool);
    typedef bool (WINAPIC* LPOnObjectCreation)(ObjectS*, IObject::hTagHeader*);
    typedef bool (WINAPIC* LPOnKillMultiplier)(IPlayer::PlayerInfo killer, DWORD multiplier);
    typedef bool (WINAPIC* LPOnVehicleRespawnProcess)(ObjectS* cur_object, IObject::objManaged& managedObj, bool isManaged);
    typedef bool (WINAPIC* LPOnObjectDeleteManagement)(ObjectS* cur_object, int curTicks, bool isManaged);
    typedef bool (WINAPIC* LPOnObjectDamageLookupProcess)(IObject::objDamageInfo& damageInfo, ident& obj_recv, bool& allowDamage, bool isManaged);
    typedef bool (WINAPIC* LPOnObjectDamageApplyProcess)(const IObject::objDamageInfo& damageInfo, ident& obj_recv, IObject::objHitInfo& hitInfo, bool isBacktap, bool& allowDamage, bool isManaged);
    typedef void (WINAPIC* LPOnMapLoad)(ident, const wchar_t[32]);
    typedef toggle (WINAPIC* LPOnVehicleAIEntry)(ident, ident, WORD, toggle);
    typedef toggle (WINAPIC* LPOnEquipmentDropCurrent)(IPlayer::PlayerInfo, ident, BipedS*, ident, WeaponS*, toggle);
    typedef toggle (WINAPIC* LPOnServerStatus)(int, toggle);
    
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
    static_assert_check(is_same<decltype(EXTversion), addon::versionEAO>::value, "EXTversion is incorrect, please fix this.");
    static_assert_check(is_same<decltype(&EXTOnEAOLoad), LPONoParamsReturn>::value, "EXTOnEAOLoad is incorrect, please fix this.");
    static_assert_check(is_same<decltype(&EXTOnEAOUnload), LPVoidFunction>::value, "EXTOnEAOUnload is incorrect, please fix this.");
    static_assert_check(is_same<decltype(EXTPluginInfo), addon::addonInfo*>::value, "EXTPluginInfo is incorrect, please fix this.");


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

        //Database APIs verification
    __if_exists(EXTHookDatabase) {
        typedef toggle (WINAPIC* LPIDReturn)(int);
        typedef bool (WINAPIC* LPIDReturnBOOL)(int StmtID);
        typedef DWORD (WINAPIC* LPIDReturnDWORD)(int StmtID);
        typedef DBSQL::USHORT (WINAPIC* LPIDReturnUSHORT)(int StmtID);
        typedef int (WINAPIC* LPIDReturnTypeINT)(int StmtID, LPCWSTR Name);
        typedef DWORD (WINAPIC* LPIDReturnTypeDWORD)(int StmtID, LPCWSTR Name);
        typedef bool (WINAPIC* LPDBRowAR)(int StmtID, UINT nRow, bool Absolute);
        typedef bool (WINAPIC* LPIDReturnTypeBOOLUSHORT)(int StmtID, DBSQL::USHORT Column);
        typedef int (WINAPIC* LPIDReturnTypeINTUS)(int StmtID, DBSQL::USHORT Column);
        typedef DBSQL::USHORT (WINAPIC* LPIDReturnTypeUSHORT)(int StmtID, LPCWSTR Name);
        typedef bool (WINAPIC* LPIDReturnTypeBOOL)(int StmtID, LPCWSTR Name);
        typedef DWORD (WINAPIC* LPIDReturnTypeDWORDUS)(int StmtID, DBSQL::USHORT Column);
        typedef bool (WINAPIC* LPDBConnect)(LPCWSTR MDBPath,LPCWSTR User, LPCWSTR Pass,bool Exclusive);
        typedef bool (WINAPIC* LPDBName)(int StmtID, DBSQL::USHORT Column, LPWSTR Name, SHORT NameLen);
        typedef bool (WINAPIC* LPDBData)(int StmtID, DBSQL::USHORT Column, LPVOID pBuffer, DBSQL::ULONG pBufLen, LONG * dataLen, int Type);
        typedef bool (WINAPIC* LPDBBindColumn)(int StmtID, DBSQL::USHORT Column, LPVOID pBuffer, DBSQL::ULONG pBufferSize, LONG * pReturnedBufferSize, DBSQL::USHORT nType);
        static_assert_check(is_same<decltype(&EXTHookDatabase), LPReturnBOOL>::value, "EXTHookDatabase is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseNew), LPONoParamsReturn>::value, "EXTOnDatabaseNew is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseDestroy), LPONoParamsReturn>::value, "EXTOnDatabaseDestroy is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseConnect), LPDBConnect>::value, "EXTOnDatabaseConnect is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseDisconnect), LPVoidFunction>::value, "EXTOnDatabaseDisconnect is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementNew), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementNew is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementDestroy), LPIDReturn>::value, "EXTOnDatabaseStatementDestroy is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnCount), LPIDReturnUSHORT>::value, "EXTOnDatabaseStatementColumnCount is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementChangedRowCount), LPIDReturnDWORD>::value, "EXTOnDatabaseStatementChangedRowCount is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementQuery), LPIDReturnTypeBOOL>::value, "EXTOnDatabaseStatementQuery is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetch), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetch is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchRow), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchRow is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchPrevious), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchPrevious is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchNext), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchNext is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchRowAR), LPDBRowAR>::value, "EXTOnDatabaseStatementFetchRowAR is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchFirst), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchFirst is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementFetchLast), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementFetchLast is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementCancel), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementCancel is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementBindColumn), LPDBBindColumn>::value, "EXTOnDatabaseStatementBindColumn is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnByName), LPIDReturnTypeUSHORT>::value, "EXTOnDatabaseStatementColumnByName is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementData), LPDBData>::value, "EXTOnDatabaseStatementData is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnType), LPIDReturnTypeINTUS>::value, "EXTOnDatabaseStatementColumnType is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnSize), LPIDReturnTypeDWORDUS>::value, "EXTOnDatabaseStatementColumnSize is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnScale), LPIDReturnTypeDWORDUS>::value, "EXTOnDatabaseStatementColumnScale is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementColumnName), LPDBName>::value, "EXTOnDatabaseStatementColumnName is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementIsColumnNullable), LPIDReturnTypeBOOLUSHORT>::value, "EXTOnDatabaseStatementIsColumnNullable is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnDatabaseStatementIsValid), LPIDReturnBOOL>::value, "EXTOnDatabaseStatementIsValid is incorrect, please fix this.");
    }

        //Console API verification
    __if_exists(EXTHookConsole) {
        static_assert_check(is_same<decltype(&EXTHookConsole), LPReturnBOOL>::value, "EXTHookConsole is incorrect, please fix this.");
    }

        //Timer API verification
    #ifdef EXT_ITIMER
        typedef void (WINAPIC* LPOnTimerExecute)(DWORD);
        typedef void (WINAPIC* LPOnTimerCancel)(DWORD);
        static_assert_check(is_same<decltype(&EXTOnTimerExecute), LPOnTimerExecute>::value, "EXTOnTimerExecute is incorrect, please fix this.");
        static_assert_check(is_same<decltype(&EXTOnTimerCancel), LPOnTimerCancel>::value, "EXTOnTimerCancel is incorrect, please fix this.");
    #endif
}
#endif
#endif