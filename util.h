#ifndef utilH
#define utilH

namespace util {
	static char colon = ':';
	static char newline = '\n';
	static char pipe = '|';
	static wchar_t commaW = L',';
	static wchar_t me[] = L"me";
	static const wchar_t backslash = L'\\';
	static GAME_MODE_S modeAll = {1,1,1};
	static GAME_MODE_S modeSingle = {1, 0, 0};
	static GAME_MODE_S modeSingleMulti = {1, 1, 0};
	static GAME_MODE_S modeSingleHost = {1, 0, 1};
	static GAME_MODE_S modeMulti = {0,1,0};
	static GAME_MODE_S modeMultiHost = {0,1,1};
	static GAME_MODE_S modeHost = {0,0,1};
	#define PI 3.141592653589793f
	#define TOGGLE(val, on) val = (on==-1 ? !val : on)

	extern "C" void dllport *Alloc(size_t Size);
	extern "C" void dllport Free(void* Address);

	extern "C" class dllport ArgContainer {
	private:
		wchar_t* args[10];

	public:
		size_t argc;
		ArgContainer();
		ArgContainer(const wchar_t arg[]);
		ArgContainer(const wchar_t arg[], int numArrayLink);
		ArgContainer(ArgContainer const &copy);
		wchar_t* WINAPIC operator[](size_t i);
		ArgContainer& WINAPIC operator=(ArgContainer const &copy);
		ArgContainer WINAPIC operator*();
		virtual ~ArgContainer();
	};

	extern "C" void dllport toCharW(char* charA, int len, wchar_t* charW);
	extern "C" void dllport toCharA(wchar_t* charW, int len, char* charA);
	extern "C" toggle dllport StrToBooleanA(const char str[]);
	extern "C" toggle dllport StrToBooleanW(const wchar_t str[]);
	extern "C" toggle dllport StrToTeamA(const char str[]);
	extern "C" toggle dllport StrToTeamW(const wchar_t str[]);
	extern "C" void dllport ReplaceA(char* regStr);
	extern "C" void dllport ReplaceW(wchar_t* regStr);
	extern "C" void dllport ReplaceUndoA(char* regStr);
	extern "C" void dllport ReplaceUndoW(wchar_t* regStr);
	extern "C" bool dllport isnumberA(const char* str);
	extern "C" bool dllport isnumberW(const wchar_t* str);
	extern "C" bool dllport hashCheckA(const char* str);
	extern "C" bool dllport hashCheckW(const wchar_t* str);
	extern "C" void dllport shiftStrA(char regStr[], int len, int pos, int lenShift, bool leftRight);
	extern "C" void dllport shiftStrW(wchar_t regStr[], int len, int pos, int lenShift, bool leftRight);
	extern "C" void dllport regexReplaceW(wchar_t regStr[], bool isDB);
	extern "C" bool dllport regexMatchW(wchar_t srcStr[], wchar_t regex[]);
	extern "C" bool dllport regexiMatchW(wchar_t srcStr[], wchar_t regex[]);
	extern "C" bool dllport FormatVarArgsA(const char* _Format, va_list ArgList, char* writeTo);
	extern "C" bool dllport FormatVarArgsW(const wchar_t* _Format, va_list ArgList, wchar_t* writeTo);
	extern "C" bool dllport dirExistsW(const wchar_t dirName[]);
	#pragma pack(push,1)
	struct haloConsole	{
			int r;
			char output[1024];
	};
	#pragma pack(pop)

