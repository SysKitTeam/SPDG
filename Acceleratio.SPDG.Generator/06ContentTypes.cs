using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator
{
    public partial class DataGenerator
    {
        public static List<string> allSiteColumns;

        public void CreateContentTypes()
        {
            if (!workingDefinition.CreateContentTypes || workingDefinition.MaxNumberOfContentTypesPerSiteCollection == 0)
            {
                return;
            }

            foreach (SiteCollInfo siteCollInfo in workingSiteCollections)
            {
                using (SPSite siteColl = new SPSite(siteCollInfo.URL))
                {
                    foreach (SiteInfo siteInfo in siteCollInfo.Sites)
                    {
                        using (SPWeb web = siteColl.OpenWeb(siteInfo.URL))
                        {
                            for (int c = 0; c < workingDefinition.MaxNumberOfContentTypesPerSiteCollection; c++)
                            {
                                string contentTypeName = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);
                                SPContentType contentType = new SPContentType(web.ContentTypes["Document"], web.ContentTypes, contentTypeName + " Document");
                                web.ContentTypes.Add(contentType);
                                contentType.Group = "Custom SPDG Content Types";
                                contentType.Description = contentTypeName + " content type";
                                List<string> randomSiteColumns = GetRandomSiteColumns();
                                foreach (string siteColumn in randomSiteColumns)
                                {
                                    contentType.FieldLinks.Add(new SPFieldLink(siteColl.RootWeb.Fields.GetField(siteColumn)));
                                }

                                contentType.Update();
                                Log.Write("Content Type created: '" + contentTypeName + " Document', in site: " + web.Url);


                                if (workingDefinition.ContentTypesCanInheritFromOtherContentType)
                                {
                                    c++;
                                    if (c < workingDefinition.MaxNumberOfContentTypesPerSiteCollection)
                                    {
                                        contentTypeName = SampleData.GetSampleValueRandom(SampleData.BusinessDocsTypes);
                                        SPContentType childContentType = new SPContentType(contentType, web.ContentTypes, contentTypeName + " Document");
                                        web.ContentTypes.Add(childContentType);
                                        childContentType.Group = "Custom SPDG Content Types";
                                        childContentType.Description = contentTypeName + " content type";
                                        randomSiteColumns = GetRandomSiteColumns();
                                        foreach (string siteColumn in randomSiteColumns)
                                        {
                                            contentType.FieldLinks.Add(new SPFieldLink(siteColl.RootWeb.Fields.GetField(siteColumn)));
                                        }
                                        childContentType.Update();
                                        Log.Write("Content Type created: '" + contentTypeName + " Document', in site: " + web.Url);
                                    }
                                }
                            }


                        }
                    }
                }
            }
        }

        private static List<string> GetRandomSiteColumns()
        {
            if (allSiteColumns == null )
            {
                allSiteColumns = new List<string>();
                allSiteColumns.Add("Address");
                allSiteColumns.Add("Birthday");
                allSiteColumns.Add("Business Phone");
                allSiteColumns.Add("Car Phone");
                allSiteColumns.Add("City");
                allSiteColumns.Add("Company");
                allSiteColumns.Add("Department");
                allSiteColumns.Add("E-Mail");
                allSiteColumns.Add("First Name");
                allSiteColumns.Add("Home Phone");
                allSiteColumns.Add("Other Address City");
                allSiteColumns.Add("Related Company");
                allSiteColumns.Add("Radio Phone");
                allSiteColumns.Add("E-mail 2");
                allSiteColumns.Add("E-mail 3");
            }

            List<string> randomSites = new List<string>();
            Random random = new Random();
            for(int i=0; i<7; i++)
            {
                int randomNumber = random.Next(0, allSiteColumns.Count - 1);
                if( !randomSites.Any( x => x == allSiteColumns[randomNumber] ) )
                {
                    randomSites.Add(allSiteColumns[randomNumber]);
                }
            }

            return randomSites;
        }



    }
}
