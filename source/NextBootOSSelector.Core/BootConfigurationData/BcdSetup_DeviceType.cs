using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("58262605-0332-4775-ADE2-233C712AC121")]
    public enum BcdSetup_DeviceType
    {
        NULL_ENUM_VALUE,
        DeviceBootPartition,
        DeviceWindowsPartition,
        DeviceRamdiskBootPartition,
        DeviceRamdiskWindowsPartition
    }
}