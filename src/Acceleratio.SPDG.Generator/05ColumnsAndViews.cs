using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acceleratio.SPDG.Generator.Objects;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        private List<SPDGFieldInfo> _availableFieldInfos = new List<SPDGFieldInfo>()
        {
            new SPDGFieldInfo("First Name", SPFieldType.Text),
            new SPDGFieldInfo("Last Name", SPFieldType.Text),
            new SPDGFieldInfo("Address", SPFieldType.Text),
            new SPDGFieldInfo("Birthday", SPFieldType.DateTime),
            new SPDGFieldInfo("Salary", SPFieldType.Integer),
            new SPDGFieldInfo("Company", SPFieldType.Text),
            new SPDGFieldInfo("Email", SPFieldType.Text),
            new SPDGFieldInfo("City", SPFieldType.Text),
            new SPDGFieldInfo("Phone", SPFieldType.Text),
            new SPDGFieldInfo("Web", SPFieldType.Text)
        };

        public void CreateColumnsAndViews()
        {
            if( !workingDefinition.CreateColumns || workingDefinition.MaxNumberOfColumnsPerList == 0)
            {
                return;
            }

            int totalProgress = CalculateTotalColumnsAndViewsForProgressReporting();

            progressOverall("Creating Columns and Views", totalProgress);

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

                                progressDetail("Creating columns in List '" + list.RootFolder.Url + "'", 0);
                               
                                var newFields = _availableFieldInfos.Take(workingDefinition.MaxNumberOfColumnsPerList).ToList();
                                list.AddFields(newFields, true);

                                progressDetail("Columns created in List '" + list.RootFolder.Url + "'", newFields.Count);

                                progressDetail("Creating views in list  '" + list.RootFolder.Url + "'", 0);
                                
                               
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
                                progressDetail("Created views in list '" + list.RootFolder.Url + "'", workingDefinition.MaxNumberOfViewsPerList);
                            }
                        }
                    }
                }
            }
        }



    }
}
