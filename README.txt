
What is iPdfWriter?
====================

iPdfWriter is a lightweight implementation that allows modifying a pdf document totally or partially by replacing tags


Changes in this version 1.0.2
=============================

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
