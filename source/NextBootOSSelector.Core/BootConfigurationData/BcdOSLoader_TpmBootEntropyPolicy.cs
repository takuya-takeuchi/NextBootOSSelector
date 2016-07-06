using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("58535AB6-31DA-43FE-8D87-3DE008476916")]
    public enum BcdOSLoader_TpmBootEntropyPolicy
    {
        TpmBootEntropyPolicyDefault,
        TpmBootEntropyPolicyForceDisable,
        TpmBootEntropyPolicyForceEnable
    }
}
