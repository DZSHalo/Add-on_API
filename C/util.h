#ifndef utilH
#define utilH

#define WINAPIC     __cdecl

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
    dllport wchar_t* ArgContainerVars_At(ArgContainerVars& vars, size_t i);
    typedef struct IUtil {
        void* (*AllocMem)(size_t Size);
        void(*FreeMem)(void* Address);
        void (*toCharW)(const char* charA, int len, wchar_t* charW);
        void (*toCharA)(const wchar_t* charW, int len, char* charA);
        toggle (*StrToBooleanA)(const char str[]);
        toggle (*StrToBooleanW)(const wchar_t str[]);
        e_color_team_index (*StrToTeamA)(const char str[]);
        e_color_team_index (*StrToTeamW)(const wchar_t str[]);
        void (*ReplaceA)(char* regStr);
        void (*ReplaceW)(wchar_t* regStr);
        void (*ReplaceUndoA)(char* regStr);
        void (*ReplaceUndoW)(wchar_t* regStr);
        bool (*isnumberA)(const char* str);
        bool (*isnumberW)(const wchar_t* str);
        bool (*ishashA)(const char* str);
        bool (*ishashW)(const wchar_t* str);
        void (*shiftStrA)(char regStr[], int len, int pos, int lenShift, bool leftRight);
        void (*shiftStrW)(wchar_t regStr[], int len, int pos, int lenShift, bool leftRight);
        void (*regexReplaceW)(wchar_t regStr[], bool isDB);
        bool (*regexMatchW)(wchar_t srcStr[], wchar_t regex[]);
        bool (*regexiMatchW)(wchar_t srcStr[], wchar_t regex[]);


        bool (*FormatVarArgsA)(const char* _Format, char * ArgList, char* writeTo);
        bool (*FormatVarArgsW)(const wchar_t* _Format, char * ArgList, wchar_t* writeTo);
        bool (*findSubStrFirstA)(const char* dest, const char* src);
        bool (*findSubStrFirstW)(const wchar_t* dest, const wchar_t* src);

        bool (*islettersA)(const char* str);
        bool (*islettersW)(const wchar_t* str);

        bool (*isfloatA)(const char* str);
        bool (*isfloatW)(const wchar_t* str);
        bool (*isdoubleA)(const char* str);
        bool (*isdoubleW)(const wchar_t* str);
        int (*strcatW)(wchar_t* dest, size_t len, const wchar_t* src);
        int (*strcatA)(char *dest, size_t len, const char* src);
        void (*str_to_wstr)(char* str, wchar_t* wstr);
        bool (*strcmpW)(const wchar_t* str1, const wchar_t* str2);
        bool (*strcmpA)(const char* str1, const char* str2);
        bool (*stricmpW)(const wchar_t* str1, const wchar_t* str2);
        bool (*stricmpA)(const char* str1, const char* str2);
    } IUtil;
    dllport IUtil* getIUtil(unsigned int hash);
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

#endif