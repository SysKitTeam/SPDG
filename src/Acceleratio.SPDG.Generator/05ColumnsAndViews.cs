using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {

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
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (SPWeb web = siteColl.OpenWeb(siteInfo.ID))
                        {
                            foreach (ListInfo listInfo in siteInfo.Lists)
                            {
                                progressDetail("Creating columns in List '" + listInfo.Name + "'");
                                SPList list = web.Lists[listInfo.Name];

                                for(int c = 0; c < workingDefinition.MaxNumberOfColumnsPerList; c++)
                                {
                                    if( c == 0 )
                                    {
                                        list.Fields.Add("First Name", SPFieldType.Text, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["First Name"].InternalName);
                                        view.Update();
                                    }
                                    else if( c == 1)
                                    {
                                        list.Fields.Add("Last Name", SPFieldType.Text, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["Last Name"].InternalName);
                                        view.Update();
                                    }
                                    else if (c == 2)
                                    {
                                        list.Fields.Add("Address", SPFieldType.Text, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["Address"].InternalName);
                                        view.Update();
                                    }
                                    else if (c == 3)
                                    {
                                        list.Fields.Add("Birthday", SPFieldType.DateTime, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["Birthday"].InternalName);
                                        view.Update();
                                    }
                                    else if (c == 4)
                                    {
                                        list.Fields.Add("Salary", SPFieldType.Integer, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["First Name"].InternalName);
                                        view.Update();
                                    }
                                    else if (c == 5)
                                    {
                                        list.Fields.Add("Company", SPFieldType.Text, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["Company"].InternalName);
                                        view.Update();
                                    }
                                    else if (c == 6)
                                    {
                                        list.Fields.Add("Email", SPFieldType.Text, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["Email"].InternalName);
                                        view.Update();
                                    }
                                    else if (c == 7)
                                    {
                                        list.Fields.Add("City", SPFieldType.Text, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["City"].InternalName);
                                        view.Update();
                                    }
                                    else if (c == 8)
                                    {
                                        list.Fields.Add("Phone", SPFieldType.Text, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["Phone"].InternalName);
                                        view.Update();
                                    }
                                    else if (c == 9)
                                    {
                                        list.Fields.Add("Web", SPFieldType.Text, false);
                                        SPView view = list.DefaultView;
                                        view.ViewFields.Add(list.Fields["Web"].InternalName);
                                        view.Update();
                                    }

                                }
                                list.DefaultView.Update();

                                Log.Write("Columns created in List '" + web.Url + "/" + listInfo.Name + "'");

                                progressDetail("Creating view in list '" + listInfo.Name + "'");

                                for (int c = 0; c < workingDefinition.MaxNumberOfViewsPerList; c++)
                                {

                                    List<string> addedFields = new List<string>();
                                    System.Collections.Specialized.StringCollection viewFields = new System.Collections.Specialized.StringCollection();

                                    for(int f = 0; f < c + 1; f++ )
                                    {
                                        if( f == 0)
                                        {
                                            viewFields.Add(list.Fields["First Name"].InternalName);
                                        }

                                        if (f == 1)
                                        {
                                            viewFields.Add(list.Fields["Last Name"].InternalName);
                                        }

                                        if (f == 2)
                                        {
                                            viewFields.Add(list.Fields["Address"].InternalName);
                                        }

                                        if (f == 3)
                                        {
                                            viewFields.Add(list.Fields["Birthday"].InternalName);
                                        }

                                        if (f == 4)
                                        {
                                            viewFields.Add(list.Fields["Salary"].InternalName);
                                        }

                                        if (f == 5)
                                        {
                                            viewFields.Add(list.Fields["Company"].InternalName);
                                        }

                                        if (f == 6)
                                        {
                                            viewFields.Add(list.Fields["Email"].InternalName);
                                        }

                                        if (f == 7)
                                        {
                                            viewFields.Add(list.Fields["City"].InternalName);
                                        }

                                        if (f == 8)
                                        {
                                            viewFields.Add(list.Fields["Phone"].InternalName);
                                        }

                                        if (f == 9)
                                        {
                                            viewFields.Add(list.Fields["Web"].InternalName);
                                        }
                                    }

                                    list.Views.Add("View " + (c+1).ToString(), viewFields, null, 100, true, false );
                                }
                                
                                list.Update();
                            }
                        }
                    }
                }
            }
        }



    }
}
