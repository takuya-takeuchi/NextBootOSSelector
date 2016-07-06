using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("CE902CA0-13A5-48C7-910E-87B399C488B1")]
    public enum BcdLibrary_DebuggerType
    {
        DebuggerSerial,
        Debugger1394,
        DebuggerUsb
    }
}