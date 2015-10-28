//APPROVED
typedef struct {
    PADDING(0x20);
    real weight;
    s_tag_reference item;   //Supported type: equipment, garbage, item, or weapon
    PADDING(0x20);
} s_item_permutation_block;
static_assert_check(sizeof(s_item_permutation_block) == 0x54, "Incorrect size of s_item_permutation_block");

typedef struct {
    //Item permutations block
    s_tag_block item_permutations;  //Max is 32
    short spawn_time;
    short pad;
    PADDING(0x4C);
} s_item_collection_meta;
static_assert_check(sizeof(s_item_collection_meta) == 0x5C, "Incorrect size of s_item_collection_meta");