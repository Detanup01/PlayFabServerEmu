using LiteDB;
using System.Linq.Expressions;

namespace PlayFabEmuCore.BackEnd;

public class DataBaseConnection<T> where T : new()
{
    LiteDatabase DB;
    ILiteCollection<T> Collection;

    public DataBaseConnection()
    {
        DB = new LiteDatabase(new ConnectionString() { Connection = ConnectionType.Shared, Filename = $"{typeof(T).Name}.db" });
        Collection = DB.GetCollection<T>(typeof(T).Name);
    }

    public DataBaseConnection(string db_name)
    {
        DB = new LiteDatabase(new ConnectionString() { Connection = ConnectionType.Shared, Filename = $"{db_name}.db" });
        Collection = DB.GetCollection<T>(typeof(T).Name);
    }
    public DataBaseConnection(string db_name, string collection_name)
    {
        DB = new LiteDatabase(new ConnectionString() { Connection = ConnectionType.Shared, Filename = $"{db_name}.db" });
        Collection = DB.GetCollection<T>(collection_name);
    }

    public virtual void Create(T user)
    {
        Collection.Insert(user);
    }

    public virtual void Update(T user)
    {
        Collection.Upsert(user);
    }

    public virtual List<T> GetList(Expression<Func<T, bool>> predicate)
    {
        return Collection.Find(predicate).ToList();
    }

    public virtual T GetOne(Expression<Func<T, bool>> predicate)
    {
        return Collection.FindOne(predicate);
    }

    public virtual void Close()
    {
        DB.Dispose();
    }
}
