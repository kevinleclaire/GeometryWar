using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

using System.Collections.Generic;

namespace TP3
{
    /// <summary>
    /// Test pour la StringTable
    /// </summary>
    [TestClass]
    public class TP3Test
    {
        [TestMethod]
        public void TestFichierVide()
        {
            StringTable st = StringTable.GetInstance();
            Assert.AreEqual(ErrorCode.MISSING_FIELD, st.Parse(""));
        }
        [TestMethod]
        public void TestFichierCorrect()
        {
            StringTable st = StringTable.GetInstance();
            Assert.AreEqual(ErrorCode.OK, st.Parse(File.ReadAllText("Data/st.txt")));
        }
        [TestMethod]
        public void TestContenueIncorrect()
        {
            StringTable st = StringTable.GetInstance();
            Assert.AreEqual(ErrorCode.MISSING_FIELD, st.Parse("sdgsfgsdfgsfdg"));
        }
        [TestMethod]
        public void TestItemManquant1()
        {
            StringTable st = StringTable.GetInstance();
            Assert.AreEqual(ErrorCode.MISSING_FIELD, st.Parse(@"ID_TOTAL_TIMETemps total---Total timeID_LIFEVie---Life"));
        }
        [TestMethod]
        public void TestItemManquant2()
        {
            StringTable st = StringTable.GetInstance();
            Assert.AreEqual(ErrorCode.MISSING_FIELD, st.Parse(@"ID_TOTAL_TIME==>Temps totalTotal timeID_LIFE==>VieLife"));
        }
        [TestMethod]
        public void TestItemsManquants3()
        {
            StringTable st = StringTable.GetInstance();
            Assert.AreEqual(ErrorCode.MISSING_FIELD, st.Parse(@" ==> Temps total---Total time ==>Vie--- Life"));
        }
        [TestMethod]
        public void TestGetValueValide()
        {
            StringTable st = StringTable.GetInstance();
            Assert.AreEqual(st.GetValue(Language.French,"ID_TOTAL_TIME"),"Temps total");
        }
        [TestMethod]
        public void TestGetValueInvalide()
        {
            StringTable st = StringTable.GetInstance();
            Assert.AreNotEqual(st.GetValue(Language.English, "ID_TOTAL_TIME"), "Temps total");
        }
    }
}
