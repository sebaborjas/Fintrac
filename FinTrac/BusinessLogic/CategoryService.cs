using BusinessLogic;

namespace BusinessLogic
{
    public class CategoryService
    {
        private MemoryDatabase memoryDatabase;

        public CategoryService(MemoryDatabase memoryDatabase)
        {
            this.memoryDatabase = memoryDatabase;
        }
    }
}