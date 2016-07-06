using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("C7C65422-E94F-47AC-906B-AB655E1BE5DA")]
    public enum BcdOSLoader_SafeBoot
    {
        SafemodeMinimal,
        SafemodeNetwork,
        SafemodeDsRepair
    }
}