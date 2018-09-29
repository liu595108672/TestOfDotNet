using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;

namespace TestDataBase
{
    public class DBTest
    {
        public string dbLink = "";
        public string _sql="";
        private SqlConnection conn = null;

        public DBTest(string link)
        {
            dbLink = link;
            conn = new SqlConnection(link);
        }
        public void InsertIntoDB(string sql)
        {
            _sql = sql;
            InsertIntoDB();
        }

        public void InsertIntoDB()
        {

        }
    }
}
