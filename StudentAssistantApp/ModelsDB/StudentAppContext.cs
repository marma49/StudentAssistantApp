using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentAssistantApp.Models
{
    public class StudentAppContext : DbContext
    {
        public DbSet<DBTask> DBTasks { get; set; }
        public DbSet<DBMark> DBMarks { get; set; }
        public DbSet<DBUser> DBUsers { get; set; }
        public DbSet<DBNote> DBNotes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-0F2A56E\SQLEXPRESS;Initial Catalog=StudentAssistentDB;Integrated Security=True");
        }
    }
}
