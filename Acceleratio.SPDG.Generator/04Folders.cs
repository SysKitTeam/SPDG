using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        public void CreateFolders()
        {
            if( workingDefinition.MaxNumberOfFoldersToGenerate > 0 )
            {
                int totalProgress = workingDefinition.NumberOfSitesToCreate *
                        workingDefinition.MaxNumberOfFoldersToGenerate ;

                if (workingDefinition.CreateNewSiteCollections > 0)
                {
                    totalProgress = totalProgress * workingDefinition.CreateNewSiteCollections;
                }

                if (workingDefinition.CreateNewWebApplications > 0)
                {
                    totalProgress = totalProgress * workingDefinition.CreateNewWebApplications;
                }

                progressOverall("Creating Folders", totalProgress);
            }

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
                                if(listInfo.isLib)
                                {
                                    for (int counter = 1; counter <= workingDefinition.MaxNumberOfFoldersToGenerate; counter++)
                                    {
                                        try
                                        {
                                            Log.Write("Creating folders in '" + web.Url + "/" + listInfo.Name);

                                            SPList list = web.Lists[listInfo.Name];
                                            string folderName = findAvailableListName(list);
                                            SPFolder folder = list.RootFolder.SubFolders.Add(folderName);
                                            folder.Update();

                                            FolderInfo folderInfo = new FolderInfo();
                                            folderInfo.Name = folderName;
                                            folderInfo.URL = folder.Url;
                                            listInfo.Folders.Add(folderInfo);

                                            progressDetail("Folder created '" + folderInfo.Name + "'");

                                            for (int l = 1; l < workingDefinition.MaxNumberOfNestedFolderLevelPerLibrary; l++)
                                            {
                                                counter++;
                                                if (counter >= workingDefinition.MaxNumberOfFoldersToGenerate)
                                                {
                                                    break;
                                                }

                                                folderName = findAvailableListName(list);
                                                folder = folder.SubFolders.Add(folderName);
                                                //folder.Name = "Folder" + folderNumber;
                                                folder.Update();

                                                FolderInfo folderInfo2 = new FolderInfo();
                                                folderInfo2.Name = folderName;
                                                folderInfo2.URL = folder.Url;
                                                listInfo.Folders.Add(folderInfo2);

                                                progressDetail("Folder created '" + folderInfo2.Name + "'");
                                            }

                                       }
                                        catch(Exception ex)
                                        {
                                            Errors.Log(ex);
                                        }
                                    }

                                     

                                }
                            }
                        }
                    }
                }
            }
        }

        private string findAvailableListName(SPList list)
        {
            string candidate = SampleData.GetSampleValueRandom(SampleData.Countries);

            return candidate;
        }
    }
}
