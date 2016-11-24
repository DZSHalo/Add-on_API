Add-on API for C Sharp

==========

Interfaces for Halo Extension for your customize plugin with little to no reverse engineering requirement.

---

Instruction of how to setup your first C Sharp Add-on with Visual Studio IDE.

1. Create a "Class Library" project.
2. Unload your project.
3. Edit _Your_Project_Name_.csproj
4. Scroll down to where `<Compile Include="_Your_Project_Name_.cs">` is located then go one line up or down for insertion to include the following code.
..* `    <Compile Include="..\Add-on API\Add-on API.cs">
      <Link>Add-on API\Add-on API.cs</Link>
    </Compile>
    <Compile Include="..\Add-on API\CSharp\*.cs">
      <Link>Add-on API\%(RecursiveDir)%(Filename)%(Extension).cs</Link>
    </Compile>
    <Compile Include="..\Add-on API\CSharp\cseries\*.cs">
      <Link>Add-on API\cseries\%(RecursiveDir)%(Filename)%(Extension).cs</Link>
    </Compile>
    <Compile Include="..\Add-on API\CSharp\tags\*.cs">
      <Link>Add-on API\tags\%(RecursiveDir)%(Filename)%(Extension).cs</Link>
    </Compile>`
5. Install `UnmanagedExports` from nuget package in order to use DLLExport ability.
6. Delcare first 2 `EXTOnEAOLoad` and `EXTOnEAOUnload` function to initialize your first Add-on in C Sharp language.
