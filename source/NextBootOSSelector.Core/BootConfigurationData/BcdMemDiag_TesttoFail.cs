using System.Runtime.InteropServices;

namespace Ouranos.NextBootOSSelector.BootConfigurationData
{
    [Guid("045979A6-B9D1-4809-B8C9-7FEDF7BE8ABA")]
    public enum BcdMemDiag_TesttoFail
    {
        MemtestStride,
        MemtestMats,
        MemtestInverseCoupling,
        MemtestRandomPattern,
        MemtestCheckerboard
    }
}