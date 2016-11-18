using System;
using System.Runtime.InteropServices;

public enum e_source_out : ushort {
    NONE = 0,
    A = 1,
    B = 2,
    C = 3,
    D = 4
};
public enum e_source_in : ushort {
    NONE = 0,
    A = 1,
    B = 2,
    C = 3,
    D = 4
};
public enum e_source_in_out : ushort {
    NONE = 0,
    A_IN = 1,
    B_IN = 2,
    C_IN = 3,
    D_IN = 4,
    A_OUT = 5,
    B_OUT = 6,
    C_OUT = 7,
    D_OUT = 8
};

public enum e_function :ushort {
    ONE = 0,
    ZERO,
    COSINE,
    COSINE_VARIABLE_PERIOD,
    DIAGONAL_WAVE,
    DIAGONAL_WAVE_VARIABLE_PERIOD,
    SLIDE,
    SLIDE_VARIABLE_PERIOD,
    NOISE,
    JITTER,
    WANDER,
    SPARK
};

public enum e_side_effect : ushort {
    NONE = 0,
    HARMLESS,
    LETHAL_TO_THE_UNSUSPECTING,
    EMP
};

public enum e_category : ushort {
    NONE = 0,
    FALLING,
    BULLET,
    GRENADE,
    HIGH_EXPLOSIVE,
    SNIPER,
    MELEE,
    FLAME,
    MOUNTED_WEAPON,
    VEHICLE,
    PLASMA,
    NEEDLE,
    SHOTGUN
};

public enum e_tag_group : uint {
    //TODO: Need testing for tags lookup to be sure this is correct or may need to be reversed.
    TAG_ACTR = 0x61637472, //actor
    TAG_ACTV = 0x61637476, //actor_variant
    TAG_ANT_ = 0x616e7421, //antenna
    TAG_ANTR = 0x616e7472, //model_animations
    TAG_BIPD = 0x62697064, //biped
    TAG_BTIM = 0x6269746d, //bitmap
    TAG_BOOM = 0x626f6f6d, //spheroid
    TAG_CDMG = 0x63646d67, //continuous_damage_effect
    TAG_COLL = 0x636f6c6c, //model_collision_geometry
    TAG_COLO = 0x636f6c6f, //color_table
    TAG_CONT = 0x636f6e74, //contrail
    TAG_CTRL = 0x6374726c, //device_control
    TAG_DECA = 0x64656361, //decal
    TAG_DELA = 0x44654c61, //ui_widget_definition
    TAG_DEVC = 0x64657663, //input_device_defaults
    TAG_DEVI = 0x64657669, //device
    TAG_DOBC = 0x646f6263, //detail_object_collection
    TAG_EFFE = 0x65666665, //effect
    TAG_ELEC = 0x656c6563, //lighting
    TAG_EQIP = 0x65716970, //equipment
    TAG_FLAG = 0x666c6167, //flag
    TAG_FOG_ = 0x666f6720, //fog
    TAG_FONT = 0x666f6e74, //font
    TAG_FOOT = 0x666f6f74, //material_effects
    TAG_GARB = 0x67617262, //garbage
    TAG_GLW_ = 0x676c7721, //glow
    TAG_GRHI = 0x67726869, //grenade_hud_interface
    TAG_HMT_ = 0x686d7420, //hud_message_text
    TAG_HUD_ = 0x68756423, //hud_number
    TAG_HUDG = 0x68756467, //hud_globals
    TAG_ITEM = 0x6974656d, //item
    TAG_ITMC = 0x69746d63, //item_collection
    TAG_JPT_ = 0x6a707421, //damage_effect
    TAG_LENS = 0x6c656e73, //lens_flare
    TAG_LIFI = 0x6c696669, //device_light_fixture
    TAG_LIGH = 0x6c696768, //light
    TAG_LSND = 0x6c736e64, //sound_looping
    TAG_MACH = 0x6d616368, //device_machine
    TAG_MATG = 0x6d617467, //globals
    TAG_METR = 0x6d657472, //meter
    TAG_MGS2 = 0x6d677332, //light_volume
    TAG_MOD2 = 0x6d6f6432, //gbxmodel
    TAG_MODE = 0x6d6f6465, //model
    TAG_MPLY = 0x6d706c79, //multiplayer_scenario_description
    TAG_NGPR = 0x6e677072, //preferences_network_game
    TAG_OBJE = 0x6f626a65, //object
    TAG_PART = 0x70617274, //particle
    TAG_PCTL = 0x7063746c, //particle_system
    TAG_PHYS = 0x70687973, //physics
    TAG_PLAC = 0x706c6163, //placeholder
    TAG_PPHY = 0x70706879, //point_physics
    TAG_PROJ = 0x70726f6a, //projectile
    TAG_RAIN = 0x7261696e, //weather_particle_system
    TAG_SBSP = 0x73627370, //scenario_structure_bsp
    TAG_SCEN = 0x7363656e, //scenery
    TAG_SCEX = 0x73636578, //shader_transparent_chicago_extended
    TAG_SCHI = 0x73636869, //shader_transparent_chicago
    TAG_SCNR = 0x73636e72, //scenario
    TAG_SENV = 0x73656e76, //shader_enviroment
    TAG_SGLA = 0x73676c61, //shader_transparent_glass
    TAG_SHDR = 0x73686472, //shader
    TAG_SKY_ = 0x736b7920, //sky
    TAG_SMET = 0x736d6574, //shader_transparent_meter
    TAG_SND_ = 0x736e6421, //sound
    TAG_SNDE = 0x736e6465, //sound_enviornment
    TAG_SOSO = 0x736f736f, //shader_model
    TAG_SOTR = 0x736f7472, //shader_transparent_generic
    TAG_SOUL = 0x536f756c, //ui_widget_collection
    TAG_SPLA = 0x73706c61, //shader_transparent_plasma
    TAG_SSCE = 0x73736365, //sound_scenery
    TAG_STR_ = 0x73747223, //string_list
    TAG_SWAT = 0x73776174, //shader_transparent_water
    TAG_TAGC = 0x74616763, //tag_collection
    TAG_TRAK = 0x7472616b, //camera_track
    TAG_UDLG = 0x75646c67, //dialogue
    TAG_UNHI = 0x756e6869, //unit_hud_interface
    TAG_UNIT = 0x756e6974, //unit
    TAG_USTR = 0x75737472, //unicode_string_list
    TAG_VEHI = 0x76656869, //vehicle
    TAG_VCKY = 0x76636b79, //virtual_keyboard
    TAG_WEAP = 0x77656170, //weapon
    TAG_WIND = 0x77696e64, //wind
    TAG_WPHI = 0x77706869  //weapon_hud_interface
}
