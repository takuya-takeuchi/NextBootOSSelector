using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("3DDDE433-7343-4B67-B5E7-F66742892B6B")]
    public enum BcdSetupTemplateElementTypes
    {
        BcdSetupBoolean_OmitOsLoaderElements = 0x46000004,
        BcdSetupBoolean_RecoveryOs = 0x46000010,
        BcdSetupInteger_DeviceType = 0x45000001,
        BcdSetupString_ApplicationRelativePath = 0x42000002,
        BcdSetupString_RamdiskDeviceRelativePath = 0x42000003
    }
}