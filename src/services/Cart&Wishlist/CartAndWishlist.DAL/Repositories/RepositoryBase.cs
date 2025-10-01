using System.Data;
using System.Data.Common;
using CartAndWishlist.DAL.Database;
using CartAndWishlist.DAL.Database.ConnectionAccessor;
using CartAndWishlist.DAL.Repositories.Implementations;

namespace CartAndWishlist.DAL.Repositories;

public abstract class RepositoryBase
{
    protected DbConnection? Connection;
    protected DbTransaction? Transaction;
    
    public void SetTransaction(DbConnection connection, DbTransaction transaction)
    {
        Connection = connection;
        Transaction = transaction;
    }

    public void ThrowIfConnectionOrTransactionIsUninitialized()
    {
        if (Connection == null || Transaction == null)
            throw new InvalidOperationException("Repository is not initialized with connection or transaction");
    }
}