# Library Management

This C# project is a simple library management system that allows users to manage books, authors, and users. The system supports adding, borrowing, and returning books, as well as displaying available books, borrowed books, users, and authors.

## Features

- Add new books, authors, and users
- Borrow and return books
- Display all books, borrowed books, and available books
- Display all users and authors

## Classes Overview

### Person

An abstract class representing a person with first and last names.

### User

A class representing a user of the library, inheriting from `Person`.

### Author

A class representing an author, inheriting from `Person`.

### Book

A class representing a book in the library, with properties for ID, title, author, and borrowed status.

### Library

A class implementing the `IBookManagement` interface, responsible for managing books, users, and authors in the library.

## Interface

### IBookManagement

An interface defining methods for adding and borrowing books.

## Usage

To use this library management system, run the `Main` method in `Program.cs`. The following options are available through a console menu:

1. Add book
2. Borrow book
3. Display books
4. Display users
5. Display authors
6. Add author
7. Display borrowed books
8. Exit

## Example

Here's an example of how to add a book and borrow it:

1. Add a user:
   ```csharp
   User user1 = new User(101, "Pior", "Zieliński");
   library.AddUser(user1);
   ```

2. Add an author:
   ```csharp
   Author author1 = new Author(library.NextAuthorId(), "Jan", "Kowalski");
   library.AddAuthor(author1);
   ```

3. Add a book:
   ```csharp
   Book book1 = new Book(library.NextBookId(), "C# For Beginners", author1, false, null);
   library.AddBook(book1);
   ```

4. Borrow a book:
   ```csharp
   library.BorrowBook(book1.BookID, user1.UserId);
   ```

## Installation

Clone the repository and open the project in your preferred C# IDE.

```bash
git clone https://github.com/baskaaleksander/LibraryManagment.git
```

Build and run the project to start using the library management system.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any improvements.

## License

This project is licensed under the MIT License.
