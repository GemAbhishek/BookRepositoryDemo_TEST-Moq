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
    public class sellControllerTest
    {
        ApplicationDbContext db;
        [SetUp]
        public void Setup()
        {
            var book = new List<SellRecord>
            {
                new SellRecord{Id = 1, BookName = "xyz", SellQty=1000, date =DateTime.Parse("01-01-2020")},
                new SellRecord{Id = 2, BookName = "abc", SellQty=2000, date =DateTime.Parse("02-01-2020")}
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
        public void Update_Test()
        {
            ISellRepo BookData = new SellRepo(db);
            SellController obj = new SellController(BookData);
            var data = obj.Put(1,50);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            
        }
    }
}