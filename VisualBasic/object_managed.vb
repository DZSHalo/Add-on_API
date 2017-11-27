
Imports System.Runtime.InteropServices

Namespace Addon_API
    'public struct hTagIndexTableHeader {
    '    }

    Public Structure hTagHeaderPtr
        Public ptr As IntPtr
        Public Sub New(data As IntPtr)
            ptr = data
        End Sub
    End Structure
    Public Structure hTagHeader_managed
        Private gPtr As hTagHeaderPtr
        Public hTagHeader_n As hTagHeader
        Public Property nPtr() As hTagHeaderPtr
            Private Get
                Return gPtr
            End Get
            Set
                gPtr = Value
                If Value.ptr <> IntPtr.Zero Then
                    hTagHeader_n = DirectCast(Marshal.PtrToStructure(Value.ptr, GetType(hTagHeader)), hTagHeader)
                Else
                    hTagHeader_n = New hTagHeader()
                End If
            End Set
        End Property
        Public Sub New(ByRef data As hTagHeader_managed)
            gPtr = data.gPtr
            hTagHeader_n = data.hTagHeader_n
        End Sub
        Public Sub New(dPtr As hTagHeaderPtr)
            gPtr = dPtr
            If dPtr.ptr <> IntPtr.Zero Then
                hTagHeader_n = DirectCast(Marshal.PtrToStructure(dPtr.ptr, GetType(hTagHeader)), hTagHeader)
            Else
                hTagHeader_n = New hTagHeader()
            End If
        End Sub
        Public Shared Widening Operator CType(dPtr As hTagHeaderPtr) As hTagHeader_managed
            Return New hTagHeader_managed(dPtr)
        End Operator
        Public Sub Save()
            If gPtr.ptr <> IntPtr.Zero Then
                Marshal.StructureToPtr(hTagHeader_n, gPtr.ptr, False)
            End If
        End Sub
        Public Sub Refresh()
            If gPtr.ptr <> IntPtr.Zero Then
                hTagHeader_n = DirectCast(Marshal.PtrToStructure(gPtr.ptr, GetType(hTagHeader)), hTagHeader)
            End If
        End Sub
        Public Function isNull() As Boolean
            Return gPtr.ptr = IntPtr.Zero
        End Function
    End Structure

    Public Structure objDamageInfoPtr
        Public ptr As IntPtr
    End Structure
    Public Structure objDamageInfo_managed
        Private gPtr As objDamageInfoPtr
        Public objDamageInfo_n As objDamageInfo
        Public Sub New(dPtr As objDamageInfoPtr)
            gPtr = dPtr
            If dPtr.ptr <> IntPtr.Zero Then
                objDamageInfo_n = DirectCast(Marshal.PtrToStructure(dPtr.ptr, GetType(objDamageInfo)), objDamageInfo)
            Else
                objDamageInfo_n = New objDamageInfo()
            End If
        End Sub
        Public Shared Widening Operator CType(dPtr As objDamageInfoPtr) As objDamageInfo_managed
            Return New objDamageInfo_managed(dPtr)
        End Operator
        Public Sub Save()
            If gPtr.ptr <> IntPtr.Zero Then
                Marshal.StructureToPtr(objDamageInfo_n, gPtr.ptr, False)
            End If
        End Sub
        Public Sub Refresh()
            If gPtr.ptr <> IntPtr.Zero Then
                objDamageInfo_n = DirectCast(Marshal.PtrToStructure(gPtr.ptr, GetType(objDamageInfo)), objDamageInfo)
            End If
        End Sub
        Public Function isNotNull() As Boolean
            Return gPtr.ptr <> IntPtr.Zero
        End Function
    End Structure

    Public Structure objHitInfoPtr
        Public ptr As IntPtr
    End Structure
    'public struct objHitInfo_managed {
    '    }


    Public Structure objManagedPtr
        Public ptr As IntPtr
    End Structure
    Public Structure objManaged_managed
        Private gPtr As objManagedPtr
        Public objManaged_n As objManaged
        Public Sub New(dPtr As objManagedPtr)
            gPtr = dPtr
            If dPtr.ptr <> IntPtr.Zero Then
                objManaged_n = DirectCast(Marshal.PtrToStructure(dPtr.ptr, GetType(objManaged)), objManaged)
            Else
                objManaged_n = New objManaged()
            End If
        End Sub
        Public Shared Widening Operator CType(dPtr As objManagedPtr) As objManaged_managed
            Return New objManaged_managed(dPtr)
        End Operator
        Public Sub Save()
            If gPtr.ptr <> IntPtr.Zero Then
                Marshal.StructureToPtr(objManaged_n, gPtr.ptr, False)
            End If
        End Sub
        Public Sub Refresh()
            If gPtr.ptr <> IntPtr.Zero Then
                objManaged_n = DirectCast(Marshal.PtrToStructure(gPtr.ptr, GetType(objManaged)), objManaged)
            End If
        End Sub
        Public Function isNotNull() As Boolean
            Return gPtr.ptr <> IntPtr.Zero
        End Function
    End Structure

    Public Structure objCreationInfoPtr
        Public ptr As IntPtr
    End Structure
    'public struct objCreationInfo_managed {
    '    }


#If EXT_IOBJECT Then
	<StructLayoutAttribute(LayoutKind.Sequential)> _
	Public Class objTagGroupList
		Implements IDisposable
		Public count As UInteger
		Private tag_list As System.IntPtr
		Public Function list(iterator As UInteger) As hTagHeader_managed
			Dim i As New hTagHeader_managed()
			If iterator < count Then
				i.nPtr = New hTagHeaderPtr(DirectCast(Marshal.ReadInt32(New IntPtr(iterator * 4 + tag_list.ToInt32())), IntPtr))
			End If
			Return i
		End Function
		Protected Overrides Sub Finalize()
			Try
				Dispose(False)
			Finally
				MyBase.Finalize()
			End Try
		End Sub
		Protected Sub Dispose(disposing As Boolean)
			#If EXT_IUTIL Then
			If tag_list <> IntPtr.Zero Then
				Addon_API.[Global].pIUtil.m_freeMem(tag_list)
				tag_list = IntPtr.Zero
			End If
			#Else
			#Error EXT_IUTIL is a requirement for using objTagGroupList structure.
			#End If
		End Sub
		Public Sub Dispose()
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub
	End Class
#End If
End Namespace
