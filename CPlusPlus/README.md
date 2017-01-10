Add-on API for C++

==========

Interfaces for Halo Extension for your customize plugin with little to no reverse engineering requirement.

---

Instruction of how to setup your first C++ Add-on with Visual Studio IDE.

1. Create a "Win32 Project" under Templates --> Visual C++ --> Win32
2. Click Next
3. Under Application type, choose "DLL". As for other options you can choose what you want to use with. Then click "Finish".
  * **NOTICE: Except for "Empty Project" must be unchecked, this is part of requirement for first timer!**
5. Open "_Your_project_name_.cpp" or hpp file to insert the following code below:
  ```
#include "..\Add-on API\Add-on API.h"
#pragma comment(lib, "../Add-on API/Add-on API.lib")
```

6. Delcare first 2 `EXTOnEAOLoad` and `EXTOnEAOUnload` function to initialize your first Add-on in C language.
7. To use one of the API and hook available comes with Add-on API, please review the documentation for it.
