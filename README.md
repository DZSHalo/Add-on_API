Add-on API

==========


Interfaces for Halo Extension for your customize plugin with no reverse engineering requirement.


All of the add-ons are required to be in (your halo directory)/extension/plugins directory. Any additional DLL/modules are therefore required to be in (your halo directory)/extension/DLLs in order to load properly.

If you do not see the extension folder, then you are likely haven't setup your first time with Halo Extension. So first, start up either your client and dedicated server. Secondly, type in 'load' without the quote in the console to automatic create the directories along with other files as well. Thirdly, proceed with put the add-ons in the plugins directory. Finally, type in the plugin name in the console as shown ext_addon_load "plugin_name_here" to become active plugin.

However, if you do get a "Requested function 'load' cannot be executed now." message. You have not installed the Halo Extension which basically required winmm.dll and h-ext.dll to be in your halo directory.

More coming soon...