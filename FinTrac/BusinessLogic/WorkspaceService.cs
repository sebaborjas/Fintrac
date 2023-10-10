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
            return _memoryDatabase.Workspaces.First(x => x.ID == ID);
        }

        public void UpdateName(Workspace workspace, string newName)
        {
            _memoryDatabase.Workspaces.First(x => x.ID == workspace.ID).Name = newName;
            _memoryDatabase.Users.FindAll(x => x.WorkspaceList.Contains(workspace)).ForEach(x => x.WorkspaceList.Find(x => x.ID == workspace.ID).Name = newName);
            workspace.Name = newName;

        }

        public void DeleteWorkspace(Workspace workspace)
        {
            User oldUserAdmin = workspace.UserAdmin;
            if (_memoryDatabase.Users.Count(x => x.WorkspaceList.Contains(workspace)) > 1)
            {
                User newUserAdmin = _memoryDatabase.Users.First(x => x.WorkspaceList.Contains(workspace) && x != oldUserAdmin);
                workspace.UserAdmin = newUserAdmin;
            }
            else
            {
                _memoryDatabase.Workspaces.Remove(workspace);
            }
            oldUserAdmin.WorkspaceList.Remove(workspace);
            _memoryDatabase.Users.First(x => x == oldUserAdmin).WorkspaceList.Remove(workspace);
        }
    }

}
