using System.Collections.Generic;
using System.Linq;
using System;

using NUnit.Framework;
using ZTJ_Brew_Project_EFClasses.Models;
using Microsoft.EntityFrameworkCore;

namespace ZTJ_Brew_Tests
{
    [TestFixture]
    public class SupplierTableTests
    {
        //Put global var
        BitsContext dbContext;
        Supplier s;
        List<Supplier> suppliers;

        [SetUp]
        public void Setup()
        {
            dbContext = new BitsContext();
            //SHOULD ADD!-----------------------------------------------------------------
            //dbContext.Database.ExecuteSqlRaw("call usp_resetSuppliers()");
        }

        [Test]
        public void GetAllTest()
        {
            suppliers = dbContext.Suppliers.OrderBy(s => s.SupplierId).ToList();
            Assert.AreEqual(6, suppliers.Count);
            Assert.AreEqual("BSG Craft Brewing", suppliers[0].Name);
        }

        [Test]
        public void GetByPrimaryKeyTest()
        {
            s = dbContext.Suppliers.Find(1);
            Assert.IsNotNull(s);
            Assert.AreEqual("BSG Craft Brewing", s.Name);
        }

        [Test]
        public void CreateTest()
        {
            Assert.IsTrue(AddTestSupplier());
            Supplier s2 = dbContext.Suppliers.Where(s => s.Name == "Test").SingleOrDefault();
            Assert.AreEqual("1231231234", s2.Phone);
            Assert.IsTrue(RemoveTestSupplier(s2));
        }

        [Test]
        public void DeleteTest()
        {
            Assert.IsTrue(AddTestSupplier());
            Supplier s2 = dbContext.Suppliers.Where(s => s.Name == "Test").SingleOrDefault();
            Assert.IsTrue(RemoveTestSupplier(s2));
            Assert.IsNull(dbContext.Suppliers.Where(s => s.Name == "Test").SingleOrDefault());
        }

        [Test]
        public void UpdateTest()
        {
            Assert.IsTrue(AddTestSupplier());
            Supplier s2 = dbContext.Suppliers.Where(s => s.Name == "Test").SingleOrDefault();
            s2.Phone = "9879879876";
            dbContext.Suppliers.Update(s2);
            dbContext.SaveChanges();
            Supplier s3 = dbContext.Suppliers.Where(s => s.Name == "Test").SingleOrDefault();
            Assert.AreEqual("9879879876", s3.Phone);
            Assert.IsTrue(RemoveTestSupplier(s3));
        }

        [Test]
        //This function has a z so it will run last out of all the tests
        public void zTestDataNotThere()
        {
            Supplier s2 = dbContext.Suppliers.Where(s => s.Name == "Test").SingleOrDefault();
            Assert.IsNull(s2);
        }

        public bool AddTestSupplier()
        {
            try
            {
                s = new Supplier();
                s.Name = "Test";
                s.Phone = "1231231234";
                s.Email = "test@test.com";
                s.Website = "https://www.test.com";
                s.ContactFirstName = "Real";
                s.ContactLastName = "Person";
                s.ContactPhone = "1235675678";
                s.Note = "Test Data";
                dbContext.Suppliers.Add(s);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveTestSupplier(Supplier remove)
        {
            try
            {
                dbContext.Suppliers.Remove(remove);
                dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
