//APPROVED
import Add_on_API.D.cseries.cseries;

struct s_wind_meta {
    real_range      velocity_world_unit;
    real_rotation2d variation_area;
    _real            local_variation_weight;
    _real            local_variation_rate;
    _real            damping;
};
static assert(s_wind_meta.sizeof == 0x1C, "Incorrect size of s_wind_meta");