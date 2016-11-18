//APPROVED
typedef enum e_boundary_effect : unsigned short {
    BOUNDARY_EFFECT_BOUNCE = 0,
    BOUNDARY_EFFECT_WRAP
} e_boundary_effect;
typedef enum e_normal_particle : unsigned short {
    DISTRIBUTED_RANDOMLY = 0,
    DISTRIBUTED_UNIFORMLY
} e_normal_particle;
typedef enum e_trailing_particle : unsigned short {
    EMIT_VERTICALLY = 0,
    EMIT_NORMAL_UP,
    EMIT_RANDOMLY 
} e_trailing_particle;
