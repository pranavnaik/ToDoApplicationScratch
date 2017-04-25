using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ToDoApplication.Models;

namespace ToDoApplication.DAL
{
    public class DbAccessLayer : DbContext
    {
        //mapping to correct database tables
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Task>().ToTable("tblTasks");
            modelBuilder.Entity<User>().ToTable("tblUsers");
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}