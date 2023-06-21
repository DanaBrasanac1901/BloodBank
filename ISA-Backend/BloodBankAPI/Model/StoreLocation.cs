using System.Collections;

namespace BloodBankAPI.Model
{
    public class StoreLocation
    {

        public Location storedLoc { get; set; }
        public bool isNew { get; set; }
        public Queue locs { get; set; }

        private static StoreLocation instance = null;
        private static readonly object padlock = new object();

        public StoreLocation()
        {
            locs= new Queue();
        }

        public static StoreLocation Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new StoreLocation();
                    }
                    return instance;
                }
            }
        }

        public void Store(Location loc)
        {
            storedLoc = loc;
            locs.Enqueue(loc);
            isNew = true;
        }
    }
}
