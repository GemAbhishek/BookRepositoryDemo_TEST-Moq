using BookRepositoryDemo.Controllers;
using BookRepositoryDemo.Model;
using BookRepositoryDemo.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BookRepositoryDemo_Tests
{
    public class BookControllertest
    {
        ApplicationDbContext db;
        [SetUp]
        public void Setup()
        {
            var book = new List<Book>
            {
                new Book{Id = 1, Name = "XYZ", Author="zzz", Price =111, IsAvailable = true},
                new Book{Id = 2, Name = "ABC", Author="ccc", Price =222, IsAvailable = true},
                new Book{Id = 3, Name = "EFG", Author="ggg", Price =333, IsAvailable = true},
                new Book{Id = 4, Name = "HIJ", Author="jjj", Price =444, IsAvailable = true},
                new Book{Id = 5, Name = "KLM", Author="mmm", Price =555, IsAvailable = true}                
            };
            var bookdata = book.AsQueryable();
            var mockSet = new Mock<DbSet<Book>>();
            mockSet.As<IQueryable<Book>>().Setup(m => m.Provider).Returns(bookdata.Provider);
            mockSet.As<IQueryable<Book>>().Setup(m => m.Expression).Returns(bookdata.Expression);
            mockSet.As<IQueryable<Book>>().Setup(m => m.ElementType).Returns(bookdata.ElementType);
            mockSet.As<IQueryable<Book>>().Setup(m => m.GetEnumerator()).Returns(bookdata.GetEnumerator());
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Books).Returns(mockSet.Object);
            db = mockContext.Object;
        }


        [Test]
        public void get_DetailAll_test()
        {
            IBookRepo BookData = new BookRepo(db);
            BookController obj = new BookController(BookData);
            var data = obj.Get();
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [Test]
        public void get_ById_Test()
        {
            IBookRepo BookData = new BookRepo(db);
            BookController obj = new BookController(BookData);
            var data = obj.Get(2);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Delete_Test()
        {
            IBookRepo BookData = new BookRepo(db);
            BookController obj = new BookController(BookData);
            var data = obj.Delete(2);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Add_Detail_Test()
        {
            IBookRepo BookData = new BookRepo(db);
            BookController obj = new BookController(BookData);
            Book book = new Book { Name = "QRS", Author = "sss", Price = 777, IsAvailable = true };
            var data = obj.Post(book);
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        public void Update_Test()
        {
            IBookRepo BookData = new BookRepo(db);
            BookController obj = new BookController(BookData);
            var data = obj.Put(5,new Book { Name = "AAAA", Author = "aaa", Price = 222, IsAvailable = true });
            var okResult = data as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}