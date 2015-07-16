using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        SPListTemplateType lastTemplateType = SPListTemplateType.NoListTemplate;
        string lastListPrefix = "List";

        public void CreateLists()
        {
            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using( SPWeb web = siteColl.OpenWeb(siteInfo.URL)) 
                        {
                            for( int s = 0; s < workingDefinition.MaxNumberOfListsAndLibrariesPerSite; s++ )
                            {
                                string listNum = (s + 1).ToString("00");
                                getNextTemplateType();
                                web.Lists.Add(lastListPrefix + listNum, string.Empty, lastTemplateType);

                                ListInfo listInfo = new ListInfo();
                                listInfo.Name = lastListPrefix + listNum;

                                siteInfo.Lists.Add(listInfo);
                            }
                        }
                    }
                }
            }


        }

        private SPListTemplateType getNextTemplateType()
        {
            bool changed = false;
            if (workingDefinition.LibTypeList && lastTemplateType != SPListTemplateType.GenericList)
            {
                lastTemplateType = SPListTemplateType.GenericList;
                lastListPrefix = "List";
                changed = true;
            }

            if( !changed && workingDefinition.LibTypeDocument && lastTemplateType != SPListTemplateType.DocumentLibrary)
            {
                lastTemplateType = SPListTemplateType.DocumentLibrary;
                lastListPrefix = "Library";
                changed = true;
            }

            if (!changed && workingDefinition.LibTypeTasks && lastTemplateType != SPListTemplateType.Tasks)
            {
                lastTemplateType = SPListTemplateType.DocumentLibrary;
                lastListPrefix = "Tasks";
                changed = true;
            }

            return lastTemplateType;
        }
    }
}
