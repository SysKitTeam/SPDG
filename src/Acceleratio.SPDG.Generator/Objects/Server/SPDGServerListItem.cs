using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    public class SPDGServerListItem: SPDGListItem
    {
        private readonly SPListItem _item;

        public SPDGServerListItem(SPListItem item)
        {
            _item = item;
        }

        public override object this[string fieldName]
        {
            get { return _item[fieldName]; }
            set { _item[fieldName] = value; }
        }

        public override IEnumerable<string> GetAvailableFields()
        {
            return _item.Fields.Cast<SPField>().Select(x => x.InternalName);
        }

        public override SPDGRoleAssignment GetRoleAssignmentByPrincipal(SPDGPrincipal principal)
        {
            return ServerRoleAssignmentHelper.GetRoleAssignmentByPrincipal(_item, principal);
        }

        public override void AddRoleAssignment(SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roledefinitions)
        {
            ServerRoleAssignmentHelper.AddRoleAssignment(_item, principal, roledefinitions);
        }

        public override void BreakRoleInheritance(bool copyRoleAssignments)
        {
            _item.BreakRoleInheritance(copyRoleAssignments);
        }

        public override bool HasUniqueRoleAssignments
        {
            get { return _item.HasUniqueRoleAssignments; }
        }

        public override int Id
        {
            get { return _item.ID; }
        }

        public override string DisplayName
        {
            get { return _item.DisplayName; }
        }

        public override void Update()
        {
            _item.Update();
        }
    }
}
