using Epam.Library.DalContracts;
using Epam.Library.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Epam.Library.DatabaseDal
{
    public class LibraryObjectDatabaseDal : ILibraryObjectDal
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["Library_DataBase_admin"].ConnectionString;

        public bool CheckObjectLikeDeletedById(int id)
        {
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "CheckObjectLikeDeletedById";
                cmd.Parameters.AddWithValue(@"ObjectId", id);

                connect.Open();
                var rowsCount = cmd.ExecuteNonQuery();
                return rowsCount > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteByIdLibraryObject";
                cmd.Parameters.AddWithValue(@"IdForDelete", id);

                connect.Open();
                var rowsCount = cmd.ExecuteNonQuery();
                return rowsCount > 0;
            }
        }
        public IEnumerable<LibraryObject> GetAll()
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllLibraryObjects";

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["PersonId"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }
                    reader.NextResult();


                    
                }
            }
            return libraryObjects;
        }

        public IEnumerable<LibraryObject> GetAllDeletedObjects()
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllDeletedObjects";

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["PersonId"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }
                    reader.NextResult();


                    
                }
            }
            return libraryObjects;
        }

        public IEnumerable<LibraryObject> GetBooksPatentsByPerson(int personId)
        {

            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectBooksPatentsByPerson";
                cmd.Parameters.AddWithValue(@"PersonId", personId);

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }

                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["PersonId"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }
                }
            }
            return libraryObjects;
        }

        public IEnumerable<LibraryObject> GetByTitle(string title)
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectByTitleLibraryObjects";
                cmd.Parameters.AddWithValue(@"Title", title);

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["PersonId"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }                    
                }
            }
            return libraryObjects;
        }

        public ILookup<int, LibraryObject> GroupingByPublishingYear()
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllLibraryObjects";

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["Id"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }
                   
                }
            }

            ILookup<int, LibraryObject> resultCollection = libraryObjects.ToLookup(p => p.PublishingYear, p => p);

            return resultCollection;
        }

        public bool RestoreObject(int id)
        {
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RestoreObjectById";
                cmd.Parameters.AddWithValue(@"ObjectId", id);

                connect.Open();
                var rowsCount = cmd.ExecuteNonQuery();
                return rowsCount > 0;
            }
        }
        public IEnumerable<LibraryObject> SortingByYearDirectOrder()
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllLibraryObjects";

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["Id"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }                    
                    
                }
            }

            IQueryable<LibraryObject> resultLibraryObjects = libraryObjects.AsQueryable()
                 .OrderBy(p => p.PublishingYear);

            List<LibraryObject> resultCollection = resultLibraryObjects.ToList();

            return resultCollection;
        }

        public IEnumerable<LibraryObject> SortingByYearReverseOrder()
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllLibraryObjects";

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["Id"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }                    
                }
            }
            IQueryable<LibraryObject> resultLibraryObjects = libraryObjects.AsQueryable()
                 .OrderByDescending(p => p.PublishingYear);

            List<LibraryObject> resultCollection = resultLibraryObjects.ToList();

            return resultCollection;
        }

        public IEnumerable<LibraryObject> SelectLibraryObjectsForPage(int pageNumber, int pageCount)
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectLibraryObjectsForPage";
                cmd.Parameters.AddWithValue(@"PageCount", pageCount);
                cmd.Parameters.AddWithValue(@"PageNum", pageNumber);

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["PersonId"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }              

                    
                }
            }
            return libraryObjects;
        }

        public IEnumerable<LibraryObject> SelectLibraryObjectsForPageSortByYearDirect
        (int pageNumber, int pageCount)
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectLibraryObjectsForPageSortByYearDirect";
                cmd.Parameters.AddWithValue(@"PageCount", pageCount);
                cmd.Parameters.AddWithValue(@"PageNum", pageNumber);

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["PersonId"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }                    
                }
            }
            return libraryObjects;
        }

        public IEnumerable<LibraryObject> SelectLibraryObjectsForPageSortByYearReverse
        (int pageNumber, int pageCount)
        {
            List<LibraryObject> libraryObjects = new List<LibraryObject>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectLibraryObjectsForPageSortByYearReverse";
                cmd.Parameters.AddWithValue(@"PageCount", pageCount);
                cmd.Parameters.AddWithValue(@"PageNum", pageNumber);

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
                        var note = reader["Note"];
                        if (note != DBNull.Value)
                        {
                            newBook.Note = (string)note;
                        }

                        libraryObjects.Add(newBook);
                    }
                    reader.NextResult();

                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["PersonId"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                        int bookId = (int)reader["BookId"];

                        Book book = (Book)libraryObjects.FirstOrDefault(b => b.Id == bookId);
                        if (book != null)
                            book.Authors.Add(newPerson);
                    }                    
                }
            }
            return libraryObjects;
        }
    }
}
