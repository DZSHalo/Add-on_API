module Add_on_API.D.object;

import Add_on_API.Add_on_API;

static if(__traits(compiles, EXT_IOBJECT)) {
// #define objectH
//import Add_on_API.player; //Required in order to obtain playerInfo structure.

    // Structure definitions
// #pragma pack(push, 1)
    // Structure of tag index table
    struct hTagIndexTableHeader {
        uint next_ptr;
        uint starting_index; // ??
        uint unk;
        uint entityCount;
        uint unk1;
        uint readOffset;
        ubyte unk2[8];
        uint readSize;
        uint unk3;
    };
    // Structure of the tag header
    struct hTagHeader {
        e_tag_group group_tag;      //0x00 // ie weap
        e_tag_group parent_tags[2]; //0x04 & 0x08 // ie weap
        s_ident id;                 //0x0C // unique id for map
        char* tagName;              //0x10 // name of tag
        uint* group_meta_tag;       //0x14 // data for this group_tag
        uint* parent_meta_tag[2];   //0x18 // data for this parent_tags[i]
    };
    static assert(hTagHeader.sizeof == 0x20, "Incorrect size of hTagHeader");
    struct objDamageFlags {
        ubyte bitFieldFlag;
        /*bool isExplode : 1;  //0x00.0 grenade, banshee's secondary weapon, flamethrower applies here. Need better name.
        bool Unknown0 : 2;   //0x00.1-2
        bool isWeapon : 1;   //0x00.3 Confirmed player's weapon, vehicle's weapon show up here every time.

        bool Unknown2 : 4;   //0x01.0-4*/
        ubyte Unknown6[3];   //0x02-4
    };
    static assert(objDamageFlags.sizeof == 0x4, "Incorrect size of objDamageFlags");
    struct objDamageInfo {
        s_ident tag_id;
        objDamageFlags flags;
        s_ident player_causer;
        s_ident causer;           // obj of causer
        char Unknown0[0x30];
        float modifier;         // 1.0 = max dmg, < 0 decreases dmg.
        float modifier1;        // 1.0 default > 1.0 increases dmg.
        char Unknown1[8];
    };
    static assert(objDamageInfo.sizeof == 0x50, "Incorrect size of objDamageInfo");
    struct objHitInfo {
        char desc[0x20];
        char Unknown0[0x28];    // doesn't seem to be that useful, mostly 0s with a few 1.0 floats.    
    };
    static assert(objHitInfo.sizeof == 0x48, "Incorrect size of objHitInfo");
    struct objManaged {
        vect3 world = vect3( -1, -1, -1 );
        vect3 velocity = vect3( -1, -1, -1 );
        vect3 rotation = vect3( -1, -1, -1 );
        vect3 scale = vect3( -1, -1, -1 );
    };
// #pragma pack(pop)
    extern (C) struct IObject {
        /*
         * Get pointer of object's active structure.
         * Params:
         * obj_id = Unique s_ident of an object created.
         * Returns: Return pointer of object's active structure or null.
         */
        s_object* function(s_ident obj_id) m_get_address;
        /*
         * Lookup tag object and return object's tag header.
         * Params:
         * objectTag = Unique asset tag s_ident.
         * Returns: Return pointer of tag header of an asset tag.
         */
        hTagHeader* function(s_ident objectTag) m_lookup_tag;
        /*
         * Lookup tag object by type and name of a tag.
         * Params:
         * tagType = Type of tag.
         * tag = Name of an asset tag.
         * Returns: Return pointer of tag header of an asset tag.
         */
        hTagHeader* function(const e_tag_group group_tag, const(char) * tag_name) m_lookup_tag_type_name;
        /*
         * To destroy an existing object.
         * Params:
         * obj_id = Unique s_ident of an object created.
         * Returns: Return true if successful destruction, false if unable to destroy.
         */
        bool function(s_ident obj_id) m_destroy;
        /*
         * To copy existing object at specific player.
         * Params:
         * model_Tag = Unique asset tag s_ident.
         * plI = PlayerInfo
         * Returns: Return true or false if unable to copy.
         */
        bool function(s_ident* model_Tag, PlayerInfo plI) m_copy;
        /*
         * Eject object, usually bipeds, from enterable object. (NOTE: This does not instant eject object if there's an eject animation involved.)
         * Params:
         * obj_id = Unique s_ident of an object created.
         * Returns: Return true or false if unable to eject.
         */
        bool function(s_ident obj_id) m_eject;
        /*
         * Update an object action to players. (Currently supported for ammo count and shield.)
         * Params:
         * obj_id = Unique s_ident of an object created.
         * Returns: Does not return any value.
         */
        void function(s_ident obj_id) m_update;
        /*
         * To kill an object, usually bipeds, with existing health.
         * Params:
         * obj_id = Unique s_ident of an object created.
         * Returns: Does not return any value.
         */
        void function(s_ident biped_id) m_kill;
        /*
         * To create an object.
         * Params:
         * model_Tag = Unique asset tag s_ident.
         * parentId = Owner of an object.
         * idlingTime = How much time, in ticks, idling permitted before remove from arena.
         * out_objId = Unique s_ident of an object creation.
         * location = Location to spawn at.
         * Returns: Return true or false if unable to create an object.
         */
        bool function(s_ident model_Tag, s_ident parentId, int idlingTime, ref s_ident  out_objId, vect3* location) m_create;
        /*
         * Assign equipment to biped.
         * Params:
         * biped_id = Unique s_ident of an biped created.
         * obj_id = Unique s_ident of an object created.
         * Returns: Return true or false if unable to assign equipment.
         */
        bool function(s_ident biped_id, s_ident obj_id) m_equipment_assign;
        /*
         * Move an object to another location.
         * Params:
         * obj_id = Unique s_ident of an object created.
         * obj_setup = 
         * Returns: Does not return any value.
         */
        void function(s_ident obj_id, objManaged obj_setup) m_move;
        /*
         * Drop current equipment from biped.
         * Params:
         * biped_id = Unique s_ident of an biped created.
         * Returns: Return true or false if unable to drop current equipment.
         */
        bool function(s_ident biped_id) m_equipment_drop_current;
        /*
         * Move and reset an object.
         * Params:
         * obj_id = Unique s_ident of an object created.
         * location = Location to move at.
         * Returns: Does not return any value.
         */
        void function(s_ident obj_id, vect3* location) m_move_and_reset;
        /*
         * Set object, usually cheats, to specific player. NOTE: Make sure you set it back to zero after you're done using it!
         * Params:
         * pl_ind = Player index
         * Returns: Does not return any value.
         */
        void function(playerindex pl_ind) m_set_object_spawn_player_x;
    };
    export extern(C) IObject* getIObject(uint hash);
}
