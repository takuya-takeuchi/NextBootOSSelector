using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("515F1C87-65DE-4E4C-953D-0612C9876EDA")]
    public enum BcdLibrary_DebuggerStartPolicy
    {
        DebuggerStartActive,
        DebuggerStartAutoEnable,
        DebuggerStartDisable
    }
}