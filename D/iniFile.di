module D.iniFile;

import Add_on_API;

static if(__traits(compiles, EXT_ICINIFILE)) {

    enum INIFILELENMAX = 512;
    enum INIFILESECTIONMAX = 128;
    enum INIFILEKEYMAX = 128;
    enum INIFILEVALUEMAX = 128;
    enum commentChar: wchar {
        pound = '#',
        semiColon = ';'
    }
    extern(C) struct ICIniFile {
        /*
         * To release ICIniFile, cannot be re-used after release.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * Returns: Does not return a value.
         */
        void function(ICIniFile* self) m_release;
        /*
         * To close a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * Returns: Does not return a value.
         */
        void function(ICIniFile* self) m_close;
        /*
         * To open a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * fileName = Name of a file to open.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* fileName) m_open_file;
        /*
         * To create a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * fileName = Name of a file to create.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* fileName) m_create_file;
        /*
         * To delete a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * fileName = Name of a file to delete.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* fileName) m_delete_file;
        /*
         * To obtain raw content from an opened file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * content = Raw content within a file.
         * len = Length of the content.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar** content, uint* len) m_content;
        /*
         * To add a section in a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of a section to add. Maximum characters is INIFILESECTIONMAX.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName) m_section_add;
        /*
         * To delete a section in a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of a section to delete. Maximum characters is INIFILESECTIONMAX. (This will delete all keys within a section!)
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName) m_section_delete;
        /*
         * To verify a section in a file do exist.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of a section to verify if exist or not. Maximum characters is INIFILESECTIONMAX.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName) m_section_exist;
        /*
         * To verify key within existing section in a file do exist.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of an existing section. Maximum characters is INIFILESECTIONMAX.
         * keyName = Name of a key to verify if exist or not. Maximum characters is INIFILEKEYMAX.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName, const wchar* keyName) m_key_exist;
        /*
         * To set value within existing key and section in a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of an existing section. Maximum characters is INIFILESECTIONMAX. (INFO: If a section has not been create, it will create one automatically.)
         * keyName = Name of an existing key in a section. Maximum characters is INIFILEKEYMAX. (INFO: If a key has not been create, it will create one automatically.)
         * valueName = Name of a value to set in a key. Maximum characters is INIFILEVALUEMAX. (NOTICE: It will overwrite existing value!)
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName, const wchar* keyName, const wchar* valueName) m_value_set;
        /*
         * To get a value within existing key and section in a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of an existing section. Maximum characters is INIFILESECTIONMAX.
         * keyName = Name of a key from a section. Maximum characters is INIFILEKEYMAX.
         * valueName = Name of value to get from a key. Maximum characters is INIFILEVALUEMAX.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName, const wchar* keyName, wchar* valueName) m_value_get;
        /*
         * To save data buffer into file content.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self) m_save;
        /*
         * To load content into data buffer.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self) m_load;
        /*
         * To clear the content and data buffer. (NOTICE: If you want to clear everything in a file, you must save it after call m_clear.)
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * Returns: Does not return a value.
         */
        void function(ICIniFile* self) m_clear;
        /*
         * To delete a key within existing section in a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of an existing section. Maximum characters is INIFILESECTIONMAX.
         * keyName = Name of a key to delete from a section. Maximum characters is INIFILEKEYMAX.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName, const wchar* keyName) m_key_delete;
        /*
         * Get total count of sections from a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * Returns: Return total count of keys.
         */
        uint function(ICIniFile* self) m_section_count;
        /*
         * Get a section name by index in a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionIndex = Input section index.
         * sectionName = Get name of section. Maximum characters is INIFILESECTIONMAX.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, uint sectionIndex, wchar* sectionName) m_section_index;
        /*
         * Get total count of keys from existing section in a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of an existing section. Maximum characters is INIFILESECTIONMAX.
         * Returns: Return total count of keys.
         */
        uint function(ICIniFile* self, const wchar* sectionName) m_key_count;
        /*
         * Get a key and value from section's key index in a file.
         * Params:
         * self = Must include pointer of existing ICIniFile variable.
         * sectionName = Name of an existing section. Maximum characters is INIFILESECTIONMAX.
         * keyIndex = Input key index.
         * keyName = Name of a key to get from a section. Maximum characters is INIFILEKEYMAX.
         * valueName = Name of a value to get from a section. Maximum characters is INIFILEVALUEMAX.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName, uint keyIndex, wchar* keyName, wchar* valueName) m_key_index;
    }
    export extern(C) ICIniFile* getICIniFile(uint hash);
}
