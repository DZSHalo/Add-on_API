module Add_on_API.D.tags.tag_enum;

import Add_on_API.Add_on_API;

enum e_attachment_out : ushort {
    NONE  = 0,
    A_OUT = 1,
    B_OUT = 2,
    C_OUT = 3,
    D_OUT = 4
};
enum e_attachment_in : ushort {
    NONE = 0,
    A_IN = 1,
    B_IN = 2,
    C_IN = 3,
    D_IN = 4
};
enum e_attachment_in_out : ushort {
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

enum e_function :ushort {
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

//http://stackoverflow.com/questions/24193389/how-to-use-anonymous-unions-with-enums
enum e_tag_group : tagType {
    TAG_ACTR = tagType(MAKE_ID!("actr")), //actor
    TAG_ACTV = tagType(MAKE_ID!("actv")), //actor_variant
    TAG_ANT_ = tagType(MAKE_ID!("ant!")),  //antenna
    TAG_ANTR = tagType(MAKE_ID!("antr")), //model_animations
    TAG_BIPD = tagType(MAKE_ID!("bipd")), //biped
    TAG_BTIM = tagType(MAKE_ID!("bitm")), //bitmap
    TAG_BOOM = tagType(MAKE_ID!("boom")), //spheroid
    TAG_CDMG = tagType(MAKE_ID!("cdmg")), //continuous_damage_effect
    TAG_COLL = tagType(MAKE_ID!("coll")), //model_collision_geometry
    TAG_COLO = tagType(MAKE_ID!("colo")), //color_table
    TAG_CONT = tagType(MAKE_ID!("cont")), //contrail
    TAG_CTRL = tagType(MAKE_ID!("ctrl")), //device_control
    TAG_DECA = tagType(MAKE_ID!("deca")), //decal
    TAG_DELA = tagType(MAKE_ID!("DeLa")), //ui_widget_definition
    TAG_DEVC = tagType(MAKE_ID!("devc")), //input_device_defaults
    TAG_DEVI = tagType(MAKE_ID!("devi")), //device
    TAG_DOBC = tagType(MAKE_ID!("dobc")), //detail_object_collection
    TAG_EFFE = tagType(MAKE_ID!("effe")), //effect
    TAG_ELEC = tagType(MAKE_ID!("elec")), //lighting
    TAG_EQIP = tagType(MAKE_ID!("eqip")), //equipment
    TAG_FLAG = tagType(MAKE_ID!("flag")), //flag
    TAG_FOG_ = tagType(MAKE_ID!("fog ")), //fog
    TAG_FONT = tagType(MAKE_ID!("font")), //font
    TAG_FOOT = tagType(MAKE_ID!("foot")), //material_effects
    TAG_GARB = tagType(MAKE_ID!("garb")), //garbage
    TAG_GLW_ = tagType(MAKE_ID!("glw!")), //glow
    TAG_GRHI = tagType(MAKE_ID!("grhi")), //grenade_hud_interface
    TAG_HMT_ = tagType(MAKE_ID!("hmt ")), //hud_message_text
    TAG_HUD_ = tagType(MAKE_ID!("hud#")), //hud_number
    TAG_HUDG = tagType(MAKE_ID!("hudg")), //hud_globals
    TAG_ITEM = tagType(MAKE_ID!("item")), //item
    TAG_ITMC = tagType(MAKE_ID!("itmc")), //item_collection
    TAG_JPT_ = tagType(MAKE_ID!("jpt!")), //damage_effect
    TAG_LENS = tagType(MAKE_ID!("lens")), //lens_flare
    TAG_LIFI = tagType(MAKE_ID!("lifi")), //device_light_fixture
    TAG_LIGH = tagType(MAKE_ID!("ligh")), //light
    TAG_LSND = tagType(MAKE_ID!("lsnd")), //sound_looping
    TAG_MACH = tagType(MAKE_ID!("mach")), //device_machine
    TAG_MATG = tagType(MAKE_ID!("matg")), //globals
    TAG_METR = tagType(MAKE_ID!("metr")), //meter
    TAG_MGS2 = tagType(MAKE_ID!("mgs2")), //light_volume
    TAG_MOD2 = tagType(MAKE_ID!("mod2")), //gbxmodel
    TAG_MODE = tagType(MAKE_ID!("mode")), //model
    TAG_MPLY = tagType(MAKE_ID!("mply")), //multiplayer_scenario_description
    TAG_NGPR = tagType(MAKE_ID!("ngpr")), //preferences_network_game
    TAG_OBJE = tagType(MAKE_ID!("obje")), //object
    TAG_PART = tagType(MAKE_ID!("part")), //particle
    TAG_PCTL = tagType(MAKE_ID!("pctl")), //particle_system
    TAG_PHYS = tagType(MAKE_ID!("phys")), //physics
    TAG_PLAC = tagType(MAKE_ID!("plac")), //placeholder
    TAG_PPHY = tagType(MAKE_ID!("pphy")), //point_physics
    TAG_PROJ = tagType(MAKE_ID!("proj")), //projectile
    TAG_RAIN = tagType(MAKE_ID!("rain")), //weather_particle_system
    TAG_SBSP = tagType(MAKE_ID!("sbsp")), //scenario_structure_bsp
    TAG_SCEN = tagType(MAKE_ID!("scen")), //scenery
    TAG_SCEX = tagType(MAKE_ID!("scex")), //shader_transparent_chicago_extended
    TAG_SCHI = tagType(MAKE_ID!("schi")), //shader_transparent_chicago
    TAG_SCNR = tagType(MAKE_ID!("scnr")), //scenario
    TAG_SENV = tagType(MAKE_ID!("senv")), //shader_enviroment
    TAG_SGLA = tagType(MAKE_ID!("sgla")), //shader_transparent_glass
    TAG_SHDR = tagType(MAKE_ID!("shdr")), //shader
    TAG_SKY_ = tagType(MAKE_ID!("sky ")), //sky
    TAG_SMET = tagType(MAKE_ID!("smet")), //shader_transparent_meter
    TAG_SND_ = tagType(MAKE_ID!("snd!")), //sound
    TAG_SNDE = tagType(MAKE_ID!("snde")), //sound_enviornment
    TAG_SOSO = tagType(MAKE_ID!("soso")), //shader_model
    TAG_SOTR = tagType(MAKE_ID!("sotr")), //shader_transparent_generic
    TAG_SOUL = tagType(MAKE_ID!("Soul")), //ui_widget_collection
    TAG_SPLA = tagType(MAKE_ID!("spla")), //shader_transparent_plasma
    TAG_SSCE = tagType(MAKE_ID!("ssce")), //sound_scenery
    TAG_STR_ = tagType(MAKE_ID!("str#")), //string_list
    TAG_SWAT = tagType(MAKE_ID!("swat")), //shader_transparent_water
    TAG_TAGC = tagType(MAKE_ID!("tagc")), //tag_collection
    TAG_TRAK = tagType(MAKE_ID!("trak")), //camera_track
    TAG_UDLG = tagType(MAKE_ID!("udlg")), //dialogue
    TAG_UNHI = tagType(MAKE_ID!("unhi")), //unit_hud_interface
    TAG_UNIT = tagType(MAKE_ID!("unit")), //unit
    TAG_USTR = tagType(MAKE_ID!("ustr")), //unicode_string_list
    TAG_VEHI = tagType(MAKE_ID!("vehi")), //vehicle
    TAG_VCKY = tagType(MAKE_ID!("vcky")), //virtual_keyboard
    TAG_WEAP = tagType(MAKE_ID!("weap")), //weapon
    TAG_WIND = tagType(MAKE_ID!("wind")), //wind
    TAG_WPHI = tagType(MAKE_ID!("wphi"))  //weapon_hud_interface
};