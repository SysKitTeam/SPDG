using System;
using System.ComponentModel;
using Acceleratio.SPDG.Generator.Objects;
using Acceleratio.SPDG.Generator.Utilities;

namespace Acceleratio.SPDG.Generator
{
    public partial class ServerDataGenerator : DataGenerator
    {
        public ServerDataGenerator(ServerGeneratorDefinition definition) : base(definition)
        {
        }

        protected new ServerGeneratorDefinition WorkingDefinition
        {
            get { return (ServerGeneratorDefinition) base.WorkingDefinition; }
        }

        protected override SPDGObjectsFactory CreateObjectsFactory()
        {
            return new SPDGServerObjectsFactory();
        }


        protected override int CalculateTotalItemsForProgressReporting()
        {
            var total = base.CalculateTotalItemsForProgressReporting();

            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateTotalListsForProgressReporting()
        {
            var total = base.CalculateTotalListsForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateTotalFoldersForProgressReporting()
        {
            var total = base.CalculateTotalFoldersForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }

            return total;
        }

        protected override int CalculateTotalColumnsAndViewsForProgressReporting()
        {
            var total = base.CalculateTotalColumnsAndViewsForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateTotalContentTypesForProgressReporting()
        {
            var total = base.CalculateTotalContentTypesForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateOverallPermissionsForProgressReporting(int totalInSiteCollection)
        {
            var total= base.CalculateOverallPermissionsForProgressReporting(totalInSiteCollection);
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        protected override int CalculateTotalSitesForProgressReporting()
        {
            var total= base.CalculateTotalSitesForProgressReporting();
            if (WorkingDefinition.CreateNewWebApplications > 0)
            {
                total = total * WorkingDefinition.CreateNewWebApplications;
            }
            return total;
        }

        public override bool startDataGeneration(BackgroundWorker backgroundWorker)
        {
            if (!WorkingDefinition.CredentialsOfCurrentUser)
            {
                var parts = WorkingDefinition.Username.Split('\\');

                if (Common.ImpersonateValidUser(parts[1], parts[0], WorkingDefinition.Password))
                {
                    try
                    {
                        return base.startDataGeneration(backgroundWorker);
                    }
                    finally
                    {
                        Common.UndoImpersonation();
                    }   
                }
            }
            else
            {
                return base.startDataGeneration(backgroundWorker);
            }
            return false;
        }
    }
}