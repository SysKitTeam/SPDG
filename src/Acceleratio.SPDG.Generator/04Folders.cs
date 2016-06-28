using System;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        public void CreateFolders()
        {
            if( workingDefinition.MaxNumberOfFoldersToGenerate > 0 )
            {
                int totalProgress = CalculateTotalFoldersForProgressReporting();

                updateProgressOverall("Creating Folders", totalProgress);
            }

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
                                if(listInfo.isLib)
                                {
                                    for (int counter = 1; counter <= workingDefinition.MaxNumberOfFoldersToGenerate; counter++)
                                    {
                                        try
                                        {
                                            Log.Write("Creating folders in '" + web.Url + "/" + listInfo.Name);

                                            var list = web.GetList(listInfo.Name);
                                            string folderName = findAvailableFolderName(list);
                                            var folder = list.RootFolder.AddFolder(folderName);
                                            folder.Update();

                                            FolderInfo folderInfo = new FolderInfo();
                                            folderInfo.Name = folderName;
                                            folderInfo.URL = folder.Url;
                                            listInfo.Folders.Add(folderInfo);

                                            updateProgressDetail("Folder created '" + folderInfo.Name + "'");

                                            for (int l = 0; l < workingDefinition.MaxNumberOfNestedFolderLevelPerLibrary; l++)
                                            {
                                                counter++;
                                                if (counter >= workingDefinition.MaxNumberOfFoldersToGenerate)
                                                {
                                                    break;
                                                }

                                                folderName = findAvailableFolderName(list);
                                                folder = folder.AddFolder(folderName);
                                                //folder.Name = "Folder" + folderNumber;
                                                folder.Update();

                                                FolderInfo folderInfo2 = new FolderInfo();
                                                folderInfo2.Name = folderName;
                                                folderInfo2.URL = folder.Url;
                                                listInfo.Folders.Add(folderInfo2);

                                                updateProgressDetail("Folder created '" + folderInfo2.Name + "'");
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

        private string findAvailableFolderName(SPDGList list)
        {
            string candidate = SampleData.GetSampleValueRandom(SampleData.Countries);

            return candidate;
        }
    }
}
