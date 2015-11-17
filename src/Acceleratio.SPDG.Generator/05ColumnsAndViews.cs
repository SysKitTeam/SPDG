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
                                progressDetail("Creating columns in List '" + listInfo.Name + "'");
                                var list = web.GetList(listInfo.Name);

                                var fieldsToCreate = _availableFieldInfos.Take(workingDefinition.MaxNumberOfColumnsPerList);
                                list.AddFields(fieldsToCreate, true);
                             
                                Log.Write("Columns created in List '" + list.RootFolder.Url + "'");

                                progressDetail("Creating view in list '" + listInfo.Name + "'");

                                //TODO:rf return view generation
                                //for (int c = 0; c < workingDefinition.MaxNumberOfViewsPerList; c++)
                                //{

                                //    List<string> addedFields = new List<string>();
                                //    System.Collections.Specialized.StringCollection viewFields = new System.Collections.Specialized.StringCollection();

                                //    for(int f = 0; f < c + 1; f++ )
                                //    {
                                //        if( f == 0)
                                //        {
                                //            viewFields.Add(list.Fields["First Name"].InternalName);
                                //        }

                                //        if (f == 1)
                                //        {
                                //            viewFields.Add(list.Fields["Last Name"].InternalName);
                                //        }

                                //        if (f == 2)
                                //        {
                                //            viewFields.Add(list.Fields["Address"].InternalName);
                                //        }

                                //        if (f == 3)
                                //        {
                                //            viewFields.Add(list.Fields["Birthday"].InternalName);
                                //        }

                                //        if (f == 4)
                                //        {
                                //            viewFields.Add(list.Fields["Salary"].InternalName);
                                //        }

                                //        if (f == 5)
                                //        {
                                //            viewFields.Add(list.Fields["Company"].InternalName);
                                //        }

                                //        if (f == 6)
                                //        {
                                //            viewFields.Add(list.Fields["Email"].InternalName);
                                //        }

                                //        if (f == 7)
                                //        {
                                //            viewFields.Add(list.Fields["City"].InternalName);
                                //        }

                                //        if (f == 8)
                                //        {
                                //            viewFields.Add(list.Fields["Phone"].InternalName);
                                //        }

                                //        if (f == 9)
                                //        {
                                //            viewFields.Add(list.Fields["Web"].InternalName);
                                //        }
                                //    }

                                //    list.Views.Add("View " + (c+1).ToString(), viewFields, null, 100, true, false );
                                //}
                                
                                //list.Update();
                            }
                        }
                    }
                }
            }
        }



    }
}
