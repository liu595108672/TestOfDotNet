using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExtention
{
    public static class PersonExtension
    {
        public static string ToString(this Person person)
        {
            return "This is a extend function ToString()";
        }

        public static string ToFormatedString(this Person person)
        {
            return string.Format("name: {0} ,age: {1} ,gender {2}",person.name,person.age,person.gender);
        }
    }

    public class Person
    {
        public string name { set; get; }
        public int age { set; get; }
        public int gender { set; get; }

        public Person(string name,int age,int gender)
        {
            this.name = name;
            this.age = age;
            this.gender = gender;
        }
        
        public Person()
        {

        }

        public override string ToString()
        {
            return "This is a overrided function ToString()";
        }

    }
}
