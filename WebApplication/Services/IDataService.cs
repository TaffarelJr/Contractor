using System.Collections.Generic;

namespace WebApplication.Services
{
    public interface IDataService
    {
        IEnumerable<string> SelectAll();

        string SelectById(int id);

        void Add(string value);

        void Insert(int id, string value);

    }
}
