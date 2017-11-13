#If EXT_IUTIL Then
Imports System.Text
#End If
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Sequential)>
Public Structure tm
    Public tm_sec As Integer ' seconds after the minute - [0,59] 
    Public tm_min As Integer ' minutes after the hour - [0,59] 
    Public tm_hour As Integer ' hours since midnight - [0,23] 
    Public tm_mday As Integer ' day of the month - [1,31] 
    Public tm_mon As Integer ' months since January - [0,11] 
    Public tm_year As Integer ' years since 1900 
    Public tm_wday As Integer ' days since Sunday - [0,6] 
    Public tm_yday As Integer ' days since January 1 - [0,365] 
    Public tm_isdst As Integer ' daylight savings time flag 
End Structure

'Necessary for easier management.
Public Structure IntPtrValue
    Public ptr As IntPtr
    Public Property value() As Integer
        Get
            Return Marshal.ReadInt32(ptr)
        End Get
        Set
            Marshal.WriteInt32(ptr, Value)
        End Set
    End Property
End Structure
Public Structure UIntPtrValue
    Public ptr As IntPtr
    Public Property value() As UInteger
        Get
            Return CUInt(Marshal.PtrToStructure(ptr, GetType(UInt32)))
        End Get
        Set
            Marshal.StructureToPtr(Value, ptr, True)
        End Set
    End Property
End Structure

