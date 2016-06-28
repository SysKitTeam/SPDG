using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Acceleratio.SPDG.Generator.Model;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    class SPDGClientListItem :SPDGListItem
    {
        private readonly ListItem _item;
        private readonly ClientContext _context;

        public static Expression<Func<ListItem, object>>[] IncludeExpression
        {
            get
            {
                List<Expression<Func<ListItem, object>>> includeExpression = new List<Expression<Func<ListItem, object>>>();
                includeExpression.Add(item => item.Id);
                includeExpression.Add(item => item.DisplayName);
                includeExpression.Add(item => item.HasUniqueRoleAssignments);                
                return includeExpression.ToArray();
            }
        }
        public SPDGClientListItem(ListItem item, ClientContext context)
        {
            _item = item;
            _context = context;
        }

        public override object this[string fieldName]
        {
            get { return _item[fieldName]; }
            set { _item[fieldName] = value; }
        }

        public override IEnumerable<string> GetAvailableFields()
        {
            return _item.FieldValues.Keys;
        }

        public override SPDGRoleAssignment GetRoleAssignmentByPrincipal(SPDGPrincipal principal)
        {
            return ClientRoleAssignmentHelper.GetRoleAssignmentByPrincipal(_item, _context, principal);
        }

        public override void AddRoleAssignment(SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roledefinitions)
        {
            ClientRoleAssignmentHelper.AddRoleAssignment(_item, _context, principal, roledefinitions);
        }

        public override void BreakRoleInheritance(bool copyRoleDefinitions)
        {
            _item.BreakRoleInheritance(copyRoleDefinitions, false);
            _context.ExecuteQuery();
        }

        public override bool HasUniqueRoleAssignments
        {
            get { return _item.HasUniqueRoleAssignments; }
        }

        public override int Id
        {
            get { return _item.Id; }
        }


        public override string DisplayName
        {
            get { return _item.DisplayName; }
        }

        public override void Update()
        {
            _context.ExecuteQuery();
        }
    }
}
