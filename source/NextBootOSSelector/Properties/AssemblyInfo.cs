using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Media;
using Ouranos.NextBootOSSelector.Resource;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(AssemblyProperties.Title)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(AssemblyProperties.Company)]
[assembly: AssemblyProduct(AssemblyProperties.Product)]
[assembly: AssemblyCopyright(AssemblyProperties.Copyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("76fa3be9-ff9d-4b4b-a743-4cc53b198d6e")]

// required to support per-monitor DPI awareness in Windows 8.1+
// see also https://mui.codeplex.com/wikipage?title=Per-monitor%20DPI%20awareness
[assembly: DisableDpiAwareness]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(AssemblyProperties.Version)]
[assembly: AssemblyFileVersion(AssemblyProperties.FileVersion)]
