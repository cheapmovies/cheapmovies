using System.Linq;

namespace CheapMovies.Store
{
    public class StoreRepository : IStoreRepository
    {
        private StoreDbContext dbContext { get; set; }

        public StoreRepository(StoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void StoreValue(string key, string value) 
        {
            string storedValue = this.GetValue(key);
            if (string.IsNullOrEmpty(storedValue))
            {
                this.AddItem(key, value);
            }
            else
            {
                var storedItem = (from item in this.dbContext.Items
                    where item.Key == key
                    select item).FirstOrDefault();
                storedItem.Value = value;

                this.dbContext.Items.Update(storedItem);
                this.dbContext.SaveChanges();
            }
        }

        public void AddItem(string key, string value) 
        {
            Item item = new Item();
            item.Key = key;
            item.Value = value;

            this.dbContext.Items.Add(item);
            this.dbContext.SaveChanges();
        }

        public string GetValue(string key)
        {
            var result = from item in this.dbContext.Items
                where item.Key == key
                select item.Value;

            if (result == null)
            {
                return string.Empty;
            }
            
            return result.FirstOrDefault();
        }
    }
}
