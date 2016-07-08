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

typedef struct real_range {
    real min;
    real max;
} real_range;
typedef struct real_color {
    real red;
    real green;
    real blue;
} real_color;
typedef struct real_color_alpha {
    real alpha;
    real red;
    real green;
    real blue;
} real_color_alpha;

typedef struct real_vector2d {
    real x;
    real y;
    real_vector2d(float x, float y) {
        this->x = x;
        this->y = y;
    }
} real_vector2d;
typedef struct real_rotation2d {
    real yaw;
    real pitch;
} real_rotation2d;
typedef struct real_vector3d {
    real x;
    real y;
    real z;
    real_vector3d() {
        this->x = this->y = this->z = (float)0xFFFFFFFF;
    }
    real_vector3d(float x, float y, float z) {
        this->x = x;
        this->y = y;
        this->z = z;
    }
    bool operator == (real_vector3d &v3) {
        if (v3.x == this->x && v3.y == this->y && v3.z == this->z)
            return 1;
        return 0;
    }
    bool operator != (real_vector3d &v3) {
        return !this->operator==(v3);
    }
} real_vector3d;
typedef struct real_offset3d {
    // X-Component
    real i;
    // Y-Component
    real j;
    // Z-Component
    real k;

} real_offset3d;
typedef struct real_quaternion {
    // X-Component
    real i;
    // Y-Component
    real j;
    // Z-Component
    real k;
    // ?-Component
    real w;
} real_quaternion;

typedef struct byte_color_alpha {
    unsigned char alpha;
    unsigned char red;
    unsigned char green;
    unsigned char blue;
} byte_color_alpha;

typedef struct short_range {
    short min;
    short max;
} short_range;

typedef struct short_point {
    short x;
    short y;
} short_point;

typedef union s_ident {
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
} s_ident;

typedef struct s_ident_ret {
    s_ident ret;
} s_ident_ret;