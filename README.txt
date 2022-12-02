
What is iPdfWriter?
====================

iPdfWriter is a lightweight implementation that allows modifying a pdf document totally or partially by replacing tags.


Changes in this version (v1.0.6)
================================

· Added
  -----

   Now uses iPdfWriter.Abstractions.

· Changed
  -------

    - I M P O R T A N T !!!
   
      Changed the iTin.Utilities.Pdf.Writer namespace to iPdfWriter

    - Rename test folder for samples folder

    - Library versions for this version

    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | Library                                           Version   Description                                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iPdfWriter                                        1.0.0.6   Pdf writer                                                            |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core                                         2.0.0.6   Base library containing various extensions, helpers, common constants |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Drawing                                 1.0.0.3   Drawing objects, extension, helpers, common constants                 |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Abstractions                   1.0.0.0   Generic Common Hardware Abstractions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Common                         1.0.0.5   Common Hardware Infrastructure                                        |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Linux.Devices.Graphics.Font    1.0.0.1   Linux Hardware Infrastructure                                         |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.MacOS.Devices.Graphics.Font    1.0.0.1   MacOS Hardware Infrastructure                                         |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Windows.Devices.Graphics.Font  1.0.0.1   Windows Hardware Infrastructure                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Interop.Shared                          1.0.0.4   Generic Shared Interop Definitions                                    |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Interop.Windows.Devices                 1.0.0.1   Win32 Generic Interop Calls                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO                                      1.0.0.3   Common I/O calls                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO.Compression                          1.0.0.3   Compression library                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models                                  1.0.0.3   Data models base                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Charting                  1.0.0.3   Base charting models                                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Styling                   1.0.0.3   Base styling models                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Logging                                      1.0.0.2   Logging library                                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Hardware.Abstractions.Devices.Graphics.Font  1.0.0.1   Generic Common Hardware Abstractions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Registry.Windows                             1.0.0.3   Windows registry acces                                                |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Design                         1.0.0.5   Pdf design elements                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•

· Remove
  -----

    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | Library                                           Version   Description                                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Abstractions.Writer                1.0.0.0   Generic Common Writer's Abstractions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•

v1.0.5
======

· Added
  -----
    - Add support for asynchronous calls in solution projects.

    - Added support for using Span<T> for supported platforms.

    - Test projects to show asynchronous usage.
 
    - The functionality of being able to create an instance of the PdfInput class from HTML code has been added.
      For more information, please see sample25.cs

    - A functionality has been added that allows to add or modify the metadata information of a pdf file.
      For more information, please see please see sample26.cs

    - A functionality has been added that allows to add a password to pdf file.
      For more information, please see please see sample27.cs

    - A functionality has been added that allows to insert an image into a pdf file
      For more information, please see please see sample28.cs

· Changed
  -------

    - Library versions for this version

    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | Library                                           Version   Description                                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core                                         2.0.0.6   Base library containing various extensions, helpers, common constants |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Drawing                                 1.0.0.3   Drawing objects, extension, helpers, common constants                 |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Abstractions                   1.0.0.0   Generic Common Hardware Abstractions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Common                         1.0.0.5   Common Hardware Infrastructure                                        |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Linux.Devices.Graphics.Font    1.0.0.1   Linux Hardware Infrastructure                                         |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.MacOS.Devices.Graphics.Font    1.0.0.1   MacOS Hardware Infrastructure                                         |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Windows.Devices.Graphics.Font  1.0.0.1   Windows Hardware Infrastructure                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Interop.Shared                          1.0.0.4   Generic Shared Interop Definitions                                    |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Interop.Windows.Devices                 1.0.0.1   Win32 Generic Interop Calls                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO                                      1.0.0.3   Common I/O calls                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO.Compression                          1.0.0.3   Compression library                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models                                  1.0.0.3   Data models base                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Charting                  1.0.0.3   Base charting models                                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Styling                   1.0.0.3   Base styling models                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Logging                                      1.0.0.2   Logging library                                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Hardware.Abstractions.Devices.Graphics.Font  1.0.0.1   Generic Common Hardware Abstractions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Registry.Windows                             1.0.0.3   Windows registry acces                                                |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Abstractions.Writer                1.0.0.0   Generic Common Writer's Abstractions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Design                         1.0.0.5   Pdf design elements                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Writer                         1.0.0.4   Pdf writer                                                            |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•

