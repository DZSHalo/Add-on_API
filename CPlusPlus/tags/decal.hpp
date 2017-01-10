//APPROVED

typedef enum e_decal_type : unsigned short {
    DECAL_TYPE_SCRATCH=0,
    DECAL_TYPE_SPLATTER,
    DECAL_TYPE_BURN,
    DECAL_TYPE_PAINTED_SIGN
} e_decal_type;

typedef enum e_decal_layer : unsigned short {
    DECAL_LAYER_PRIMARY=0,
    DECAL_LAYER_SECONDARY,
    DECAL_LAYER_LIGHT,
    DECAL_LAYER_ALPHA_TESTED,
    DECAL_LAYER_WATER
} e_decal_layer;
