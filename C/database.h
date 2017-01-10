#ifndef databaseH
#define databaseH

#ifdef __cplusplus
CNATIVE {
#endif
#if defined(EXT_IDATABASE) || defined(EXT_IDATABASESTATEMENT)
    //--
    //#pragma comment(lib,"Msvcrt.lib") //Basically bypass the error of vsnprintf... Replace the odbccp32.lib file with vista SDK!!!
    #pragma comment(lib,"odbc32.lib")
    #pragma comment(lib,"odbccp32.lib")
    //--
    #include <sql.h>
    #include <sqlext.h>
    #include <odbcinst.h>

    #define IS_SQL_ERR !IS_SQL_OK
    #define IS_SQL_OK(res) (res==SQL_SUCCESS_WITH_INFO || res==SQL_SUCCESS)
    #define SAFE_STR(str) ((str==NULL) ? L"" : str)
#endif
    //--
#ifdef EXT_IDATABASE
    typedef struct IDatabase {
        /// <summary>
        /// To connect a supported database.
        /// </summary>
        /// <param name="ConnectionStr">See https://msdn.microsoft.com/en-us/library/ms715433.aspx for details.</param>
        /// <param name="Option">See Attribute at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        /// <param name="Param">See ValuePtr at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        /// <param name="ParamLen">See StringLength at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_connect)(SQLWCHAR* ConnectionStr, SQLINTEGER Option, SQLPOINTER Param, SQLINTEGER ParamLen);
        /// <summary>
        /// To connect local MDB file database. (Please note this function call to m_connect function.)
        /// </summary>
        /// <param name="MDBPath">Path to local MDB file database.</param>
        /// <param name="User">Username if required to connect.</param>
        /// <param name="Pass">Password if required to connect.</param>
        /// <param name="Exclusive">True to restrict access to database or false to share database.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_connect_mdb)(const SQLWCHAR* MDBPath, const SQLWCHAR* User, const SQLWCHAR* Pass, bool Exclusive);
        /// <summary>
        /// To disconnect current active database.
        /// </summary>
        /// <returns>Does not return any value.</returns>
        void (*m_disconnect)();
        /// <summary>
        /// To verify if active database is still connected.
        /// </summary>
        /// <returns>Return false if connection is dead from last attempt query or other value.</returns>
        SQLINTEGER (*m_status)();
        /// <summary>
        /// To check an active database has up-to-date tables, if not it will automate update the tables.
        /// </summary>
        /// <returns>Does not return any value.</returns>
        void (*m_check)();
    } IDatabase;
    dllport IDatabase* getIDatabase(unsigned int hash);
#endif
#ifdef EXT_IDATABASESTATEMENT
    typedef struct IDBStmt IDBStmt;
    struct IDBStmt {
        /// <summary>
        /// To release a statement, cannot be re-used.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Does not return any value.</returns>
        void (*m_release)(IDBStmt* self);
        /// <summary>
        /// To verify if Statement is valid.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_is_valid)(IDBStmt* self);
        /// <summary>
        /// Get total of column count after query executed.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return 0 for no column or the amount of column(s).</returns>
        SQLSMALLINT (*m_get_column_count)(IDBStmt* self);
        /// <summary>
        /// Get total of changed row count after query executed.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return 0 for no changed or the amount of changed row(s).</returns>
        SQLINTEGER (*m_get_changed_row_count)(IDBStmt* self);
        /// <summary>
        /// To query a statement to database handler.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name=""></param>
        /// <returns>Return true or false.</returns>
        bool (*m_query)(IDBStmt* self, const SQLWCHAR* strSQL);
        /// <summary>
        /// Fetch existing data after queried to database statement.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_fetch)(IDBStmt* self);
        /// <summary>
        /// Fetch data from specific row after queried to database statement.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="nRow">Row number to get data from.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_fetch_row)(IDBStmt* self, SQLUSMALLINT nRow);
        /// <summary>
        /// Fetch data from previous queried row.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_fetch_previous)(IDBStmt* self);
        /// <summary>
        /// Fetch data from next queried row.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_fetch_next)(IDBStmt* self);
        /// <summary>
        /// Fetch data from specific queried absolute row. //Default: ULONG nRow, bool Absolute = 1
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="nRow">Row number to get data from.</param>
        /// <param name="Absolute">Usually set to 1.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_fetch_row_ar)(IDBStmt* self, SQLINTEGER nRow, bool Absolute);
        /// <summary>
        /// Fetch data from first row.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_fetch_first)(IDBStmt* self);
        /// <summary>
        /// Fetch data from last row.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_fetch_last)(IDBStmt* self);
        /// <summary>
        /// Cancel executed statement.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_cancel)(IDBStmt* self);
        /// <summary>
        /// To bind column's data. //Default: unsigned short Column, LPVOID pBuffer, ULONG pBufferSize, LONG* pReturnedBufferSize = NULL, unsigned short nType = SQL_C_TCHAR
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="Column">Column's number.</param>
        /// <param name="pBuffer">Input a buffer to be written to.</param>
        /// <param name="pBufLen">Size of a buffer limitation.</param>
        /// <param name="dataLen">Output size of data.</param>
        /// <param name="Type">Column type of data.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_bind_column)(IDBStmt* self, SQLUSMALLINT Column, SQLPOINTER pBuffer, SQLINTEGER pBufferSize, SQLINTEGER* dataLen, SQLUSMALLINT nType);
        /// <summary>
        /// To find column number of column's name if existed.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="Column">Column's name to find.</param>
        /// <returns>Return 0 if cannot find column's name or colum's number.</returns>
        SQLUSMALLINT (*m_get_column_by_name)(IDBStmt* self, SQLWCHAR* Column);
        /// <summary>
        /// To get data from fetched query statement. //Default: unsigned short Column; LPVOID pBuffer; ULONG pBufLen; LONG* dataLen = NULL; int Type = SQL_C_DEFAULT;
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="Column">Column's number.</param>
        /// <param name="pBuffer">Input a buffer to be written to.</param>
        /// <param name="pBufLen">Size of a buffer limitation.</param>
        /// <param name="dataLen">Output size of data.</param>
        /// <param name="Type">Column type of data.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_get_data)(IDBStmt* self, SQLUSMALLINT Column, SQLPOINTER pBuffer, SQLINTEGER pBufLen, SQLINTEGER* dataLen, SQLSMALLINT Type);
        /// <summary>
        /// Get column type.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="Column">Column's number.</param>
        /// <returns>Return type of column.</returns>
        SQLSMALLINT (*m_get_column_type)(IDBStmt* self, SQLUSMALLINT Column);
        /// <summary>
        /// Get column size.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="Column">Column's number.</param>
        /// <returns>Return size of column.</returns>
        SQLUINTEGER (*m_get_column_size)(IDBStmt* self, SQLUSMALLINT Column);
        /// <summary>
        /// Get column scale.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="Column">Column's number.</param>
        /// <returns>Return scale of column.</returns>
        SQLSMALLINT (*m_get_column_scale)(IDBStmt* self, SQLUSMALLINT Column);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="Column">Column's number.</param>
        /// <param name="Name">Name of the column.</param>
        /// <param name="NameLen">Length of the column's name.</param>
        /// <returns>Return true or false.</returns>
        bool (*m_get_column_name)(IDBStmt* self, SQLUSMALLINT Column, SQLWCHAR* Name, SQLSMALLINT NameLen);
        /// <summary>
        /// To see if column is nullable or not.
        /// </summary>
        /// <param name="self">Must include pointer of existing IDBStmt variable.</param>
        /// <param name="Column">Column's number.</param>
        /// <returns>Return true if is nullable or false.</returns>
        bool (*m_is_column_nullable)(IDBStmt* self, SQLUSMALLINT Column);
    };
    dllport IDBStmt* getIDBStmt(unsigned int hash);
#endif
#ifdef __cplusplus
}
#endif
#endif