v1.0.4
======

· Added
  -----

    - Add TextLines method to PdfInput class for get the lines of text for an PdfInput, optionally you can set both the start and end pages and a value indicating whether blank lines are included in the result 
      or uses a predicate for filtering.

· Changed
  -------

    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | Library                                           Version   Description                                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core                                         2.0.0.4   Base library containing various extensions, helpers, common constants |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Drawing                                 1.0.0.2   Drawing objects, extension, helpers, common constants                 |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Common                         1.0.0.3   Common Hardware Infrastructure                                        |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Linux.Devices.Graphics.Font    1.0.0.0   Linux Hardware Infrastructure                                         |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.MacOS.Devices.Graphics.Font    1.0.0.0   MacOS Hardware Infrastructure                                         |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Windows.Devices.Graphics.Font  1.0.0.0   Windows Hardware Infrastructure                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO                                      1.0.0.1   Common I/O calls                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO.Compression                          1.0.0.1   Compression library                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Interop.Shared                          1.0.0.2   Generic Shared Interop Definitions                                    |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Interop.Windows.Devices                 1.0.0.0   Win32 Generic Interop Calls                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models                                  1.0.0.2   Data models base                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Charting                  1.0.0.2   Base charting models                                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Styling                   1.0.0.2   Base styling models                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Logging                                      1.0.0.1   Logging library                                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Hardware.Abstractions.Devices.Graphics.Font  1.0.0.0   Generic Common Hardware Abstractions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Registry.Windows                             1.0.0.2   Windows registry acces                                                |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Design                         1.0.0.4   Pdf design elements                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Writer                         1.0.0.3   Pdf writer                                                            |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•

v1.0.3
======

· Fixes
  -----

    - Fixes an error that occurred when trying to load a style from file and this file does not have any extension.

· Added
  -----
    
    - Add support for netstandard2.1
 
        - Add SplitEnumerator ref struct.
   
        - Add support for the use of the ~ character in the iTin.Core.IO library

        - ByteReader class rewritten to work with Span in net core projects.

    - Add sample project for net60

    - Added the ability to create a table from a typed enumerable and from a datatable 
        To do this, two new methods have been added to the PdfTable static class.
        There are two overloads for each method, one to render the table applying **ccs** styles, this functionality is implemented and the other rendering the table natively, 
        it is currently under development
     
        •—————————————————————————————————————————————————————————————————————————————————————————•
        | Method                                Description                                       |
        •—————————————————————————————————————————————————————————————————————————————————————————•
        | PdfTable.CreateFromEnumerable(..)     please see [sample16.cs], [sample17.cs]           |
        •—————————————————————————————————————————————————————————————————————————————————————————•
        | PdfTable.CreateFromDataTable(..)      please see [sample18.cs], [sample19.cs]           |
        •—————————————————————————————————————————————————————————————————————————————————————————•

    - Unify calls to handle font from file, currently this functionality is only available for Windows systems, 
      The logic of each platform is in its own assembly iTin.Core.Hardware.*Target-System*.Devices.Graphics.Font.

        Where:
        ------
        Target-System, it can be Linux, Windows or MacOS and the platform independent logic is found in the iTin.Hardware.Abstractions.Devices.Graphics.Font assembly, 
        so that a call is made independent of the target platform and this assembly has the responsibility of managing the final call to the platform destination.
        
    - Add ExtractPages method to PdfInput class for extract pages from an input.
    - Add NumberOfPages method to PdfInput class, for get number of pages of an input.

· Changed
  -------

    - The way to render the replacements has been rewritten to achieve higher processing speeds, now we should notice an improvement in general times when processing a file.
  
    - Renamed TextOffet, ImageOffset and TableOffset properties for Offset property. 

    - Change license type to GNU

    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | Library                                           Version   Description                                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core                                         2.0.0.4   Base library containing various extensions, helpers, common constants |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Drawing                                 1.0.0.2   Drawing objects, extension, helpers, common constants                 |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Common                         1.0.0.3   Common Hardware Infrastructure                                        |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Linux.Devices.Graphics.Font    1.0.0.0   Linux Hardware Infrastructure                                         |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.MacOS.Devices.Graphics.Font    1.0.0.0   MacOS Hardware Infrastructure                                         |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Hardware.Windows.Devices.Graphics.Font  1.0.0.0   Windows Hardware Infrastructure                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO                                      1.0.0.1   Common I/O calls                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO.Compression                          1.0.0.1   Compression library                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Interop.Shared                          1.0.0.2   Generic Shared Interop Definitions                                    |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Interop.Windows.Devices                 1.0.0.0   Win32 Generic Interop Calls                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models                                  1.0.0.2   Data models base                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Charting                  1.0.0.2   Base charting models                                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Styling                   1.0.0.2   Base styling models                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Logging                                      1.0.0.1   Logging library                                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Hardware.Abstractions.Devices.Graphics.Font  1.0.0.0   Generic Common Hardware Abstractions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Registry.Windows                             1.0.0.2   Windows registry acces                                                |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Design                         1.0.0.3   Pdf design elements                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Writer                         1.0.0.2   Pdf writer                                                            |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•

