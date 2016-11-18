//APPROVED except for s_damage_effect_flags

typedef enum e_screen_flash_type : unsigned short {
    SCREEN_FLASH_NONE = 0,
    SCREEN_FLASH_LIGHTEN,
    SCREEN_FLASH_DARKEN,
    SCREEN_FLASH_MAX,
    SCREEN_FLASH_MIN,
    SCREEN_FLASH_INVERT,
    SCREEN_FLASH_TINT
} e_screen_flash_type;

typedef enum e_priority : unsigned short {
    PRIORITY_LOW=0,
    PRIORITY_MEDIUM,
    PRIORITY_HIGH
} e_priority;

typedef enum e_fade_function : unsigned short {
    FADE_FUNCTION_LINEAR = 0,
    FADE_FUNCTION_EARLY,
    FADE_FUNCTION_VERY_EARLY,
    FADE_FUNCTION_LATE,
    FADE_FUNCTION_VERY_LATE,
    FADE_FUNCTION_COSINE
} e_fade_function;
