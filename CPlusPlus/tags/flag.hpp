//APPROVED
typedef enum e_trailing_edge_shape : unsigned short {
    TRAILING_EDGE_SHAPE_FLAT=0,
    TRAILING_EDGE_SHAPE_CONCAVE_TRIANGULAR,
    TRAILING_EDGE_SHAPE_CONVEX_TRIANGULAR,
    TRAILING_EDGE_SHAPE_TRAPEZOID_SHORT_TOP,
    TRAILING_EDGE_SHAPE_TRAPEZOID_SHORT_BOTTOM
} e_trailing_edge_shape;

typedef enum e_attached_edge_shape : unsigned short {
    ATTACHED_EDGE_SHAPE_FLAT=0,
    ATTACHED_EDGE_SHAPE_CONCAVE_TRIANGULAR
} e_attached_edge_shape;