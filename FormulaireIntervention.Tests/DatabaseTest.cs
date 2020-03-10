using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormulaireIntervention.Models;

namespace FormulaireIntervention.Tests
{
    [TestClass]
    public class DatabaseTestConnection
    {

        [TestMethod]
        public void TestConnection()
        {
            DBConnection DB = new DBConnection();          
        }

    }
    [TestClass]
    public class DatabaseTestSelect
    {
        int userID=1;
        string testFirstName = "testaba";
        string testLastName = "TEST1312";
        string testAddress = "Testestestest";
        string testPhoneNumber = "000000000";
        [TestInitialize()]
        public void DatabaseInitialize() 
        {
            DBConnection DB = new DBConnection();
            
            DB.InsertNewClientInDatabase(testFirstName, testLastName, testAddress, testPhoneNumber);
            
        }
        [TestMethod()]
        public void TestSelectByName()
        {
            DBConnection DB = new DBConnection();
            int result = 0;
            try
            {
                result = DB.SelectClientIDInDB(testFirstName, testLastName);            
            }
            catch(Exception e)
            {
                throw e;
            }
            userID = result;

            Assert.IsTrue(result != 0);
        }
        [TestMethod()]
        public void TestSelectByID()
        {
            DBConnection DB = new DBConnection();
            List<string> result = new List<string>();
            try
            {
                result.Add(DB.SelectOneClientInDB(userID).ToString());
            }
            catch (Exception e)
            {
                throw e;
            }

            Assert.IsTrue(result.Count != 0);
        }
        [TestCleanup()]
        public void DatabaseCleanup() 
        {
            DBConnection DB = new DBConnection();
            try
            {
                DB.DeleteUserInDB(testFirstName, testLastName);
            }
            catch (Exception e)
            {
                throw e;
            }          
        }
    }
    [TestClass]
    public class DatabaseTestInsert
    {

    }
}
