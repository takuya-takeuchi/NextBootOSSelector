using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("F2022D3F-E2CF-4C4A-ABDF-2FF23CEB1384")]
    public enum BcdOSLoader_NxPolicy
    {
        NxPolicyOptIn,
        NxPolicyOptOut,
        NxPolicyAlwaysOff,
        NxPolicyAlwaysOn
    }
}