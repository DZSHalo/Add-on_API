#ifndef iniFileH
#define iniFileH

#define INIFILELENMAX 512

extern "C" class ICIniFile {
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
	virtual void WINAPIC Release();
	virtual void WINAPIC Close()=0;
	virtual bool WINAPIC Open(wchar_t const fileName[])=0;
	virtual bool WINAPIC Create(wchar_t const fileName[])=0;
	virtual bool WINAPIC Delete(wchar_t const fileName[])=0;
	virtual bool WINAPIC Content(const wchar_t*& content, size_t &len)=0;
	virtual bool WINAPIC SectionAdd(wchar_t const sectionName[])=0;
	virtual bool WINAPIC SectionDelete(wchar_t const sectionName[])=0;
	virtual bool WINAPIC SectionExist(wchar_t const sectionName[])=0;
	virtual bool WINAPIC ValueExist(wchar_t const keyName[], const wchar_t sectionName[])=0;
	virtual bool WINAPIC ValueSet(wchar_t const keyName[], wchar_t valueName[], const wchar_t sectionName[])=0;
	virtual bool WINAPIC ValueGet(wchar_t const keyName[], wchar_t valueName[], const wchar_t sectionName[])=0;
	virtual bool WINAPIC Save()=0;
	virtual bool WINAPIC Load()=0;
	virtual void WINAPIC Clear()=0;
};
extern "C" dllport ICIniFile* WINAPIC getICIniFile();

#endif