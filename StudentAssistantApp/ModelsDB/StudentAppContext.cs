using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;
using StudentAssistantApp.ModelsDB;

namespace StudentAssistantApp.Models
{
    public class StudentAppContext : DbContext
    {
        public DbSet<DBTask> DBTasks { get; set; }
        public DbSet<DBMark> DBMarks { get; set; }
        public DbSet<DBUser> DBUsers { get; set; }
        public DbSet<DBNote> DBNotes { get; set; }
        public DbSet<DBEvent> DBEvents { get; set; }
        public DbSet<DBSubject> DBSubjects { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Initial Catalog=StudentAssistentDB;Integrated Security=True");
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-J99J4U4\SQLEXPRESS;Initial Catalog=StudentAssistentDB;Integrated Security=True");
        }
    }
}
