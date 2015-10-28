//APPROVED
typedef struct  {
    bool modify_particle_color_in_range : 1;
    bool particles_move_backards : 1;
    bool particles_move_in_both_directions : 1; //Easter egg: Guerilla only shows as "partices" when should be "particles".
    bool trailing_particles_fade_over_time : 1;
    bool trailing_particles_shrink_over_time : 1;
    bool trailing_particles_slow_over_time : 1;
    bool unused0 : 1;
    bool unused1 : 1;
    bool unused2 : 8;
    bool unused3 : 8;
    bool unused4 : 8;
} s_glow_flags;
static_assert_check(sizeof(s_glow_flags) == 0x04, "Incorrect size of s_glow_flags");

enum e_boundary_effect : unsigned short {
    BOUNDARY_EFFECT_BOUNCE = 0,
    BOUNDARY_EFFECT_WRAP = 1
};
enum e_normal_particle : unsigned short {
    DISTRIBUTED_RANDOMLY = 0,
    DISTRIBUTED_UNIFORMLY = 1
};
enum e_trailing_particle : unsigned short {
    EMIT_VERTICALLY = 0,
    EMIT_NORMAL_UP = 1,
    EMIT_RANDOMLY = 2
};

typedef struct {
    char                attachment_marker[0x20];
    unsigned short      number_of_particles;
    e_boundary_effect   boundary_effect;
    e_normal_particle   normal_particle_distribution;
    e_trailing_particle trailing_particle_distribution;
    s_glow_flags        glow_flags;
    PADDING(0x24);
    e_attachment_out    attachment_particle;
    PADDING(0x02);
    real                particle_rotational_velocity;
    real                particle_rot_vel_mul_low;
    real                particle_rot_vel_mul_high;
    e_attachment_out    attachment_effect;
    PADDING(0x02);
    real                effect_rotational_velocity;
    real                effect_rot_vel_mul_low;
    real                effect_rot_vel_mul_high;
    e_attachment_out    attachment_translational;
    PADDING(0x02);
    real                effect_translational_velocity;
    real                effect_trans_vel_mul_low;
    real                effect_trans_vel_mul_high;
    e_attachment_out    attachment_distance;
    PADDING(0x02);
    real                min_distance_particle_to_object;
    real                max_distance_particle_to_object;
    real                distance_to_object_mul_low;
    real                distance_to_object_mul_high;
    PADDING(0x08);
    e_attachment_out    attachment_size;
    PADDING(0x02);
    real_range          particle_size_bounds_world_unit;
    real_range          size_attachment_multiplier;
    e_attachment_out    attachment_color;
    PADDING(0x02);
    real_color_alpha    color_bound_0;
    real_color_alpha    color_bound_1;
    real_color_alpha    scale_color_0;
    real_color_alpha    scale_color_1;
    real                color_rate_of_change;
    real                fadding_percentage_of_glow;
    real                particle_generation_freq_Hz;
    real                lifetime_of_trailing_particles_second;
    real                velocity_of_trailing_particles_world_unit_per_second;
    real                trailing_particle_minimum_t;
    real                trailing_particle_maximum_t;
    PADDING(0x34);
    //To obtain file
    s_tag_reference     texture;

    //Link to tag file in char array.

} s_glow_meta;
static_assert_check(sizeof(s_glow_meta) == 0x154, "Incorrect size of s_glow_meta");