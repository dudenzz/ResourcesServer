using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Server.Model m1 = new Model();
            m1.read10000 += m1_read10000;
            m1.read("C://w2vExample/mens.txt");
            string[] sims = m1.most_similar("men", 2);
            Assert.AreEqual(1, 1);
        }

        void m1_read10000(int current, int size)
        {
        }
    }
}
