using System.Xml.Serialization;
using Microsoft.Azure.ActiveDirectory.GraphClient;

namespace Acceleratio.SPDG.Generator
{
    public class ClientGeneratorDefinition : GeneratorDefinitionBase
    {
        public string TenantName { get; set; }
        
        public override bool IsClientObjectModel { get { return true; } }

        [XmlIgnore]
        public string AzureAdAccessToken { get; private set; }
        public override void ValidateCredentials()
        {
            AzureAdAccessToken = TokenHelper.GetUserAccessToken(Username, Password);
            
            //var onlineCredentials = new SharePointOnlineCredentials(Username, Utilities.Common.StringToSecureString(Password));
            //using (ClientContext context = new ClientContext(string.Format("https://{0}-admin.sharepoint.com", TenantName)))
            //{
            //    context.Credentials = onlineCredentials;
            //    var tenant = new Tenant(context);
            //    context.Load(tenant);
            //    context.ExecuteQuery();
            //}
        }
    }
}