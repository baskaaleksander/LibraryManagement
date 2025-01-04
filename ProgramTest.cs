using System;
using System.Collections.Generic;
using System.Linq;
using kolokwium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static LibraryManagment.Program;

namespace kolokwium.Tests
{
    [TestClass]
    public class LibraryTests
    {
        private Library library;
        private List<Book> books;
        private List<User> users;
        private List<Author> authors;

        [TestInitialize]
        public void Setup()
        {
            books = new List<Book>();
            users = new List<User>();
            authors = new List<Author>();
            library = new Library(books, users, authors);

            var author = new Author(1, "John", "Doe");
            authors.Add(author);

            var user = new User(1, "Jane", "Doe");
            users.Add(user);

            var book = new Book(1, "Test Book", author, false, null);
            books.Add(book);
        }

        [TestMethod]
        public void BorrowBook_BookNotFound_ShouldDisplayErrorMessage()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                library.BorrowBook(999, 1);

                var expected = "Couldn't find book with that ID\r\n";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void BorrowBook_UserNotFound_ShouldDisplayErrorMessage()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                library.BorrowBook(1, 999);

                var expected = "Couldn't find user with that ID\r\n";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void BorrowBook_BookAlreadyBorrowed_ShouldDisplayErrorMessage()
        {
            books[0].IsBorrowed = true;

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                library.BorrowBook(1, 1);

                var expected = "Book is already borrowed.\r\n";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void BorrowBook_Success_ShouldBorrowBook()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                library.BorrowBook(1, 1);

                var expected = "Book 'Test Book' is borrowed by Jane Doe.\r\n";
                Assert.AreEqual(expected, sw.ToString());
                Assert.IsTrue(books[0].IsBorrowed);
                Assert.AreEqual(users[0], books[0].BorrowedBy);
            }
        }
        [TestMethod]
        public void ReturnBook_BookNotFound_ShouldDisplayErrorMessage()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                library.ReturnBook(999);

                var expected = "You can't return this book.\r\n";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void ReturnBook_BookNotBorrowed_ShouldDisplayErrorMessage()
        {
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                library.ReturnBook(1);

                var expected = "You can't return this book.\r\n";
                Assert.AreEqual(expected, sw.ToString());
            }
        }

        [TestMethod]
        public void ReturnBook_Success_ShouldReturnBook()
        {
            books[0].IsBorrowed = true;
            books[0].BorrowedBy = users[0];

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                library.ReturnBook(1);

                var expected = "Book 'Test Book' got returned.\r\n";
                Assert.AreEqual(expected, sw.ToString());
                Assert.IsFalse(books[0].IsBorrowed);
                Assert.IsNull(books[0].BorrowedBy);
            }
        }

    }
}
