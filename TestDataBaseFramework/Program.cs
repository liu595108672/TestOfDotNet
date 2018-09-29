using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;

namespace TestDataBaseFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            string link = "server=.;database=DBForTest;integrated security=SSPI";
            SqlConnection conn = new SqlConnection(link);


            string linkByUser = "server=.;database=DBForTest;uid=sa;pwd=123";
            string name = "Jay.Liu";
            SqlConnection conn2 = new SqlConnection(linkByUser);
            try
            {
                conn.Open();
                SqlCommand sc = new SqlCommand(string.Format(@"INSERT INTO PERSON(NAME,AGE,GENDER) VALUES('{0}',24,'male')",name), conn);
                int? count = (int?) sc.ExecuteScalar();
                Console.WriteLine(count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.Read();

        }
    }
}