v1.0.2
======

· Critical
  --------

    - Important!!!

      Fixes an error caused in the previous version, the nuget packages were not updated correctly when creating the version

                                                I'm sorry for the inconveniences..

· Fixes
  -----

    - Upgrade Newtonsoft.Json nuget package to version 13.0.1 (without critical errors)

    - Fix an error that occurs when trying to create an html table with html5 tags (not supported)

    - Library versions for this version

· Changed
  -------

    - Change license type to GNU

    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | Library                               Version   Description                                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core                             2.0.0.3   Base library containing various extensions, helpers, common constants |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Drawing                     1.0.0.1   Drawing objects, extension, helpers, common constants                 |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO                          1.0.0.1   Common I/O calls                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO.Compression              1.0.0.1   Compression library                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models                      1.0.0.1   Data models base                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Charting      1.0.0.1   Base charting models                                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Styling       1.0.0.1   Base styling models                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Logging                          1.0.0.0   Logging library                                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Registry.Windows                 1.0.0.1   Common classes for model definitions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Design             1.0.0.2   Pdf design elements                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Writer             1.0.0.1   Pdf writer                                                            |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•


v1.0.1
======

· Added
  -----

    - Library documentation.

    - tools folder in solution root. Contains a script for update help md files.

· Changed
  -------

    - Changed IResultGeneric interface. Changed Value property name by Result (for code clarify).

      · This change may have implications in your final code, it is resolved by changing Value to Result

    - Update result classes for support more scenaries.

    - Library versions for this version

    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | Library                               Version   Description                                                           |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core                             2.0.0.3   Base library containing various extensions, helpers, common constants |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Drawing                     1.0.0.1   Drawing objects, extension, helpers, common constants                 |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO                          1.0.0.1   Common I/O calls                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO.Compression              1.0.0.1   Compression library                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models                      1.0.0.1   Data models base                                                      |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Charting      1.0.0.1   Base charting models                                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Styling       1.0.0.1   Base styling models                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Logging                          1.0.0.0   Logging library                                                       |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Registry.Windows                 1.0.0.1   Common classes for model definitions                                  |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Design             1.0.0.1   Pdf design elements                                                   |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Writer             1.0.0.1   Pdf writer                                                            |
    •———————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•


v1.0.0
======

· Added
  -----

    - Library versions for this version

    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | Library                               Version     Description                                                           |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core                             2.0.0.0     Base library containing various extensions, helpers, common constants |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Drawing                     1.0.0.0     Drawing objects, extension, helpers, common constants                 |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO                          1.0.0.0     Common I/O calls                                                      |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.IO.Compression              1.0.0.0     Compression library                                                   |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models                      1.0.0.0     Data models base                                                      |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Charting      1.0.0.0     Base charting models                                                  |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Core.Models.Design.Styling       1.0.0.0     Base styling models                                                   |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Logging                          1.0.0.0     Logging library                                                       |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Registry.Windows                 1.0.0.0     Common classes for model definitions                                  |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Design             1.0.0.0     Pdf design elements                                                   |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•
    | iTin.Utilities.Pdf.Writer             1.0.0.0     Pdf writer                                                            |
    •—————————————————————————————————————————————————————————————————————————————————————————————————————————————————————————•


Install via NuGet
=================

PM> Install-Package iPdfWriter

For more information, please see https://www.nuget.org/packages/iPdfWriter/


Documentation
=============

 - For Writer code documentation, please see next link https://github.com/iAJTin/iPdfWriter/blob/master/documentation/iTin.Utilities.Pdf.Writer.md


Usage
=====

Examples
--------

Coming soon!!!
