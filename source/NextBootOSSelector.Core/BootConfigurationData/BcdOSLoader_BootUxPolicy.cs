using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("958963FA-10B0-4468-BAEC-F62E5511EADB")]
    public enum BcdOSLoader_BootUxPolicy
    {
        BgPolicyDisabled,
        BgPolicyBasic,
        BgPolicyStandard
    }
}