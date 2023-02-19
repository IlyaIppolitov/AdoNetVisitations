using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdoNetVisitations
{
    internal class StudentsVisitationService
    {   
        public Visitation[] GetVisitations()
        {
            var connectionString = "Data Source=myapp.db";
            var sql = "select * from Visitations";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            var result = new List<Visitation>();
            foreach (IDataRecord row in reader)
            {
                var visitation = new Visitation(
                    row.GetInt64(row.GetOrdinal("Id")),
                    row.GetString(row.GetOrdinal("Name")),
                    row.GetString(row.GetOrdinal("Surname")),
                    DateOnly.FromDateTime(DateTime.Parse(row.GetString(row.GetOrdinal("Date"))))
                );
                result.Add(visitation);
            }
            return result.ToArray();
        }
        public Student[] GetStudents()
        {
            var connectionString = "Data Source=myapp.db";
            var sql = "select * from Students";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            var result = new List<Student>();
            foreach (IDataRecord row in reader)
            {
                var student = new Student(
                    row.GetInt64(row.GetOrdinal("Id")),
                    row.GetString(row.GetOrdinal("Name")),
                    row.GetString(row.GetOrdinal("Surname"))
                );
                result.Add(student);
            }
            return result.ToArray();
        }

        public bool CheckStudentByName(string name, string surname)
        {
            var connectionString = "Data Source=myapp.db";
            var sql = $"select * from Students where Name like '{name}' and Surname like '{surname}'";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            using var reader = command.ExecuteReader();
            var result = new List<Student>();
            foreach (IDataRecord row in reader)
            {
                var student = new Student(
                    row.GetInt64(row.GetOrdinal("Id")),
                    row.GetString(row.GetOrdinal("Name")),
                    row.GetString(row.GetOrdinal("Surname"))
                );
                result.Add(student);
            }
            
            return result.Count > 0;
        }

        // создание таблицы с учётом проверки её сущестования
        public void CreateTableVisitations()
        {
            var connectionString = "Data Source = myapp.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"create table if not exists Visitations " +
                $"(id integer not null primary key, " +
                $"Name text not null," +
                $"Surname text not null," +
                $"Date date not null )";
            command.ExecuteNonQuery();
        }
        public void CreateTableStudents()
        {
            var connectionString = "Data Source = myapp.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"create table if not exists Students " +
                $"(id integer not null primary key, " +
                $"Name text not null," +
                $"Surname text not null )";
            command.ExecuteNonQuery();
        }

        public void AddVisitation(string name, string surname, DateOnly date)
        {
            var connectionString = "Data Source = myapp.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"insert into Visitations (Name, Surname, Date) values" +
                $"('{name}','{surname}','{date}')";
            command.ExecuteNonQuery();
        }

        public void AddStudent(string name, string surname)
        {
            var connectionString = "Data Source = myapp.db";
            using var connection = new SqliteConnection(connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"insert into Students (Name,Surname) values" +
                $"('{name}','{surname}')";
            command.ExecuteNonQuery();
        }

        public void FillTestVisitations()
        {
            for (int i = 0 ; i < 10; i++)
            {
                AddVisitation("Name" + i+1, "Surname" + i+1, new DateOnly(2022, 1, i+1));
            }
        }

        public void FillTestStudents()
        {
            for (int i = 0; i < 10; i++)
            {
                AddStudent("Name" + i+1, "Surname" + i+1);
            }
        }
    }

    internal class Visitation
    {
        public Visitation(long id, string name, string surname, DateOnly dateOnly) {
            Id = id;
            Name = name;
            Surname = surname;
            Date = dateOnly;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly Date { get; set; }

        public override string ToString()
        {
            return $"{Id.ToString()} - {Name} - {Surname} - {Date.ToString()}";
        }
    }

    internal class Student
    {
        public Student(long id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{Id.ToString()} - {Name} - {Surname}";
        }
    }
}
