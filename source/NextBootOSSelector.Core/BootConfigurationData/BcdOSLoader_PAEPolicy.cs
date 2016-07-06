using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("3E0B67FA-9545-4D18-94A0-5F8A10DE957E")]
    public enum BcdOSLoader_PAEPolicy
    {
        PaePolicyDefault,
        PaePolicyForceEnable,
        PaePolicyForceDisable
    }
}