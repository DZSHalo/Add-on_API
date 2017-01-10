Add-on API for D

==========

Interfaces for Halo Extension for your customize plugin with little to no reverse engineering requirement.

---

**NOTICE: You must have Visual D plugin install before attempt to use Visual Studio IDE with D language!**

Instruction of how to setup your first D Add-on with Visual Studio IDE.

1. Create a "Dynamic Library (DLL)" under Templates --> Other Languages --> D
2. Right-click your project, then click properties in context menu.
  1. In Configuration Properties --> Compiler --> General tab, add `"..\Add-on API"` in "Additional Import Paths" text field.
  2. In Debugging tab, I highly recommend to change `Mago` to `Visual Studio` in Debugger's dropdown menu.
    * **NOTICE: Visual Studio option for debug does not include "IMAGE_FILE_DLL" flag in nt header structure. You will need to manually add it or use Mago debugger option instead. Otherwise, Converter and H-Ext will reject it.**
  3. In Linker --> General tab, add `"..\Add-on API"` in "Library Search Path" text field.
3. Unload your project.
4. Edit _Your_Project_name.visualdproj
5. Scroll down to where `<Folder name="_Your_project_Name_">` is located, then go one line down to include the following code.
  ```
  <Folder name="Add-on API">
   <Folder name="cseries">
    <File path="..\Add-on API\D\cseries\cseries.d" />
   </Folder>
   <Folder name="D">
    <File path="..\Add-on API\D\database.d" />
    <File path="..\Add-on API\D\hext.d" />
    <File path="..\Add-on API\D\object.d" />
    <File path="..\Add-on API\D\player.d" />
    <File path="..\Add-on API\D\structs.d" />
    <File path="..\Add-on API\D\util.d" />
   </Folder>
   <File path="..\Add-on API\Add_on_API.di" />  <!--Not required, yet recommend to keep-->
  </Folder>
```

6. Load your project.
7. Create a file named "global" as this is where you will declare your defined API versions and addon_info.
  * **NOTICE: THIS IS A REQUIREMENT OR OTHERWISE IT WILL NOT COMPILE CORRECTLY.**
8. Delcare first 2 `EXTOnEAOLoad` and `EXTOnEAOUnload` function to initialize your first Add-on in D language.
9. To use one of the API and hook available comes with Add-on API, please review the documentation for it.
