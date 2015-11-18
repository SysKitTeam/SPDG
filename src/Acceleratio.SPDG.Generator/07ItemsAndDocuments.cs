using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.IO;
using Acceleratio.SPDG.Generator.Objects;

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
                totalProgress = CalculateTotalItemsForProgressReporting();
            }
            else
            {
                return;
            }

            progressOverall("Creating Items and Documents", totalProgress);

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (var siteColl = ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (var web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            foreach (ListInfo listInfo in siteInfo.Lists)
                            {
                                if (!listInfo.isLib)
                                {
                                    var list = web.GetList(listInfo.Name);

                                    if( listInfo.TemplateType == SPListTemplateType.Tasks )
                                    {
                                        Log.Write("Start adding items to tasks list: " + listInfo.Name + " in site: " + web.Url);
                                    }
                                    else if (listInfo.TemplateType == SPListTemplateType.Events)
                                    {
                                        Log.Write("Start adding events to calendar: " + listInfo.Name + " in site: " + web.Url);
                                    }
                                    else
                                    {
                                        Log.Write("Start adding items to list: " + listInfo.Name + " in site: " + web.Url);
                                    }
                                    
                                    List<SPDGListItemInfo> listItemsToCreate=new List<SPDGListItemInfo>();
                                    for (int i = 0; i < workingDefinition.MaxNumberofItemsToGenerate; i++ )
                                    {
                                        var itemInfo=new SPDGListItemInfo();
                                        if (listInfo.TemplateType == SPListTemplateType.Tasks)
                                        {
                                            populateTask(itemInfo);
                                        }
                                        else if (listInfo.TemplateType == SPListTemplateType.Events)
                                        {
                                            populateEvent(itemInfo);
                                        }
                                        else
                                        {
                                            populateItemInfo(list, itemInfo, false);
                                        }
                                        listItemsToCreate.Add(itemInfo);
                                    }
                                    //TODO:rf better messages
                                    Log.Write(string.Format("Created {0} items for list {1}: ", workingDefinition.MaxNumberofItemsToGenerate, list.RootFolder.Url));
                                    list.AddItems(listItemsToCreate);
                                    Log.Write(string.Format("Added {0} items to list {1}: ", workingDefinition.MaxNumberofItemsToGenerate, list.RootFolder.Url));
                                }
                                else
                                {
                                    docsAdded = 0;
                                    var list = web.GetList(listInfo.Name);
                                    Log.Write("Start adding documents to library: " + listInfo.Name + " in site: " + web.Url);

                                    //TODO:rf bring back documents support
                                    //while (docsAdded < workingDefinition.MaxNumberofItemsToGenerate)
                                    //{
                                    //    addDocumentToFolder(list.RootFolder);
                                    //}

                                }
                            }
                        }
                    }
                }
            }
        }

        private void addDocumentToFolder(SPFolder folder)
        {
            //TODO:rf bring back document support
            //if( docsAdded >= workingDefinition.MaxNumberofItemsToGenerate)
            //{
            //    return;
            //}

            //fileTypeRotator();
            //byte[] fileContent = getFileContent();
            //SPFile spFile = folder.Files.Add(SampleData.GetSampleValueRandom(SampleData.FirstNames) + " " + SampleData.GetSampleValueRandom(SampleData.LastNames)  + " " + SampleData.GetRandomNumber(1, 30000) + "." + currentFileType , fileContent, true);
            //if (spFile.Item != null)
            //{ 
            //    populateItemInfo( (SPList) folder.DocumentLibrary, spFile.Item, true);
            //}
            //docsAdded++;
            

            //foreach(SPFolder childFolder in folder.SubFolders)
            //{
            //    if( docsAdded >= workingDefinition.MaxNumberofItemsToGenerate)
            //    {
            //        break;
            //    }

            //    if( childFolder.Url.IndexOf("/Forms") == -1  )
            //    {
            //        addDocumentToFolder(childFolder);
            //    }
            //}
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
                content = SampleData.CreatePDF(workingDefinition.MinDocumentSizeKB, workingDefinition.MaxDocumentSizeMB * 1024);
            }
            else if (currentFileType == "png")
            {
                content = SampleData.AddRandomPngFile();
            }

            return content;
        }

        private void populateItemInfo(SPDGList list, ISPDGListItemInfo item, bool isDocLib )
        {
            List<string> userFields = new List<string>();
            foreach(var field in list.Fields)
            {
                if( _availableFieldInfos.Any(x=>x.DisplayName==field.Title))
                {
                    userFields.Add(field.InternalName);
                }
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
        }

        private void populateTask(ISPDGListItemInfo item)
        {
            string title = SampleData.GetSampleValueRandom(SampleData.Accounts) + " Task";
            item["Title"] = title;
            item["Status"] = "In Progress";
            item["DueDate"] = SampleData.GetRandomDate(2013, 2015);
            item["Priority"] = "(2) Normal";
            item["PercentComplete"] = SampleData.GetRandomNumber(1, 100) / 100;                        
        }

        private void populateEvent(ISPDGListItemInfo item)
        {            
            string title = SampleData.GetSampleValueRandom(SampleData.Accounts) + " Event";
            item["Title"] = title;
            DateTime time = SampleData.GetRandomDateCurrentMonth();
            item["EventDate"] = time;
            item["EndDate"] = time;
            item["Location"] = SampleData.GetSampleValueRandom(SampleData.Cities);                 
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
