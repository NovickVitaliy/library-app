using System.Data.Common;

namespace CartAndWishlist.DAL.Database.ConnectionAccessor;

public interface IDatabaseConnectionAccessor
{
    const string DatabaseConnectionConfigurationKey = "Default";
    DbConnection GetConnection();
}