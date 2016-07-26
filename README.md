[<img src="https://cloud.githubusercontent.com/assets/19644161/16901791/945c8cd8-4c4d-11e6-8858-4dc084223af3.png">](https://github.com/Acceleratio/SPDG/raw/master/releases/binaries/SPDG.zip)
# SPDG
SharePoint Data Generator
SPDG is an open-source tool developed by [Acceleratio Ltd.](https://acceleratio.net/) to generate mock data for SharePoint. The tool can be used to populate SharePoint On-Premises or SharePoint Online. 

You can create data to test SharePoint, to simulate and recreate specific scenarios in which you can engage multiple SharePoint features, or to give SharePoint or SharePoint-related demos. For example, we use it to showcase and conduct demos for [SPDocKit](https://www.spdockit.com/), our SharePoint admin tool.

## SPDG can generate the following SharePoint demo and sample data: 
* Active Directory or Azure Directory 
* Web applications
* Site collections
* Subsites, lists, folders, items and documents 
* Content Types, Site columns, and views
* Customized SharePoint groups
* Unique permissions 

SPDG uses random names from the .csv files stored in the directory. Based on that data, it generates user names, web applications, sites, site collections, and other SharePoint content. [Learn more...](https://github.com/Acceleratio/SPDG/wiki/Help)

Please note that SPDG does not support SharePoint 2007. 

##Requirements
To build this solution you must have .net 4.5, however once built Acceleratio SPDG can also be used on machines with .net 3.5 (potential SP2010 environments). Visual Studio 2015 is recommended.

SPDG can be run on either a workstation or a SharePoint server. However, please note that for On Premises data generation it MUST be run from a SharePoint server, since the Server Object Model is used is the generation procedure. 

##Usage
To use SPDG you can:
* Build the binaries yourself
* Use the [ClickOnce](https://github.com/Acceleratio/SPDG/raw/master/releases/ClickOnce/setup.exe) installer
* [Download](https://github.com/Acceleratio/SPDG/raw/master/releases/binaries/SPDG.zip) the latest binaries

