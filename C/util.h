#ifndef utilH
#define utilH

#define WINAPIC     __cdecl

#ifndef _INC_TIME
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
#define _TM_DEFINED
#endif
#pragma pack(push,1)
    struct haloConsole {
        int r;
        char output[1024];
    };
#pragma pack(pop)
#ifdef EXT_IUTIL

#ifdef __cplusplus
CNATIVE {
#endif
    /*typedef struct ArgContainer {
        wchar_t* args[10];
        size_t argc;
        ArgContainer();
        //dllport ArgContainer(const ArgContainer&);
        //ArgContainer(const ArgContainer&);
        ArgContainer(const wchar_t arg[]);
        ArgContainer(const wchar_t arg[], int numArrayLink);
        ArgContainer(ArgContainer const &copy);
        wchar_t* operator[](size_t i);
        ArgContainer& operator=(ArgContainer const &copy);
        //ArgContainer& operator=(ArgContainer &copy);
        //ArgContainer(wchar_t arg[]);
        //wchar_t* operator[](size_t i);
        ArgContainer operator*();
        ~ArgContainer();
    } ArgContainer;*/
    typedef struct ArgContainerVars {
        wchar_t* args[10];
        size_t argc;
    } ArgContainerVars;
    dllport void ArgContainerVars_Constructor(ArgContainerVars& vars);
    dllport void ArgContainerVars_Deconstructor(ArgContainerVars& vars);
    dllport void ArgContainerVars_Set(ArgContainerVars& vars, const wchar_t* arg);
    dllport void ArgContainerVars_Set_N(ArgContainerVars& vars, const wchar_t* arg, int numArrayLink);
    dllport void ArgContainerVars_Copy(ArgContainerVars& vars, const ArgContainerVars& copy);
    dllport wchar_t* ArgContainerVars_At(ArgContainerVars& vars, unsigned int i);
    typedef struct IUtil {
        /// <summary>
        /// Allocate memory.
        /// </summary>
        /// <param name="Size">The size of allocate memory need to be used.</param>
        /// <returns>Return allocate memory.</returns>
        void* (*AllocMem)(unsigned int Size);
        /// <summary>
        /// Free memory from allocate memory.
        /// </summary>
        /// <param name="Address">Pointer of an allocate memory to be free from.</param>
        /// <returns>No return value.</returns>
        void (*FreeMem)(void* Address);
        /// <summary>
        /// Convert a string to wide string.
        /// </summary>
        /// <param name="charA">String</param>
        /// <param name="len">Total length to convert from string.</param>
        /// <param name="charW">Buffered wide string</param>
        /// <returns>No return value.</returns>
        void (*toCharW)(const char* charA, int len, wchar_t* charW);
        /// <summary>
        /// Convert a wide string to string.
        /// </summary>
        /// <param name="charW">Wide string</param>
        /// <param name="len">Total length to convert from wide string.</param>
        /// <param name="charA">Buffered string</param>
        /// <returns>No return value.</returns>
        void (*toCharA)(const wchar_t* charW, int len, char* charA);
        /// <summary>
        /// Translate a string into boolean.
        /// </summary>
        /// <param name="str">String to translate from.</param>
        /// <returns>Return -1 if string doesn't have a translation to boolean.</returns>
        e_boolean (*StrToBooleanA)(const char* str);
        /// <summary>
        /// Translate a wide string into boolean.
        /// </summary>
        /// <param name="str">Wide string to translate from.</param>
        /// <returns>Return -1 if string doesn't have a translation to boolean.</returns>
        e_boolean (*StrToBooleanW)(const wchar_t* str);
        /// <summary>
        /// Translate a string into team index.
        /// </summary>
        /// <param name="str">String to translate from.</param>
        /// <returns>Return -1 if string doesn't have a translation to team index.</returns>
        e_color_team_index (*StrToTeamA)(const char* str);
        /// <summary>
        /// Translate a wide string into team index.
        /// </summary>
        /// <param name="str">Wide string to translate from.</param>
        /// <returns>Return -1 if string doesn't have a translation to team index.</returns>
        e_color_team_index (*StrToTeamW)(const wchar_t* str);
        /// <summary>
        /// Format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to format escape characters if any.</param>
        /// <returns>No return value.</returns>
        void (*ReplaceA)(char* regStr);
        /// <summary>
        /// Format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to format escape characters if any.</param>
        /// <returns>No return value.</returns>
        void (*ReplaceW)(wchar_t* regStr);
        /// <summary>
        /// Undo format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to undo format escape characters if any.</param>
        /// <returns>No return value.</returns>
        void (*ReplaceUndoA)(char* regStr);
        /// <summary>
        /// Undo format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to undo format escape characters if any.</param>
        /// <returns>No return value.</returns>
        void (*ReplaceUndoW)(wchar_t* regStr);
        /// <summary>
        /// Verify if whole string contain digits.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>Return true if valid.</returns>
        bool (*isnumberA)(const char* str);
        /// <summary>
        /// Verify if whole wide string contain digits.
        /// </summary>
        /// <param name="str">Wide string to check.</param>
        /// <returns>Return true if valid.</returns>
        bool (*isnumberW)(const wchar_t* str);
        /// <summary>
        /// Verify if whole string contain characters & digits.
        /// </summary>
        /// <param name="str">String to check.</param>
        /// <returns>Return true if valid.</returns>
        bool (*ishashA)(const char* str);
        /// <summary>
        /// Verify if whole wide string contain characters & digits.
        /// </summary>
        /// <param name="str">Wide string to check.</param>
        /// <returns>Return true if valid.</returns>
        bool (*ishashW)(const wchar_t* str);
        /// <summary>
        /// Move partial of string to left or right.
        /// </summary>
        /// <param name="regStr">String to be shift.</param>
        /// <param name="len">Length of string to be move.</param>
        /// <param name="pos">Position of the string to be shift.</param>
        /// <param name="lenShift">Amount of length to shift left or right.</param>
        /// <param name="leftRight">True for shift to right and false for shift to left.</param>
        /// <returns>Return true for success, failed if one or more argument is invalid.</returns>
        e_boolean (*shiftStrA)(char* regStr, int len, int pos, int lenShift, bool leftRight);
        /// <summary>
        /// Move partial of wide string to left or right.
        /// </summary>
        /// <param name="regStr">Wide string to be shift.</param>
        /// <param name="len">Length of wide string to be move.</param>
        /// <param name="pos">Position of the wide string to be shift.</param>
        /// <param name="lenShift">Amount of length to shift left or right.</param>
        /// <param name="leftRight">True for shift to right and false for shift to left.</param>
        /// <returns>Return true for success, failed if one or more argument is invalid.</returns>
        e_boolean (*shiftStrW)(wchar_t* regStr, int len, int pos, int lenShift, bool leftRight);
        /// <summary>
        /// Format a current string to support escape characters if any.
        /// </summary>
        /// <param name="regStr">String to format escape characters if any.</param>
        /// <param name="isDB">True if goig to use escape characters in database query.</param>
        /// <returns>No return value.</returns>
        void (*regexReplaceW)(wchar_t* regStr, bool isDB);
        /// <summary>
        /// Find a regular expression string against source string to be a match.
        /// </summary>
        /// <param name="srcStr">Source string</param>
        /// <param name="regex">Regular expression string</param>
        /// <returns>Return true if is a match.</returns>
        bool (*regexMatchW)(wchar_t* srcStr, wchar_t* regex);
        /// <summary>
        /// Find a regular expression string against source string to be a match.
        /// </summary>
        /// <param name="srcStr">Source string</param>
        /// <param name="regex">Regular expression string</param>
        /// <returns>Return true if is a match.</returns>
        bool (*regexiMatchW)(wchar_t* srcStr, wchar_t* regex);

        /// <summary>
        /// Format variable arguments list into given prefix string.
        /// </summary>
        /// <param name="writeTo">Output string</param>
        /// <param name="_Format">Format message string</param>
        /// <param name="ArgList">Variable arguments list</param>
        /// <returns>Return true or false for format completion.</returns>
        bool (*FormatVarArgsListA)(char* writeTo, const char* _Format, char* ArgList);
        /// <summary>
        /// Format variable arguments list into given prefix string.
        /// </summary>
        /// <param name="writeTo">Output string</param>
        /// <param name="_Format">Format message string</param>
        /// <param name="ArgList">Variable arguments list</param>
        /// <returns>Return true or false for format completion.</returns>
        bool (*FormatVarArgsListW)(wchar_t* writeTo, const wchar_t* _Format, char* ArgList);
        /// <summary>
        /// Compare beginning of case-senitive string against another string.
        /// </summary>
        /// <param name="str1">Beginning of string #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        bool (*findSubStrFirstA)(const char* dest, const char* src);
        /// <summary>
        /// Compare beginning of case-senitive string against another string.
        /// </summary>
        /// <param name="str1">Beginning of string #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        bool (*findSubStrFirstW)(const wchar_t* dest, const wchar_t* src);

        /// <summary>
        /// Test if string contains a letters or not.
        /// </summary>
        /// <param name="str">String to test if is a letters.</param>
        /// <returns>Return true if is letters.</returns>
        bool (*islettersA)(const char* str);
        /// <summary>
        /// Test if string contains a letters or not.
        /// </summary>
        /// <param name="str">String to test if is a letters.</param>
        /// <returns>Return true if is letters.</returns>
        bool (*islettersW)(const wchar_t* str);

        /// <summary>
        /// Test if string contains a float or not.
        /// </summary>
        /// <param name="str">String to test if is a float.</param>
        /// <returns>Return true if is a float.</returns>
        bool (*isfloatA)(const char* str);
        /// <summary>
        /// Test if string contains a float or not.
        /// </summary>
        /// <param name="str">String to test if is a float.</param>
        /// <returns>Return true if is a float.</returns>
        bool (*isfloatW)(const wchar_t* str);
        /// <summary>
        /// Test if string contains a double or not.
        /// </summary>
        /// <param name="str">String to test if is a double.</param>
        /// <returns>Return true if is a double.</returns>
        bool (*isdoubleA)(const char* str);
        /// <summary>
        /// Test if string contains a double or not.
        /// </summary>
        /// <param name="str">String to test if is a double.</param>
        /// <returns>Return true if is a double.</returns>
        bool (*isdoubleW)(const wchar_t* str);
        /// <summary>
        /// Append an existing string with new string.
        /// </summary>
        /// <param name="dest">Destination to write an existing string.</param>
        /// <param name="len">Maximum size of an dest string.</param>
        /// <param name="src">New string to copy from.</param>
        /// <returns>Return 1 every time.</returns>
        int (*strcatW)(wchar_t* dest, size_t len, const wchar_t* src);
        /// <summary>
        /// Append an existing string with new string.
        /// </summary>
        /// <param name="dest">Destination to write an existing string.</param>
        /// <param name="len">Maximum size of an dest string.</param>
        /// <param name="src">New string to copy from.</param>
        /// <returns>Return 1 every time.</returns>
        int (*strcatA)(char *dest, size_t len, const char* src);
        /// <summary>
        /// Convert a string to wide string.
        /// </summary>
        /// <param name="str">String</param>
        /// <param name="wstr">Buffered wide string./param>
        /// <returns>No return value.</returns>
        void (*str_to_wstr)(char* str, wchar_t* wstr);
        /// <summary>
        /// Case-senitive string to compare against another string..
        /// </summary>
        /// <param name="str1">String #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        bool (*strcmpW)(const wchar_t* str1, const wchar_t* str2);
        /// <summary>
        /// Case-senitive string to compare against another string..
        /// </summary>
        /// <param name="str1">String #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        bool (*strcmpA)(const char* str1, const char* str2);
        /// <summary>
        /// Case-insenitive string to compare against another string.
        /// </summary>
        /// <param name="str1">String #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        bool (*stricmpW)(const wchar_t* str1, const wchar_t* str2);
        /// <summary>
        /// Case-insenitive string to compare against another string.
        /// </summary>
        /// <param name="str1">String #1 to compare against.</param>
        /// <param name="str2">String #2 to compare against.</param>
        /// <returns>Only return true if is a match.</returns>
        bool (*stricmpA)(const char* str1, const char* str2);
        /// <summary>
        /// Check if a directory exist.
        /// </summary>
        /// <param name="pathStr">Must have directory name.</param>
        /// <param name="errorCode">Given error code if failed.</param>
        /// <returns>Return true if directory exist, false with given errorCode.</returns>
        bool (*isDirExist)(const wchar_t* str, unsigned int* errorCode);
        /// <summary>
        /// Check if a file exist.
        /// </summary>
        /// <param name="pathStr">Must have directory (optional) and file name.</param>
        /// <param name="errorCode">Given error code if failed.</param>
        /// <returns>Return true if file exist, false with given errorCode.</returns>
        bool (*isFileExist)(const wchar_t* pathStr, unsigned int* errorCode);
        /// <summary>
        /// Format variable arguments into given prefix string.
        /// </summary>
        /// <param name="writeTo">Output string</param>
        /// <param name="_Format">Format message string</param>
        /// <param name="...">Variable arguments</param>
        /// <returns>Return true or false for format completion.</returns>
        bool (*FormatVarArgsA)(char* writeTo, const char* _Format, ...);
        /// <summary>
        /// Format variable arguments into given prefix string.
        /// </summary>
        /// <param name="writeTo">Output string</param>
        /// <param name="_Format">Format message string</param>
        /// <param name="...">Variable arguments</param>
        /// <returns>Return true or false for format completion.</returns>
        bool (*FormatVarArgsW)(wchar_t* writeTo, const wchar_t* _Format, ...);
    } IUtil;
    dllport IUtil* getIUtil(unsigned int hash);
    //This variable is needed for IObject usage and prevent link breakage.
    extern IUtil* pIUtil;
#ifdef __cplusplus
}
class ArgContainer {
public:
    ArgContainerVars vars;
    ArgContainer() {
        ArgContainerVars_Constructor(vars);
    }
    ArgContainer(const wchar_t* arg) {
        ArgContainerVars_Set(vars, arg);
    }
    ArgContainer(const wchar_t* arg, int numArrayLink) {
        ArgContainerVars_Set_N(vars, arg, numArrayLink);
    }
    wchar_t* operator[](unsigned int i) {
        return ArgContainerVars_At(vars, i);
    }
    ArgContainer& operator=(ArgContainer &copy) {
        ArgContainerVars_Copy(vars, (ArgContainerVars&)copy);
        return *this;
    }
    ~ArgContainer() {
        ArgContainerVars_Deconstructor(vars);
    }
};
template<class T>
class dynamicStack {
private:
    struct d_stack {
        T* d_type;
        d_stack* next_d_type;
        d_stack() {
            d_type = 0;
            next_d_type = 0;
        }
    };
    d_stack* stack;
public:
    dynamicStack<T>() { stack = 0; }
    dynamicStack(dynamicStack const &copy) {
        d_stack* scan = copy.stack;
        d_stack* clone;
        if (!scan) {
            stack = 0;
            return;
        }
        else {
            stack = (d_stack*)pIUtil->AllocMem(sizeof(d_stack));
            stack->d_type = (T*)pIUtil->AllocMem(sizeof(T));
            *stack->d_type = *scan->d_type;
            scan = scan->next_d_type;
            stack->next_d_type = 0;
            clone = stack;

        }
        while (scan) {
            d_stack* newStack = (d_stack*)pIUtil->AllocMem(sizeof(d_stack));
            newStack->d_type = (T*)pIUtil->AllocMem(sizeof(T));
            *newStack->d_type = *scan->d_type;
            newStack->next_d_type = 0;
            clone->next_d_type = newStack;
            clone = newStack;
            scan = scan->next_d_type;
        }

    }
    ~dynamicStack<T>() {
        this->clear();
    }
    class iterator;
    class const_iterator;
    void push_back(T type) {
        if (!stack) {
            stack = (d_stack*)pIUtil->AllocMem(sizeof(d_stack));
            stack->d_type = (T*)pIUtil->AllocMem(sizeof(T));
            *stack->d_type = type;
            stack->next_d_type = 0;
        }
        else {
            d_stack* src = stack;
            while (src->next_d_type) {
                src = src->next_d_type;
            }
            d_stack* newSrc = (d_stack*)pIUtil->AllocMem(sizeof(d_stack));
            newSrc->d_type = (T*)pIUtil->AllocMem(sizeof(T));
            *newSrc->d_type = type;
            newSrc->next_d_type = 0;
            src->next_d_type = newSrc;
        }
    }
    void pop_back() {
        if (stack) {
            d_stack* src = stack;
            d_stack* nextSrc = stack->next_d_type;
            pIUtil->FreeMem(stack->d_type);
            pIUtil->FreeMem(stack);
            stack = nextSrc;
        }
    }
    bool remove(size_t i) {
        bool isDel = 1;
        d_stack* src = stack;
        d_stack* prevSrc = 0;
        for (i; i>0; i--) {
            if (src && src->next_d_type) {
                prevSrc = src;
                src = src->next_d_type;
            }
            else {
                isDel = 0;
                break;
            }
        }
        if (isDel) {
            if (prevSrc) {
                prevSrc->next_d_type = src->next_d_type;
            }
            else
                stack = src->next_d_type;
            pIUtil->FreeMem(src->d_type);
            pIUtil->FreeMem(src);
        }
        return isDel;
    }
    T* operator[](size_t i) {
        d_stack* src = stack;
        for (i; i>0; i--) {
            if (src && src->next_d_type) {
                src = src->next_d_type;
            }
            else {
                src = 0;
                break;
            }
        }
        if (src)
            return src->d_type;
        else
            return 0;
    }
    const T* operator[](size_t i) const {
        d_stack* src = stack;
        for (i; i>0; i--) {
            if (src) {
                src = src->next_d_type;
            }
            else {
                src = 0;
                break;
            }
        }
        if (src)
            return (const T*)(src->d_type);
        else
            return (const T*)0;
    }
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
    }*/
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
    void clear() {
        if (stack) {
            d_stack* src = stack;
            d_stack* holder = 0;
            while (src) {
                if (src->d_type)
                    pIUtil->FreeMem(src->d_type);
                holder = src;
                src = src->next_d_type;
                pIUtil->FreeMem(holder);
            }
            stack = 0;
        }
    }
    //example of https:// gist.github.com/jeetsukumaran/307264 with nessary modifications.
    class iterator {
    private:
        d_stack* ptr_;
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
};
#endif

#endif

//Optimization section begin

//Source: http://stackoverflow.com/a/17005764
#define idiv_ceil(x, y) (x / y + (((x < 0) ^ (y > 0)) && (x%y)))


//Optimization section end

#endif