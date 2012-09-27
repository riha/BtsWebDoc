# BizTalk Web Documentor #

BizTalk Web Documentor automates the creation of technical documentation for BizTalk based solutions by extracting configuration data and visualize this data on a web site.

The solution consists of two different tools; a command line based tool that reads BizTalk installations, configurations and persist them to disk. A web application that reads the different versions of the documentation from disk and shows the information on an easy-to-read web page.

The command line tool will read your current installation and configuration, give it a unique version number and persist a set of compressed files to disk. The execution of the command line tool can be done manually or scheduled. This allows showing all available versions of the documentation in the web applicationand makes it possible to easily switch between versions.

The tool will in the current version (1.1) document the following BizTalk artifacts.

* Receive ports
* Send ports
* Orchestrations
* Maps
* Schemas
* Pipelines
* Assemblies

## Requirements ##

* .NET 4
* IIS 7.5
* BizTalk Server 2006 +

## Setup ##

1. Download the latest package at any location on a machine that has BizTalk Server installed.
2. Run the command line client found in the *Client* folder using the following command ``client.exe -export``. By default the client will try and find a local BizTalk Configuration database. If you have a setup with separate database server or other specific setting run ''client.exe –help'' to see info on the different options. The command line client will save the set of configuration files to the folder above the folder it executes in, this can be changed using the ``-folder`` parameter when running the client to set the path to any export folder.
3. Point a IIS stand alone web site, or a application in a exiting web site, to the folder named Web in the package. By default the web application will look for exported documentation files in a folder above the folder it executes in. This can however be changed in the web.config suing the DocumentsFolder appSettings key.