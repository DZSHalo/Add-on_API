//APPROVED
import D.cseries.cseries;

struct s_sound_environment_meta {
    short   priority;
    char[0x02] PADDING;
    _real    room_intensity;
    _real    room_intensity_hf;
    _real    room_rolloff_factor;
    _real    decay_time;
    _real    decay_hf_ratio;
    _real    reflections_intensity;
    _real    reflections_delay;
    _real    reverb_intensity;
    _real    reverb_delay;
    _real    diffusion;
    _real    density;
    _real    hf_reference;
};
static assert(s_sound_environment_meta.sizeof == 0x34, "Incorrect size of s_sound_environment_meta");