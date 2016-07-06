using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("BF9A555C-5298-4B64-B21F-77D045974D18")]
    public enum BcdLibrary_SiPolicy
    {
        IntegrityServicesDefault,
        IntegrityServicesEnable,
        IntegrityServicesDisable
    }
}