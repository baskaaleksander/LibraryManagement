namespace LibraryManagment
{
    internal class Program
    {
        public abstract class Person
        {
            public string FirstName;
            public string LastName;
            public abstract void GetInformation();
           
        }

        public class User : Person
        {
            public int UserId;

            public User(int userId, string firstName, string lastName)
            {
                UserId = userId;
                FirstName = firstName;
                LastName = lastName;
            }

            public override void GetInformation()
            {
                Console.WriteLine($"User: ID {UserId} Name {FirstName} Surname {LastName}");
            }
        }
        public class Author : Person
        {
            public int AuthorID { get; set; }
            public Author(int authorID, string firstName, string lastName)
            {
                AuthorID = authorID;
                FirstName = firstName;
                LastName = lastName;
            }

            public override void GetInformation()
            {
                Console.WriteLine($"Author: ID {AuthorID} Name {FirstName} Surname {LastName}");
            }
        }


        public class Book
        {
            public int BookID { get; set; } 
            public string Title { get; set; }
            public Author Author { get; set; }
            public bool IsBorrowed { get; set; }
            public User BorrowedBy {  get; set; }

            public Book(int bookID, string title, Author author, bool isBorrowed, User borrowedBy)
            {
                BookID = bookID;
                Title = title;
                Author = author;
                IsBorrowed = isBorrowed;
                BorrowedBy = borrowedBy;
            }
            public void GetInformation()
            {
                Console.WriteLine($"Book: ID {BookID} Title {Title} Author {Author.FirstName} {Author.LastName}");
            }
            public void GetBorrowedInformation()
            {
                if (IsBorrowed)
                {
                    Console.WriteLine($"Borrowed by:");
                    BorrowedBy?.GetInformation();
                }
                else
                {
                    Console.WriteLine("Book is not borrowed.");
                }
            }

        }
        interface IBookManagement
        {

            void AddBook(Book book);
            void BorrowBook(int bookId, int userId);
        }

        public class Library : IBookManagement
        {
            List<Book> books { get; set; }
            List<User> users { get; set; }
            List<Author> authors { get; set; }
            int nextBookId { get; set; }
            int nextAuthorId { get; set; }
            int bookCount { get; set; }

            public Library(List<Book> books, List<User> users, List<Author> authors)
            {
                this.books = books;
                this.users = users;
                this.authors = authors;
                this.nextBookId = 1;
                this.nextAuthorId = 1;
                this.bookCount = 0;
            }

            public void AddBook(Book book) 
            {
                nextBookId++;
                bookCount++;
                books.Add(book);
            }
            public int NextAuthorId()
            {
                return nextAuthorId;
            }
            public int NextBookId()
            {
                return nextBookId;
            }
            public void BorrowBook(int bookId, int userId)
            {
                var book = books.FirstOrDefault(b => b.BookID == bookId);
                var user = GetUserByID(userId);
                if (book == null)
                {
                    Console.WriteLine("Couldn't find book with that ID.");
                    return;
                }
                if (user == null)
                {
                    Console.WriteLine("Couldn't find user with that ID.");
                    return;
                }
                if (book.IsBorrowed)
                {
                    Console.WriteLine("Book is already borrowed.");
                    return;
                }
                book.IsBorrowed = true;
                book.BorrowedBy = user;
                Console.WriteLine($"Book '{book.Title}' is borrowed by {user.FirstName} {user.LastName}.");
            }
            public void ReturnBook(int bookId)
            {
                var book = books.FirstOrDefault(b => b.BookID == bookId);
                if (book != null && book.IsBorrowed)
                {
                    book.IsBorrowed = false;
                    book.BorrowedBy = null;
                    Console.WriteLine($"Book '{book.Title}' got returned.");
                }
                else
                {
                    Console.WriteLine("You can't return this book.");
                }
            }


            public void AddUser(User user)
            {
                users.Add(user);
            }
            public void AddAuthor(Author author) 
            {
                nextAuthorId++;
                authors.Add(author);
            }
            public void DisplayBooks() 
            {
                foreach (Book book in books)
                {
                    book.GetInformation();
                }
            }
            public void DisplayBorrowedBooks() 
            { 
                foreach (Book book in books)
                {
                    if (book.IsBorrowed)
                    {
                        book.GetInformation();
                    }
                }
            }
            public void DisplayNonBorrowedBooks()
            {
                foreach (Book book in books)
                {
                    if (!book.IsBorrowed)
                    {
                        book.GetInformation();
                    }
                }
            }
            public void DisplayUsers() 
            {
                foreach(User user in users)
                {
                    user.GetInformation();
                }
            }
            public void DisplayAuthors() 
            { 
                foreach(Author author in authors)
                {
                    Console.WriteLine($"Author: ID {author.AuthorID} Name {author.FirstName} Surname {author.LastName}");
                }
            }
            private T GetByID<T>(List<T> list, Func<T, bool> predicate)
            {
                return list.FirstOrDefault(predicate);
            }

            public User GetUserByID(int id) => GetByID(users, u => u.UserId == id);
            public Author GetAuthorByID(int id) => GetByID(authors, a => a.AuthorID == id);

        }
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            List<User> users = new List<User>();
            List<Author> authors = new List<Author>();
            Library library = new Library(books, users, authors);
            User user1 = new User(101, "Pior", "Zieliński");
            library.AddUser(user1);
            User user2 = new User(102, "Jan", "Kowalski");
            library.AddUser(user2);
            Author author1 = new Author(library.NextAuthorId(), "Jan", "Kowalski");
            library.AddAuthor(author1);
            Author author2 = new Author(library.NextAuthorId(), "Anna", "Nowak");
            library.AddAuthor(author2);
            Author author3 = new Author(library.NextAuthorId(), "Robert", "Marting");
            library.AddAuthor(author3);
            Book book1 = new Book(library.NextBookId(), "C# For begginers", author1, false, null);
            library.AddBook(book1);
            Book book2 = new Book(library.NextBookId(), "Advanced C#", author2, false, null);
            library.AddBook(book2);
            Book book3 = new Book(library.NextBookId(), "Projects Templates", author3, false, null);
            library.AddBook(book3);

            while (true)
            {
            try
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. Borrow book");
                Console.WriteLine("3. Display books");
                Console.WriteLine("4. Display users");
                Console.WriteLine("5. Display authors");
                Console.WriteLine("6. Add author");
                Console.WriteLine("7. Display borrowed books");
                Console.WriteLine("8. Exit");
                Console.WriteLine("Choose option:");
                int wybor = int.Parse(Console.ReadLine());
                switch (wybor)
                {
                case 1:
                    Console.WriteLine("Enter the book name");
                    string nazwaKsiazki = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(nazwaKsiazki))
                    {
                        Console.WriteLine("Name field cannot be empty.");
                        break;
                    }
                    library.DisplayAuthors();
                    Console.WriteLine("Enter author ID");
                    if (!int.TryParse(Console.ReadLine(), out int idAutora))
                    {
                        Console.WriteLine("Invalid author ID format.");
                        break;
                    }
                    if (library.GetAuthorByID(idAutora) == null)
                    {
                        Console.WriteLine("Cannot find author with following ID.");
                        break;
                    }
                    Book book = new Book(library.NextBookId(), nazwaKsiazki, library.GetAuthorByID(idAutora), false, null);
                    library.AddBook(book);
                    break;
                case 2:
                    library.DisplayNonBorrowedBooks();
                    Console.WriteLine("Enter book ID");
                    if (!int.TryParse(Console.ReadLine(), out int idKsiazki))
                    {
                        Console.WriteLine("Invalid book ID format.");
                        break;
                    }

                    library.DisplayUsers();
                    Console.WriteLine("Enter user ID.");
                    if (!int.TryParse(Console.ReadLine(), out int idUsera))
                    {
                        Console.WriteLine("Invalid user ID format.");
                        break;
                    }

                    if (library.GetUserByID(idUsera) == null)
                    {
                        Console.WriteLine("Cannot find user with following ID.");
                        break;
                    }

                    library.BorrowBook(idKsiazki, idUsera);

                    break;
                case 3:
                    Console.WriteLine("Books");
                    library.DisplayBooks();
                    break;
                case 4:
                    Console.WriteLine("Users");
                    library.DisplayUsers();
                    break;
                case 5:
                    Console.WriteLine("Authors");
                    library.DisplayAuthors();
                    break;
                case 6:
                    Console.WriteLine("Enter author name");
                    string firstName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(firstName))
                    {
                        Console.WriteLine("Author name field cannot be empty.");
                        break;
                    }
                    Console.WriteLine("Podaj nazwisko");
                    string lastName = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(lastName))
                    {
                        Console.WriteLine("Author surname field cannot be empty.");
                        break;
                    }
                    Author author = new Author(library.NextAuthorId(), firstName, lastName);
                    library.AddAuthor(author);
                    break;
                case 7:
                    Console.WriteLine("Borrowed books");
                    library.DisplayBorrowedBooks();
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Invalid option");
                    return;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Data format error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            }
        }
    }
}
