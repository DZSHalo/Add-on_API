Imports System.Runtime.InteropServices

Public Enum e_source_out As UShort
    NONE = 0
    A = 1
    B = 2
    C = 3
    D = 4
End Enum
Public Enum e_source_in As UShort
    NONE = 0
    A = 1
    B = 2
    C = 3
    D = 4
End Enum
Public Enum e_source_in_out As UShort
    NONE = 0
    A_IN = 1
    B_IN = 2
    C_IN = 3
    D_IN = 4
    A_OUT = 5
    B_OUT = 6
    C_OUT = 7
    D_OUT = 8
End Enum

Public Enum e_function As UShort
    ONE = 0
    ZERO
    COSINE
    COSINE_VARIABLE_PERIOD
    DIAGONAL_WAVE
    DIAGONAL_WAVE_VARIABLE_PERIOD
    SLIDE
    SLIDE_VARIABLE_PERIOD
    NOISE
    JITTER
    WANDER
    SPARK
End Enum

Public Enum e_side_effect As UShort
    NONE = 0
    HARMLESS
    LETHAL_TO_THE_UNSUSPECTING
    EMP
End Enum

Public Enum e_category As UShort
    NONE = 0
    FALLING
    BULLET
    GRENADE
    HIGH_EXPLOSIVE
    SNIPER
    MELEE
    FLAME
    MOUNTED_WEAPON
    VEHICLE
    PLASMA
    NEEDLE
    SHOTGUN
End Enum

Public Enum e_tag_group As UInteger
    'TODO: Need testing for tags lookup to be sure this is correct or may need to be reversed.
    TAG_ACTR = &H61637472
    'actor
    TAG_ACTV = &H61637476
    'actor_variant
    TAG_ANT_ = &H616E7421
    'antenna
    TAG_ANTR = &H616E7472
    'model_animations
    TAG_BIPD = &H62697064
    'biped
    TAG_BTIM = &H6269746D
    'bitmap
    TAG_BOOM = &H626F6F6D
    'spheroid
    TAG_CDMG = &H63646D67
    'continuous_damage_effect
    TAG_COLL = &H636F6C6C
    'model_collision_geometry
    TAG_COLO = &H636F6C6F
    'color_table
    TAG_CONT = &H636F6E74
    'contrail
    TAG_CTRL = &H6374726C
    'device_control
    TAG_DECA = &H64656361
    'decal
    TAG_DELA = &H44654C61
    'ui_widget_definition
    TAG_DEVC = &H64657663
    'input_device_defaults
    TAG_DEVI = &H64657669
    'device
    TAG_DOBC = &H646F6263
    'detail_object_collection
    TAG_EFFE = &H65666665
    'effect
    TAG_ELEC = &H656C6563
    'lighting
    TAG_EQIP = &H65716970
    'equipment
    TAG_FLAG = &H666C6167
    'flag
    TAG_FOG_ = &H666F6720
    'fog
    TAG_FONT = &H666F6E74
    'font
    TAG_FOOT = &H666F6F74
    'material_effects
    TAG_GARB = &H67617262
    'garbage
    TAG_GLW_ = &H676C7721
    'glow
    TAG_GRHI = &H67726869
    'grenade_hud_interface
    TAG_HMT_ = &H686D7420
    'hud_message_text
    TAG_HUD_ = &H68756423
    'hud_number
    TAG_HUDG = &H68756467
    'hud_globals
    TAG_ITEM = &H6974656D
    'item
    TAG_ITMC = &H69746D63
    'item_collection
    TAG_JPT_ = &H6A707421
    'damage_effect
    TAG_LENS = &H6C656E73
    'lens_flare
    TAG_LIFI = &H6C696669
    'device_light_fixture
    TAG_LIGH = &H6C696768
    'light
    TAG_LSND = &H6C736E64
    'sound_looping
    TAG_MACH = &H6D616368
    'device_machine
    TAG_MATG = &H6D617467
    'globals
    TAG_METR = &H6D657472
    'meter
    TAG_MGS2 = &H6D677332
    'light_volume
    TAG_MOD2 = &H6D6F6432
    'gbxmodel
    TAG_MODE = &H6D6F6465
    'model
    TAG_MPLY = &H6D706C79
    'multiplayer_scenario_description
    TAG_NGPR = &H6E677072
    'preferences_network_game
    TAG_OBJE = &H6F626A65
    'object
    TAG_PART = &H70617274
    'particle
    TAG_PCTL = &H7063746C
    'particle_system
    TAG_PHYS = &H70687973
    'physics
    TAG_PLAC = &H706C6163
    'placeholder
    TAG_PPHY = &H70706879
    'point_physics
    TAG_PROJ = &H70726F6A
    'projectile
    TAG_RAIN = &H7261696E
    'weather_particle_system
    TAG_SBSP = &H73627370
    'scenario_structure_bsp
    TAG_SCEN = &H7363656E
    'scenery
    TAG_SCEX = &H73636578
    'shader_transparent_chicago_extended
    TAG_SCHI = &H73636869
    'shader_transparent_chicago
    TAG_SCNR = &H73636E72
    'scenario
    TAG_SENV = &H73656E76
    'shader_enviroment
    TAG_SGLA = &H73676C61
    'shader_transparent_glass
    TAG_SHDR = &H73686472
    'shader
    TAG_SKY_ = &H736B7920
    'sky
    TAG_SMET = &H736D6574
    'shader_transparent_meter
    TAG_SND_ = &H736E6421
    'sound
    TAG_SNDE = &H736E6465
    'sound_enviornment
    TAG_SOSO = &H736F736F
    'shader_model
    TAG_SOTR = &H736F7472
    'shader_transparent_generic
    TAG_SOUL = &H536F756C
    'ui_widget_collection
    TAG_SPLA = &H73706C61
    'shader_transparent_plasma
    TAG_SSCE = &H73736365
    'sound_scenery
    TAG_STR_ = &H73747223
    'string_list
    TAG_SWAT = &H73776174
    'shader_transparent_water
    TAG_TAGC = &H74616763
    'tag_collection
    TAG_TRAK = &H7472616B
    'camera_track
    TAG_UDLG = &H75646C67
    'dialogue
    TAG_UNHI = &H756E6869
    'unit_hud_interface
    TAG_UNIT = &H756E6974
    'unit
    TAG_USTR = &H75737472
    'unicode_string_list
    TAG_VEHI = &H76656869
    'vehicle
    TAG_VCKY = &H76636B79
    'virtual_keyboard
    TAG_WEAP = &H77656170
    'weapon
    TAG_WIND = &H77696E64
    'wind
    TAG_WPHI = &H77706869
    'weapon_hud_interface
End Enum
