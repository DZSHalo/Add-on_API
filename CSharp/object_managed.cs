using System;
using System.Runtime.InteropServices;

namespace Addon_API {
    /*public struct hTagIndexTableHeader {
    }*/
    public struct hTagHeaderPtr {
        public IntPtr ptr;
        public hTagHeaderPtr(IntPtr data) {
            ptr = data;
        }
    }
    public struct hTagHeader_managed {
        private hTagHeaderPtr gPtr;
        public hTagHeader hTagHeader_n;
        public hTagHeaderPtr nPtr {
            private get { return gPtr; }
                    set {
                        gPtr = value;
                        if (value.ptr != IntPtr.Zero)
                            hTagHeader_n = (hTagHeader)Marshal.PtrToStructure(value.ptr, typeof(hTagHeader));
                        else
                            hTagHeader_n = new hTagHeader();
                    }
        }
        public hTagHeader_managed(ref hTagHeader_managed data) {
            gPtr = data.gPtr;
            hTagHeader_n = data.hTagHeader_n;
        }
        public hTagHeader_managed(hTagHeaderPtr dPtr) {
            gPtr = dPtr;
            if (dPtr.ptr != IntPtr.Zero)
                hTagHeader_n = (hTagHeader)Marshal.PtrToStructure(dPtr.ptr, typeof(hTagHeader));
            else
                hTagHeader_n = new hTagHeader();
        }
        static public implicit operator hTagHeader_managed(hTagHeaderPtr dPtr) {
            return new hTagHeader_managed(dPtr);
        }
        public void Save() {
            if (gPtr.ptr != IntPtr.Zero)
                Marshal.StructureToPtr(hTagHeader_n, gPtr.ptr, false);
        }
        public void Refresh() {
            if (gPtr.ptr != IntPtr.Zero)
                hTagHeader_n = (hTagHeader)Marshal.PtrToStructure(gPtr.ptr, typeof(hTagHeader));
        }
        public bool isNull() {
            return gPtr.ptr == IntPtr.Zero;
        }
    }

    public struct objDamageInfoPtr {
        public IntPtr ptr;
    }
    public struct objDamageInfo_managed {
        private objDamageInfoPtr gPtr;
        public objDamageInfo objDamageInfo_n;
        public objDamageInfo_managed(objDamageInfoPtr dPtr) {
            gPtr = dPtr;
            if (dPtr.ptr != IntPtr.Zero)
                objDamageInfo_n = (objDamageInfo)Marshal.PtrToStructure(dPtr.ptr, typeof(objDamageInfo));
            else
                objDamageInfo_n = new objDamageInfo();
        }
        static public implicit operator objDamageInfo_managed(objDamageInfoPtr dPtr) {
            return new objDamageInfo_managed(dPtr);
        }
        public void Save() {
            if (gPtr.ptr != IntPtr.Zero)
                Marshal.StructureToPtr(objDamageInfo_n, gPtr.ptr, false);
        }
        public void Refresh() {
            if (gPtr.ptr != IntPtr.Zero)
                objDamageInfo_n = (objDamageInfo)Marshal.PtrToStructure(gPtr.ptr, typeof(objDamageInfo));
        }
        public bool isNotNull() {
            return gPtr.ptr != IntPtr.Zero;
        }
    }

    public struct objHitInfoPtr {
        public IntPtr ptr;
    }
    /*public struct objHitInfo_managed {
    }*/

    public struct objManagedPtr {
        public IntPtr ptr;
    }
    public struct objManaged_managed {
        private objManagedPtr gPtr;
        public objManaged objManaged_n;
        public objManaged_managed(objManagedPtr dPtr) {
            gPtr = dPtr;
            if (dPtr.ptr != IntPtr.Zero)
                objManaged_n = (objManaged)Marshal.PtrToStructure(dPtr.ptr, typeof(objManaged));
            else
                objManaged_n = new objManaged();
        }
        static public implicit operator objManaged_managed(objManagedPtr dPtr) {
            return new objManaged_managed(dPtr);
        }
        public void Save() {
            if (gPtr.ptr != IntPtr.Zero)
                Marshal.StructureToPtr(objManaged_n, gPtr.ptr, false);
        }
        public void Refresh() {
            if (gPtr.ptr != IntPtr.Zero)
                objManaged_n = (objManaged)Marshal.PtrToStructure(gPtr.ptr, typeof(objManaged));
        }
        public bool isNotNull() {
            return gPtr.ptr != IntPtr.Zero;
        }
    }

    public struct objCreationInfoPtr {
        public IntPtr ptr;
    }
    /*public struct objCreationInfo_managed {
    }*/

#if EXT_IOBJECT
    [StructLayoutAttribute(LayoutKind.Sequential)]
    public class objTagGroupList : IDisposable {
        public uint count;
        private System.IntPtr tag_list;
        public hTagHeader_managed list(uint iterator) {
            hTagHeader_managed i = new hTagHeader_managed();
            if (iterator < count) {
                i.nPtr = new hTagHeaderPtr((IntPtr)Marshal.ReadInt32(new IntPtr(iterator * 4 + tag_list.ToInt32())));
            }
            return i;
        }
        ~objTagGroupList() {
            Dispose(false);
        }
        protected void Dispose(bool disposing) {
#if EXT_IUTIL
            if (tag_list != IntPtr.Zero) {
                Addon_API.Global.pIUtil.m_freeMem(tag_list);
                tag_list = IntPtr.Zero;
            }
#else
#error EXT_IUTIL is a requirement for using objTagGroupList structure.
#endif
        }
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
#endif
}
