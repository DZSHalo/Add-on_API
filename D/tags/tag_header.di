module Add_on_API.D.tags.tag_header;

import Add_on_API.Add_on_API;


union tagType {
    uint tagTypeU;
    char[4] tagTypeC;
    //@disable this();
    this(uint Uint) {
        tagTypeU = Uint;
    }
}


struct s_tag_header {
    char[0x24]  PADDING0;
    e_tag_group tag_type;
    uint        random_number1;
    uint        header_size;
    char[0x08]  PADDING1;
    ushort      tag_version;
    short       engine_version;
    uint        engine_name;
};
static assert(s_tag_header.sizeof == 0x40, "Incorrect size of s_tag_header");

struct s_tag_reference {
    e_tag_group group_tag;
    tag_name_reference name;
    tag_name_length name_length; //Excluding null terminate (Is not in used ingame)
    s_ident tag_index; //Always -1 in Guerilla, is used ingame.
};
static assert(s_tag_reference.sizeof == 0x10, "Incorrect size of s_tag_reference");

struct s_tag_block_definition{

};

struct s_tag_block {
    int    count;
    void*   address;
    const   s_tag_block_definition* definition;
};
struct s_tag_group {

};