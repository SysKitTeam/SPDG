using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.Sharing;

namespace Acceleratio.SPDG.Generator.Client.SPModel
{
    class SPDGClientListItem : SPDGListItem
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
                includeExpression.Add(item => item["EncodedAbsUrl"]);
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
        }


        public override void ShareWithPeople(IEnumerable<string> emails, bool isEdit)
        {
            if (emails == null || !emails.Any())
            {
                return;
            }
            var userRoles = new List<UserRoleAssignment>();
            foreach (var user in emails)
            {
                UserRoleAssignment role = new UserRoleAssignment();
                role.UserId = user;
                role.Role = isEdit ? Role.Edit : Role.View;
                userRoles.Add(role);
            }
            string message = "Please accept this invite.";
            
            //the parameter before the message is responsible for sending emails
            DocumentSharingManager.UpdateDocumentSharingInfo(_context, (string) _item["EncodedAbsUrl"], userRoles, true, true, false, message, true, true);
            
          
            _context.ExecuteQuery();


            // alternatively we can do this like below, but this does not add any people
            // Web.CreateAnonymousLink(_context, (string)_item["EncodedAbsUrl"], false);
            // Web.CreateOrganizationSharingLink(_context, (string) _item["EncodedAbsUrl"], true);
            // _context.ExecuteQuery();

            // for web sharing the following can be used
            //  WebSharingManager.UpdateWebSharingInformation(_context, _item.ParentList.ParentWeb, userRoles, true, message, true, true);
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

        public override bool SupportsSharing => true;

        public override void Update()
        {
            _context.ExecuteQuery();
        }
    }
}
