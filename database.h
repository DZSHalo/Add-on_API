#ifndef databaseH
#define databaseH

extern "C" namespace DBSQL {
	//--
	//#pragma comment(lib,"Msvcrt.lib") //Basically bypass the error of vsnprintf... Replace the odbccp32.lib file with vista SDK!!!
	#pragma comment(lib,"odbc32.lib")
	//#pragma comment(lib,"odbcbcp.lib")
	#pragma comment(lib,"odbccp32.lib")
	//--
	#include <sql.h>
	#include <sqlext.h>
	#include <odbcinst.h>

	#define IS_SQL_ERR !IS_SQL_OK
	#define IS_SQL_OK(res) (res==SQL_SUCCESS_WITH_INFO || res==SQL_SUCCESS)
	#define SAFE_STR(str) ((str==NULL) ? _T("") : str)
	//--
	
	class IDBConnection {
		public:
			virtual bool WINAPIC Connect(LPCWSTR MDBPath,LPCWSTR User=L"", LPCWSTR Pass=L"",bool Exclusive=0)=0;
			//bool Connect(LPCTSTR svSource);
			//static DBConnection& operator=(DBConnection const&);
			virtual void WINAPIC Disconnect()=0;
			virtual void WINAPIC STMTStatus()=0;
			virtual void WINAPIC Check()=0;
			virtual SQLHDBC WINAPIC HDBC()=0;
			//static IDBConnection dllport WINAPIC getIDBConnection();
		private:
			//IDBConnection();
			//~IDBConnection();
	};
	class IDBStmt {
	public:
		virtual void Release();
		virtual bool WINAPIC IsValid()=0;
		virtual USHORT WINAPIC GetColumnCount()=0;
		virtual DWORD WINAPIC GetChangedRowCount()=0;
		virtual bool WINAPIC Query(LPCWSTR strSQL)=0;
		virtual bool WINAPIC Fetch()=0;
		virtual bool WINAPIC FetchRow(UINT nRow)=0;
		virtual bool WINAPIC FetchPrevious()=0;
		virtual bool WINAPIC FetchNext()=0;
		virtual bool WINAPIC FetchRow(ULONG nRow,bool Absolute=1)=0;
		virtual bool WINAPIC FetchFirst()=0;
		virtual bool WINAPIC FetchLast()=0;
		virtual bool WINAPIC Cancel()=0;

		virtual bool WINAPIC BindColumn(USHORT Column, LPVOID pBuffer, ULONG pBufferSize, LONG * pReturnedBufferSize=NULL, USHORT nType=SQL_C_TCHAR)=0;
		virtual USHORT WINAPIC GetColumnByName(LPCTSTR Column)=0;
		virtual bool WINAPIC GetData(USHORT Column, LPVOID pBuffer, ULONG pBufLen, LONG * dataLen=NULL, int Type=SQL_C_DEFAULT)=0;
		virtual int WINAPIC GetColumnType( USHORT Column )=0;
		virtual DWORD WINAPIC GetColumnSize( USHORT Column )=0;
		virtual DWORD WINAPIC GetColumnScale( USHORT Column )=0;
		virtual bool WINAPIC GetColumnName( USHORT Column, LPTSTR Name, SHORT NameLen )=0;
		virtual bool WINAPIC IsColumnNullable( USHORT Column )=0;
	};
	dllport IDBConnection* WINAPIC getIDBConnection();
	dllport IDBStmt* WINAPIC getIDBStmt();
};
#endif