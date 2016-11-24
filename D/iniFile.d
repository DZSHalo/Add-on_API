module Add_on_API.D.iniFile;

import Add_on_API.Add_on_API;

static if(__traits(compiles, EXT_ICINIFILE)) {

    enum INIFILELENMAX = 512;
    enum commentChar: wchar {
        pound = '#',
        semiColon = ';'
    };
    extern(C) struct ICIniFile {
        /*
         * To release ICIniFile, cannot be re-used after release.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * Returns: Does not return a value.
         */
        void function(ICIniFile* self) m_release;
        /*
         * To close a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * Returns: Does not return a value.
         */
        void function(ICIniFile* self) m_close;
        /*
         * To open a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * fileName = Name of a file to open.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* fileName) m_open_file;
        /*
         * To create a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * fileName = Name of a file to create.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* fileName) m_create_file;
        /*
         * To delete a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * fileName = Name of a file to delete.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* fileName) m_delete_file;
        /*
         * To obtain raw content from an opened file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * content = Raw content within a file.
         * len = Length of the content.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, ref const wchar* content, ref uint len) m_content;
        /*
         * To add a section in a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * sectionName = Name of a section to add. Maximum characters is 256 long.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName) m_section_add;
        /*
         * To delete a section in a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * sectionName = Name of a section to delete. Maximum characters is 256 long. (This will delete all keys within a section!)
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName) m_section_delete;
        /*
         * To verify a section in a file do exist.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * sectionName = Name of a section to verify if exist or not. Maximum characters is 256 long.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* sectionName) m_section_exist;
        /*
         * To verify key within existing section in a file do exist.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * keyName = Name of a key to verify if exist or not. Maximum characters is 256 long.
         * sectionName = Name of an existing section. Maximum characters is 256 long.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* keyName, const wchar* sectionName) m_key_exist;
        /*
         * To set value within existing key and section in a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * keyName = Name of an existing key in a section. Maximum characters is 256 long. (INFO: If a key has not be create, it will create one automatically.)
         * valueName = Name of a value to set in a key. Maximum characters is 256 long. (NOTICE: It will overwrite existing value!)
         * sectionName = Name of an existing section. Maximum characters is 256 long. (INFO: If a section has not be create, it will create one automatically.)
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* keyName, wchar* valueName, const wchar* sectionName) m_value_set;
        /*
         * To get a value within existing key and section in a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * keyName = Name of a key from a section. Maximum characters is 256 long.
         * valueName = Name of value to get from a key. Maximum characters is 256 long.
         * sectionName = Name of an existing section. Maximum characters is 256 long.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* keyName, wchar* valueName, const wchar* sectionName) m_value_get;
        /*
         * To save data buffer into file content.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self) m_save;
        /*
         * To load content into data buffer.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self) m_load;
        /*
         * To clear the content and data buffer. (NOTICE: If you want to clear everything in a file, you must save it after call m_clear.)
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * Returns: Does not return a value.
         */
        void function(ICIniFile* self) m_clear;
        /*
         * To delete a key within existing section in a file.
         * Params:
         * self = Must include pointer of an existing ICIniFile variable.
         * keyName = Name of a key to delete from a section. Maximum characters is 256 long.
         * sectionName = Name of an existing section. Maximum characters is 256 long.
         * Returns: Return true or false.
         */
        bool function(ICIniFile* self, const wchar* keyName, const wchar* sectionName) m_key_delete;
    }
    export extern(C) ICIniFile* getICIniFile(uint hash);
}
