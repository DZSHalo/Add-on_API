module D.cseries.cseries;

import Add_on_API;

//TODO: Need to check how to detect big/little endian order to fix this.
template MAKE_ID(char[4] s) {
    static assert(s.length == 4, "Must be exactly 4 char long, it is " ~ s.length);
    const uint MAKE_ID = (s[0] << 24) | (s[1] << 16) | (s[2] << 8) | s[3];
}
//auto MAKE_ID(  ARG1 )(ARG1 s) { return s; }

alias uint tag_name_length;
alias uint tag; //TODO: For now just do this until we find out what's it defined as.
alias const(char*) tag_name_reference;
alias float _real;
struct real_range {
    _real min;
    _real max;
}
struct real_color {
    _real red;
    _real green;
    _real blue;
}
struct real_color_alpha {
    _real alpha;
    _real red;
    _real green;
    _real blue;
}
struct real_vector2d {
    _real x = _real.nan;
    _real y = _real.nan;
    this(_real x, _real y) {
        this.x = x;
        this.y = y;
    }
    bool opEquals(ref real_vector2d v2) {
        if (v2.x == this.x && v2.y== this.y)
            return 1;
        return 0;
    }
}
struct real_rotation2d {
    _real yaw;
    _real pitch;
}
struct real_vector3d {
    _real x = _real.nan;
    _real y = _real.nan;
    _real z = _real.nan;
    this(_real x, _real y, _real z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    bool opEquals(ref real_vector3d v3) {
        if (v3.x == this.x && v3.y == this.y && v3.z == this.z)
            return 1;
        return 0;
    }
}
struct real_offset3d {
    // X-Component
    _real i;
    // Y-Component
    _real j;
    // Z-Component
    _real k;

}
struct real_quaternion {
    // X-Component
    _real i;
    // Y-Component
    _real j;
    // Z-Component
    _real k;
    // ?-Component
    _real w;
}

struct byte_color {
    ubyte alpha;
    ubyte red;
    ubyte green;
    ubyte blue;
}
struct short_range {
    short min;
    short max;
}

union s_ident {
    int Tag = -1;
    struct {
        short index;
        short salt;
    }
}
static assert(s_ident.sizeof == 0x4, "Incorrect size of s_ident");