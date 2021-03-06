#ifndef objectH
#define objectH
#include "player.h" //Required in order to obtain playerInfo structure.

#ifdef __cplusplus
CNATIVE{
#endif

    // Structure definitions
#pragma pack(push, 1)
    // Structure of tag index table
    typedef struct hTagIndexTableHeader {
        unsigned int next_ptr;
        unsigned int starting_index; // ??
        unsigned int unk;
        unsigned int entityCount;
        unsigned int unk1;
        unsigned int readOffset;
        unsigned char unk2[8];
        unsigned int readSize;
        unsigned int unk3;
    } hTagIndexTableHeader;
    //http://stackoverflow.com/questions/24193389/how-to-use-anonymous-unions-with-enums
    // Structure of the tag header
    typedef struct hTagHeader {
        e_tag_group group_tag;      //0x00 // ie weap
        e_tag_group parent_tags[2]; //0x04 // ie weap
        s_ident ident;              //0x0C // unique ident for map
        char* tag_name;             //0x10 // name of tag
        void* group_meta_tag;       //0x14 // data for this group_tag
        void* parent_meta_tag[2];   //0x18 // data for this parent_tags[i]
    } hTagHeader;
    static_assert_check(sizeof(hTagHeader) == 0x20, "Incorrect size of hTagHeader");
    typedef struct objDamageFlags {
        bool isExplode : 1;         //0x00.0 grenade, banshee's secondary weapon, flamethrower applies here. Need better name.
        bool Unknown0 : 1;          //0x00.1
        bool Unknown1 : 1;          //0x00.2
        bool isWeapon : 1;          //0x00.3 Confirmed player's weapon, vehicle's weapon show up here every time.
        bool Unknown2 : 1;          //0x00.4
        bool ignoreShield : 1;      //0x00.5
        bool Unknown4 : 2;          //0x00.6-7
        unsigned char Unknown6[3];  //0x01-4
    } objDamageFlags;
    static_assert_check(sizeof(objDamageFlags) == 0x4, "Incorrect size of objDamageFlags");
    typedef struct objDamageInfo {
        s_ident tag_id;
        objDamageFlags flags;
        s_ident player_causer;
        s_ident causer;           // obj of causer
        char Unknown0[0x30];
        float modifier;         // 1.0 = max dmg, < 0 decreases dmg.
        float modifier1;        // 1.0 default > 1.0 increases dmg.
        char Unknown1[8];
    } objDamageInfo;
    static_assert_check(sizeof(objDamageInfo) == 0x50, "Incorrect size of objDamageInfo");
    typedef struct objHitInfo {
        char desc[0x20];
        char Unknown0[0x28];    // doesn't seem to be that useful, mostly 0s with a few 1.0 floats.    
    } objHitInfo;
    static_assert_check(sizeof(objHitInfo) == 0x48, "Incorrect size of objHitInfo");
    typedef struct objManaged {
        real_vector3d world;
        real_vector3d velocity;
        real_vector3d rotation;
        real_vector3d scale;
    } objManaged;
    typedef struct objCreationInfo {
        s_ident map_id;
        s_ident parent_id;
        real_vector3d   pos;
    } objCreationInfo;
#pragma pack(pop)
#ifdef EXT_IOBJECT
    typedef struct objTagGroupList {
        unsigned int count;
        hTagHeader** tag_list;
#ifdef __cplusplus
        objTagGroupList() {
            count = 0;
            tag_list = 0;
        }
        ~objTagGroupList() {
            if (tag_list)
                pIUtil->m_freeMem(tag_list);
        }
#endif
    } objTagGroupList;
    typedef struct IObject {
        /// <summary>
        /// Get pointer of object's active structure.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Return pointer of object's active structure or null.</returns>
        s_object* (*m_get_address)(s_ident obj_id);
        /// <summary>
        /// Lookup tag object and return object's tag header.
        /// </summary>
        /// <param name="objectTag">Unique asset tag s_ident.</param>
        /// <returns>Return pointer of tag header of an asset tag.</returns>
        hTagHeader* (*m_lookup_tag)(s_ident objectTag);
        /// <summary>
        /// Lookup tag object by type and name of a tag.
        /// </summary>
        /// <param name="tagType">Type of tag.</param>
        /// <param name="tag">Name of an asset tag.</param>
        /// <returns>Return pointer of tag header of an asset tag.</returns>
        hTagHeader* (*m_lookup_tag_type_name)(e_tag_group group_tag, const char* tag_name);
        /// <summary>
        /// To destroy an existing object.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Return true if successful destruction, false if unable to destroy.</returns>
        bool (*m_destroy)(s_ident obj_id);
        /// <summary>
        /// To copy existing object at specific player.
        /// </summary>
        /// <param name="model_Tag">Unique asset tag s_ident.</param>
        /// <param name="plI">PlayerInfo</param>
        /// <returns>Return true or false if unable to copy.</returns>
        bool (*m_copy)(s_ident model_Tag, PlayerInfo plI);
        /// <summary>
        /// Eject object, usually bipeds, from enterable object. (NOTE: This does not instant eject object if there's an eject animation involved.)
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Return true or false if unable to eject.</returns>
        bool (*m_eject)(s_ident obj_id);
        /// <summary>
        /// Update an object action to players. (Currently supported for ammo count and shield.)
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Does not return any value.</returns>
        void (*m_update)(s_ident obj_id);
        /// <summary>
        /// To kill an object, usually bipeds, with existing health.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Does not return any value.</returns>
        void (*m_kill)(s_ident obj_id);
        /// <summary>
        /// To create an object.
        /// </summary>
        /// <param name="model_Tag">Unique asset tag s_ident.</param>
        /// <param name="parentId">Owner of an object.</param>
        /// <param name="idlingTime">How much time, in ticks, idling permitted before remove from arena.</param>
        /// <param name="out_objId">Unique s_ident of an object creation.</param>
        /// <param name="location">Location to spawn at.</param>
        /// <returns>Return true or false if unable to create an object.</returns>
        bool (*m_create)(s_ident model_Tag, s_ident parentId, int idlingTime, s_ident* out_objId, const real_vector3d* location);
        /// <summary>
        /// Assign equipment to biped.
        /// </summary>
        /// <param name="biped_id">Unique s_ident of an biped created.</param>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <returns>Return true or false if unable to assign equipment.</returns>
        bool (*m_equipment_assign)(s_ident biped_id, s_ident obj_id);
        /// <summary>
        /// Move an object to another location.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <param name="obj_setup"></param>
        /// <returns>Does not return any value.</returns>
        void (*m_move)(s_ident obj_id, objManaged obj_setup);
        /// <summary>
        /// Drop current equipment from biped.
        /// </summary>
        /// <param name="biped_id">Unique s_ident of an biped created.</param>
        /// <returns>Return true or false if unable to drop current equipment.</returns>
        bool (*m_equipment_drop_current)(s_ident biped_id);
        /// <summary>
        /// Move and reset an object.
        /// </summary>
        /// <param name="obj_id">Unique s_ident of an object created.</param>
        /// <param name="location">Location to move at.</param>
        /// <returns>Does not return any value.</returns>
        void (*m_move_and_reset)(s_ident obj_id, const real_vector3d* location);
        /// <summary>
        /// Set object, usually cheats, to specific player. NOTE: Make sure you set it back to zero after you're done using it!
        /// </summary>
        /// <param name="pl_ind">Player index</param>
        /// <returns>Does not return any value.</returns>
        void (*m_set_object_spawn_player_x)(playerindex pl_ind);
        /// <summary>
        /// Obtain list of specific object tags.
        /// </summary>
        /// <param name="tag_group">Find specific object tag group.</param>
        /// <param name="tag_list">Output list of specific object tags.</param>
        /// <returns>Return true or false if unable to find tag group.</returns>
        bool (*m_get_lookup_group_tag_list)(e_tag_group tag_group, objTagGroupList* tag_list);
        /// <summary>
        /// Apply damage to specific object. (WARNING: May not be safe to use on certain custom maps.)
        /// </summary>
        /// <param name="receiver">An object receive the damage.</param>
        /// <param name="causer">An object cause the damage.</param>
        /// <param name="multiply">Mulitply the damage.</param>
        /// <param name="flags">Type of damage flags</param>
        /// <returns>Return true or false if unable to apply generic damage.</returns>
        bool (*m_apply_damage_generic)(s_ident receiver, s_ident causer, float multiply, objDamageFlags flags);
        /// <summary>
        /// Apply damage to specific object.
        /// </summary>
        /// <param name="receiver">An object receive the damage.</param>
        /// <param name="causer">An object cause the damage.</param>
        /// <param name="tag">Apply type of tag damage to receiver.</param>
        /// <param name="multiply">Mulitply the damage.</param>
        /// <param name="flags">Type of damage flags</param>
        /// <returns>Return true or false if unable to apply custom damage.</returns>
        void (*m_apply_damage_custom)(s_ident receiver, s_ident causer, const hTagHeader* tag, float multiply, objDamageFlags flags);
    } IObject;

CNATIVE dllport IObject* getIObject(unsigned int hash);
#endif

#ifdef __cplusplus
}
#endif

#endif