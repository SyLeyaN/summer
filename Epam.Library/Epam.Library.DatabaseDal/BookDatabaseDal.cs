using Epam.Library.DalContracts;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Epam.Library.DatabaseDal
{
    public class BookDatabaseDal : IBookDal
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["Library_DataBase_admin"].ConnectionString;
        public int Add(Book book)
        {
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertBook";
                cmd.Parameters.AddWithValue(@"Title", book.Title);
                cmd.Parameters.AddWithValue(@"NumberOfPages", book.NumberOfPages);
                cmd.Parameters.AddWithValue(@"PublishingYear", book.PublishingYear);
                cmd.Parameters.AddWithValue(@"Note", book.Note ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue(@"PublishingCity", book.PublishingCity);
                cmd.Parameters.AddWithValue(@"PublishingHouse", book.PublishingHouse);
                cmd.Parameters.AddWithValue(@"ISBN", book.ISBN ?? (object)DBNull.Value);

                if (book.Authors.Any())
                {
                    DataTable persons = new DataTable();
                    persons.Columns.Add("PersonId", typeof(int));
                    foreach (var person in book.Authors)
                    {
                        if (book.Authors.Any())
                            persons.Rows.Add(person.Id);
                    }

                    cmd.Parameters.Add(new SqlParameter("TableOfPersons", persons));
                }

                var id = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "Id",
                    Direction = ParameterDirection.Output
                };

                cmd.Parameters.Add(id);

                var returnValue = new SqlParameter
                {
                    DbType = DbType.Int32,
                    Direction = ParameterDirection.ReturnValue
                };

                cmd.Parameters.Add(returnValue);

                connect.Open();
                cmd.ExecuteNonQuery();

                if ((int)returnValue.Value == -1)
                {
                    throw new ObjectNotUniqueException();
                }

                book.Id = (int)id.Value;
            }
            return book.Id;
        }

        public IEnumerable<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllBooks";

                connect.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book newBook = new Book();

                        newBook.Id = (int)reader["Id"];
                        newBook.Title = (string)reader["Title"];
                        newBook.PublishingYear = (int)reader["PublishingYear"];
                        newBook.PublishingHouse = (string)reader["PublishingHouse"];
                        newBook.PublishingCity = (string)reader["PublishingCity"];
                        newBook.NumberOfPages = (int)reader["NumberOfPages"];
                        var isbn = reader["ISBN"];
                        if (isbn != DBNull.Value)
                        {
                            newBook.ISBN = (string)isbn;
                        }
                        newBook.Note = (string)reader["Note"];

                        books.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["Id"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = books.FirstOrDefault(b => b.Id == bookId);
                        book.Authors.Add(newPerson);
                    }
                }
            }
            return books;
        }

        public ILookup<string, Book> GetAndGroupByPublishingHouse(string publishingHouseFilter)
        {
            List<Book> books = new List<Book>();

            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectByPublishingHouseBooks";
                cmd.Parameters.AddWithValue(@"PublishingHouseFilter", publishingHouseFilter);

                connect.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book newBook = new Book();

                        newBook.Id = (int)reader["Id"];
                        newBook.Title = (string)reader["Title"];
                        newBook.PublishingYear = (int)reader["PublishingYear"];
                        newBook.PublishingHouse = (string)reader["PublishingHouse"];
                        newBook.PublishingCity = (string)reader["PublishingCity"];
                        newBook.NumberOfPages = (int)reader["NumberOfPages"];
                        var isbn = reader["ISBN"];
                        if (isbn != DBNull.Value)
                        {
                            newBook.ISBN = (string)isbn;
                        }
                        newBook.Note = (string)reader["Note"];

                        books.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["Id"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = books.FirstOrDefault(b => b.Id == bookId);
                        book.Authors.Add(newPerson);
                    }
                }
            }

            ILookup<string, Book> resultCollection = books.ToLookup(b => b.PublishingHouse, b => b);

            return resultCollection;
        }

        public IEnumerable<Book> GetByAuthor(int authorId)
        {
            List<Book> books = new List<Book>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectByAuthorBooks";
                cmd.Parameters.AddWithValue(@"AuthorId", authorId);

                connect.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book newBook = new Book();

                        newBook.Id = (int)reader["Id"];
                        newBook.Title = (string)reader["Title"];
                        newBook.PublishingYear = (int)reader["PublishingYear"];
                        newBook.PublishingHouse = (string)reader["PublishingHouse"];
                        newBook.PublishingCity = (string)reader["PublishingCity"];
                        newBook.NumberOfPages = (int)reader["NumberOfPages"];
                        var isbn = reader["ISBN"];
                        if (isbn != DBNull.Value)
                        {
                            newBook.ISBN = (string)isbn;
                        }
                        newBook.Note = (string)reader["Note"];

                        books.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["PersonId"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = books.FirstOrDefault(b => b.Id == bookId);
                        book.Authors.Add(newPerson);
                    }
                }
            }
            return books;
        }

        public Book GetById(int id)
        {
            Book newBook = new Book();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectByIdBooks";
                cmd.Parameters.AddWithValue("BookId", id);

                connect.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newBook.Id = (int)reader["Id"];
                        newBook.Title = (string)reader["Title"];
                        newBook.PublishingYear = (int)reader["PublishingYear"];
                        newBook.PublishingHouse = (string)reader["PublishingHouse"];
                        newBook.PublishingCity = (string)reader["PublishingCity"];
                        newBook.NumberOfPages = (int)reader["NumberOfPages"];
                        var isbn = reader["ISBN"];
                        if (isbn != DBNull.Value)
                        {
                            newBook.ISBN = (string)isbn;
                        }
                        newBook.Note = (string)reader["Note"];
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["Id"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];

                        newBook.Authors.Add(newPerson);
                    }
                }
            }
            return newBook;
        }
    }
}
