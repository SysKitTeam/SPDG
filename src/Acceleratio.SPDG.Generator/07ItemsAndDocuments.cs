using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;
using Acceleratio.SPDG.Generator.Utilities;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        private int docsAdded = 0;
        private string currentFileType = null;

        public void CreateItemsAndDocuments()
        {
            int totalProgress = 0;
            if( workingDefinition.MaxNumberofItemsToGenerate > 0 
                || workingDefinition.MaxNumberofDocumentLibraryItemsToGenerate > 0
                || workingDefinition.MaxNumberofItemsBigListToGenerate >0)
            {
                totalProgress = CalculateTotalItemsForProgressReporting();
            }
            else
            {
                return;
            }

            updateProgressOverall("Creating Items and Documents", totalProgress);

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (var siteColl = ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (var web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            var available = siteInfo.Lists;
                            //shuffle because of big lists
                            available.Shuffle();
                            foreach (ListInfo listInfo in available)
                            {
                                titleUsage.Clear();
                                if (!listInfo.isLib)
                                {
                                    var list = web.GetList(listInfo.Name);

                                    if( listInfo.TemplateType == SPDGListTemplateType.Tasks )
                                    {
                                        updateProgressDetail("Start adding items to tasks list: " + listInfo.Name + " in site: " + web.Url,0);
                                    }
                                    else if (listInfo.TemplateType == SPDGListTemplateType.Events)
                                    {
                                        updateProgressDetail("Start adding events to calendar: " + listInfo.Name + " in site: " + web.Url,0);
                                    }
                                    else
                                    {
                                        updateProgressDetail("Start adding items to list: " + listInfo.Name + " in site: " + web.Url,0);
                                    }
                                    
                                    List<ISPDGListItemInfo> batch=new List<ISPDGListItemInfo>();
                                    int itemCount = listInfo.isBigList ? workingDefinition.MaxNumberofItemsBigListToGenerate : workingDefinition.MaxNumberofItemsToGenerate;
                                    itemCount = SampleData.GetRandomNumber(itemCount*3/4, itemCount);
                                    for (int i = 0; i < itemCount; i++ )
                                    {
                                        var itemInfo=new SPDGListItemInfo();
                                        if (listInfo.TemplateType == SPDGListTemplateType.Tasks)
                                        {
                                            populateTask(itemInfo);
                                        }
                                        else if (listInfo.TemplateType == SPDGListTemplateType.Events)
                                        {
                                            populateEvent(itemInfo);
                                        }
                                        else
                                        {
                                            
                                            populateItemInfo(list, itemInfo, false);
                                        }
                                        batch.Add(itemInfo);

                                        if (batch.Count > 400)
                                        {
                                            list.AddItems(batch);
                                            updateProgressDetail(string.Format("Created {0}/{1} items for list {2}: ", i + 1, itemCount, list.RootFolder.Url), batch.Count);                                            
                                            batch.Clear();
                                        }
                                    }
                                    if (batch.Count > 0)
                                    {
                                        list.AddItems(batch);
                                        updateProgressDetail(string.Format("Created {0} items for list {1}: ", itemCount, list.RootFolder.Url), batch.Count);
                                        batch.Clear();
                                    }
                                    listInfo.ItemCount = itemCount;
                                }
                                else
                                {
                                    docsAdded = 0;
                                    var list = web.GetList(listInfo.Name);
                                    updateProgressDetail("Start adding documents to library: " + listInfo.Name + " in site: " + web.Url,0);
                                                                       
                                    while (docsAdded < workingDefinition.MaxNumberofDocumentLibraryItemsToGenerate)
                                    {
                                        addDocumentToFolder(list,list.RootFolder);
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

        private void addDocumentToFolder(SPDGList docLib, SPDGFolder folder)
        {                        
            fileTypeRotator();
            byte[] fileContent = getFileContent();
            var url = SampleData.GetSampleValueRandom(SampleData.FirstNames) + " " + SampleData.GetSampleValueRandom(SampleData.LastNames) + " " + SampleData.GetRandomNumber(1, 30000) + "." + currentFileType;
            var spFile = folder.AddFile(url, fileContent, true);
            var fileItem = spFile.Item;
            if (fileItem != null)
            { 
                populateItemInfo(docLib, fileItem, true);
                fileItem.Update();                
            }
            docsAdded++;
            

            foreach(var childFolder in folder.SubFolders)
            {
                if( docsAdded >= workingDefinition.MaxNumberofDocumentLibraryItemsToGenerate)
                {
                    break;
                }

                if( childFolder.Url.IndexOf("/Forms") == -1  )
                {
                    addDocumentToFolder(docLib, childFolder);
                }
            }
            updateProgressDetail("Adding document to folder: " + folder.Url);
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


        private static Dictionary<string,int> titleUsage=new Dictionary<string, int>();
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
            if (!titleUsage.ContainsKey(title))
            {
                titleUsage[title] = 0;
            }
            titleUsage[title]++;
            if (titleUsage[title] != 1)
            {
                title += " No. " + titleUsage[title];
            }
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
