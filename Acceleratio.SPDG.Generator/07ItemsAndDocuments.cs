using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.IO;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        private int docsAdded = 0;
        private string currentFileType = null;

        public void CreateItemsAndDocuments()
        {
            int totalProgress = 0;
            if( workingDefinition.MaxNumberofItemsToGenerate > 0 )
            { 
                totalProgress = workingDefinition.NumberOfSitesToCreate *
                                workingDefinition.MaxNumberOfListsAndLibrariesPerSite *
                            workingDefinition.MaxNumberofItemsToGenerate;

                if (workingDefinition.CreateNewSiteCollections > 0)
                {
                    totalProgress = totalProgress * workingDefinition.CreateNewSiteCollections;
                }

                if (workingDefinition.CreateNewWebApplications > 0)
                {
                    totalProgress = totalProgress * workingDefinition.CreateNewWebApplications;
                }
            }
            else
            {
                return;
            }

            progressOverall("Creating Items and Documents", totalProgress);

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            foreach (ListInfo listInfo in siteInfo.Lists)
                            {
                                if (!listInfo.isLib)
                                {
                                    SPList list = web.Lists[listInfo.Name];
                                    Log.Write("Start adding items to list: " + listInfo.Name + " in site: " + web.Url);

                                    for (int i = 0; i < workingDefinition.MaxNumberofItemsToGenerate; i++ )
                                    {
                                        addItemToList(list, null, false);
                                    }

                                    
                                }
                                else
                                {
                                    docsAdded = 0;
                                    SPList list = web.Lists[listInfo.Name];
                                    Log.Write("Start adding documents to library: " + listInfo.Name + " in site: " + web.Url);

                                    while (docsAdded < workingDefinition.MaxNumberofItemsToGenerate)
                                    {
                                        addDocumentToFolder(list.RootFolder);
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void addDocumentToFolder(SPFolder folder)
        {
            if( docsAdded >= workingDefinition.MaxNumberofItemsToGenerate)
            {
                return;
            }

            fileTypeRotator();
            byte[] fileContent = getFileContent();
            SPFile spFile = folder.Files.Add(SampleData.GetSampleValueRandom(SampleData.FirstNames) + " " + SampleData.GetSampleValueRandom(SampleData.LastNames)  + " " + SampleData.GetRandomNumber(1, 30000) + "." + currentFileType , fileContent, true);
            if (spFile.Item != null)
            { 
                addItemToList( (SPList) folder.DocumentLibrary, spFile.Item, true);
            }
            docsAdded++;
            

            foreach(SPFolder childFolder in folder.SubFolders)
            {
                if( docsAdded >= workingDefinition.MaxNumberofItemsToGenerate)
                {
                    break;
                }

                if( childFolder.Url.IndexOf("/Forms") == -1  )
                {
                    addDocumentToFolder(childFolder);
                }
            }
        }

        private byte[] getFileContent()
        {
            byte[] content = null;
            if( currentFileType == "test")
            {
                FileStream fstream = File.OpenRead("C:\\Temp\\test.docx");
                content = new byte[fstream.Length];
                fstream.Read(content, 0, (int)fstream.Length);
                fstream.Close();
            }
            else if (currentFileType == "docx")
            {
                content = SampleData.CreateDocx();
            }
            else if (currentFileType == "xlsx")
            {
                content = SampleData.CreateExcel();
            }
            else if (currentFileType == "pdf")
            {
                content = SampleData.CreatePDF();
            }
            else if (currentFileType == "png")
            {
                content = SampleData.AddRandomPngFile();
            }

            return content;
        }

        private void addItemToList( SPList list, SPListItem item, bool isDocLib )
        {
            List<string> userFields = new List<string>();
            foreach(SPField field in list.Fields)
            {
                if( !field.SourceId.StartsWith("http://"))
                {
                    userFields.Add(field.Title);
                }
            }

            if( item == null )
            {
                item = list.AddItem();
            }

            string title = getFieldValue("First Name") + " "  + getFieldValue("Last Name");
            item["Title"] = title;

            foreach( string fieldName in userFields )
            {
                object value = getFieldValue(fieldName);
                if( value != null )
                {
                    item[fieldName] = value;
                }
            }

            item.Update();
            if (isDocLib)
            {
                progressDetail("Document added: " + title);
            }
            else
            {
                progressDetail("Item added: " + title);
            }
            

        }

        private object getFieldValue( string fieldName)
        {
            if( fieldName == "First Name")
            {
                return SampleData.GetSampleValueRandom(SampleData.FirstNames);
            }
            else if (fieldName == "Last Name")
            {
                return SampleData.GetSampleValueRandom(SampleData.LastNames);
            }
            else if (fieldName.ToLower().Contains("phone"))
            {
                return SampleData.GetSampleValueRandom(SampleData.PhoneNumbers);
            }
            else if (fieldName.ToLower().StartsWith("web") )
            {
                return SampleData.GetSampleValueRandom(SampleData.WebSites);
            }
            else if (fieldName.ToLower().StartsWith("year"))
            {
                return SampleData.GetSampleValueRandom(SampleData.Years);
            }
            else if (fieldName.ToLower().StartsWith("email"))
            {
                return SampleData.GetSampleValueRandom(SampleData.EmailAddreses);
            }
            else if (fieldName == "Country")
            {
                return SampleData.GetSampleValueRandom(SampleData.Countries);
            }
            else if (fieldName == "Company")
            {
                return SampleData.GetSampleValueRandom(SampleData.Companies);
            }
            else if (fieldName == "City")
            {
                return SampleData.GetSampleValueRandom(SampleData.Cities);
            }
            else if (fieldName.ToLower().Contains("address"))
            {
                return SampleData.GetSampleValueRandom(SampleData.Addresses);
            }
            else if (fieldName == "Account")
            {
                return SampleData.GetSampleValueRandom(SampleData.Accounts);
            }
            else if (fieldName == "Salary")
            {
                return SampleData.GetRandomNumber(20000, 200000);
            }
            else if (fieldName == "Birthday")
            {
                return SampleData.GetRandomDate(1940, 2000);
            }

            return null;
        }

        private void fileTypeRotator()
        {
            bool changed = false;

            if (currentFileType == null)
            {
                if (workingDefinition.IncludeDocTypeDOCX) { currentFileType = "docx"; return; }
                if (workingDefinition.IncludeDocTypeXLSX) {currentFileType = "xlsx"; return;}
                if (workingDefinition.IncludeDocTypePDF){ currentFileType = "pdf"; return;}
                if (workingDefinition.IncludeDocTypeImages) { currentFileType = "png"; return; }
                return;
            }

            if( currentFileType == "docx" )
            {
                if (workingDefinition.IncludeDocTypeXLSX) {currentFileType = "xlsx"; return;}
                if (workingDefinition.IncludeDocTypePDF) {currentFileType = "pdf"; return;}
                if (workingDefinition.IncludeDocTypeImages) { currentFileType = "png"; return; }
                return;
            }

            if (currentFileType == "xlsx")
            {
                if (workingDefinition.IncludeDocTypePDF) {currentFileType = "pdf"; return;}
                if (workingDefinition.IncludeDocTypeImages) { currentFileType = "png"; return; }
                if (workingDefinition.IncludeDocTypeDOCX) {currentFileType = "docx"; return;}
                return;
            }

            if (currentFileType == "pdf")
            {
                if (workingDefinition.IncludeDocTypeImages) { currentFileType = "png"; return; }
                if (workingDefinition.IncludeDocTypeDOCX) {currentFileType = "docx"; return;}
                if (workingDefinition.IncludeDocTypeXLSX){ currentFileType = "xlsx"; return;}
                return;
            }

            if( currentFileType == "png" )
            {
                if (workingDefinition.IncludeDocTypeDOCX) {currentFileType = "docx"; return;}
                if (workingDefinition.IncludeDocTypeXLSX){ currentFileType = "xlsx"; return;}
                if (workingDefinition.IncludeDocTypePDF) { currentFileType = "pdf"; return; }
                
            }

        }
    }
}
