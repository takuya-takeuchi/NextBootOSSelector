using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("384DA285-55B5-414F-8EC7-165E6ABA2655")]
    public enum BcdLibraryElementTypes
    {
        BcdLibraryBoolean_AllowBadMemoryAccess = 0x1600000b,
        BcdLibraryBoolean_AllowPrereleaseSignatures = 0x16000049,
        BcdLibraryBoolean_AttemptNonBcdStart = 0x16000031,
        BcdLibraryBoolean_AutoRecoveryEnabled = 0x16000009,
        BcdLibraryBoolean_ConsoleExtendedInput = 0x16000050,
        BcdLibraryBoolean_DebuggerEnabled = 0x16000010,
        BcdLibraryBoolean_DebuggerIgnoreUsermodeExceptions = 0x16000017,
        BcdLibraryBoolean_DisableIntegrityChecks = 0x16000048,
        BcdLibraryBoolean_DisplayAdvancedOptions = 0x16000040,
        BcdLibraryBoolean_DisplayOptionsEdit = 0x16000041,
        BcdLibraryBoolean_EmsEnabled = 0x16000020,
        BcdLibraryBoolean_GraphicsModeDisabled = 0x16000046,
        BcdLibraryBoolean_TraditionalKsegMappings = 0x1600000f,
        BcdLibraryDevice_ApplicationDevice = 0x11000001,
        BcdLibraryInteger_1394DebuggerChannel = 0x15000015,
        BcdLibraryInteger_AvoidLowPhysicalMemory = 0x1500000e,
        BcdLibraryInteger_ConfigAccessPolicy = 0x15000047,
        BcdLibraryInteger_DebuggerStartPolicy = 0x15000018,
        BcdLibraryInteger_DebuggerType = 0x15000011,
        BcdLibraryInteger_EmsBaudRate = 0x15000023,
        BcdLibraryInteger_EmsPort = 0x15000022,
        BcdLibraryInteger_FirstMegabytePolicy = 0x1500000c,
        BcdLibraryInteger_FVEKeyRingAddress = 0x15000042,
        BcdLibraryInteger_GraphicsResolution = 0x15000052,
        BcdLibraryInteger_InitialConsoleInput = 0x15000051,
        BcdLibraryInteger_RelocatePhysicalMemory = 0x1500000d,
        BcdLibraryInteger_SerialDebuggerBaudRate = 0x15000014,
        BcdLibraryInteger_SerialDebuggerPort = 0x15000013,
        BcdLibraryInteger_SerialDebuggerPortAddress = 0x15000012,
        BcdLibraryInteger_SiPolicy = 0x1500004b,
        BcdLibraryInteger_TruncatePhysicalMemory = 0x15000007,
        BcdLibraryIntegerList_BadMemoryList = 0x1700000a,
        BcdLibraryObjectList_InheritedObjects = 0x14000006,
        BcdLibraryObjectList_RecoverySequence = 0x14000008,
        BcdLibraryString_ApplicationPath = 0x12000002,
        BcdLibraryString_DebuggerBusParameter = 0x12000019,
        BcdLibraryString_Description = 0x12000004,
        BcdLibraryString_FontPath = 0x1200004a,
        BcdLibraryString_LoadOptionsString = 0x12000030,
        BcdLibraryString_PreferredLocale = 0x12000005,
        BcdLibraryString_UsbDebuggerTargetName = 0x12000016
    }
}