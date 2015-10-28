


enum e_attachment_out : unsigned short {
    ATTACHMENT_NONE_OUT = 0,
    ATTACHMENT_A_OUT = 1,
    ATTACHMENT_B_OUT = 2,
    ATTACHMENT_C_OUT = 3,
    ATTACHMENT_D_OUT = 4
};
enum e_attachment_in : unsigned short {
    ATTACHMENT_NONE_IN = 0,
    ATTACHMENT_A_IN = 1,
    ATTACHMENT_B_IN = 2,
    ATTACHMENT_C_IN = 3,
    ATTACHMENT_D_IN = 4
};
enum e_attachment_in_out : unsigned short {
    ATTACHMENT_NONE = 0,
    ATTACHMENT_A_IN_ = 1,
    ATTACHMENT_B_IN_ = 2,
    ATTACHMENT_C_IN_ = 3,
    ATTACHMENT_D_IN_ = 4,
    ATTACHMENT_A_OUT_ = 5,
    ATTACHMENT_B_OUT_ = 6,
    ATTACHMENT_C_OUT_ = 7,
    ATTACHMENT_D_OUT_ = 8
};

enum e_function :unsigned short {
    WOBBLE_FUNCTION_ONE = 0,
    WOBBLE_FUNCTION_ZERO,
    WOBBLE_FUNCTION_COSINE,
    WOBBLE_FUNCTION_COSINE_VARIABLE_PERIOD,
    WOBBLE_FUNCTION_DIAGONAL_WAVE,
    WOBBLE_FUNCTION_DIAGONAL_WAVE_VARIABLE_PERIOD,
    WOBBLE_FUNCTION_SLIDE,
    WOBBLE_FUNCTION_SLIDE_VARIABLE_PERIOD,
    WOBBLE_FUNCTION_NOISE,
    WOBBLE_FUNCTION_JITTER,
    WOBBLE_FUNCTION_WANDER,
    WOBBLE_FUNCTION_SPARK
};

