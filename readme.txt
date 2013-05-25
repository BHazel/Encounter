ENCOUNTER 2013-05-25, 0008 "NORTHCOTE" README
=============================================

Encounter is a simple program for calculating the interaction energy between two molecules using the
output from a GAUSSIAN two-component counterpoise correction calculation.

The Counterpoise Correction arises due to the Basis Set Superposition Error in quantum modeling.

Installation
============

Encounter comes in 3 language implementations: C++/Qt, Java, and .NET/C#. For detailed installation
instructions, please see the wiki at http://encounter.codeplex.com.

C++/Qt (Encounter)
------------------

Location: src/cpp

You require the Qt Framework 4 or newer installed to use this implementation.

To install, load the Qt Creator project and build. Alternatively, you can compile from the command-line
using the following commands from the project directory:

    qmake
    make

Java (JCounterpoise)
--------------------

Location: src/java

You require the Java Development Kit SE 6 or newer installed to use this implementation.

To install on a Unix system, you can use the configuration script to compile and install. Open the
configure file and edit the variables marked at the top of file. Then run the following commands to
install:

    configure
    make

.NET/C# (Sharpen)
-----------------

Location: src/cs

You require the .NET Framework 2.0 or newer and, ideally, Visual Studio installed to use
this implementation.

To install, load the Visual Studio project and build.

Objective-C/Cocoa (Xen)
-----------------------

Location: src/objc

You require Mac OS X 10.7 "Lion"and Xcode 4 installed to use this implementation.

To install, load the Xcode project and build.

Samples
=======

A sample calculation is included with the source code in the samples directory.