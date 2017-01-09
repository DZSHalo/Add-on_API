module D.database;

import Add_on_API;

static if (__traits(compiles, EXT_IDATABASE) || __traits(compiles, EXT_IDATABASESTATEMENT)) {

pragma(lib, "odbc32.lib");
pragma(lib, "odbccp32.lib");
alias SQLWCHAR = wchar;
import etc.c.odbc.sql;
public import etc.c.odbc.sqlucode;
public import etc.c.odbc.sqlext;

static if (!__traits(compiles, SQLConfigDataSourceW)) {

    // SQLConfigDataSource request flags
    enum  ODBC_ADD_DSN     = 1;               // Add data source
    enum  ODBC_CONFIG_DSN  = 2;               // Configure (edit) data source
    enum  ODBC_REMOVE_DSN  = 3;               // Remove data source
    extern (Windows) int SQLConfigDataSourceW(void*       hwndParent,
                                                ushort       fRequest,
                                                wchar*     lpszDriver,
                                                wchar*     lpszAttributes);
}

    auto IS_SQL_OK(  ARG1 )(ARG1 res) { return (res==SQL_SUCCESS_WITH_INFO || res==SQL_SUCCESS); }
    auto IS_SQL_ERR( ARG1 )(ARG1 res) { return !IS_SQL_OK(res); }
    auto SAFE_STR(  ARG1 )(ARG1 str) { return ((str is null) ? ""w : str); }
static if (__traits(compiles, EXT_IDATABASE)) {
    extern(C) struct IDatabase {
        /*
         * To connect a supported database.
         * Params:
         * ConnectionStr = See https://msdn.microsoft.com/en-us/library/ms715433.aspx for details.</param>
         * Option = See Attribute at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
         * Param = See ValuePtr at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
         * ParamLen = See StringLength at https://msdn.microsoft.com/en-us/library/ms713605.aspx for details.</param>
         * Returns: Return true or false.
         */
        bool function(SQLWCHAR* ConnectionStr, SQLINTEGER Option, SQLPOINTER Param, SQLINTEGER ParamLen) m_connect;
        /*
         * To connect local MDB file database. (Please note this function call to m_connect function.)
         * Params:
         * MDBPath = Path to local MDB file database.</param>
         * User = Username if required to connect.</param>
         * Pass = Password if required to connect.</param>
         * Exclusive = True to restrict access to database or false to share database.</param>
         * Returns: Return true or false.
         */
        bool function(const SQLWCHAR* MDBPath, const SQLWCHAR* User, const SQLWCHAR* Pass, bool Exclusive) m_connect_mdb;
        /*
         * To disconnect current active database.
         * Params:
         * Returns: Does not return any value.
         */
        void function() m_disconnect;
        /*
         * To verify if active database is still connected.
         * Params:
         * Returns: Return false if connection is dead from last attempt query or other value.
         */
        SQLINTEGER function() m_status;
        /*
         * To check an active database has up-to-date tables, if not it will automate update the tables.
         * Params:
         * Returns: Does not return any value.
         */
        void function() m_check;
}
    export extern(C) IDatabase* getIDatabase(uint hash);
}
//--
static if (__traits(compiles, EXT_IDATABASESTATEMENT)) {
    extern(C) struct IDBStmt {
        /*
         * To release a statement, cannot be re-used.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Does not return any value.
         */
        void function(IDBStmt* self) m_release;
        /*
         * To verify if Statement is valid.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self) m_is_valid;
        /*
         * Get total of column count after query executed.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return 0 for no column or the amount of column(s).
         */
        short function(IDBStmt* self) m_get_column_count;
        /*
         * Get total of changed row count after query executed.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return 0 for no changed or the amount of changed row(s).
         */
        int function(IDBStmt* self) m_get_changed_row_count;
        /*
         * To query a statement to database handler.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self, SQLWCHAR* strSQL) m_query;
        /*
         * Fetch existing data after queried to database statement.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self) m_fetch;
        /*
         * Fetch data from specific row after queried to database statement.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * nRow = Row number to get data from.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self, SQLUSMALLINT nRow) m_fetch_row;
        /*
         * Fetch data from previous queried row.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self) m_fetch_previous;
        /*
         * Fetch data from next queried row.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self) m_fetch_next;
        /*
         * Fetch data from specific queried absolute row. //Default: ULONG nRow, bool Absolute = 1
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * nRow = Row number to get data from.
         * Absolute = Usually set to 1.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self, SQLUSMALLINT nRow, bool Absolute) m_fetch_row_ar;
        /*
         * Fetch data from first row.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self) m_fetch_first;
        /*
         * Fetch data from last row.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self) m_fetch_last;
        /*
         * Cancel executed statement.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self) m_cancel;
        /*
         * To bind column's data. //Default: unsigned short Column, LPVOID pBuffer, ULONG pBufferSize, LONG* pReturnedBufferSize = null, unsigned short nType = SQL_C_TCHAR
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Column = Column's number.
         * pBuffer = Input a buffer to be written to.
         * pBufLen = Size of a buffer limitation.
         * dataLen = Output size of data.
         * Type = Column type of data.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self, SQLUSMALLINT Column, SQLPOINTER pBuffer, SQLINTEGER pBufferSize, SQLINTEGER* dataLen, SQLUSMALLINT nType) m_bind_column;
        /*
         * To find column number of column's name if existed.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Column = Column's name to find.
         * Returns: Return 0 if cannot find column's name or colum's number.
         */
        SQLUSMALLINT function(IDBStmt* self, SQLWCHAR* Column) m_get_column_by_name;
        /*
         * To get data from fetched query statement. //Default unsigned short Column; LPVOID pBuffer; ULONG pBufLen; LONG* dataLen = null; int Type = SQL_C_DEFAULT;
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Column = Column's number.
         * pBuffer = Input a buffer to be written to.
         * pBufLen = Size of a buffer limitation.
         * dataLen = Output size of data.
         * Type = Column type of data.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self, SQLUSMALLINT Column, SQLPOINTER pBuffer, SQLINTEGER pBufLen, SQLINTEGER* dataLen, SQLSMALLINT Type) m_get_data;
        /*
         * Get column type.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Column = Column's number.
         * Returns: Return type of column.
         */
        SQLSMALLINT function(IDBStmt* self, SQLUSMALLINT Column) m_get_column_type;
        /*
         * Get column size.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Column = Column's number.
         * Returns: Return size of column.
         */
        SQLUINTEGER function(IDBStmt* self, SQLUSMALLINT Column) m_get_column_size;
        /*
         * Get column scale.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Column = Column's number.
         * Returns: Return scale of column.
         */
        uint function(IDBStmt* self, SQLUSMALLINT Column) m_get_column_scale;
        /*
         * 
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Column = Column's number.
         * Name = Name of the column.
         * NameLen = Length of the column's name.
         * Returns: Return true or false.
         */
        bool function(IDBStmt* self, SQLUSMALLINT Column, SQLWCHAR* Name, SQLSMALLINT NameLen) m_get_column_name;
        /*
         * To see if column is nullable or not.
         * Params:
         * self = Must include pointer of existing IDBStmt variable.
         * Column = Column's number.
         * Returns: Return true if is nullable or false.
         */
        bool function(IDBStmt* self, SQLUSMALLINT Column) m_is_column_nullable;
    }
    export extern(C) IDBStmt* getIDBStmt(uint hash);
}
}
