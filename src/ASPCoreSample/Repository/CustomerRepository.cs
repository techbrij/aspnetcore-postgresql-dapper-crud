using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using ASPCoreSample.Models;

namespace ASPCoreSample.Repository
{
    public class CustomerRepository : IRepository<Customer>
    {
        private string connectionString;
        public CustomerRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }

        public void Add(Customer item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO customer (name,phone,email,address) VALUES(@Name,@Phone,@Email,@Address)", item);
            }

        }

        public IEnumerable<Customer> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Customer>("SELECT * FROM customer");
            }
        }

        public Customer FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<Customer>("SELECT * FROM customer WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM customer WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(Customer item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE customer SET name = @Name,  phone  = @Phone, email= @Email, address= @Address WHERE id = @Id", item);
            }
        }
    }
}
