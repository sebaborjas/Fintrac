using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Domain;
using Domain.Exceptions;

namespace BusinessLogic
{
    public class GoalService
    {
        private readonly MemoryDatabase _memoryDatabase;

        public GoalService(MemoryDatabase memoryDatabase)
        {
            _memoryDatabase = memoryDatabase;
        }

        public void Add(Workspace workspace, Goal goal)
        {
            if (workspace.GoalList.Contains(goal))
            {
                throw new GoalAlreadyExistsException();
            }
            if (goal.Categories.Count == 0)
            {
                throw new EmptyCategoryListException();
            }
            try
            {
                workspace.GoalList.Add(goal);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public Goal Get(Workspace workspace, string title)
        {
            return _memoryDatabase.Users.Find(x => x.WorkspaceList.Contains(workspace)).WorkspaceList.Find(x => x.ID == workspace.ID).GoalList.Find(x => x.Title == title);

        }
    }
}