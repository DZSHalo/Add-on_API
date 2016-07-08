using System;
using System.Runtime.InteropServices;

//TODO: Need to work on global defines for important message, unable to do class check compile time or don't have info of how-to...

#if EXT_ITIMER || EXT_HKTIMER
#warning Make sure you have export EXTOnTimerExecute and EXTOnTimerCancel functions!
#endif
#if EXT_HKDATABASE
#warning Please verify online documentation you have exported all necessary functions!
#endif