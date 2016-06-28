using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.Model;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        private List<SPDGFieldInfo> _availableFieldInfos = new List<SPDGFieldInfo>()
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

        public void CreateColumnsAndViews()
        {
            if( !workingDefinition.CreateColumns || workingDefinition.MaxNumberOfColumnsPerList == 0)
            {
                return;
            }

            int totalProgress = CalculateTotalColumnsAndViewsForProgressReporting();

            updateProgressOverall("Creating Columns and Views", totalProgress);

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
                                var list = web.GetList(listInfo.Name);

                                updateProgressDetail("Creating columns in List '" + list.RootFolder.Url + "'", 0);
                               
                                var newFields = _availableFieldInfos.Take(workingDefinition.MaxNumberOfColumnsPerList).ToList();
                                list.AddFields(newFields, true);

                                updateProgressDetail("Columns created in List '" + list.RootFolder.Url + "'", newFields.Count);

                                updateProgressDetail("Creating views in list  '" + list.RootFolder.Url + "'", 0);
                                
                               
                                var listFields = list.Fields.ToList();
                                for (int c = 0; c < workingDefinition.MaxNumberOfViewsPerList; c++)
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
                                updateProgressDetail("Created views in list '" + list.RootFolder.Url + "'", workingDefinition.MaxNumberOfViewsPerList);
                            }
                        }
                    }
                }
            }
        }



    }
}
