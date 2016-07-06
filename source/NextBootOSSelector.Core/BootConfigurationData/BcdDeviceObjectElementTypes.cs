using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("8CF31856-D37B-4FD3-80DA-86952054D7A6")]
    public enum BcdDeviceObjectElementTypes
    {
        BcdDeviceBoolean_ExportAsCdRamdiskImageOffset = 0x36000006,
        BcdDeviceDevice_SdiDevice = 0x31000003,
        BcdDeviceInteger_MulticastEnabled = 0x36000009,
        BcdDeviceInteger_MulticastTftpFallback = 0x3600000a,
        BcdDeviceInteger_RamdiskImageLength = 0x35000005,
        BcdDeviceInteger_RamdiskImageOffset = 0x35000001,
        BcdDeviceInteger_TftpBlockSize = 0x35000007,
        BcdDeviceInteger_TftpClientPort = 0x35000002,
        BcdDeviceInteger_TftpWindowSize = 0x35000008,
        BcdDeviceString_SdiPath = 0x32000004
    }
}