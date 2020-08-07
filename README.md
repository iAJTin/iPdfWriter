<p align="center">
  <img src="https://cdn.rawgit.com/iAJTin/iPdf/master/nuget/iPdf.png"  
       height="32"/>
</p>
<p align="center">
  <a href="https://github.com/iAJTin/iPdf">
    <img src="https://img.shields.io/badge/iTin-iPdf-green.svg?style=flat"/>
  </a>
</p>

***

# What is iPdf?
iPdf is a lightweight implementation that allows us to obtain the SMBIOS information

# Install via NuGet

<table>
  <tr>
    <td>
      <a href="https://github.com/iAJTin/iPdf">
        <img src="https://img.shields.io/badge/-iPdf-green.svg?style=flat"/>
      </a>
    </td>
    <td>
      <a href="https://www.nuget.org/packages/iPdf/">
        <img alt="NuGet Version" 
             src="https://img.shields.io/nuget/v/iPdf.svg" /> 
      </a>
    </td>  
  </tr>
</table>

# Usage

Call **DMI.Instance.Structures** for getting all SMBIOS structures availables.

## Examples

1. Gets and prints **SMBIOS** version.

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

# How can I send feedback!!!

If you have found **iPdf** useful at work or in a personal project, I would love to hear about it. If you have decided not to use **iPdf**, please send me and email stating why this is so. I will use this feedback to improve **iPdf** in future releases.

My email address is fdo.garcia.vega@gmail.com
