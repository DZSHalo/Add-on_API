//INCOMPLETE - Unknown where pointer of string data at in Guerilla (Need verify in-game)
/* How it stored in order list below.
 * s_string_list_meta
 * s_string_reference list
 * strings list
 */
typedef struct s_string_reference {
    unsigned int    string_bytes; //Max 4096
    UNKNOWN(0x10);
} s_string_reference;
static_assert_check(sizeof(s_string_reference) == 0x14, "Incorrect size of s_string_reference");

typedef struct s_string_list_meta {
    s_tag_block string_references;
} s_string_list_meta;
static_assert_check(sizeof(s_string_list_meta) == 0x0C, "Incorrect size of s_string_list_meta");