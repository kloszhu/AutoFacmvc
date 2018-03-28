using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Com.Models.EasyUI;

namespace Common.Tests
{
    [TestClass()]
    public class JsonNullNotOutTests
    {
        [TestMethod()]
        public void NullNotOutTest()
        {
            EasyUITree easyUI = new EasyUITree();
            easyUI.treeid = 2;
           easyUI.NullNotOutJson();
            Assert.Fail();
        }
    }
}