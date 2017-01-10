//APPROVED
typedef struct s_color_block {
    char    name[0x20];
    real_color_alpha color;
} s_color_block;
static_assert_check(sizeof(s_color_block) == 0x30, "Incorrect size of s_color_block");

typedef struct s_color_table_meta {
    s_tag_block colors;
} s_color_table_meta;
static_assert_check(sizeof(s_color_table_meta) == 0x0C, "Incorrect size of s_color_table_meta");
