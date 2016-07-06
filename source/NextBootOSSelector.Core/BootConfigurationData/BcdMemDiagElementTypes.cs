using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("934CE6DA-8242-493E-B843-E781C96BD98D")]
    public enum BcdMemDiagElementTypes
    {
        BcdMemDiagBoolean_Cacheenable = 0x26000005,
        BcdMemDiagInteger_FailureCount = 0x25000003,
        BcdMemDiagInteger_PassCount = 0x25000001,
        BcdMemDiagInteger_TestMix = 0x25000002,
        BcdMemDiagInteger_TestToFail = 0x25000004
    }
}