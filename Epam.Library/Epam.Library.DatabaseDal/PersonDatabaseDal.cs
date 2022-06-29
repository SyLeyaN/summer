using Epam.Library.DalContracts;
using Epam.Library.Entities;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Epam.Library.DatabaseDal
{
    public class PersonDatabaseDal : IPersonDal
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["Library_DataBase_admin"].ConnectionString;

        public int Add(Person person)
        {
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertPerson";
                cmd.Parameters.AddWithValue(@"Name", person.Name);
                cmd.Parameters.AddWithValue(@"Surname", person.Surname);

                var id = new SqlParameter
                {
                    DbType = DbType.Int32,
                    ParameterName = "Id",
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(id);

                connect.Open();
                cmd.ExecuteNonQuery();
                person.Id = (int)id.Value;
            }
            return person.Id;
        }

        public bool Delete(int id)
        {
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeletePerson";
                cmd.Parameters.AddWithValue(@"PersonId", id);

                connect.Open();
                var rowsCount = cmd.ExecuteNonQuery();
                return rowsCount > 0;
            }
        }

        public IEnumerable<Person> GetAll()
        {
            List<Person> persons = new List<Person>();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectAllPersons";

                connect.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person newPerson = new Person();

                        newPerson.Id = (int)reader["Id"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];

                        persons.Add(newPerson);
                    }
                }
            }
            return persons;
        }

        public Person GetById(int id)
        {
            Person newPerson = new Person();
            using (var connect = new SqlConnection(_connectionString))
            {
                var cmd = connect.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SelectByIdPersons";
                cmd.Parameters.AddWithValue(@"PersonId", id);

                connect.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newPerson.Id = (int)reader["Id"];
                        newPerson.Name = (string)reader["Name"];
                        newPerson.Surname = (string)reader["Surname"];
                    }
                }
            }
            return newPerson;
        }
    }
}
