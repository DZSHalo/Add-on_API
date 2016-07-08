//APPROVED
typedef enum e_collection_type : unsigned short {
    COLLECTION_TYPE_SCREEN_FACING = 0,
    COLLECTION_TYPE_VIEWER_FACING
} e_collection_type;

typedef struct s_detail_object_type_flags {
    bool    unused : 2;
    bool    interpolate_color_in_hsv : 1;
    bool    more_colors : 1;
    bool    unused1 : 4;
} s_detail_object_type_flags;

typedef struct s_detail_object_type_block {
    char                        name[0x20];
    unsigned char               sequence_index;
    s_detail_object_type_flags  type_flags;
    PADDING(0x02);
    real                        color_override_factor;
    PADDING(0x08);
    real                        near_fade_distance_world_units;
    real                        far_fade_distance_world_units;
    real                        size_world_units_per_pixels;
    PADDING(0x04);
    real_color                  minimum_color;
    real_color                  maximum_color;
    byte_color_alpha            ambient_color;
    PADDING(0x04);
} s_detail_object_type_block;
static_assert_check(sizeof(s_detail_object_type_block) == 0x60, "Incorrect size of s_detail_object_type_block");

typedef struct s_detail_object_collection_meta {
    e_collection_type   collection_type;
    PADDING(0x02);
    real                global_z_offset_applied_to_all_detail_objects_;
    PADDING(0x2C);
    s_tag_reference     sprite_plate; //bitm
    s_tag_block         types;
    PADDING(0x30);
} s_detail_object_collection_meta;
static_assert_check(sizeof(s_detail_object_collection_meta) == 0x80, "Incorrect size of s_detail_object_collection_meta");