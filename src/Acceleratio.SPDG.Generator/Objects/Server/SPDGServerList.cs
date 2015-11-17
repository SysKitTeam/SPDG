using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Acceleratio.SPDG.Generator.Objects.Server
{
    class SPDGServerList :SPDGList
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
                return  new SPDGServerFolder(_spList.RootFolder);
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
                var fieldName=_spList.Fields.Add(fieldInfo.DisplayName, fieldInfo.FieldType, false);
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
                var item=_spList.AddItem();
                foreach (var field in itemInfo.GetAvailableFields())
                {
                    item[field] = itemInfo[field];
                }

                item.Update();
            }
        }
    }
}
