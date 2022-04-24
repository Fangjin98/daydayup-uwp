using DayDayUp.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayDayUp.Services
{
    public interface IDataAccess
    {
        Task<List<Todo>> GetDataAsync();

        List<Todo> GetData();

        void AddDataAsync(Todo item);

        Task DeleteDataAsync(Todo item);

        Task UpdateDataAsync(Todo item);
    }


    public class LiteDbDataAccess : IDataAccess
    {
        public LiteDbDataAccess()
        {
            name = "MyTodos";

            var filePath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, name);
            Debug.WriteLine(filePath);
            db = new LiteDatabase(filePath);
        }

        public async void AddDataAsync(Todo item)
        {
            var collection=db.GetCollection<Todo>();
            collection.Insert(item);
        }

        public async Task DeleteDataAsync(Todo item)
        {
            var collection = db.GetCollection<Todo>();
            collection.Delete(item.Id);
        }

        public List<Todo> GetData()
        {
            return db.GetCollection<Todo>().FindAll().ToList();
        }

        public Task<List<Todo>> GetDataAsync()
        {
            return Task.FromResult(db.GetCollection<Todo>().FindAll().ToList());
        }

        public async Task UpdateDataAsync(Todo item)
        {
            var collection = db.GetCollection<Todo>();
            collection.Update(item);
        }
        
        private LiteDatabase db;

        private string name;

    }
}
