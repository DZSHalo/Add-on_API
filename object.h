#ifndef objectH
#define objectH
#include "player.h" //Required in order to obtain playerInfo structure.
CNATIVE class IObject {
public:
    // Structure definitions
    #pragma pack(push, 1)
    // Structure of tag index table
    struct hTagIndexTableHeader {
        DWORD next_ptr;
        DWORD starting_index; // ??
        DWORD unk;
        DWORD entityCount;
        DWORD unk1;
        DWORD readOffset;
        BYTE unk2[8];
        DWORD readSize;
        DWORD unk3;
    };
    // Structure of the tag header
    struct hTagHeader {
        DWORD tagType1;     //0x00 // ie weap
        DWORD tagType2;     //0x04 // I don't know
        DWORD tagType3;     //0x08 // I don't know
        ident id;           //0x0C // unique id for map
        char* tagName;      //0x10 // name of tag
        DWORD* metaData1;   //0x14 // data for this tagType1
        DWORD* metaData2;   //0x18 // data for this tagType2
        DWORD* metaData3;   //0x1C // data for this tagType3
    };
    static_assert_check(sizeof(hTagHeader) == 0x20, "Incorrect size of hTagHeader");
    struct objDamageFlags {
        bool isExplode: 1;  //0x00.0 grenade, banshee's secondary weapon, flamethrower applies here. Need better name.
        bool Unknown0: 2;   //0x00.1-2
        bool isWeapon: 1;   //0x00.3 Confirmed player's weapon, vehicle's weapon show up here every time.

        bool Unknown2: 4;   //0x01.0-4
        BYTE Unknown6[3];   //0x02-4
    };
    static_assert_check(sizeof(objDamageFlags) == 0x4, "Incorrect size of objDamageFlags");
    struct objDamageInfo {
        ident tag_id;
        objDamageFlags flags;
        ident player_causer;
        ident causer;           // obj of causer
        char Unknown0[0x30];
        float modifier;         // 1.0 = max dmg, < 0 decreases dmg.
        float modifier1;        // 1.0 default > 1.0 increases dmg.
        char Unknown1[8];
    };
    static_assert_check(sizeof(objDamageInfo) == 0x50, "Incorrect size of objDamageInfo");
    struct objHitInfo {
        char desc[0x20];
        char Unknown0[0x28];    // doesn't seem to be that useful, mostly 0s with a few 1.0 floats.    
    };
    static_assert_check(sizeof(objHitInfo) == 0x48, "Incorrect size of objHitInfo");
    struct objInfo {
        DWORD tagType;
        char* tagName;
        DWORD empty;
        ident mapObjId;
    };
    struct objManaged {
        vect3 world;
        vect3 velocity;
        vect3 rotation;
        vect3 scale;
    };
    enum e_tag_types {
        TAG_ACTV = 'actv',
        TAG_ANT  = 'ant!',
        TAG_ANTR = 'antr',
        TAG_BIPD = 'bipd',
        TAG_BTIM = 'bitm',
        TAG_COLL = 'coll',
        TAG_COLO = 'colo',
        TAG_CONT = 'cont',
        TAG_DECA = 'deca',
        TAG_DELA = 'DeLa',
        TAG_DEVC = 'devc',
        TAG_EFFE = 'effe',
        TAG_EQIP = 'eqip',
        TAG_FLAG = 'flag',
        TAG_FONT = 'font',
        TAG_FOOT = 'foot',
        TAG_GRHI = 'grhi',
        TAG_HMT = 'hmt ',
        TAG_HUD = 'hud#',
        TAG_HUDG = 'hudg',
        TAG_ITMC = 'itmc',
        TAG_JPT = 'jpt!',
        TAG_LENS = 'lens',
        TAG_LIGH = 'ligh',
        TAG_LSND = 'lsnd',
        TAG_MATG = 'matg',
        TAG_METR = 'metr',
        TAG_MGS2 = 'mgs2',
        TAG_MOD2 = 'mod2',
        TAG_PART = 'part',
        TAG_PCTL = 'pctl',
        TAG_PHYS = 'phys',
        TAG_PPHY = 'pphy',
        TAG_PROJ = 'proj',
        TAG_SBSP = 'sbsp',
        TAG_SCEN = 'scen',
        TAG_SCEX = 'scex',
        TAG_SCHI = 'schi',
        TAG_SCNR = 'scnr',
        TAG_SENV = 'senv',
        TAG_SGLA = 'sgla',
        TAG_SKY = 'sky ',
        TAG_SMET = 'smet',
        TAG_SND = 'snd!',
        TAG_SNDE = 'snde',
        TAG_SOUL = 'Soul',
        TAG_SPLA = 'spla',
        TAG_SSCE = 'ssce',
        TAG_STR = 'str#',
        TAG_TAGC = 'tagc',
        TAG_TRAK = 'trak',
        TAG_UDLG = 'udlg',
        TAG_UNHI = 'unhi',
        TAG_USTR = 'ustr',
        TAG_VEHI = 'vehi',
        TAG_WEAP = 'weap',
        TAG_WIND = 'wind',
        TAG_WPHI = 'wphi'
    };
    #pragma pack(pop)
    virtual ObjectS* WINAPIC GetObjectAddress(ident obj_id)=0;
    virtual hTagHeader* WINAPIC LookupTag(ident objectTag)=0;
    virtual hTagHeader* WINAPIC LookupTagTypeName(const char* tagType, const char* tag)=0;
    virtual bool WINAPIC Delete(ident obj_id)=0;
    virtual bool WINAPIC Copy(ident* model_Tag, IPlayer::PlayerInfo plI)=0;
    virtual bool WINAPIC Eject(ident obj_id)=0;
    virtual void WINAPIC Update(ident obj_id)=0;
    virtual void WINAPIC Kill(ident obj_id)=0;
    virtual bool WINAPIC Create(ident obj_id, ident parentId, int idlingTime, ident& out_objId, vect3* location)=0;
    virtual bool WINAPIC EquipmentAssign(ident biped_id, ident obj_id)=0;
    virtual void WINAPIC Move(ident obj_id, IObject::objManaged obj_setup)=0;
    virtual bool WINAPIC EquipmentDropCurrent(ident biped_id)=0;
    virtual void WINAPIC MoveAndReset(ident obj_id, vect3* location)=0;
};
#ifdef EXT_IOBJECT
CNATIVE dllport IObject* pIObject;
#endif

#endif