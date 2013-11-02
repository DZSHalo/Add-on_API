#ifndef iniFileH
#define iniFileH

#define INIFILELENMAX 512

extern "C" class dllport CIniFile {
public:
#pragma pack(push,1)
	struct Record {
		wchar_t comments[256];
		wchar_t commented;
		wchar_t section[128];
		wchar_t key[128];
		wchar_t value[256];
	};
#pragma pack(pop)
	enum commentChar {
		pound = L'#',
		semiColon = L';'
	};
	CIniFile();
	~CIniFile();
	CIniFile(CIniFile const&);
	CIniFile& operator=(CIniFile const&);
	void WINAPIC Close();
	bool WINAPIC Open(wchar_t const fileName[]);
	bool WINAPIC Create(wchar_t const fileName[]);
	bool WINAPIC Delete(wchar_t const fileName[]);
	bool WINAPIC Content(const wchar_t*& content, size_t &len);
	bool WINAPIC SectionAdd(wchar_t const sectionName[]);
	bool WINAPIC SectionDelete(wchar_t const sectionName[]);
	bool WINAPIC SectionExist(wchar_t const sectionName[]);
	bool WINAPIC ValueExist(wchar_t const keyName[], const wchar_t sectionName[]);
	bool WINAPIC ValueSet(wchar_t const keyName[], wchar_t valueName[], const wchar_t sectionName[]);
	bool WINAPIC ValueGet(wchar_t const keyName[], wchar_t valueName[], const wchar_t sectionName[]);
	bool WINAPIC Save();
	bool WINAPIC Load();
	void WINAPIC Clear();
private:
	wchar_t tempFileName[INIFILELENMAX];
	const wchar_t* strFileContentW;
	DWORD size;
	HANDLE hFile;
	HANDLE hFileMap;
#pragma warning( push )
#pragma warning( disable : 4251)
	util::dynamicStack<Record>* content;
#pragma warning( pop )
};

#endif