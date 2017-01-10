Imports System.Runtime.InteropServices

'TODO: Need to work on global defines for important message, unable to do class check compile time or don't have info of how-to...

#If EXT_ITIMER AndAlso Not EXT_HKTIMER Then
#Error EXT_HKTIMER is a requirement to be used with EXT_ITIMER.
#Elif Not EXT_ITIMER AndAlso EXT_HKTIMER Then
#Error EXT_ITIMER is a requirement to be used with EXT_HKTIMER.
#Elif EXT_ITIMER AndAlso EXT_HKTIMER Then
#Warning Make sure you have export EXTOnTimerExecute and EXTOnTimerCancel functions!
#End If
#If EXT_HKDATABASE Then
#Warning Please verify online documentation you have exported all necessary functions!
#End If
