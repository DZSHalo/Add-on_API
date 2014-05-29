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
		DWORD tagType1; // ie weap
		DWORD tagType2; // I don't know
		DWORD tagType3; // I don't know
		DWORD id; // unique id for map
		char* tagName; // name of tag
		LPBYTE metaData; // data for this tag
		DWORD unk1[2]; // I don't know
	};
	struct objDamageFlags {
		bool isExplode: 1;	//0x00.0 grenade, banshee's secondary weapon, flamethrower applies here. Need better name.
		bool Unknown0: 2;	//0x00.1-2
		bool isWeapon: 1;	//0x00.3 Confirmed player's weapon, vehicle's weapon show up here every time.

		bool Unknown2: 4;	//0x01.0-4
		BYTE Unknown6[3];	//0x02-4
	};
	static_assert_check(sizeof(objDamageFlags) == 0x4, "Incorrect size of objDamageFlags");
	struct objDamageInfo {
		ident tag_id;
		objDamageFlags flags;
		ident player_causer;
		ident causer; // obj of causer
		char Unknown0[0x30];
		float modifier; // 1.0 = max dmg, < 0 decreases dmg.
		float modifier1; // 1.0 default > 1.0 increases dmg.
		char Unknown1[8];
	};
	static_assert_check(sizeof(objDamageInfo) == 0x50, "Incorrect size of objDamageInfo");
	struct objHitInfo {
		char desc[0x20];
		char Unknown0[0x28]; 	// doesn't seem to be that useful, mostly 0s with a few 1.0 floats.	
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
	#pragma pack(pop)
	virtual ObjectS* WINAPIC GetObjectAddress(int mode, ident obj_id)=0;
	virtual hTagHeader* WINAPIC LookupTag(ident objectTag)=0;
	virtual hTagHeader* WINAPIC LookupTagTypeName(const char* tagType, const char* tag)=0;
	virtual bool WINAPIC Delete(ident obj_id)=0;
	virtual bool WINAPIC Copy(ident* model_Tag, IPlayer::PlayerInfo plI)=0;
	virtual bool WINAPIC Eject(ident obj_id)=0;
	virtual void WINAPIC Update(ident obj_id)=0;
	virtual void WINAPIC Swap(ident biped_id, IPlayer::PlayerInfo plI)=0;
};
CNATIVE dllport IObject* pIObject;

#endif