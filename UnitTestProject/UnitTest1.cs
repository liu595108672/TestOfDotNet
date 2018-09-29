using System;
using TestExtention;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [TestClass,TestCategory("abc")]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPersonExtension()
        {
            Person p = new Person("jay", 24, 1);

            Assert.AreEqual(p.ToFormatedString(), "name: jay ,age: 24 ,gender 1");

            Assert.IsInstanceOfType(p, typeof(Person));  
        }


        [TestMethod]
        [DataRow(1,2,3)]
        [DataRow(3,3,6)]
        [DataRow(2,2,4)]
        
        public void TestSumFunction(int n1,int n2,int sum)
        {
            Assert.AreEqual(Sum(n1, n2), sum);
        }


        [TestMethod]
        public void TestExceptionThrow()
        {
            Assert.ThrowsException<Exception>(()=>ExceptionThrow());
        }


        public int Sum(int n1,int n2)
        {
            return n1 + n2;
        }

        public void ExceptionThrow()
        {
            throw new Exception("New Type Exception");
        }
    }
}
