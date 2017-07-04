using System.Collections.Generic;

namespace OrmComparison
{
    public interface IDataAccess
    {
        string Name { get; }
        Dto SelectOne(int id);
        List<Dto> SelectAll();
        int RunNonQuery();
    }
}