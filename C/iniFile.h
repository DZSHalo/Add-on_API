#ifndef iniFileH
#define iniFileH

#define INIFILELENMAX 512
#define INIFILESECTIONMAX 128
#define INIFILEKEYMAX 128
#define INIFILEVALUEMAX 128

#ifdef __cplusplus
CNATIVE {
#endif
    typedef enum commentChar {
        pound = L'#',
        semiColon = L';'
    } commentChar;
    typedef struct ICIniFile ICIniFile;
    typedef struct ICIniFile {
        /// <summary>
        /// To release ICIniFile, cannot be re-used after release.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Does not return a value.</returns>
        void (*m_release)(ICIniFile* self);
        /// <summary>
        /// To close a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Does not return a value.</returns>
        void (*m_close)(ICIniFile* self);
        /// <summary>
        /// To open a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="fileName">Name of a file to open.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_open_file)(ICIniFile* self, const wchar_t* fileName);
        /// <summary>
        /// To create a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="fileName">Name of a file to create.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_create_file)(ICIniFile* self, const wchar_t* fileName);
        /// <summary>
        /// To delete a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="fileName">Name of a file to delete.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_delete_file)(ICIniFile* self, const wchar_t* fileName);
        /// <summary>
        /// To obtain raw content from an opened file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="content">Raw content within a file.</param>
        /// <param name="len">Length of the content.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_content)(ICIniFile* self, const wchar_t** content, unsigned int* len);
        /// <summary>
        /// To add a section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of a section to add. Maximum characters is INIFILESECTIONMAX.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_section_add)(ICIniFile* self, const wchar_t* sectionName);
        /// <summary>
        /// To delete a section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of a section to delete. Maximum characters is INIFILESECTIONMAX. (This will delete all keys within a section!)</param>
        /// <returns>Return true or false.</returns>
        bool (*m_section_delete)(ICIniFile* self, const wchar_t* sectionName);
        /// <summary>
        /// To verify a section in a file do exist.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of a section to verify if exist or not. Maximum characters is INIFILESECTIONMAX.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_section_exist)(ICIniFile* self, const wchar_t* sectionName);
        /// <summary>
        /// To verify key within existing section in a file do exist.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <param name="keyName">Name of a key to verify if exist or not. Maximum characters is INIFILEKEYMAX.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_key_exist)(ICIniFile* self, const wchar_t* sectionName, const wchar_t* keyName);
        /// <summary>
        /// To set key within existing section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX. (INFO: If a section has not be create, it will create one automatically.)</param>
        /// <param name="keyName">Name of an existing key in a section. Maximum characters is INIFILEKEYMAX. (INFO: If a key has not be create, it will create one automatically.)</param>
        /// <param name="valueName">Name of a value to set in a key. Maximum characters is INIFILEVALUEMAX. (NOTICE: It will overwrite existing value!)</param>
        /// <returns>Return true or false.</returns>
        bool (*m_value_set)(ICIniFile* self, const wchar_t* sectionName, const wchar_t* keyName, const wchar_t* valueName);
        /// <summary>
        /// To get key within existing section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <param name="keyName">Name of a key to get from a section. Maximum characters is INIFILEKEYMAX.</param>
        /// <param name="valueName">Name of a value to get from a key. Maximum characters is INIFILEVALUEMAX.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_value_get)(ICIniFile* self, const wchar_t* sectionName, const wchar_t* keyName, wchar_t* valueName);
        /// <summary>
        /// To save data buffer into file content.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_save)(ICIniFile* self);
        /// <summary>
        /// To load content into data buffer.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_load)(ICIniFile* self);
        /// <summary>
        /// To clear the content and data buffer. (NOTICE: If you want to clear everything in a file, you must save it after call m_clear.)
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Does not return a value.</returns>
        void (*m_clear)(ICIniFile* self);
        /// <summary>
        /// To delete a key within existing section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <param name="keyName">Name of a key to delete from a section. Maximum characters is INIFILEKEYMAX.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_key_delete)(ICIniFile* self, const wchar_t* sectionName, const wchar_t* keyName);
        /// <summary>
        /// Get total count of sections from a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <returns>Return total count of keys.</returns>
        unsigned int (*m_section_count)(ICIniFile* self);
        /// <summary>
        /// Get a section name by index in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionIndex">Input section index.</param>
        /// <param name="sectionName">Get name of section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_section_index)(ICIniFile* self, unsigned int sectionIndex, wchar_t* sectionName);
        /// <summary>
        /// Get total count of keys from existing section in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <returns>Return total count of keys.</returns>
        unsigned int (*m_key_count)(ICIniFile* self, const wchar_t* sectionName);
        /// <summary>
        /// Get a key and value from section's key index in a file.
        /// </summary>
        /// <param name="self">Must include pointer of existing ICIniFile variable.</param>
        /// <param name="sectionName">Name of an existing section. Maximum characters is INIFILESECTIONMAX.</param>
        /// <param name="keyIndex">Input key index.</param>
        /// <param name="keyName">Name of a key to get from a section. Maximum characters is INIFILEKEYMAX.</param>
        /// <param name="valueName">Name of a value to get from a section. Maximum characters is INIFILEVALUEMAX.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_key_index)(ICIniFile* self, const wchar_t* sectionName, unsigned int keyIndex, wchar_t* keyName, wchar_t* valueName);
    } ICIniFile;
#ifdef EXT_ICINIFILE
    CNATIVE dllport ICIniFile* getICIniFile(unsigned int hash);
#endif
#ifdef __cplusplus
}
#endif
#endif
