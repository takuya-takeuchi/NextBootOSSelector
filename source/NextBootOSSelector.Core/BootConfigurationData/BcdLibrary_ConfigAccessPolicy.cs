using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("EC8883F8-90B9-417B-A7A7-4DB599639FE6")]
    public enum BcdLibrary_ConfigAccessPolicy
    {
        ConfigAccessPolicyDefault,
        ConfigAccessPolicyDisallowMmConfig
    }
}