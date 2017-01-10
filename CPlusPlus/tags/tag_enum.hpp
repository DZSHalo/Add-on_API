typedef enum e_source_out : unsigned short {
    SOURCE_NONE_OUT = 0,
    SOURCE_A_OUT = 1,
    SOURCE_B_OUT = 2,
    SOURCE_C_OUT = 3,
    SOURCE_D_OUT = 4
} e_source_out;
typedef enum e_source_in : unsigned short {
    SOURCE_NONE_IN = 0,
    SOURCE_A_IN = 1,
    SOURCE_B_IN = 2,
    SOURCE_C_IN = 3,
    SOURCE_D_IN = 4
} e_source_in;
typedef enum e_source_in_out : unsigned short {
    SOURCE_NONE = 0,
    SOURCE_A_IN_ = 1,
    SOURCE_B_IN_ = 2,
    SOURCE_C_IN_ = 3,
    SOURCE_D_IN_ = 4,
    SOURCE_A_OUT_ = 5,
    SOURCE_B_OUT_ = 6,
    SOURCE_C_OUT_ = 7,
    SOURCE_D_OUT_ = 8
} e_source_in_out;

typedef enum e_function : unsigned short {
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
} e_function;

typedef enum e_side_effect : unsigned short {
    SIDE_EFFECT_NONE = 0,
    SIDE_EFFECT_HARMLESS,
    SIDE_EFFECT_LETHAL_TO_THE_UNSUSPECTING,
    SIDE_EFFECT_EMP
} e_side_effect;

typedef enum e_category : unsigned short {
    CATEGORY_NONE = 0,
    CATEGORY_FALLING,
    CATEGORY_BULLET,
    CATEGORY_GRENADE,
    CATEGORY_HIGH_EXPLOSIVE,
    CATEGORY_SNIPER,
    CATEGORY_MELEE,
    CATEGORY_FLAME,
    CATEGORY_MOUNTED_WEAPON,
    CATEGORY_VEHICLE,
    CATEGORY_PLASMA,
    CATEGORY_NEEDLE,
    CATEGORY_SHOTGUN
} e_category;
