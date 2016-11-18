//APPROVED

typedef enum e_render_type : unsigned short {
    RENDER_TYPE_VERTICAL_ORIENTATION = 0,
    RENDER_TYPE_HORIZONTAL_ORIENTATION,
    RENDER_TYPE_MEDIA_MAPPED,
    RENDER_TYPE_GROUND_MAPPED,
    RENDER_TYPE_VIEWER_FACING,
    RENDER_TYPE_DOUBLE_MARKER_LINKED
} e_render_type;

typedef enum e_blend_function : unsigned short {
    BLEND_FUNC_ALPHA_BLEND=0,
    BLEND_FUNC_MULTIPLY,
    BLEND_FUNC_DOUBLE_MULTIPLY,
    BLEND_FUNC_ADD,
    BLEND_FUNC_SUBTRACT,
    BLEND_FUNC_COMPONENT_MIN,
    BLEND_FUNC_COMPONENT_MAX,
    BLEND_FUNC_ALPHA_MULTIPLY_ADD
} e_blend_function;

typedef enum e_fade_mode : unsigned short {
    FADE_NONE=0,
    FADE_WHEN_PERPENDICULAR,
    FADE_WHEN_PARALLEL
} e_fade_mode;

typedef enum e_secondary_map_anchor : unsigned short {
    ANCHOR_WITH_PRIMARY=0,
    ANCHOR_WITH_SCREEN_SPACE,
    ANCHOR_ZSPRITE
} e_secondary_map_anchor;
