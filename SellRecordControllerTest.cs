using BookRepositoryDemo.Controllers;
using BookRepositoryDemo.Model;
using BookRepositoryDemo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookRepositoryDemo_Tests
{
    public class sellRecordControllerTest
    {
        ApplicationDbContext db;
        [SetUp]
        public void Setup()
        {
            var book = new List<SellRecord>
            {
                new SellRecord{Id = 1, BookName = "xyz", SellQty=1000, date =DateTime.Parse("01-01-2020")},
                new SellRecord{Id = 2, BookName = "abc", SellQty=2000, date =DateTime.Parse("02-01-2020")},
                new SellRecord{Id = 3, BookName = "def", SellQty=3000, date =DateTime.Parse("03-01-2020")},
                new SellRecord{Id = 4, BookName = "ghi", SellQty=4000, date =DateTime.Parse("04-01-2020")}
            };
            var bookdata = book.AsQueryable();
            var mockSet = new Mock<DbSet<SellRecord>>();
            mockSet.As<IQueryable<SellRecord>>().Setup(m => m.Provider).Returns(bookdata.Provider);
            mockSet.As<IQueryable<SellRecord>>().Setup(m => m.Expression).Returns(bookdata.Expression);
            mockSet.As<IQueryable<SellRecord>>().Setup(m => m.ElementType).Returns(bookdata.ElementType);
            mockSet.As<IQueryable<SellRecord>>().Setup(m => m.GetEnumerator()).Returns(bookdata.GetEnumerator());
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.SellRecords).Returns(mockSet.Object);
            db = mockContext.Object;
        }


        [Test]
        public void get_DetailAll_Test()
        {
            var BookData = new Mock<SellRecordRepo>(db);
            SellRecordController obj = new SellRecordController(BookData.Object);
            var data = obj.Get();
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
        [Test]
        public void get_ById_Test()
        {
            ISellRecordRepo BookData = new SellRecordRepo(db);
            SellRecordController obj = new SellRecordController(BookData);
            var data = obj.Get(2);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Delete_Test()
        {
            ISellRecordRepo BookData = new SellRecordRepo(db);
            SellRecordController obj = new SellRecordController(BookData);
            var data = obj.Delete(3);
            var okResult = data as OkObjectResult;
            
            //var okResult = data as string; // for checking if string is returned.

            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Add_Detail_Test()
        {
            ISellRecordRepo BookData = new SellRecordRepo(db);
            SellRecordController obj = new SellRecordController(BookData);
            SellRecord book = new SellRecord { BookName = "ttttttttt", SellQty = 990, date = DateTime.Parse("03-01-2020") };
            var data = obj.Post(book);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_Test()
        {
            ISellRecordRepo BookData = new SellRecordRepo(db);
            SellRecordController obj = new SellRecordController(BookData);
            var data = obj.Put(2, new SellRecord {BookName = "abcppp", SellQty = 500, date = DateTime.Parse("02-01-2020") });
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

    }
}