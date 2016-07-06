using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("BBDF9516-5FD7-46A3-8BAE-C7BBE13191C8")]
    public enum BcdOSLoader_X2APICPolicy
    {
        X2APICPolicyDefault,
        X2APICPolicyDisable,
        X2APICPolicyEnable
    }
}