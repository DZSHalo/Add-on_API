//APPROVED
typedef struct {
    real_range      velocity_world_unit;
    real_rotation2d variation_area;
    real            local_variation_weight;
    real            local_variation_rate;
    real            damping;
} s_wind_meta;
static_assert_check(sizeof(s_wind_meta) == 0x1C, "Incorrect size of s_wind_meta");