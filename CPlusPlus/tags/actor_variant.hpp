//APPROVED
typedef enum e_movement_type : unsigned short {
    MOVEMENT_ALWAYS_RUN=0,
    MOVEMENT_ALWAYS_CROUCH,
    MOVEMENT_SWITCH_TYPES
} e_movement_type;

typedef enum e_special_fire_mode : unsigned short {
    SPECIAL_FIRE_MODE_NONE=0,
    SPECIAL_FIRE_MODE_OVERCHARGE,
    SPECIAL_FIRE_MODE_SECONDARY_TRIGGER
} e_special_fire_mode;

typedef enum e_special_fire_situation : unsigned short {
    SPECIAL_FIRE_SITUATION_NEVER=0,
    SPECIAL_FIRE_SITUATION_ENEMY_VISIBLE,
    SPECIAL_FIRE_SITUATION_STRAFING
} e_special_fire_situation;

typedef enum e_grenade_type : unsigned short {
    GRENADE_TYPE_HUMAN_FRAGMENTATION=0,
    GRENADE_TYPE_COVENANT_PLASMA
} e_grenade_type;

typedef enum e_trajectory_type : unsigned short {
    TRAJECTORY_TYPE_TOSS=0,
    TRAJECTORY_TYPE_LOB,
    TRAJECTORY_TYPE_BOUNCE
} e_trajectory_type;

typedef enum e_grenade_stimulus : unsigned short {
    GRENADE_STIMULUS_NEVER=0,
    GRENADE_STIMULUS_VISIBLE_TARGET,
    GRENADE_STIMULUS_SEEK_COVER
} e_grenade_stimulus;
