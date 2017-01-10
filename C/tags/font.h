//INCOMPLETE - 2 things are missing from Guerilla
typedef struct s_font_character_tables {
    s_tag_block     character_table; //Max 256
} s_font_character_tables;

typedef struct s_font_character_table {
    signed short     character_index;
} s_font_character_table;

typedef struct s_font_character {
    signed short    character;
    signed short    character_width;
    short           bitmap_width;
    short           bitmap_height;
    signed short    bitmap_origin_x;
    signed short    bitmap_origin_y;
    signed short    hardware_character_index; //Unknown location for this variable...
    PADDING(0x02);
    unsigned int    pixels_offset;
} s_font_character;
static_assert_check(sizeof(s_font_character) == 0x14, "Incorrect size of s_font_character");

typedef struct s_font_meta {
    unsigned int    flags;
    signed short    ascending_height;
    signed short    decending_height;
    signed short    leading_height;
    signed short    leading_width; //easter egg (missing a g)
    PADDING(0x24);
    s_tag_block     character_tables; //Max 256
    s_tag_reference bold;
    s_tag_reference italic;
    s_tag_reference condense;
    s_tag_reference underline;
    s_tag_block     characters;
    unsigned int    pixels_bytes;
    PADDING(0x04);
    UNKNOWN(0x0C);
} s_font_meta;
static_assert_check(sizeof(s_font_meta) == 0x9C, "Incorrect size of s_font_meta");