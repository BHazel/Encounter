Encounter
=========

**A new version of Encounter is under development based on Node.js and Electron.  The repository will be available here on GitHub shortly.**

![Encounter](wiki/encounter.png)

Encounter is a simple program for calculating the interaction energy between two molecules using the output from a GAUSSIAN two-component counterpoise correction calculation.

The Counterpoise Correction arises due to the Basis Set Superposition Error in quantum modeling.

CI Status
---------

[![Build Status](https://travis-ci.org/BHazel/Encounter.svg?branch=master)](https://travis-ci.org/BHazel/Encounter) C++/Qt

**No CI** Java

[![Build status](https://ci.appveyor.com/api/projects/status/66sepbt149a3ttkn?svg=true)](https://ci.appveyor.com/project/BHazel/encounter) .NET/C#

**No CI** Objective-C/Cocoa

Build & Usage
-------------

Encounter comes in 4 different languages:

* C++/Qt (Encounter)
* Java (JCounterpoise)
* .NET/C# (Sharpen)
* Objective-C/Cocoa (Xen)

All source code can be found in the _1.0-northcote_ directory.

Please see the [wiki](https://github.com/BHazel/Encounter/wiki) for detailed build and usage instructions.

A sample GAUSSIAN counterpoise correction calculation is included in the _samples_ directory.
