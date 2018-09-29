using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;
using System.Data;

namespace TestDataBase
{
    public class DBTest
    {
        public string dbLink = "";
        public string _sql="";

        public DBTest(string link)
        {
            dbLink = link;

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
