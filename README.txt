
What is iPdf?
================

iPdf is a lightweight implementation that allows us to obtain the iPdf information

Library versions
================

Library versions for current iSMBIOS version (1.1.3)

•———————————————————————————————————————————————————————————————————————————————————————————•
| Library                                  Version      Description                         |
•———————————————————————————————————————————————————————————————————————————————————————————•
|iTin.Core                                 1.0.1.0		Common calls                        |
•———————————————————————————————————————————————————————————————————————————————————————————•
|iTin.Core.Interop                         1.0.0.0		Interop calls                       |
•———————————————————————————————————————————————————————————————————————————————————————————•
|iTin.Core.Hardware                        1.0.1.0		Hardware Interop Calls              |
•———————————————————————————————————————————————————————————————————————————————————————————•
|iTin.Core.Hardware.Specification.Dmi      3.3.0.1		DMI Specification Implementation    |
•———————————————————————————————————————————————————————————————————————————————————————————•
|iTin.Core.Hardware.Specification.Smbios   3.3.0.2		SMBIOS Specification Implementation |
•———————————————————————————————————————————————————————————————————————————————————————————•
|iTin.Core.Hardware.Specification.Tpm      1.0.0.0		TPM Specification Implementation    |
•———————————————————————————————————————————————————————————————————————————————————————————•

Install via NuGet
=================

For more information, please see https://www.nuget.org/packages/iPdf/

Usage
=====

Call DMI.Instance.Structures for getting all SMBIOS structures availables.

Examples
--------

1. Gets and prints SMBIOS version.

       Console.WriteLine($@" SMBIOS Version > {DMI.Instance.SmbiosVersion}");

2. Gets and prints all **SMBIOS** availables structures.

       DmiStructureCollection structures = DMI.Instance.Structures;
       foreach (DmiStructure structure in structures)
       {
           Console.WriteLine($@" {(int)structure.Class:D3}-{structure.FriendlyClassName}");

           int totalStructures = structure.Elements.Count;
           if (totalStructures > 1)
           {
               Console.WriteLine($@"     > {totalStructures} structures");
           }
       }
