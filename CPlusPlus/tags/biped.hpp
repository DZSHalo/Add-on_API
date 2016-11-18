//APPROVED EXCEPT FOR e_unknown_enum and s_biped__flags
typedef enum e_export_in : unsigned short {
    EXPORT_IN_NONE=0,
    EXPORT_IN_BODY_VITALITY,
    EXPORT_IN_SHIELD_VITALITY,
    EXPORT_IN_RECENT_BODY_DAMAGE,
    EXPORT_IN_RECENT_SHIELD_DAMAGE,
    EXPORT_IN_RANDOM_CONSTANT,
    EXPORT_IN_UMBRELLA_SHIELD_VITALITY,
    EXPORT_IN_SHIELD_STUN,
    EXPORT_IN_RECENT_UMBRELLA_SHIELD_VITALITY,
    EXPORT_IN_UMBRELLA_SHIELD_STUN,
    EXPORT_IN_REGION_00_DAMAGE,
    EXPORT_IN_REGION_01_DAMAGE,
    EXPORT_IN_REGION_02_DAMAGE,
    EXPORT_IN_REGION_03_DAMAGE,
    EXPORT_IN_REGION_04_DAMAGE,
    EXPORT_IN_REGION_05_DAMAGE,
    EXPORT_IN_REGION_06_DAMAGE,
    EXPORT_IN_REGION_07_DAMAGE,
    EXPORT_IN_ALIVE,
    EXPORT_IN_COMPASS
} e_export_in;

typedef enum e_map_to :unsigned short {
    MAP_TO_LINEAR=0,
    MAP_TO_EARLY,
    MAP_TO_VERY_EARLY,
    MAP_TO_LATE,
    MAP_TO_VERY_LATE,
    MAP_TO_CONSINE
} e_map_to;

typedef enum e_bound_mode : unsigned short {
    BOUND_MODE_CLIP=0,
    BOUND_MODE_CLIP_AND_NORMALIZE,
    BOUND_MODE_SCALE_TO_FIT,
} e_bound_mode;

typedef enum e_predicted_resource_type :unsigned short {
    PREDICTED_RESOURCE_TYPE_BITMAP=0,
    PREDICTED_RESOURCE_TYPE_SOUND
} e_predicted_resource_type;

typedef enum e_default_team : unsigned short {
    DEFAULT_TEAM_NONE=0,
    DEFAULT_TEAM_PLAYER,
    DEFAULT_TEAM_HUMAN,
    DEFAULT_TEAM_COVENANT,
    DEFAULT_TEAM_FLOOD,
    DEFAULT_TEAM_SENTINEL,
    DEFAULT_TEAM_UNUSED6,
    DEFAULT_TEAM_UNUSED7,
    DEFAULT_TEAM_UNUSED8,
    DEFAULT_TEAM_UNUSED9
} e_default_team;

typedef enum e_sound_volume : unsigned short {
    SOUND_VOLUME_SILENT=0,
    SOUND_VOLUME_MEDIUM,
    SOUND_VOLUME_LOUD,
    SOUND_VOLUME_SHOUT,
    SOUND_VOLUME_QUIET
} e_sound_volume;

typedef enum e_unit_action : unsigned short {
    UNIT_ACTION_NONE=0,
    UNIT_ACTION_DRIVER_SEAT_POWER,
    UNIT_ACTION_GUNNER_SEAT_POWER,
    UNIT_ACTION_AIMING_CHANGE,
    UNIT_ACTION_MOUTH_APERTURE,
    UNIT_ACTION_INTEGRATED_LIGHT_POWER,
    UNIT_ACTION_CAN_BLINK,
    UNIT_ACTION_SHIELD_SAPPING
} e_unit_action;

typedef enum e_motion_sensor_blip_size : unsigned short {
    BLIP_SIZE_MEDIUM=0,
    BLIP_SIZE_SMALL,
    BLIP_SIZE_LARGE,
} e_motion_sensor_blip_size;

typedef enum e_unknown_enum : unsigned short {
    UNKNOWN_ENUM_NONE=0,
    UNKNOWN_ENUM_FLYING_VELOCITY
} e_unknown_enum;
