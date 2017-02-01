using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace WebApplication.Services
{
    public class DataService : IDataService
    {
        private readonly ConcurrentDictionary<int, string> _data = new ConcurrentDictionary<int, string>();

        public DataService()
        {
            _data.TryAdd(1, "value1");
            _data.TryAdd(2, "value2");
        }

        public IEnumerable<string> SelectAll()
        {
            return _data.Values;
        }

        public string SelectById(int id)
        {
            if (_data.TryGetValue(id, out string value))
                return value;

            return "<no data found>";
        }

        public void Add(string value)
        {
            var id = 0;
            while (!_data.TryAdd(id++, value)) { };
        }

        public void Insert(int id, string value)
        {
            _data.AddOrUpdate(id, value, (i, v) => value);
        }

    }
}
