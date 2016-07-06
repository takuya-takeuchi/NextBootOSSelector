using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("8D2EEEC8-B78F-40BD-99E8-0BCB8883AFA1")]
    public enum BcdOSLoader_BootStatusPolicy
    {
        BootStatusPolicyDisplayAllFailures,
        BootStatusPolicyIgnoreAllFailures,
        BootStatusPolicyIgnoreShutdownFailures,
        BootStatusPolicyIgnoreBootFailures
    }
}