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
        public void GetLookupData<T>(T lookupTable) where T: class
        {
            //TableA lookupTable = new TableA()
            //{
            //    Code = "AAA",
            //    Description = "AAA"
            //};

            //string tableName = "Cat";
            //var type = Assembly.GetExecutingAssembly()
            //        .GetTypes()
            //        .FirstOrDefault(t => t.Name == tableName);

            //var mySet = _context.Set(lookupTable.GetType());
            _context.Add(lookupTable);
            _context.SaveChanges();
        }
        public void Prueba(object test)
        {
            _context.Add(test);
            _context.SaveChanges();
        }

    }

    public interface ILookupTableService
    {
        dynamic GetDeserializedJson();
        void GetLookupData<T>(T lookupTable) where T : class;
        void Prueba(object test);
    }
}
