
using System.Collections;

namespace DrugControl.Shared
{
    public class RepositoryBase
    {
        public ArrayList list = new ArrayList();

        public int idCounter = 1;

        public void increaseId() 
        { 
            idCounter++; 
        }

        public EntityBase GetId(int selectedId, RepositoryBase repository)
        {
            EntityBase entity = null;

            foreach (EntityBase entityAdded in repository.list)
            {
                if (entityAdded.id == selectedId)
                {
                    entity = entityAdded;
                    break;
                }
            }
            return entity;
        }

        public int isValidId(int selectedId, RepositoryBase repository)
        {
            do
            {
                if (selectedId <= 0 || selectedId > repository.idCounter - 1)
                {
                    InterfaceBase.ColorfulMessage("\nThis ID doesn't exist. Try again:" + "\n→ ", ConsoleColor.Red);
                    selectedId = Convert.ToInt32(Console.ReadLine());
                }

                else { break; }

            } while (true);

            return selectedId;
        }

        public bool HasEntity()
        {
            if (list.Count == 0) { return false; }
            else { return true; }
        }

        public void AddNewEntity(EntityBase entity)
        {
            list.Add(entity);
            entity.id = idCounter;
            increaseId();
        }

        public void RemoveEntity(int selectedId, RepositoryBase repository)
        {
            EntityBase entity = GetId(selectedId, repository);

            if (entity != null) { repository.list.Remove(entity); }
        }
    }
}
