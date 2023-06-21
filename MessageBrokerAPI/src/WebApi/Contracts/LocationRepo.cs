using Model;
using System.Collections.Generic;
using System.IO;

namespace WebApi.Contracts
{
    public class LocationRepo
    {

        public IDictionary<int, Location> locations { get; set; }

        public void Init()
        {
            locations = new Dictionary<int, Location>();
            IEnumerable<string> lines = File.ReadLines("locations.txt");


            foreach (string line in lines)
            {
                string[] data=line.Split(",");
                locations.Add(int.Parse(data[0]),new Location { Id=int.Parse(data[0]),Latitude = float.Parse(data[1]), Longitude = float.Parse(data[1]) });
            }
        }

        public Location GetLocation(int id)
        {
            Location loc;
            locations.TryGetValue(id, out loc);
            if (loc != null) return loc;
            return null;
        }
    }
}
