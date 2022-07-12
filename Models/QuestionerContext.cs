using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QUIZ.Models;

namespace QUIZ.Models
{
    public class QuestionerContext : DbContext
    {
        public QuestionerContext(DbContextOptions<QuestionerContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual  DbSet<QuestionTable> QuestionTables { get; set; }
        public virtual DbSet<AnswerTable> AnswerTables { get; set; }
        public virtual DbSet<TopicTable> TopicTables { get; set; }
        public virtual DbSet<ScoreRecorder> ScoreRecorders { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("UserCredential");
            modelBuilder.Entity<QuestionTable>().ToTable("QuestionTable");
            modelBuilder.Entity<AnswerTable>().ToTable("AnswerTable");
            modelBuilder.Entity<TopicTable>().ToTable("TopicTable");
            modelBuilder.Entity<ScoreRecorder>().ToTable("ScoreTable");
            modelBuilder.Entity<AnswerTable>(x => x.HasOne(d => d.QuestionTableRefernce).WithMany(p => p.answerTable).
            HasForeignKey(e=>e.QuestionId).HasConstraintName("FK_Answer_Question").OnDelete(DeleteBehavior.Cascade));
            modelBuilder.Entity<QuestionTable>(x => x.HasOne(d => d.topicTable).WithMany(p => p.questionTable).
            HasForeignKey(e => e.TopicId).HasConstraintName("Topic Foreign Key").OnDelete(DeleteBehavior.Cascade));
            modelBuilder.Entity<ScoreRecorder>(x => x.HasOne(c => c.user).WithMany(c=>c.ScoreRecorders).HasForeignKey(b=>b.UserId).
            HasConstraintName("User Foreign Key").OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<ScoreRecorder>(x => x.HasOne(c => c.topicTable).WithMany(c => c.ScoreRecorders).HasForeignKey(b => b.TopicId)
            .HasConstraintName("Topic score Foreign Key").OnDelete(DeleteBehavior.Cascade));
        }
        public DbSet<QUIZ.Models.LoginModel> LoginModel { get; set; }
    }
}
