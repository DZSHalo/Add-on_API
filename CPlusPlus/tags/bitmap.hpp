//INCOMPLETE - 2 data parts are missing from Guerilla
//Total 2 unknown values and appears to possiblity have another s_tag_reference involvement.
typedef enum e_bitmap_type : unsigned short {
    BITMAP_TYPE_2D_TEXTURES = 0,
    BITMAP_TYPE_3D_TEXTURES,
    BITMAP_TYPE_CUBE_MAPS,
    BITMAP_TYPE_SPRITES,
    BITMAP_TYPE_INTERFACE_BITMAPS
} e_bitmap_type;
typedef enum e_bitmap_format : unsigned short {
    BITMAP_FORMAT_COMPRESSED_WITH_COLOR_KEY_TRANSPARENCY = 0,
    BITMAP_FORMAT_COMPRESSED_WITH_EXPLICIT_ALPHA,
    BITMAP_FORMAT_COMPRESSED_WITH_INTERPOLATED_ALPHA,
    BITMAP_FORMAT_16_BIT_COLOR,
    BITMAP_FORMAT_32_BIT_COLOR,
    BITMAP_FORMAT_MONOCHROME,
} e_bitmap_format;
typedef enum e_bitmap_usage : unsigned short {
    BITMAP_USAGE_ALPHA_BLEND = 0,
    BITMAP_USAGE_DEFAULT,
    BITMAP_USAGE_HEIGHT_MAP,
    BITMAP_USAGE_DETAIL_MAP,
    BITMAP_USAGE_LIGHT_MAP,
    BITMAP_USAGE_VECTOR_MAP
} e_bitmap_usage;
typedef enum e_sprite_size : unsigned short {
    SPRITE_32x32 = 0,
    SPRITE_64x64,
    SPRITE_128x128,
    SPRITE_256x256,
    SPRITE_512x512
} e_sprite_size;
typedef enum e_sprite_usage : unsigned short {
    SPRITE_USAGE_BLEND_ADD_SUBTRACT_MULTIPLY_MAX = 0,
    BITMAP_USAGE_MULTIPLY_MIN,
    BITMAP_USAGE_DOUBLE_MULTIPLY,
} e_sprite_usage;

typedef enum e_bitmaps_type : unsigned short {
    BITMAPS_TYPE_2D_TEXTURE = 0,
    BITMAPS_TYPE_3D_TEXTURE,
    BITMAPS_TYPE_CUBE_MAP,
    BITMAPS_TYPE_WHITE
} e_bitmaps_type;
typedef enum e_bitmaps_format : unsigned short {
    BITMAPS_FORMAT_A8 = 0,
    BITMAPS_FORMAT_Y8,
    BITMAPS_FORMAT_AY8,
    BITMAPS_FORMAT_A8Y8,
    BITMAPS_FORMAT_UNUSED1,
    BITMAPS_FORMAT_UNUSED2,
    BITMAPS_FORMAT_R5G6B5,
    BITMAPS_FORMAT_UNUSED3,
    BITMAPS_FORMAT_A1R5G5B5,
    BITMAPS_FORMAT_A4R4G4B4,
    BITMAPS_FORMAT_X8R8G8B8,
    BITMAPS_FORMAT_A8R8G8B8,
    BITMAPS_FORMAT_UNUSED4,
    BITMAPS_FORMAT_UNUSED5,
    BITMAPS_FORMAT_DXT1,
    BITMAPS_FORMAT_DXT3,
    BITMAPS_FORMAT_DXT5,
    BITMAPS_FORMAT_P8_BUMP
} e_bitmaps_format;