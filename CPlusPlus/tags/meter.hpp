//INCOMPLETE - 1 data part is missing
typedef enum e_interpolate_color : unsigned short {
    INTERPOLATE_LINEARLY = 0,
    INTERPOLATE_FASTER_NEAR_EMPTY,
    INTERPOLATE_FASTER_NEAR_FULL,
    INTERPOLATE_THROUGH_RANDOM_NOISE
} e_interpolate_color;

typedef enum e_anchor_color : unsigned short {
    ANCHOR_AT_BOTH_ENDS = 0,
    ANCHOR_AT_EMPTY,
    ANCHOR_AT_FULL
} e_anchor_color;
