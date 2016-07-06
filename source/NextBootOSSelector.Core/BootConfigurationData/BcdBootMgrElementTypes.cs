using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("48ECC24D-FC20-44A7-B616-710B589CD21B")]
    public enum BcdBootMgrElementTypes
    {
        BcdBootMgrBoolean_AttemptResume = 0x26000005,
        BcdBootMgrBoolean_DisplayBootMenu = 0x26000020,
        BcdBootMgrBoolean_NoErrorDisplay = 0x26000021,
        BcdBootMgrDevice_BcdDevice = 0x21000022,
        BcdBootMgrInteger_Timeout = 0x25000004,
        BcdBootMgrObject_DefaultObject = 0x23000003,
        BcdBootMgrObject_ResumeObject = 0x23000006,
        BcdBootMgrObjectList_BootSequence = 0x24000002,
        BcdBootMgrObjectList_CustomActions = 0x27000030,
        BcdBootMgrObjectList_DisplayOrder = 0x24000001,
        BcdBootMgrObjectList_ToolsDisplayOrder = 0x24000010,
        BcdBootMgrString_BcdFilePath = 0x22000023
    }
}