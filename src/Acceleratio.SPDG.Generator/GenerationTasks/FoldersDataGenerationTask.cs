using System;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator.GenerationTasks
{
    public class FoldersDataGenerationTask : DataGenerationTaskBase
    {

        public FoldersDataGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {

        }

        public override string Title
        {
            get { return "Creating Folders"; }
        }

        public override bool IsActive
        {
            get { return CalculateTotalSteps() > 0; }
        }


        public override int CalculateTotalSteps()
        {
            int totalSteps = WorkingDefinition.NumberOfSitesToCreate *
                      WorkingDefinition.MaxNumberOfFoldersToGenerate;            
            totalSteps = totalSteps * Owner.WorkingSiteCollections.Count;            
            return totalSteps;
        }

        public override void Execute()
        {
            foreach (SiteCollInfo siteCollInfo in Owner.WorkingSiteCollections)
            {
                using (var siteColl = Owner.ObjectsFactory.GetSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (var web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            foreach (ListInfo listInfo in siteInfo.Lists)
                            {
                                if (listInfo.isLib)
                                {
                                    for (int counter = 1; counter <= WorkingDefinition.MaxNumberOfFoldersToGenerate; counter++)
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

                                            Owner.IncrementCurrentTaskProgress("Folder created '" + folderInfo.Name + "'");

                                            for (int l = 0; l < WorkingDefinition.MaxNumberOfNestedFolderLevelPerLibrary; l++)
                                            {
                                                counter++;
                                                if (counter >= WorkingDefinition.MaxNumberOfFoldersToGenerate)
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

                                                Owner.IncrementCurrentTaskProgress("Folder created '" + folderInfo2.Name + "'");
                                            }
                                        }
                                        catch (Exception ex)
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
