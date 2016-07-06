using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("33C36119-64F2-48C0-A867-BD068A78ABCD")]
    public enum BcdResumeElementTypes
    {
        BcdResumeBoolean_DebugOptionEnabled = 0x26000006,
        BcdResumeBoolean_UseCustomSettings = 0x26000003,
        BcdResumeBoolean_x86PaeMode = 0x26000004,
        BcdResumeDevice_AssociatedOsDevice = 0x21000005,
        BcdResumeDevice_HiberFileDevice = 0x21000001,
        BcdResumeInteger_BootUxPolicy = 0x25000007,
        BcdResumeString_HiberFilePath = 0x22000002
    }
}