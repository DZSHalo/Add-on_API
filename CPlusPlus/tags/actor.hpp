//APPROVED

typedef enum e_actor_type : unsigned short {
    ACTOR_ELITE = 0,
    ACTOR_JACKAL,
    ACTOR_GRUNT,
    ACTOR_HUNTER,
    ACTOR_ENGINEER,
    ACTOR_ASSASSIN,
    ACTOR_PLAYER,
    ACTOR_MARINE,
    ACTOR_CREW,
    ACTOR_COMBAT_FORM,
    ACTOR_INFECTION_FORM,
    ACTOR_CARRIER_FORM,
    ACTOR_MONITOR,
    ACTOR_SENTINEL,
    ACTOR_NONE,
    ACTOR_MOUNTED_WEAPON
} e_actor_type;

typedef enum e_danger_trigger : unsigned short {
    TRIGGER_NEVER = 0,
    TRIGGER_SHOOTING,
    TRIGGER_SHOOTING_NEAR_US,
    TRIGGER_DAMAGIN_US,
    TRIGGER_UNUSED0,
    TRIGGER_UNUSED1,
    TRIGGER_UNUSED2,
    TRIGGER_UNUSED3,
    TRIGGER_UNUSED4
} e_danger_trigger;

typedef enum e_crouch_type : unsigned short {
    CROUCH_NEVER = 0,
    CROUCH_DANGER,
    CROUCH_LOW_SHIELDS,
    CROUCH_HIGH_BEHIND_SHIELD,
    CROUCH_ANY_TARGET,
    CROUCH_FLOOD_SHAMBLE
} e_crouch_type;