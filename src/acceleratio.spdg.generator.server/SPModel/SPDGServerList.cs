using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Acceleratio.SPDG.Generator.SPModel;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Server.SPModel
{
    class SPDGServerList : SPDGList
    {
        private readonly SPList _spList;

        public override string Title
        {
            get { return _spList.Title; }
        }

        public override string DefaultViewUrl
        {
            get { return _spList.DefaultViewUrl; }
        }

        public override SPDGFolder RootFolder
        {
            get
            {
                return new SPDGServerFolder(_spList.RootFolder);
            }
        }

        public override IEnumerable<SPDGField> Fields
        {
            get
            {
                foreach (SPField field in _spList.Fields)
                {
                    yield return new SPDGField(field.Title, field.InternalName);
                }
            }
        }

        public override void AddFields(IEnumerable<SPDGFieldInfo> fields, bool addToDefaultView)
        {
            var defaultView = _spList.DefaultView;
            foreach (var fieldInfo in fields)
            {
                SPFieldType type;
                switch (fieldInfo.FieldType)
                {
                    case SPDGFieldType.DateTime:
                        type = SPFieldType.DateTime;
                        break;
                    case SPDGFieldType.Integer:
                        type = SPFieldType.Integer;
                        break;
                    case SPDGFieldType.Text:
                        type = SPFieldType.Text;
                        break;
                    default:
                        throw new NotImplementedException();

                }
                var fieldName = _spList.Fields.Add(fieldInfo.DisplayName, type, false);
                if (addToDefaultView)
                {

                    defaultView.ViewFields.Add(fieldName);
                }
            }
        }

        public SPDGServerList(SPList spList)
        {
            _spList = spList;
        }

        public override void AddItems(IEnumerable<ISPDGListItemInfo> items)
        {
            foreach (var itemInfo in items)
            {
                var item = _spList.AddItem();
                foreach (var field in itemInfo.GetAvailableFields())
                {
                    item[field] = itemInfo[field];
                }

                item.Update();
            }
        }

        public override SPDGRoleAssignment GetRoleAssignmentByPrincipal(SPDGPrincipal principal)
        {
            return ServerRoleAssignmentHelper.GetRoleAssignmentByPrincipal(_spList, principal);

        }

        public override void AddRoleAssignment(SPDGPrincipal principal, IEnumerable<SPDGRoleDefinition> roledefinitions)
        {
            ServerRoleAssignmentHelper.AddRoleAssignment(_spList, principal, roledefinitions);
        }

        public override void BreakRoleInheritance(bool copyRoleAssignments)
        {
            _spList.BreakRoleInheritance(copyRoleAssignments);
        }

        public override bool HasUniqueRoleAssignments { get; }

        public override IEnumerable<SPDGListItem> Items
        {
            get
            {
                foreach (SPListItem item in _spList.Items)
                {
                    yield return new SPDGServerListItem(item);
                }
            }
        }

        public override void AddView(string viewName, IEnumerable<string> viewFields, string strQuery, uint rowLimit, bool paged, bool makeDefault)
        {
            var strColl = new StringCollection();
            strColl.AddRange(viewFields.ToArray());
            SPView view = _spList.Views.Add(viewName, strColl, strQuery, rowLimit, paged, makeDefault);
        }

        public override bool IsDocumentLibrary => _spList.BaseTemplate == SPListTemplateType.DocumentLibrary;
    }
}
