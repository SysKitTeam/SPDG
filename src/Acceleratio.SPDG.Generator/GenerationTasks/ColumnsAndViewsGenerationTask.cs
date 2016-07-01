using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Acceleratio.SPDG.Generator.SPModel;
using Acceleratio.SPDG.Generator.Structures;

namespace Acceleratio.SPDG.Generator.GenerationTasks
{
    public class ColumnsAndViewsGenerationTask : DataGenerationTaskBase
    {
        public ColumnsAndViewsGenerationTask(IDataGenerationTaskOwner owner) : base(owner)
        {
        }

        public override string Title
        {
            get { return "Creating Columns and Views"; }
        }

        public override int CalculateTotalSteps()
        {
            int totalProgress = WorkingDefinition.MaxNumberOfColumnsPerList *
                       WorkingDefinition.NumberOfSitesToCreate *
                       WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite +
                       (WorkingDefinition.MaxNumberOfViewsPerList *
                       WorkingDefinition.NumberOfSitesToCreate *
                       WorkingDefinition.MaxNumberOfListsAndLibrariesPerSite);

            
            totalProgress = totalProgress * Owner.WorkingSiteCollections.Count;            
            return totalProgress;
        }


        private static List<SPDGFieldInfo> _availableFieldInfos = new List<SPDGFieldInfo>()
        {
            new SPDGFieldInfo("First Name", SPDGFieldType.Text),
            new SPDGFieldInfo("Last Name", SPDGFieldType.Text),
            new SPDGFieldInfo("Address", SPDGFieldType.Text),
            new SPDGFieldInfo("Birthday", SPDGFieldType.DateTime),
            new SPDGFieldInfo("Salary", SPDGFieldType.Integer),
            new SPDGFieldInfo("Company", SPDGFieldType.Text),
            new SPDGFieldInfo("Email", SPDGFieldType.Text),
            new SPDGFieldInfo("City", SPDGFieldType.Text),
            new SPDGFieldInfo("Phone", SPDGFieldType.Text),
            new SPDGFieldInfo("Web", SPDGFieldType.Text)
        };

        public static ReadOnlyCollection<SPDGFieldInfo> AvailableFieldInfos = new ReadOnlyCollection<SPDGFieldInfo>(_availableFieldInfos);

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
                                var list = web.GetList(listInfo.Name);

                                Owner.IncrementCurrentTaskProgress("Creating columns in List '" + list.RootFolder.Url + "'", 0);
                               
                                var newFields = _availableFieldInfos.Take(WorkingDefinition.MaxNumberOfColumnsPerList).ToList();
                                list.AddFields(newFields, true);

                                Owner.IncrementCurrentTaskProgress("Columns created in List '" + list.RootFolder.Url + "'", newFields.Count);

                                Owner.IncrementCurrentTaskProgress("Creating views in list  '" + list.RootFolder.Url + "'", 0);
                                
                               
                                var listFields = list.Fields.ToList();
                                for (int c = 0; c < WorkingDefinition.MaxNumberOfViewsPerList; c++)
                                {                                
                                    var viewFields = new List<string>();
                                    newFields.Shuffle();
                                    for (int i = 0; i <= c + 1 && i<=5;i++)
                                    {
                                        if (i < newFields.Count)
                                        {
                                            viewFields.Add(listFields.First(x=>x.Title == newFields[i].DisplayName).InternalName);
                                        }
                                        else
                                        {
                                            viewFields.Add(listFields[SampleData.GetRandomNumber(0,listFields.Count-1)].InternalName);
                                        }
                                    }
                                    list.AddView("View " + (c+1).ToString(), viewFields, null, 100, true, false );
                                }
                                Owner.IncrementCurrentTaskProgress("Created views in list '" + list.RootFolder.Url + "'", WorkingDefinition.MaxNumberOfViewsPerList);
                            }
                        }
                    }
                }
            }
        }
    }
}
