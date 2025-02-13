using LiteDB;
using System.Linq.Expressions;

namespace PlayFabEmuCore.BackEnd;

public class DataBaseConnection<T> where T : new()
{
    LiteDatabase DB;
    ILiteCollection<T> Collection;

    public DataBaseConnection()
    {
        DB = new LiteDatabase(new ConnectionString() { Connection = ConnectionType.Shared, Filename = $"{nameof(T)}.db" });
        Collection = DB.GetCollection<T>(nameof(T));
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
