//APPROVED
import Add_on_API.D.cseries.cseries;
import Add_on_API.D.tags.tag_include;

struct s_glow_flags {
    int bitfield;
    /*bool, "modify_particle_color_in_range", 1,
    bool, "particles_move_backards", 1,
    bool, "particles_move_in_both_directions", 1, //Easter egg: Guerilla only shows as "partices" when should be "particles".
    bool, "trailing_particles_fade_over_time", 1,
    bool, "trailing_particles_shrink_over_time", 1,
    bool, "trailing_particles_slow_over_time", 1,
    bool, "unused0", 1,
    bool, "unused1", 1,
    bool, "unused2", 8,
    bool, "unused3", 8,
    bool, "unused4", 8,*/
};
static assert(s_glow_flags.sizeof == 0x04, "Incorrect size of s_glow_flags");

enum e_boundary_effect : ushort {
    BOUNDARY_EFFECT_BOUNCE = 0,
    BOUNDARY_EFFECT_WRAP = 1
};
enum e_normal_particle : ushort {
    DISTRIBUTED_RANDOMLY = 0,
    DISTRIBUTED_UNIFORMLY = 1
};
enum e_trailing_particle : ushort {
    EMIT_VERTICALLY = 0,
    EMIT_NORMAL_UP = 1,
    EMIT_RANDOMLY = 2
};

struct s_glow_meta {
    char[0x20]          attachment_marker;
    ushort              number_of_particles;
    e_boundary_effect   boundary_effect;
    e_normal_particle   normal_particle_distribution;
    e_trailing_particle trailing_particle_distribution;
    s_glow_flags        glow_flags;
    char[0x24] PADDING0;
    e_attachment_out    attachment_particle;
    char[0x02] PADDING1;
    _real                particle_rotational_velocity;
    _real                particle_rot_vel_mul_low;
    _real                particle_rot_vel_mul_high;
    e_attachment_out    attachment_effect;
    char[0x02] PADDING2;
    _real                effect_rotational_velocity;
    _real                effect_rot_vel_mul_low;
    _real                effect_rot_vel_mul_high;
    e_attachment_out    attachment_translational;
    char[0x02] PADDING3;
    _real                effect_translational_velocity;
    _real                effect_trans_vel_mul_low;
    _real                effect_trans_vel_mul_high;
    e_attachment_out    attachment_distance;
    char[0x02] PADDING4;
    _real                min_distance_particle_to_object;
    _real                max_distance_particle_to_object;
    _real                distance_to_object_mul_low;
    _real                distance_to_object_mul_high;
    char[0x08] PADDING5;
    e_attachment_out    attachment_size;
    char[0x02] PADDING6;
    real_range          particle_size_bounds_world_unit;
    real_range          size_attachment_multiplier;
    e_attachment_out    attachment_color;
    char[0x02] PADDING7;
    real_color_alpha    color_bound_0;
    real_color_alpha    color_bound_1;
    real_color_alpha    scale_color_0;
    real_color_alpha    scale_color_1;
    _real                color_rate_of_change;
    _real                fadding_percentage_of_glow;
    _real                particle_generation_freq_Hz;
    _real                lifetime_of_trailing_particles_second;
    _real                velocity_of_trailing_particles_world_unit_per_second;
    _real                trailing_particle_minimum_t;
    _real                trailing_particle_maximum_t;
    char[0x34] PADDING8;
    //To obtain file
    s_tag_reference     texture;

    //Link to tag file in char array.

};
static assert(s_glow_meta.sizeof == 0x154, "Incorrect size of s_glow_meta");