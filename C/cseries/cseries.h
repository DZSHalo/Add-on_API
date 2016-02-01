#pragma once

//TODO: Need to check how to detect big/little endian order to fix this.
/*#define MAKE_ID(tmp) (tmp >> 24) |\
((tmp >> 8) & 0x0000ff00) |\
((tmp << 8) & 0x00ff0000) | \
(tmp << 24)*/
#define MAKE_ID(s) s

typedef float real;
typedef unsigned int tag_name_length;
typedef unsigned int tag; //TODO: For now just do this until we find out what's it defined as.
typedef const char* tag_name_reference;

typedef struct {
    real min;
    real max;
} real_range;
typedef struct {
    real red;
    real green;
    real blue;
} real_color;
typedef struct {
    real alpha;
    real red;
    real green;
    real blue;
} real_color_alpha;

typedef struct {
    real x;
    real y;
} real_vector2d;
typedef struct {
    real yaw;
    real pitch;
} real_rotation2d;
typedef struct {
    real x;
    real y;
    real z;
} real_vector3d;
typedef struct {
    // X-Component
    real i;
    // Y-Component
    real j;
    // Z-Component
    real k;

} real_offset3d;
typedef struct {
    // X-Component
    real i;
    // Y-Component
    real j;
    // Z-Component
    real k;
    // ?-Component
    real w;
} real_quaternion;

typedef struct {
    unsigned char alpha;
    unsigned char red;
    unsigned char green;
    unsigned char blue;
} byte_color;

typedef struct {
    short min;
    short max;
} short_range;

union s_ident {
    int Tag;
    struct {
        short index;
        short salt;
    };
    s_ident() {
        Tag = -1;
    };
    s_ident(int _Tag) {
        Tag = _Tag;
    };
};

struct s_ident_ret {
    s_ident ret;
};