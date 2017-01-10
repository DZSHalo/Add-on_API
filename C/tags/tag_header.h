#pragma once

typedef struct s_tag_header {
    PADDING(0x24);
    e_tag_group     tag_type;
    unsigned int    random_number1;
    unsigned int    header_size;
    PADDING(0x08);
    unsigned short  tag_version;
    short           engine_version;
    unsigned int    engine_name;
} s_tag_header;
static_assert_check(sizeof(s_tag_header) == 0x40, "Incorrect size of s_tag_header");

typedef struct s_tag_reference {
    e_tag_group group_tag;
    tag_name_reference name;
    tag_name_length name_length; //Excluding null terminate (Is not in used ingame)
    s_ident tag_index; //Always -1 in Guerilla, is used ingame.
} s_tag_reference;
static_assert_check(sizeof(s_tag_reference) == 0x10, "Incorrect size of s_tag_reference");

typedef struct s_tag_block_definition {
    //TODO: Need to research again since didn't investigate this for long time.
    unsigned int noResearchDone; //TODO: No research done
} s_tag_block_definition;

typedef struct s_tag_block {
    int    count;
    void*   address;
    const   s_tag_block_definition* definition;
} s_tag_block;
//typedef struct s_tag_group {
    //TODO: Is this needed?
//} s_tag_group;
