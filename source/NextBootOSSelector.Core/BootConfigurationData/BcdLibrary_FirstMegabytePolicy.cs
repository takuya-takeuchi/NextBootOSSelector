using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("23B9871B-25C4-45D9-8510-62D894C8CD9B")]
    public enum BcdLibrary_FirstMegabytePolicy
    {
        FirstMegabytePolicyUseNone,
        FirstMegabytePolicyUseAll,
        FirstMegabytePolicyUsePrivate
    }
}