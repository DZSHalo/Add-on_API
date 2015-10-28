#ifndef UtilExtendH
#define UtilExtendH


template<class T>
class ThreadSafeObject {
public:
    //inline ThreadSafeObject(){}
    inline ThreadSafeObject(T* type):m_type(type){InitializeCriticalSection(&m_cs);}
    ~ThreadSafeObject() {
        EnterCriticalSection (&m_cs);
        delete m_type;m_type=NULL;
        LeaveCriticalSection(&m_cs);
        DeleteCriticalSection(&m_cs);
    }
    class RefLock {
    public:
        inline RefLock(T* type, CRITICAL_SECTION &cs):m_type(type), m_cs(cs){ EnterCriticalSection (&m_cs);}
        inline RefLock(const RefLock& Src):m_type(Src.m_type), m_cs(Src.m_cs){ EnterCriticalSection (&m_cs);}
        inline ~RefLock(){LeaveCriticalSection(&m_cs);}
        inline T* operator->() const{return m_type;}
        inline T& operator*() const{return m_type;}
        T* m_type;
    private:
        CRITICAL_SECTION &m_cs;
    };
    inline RefLock GetLockedObject(){return RefLock(m_type, m_cs);}
private:
    T* m_type;
    CRITICAL_SECTION m_cs;
    //ThreadSafeObject(const ThreadSafeObject&):m_type(new T){InitializeCriticalSection(&m_cs);}
    //ThreadSafeObject<T>& operator=(const ThreadSafeObject&);
};

#endif