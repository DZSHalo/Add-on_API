module Add_on_API.D.util;

import Add_on_API.Add_on_API;

struct tm {
    int tm_sec;     /* seconds after the minute - [0,59] */
    int tm_min;     /* minutes after the hour - [0,59] */
    int tm_hour;    /* hours since midnight - [0,23] */
    int tm_mday;    /* day of the month - [1,31] */
    int tm_mon;     /* months since January - [0,11] */
    int tm_year;    /* years since 1900 */
    int tm_wday;    /* days since Sunday - [0,6] */
    int tm_yday;    /* days since January 1 - [0,365] */
    int tm_isdst;   /* daylight savings time flag */
};
static if(__traits(compiles, EXT_IUTIL)) {

// #define _TM_DEFINED

    struct ArgContainerVars {
        wchar * args[10];
        uint argc;
        //export extern (C) this();
          /* SYNTAX ERROR: (31): expected ; instead of operator */ 
    };
    extern(C) {
        export void ArgContainerVars_Constructor(ref ArgContainerVars vars);
        export void ArgContainerVars_Deconstructor(ref ArgContainerVars vars);
        export void ArgContainerVars_Set(ref ArgContainerVars vars, const wchar* arg);
        export void ArgContainerVars_Set_N(ref ArgContainerVars vars, const wchar* arg, int numArrayLink);
        export void ArgContainerVars_Copy(ref ArgContainerVars vars, ref const ArgContainerVars copy);
        export wchar* ArgContainerVars_At(ref ArgContainerVars vars, uint i);
    }
    struct ArgContainer {
        public:
        ArgContainerVars vars;
        void init() {
            ArgContainerVars_Constructor(vars);
        }
        this(const wchar* arg) {
            ArgContainerVars_Set(vars, arg);
        }
        this(const wchar* arg, int numArrayLink) {
            ArgContainerVars_Set_N(vars, arg, numArrayLink);
        }
        wchar* opIndex(uint i) {
            return ArgContainerVars_At(this.vars, i);
        }
        void opAssign(ArgContainer copy) {
            ArgContainerVars_Copy(vars, copy.vars);
        }
        ~this() {
            ArgContainerVars_Deconstructor(this.vars);
        }
    };

    extern (C) struct IUtil {
        /*
         * Allocate memory.
         * Params:
         * Size = The size of allocate memory need to be used.
         * Returns: Return allocate memory.
         */
        void* function(uint Size) AllocMem;
        /*
         * Free memory from allocate memory.
         * Params:
         * Address = Pointer of an allocate memory to be free from.
         * Returns: No return value.
         */
        void function(void* Address) FreeMem;
        /*
         * Convert a string to wide string.
         * Params:
         * charA = String
         * len = Total length to convert from string.
         * charW = Buffered wide string
         * Returns: No return value.
         */
        void function(const char* charA, int len, wchar* charW) toCharW;
        /*
         * Convert a wide string to string.
         * Params:
         * charW = Wide string
         * len = Total length to convert from wide string.
         * charA = Buffered string
         * Returns: No return value.
         */
        void function(const wchar* charW, int len, char* charA) toCharA;
        /*
         * Translate a string into boolean.
         * Params:
         * str = String to translate from.
         * Returns: Return -1 if string doesn't have a translation to boolean.
         */
        e_boolean function(const char* str) StrToBooleanA;
        /*
         * Translate a wide string into boolean.
         * Params:
         * str = Wide string to translate from.
         * Returns: Return -1 if string doesn't have a translation to boolean.
         */
        e_boolean function(const wchar* str) StrToBooleanW;
        /*
         * Translate a string into team index.
         * Params:
         * str = String to translate from.
         * Returns: Return -1 if string doesn't have a translation to team index.
         */
        e_color_team_index function(const char* str) StrToTeamA;
        /*
         * Translate a wide string into team index.
         * Params:
         * str = Wide string to translate from.
         * Returns: Return -1 if string doesn't have a translation to team index.
         */
        e_color_team_index function(const wchar* str) StrToTeamW;
        /*
         * Format a current string to support escape characters if any.
         * Params:
         * regStr = String to format escape characters if any.
         * Returns: No return value.
         */
        void function(char* regStr) ReplaceA;
        /*
         * Format a current string to support escape characters if any.
         * Params:
         * regStr = String to format escape characters if any.
         * Returns: No return value.
         */
        void function(wchar* regStr) ReplaceW;
        /*
         * Undo format a current string to support escape characters if any.
         * Params:
         * regStr = String to undo format escape characters if any.
         * Returns: No return value.
         */
        void function(char* regStr) ReplaceUndoA;
        /*
         * Undo format a current string to support escape characters if any.
         * Params:
         * regStr = String to undo format escape characters if any.
         * Returns: No return value.
         */
        void function(wchar* regStr) ReplaceUndoW;
        /*
         * Verify if whole string contain digits.
         * Params:
         * str = String to check.
         * Returns: Return true if valid.
         */
        bool function(const char* str) isnumberA;
        /*
         * Verify if whole wide string contain digits.
         * Params:
         * str = Wide string to check.
         * Returns: Return true if valid.
         */
        bool function(const wchar* str) isnumberW;
        /*
         * Verify if whole string contain characters & digits.
         * Params:
         * str = String to check.
         * Returns: Return true if valid.
         */
        bool function(const char* str) ishashA;
        /*
         * Verify if whole wide string contain characters & digits.
         * Params:
         * str = Wide string to check.
         * Returns: Return true if valid.
         */
        bool function(const wchar* str) ishashW;
        /*
         * Move partial of string to left or right.
         * Params:
         * regStr = String to be shift.
         * len = Length of string to be move.
         * pos = Position of the string to be shift.
         * lenShift = Amount of length to shift left or right.
         * leftRight = True for shift to right and false for shift to left.
         * Returns: Return true for success, failed if one or more argument is invalid.
         */
        e_boolean function(char* regStr, int len, int pos, int lenShift, bool leftRight) shiftStrA;
        /*
         * Move partial of wide string to left or right.
         * Params:
         * regStr = Wide string to be shift.
         * len = Length of wide string to be move.
         * pos = Position of the wide string to be shift.
         * lenShift = Amount of length to shift left or right.
         * leftRight = True for shift to right and false for shift to left.
         * Returns: Return true for success, failed if one or more argument is invalid.
         */
        e_boolean function(wchar* regStr, int len, int pos, int lenShift, bool leftRight) shiftStrW;
        /*
         * Format a current string to support escape characters if any.
         * Params:
         * regStr = String to format escape characters if any.
         * isDB = True if goig to use escape characters in database query.
         * Returns: No return value.
         */
        void function(wchar* regStr, bool isDB) regexReplaceW;
        /*
         * Find a regular expression string against source string to be a match.
         * Params:
         * srcStr = Source string
         * regex = Regular expression string
         * Returns: Return true if is a match.
         */
        bool function(wchar* srcStr, wchar* regex) regexMatchW;
        /*
         * Find a regular expression string against source string to be a match.
         * Params:
         * srcStr = Source string
         * regex = Regular expression string
         * Returns: Return true if is a match.
         */
        bool function(wchar* srcStr, wchar* regex) regexiMatchW;

        /*
         * Format variable arguments list into given prefix string.
         * Params:
         * writeTo = Output string
         * _Format = Format message string
         * ArgList = Variable arguments list
         * Returns: Return true or false for format completion.
         */
        bool function(char* writeTo, const char* _Format, char * ArgList) FormatVarArgsListA;
        /*
         * Format variable arguments list into given prefix string.
         * Params:
         * writeTo = Output string
         * _Format = Format message string
         * ArgList = Variable arguments list
         * Returns: Return true or false for format completion.
         */
        bool function(wchar* writeTo, const wchar* _Format, char * ArgList) FormatVarArgsListW;
        /*
         * Compare beginning of case-senitive string against another string.
         * Params:
         * str1 = Beginning of string #1 to compare against.
         * str2 = String #2 to compare against.
         * Returns: Only return true if is a match.
         */
        bool function(const char* dest, const char* src) findSubStrFirstA;
        /*
         * Compare beginning of case-senitive string against another string.
         * Params:
         * str1 = Beginning of string #1 to compare against.
         * str2 = String #2 to compare against.
         * Returns: Only return true if is a match.
         */
        bool function(const wchar* dest, const wchar* src) findSubStrFirstW;

        /*
         * Test if string contains a letters or not.
         * Params:
         * str = String to test if is a letters.
         * Returns: Return true if is letters.
         */
        bool function(const char* str) islettersA;
        /*
         * Test if string contains a letters or not.
         * Params:
         * str = String to test if is a letters.
         * Returns: Return true if is letters.
         */
        bool function(const wchar* str) islettersW;

        /*
         * Test if string contains a float or not.
         * Params:
         * str = String to test if is a float.
         * Returns: Return true if is a float.
         */
        bool function(const char* str) isfloatA;
        /*
         * Test if string contains a float or not.
         * Params:
         * str = String to test if is a float.
         * Returns: Return true if is a float.
         */
        bool function(const wchar* str) isfloatW;
        /*
         * Test if string contains a double or not.
         * Params:
         * str = String to test if is a double.
         * Returns: Return true if is a double.
         */
        bool function(const char* str) isdoubleA;
        /*
         * Test if string contains a double or not.
         * Params:
         * str = String to test if is a double.
         * Returns: Return true if is a double.
         */
        bool function(const wchar* str) isdoubleW;
        /*
         * Append an existing string with new string.
         * Params:
         * dest = Destination to write an existing string.
         * len = Maximum size of an dest string.
         * src = New string to copy from.
         * Returns: Return 1 every time.
         */
        int function(wchar* dest, size_t len, const wchar* src) strcatW;
        /*
         * Append an existing string with new string.
         * Params:
         * dest = Destination to write an existing string.
         * len = Maximum size of an dest string.
         * src = New string to copy from.
         * Returns: Return 1 every time.
         */
        int function(char* dest, size_t len, const char* src) strcatA;
        /*
         * Convert a string to wide string.
         * Params:
         * str = String
         * wstr = Buffered wide string./param>
         * Returns: No return value.
         */
        void function(char* str, wchar* wstr) str_to_wstr;
        /*
         * Case-senitive string to compare against another string..
         * Params:
         * str1 = String #1 to compare against.
         * str2 = String #2 to compare against.
         * Returns: Only return true if is a match.
         */
        bool function(const wchar* str1, const wchar* str2) strcmpW;
        /*
         * Case-senitive string to compare against another string..
         * Params:
         * str1 = String #1 to compare against.
         * str2 = String #2 to compare against.
         * Returns: Only return true if is a match.
         */
        bool function(const char* str1, const char* str2) strcmpA;
        /*
         * Case-insenitive string to compare against another string.
         * Params:
         * str1 = String #1 to compare against.
         * str2 = String #2 to compare against.
         * Returns: Only return true if is a match.
         */
        bool function(const wchar* str1, const wchar* str2) stricmpW;
        /*
         * Case-insenitive string to compare against another string.
         * Params:
         * str1 = String #1 to compare against.
         * str2 = String #2 to compare against.
         * Returns: Only return true if is a match.
         */
        bool function(const char* str1, const char* str2) stricmpA;
        /*
         * Check if a directory exist.
         * Params:
         * pathStr = Must have directory name.
         * errorCode = Given error code if failed.
         * Returns: Return true if directory exist, false with given errorCode.
         */
        bool function(const wchar* str, uint* errorCode) isDirExist;
        /*
         * Check if a file exist.
         * Params:
         * pathStr = Must have directory (optional) and file name.
         * errorCode = Given error code if failed.
         * Returns: Return true if file exist, false with given errorCode.
         */
        bool function(const wchar* str, uint* errorCode) isFileExist;
        /*
         * Format variable arguments into given prefix string.
         * Params:
         * writeTo = Output string
         * _Format = Format message string
         * ... = Variable arguments
         * Returns: Return true or false for format completion.
        */
        bool function(char* writeTo, const char* _Format, ...) FormatVarArgsA;
        /*
         * Format variable arguments into given prefix string.
         * Params:
         * writeTo = Output string
         * _Format = Format message string
         * ... = Variable arguments
         * Returns: Return true or false for format completion.
         */
        bool function(wchar* writeTo, const wchar* _Format, ...) FormatVarArgsW;
    };
    extern (C) IUtil* getIUtil(uint hash);
    __gshared IUtil* pIUtil;

    struct haloConsole {
        int r;
        char output[1024];
    };

    //TODO: Untested dynamicStack...
    class d_stack(T) {
        T* d_type;
        d_stack!(T)* next_d_type;
        void init(T) {
            d_type = null;
            next_d_type = null;
        }
    };
    class dynamicStack(T) : d_stack!(T) {
        private:
        d_stack!(T)* stack = null;
        public:
        /*void init(T)() {
            stack = 0;
        }*/
        ~this() {
            this.clear();
        }
        void push_back(T)(T type) {
            if (!stack) {
                stack = cast(d_stack!(T)*)pIUtil.AllocMem(d_stack!(T).sizeof);
                stack.d_type = cast(T*)pIUtil.AllocMem(T.sizeof);
                *stack.d_type = type;
                stack.next_d_type = null;
            } else {
                d_stack!(T)* src = stack;
                while (src.next_d_type) {
                    src = src.next_d_type;
                }
                d_stack!(T)* newSrc = cast(d_stack!(T)*)pIUtil.AllocMem(d_stack!(T).sizeof);
                newSrc.d_type = cast(T*)pIUtil.AllocMem(T.sizeof);
                *newSrc.d_type = type;
                newSrc.next_d_type = null;
                src.next_d_type = newSrc;
            }
        }
        void pop_back() {
            if (stack) {
                d_stack!(T)* src = stack;
                d_stack!(T)* nextSrc = stack.next_d_type;
                pIUtil.FreeMem(stack.d_type);
                pIUtil.FreeMem(cast(void*)stack);
                stack = nextSrc;
            }
        }
        bool remove(uint i) {
            bool isDel = 1;
            d_stack!(T)* src = stack;
            d_stack!(T)* prevSrc = null;
            while (i>0) {
                if (!is(src ==typeof(null)) && !is(src.next_d_type ==typeof(null))) {
                    prevSrc = src;
                    src = src.next_d_type;
                }
                else {
                    isDel = 0;
                    break;
                }
                i--;
            }
            if (isDel) {
                if (!is(prevSrc ==typeof(null))) {
                    prevSrc.next_d_type = src.next_d_type;
                }
                else
                    stack = src.next_d_type;
                pIUtil.FreeMem(cast(void*)src.d_type);
                pIUtil.FreeMem(cast(void*)src);
            }
            return isDel;
        }
        T* opIndex(size_t i) {
            d_stack!(T)* src = stack;
            while (i>0) {
                if (src && src.next_d_type ) {
                    src = src.next_d_type;
                }
                else {
                    src = null;
                    break;
                }
                i--;
            }
            if (!is(src ==typeof(null)))
                return src.d_type;
            else
                return null;
        }
        void clear() {
            if (stack) {
                d_stack!(T)* src = stack;
                d_stack!(T)* holder = null;
                while (src) {
                    if (src.d_type)
                        pIUtil.FreeMem(src.d_type);
                    holder = src;
                    src = src.next_d_type;
                    pIUtil.FreeMem(holder);
                }
                stack = null;
            }
        }


        /*
        iterator begin() {
        if (stack)
        return iterator(stack);
        else
        return iterator(0);
        }
        iterator end() {
        d_stack* src = stack;
        while (src && src->next_d_type) {
        src = stack->next_d_type;
        }
        return iterator(src);
        }
        /*friend T* erase(T* data) {
        d_stack* src = stack;
        d_stack* prevSrc = 0;
        //stack = stack->next_d_type;
        while(src->next_d_type) {
        if (data==src->d_type) {
        prevSrc->next_d_type = src->next_d_type;
        pIUtil->FreeMem(src->d_type);
        pIUtil->FreeMem(src);
        return prevSrc->d_type;
        }
        prevSrc = src;
        src = src->next_d_type;
        }
        return (T*)0;
        }* /
        iterator erase(iterator data) {
            d_stack* src = stack;
            d_stack* prevSrc = 0;
            while (src) {
                if (data.ptr_->d_type == src->d_type) {
                    if (prevSrc) {
                        prevSrc->next_d_type = src->next_d_type;
                    }
                    else
                        stack = prevSrc = src->next_d_type;
                    pIUtil->FreeMem(src->d_type);
                    pIUtil->FreeMem(src);
                    return prevSrc;
                }
                prevSrc = src;
                src = src->next_d_type;
            }
            return (iterator)0;
        }
        size_t size() {
            size_t count = 0;
            d_stack* src = stack;
            while (src) {
                count++;
                src = src->next_d_type;
            }
            return count;
        }
        T at(size_t offset) {
            bool doRet = 1;
            d_stack* src = stack;
            for (offset; offset>0; offset--) {
                if (src) {
                    src = src->next_d_type;
                }
                else {
                    doRet = 0;
                    break;
                }
            }
            if (doRet) {
                return *src->d_type;
            }
            throw 1;
        }
        void insert(iterator iter, T newData) {
            d_stack* insert;
            if (iter != 0) {
                insert = (d_stack*)pIUtil->AllocMem(sizeof(d_stack));
                insert->d_type = (T*)pIUtil->AllocMem(sizeof(T));
                *insert->d_type = newData;
                insert->next_d_type = iter.ptr_->next_d_type;
                iter.ptr_->next_d_type = insert;

            }
            else {
                d_stack* src = stack;
                if (!src) {
                    stack = (d_stack*)pIUtil->AllocMem(sizeof(d_stack));
                    stack->d_type = (T*)pIUtil->AllocMem(sizeof(T));
                    *stack->d_type = newData;
                    stack->next_d_type = 0;
                    return;
                }
                while (src) {
                    if (src->next_d_type == iter.ptr_) {
                        break;
                    }
                    src = src->next_d_type;
                }
                if (!src)
                    src = stack;
                insert = (d_stack*)pIUtil->AllocMem(sizeof(d_stack));
                insert->d_type = (T*)pIUtil->AllocMem(sizeof(T));
                *insert->d_type = newData;
                insert->next_d_type = src->next_d_type;
                src->next_d_type = insert;
            }
        }
* /
        class iterator {
        private:
            d_stack!(T)* ptr_;
        public:
            typedef iterator self_type;
            typedef T value_type;
            typedef T& reference;
            typedef T* pointer;
            typedef int difference_type;
            iterator() : ptr_(0) {}
            iterator(d_stack* ptr) : ptr_(ptr) {}
            self_type operator++() { //self_type i = *this; ptr_++; return i; 
                d_stack* src = this->ptr_;
                if (src)
                    this->ptr_ = src->next_d_type;
                    return *this;
                }
            self_type operator++(int junk) { //ptr_++; return *this;
                d_stack* src = this->ptr_;
                if (src)
                    this->ptr_ = src->next_d_type;
                return *this;
            }
            self_type operator+(int append) { //ptr_++; return *this;
                d_stack* src = this->ptr_;
                for (append; append>0; append--) {
                    if (src) {
                        src = src->next_d_type;
                    }
                    else {
                        break;
                    }
                }
                if (src) {
                    this->ptr_ = src;
                }
                else
                this->ptr_ = 0;
                return *this;
            }
            reference operator*() { return *(ptr_->d_type); }
            pointer operator->() { return ptr_->d_type; }
            bool operator==(const reference rhs) { return ptr_ == rhs; }
            bool operator==(const self_type& rhs) { return ptr_ == rhs.ptr_; }
            bool operator!=(const self_type& rhs) { return ptr_ != rhs.ptr_; }

            friend iterator dynamicStack::erase(iterator data);
            friend void dynamicStack::insert(iterator iter, T newData);
        };
        class const_iterator {
        public:
            typedef const_iterator self_type;
            typedef T value_type;
            typedef T& reference;
            typedef T* pointer;
            typedef int difference_type;
        private:
            pointer ptr_;
        public:
            const_iterator(pointer ptr) : ptr_(ptr) {}
            self_type operator++() { //self_type i = *this; ptr_++; return i; 
                d_stack* src = this->ptr_;
                if (src)
                    this->ptr_ = src->next_d_type;
                return *this;
            }
            self_type operator++(int junk) { //ptr_++; return *this;
                d_stack* src = this->ptr_;
                if (src)
                    this->ptr_ = src->next_d_type;
                return *this;
            }
            const reference operator*() { return *ptr_; }
            const pointer operator->() { return ptr_; }
            bool operator==(const self_type& rhs) { return ptr_ == rhs.ptr_; }
            bool operator!=(const self_type& rhs) { return ptr_ != rhs.ptr_; }

        };
        */
    };
// #pragma pack(pop)


}
