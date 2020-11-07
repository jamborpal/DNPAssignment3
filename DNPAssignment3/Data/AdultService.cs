using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Models;

namespace Assignment2.Data
{
    public class AdultService : IAdultService
    {
        
        public IList<Adult> adults { get; set; }
        private string adultFile = "adults.json";

        public AdultService()
        {
            string content = File.ReadAllText(adultFile);
            
            adults = JsonSerializer.Deserialize<List<Adult>>(content);
            
        }
        private void WriteToFile()
        {
            string productAsJson = JsonSerializer.Serialize(adults);
            File.WriteAllText(adultFile, productAsJson);
        }
        public async Task<IList<Adult>> GetAdultsAsync()
        {
            return adults;
        }
        

        public async Task<Adult> AddAdultAsync(Adult adult)
        {
            adults.Add(adult);
            WriteToFile();
            return adult;

        }

        

        public async Task RemoveAdultAsync(int id)
        {
            Adult toRemove = adults.First(t => t.Id == id);
            adults.Remove(toRemove);
            WriteToFile();
        }
        

        public void EditAdult()
        {
            throw new System.NotImplementedException();
        }

        
    }
}