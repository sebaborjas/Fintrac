using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Domain;
using Domain.Exceptions;

namespace BusinessLogic
{
    public class WorkspaceService
    {
        private readonly MemoryDatabase _memoryDatabase;

        public WorkspaceService(MemoryDatabase memoryDatabase)
        {
            this._memoryDatabase = memoryDatabase;
        }

        public void Add(Workspace w)
        {
            if (_memoryDatabase.Workspaces.Any(x => x.ID == w.ID))
            {
                throw new WorkspaceAlreadyExistsException();
            }
            try
            {
                _memoryDatabase.Workspaces.Add(w);
            }
            catch (Exception exception)
            {
                throw exception;
            }

        }

        public Workspace Get(int ID)
        {
            return _memoryDatabase.Workspaces.FirstOrDefault(x => x.ID == ID);
        }

        public void UpdateName(Workspace workspace, string newName)
        {
            Workspace alreadyExists = _memoryDatabase.Workspaces.FirstOrDefault(x => x.Name == newName);

            if (alreadyExists != null)
            {
                throw new WorkspaceAlreadyExistsException();
            }

            this.Get(workspace.ID).Name = newName;
        }

        public void DeleteWorkspace(Workspace workspace)
        {
            _memoryDatabase.Workspaces.Remove(workspace);
        }
    }

}
