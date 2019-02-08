using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PruebasJSON.Data;
using PruebasJSON.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PruebasJSON.Helpers;

namespace PruebasJSON.Services
{
    public class LookupTableService : ILookupTableService
    {
        DataContext _context;
        public LookupTableService(DataContext context)
        {
            _context = context;
        }

        public dynamic GetDeserializedJson()
        {
            string filepath = "Files/LUT Json ejemplo.json";

            using (StreamReader r = new StreamReader(filepath))
            {
                var json = r.ReadToEnd();

                return JsonConvert.DeserializeObject(json);

            }
        }
        public void GetLookupData()
        {
            LookupTable lookupTable = new LookupTable()
            {
                Code = "AAA",
                Description = "AAA"
            };
            
            var mySet = _context.Set(lookupTable.GetType());
            _context.Add(lookupTable);
            _context.SaveChanges();
        }
    }

    public interface ILookupTableService
    {
        dynamic GetDeserializedJson();
        void GetLookupData();
    }
}
