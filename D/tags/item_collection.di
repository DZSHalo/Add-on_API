//APPROVED
import Add_on_API.D.cseries.cseries;
import Add_on_API.D.tags.tag_include;

struct s_item_permutation_block {
    char[0x20] PADDING0;
    _real weight;
    s_tag_reference item;   //Supported type: equipment, garbage, item, or weapon
    char[0x20] PADDING1;
};
static assert(s_item_permutation_block.sizeof == 0x54, "Incorrect size of s_item_permutation_block");

struct s_item_collection_meta {
    //Item permutations block
    s_tag_block item_permutations;  //Max is 32
    short spawn_time;
    short pad;
    char[0x4C] PADDING;
};
static assert(s_item_collection_meta.sizeof == 0x5C, "Incorrect size of s_item_collection_meta");