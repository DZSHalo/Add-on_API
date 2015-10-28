//APPROVED
typedef struct {
    short   priority;
    PADDING(0x02);
    real    room_intensity;
    real    room_intensity_hf;
    real    room_rolloff_factor;
    real    decay_time;
    real    decay_hf_ratio;
    real    reflections_intensity;
    real    reflections_delay;
    real    reverb_intensity;
    real    reverb_delay;
    real    diffusion;
    real    density;
    real    hf_reference;
} s_sound_environment_meta;
static_assert_check(sizeof(s_sound_environment_meta) == 0x34, "Incorrect size of s_sound_environment_meta");