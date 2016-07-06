using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("593F0515-0A2B-40F2-AFA5-D25882E744D7")]
    public enum BcdOSLoader_DriverLoadFailurePolicy
    {
        DriverLoadFailurePolicyFatal,
        DriverLoadFailurePolicyUseErrorControl
    }
}