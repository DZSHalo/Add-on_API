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
typedef struct e_bitmap_flags {
    bool enable_diffusion_dithering : 1;
    bool disable_height_map_compression : 1;
    bool uniform_sprite_sequences : 1;
    bool filthy_sprite_bug_fix : 1;
    bool unused : 4;
    PADDING(1);
} e_bitmap_flags;
static_assert_check(sizeof(e_bitmap_flags) == 0x02, "Incorrect size of e_bitmap_flags");

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

typedef struct s_bitmaps_flags {
    bool    power_of_two_dimensions : 1;
    bool    compressed : 1;
    bool    palettized : 1;
    bool    swizzled : 1;
    bool    linear : 1;
    bool    v16u16 : 1;
    bool    unused : 2;
} s_bitmaps_flags;

typedef struct s_bitmap_group_sequence_block {
    char            name[0x20];
    unsigned short  first_bitmap_index;
    unsigned short  bitmap_count;
    s_tag_block     sprites;
} s_bitmap_group_sequence_block;
static_assert_check(sizeof(s_bitmap_group_sequence_block) == 0x30, "Incorrect size of s_bitmap_group_sequence_block");

typedef struct s_bitmap_group_sprite_block {
    unsigned short  bitmap_index;
    PADDING(0x06);
    real            left;
    real            right;
    real            top;
    real            bottom;
    real_vector2d   registration_point;
} s_bitmap_group_sprite_block;
static_assert_check(sizeof(s_bitmap_group_sprite_block) == 0x20, "Incorrect size of s_bitmap_group_sprite_block");

typedef struct s_bitmap_data_block {
    e_tag_group         signature;
    unsigned short      width;
    unsigned short      height;
    unsigned short      depth;
    e_bitmaps_type      type;
    e_bitmaps_format    format;
    short_point         registration_point;
    unsigned short      mipmap_count;
    PADDING(0x02);
    unsigned int        pixels_offset;
    UNKNOWN(0x04);
    s_tag_reference     tag_reference;
} s_bitmap_data_block;
static_assert_check(sizeof(s_bitmap_data_block) == 0x30, "Incorrect size of s_bitmap_data_block");


typedef struct s_bitmap_meta {
    /* type
     * Contains description
     */
    e_bitmap_type   type;
    /* format
    * Contains description
    */
    e_bitmap_format format;
    /* usage
    * Contains description
    */
    e_bitmap_usage  usage;
    //post-processing
    real            detail_fade_factor;
    real            sharpen_amount;
    real            bump_height_repeats;
    //sprite processing
    e_sprite_size sprite_budget_size;
    unsigned short  sprite_budget_count;
    // color plate
    unsigned short  color_plate_width_pixels;
    unsigned short  color_plate_height_pixels;
    unsigned int    compressed_color_plate_data_bytes;
    UNKNOWN(0x10);  //Unknown values
    //processed pixel data
    unsigned int    processed_pixel_data_bytes;
    UNKNOWN(0x10);  //Unknown values
    //miscellaneous
    real            blur_filter_size_pixels;
    real            alpha_bias;
    short           levels;
    //...more sprite processing
    e_sprite_usage  sprite_usage;
    short           sprite_spacing;
    PADDING(0x02);
    s_tag_block     sequences;  //s_bitmap_group_sequence_block
    s_tag_block     bitmaps;    //s_bitmap_data_block
} s_bitmap_meta;
static_assert_check(sizeof(s_bitmap_meta) == 0x6C, "Incorrect size of s_bitmap_meta");