#ifndef databaseH
#define databaseH

extern const char dbErrorConnectionLost[];

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
	void EXTHookDatabaseEnabled();
	void EXTHookDatabaseDisabled();
	bool EXTIsHookDatabase();
	void EXTUnloadDatabase();

	class dllport DBConnection {
		public:
			bool WINAPIC Connect(LPCWSTR MDBPath,LPCWSTR User=L"", LPCWSTR Pass=L"",bool Exclusive=0);
			//bool Connect(LPCTSTR svSource);
			DBConnection();
			~DBConnection();
			DBConnection& operator=(DBConnection const&);
			void WINAPIC Disconnect();
			void WINAPIC STMTStatus();
			void WINAPIC Check();
			SQLHDBC WINAPIC HDBC() {
				return m_hDBC;
			}
		private:
			SQLRETURN		m_nReturn;		// Internal SQL Error code
			SQLHENV			m_hEnv;			// Handle to environment
			SQLHDBC			m_hDBC;			// Handle to database connection
	};
	/*extern "C" class MDBConnection {
	};*/
	class dllport DBStmt {
	private:
		SQLHSTMT m_hStmt;
		int m_nStmt;
	public:
		DBStmt();
		~DBStmt();
		DBStmt& operator=(DBStmt const&);
		DBStmt(SQLHDBC hDBCLink);
		bool WINAPIC IsValid();
		USHORT WINAPIC GetColumnCount();
		DWORD WINAPIC GetChangedRowCount();
		bool WINAPIC Query(LPCWSTR strSQL);
		bool WINAPIC Fetch();
		bool WINAPIC FetchRow(UINT nRow);
		bool WINAPIC FetchPrevious();
		bool WINAPIC FetchNext();
		bool WINAPIC FetchRow(ULONG nRow,bool Absolute=1);
		bool WINAPIC FetchFirst();
		bool WINAPIC FetchLast();
		bool WINAPIC Cancel();
		/*bool SetPos(SQLSETPOSIROW irow);
		bool OpenCursor();
		bool CloseCursor();*/

		bool WINAPIC BindColumn(USHORT Column, LPVOID pBuffer, ULONG pBufferSize, LONG * pReturnedBufferSize=NULL, USHORT nType=SQL_C_TCHAR);
		USHORT WINAPIC GetColumnByName(LPCTSTR Column);
		bool WINAPIC GetData(USHORT Column, LPVOID pBuffer, ULONG pBufLen, LONG * dataLen=NULL, int Type=SQL_C_DEFAULT);
		int WINAPIC GetColumnType( USHORT Column );
		DWORD WINAPIC GetColumnSize( USHORT Column );
		DWORD WINAPIC GetColumnScale( USHORT Column );
		bool WINAPIC GetColumnName( USHORT Column, LPTSTR Name, SHORT NameLen );
		bool WINAPIC IsColumnNullable( USHORT Column );
	};
};
extern "C" dllport DBSQL::DBConnection iDB;
#endif