Add-on API for Visual Basic

==========

Interfaces for Halo Extension for your customize plugin with little to no reverse engineering requirement.

---
NOTICE: Add-on API should be download and install in same directory where your Add-on(s) directory going to be in.
---

Instruction of how to setup your first Visual Basic Add-on with Visual Studio IDE.

1. Create a "Class Library" project.
2. Unload your project.
3. Edit _Your_Project_Name_.csproj
4. Scroll down to where `<Compile Include="_Your_Project_Name_.vb">` is located then go one line up or down for insertion to include the following code.
  ```
    <Compile Include="..\Add-on API\Add-on API.vb">
      <Link>Add-on API\Add-on API.vb</Link>
    </Compile>
    <Compile Include="..\Add-on API\VisualBasic\*.vb">
      <Link>Add-on API\%(RecursiveDir)%(Filename)%(Extension).vb</Link>
    </Compile>
    <Compile Include="..\Add-on API\VisualBasic\cseries\*.vb">
      <Link>Add-on API\cseries\%(RecursiveDir)%(Filename)%(Extension).vb</Link>
    </Compile>
    <Compile Include="..\Add-on API\VisualBasic\tags\*.vb">
      <Link>Add-on API\tags\%(RecursiveDir)%(Filename)%(Extension).vb</Link>
    </Compile>
```

5. Load your project.
6. Right-click your project, then click properties in context menu.
  1. Under Compile tab, change the "build output path:" to exclude `bin\` portion, from both release and debug configuration, in order for converter to detect a compiled build plugin.
  2. Change "Target CPU" from `AnyCPU` to `x86` since Halo is compiled for 32-bit application usage.
7. Install `UnmanagedExports` from nuget package in order to use DLLExport ability.
8. Delcare first 2 `EXTOnEAOLoad` and `EXTOnEAOUnload` function to initialize your first Add-on in Visual Basic language.
9. To use one of the API and hook available comes with Add-on API, please review the documentation for it.
