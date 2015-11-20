using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace Acceleratio.SPDG.Generator.Objects.Client
{
    class SPDGClientFolder : SPDGFolder
    {
        private readonly Folder _folder;
        private readonly ClientContext _context;

        public SPDGClientFolder(Folder folder, ClientContext context)
        {
            _folder = folder;
            _context = context;
        }

        public override string Name
        {
            get { return _folder.Name; }
        }

        public override string Url
        {
            get { return _folder.ServerRelativeUrl; }
        }

        public override IEnumerable<SPDGFolder> SubFolders
        {
            get
            {
                _context.Load(_folder.Folders);
                _context.ExecuteQuery();
                foreach (var subFolder in _folder.Folders)
                {
                    yield return new SPDGClientFolder(subFolder, _context);
                }
            }
        }

        public override SPDGListItem Item
        {
            get
            {
                return new SPDGClientListItem(_folder.ListItemAllFields, _context);
            }
        }

        public override SPDGFolder AddFolder(string name)
        {
           var subFolder =  _folder.Folders.Add(name);
           _context.Load(subFolder);
            _context.ExecuteQuery();
            return new SPDGClientFolder(subFolder, _context);
        }

        public override SPDGFile AddFile(string url, byte[] content, bool overWrite)
        {
            FileCreationInformation fci=new FileCreationInformation();
            fci.Content = content;
            fci.Url = url;
            fci.Overwrite = overWrite;
            var file=_folder.Files.Add(fci);
            _context.Load(file);
            _context.Load(file.ListItemAllFields, SPDGClientListItem.IncludeExpression);
            _context.ExecuteQuery();
            return new SPDGClientFile(file, _context);
        }

        public override void Update()
        {
           //no update
        }
    }
}
