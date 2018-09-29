using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using TestForDapperOfWeb.Models;


namespace TestForDapperOfWeb.SqlAbout
{
    public class PersonSql
    {
        private string ConnectionString;
        private int count = 0;

        public PersonSql(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("DBConnect:ConnectionString");
        }
        public bool GetPersons(string Name)
        {
            count++;
            using (var con = new SqlConnection(ConnectionString))
            {

                try
                {
                    var list = con.Query<Person>("SELECT * FROM PERSON WHERE Name=@Name", new { Name = Name }).AsList();
                }
                catch (System.Exception)
                {
                    return false;
                }               
            }
            return true;
        }
    }
}