Namespace Addon_API
    <StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure ArgContainerVars
        <MarshalAs(UnmanagedType.LPWStr, SizeConst:=256)>
        Public arg As String
        Public arg_len As UInteger
        <MarshalAs(UnmanagedType.ByValArray, SizeConst:=10)>
        Public args As String()
        Public argc As UInteger
    End Structure
    <StructLayoutAttribute(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Public Structure ArgContainerAPI
        <DllImport("H-Ext.dll", EntryPoint:="#30", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Sub ArgContainerVars_Constructor(<[In], Out> ByRef vars As ArgContainerVars)
        End Sub
        <DllImport("H-Ext.dll", EntryPoint:="#31", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Sub ArgContainerVars_Deconstructor(<[In], Out> ByRef vars As ArgContainerVars)
        End Sub
        <DllImport("H-Ext.dll", EntryPoint:="#33", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Sub ArgContainerVars_Set(<[In], Out> ByRef vars As ArgContainerVars, <[In], MarshalAs(UnmanagedType.LPWStr)> args As String)
        End Sub
        <DllImport("H-Ext.dll", EntryPoint:="#34", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Sub ArgContainerVars_Set_N(<[In], Out> ByRef vars As ArgContainerVars, <[In], MarshalAs(UnmanagedType.LPWStr)> args As String, numArrayLink As Integer)
        End Sub
        <DllImport("H-Ext.dll", EntryPoint:="#32", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Sub ArgContainerVars_Copy(<[In], Out> ByRef vars As ArgContainerVars, <[In], Out> ByRef copy As ArgContainerVars)
        End Sub
        <DllImport("H-Ext.dll", EntryPoint:="#35", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Function ArgContainerVars_At(<[In], Out> ByRef vars As ArgContainerVars, <[In]> i As UInteger) As <MarshalAs(UnmanagedType.LPWStr)> String
        End Function
    End Structure
    Public Class ArgContainer
        Implements IDisposable
        Public vars As ArgContainerVars
        Public Property argc() As UInteger
            Get
                Return vars.argc
            End Get
            Private Set(value As UInteger)
            End Set
        End Property
        Public Sub New()
            ArgContainerAPI.ArgContainerVars_Constructor(vars)
        End Sub
        Public Sub New(<[In], MarshalAs(UnmanagedType.LPWStr)> arg As String)
            ArgContainerAPI.ArgContainerVars_Set(vars, arg)
        End Sub
        Public Sub New(<[In], MarshalAs(UnmanagedType.LPWStr)> arg As String, <[In]> numArrayLink As Integer)
            ArgContainerAPI.ArgContainerVars_Set_N(vars, arg, numArrayLink)
        End Sub
        Public Property Item(i As UInteger) As String
            Get
                Return ArgContainerAPI.ArgContainerVars_At(vars, i)
            End Get
            Private Set(value As String)
            End Set
        End Property
        Public Sub New(copy As ArgContainer)
            ArgContainerAPI.ArgContainerVars_Copy(vars, copy.vars)
        End Sub
        Public Sub New(copy As ArgContainerVars)
            ArgContainerAPI.ArgContainerVars_Copy(vars, copy)
        End Sub
        Protected Overrides Sub Finalize()
            Try
                Dispose(False)
            Finally
                MyBase.Finalize()
            End Try
        End Sub
        Protected Sub Dispose(disposing As Boolean)
            If vars.argc <> 0 Then
                ArgContainerAPI.ArgContainerVars_Deconstructor(vars)
                vars.argc = 0
            End If
        End Sub
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
        Public Shared Widening Operator CType(arg As ArgContainer) As ArgContainerVars
            Return arg.vars
        End Operator
        Public Shared Widening Operator CType(vars As ArgContainerVars) As ArgContainer
            Return New ArgContainer(vars)
        End Operator
    End Class
#If EXT_IUTIL Then
    Public Structure IUtilPtr
        Public ptr As IntPtr
    End Structure

    <StructLayoutAttribute(LayoutKind.Sequential)>
    Public Structure IUtil
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_allocMem(<[In]> Size As UInteger) As IntPtr
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_freeMem(<[In]> Address As IntPtr)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_toCharW(<[In], MarshalAs(UnmanagedType.LPStr)> charA As String, len As Integer, <[In], Out, MarshalAs(UnmanagedType.LPWStr)> charW As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_toCharA(<[In], MarshalAs(UnmanagedType.LPWStr)> charW As String, len As Integer, <[In], Out, MarshalAs(UnmanagedType.LPStr)> charA As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_strToBooleanA(<[In], MarshalAs(UnmanagedType.LPStr)> str As String) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_strToBooleanW(<[In], MarshalAs(UnmanagedType.LPWStr)> str As String) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_strToTeamA(<[In], MarshalAs(UnmanagedType.LPStr)> str As String) As e_color_team_index
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_strToTeamW(<[In], MarshalAs(UnmanagedType.LPWStr)> str As String) As e_color_team_index
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_replaceA(<[In], Out, MarshalAs(UnmanagedType.LPStr)> str As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_replaceW(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> str As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_replaceUndoA(<[In], Out, MarshalAs(UnmanagedType.LPStr)> str As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_replaceUndoW(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> str As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isNumberA(<[In], MarshalAs(UnmanagedType.LPStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isNumberW(<[In], MarshalAs(UnmanagedType.LPWStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isHashA(<[In], MarshalAs(UnmanagedType.LPStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isHashW(<[In], MarshalAs(UnmanagedType.LPWStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_shiftStrA(<[In], Out, MarshalAs(UnmanagedType.LPStr)> regStr As StringBuilder, <[In]> len As Integer, <[In]> pos As Integer, <[In]> lenShift As Integer, <[In], MarshalAs(UnmanagedType.I1)> leftRight As Boolean) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_shiftStrW(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> regStr As StringBuilder, <[In]> len As Integer, <[In]> pos As Integer, <[In]> lenShift As Integer, <[In], MarshalAs(UnmanagedType.I1)> leftRight As Boolean) As e_boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_regexReplaceW(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> regStr As StringBuilder, <[In], MarshalAs(UnmanagedType.I1)> isDB As Boolean)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_regexMatchW(<[In], MarshalAs(UnmanagedType.LPWStr)> srcStr As String, <[In], MarshalAs(UnmanagedType.LPWStr)> regex As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_regexiMatchW(<[In], MarshalAs(UnmanagedType.LPWStr)> srcStr As String, <[In], MarshalAs(UnmanagedType.LPWStr)> regex As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <Obsolete("Do not use FormatVarArgsListA function, will not work as expected.")>
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Private Delegate Function d_FormatVarArgsListA(<[In], Out, MarshalAs(UnmanagedType.LPStr)> writeTo As StringBuilder, <[In], MarshalAs(UnmanagedType.LPStr)> _Format As String, ArgList As ArgIterator) As <MarshalAs(UnmanagedType.I1)> Boolean
        <Obsolete("Do not use FormatVarArgsListW function, will not work as expected.")>
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Private Delegate Function d_FormatVarArgsListW(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> writeTo As StringBuilder, <[In], MarshalAs(UnmanagedType.LPWStr)> _Format As String, ArgList As ArgIterator) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_findSubStrFirstA(<[In], MarshalAs(UnmanagedType.LPStr)> src As String, <[In], MarshalAs(UnmanagedType.LPStr)> find As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_findSubStrFirstW(<[In], MarshalAs(UnmanagedType.LPWStr)> src As String, <[In], MarshalAs(UnmanagedType.LPWStr)> find As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isLettersA(<[In], MarshalAs(UnmanagedType.LPStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isLettersW(<[In], MarshalAs(UnmanagedType.LPWStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isFloatA(<[In], MarshalAs(UnmanagedType.LPStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isFloatW(<[In], MarshalAs(UnmanagedType.LPWStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isDoubleA(<[In], MarshalAs(UnmanagedType.LPStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isDoubleW(<[In], MarshalAs(UnmanagedType.LPWStr)> str As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_strcatW(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> dest As StringBuilder, len As UInteger, <[In], MarshalAs(UnmanagedType.LPWStr)> src As String) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_strcatA(<[In], Out, MarshalAs(UnmanagedType.LPStr)> dest As StringBuilder, len As UInteger, <[In], MarshalAs(UnmanagedType.LPStr)> src As String) As UInteger
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Sub d_str_to_wstr(<[In], MarshalAs(UnmanagedType.LPStr)> str As String, <[In], Out, MarshalAs(UnmanagedType.LPWStr)> wstr As StringBuilder)
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_strcmpW(<[In], MarshalAs(UnmanagedType.LPWStr)> str1 As String, <[In], MarshalAs(UnmanagedType.LPWStr)> str2 As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_strcmpA(<[In], MarshalAs(UnmanagedType.LPStr)> str1 As String, <[In], MarshalAs(UnmanagedType.LPStr)> str2 As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_stricmpW(<[In], MarshalAs(UnmanagedType.LPWStr)> str1 As String, <[In], MarshalAs(UnmanagedType.LPWStr)> str2 As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_stricmpA(<[In], MarshalAs(UnmanagedType.LPStr)> str1 As String, <[In], MarshalAs(UnmanagedType.LPStr)> str2 As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isDirExist(<[In], MarshalAs(UnmanagedType.LPWStr)> str1 As String, ByRef errorCode As UInteger) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_isFileExist(<[In], MarshalAs(UnmanagedType.LPWStr)> str1 As String, ByRef errorCode As UInteger) As <MarshalAs(UnmanagedType.I1)> Boolean
        ', ...
        <Obsolete("Do not use FormatVarArgsListA function, will not work as expected.")>
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Private Delegate Function d_FormatVarArgsA(<[In], Out, MarshalAs(UnmanagedType.LPStr)> writeTo As StringBuilder, <[In], MarshalAs(UnmanagedType.LPStr)> _Format As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        ', ...
        <Obsolete("Do not use FormatVarArgsListW function, will not work as expected.")>
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Private Delegate Function d_FormatVarArgsW(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> writeTo As StringBuilder, <[In], MarshalAs(UnmanagedType.LPWStr)> _Format As String) As <MarshalAs(UnmanagedType.I1)> Boolean
        <UnmanagedFunctionPointer(CallingConvention.Cdecl)>
        Public Delegate Function d_formatVariantW(<[In], Out, MarshalAs(UnmanagedType.LPWStr)> outputStr As StringBuilder, <[In]> maxOutput As UInteger, <[In], MarshalAs(UnmanagedType.LPWStr)> _Format As String, argTotal As UInteger, <[In], MarshalAs(UnmanagedType.LPArray), [ParamArray]> argList As Object()) As <MarshalAs(UnmanagedType.I1)> Boolean

        ''' <summary>
        ''' Allocate memory.
        ''' </summary>
        ''' <param name="Size">The size of allocate memory need to be used.</param>
        ''' <returns>Return allocate memory.</returns>
        <MarshalAs(UnmanagedType.FunctionPtr)>
        Public m_allocMem As d_allocMem
        ''' <summary>
        ''' Free memory from allocate memory.
        ''' </summary>
        ''' <param name="Address">Pointer of an allocate memory to be free from.</param>
        ''' <returns>No return.</returns>
        Public m_freeMem As d_freeMem
        ''' <summary>
        ''' Convert a string to wide string.
        ''' </summary>
        ''' <param name="charA">String</param>
        ''' <param name="len">Total length to convert from string.</param>
        ''' <param name="charW">Buffered wide string</param>
        ''' <returns>No return value.</returns>
        Public m_toCharW As d_toCharW
        ''' <summary>
        ''' Convert a wide string to string.
        ''' </summary>
        ''' <param name="charW">Wide string</param>
        ''' <param name="len">Total length to convert from wide string.</param>
        ''' <param name="charA">Buffered string</param>
        ''' <returns>No return value.</returns>
        Public m_toCharA As d_toCharA
        ''' <summary>
        ''' Translate a string into boolean.
        ''' </summary>
        ''' <param name="str">String to translate from.</param>
        ''' <returns>Return -1 if string doesn't have a translation to boolean.</returns>
        Public m_strToBooleanA As d_strToBooleanA
        ''' <summary>
        ''' Translate a wide string into boolean.
        ''' </summary>
        ''' <param name="str">Wide string to translate from.</param>
        ''' <returns>Return -1 if string doesn't have a translation to boolean.</returns>
        Public m_strToBooleanW As d_strToBooleanW
        ''' <summary>
        ''' Translate a string into team index.
        ''' </summary>
        ''' <param name="str">String to translate from.</param>
        ''' <returns>Return -1 if string doesn't have a translation to team index.</returns>
        Public m_strToTeamA As d_strToTeamA
        ''' <summary>
        ''' Translate a wide string into team index.
        ''' </summary>
        ''' <param name="str">Wide string to translate from.</param>
        ''' <returns>Return -1 if string doesn't have a translation to team index.</returns>
        Public m_strToTeamW As d_strToTeamW
        ''' <summary>
        ''' Format a current string to support escape characters if any.
        ''' </summary>
        ''' <param name="regStr">String to format escape characters if any.</param>
        ''' <returns>No return value.</returns>
        Public m_replaceA As d_replaceA
        ''' <summary>
        ''' Format a current string to support escape characters if any.
        ''' </summary>
        ''' <param name="regStr">String to format escape characters if any.</param>
        ''' <returns>No return value.</returns>
        Public m_replaceW As d_replaceW
        ''' <summary>
        ''' Undo format a current string to support escape characters if any.
        ''' </summary>
        ''' <param name="regStr">String to undo format escape characters if any.</param>
        ''' <returns>No return value.</returns>
        Public m_replaceUndoA As d_replaceUndoA
        ''' <summary>
        ''' Undo format a current string to support escape characters if any.
        ''' </summary>
        ''' <param name="regStr">String to undo format escape characters if any.</param>
        ''' <returns>No return value.</returns>
        Public m_replaceUndoW As d_replaceUndoW
        ''' <summary>
        ''' Verify if whole string contain digits.
        ''' </summary>
        ''' <param name="str">String to check.</param>
        ''' <returns>Return true if valid.</returns>
        Public m_isNumberA As d_isNumberA
        ''' <summary>
        ''' Verify if whole wide string contain digits.
        ''' </summary>
        ''' <param name="str">Wide string to check.</param>
        ''' <returns>Return true if valid.</returns>
        Public m_isNumberW As d_isNumberW
        ''' <summary>
        ''' Verify if whole string contain characters & digits.
        ''' </summary>
        ''' <param name="str">String to check.</param>
        ''' <returns>Return true if valid.</returns>
        Public m_isHashA As d_isHashA
        ''' <summary>
        ''' Verify if whole wide string contain characters & digits.
        ''' </summary>
        ''' <param name="str">Wide string to check.</param>
        ''' <returns>Return true if valid.</returns>
        Public m_isHashW As d_isHashW
        ''' <summary>
        ''' Move partial of string to left or right.
        ''' </summary>
        ''' <param name="regStr">String to be shift.</param>
        ''' <param name="len">Length of string to be move.</param>
        ''' <param name="pos">Position of the string to be shift.</param>
        ''' <param name="lenShift">Amount of length to shift left or right.</param>
        ''' <param name="leftRight">True for shift to right and false for shift to left.</param>
        ''' <returns>Return true for success, failed if one or more argument is invalid.</returns>
        Public m_shiftStrA As d_shiftStrA
        ''' <summary>
        ''' Move partial of wide string to left or right.
        ''' </summary>
        ''' <param name="regStr">Wide string to be shift.</param>
        ''' <param name="len">Length of wide string to be move.</param>
        ''' <param name="pos">Position of the wide string to be shift.</param>
        ''' <param name="lenShift">Amount of length to shift left or right.</param>
        ''' <param name="leftRight">True for shift to right and false for shift to left.</param>
        ''' <returns>Return true for success, failed if one or more argument is invalid.</returns>
        Public m_shiftStrW As d_shiftStrW
        ''' <summary>
        ''' Format a current string to support escape characters if any.
        ''' </summary>
        ''' <param name="regStr">String to format escape characters if any.</param>
        ''' <param name="isDB">True if goig to use escape characters in database query.</param>
        ''' <returns>No return value.</returns>
        Public m_regexReplaceW As d_regexReplaceW
        ''' <summary>
        ''' Find a regular expression string against source string to be a match.
        ''' </summary>
        ''' <param name="srcStr">Source string</param>
        ''' <param name="regex">Regular expression string</param>
        ''' <returns>Return true if is a match.</returns>
        Public m_regexMatchW As d_regexMatchW
        ''' <summary>
        ''' Find a regular expression string against source string to be a match.
        ''' </summary>
        ''' <param name="srcStr">Source string</param>
        ''' <param name="regex">Regular expression string</param>
        ''' <returns>Return true if is a match.</returns>
        Public m_regexiMatchW As d_regexiMatchW

        ''' <summary>
        ''' (DO NOT USE!) Format variable arguments list into given prefix string.
        ''' </summary>
        ''' <param name="writeTo">Output string</param>
        ''' <param name="_Format">Format message string</param>
        ''' <param name="ArgList">Variable arguments list</param>
        ''' <returns>Return true or false for format completion.</returns>
        Private m_FormatVarArgsListA As d_FormatVarArgsListA
        ''' <summary>
        ''' (DO NOT USE!) Format variable arguments list into given prefix string.
        ''' </summary>
        ''' <param name="writeTo">Output string</param>
        ''' <param name="_Format">Format message string</param>
        ''' <param name="ArgList">Variable arguments list</param>
        ''' <returns>Return true or false for format completion.</returns>
        Private m_FormatVarArgsListW As d_FormatVarArgsListW
        ''' <summary>
        ''' Compare beginning of case-senitive string against another string.
        ''' </summary>
        ''' <param name="src">Source string compare against.</param>
        ''' <param name="find">Find string to use for comparison.</param>
        ''' <returns>Only return true if is a match.</returns>
        Public m_findSubStrFirstA As d_findSubStrFirstA
        ''' <summary>
        ''' Compare beginning of case-senitive string against another string.
        ''' </summary>
        ''' <param name="src">Source string compare against.</param>
        ''' <param name="find">Find string to use for comparison.</param>
        ''' <returns>Only return true if is a match.</returns>
        Public m_findSubStrFirstW As d_findSubStrFirstW

        ''' <summary>
        ''' Test if string contains a letters or not.
        ''' </summary>
        ''' <param name="str">String to test if is a letters.</param>
        ''' <returns>Return true if is letters.</returns>
        Public m_isLettersA As d_isLettersA
        ''' <summary>
        ''' Test if string contains a letters or not.
        ''' </summary>
        ''' <param name="str">String to test if is a letters.</param>
        ''' <returns>Return true if is letters.</returns>
        Public m_isLettersW As d_isLettersW

        ''' <summary>
        ''' Test if string contains a float or not.
        ''' </summary>
        ''' <param name="str">String to test if is a float.</param>
        ''' <returns>Return true if is a float.</returns>
        Public m_isFloatA As d_isFloatA
        ''' <summary>
        ''' Test if string contains a float or not.
        ''' </summary>
        ''' <param name="str">String to test if is a float.</param>
        ''' <returns>Return true if is a float.</returns>
        Public m_isFloatW As d_isFloatW
        ''' <summary>
        ''' Test if string contains a double or not.
        ''' </summary>
        ''' <param name="str">String to test if is a double.</param>
        ''' <returns>Return true if is a double.</returns>
        Public m_isDoubleA As d_isDoubleA
        ''' <summary>
        ''' Test if string contains a double or not.
        ''' </summary>
        ''' <param name="str">String to test if is a double.</param>
        ''' <returns>Return true if is a double.</returns>
        Public m_isDoubleW As d_isDoubleW
        ''' <summary>
        ''' Append an existing string with new string.
        ''' </summary>
        ''' <param name="dest">Destination to write an existing string.</param>
        ''' <param name="len">Maximum size of an dest string.</param>
        ''' <param name="src">New string to copy from.</param>
        ''' <returns>Return 1 every time.</returns>
        Public m_strcatW As d_strcatW
        ''' <summary>
        ''' Append an existing string with new string.
        ''' </summary>
        ''' <param name="dest">Destination to write an existing string.</param>
        ''' <param name="len">Maximum size of an dest string.</param>
        ''' <param name="src">New string to copy from.</param>
        ''' <returns>Return 1 every time.</returns>
        Public m_strcatA As d_strcatA
        ''' <summary>
        ''' Convert a string to wide string.
        ''' </summary>
        ''' <param name="str">String</param>
        ''' <param name="wstr">Buffered wide string./param>
        ''' <returns>No return value.</returns>
        Public m_str_to_wstr As d_str_to_wstr
        ''' <summary>
        ''' Case-senitive string to compare against another string..
        ''' </summary>
        ''' <param name="str1">String #1 to compare against.</param>
        ''' <param name="str2">String #2 to compare against.</param>
        ''' <returns>Only return true if is a match.</returns>
        Public m_strcmpW As d_strcmpW
        ''' <summary>
        ''' Case-senitive string to compare against another string..
        ''' </summary>
        ''' <param name="str1">String #1 to compare against.</param>
        ''' <param name="str2">String #2 to compare against.</param>
        ''' <returns>Only return true if is a match.</returns>
        Public m_strcmpA As d_strcmpA
        ''' <summary>
        ''' Case-insenitive string to compare against another string.
        ''' </summary>
        ''' <param name="str1">String #1 to compare against.</param>
        ''' <param name="str2">String #2 to compare against.</param>
        ''' <returns>Only return true if is a match.</returns>
        Public m_stricmpW As d_stricmpW
        ''' <summary>
        ''' Case-insenitive string to compare against another string.
        ''' </summary>
        ''' <param name="str1">String #1 to compare against.</param>
        ''' <param name="str2">String #2 to compare against.</param>
        ''' <returns>Only return true if is a match.</returns>
        Public m_stricmpA As d_stricmpA
        ''' <summary>
        ''' Check if a directory exist.
        ''' </summary>
        ''' <param name="pathStr">Must have directory name.</param>
        ''' <param name="errorCode">Given error code if failed.</param>
        ''' <returns>Return true if directory exist, false with given errorCode.</returns>
        Public m_isDirExist As d_isDirExist
        ''' <summary>
        ''' Check if a file exist.
        ''' </summary>
        ''' <param name="pathStr">Must have directory (optional) and file name.</param>
        ''' <param name="errorCode">Given error code if failed.</param>
        ''' <returns>Return true if file exist, false with given errorCode.</returns>
        Public m_isFileExist As d_isFileExist
        ''' <summary>
        ''' (DO NOT USE!) Format variable arguments into given prefix string.
        ''' </summary>
        ''' <param name="writeTo">Output string</param>
        ''' <param name="_Format">Format message string</param>
        ''' <param name="...">Variable arguments</param>
        ''' <returns>Return true or false for format completion.</returns>
        Private m_FormatVarArgsA As d_FormatVarArgsA
        ''' <summary>
        ''' (DO NOT USE!) Format variable arguments into given prefix string.
        ''' </summary>
        ''' <param name="writeTo">Output string</param>
        ''' <param name="_Format">Format message string</param>
        ''' <param name="...">Variable arguments</param>
        ''' <returns>Return true or false for format completion.</returns>
        Private m_FormatVarArgsW As d_FormatVarArgsW
        ''' <summary>
        ''' Format variant arguments into a custom prefix string.
        ''' </summary>
        ''' <param name="outputStr">Output string</param>
        ''' <param name="maxOutput">Maximum size of output string.</param>
        ''' <param name="_Format">Format custom message string</param>
        ''' <param name="argTotal">Must be equivalent to argList's total of arrays.</param>
        ''' <param name="argList">Variant arguments in array format.</param>
        ''' <returns>Return true or false for format completion.</returns>
        Public m_formatVariantW As d_formatVariantW

        'Simple & easier user-defined conversion + checker for null.
        Public Shared Widening Operator CType(data As IUtilPtr) As IUtil
            If data.ptr <> IntPtr.Zero Then
                Return CType(Marshal.PtrToStructure(data.ptr, GetType(IUtil)), IUtil) 'New IUtil(data)
            Else
                Return New IUtil
            End If
        End Operator
        Public Function isNotNull() As Boolean
            Return m_allocMem IsNot Nothing
        End Function
    End Structure
    Partial Public Structure [Global]
        Public Shared pIUtil As IUtil
    End Structure
    Partial Public Structure [Interface]
        ''' <summary>
        ''' Returns a IUtil class-like to add support for later execution when needed.
        ''' </summary>
        ''' <param name="uniqueHash">Unique hash can be obtain from EXTOnEAOLoad</param>
        ''' <returns>Pointer of IUtil class-like.</returns>
        <DllImport("H-Ext.dll", EntryPoint:="#19", CallingConvention:=CallingConvention.Cdecl)>
        <ComVisible(True)>
        Public Shared Function getIUtil(<[In]> uniqueHash As UInteger) As IUtilPtr
        End Function
    End Structure
#End If
End Namespace
