**These build instructions are for the source code found in the _1.0-northocte_ directory.**

C++/Qt _(Encounter)_
--------------------

### Requirements

* Qt Framework 4 or newer - free download from [Digia](http://www.qt.io)

### Building

The source includes a Qt Creator project with all files necessary to build either using Qt Creator or the command-line.

**Qt Creator**

* Load Qt Creator and open the Encounter project.
* Ensure all project settings are set accordingly and build.

The Encounter executable will be built in the directory specified _Build directory_ in the Qt Creator project settings.

**Command-Line**

Run the following commands in the directory of the Qt Creator project:

    qmake
    make

The first command generates the _Makefile_ for the project.  The Encounter executable will be built in the current directory.

Java _(JCounterpoise)_
----------------------

### Requirements

* Java Development Kit SE6 or newer - free download from [Oracle](http://www.java.com)
* GNU Getopt by Aaron M. Renn - included in source

### Building

The source includes a bash shell script for launching JCounterpoise and a basic configuration script for building and installing.  Either this script or standard Java tools can be used.

**Configuration Script**

The configuration script offers a similar experience to GNU Autotools.

* Open the _configure_ file in a text editor.
* Edit the necessary variables marked at the top of the file.

Run the following commands on the command-line:

    configure
    make

The first command generates the _Makefile_ for the compilation.  The Counterpoise binary and Bash script will be built in the location specified in the _configure_ file.

**Compilation using the Java Compiler**

Run the following command from the root directory of the Java code:

    javac uk/co/bwhazel/jcounterpoise/*.java uk/co/bwhazel/jcounterpoise/dataformatters/*.java -d /bin

The binary will be built in the _bin_ folder.

.NET/C# _(Sharpen)_
-------------------

### Requirements

* Microsoft .NET Framework 2.0 or newer - free download from [Microsoft](http://www.microsoft.com/net)

The source includes the Visual Studio 2010 project files to build Sharpen.  They can be built with in Visual Studio or from the command-line using MSBuild.

**Visual Studio**

* Open the Sharpen solution in Visual Studio.
* Although it is not necessary I would recommend changing the target framework from 2.0 to the latest version installed.

The executable will be built in the _Debug_ or _Release_ directory depending on the build configuration you select in Visual Studio.

**Command-Line**

Open a Visual Studio command-prompt and run the following command from the directory containing the Sharpen solution:

    msbuild Sharpen.sln /p:Configuration=Release

This will build using the _Release_ configuration.  The executable will be built in the _Release_ directory.

**Objective-C/Cocoa _(Xen)_**

### Requirements

* Mac OS X 10.7 "Lion" or newer - free download from the Apple Mac Store
* Xcode 4 or newer

The source includes an Xcode project with all files necessary to build Xen.

The executable will be built in the _Debug_ or _Release_ directory depending on the configuration chosen within Xcode.