	template<class T>
	class ThreadSafeObject {
	public:
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
	};
	template<class T>
	class dynamicStack {
	private:
		struct d_stack {
			T* d_type;
			d_stack* next_d_type;
			d_stack() {
				d_type = NULL;
				next_d_type = NULL;
			}
		};
		d_stack* stack;
	public:
		dynamicStack<T>() { stack=NULL; }
		dynamicStack(dynamicStack const &copy) {
			d_stack* scan = copy.stack;
			d_stack* clone;
			if (!scan) {
				stack = NULL;
				return;
			} else {
				stack = (d_stack*)util::Alloc(sizeof(d_stack));
				stack->d_type = (T*)util::Alloc(sizeof(T));
				*stack->d_type = *scan->d_type;
				scan = scan->next_d_type;
				stack->next_d_type = NULL;
				clone = stack;

			}
			while(scan) {
				d_stack* newStack = (d_stack*)util::Alloc(sizeof(d_stack));
				newStack->d_type = (T*)util::Alloc(sizeof(T));
				*newStack->d_type = *clone->d_type;
				newStack->next_d_type = NULL;
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
				stack = (d_stack*)util::Alloc(sizeof(d_stack));
				stack->d_type = (T*)util::Alloc(sizeof(T));
				*stack->d_type = type;
				stack->next_d_type = NULL;
			} else {
				d_stack* src = stack;
				while (src->next_d_type) {
					src = src->next_d_type;
				}
				d_stack* newSrc = (d_stack*)util::Alloc(sizeof(d_stack));
				newSrc->d_type = (T*)util::Alloc(sizeof(T));
				*newSrc->d_type = type;
				newSrc->next_d_type = NULL;
				src->next_d_type = newSrc;
			}
		}
		void pop_back() {
			if (stack) {
				d_stack* src = stack;
				d_stack* nextSrc = stack->next_d_type;
				util::Free(stack->d_type);
				util::Free(stack);
				stack = nextSrc;
			}
		}
		bool remove(size_t i) {
			bool isDel=1;
			d_stack* src = stack;
			d_stack* prevSrc = NULL;
			for (i; i>0; i--) {
				if (src && src->next_d_type) {
					prevSrc = src;
					src = src->next_d_type;
				} else {
					isDel=0;
					break;
				}
			}
			if (isDel) {
				if (prevSrc) {
					prevSrc->next_d_type = src->next_d_type;
				} else
					stack = src->next_d_type;
				util::Free(src->d_type);
				util::Free(src);
			}
			return isDel;
		}
		T* operator[](size_t i) {
			d_stack* src = stack;
			for (i; i>0; i--) {
				if (src && src->next_d_type) {
					src = src->next_d_type;
				} else {
					src = NULL;
					break;
				}
			}
			if (src)
				return src->d_type;
			else
				return NULL;
		}
		const T* operator[](size_t i) const {
			d_stack* src = stack;
			for (i; i>0; i--) {
				if (src) {
					src = src->next_d_type;
				} else {
					src = NULL;
					break;
				}
			}
			if (src)
				return (const T*)(src->d_type);
			else
				return (const T*)NULL;
		}
		iterator begin() {
			if (stack)
				return iterator(stack);
			else
				return iterator(NULL);
		}
		iterator end() {
			d_stack* src = stack;
			while(src && src->_next_d_type) {
				src = stack->next_d_type;
			}
			return iterator(src);
		}
		iterator erase(iterator data) {
			d_stack* src = stack;
			d_stack* prevSrc = NULL;
			while(src) {
				if (data.ptr_->d_type==src->d_type) {
					if (prevSrc) {
						prevSrc->next_d_type = src->next_d_type;
					} else
						stack = prevSrc = src->next_d_type;
					util::Free(src->d_type);
					util::Free(src);
					return prevSrc;
				}
				prevSrc = src;
				src = src->next_d_type;
			}
			return (iterator)NULL;
		}
		size_t size() {
			size_t count = 0;
			d_stack* src = stack;
			while(src) {
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
				} else {
					doRet=0;
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
			if (iter!=NULL) {
				insert = (d_stack*)util::Alloc(sizeof(d_stack));
				insert->d_type = (T*)util::Alloc(sizeof(T));
				*insert->d_type = newData;
				insert->next_d_type = iter.ptr_->next_d_type;
				iter.ptr_->next_d_type = insert;

			} else {
				d_stack* src = stack;
				if (!src) {
					stack = (d_stack*)util::Alloc(sizeof(d_stack));
					stack->d_type = (T*)util::Alloc(sizeof(T));
					*stack->d_type = newData;
					stack->next_d_type = NULL;
					return;
				}
				while(src) {
					if (src->next_d_type==iter.ptr_) {
						break;
					}
					src = src->next_d_type;
				}
				if (!src)
					src = stack;
				insert = (d_stack*)util::Alloc(sizeof(d_stack));
				insert->d_type = (T*)util::Alloc(sizeof(T));
				*insert->d_type = newData;
				insert->next_d_type = src->next_d_type;
				src->next_d_type = insert;
			}
		}
		void clear() {
			if (stack) {
				d_stack* src = stack;
				d_stack* holder = NULL;
				while (src) {
					if (src->d_type)
						util::Free(src->d_type);
					holder = src;
					src = src->next_d_type;
					util::Free(holder);
				}
				stack=NULL;
			}
		}
		class iterator {
			private:
				d_stack* ptr_;
			public:
				typedef iterator self_type;
				typedef T value_type;
				typedef T& reference;
				typedef T* pointer;
				typedef int difference_type;
				iterator() : ptr_(NULL) {}
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
					for(append; append>0; append--) {
						if (src) {
							src = src->next_d_type;
						} else {
							break;
						}
					}
					if (src) {
						this->ptr_ = src;
					} else
						this->ptr_=NULL;
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
	extern "C" dllport bool WINAPIC findSubStrFirstA(const char* dest, const char* src);
	extern "C" dllport bool WINAPIC findSubStrFirstW(const wchar_t* dest, const wchar_t* src);
}
#endif