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
            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (SPWeb web = siteColl.OpenWeb(siteInfo.URL))
                        {
                            foreach (ListInfo listInfo in siteInfo.Lists)
                            {
                                if(listInfo.Name.StartsWith("Library"))
                                {
                                    for (int counter = 1; counter <= workingDefinition.MaxNumberOfFoldersToGenerate; counter++)
                                    {
                                        string folderNumber = (counter).ToString("00");
                                        SPList list = web.Lists[listInfo.Name];
                                        SPFolder folder = list.RootFolder.SubFolders.Add("Folder" + folderNumber);
                                        folder.Update();

                                        FolderInfo folderInfo = new FolderInfo();
                                        folderInfo.Name = "Folder" + folderNumber;
                                        listInfo.Folders.Add(folderInfo);

                                        for (int l = 1; l < workingDefinition.MaxNumberOfNestedFolderLevelPerLibrary; l++)
                                        {
                                            counter++;
                                            if (counter >= workingDefinition.MaxNumberOfFoldersToGenerate)
                                            {
                                                break;
                                            }

                                            folderNumber = (counter).ToString("00");
                                            folder = folder.SubFolders.Add("Folder" + folderNumber);
                                            //folder.Name = "Folder" + folderNumber;
                                            folder.Update();

                                            FolderInfo folderInfo2 = new FolderInfo();
                                            folderInfo2.Name = "Folder" + folderNumber;
                                            listInfo.Folders.Add(folderInfo2);
                                        }
                                    }

                                     

                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