//http://stackoverflow.com/questions/24193389/how-to-use-anonymous-unions-with-enums
enum e_tag_group : unsigned int {
    TAG_ACTR = MAKE_ID('actr'), //actor
    TAG_ACTV = MAKE_ID('actv'), //actor_variant
    TAG_ANT_ = MAKE_ID('ant!'),  //antenna
    TAG_ANTR = MAKE_ID('antr'), //model_animations
    TAG_BIPD = MAKE_ID('bipd'), //biped
    TAG_BTIM = MAKE_ID('bitm'), //bitmap
    TAG_BOOM = MAKE_ID('boom'), //spheroid
    TAG_CDMG = MAKE_ID('cdmg'), //continuous_damage_effect
    TAG_COLL = MAKE_ID('coll'), //model_collision_geometry
    TAG_COLO = MAKE_ID('colo'), //color_table
    TAG_CONT = MAKE_ID('cont'), //contrail
    TAG_CTRL = MAKE_ID('ctrl'), //device_control
    TAG_DECA = MAKE_ID('deca'), //decal
    TAG_DELA = MAKE_ID('DeLa'), //ui_widget_definition
    TAG_DEVC = MAKE_ID('devc'), //input_device_defaults
    TAG_DEVI = MAKE_ID('devi'), //device
    TAG_DOBC = MAKE_ID('dobc'), //detail_object_collection
    TAG_EFFE = MAKE_ID('effe'), //effect
    TAG_ELEC = MAKE_ID('elec'), //lighting
    TAG_EQIP = MAKE_ID('eqip'), //equipment
    TAG_FLAG = MAKE_ID('flag'), //flag
    TAG_FOG_ = MAKE_ID('fog '), //fog
    TAG_FONT = MAKE_ID('font'), //font
    TAG_FOOT = MAKE_ID('foot'), //material_effects
    TAG_GARB = MAKE_ID('garb'), //garbage
    TAG_GLW_ = MAKE_ID('glw!'), //glow
    TAG_GRHI = MAKE_ID('grhi'), //grenade_hud_interface
    TAG_HMT_ = MAKE_ID('hmt '), //hud_message_text
    TAG_HUD_ = MAKE_ID('hud#'), //hud_number
    TAG_HUDG = MAKE_ID('hudg'), //hud_globals
    TAG_ITEM = MAKE_ID('item'), //item
    TAG_ITMC = MAKE_ID('itmc'), //item_collection
    TAG_JPT_ = MAKE_ID('jpt!'), //damage_effect
    TAG_LENS = MAKE_ID('lens'), //lens_flare
    TAG_LIFI = MAKE_ID('lifi'), //device_light_fixture
    TAG_LIGH = MAKE_ID('ligh'), //light
    TAG_LSND = MAKE_ID('lsnd'), //sound_looping
    TAG_MACH = MAKE_ID('mach'), //device_machine
    TAG_MATG = MAKE_ID('matg'), //globals
    TAG_METR = MAKE_ID('metr'), //meter
    TAG_MGS2 = MAKE_ID('mgs2'), //light_volume
    TAG_MOD2 = MAKE_ID('mod2'), //gbxmodel
    TAG_MODE = MAKE_ID('mode'), //model
    TAG_MPLY = MAKE_ID('mply'), //multiplayer_scenario_description
    TAG_NGPR = MAKE_ID('ngpr'), //preferences_network_game
    TAG_OBJE = MAKE_ID('obje'), //object
    TAG_PART = MAKE_ID('part'), //particle
    TAG_PCTL = MAKE_ID('pctl'), //particle_system
    TAG_PHYS = MAKE_ID('phys'), //physics
    TAG_PLAC = MAKE_ID('plac'), //placeholder
    TAG_PPHY = MAKE_ID('pphy'), //point_physics
    TAG_PROJ = MAKE_ID('proj'), //projectile
    TAG_RAIN = MAKE_ID('rain'), //weather_particle_system
    TAG_SBSP = MAKE_ID('sbsp'), //scenario_structure_bsp
    TAG_SCEN = MAKE_ID('scen'), //scenery
    TAG_SCEX = MAKE_ID('scex'), //shader_transparent_chicago_extended
    TAG_SCHI = MAKE_ID('schi'), //shader_transparent_chicago
    TAG_SCNR = MAKE_ID('scnr'), //scenario
    TAG_SENV = MAKE_ID('senv'), //shader_enviroment
    TAG_SGLA = MAKE_ID('sgla'), //shader_transparent_glass
    TAG_SHDR = MAKE_ID('shdr'), //shader
    TAG_SKY_ = MAKE_ID('sky '), //sky
    TAG_SMET = MAKE_ID('smet'), //shader_transparent_meter
    TAG_SND_ = MAKE_ID('snd!'), //sound
    TAG_SNDE = MAKE_ID('snde'), //sound_enviornment
    TAG_SOSO = MAKE_ID('soso'), //shader_model
    TAG_SOTR = MAKE_ID('sotr'), //shader_transparent_generic
    TAG_SOUL = MAKE_ID('Soul'), //ui_widget_collection
    TAG_SPLA = MAKE_ID('spla'), //shader_transparent_plasma
    TAG_SSCE = MAKE_ID('ssce'), //sound_scenery
    TAG_STR_ = MAKE_ID('str#'), //string_list
    TAG_SWAT = MAKE_ID('swat'), //shader_transparent_water
    TAG_TAGC = MAKE_ID('tagc'), //tag_collection
    TAG_TRAK = MAKE_ID('trak'), //camera_track
    TAG_UDLG = MAKE_ID('udlg'), //dialogue
    TAG_UNHI = MAKE_ID('unhi'), //unit_hud_interface
    TAG_UNIT = MAKE_ID('unit'), //unit
    TAG_USTR = MAKE_ID('ustr'), //unicode_string_list
    TAG_VEHI = MAKE_ID('vehi'), //vehicle
    TAG_VCKY = MAKE_ID('vcky'), //virtual_keyboard
    TAG_WEAP = MAKE_ID('weap'), //weapon
    TAG_WIND = MAKE_ID('wind'), //wind
    TAG_WPHI = MAKE_ID('wphi')  //weapon_hud_interface
};