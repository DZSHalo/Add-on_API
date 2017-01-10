//INCOMPLETE - 1 data part is missing
#ifndef __cplusplus
typedef unsigned short e_interpolate_color;
static const e_interpolate_color INTERPOLATE_LINEARLY = 0;
static const e_interpolate_color INTERPOLATE_FASTER_NEAR_EMPTY = 1;
static const e_interpolate_color INTERPOLATE_FASTER_NEAR_FULL = 2;
static const e_interpolate_color INTERPOLATE_THROUGH_RANDOM_NOISE = 3;
typedef unsigned short e_anchor_color;
static const e_anchor_color ANCHOR_AT_BOTH_ENDS = 0;
static const e_anchor_color ANCHOR_AT_EMPTY = 1;
static const e_anchor_color ANCHOR_AT_FULL = 2;
#endif

typedef struct s_stencil_data_definition {
    //Unknown
    unsigned int noResearchDone; //TODO: No research done
} s_stencil_data_definition;

typedef struct s_meter_meta {
    PADDING(0x04);
    s_tag_reference stencil_bitmaps; //bitm
    s_tag_reference source_bitmap;   //bitm
    signed short    stencil_sequence_index;
    signed short    source_sequence_index;
    PADDING(0x14);
    e_interpolate_color interpolate_colors;
    e_anchor_color      anchor_colors;
    PADDING(0x08);
    real_color_alpha    empty_color;
    real_color_alpha    full_color;
    PADDING(0x14);
    real                unmask_distance_meter_units;
    real                mask_distance_meter_units;
    PADDING(0x14);
    unsigned int        encoded_stencil; //Read only - DO NOT MODIFY DIRECTLY!
    UNKNOWN(0x10);
} s_meter_meta;
static_assert_check(sizeof(s_meter_meta) == 0xAC, "Incorrect size of s_meter_meta");
