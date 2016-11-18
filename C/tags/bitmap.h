//INCOMPLETE - 2 data parts are missing from Guerilla
//Total 2 unknown values and appears to possiblity have another s_tag_reference involvement.
#ifndef __cplusplus
typedef unsigned short e_bitmap_type;
static const e_bitmap_type BITMAP_TYPE_2D_TEXTURES = 0;
static const e_bitmap_type BITMAP_TYPE_3D_TEXTURES = 1;
static const e_bitmap_type BITMAP_TYPE_CUBE_MAPS = 2;
static const e_bitmap_type BITMAP_TYPE_SPRITES = 3;
static const e_bitmap_type BITMAP_TYPE_INTERFACE_BITMAPS = 4;
typedef unsigned short e_bitmap_format;
static const e_bitmap_format BITMAP_FORMAT_COMPRESSED_WITH_COLOR_KEY_TRANSPARENCY = 0;
static const e_bitmap_format BITMAP_FORMAT_COMPRESSED_WITH_EXPLICIT_ALPHA = 1;
static const e_bitmap_format BITMAP_FORMAT_COMPRESSED_WITH_INTERPOLATED_ALPHA = 2;
static const e_bitmap_format BITMAP_FORMAT_16_BIT_COLOR = 3;
static const e_bitmap_format BITMAP_FORMAT_32_BIT_COLOR = 4;
static const e_bitmap_format BITMAP_FORMAT_MONOCHROME = 5;
typedef unsigned short e_bitmap_usage;
static const e_bitmap_usage BITMAP_USAGE_ALPHA_BLEND = 0;
static const e_bitmap_usage BITMAP_USAGE_DEFAULT = 1;
static const e_bitmap_usage BITMAP_USAGE_HEIGHT_MAP = 2;
static const e_bitmap_usage BITMAP_USAGE_DETAIL_MAP = 3;
static const e_bitmap_usage BITMAP_USAGE_LIGHT_MAP = 4;
static const e_bitmap_usage BITMAP_USAGE_VECTOR_MAP = 5;
#endif
typedef struct e_bitmap_flags {
    bool    enable_diffusion_dithering : 1;
    bool    disable_height_map_compression : 1;
    bool    uniform_sprite_sequences : 1;
    bool    filthy_sprite_bug_fix : 1;
    bool    unused : 4;
    PADDING(1);
} e_bitmap_flags;
static_assert_check(sizeof(e_bitmap_flags) == 0x02, "Incorrect size of e_bitmap_flags");

#ifndef __cplusplus
typedef unsigned short e_sprite_size;
static const e_sprite_size SPRITE_32x32 = 0;
static const e_sprite_size SPRITE_64x64 = 1;
static const e_sprite_size SPRITE_128x128 = 2;
static const e_sprite_size SPRITE_256x256 = 3;
static const e_sprite_size SPRITE_512x512 = 4;
typedef unsigned short e_sprite_usage;
static const e_sprite_usage SPRITE_USAGE_BLEND_ADD_SUBTRACT_MULTIPLY_MAX = 0;
static const e_sprite_usage SPRITE_USAGE_MULTIPLY_MIN = 1;
static const e_sprite_usage SPRITE_USAGE_DOUBLE_MULTIPLY = 2;

typedef unsigned short e_bitmaps_type;
static const e_bitmaps_type BITMAPS_TYPE_2D_TEXTURE = 0;
static const e_bitmaps_type BITMAPS_TYPE_3D_TEXTURE = 1;
static const e_bitmaps_type BITMAPS_TYPE_CUBE_MAP = 2;
static const e_bitmaps_type BITMAPS_TYPE_WHITE = 3;
typedef unsigned short e_bitmaps_format;
static const e_bitmaps_format BITMAPS_FORMAT_A8 = 0;
static const e_bitmaps_format BITMAPS_FORMAT_Y8 = 1;
static const e_bitmaps_format BITMAPS_FORMAT_AY8 = 2;
static const e_bitmaps_format BITMAPS_FORMAT_A8Y8 = 3;
static const e_bitmaps_format BITMAPS_FORMAT_UNUSED1 = 4;
static const e_bitmaps_format BITMAPS_FORMAT_UNUSED2 = 5;
static const e_bitmaps_format BITMAPS_FORMAT_R5G6B5 = 6;
static const e_bitmaps_format BITMAPS_FORMAT_UNUSED3 = 7;
static const e_bitmaps_format BITMAPS_FORMAT_A1R5G5B5 = 8;
static const e_bitmaps_format BITMAPS_FORMAT_A4R4G4B4 = 9;
static const e_bitmaps_format BITMAPS_FORMAT_X8R8G8B8 = 10;
static const e_bitmaps_format BITMAPS_FORMAT_A8R8G8B8 = 11;
static const e_bitmaps_format BITMAPS_FORMAT_UNUSED4 = 12;
static const e_bitmaps_format BITMAPS_FORMAT_UNUSED5 = 13;
static const e_bitmaps_format BITMAPS_FORMAT_DXT1 = 14;
static const e_bitmaps_format BITMAPS_FORMAT_DXT3 = 15;
static const e_bitmaps_format BITMAPS_FORMAT_DXT5 = 16;
static const e_bitmaps_format BITMAPS_FORMAT_P8_BUMP = 17;
#endif
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