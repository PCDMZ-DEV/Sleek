﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sleek.Models {
    public partial class MainContext : DbContext {

        // Services
        public static IConfiguration Configuration;

        public MainContext(DbContextOptions<MainContext> options, IConfiguration configuration) : base(options) {
            Configuration = configuration;
        }

        public virtual DbSet<Activity> Activity { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MainContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Activity>(entity => {
                entity.HasKey(e => e.ActId);

                entity.Property(e => e.ActId).HasColumnName("act_id");

                entity.Property(e => e.ActDate)
                    .HasColumnName("act_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ActCusid).HasColumnName("act_cusid");

                entity.Property(e => e.ActUsrid).HasColumnName("act_usrid");

                entity.Property(e => e.ActDescription)
                    .IsRequired()
                    .HasColumnName("act_description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ActType)
                    .IsRequired()
                    .HasColumnName("act_type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Customer>(entity => {
                entity.HasKey(e => e.CusId);

                entity.Property(e => e.CusId).HasColumnName("cus_id");

                entity.Property(e => e.CusAddress1)
                    .HasColumnName("cus_address1")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CusAddress2)
                    .HasColumnName("cus_address2")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CusCity)
                    .HasColumnName("cus_city")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CusCompany)
                    .HasColumnName("cus_company")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CusEmail)
                    .HasColumnName("cus_email")
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.CusFax)
                    .HasColumnName("cus_fax")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CusFirst)
                    .HasColumnName("cus_first")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CusLast)
                    .HasColumnName("cus_last")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CusNote)
                    .HasColumnName("cus_note")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CusNumber)
                    .HasColumnName("cus_number")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CusPassword)
                    .HasColumnName("cus_password")
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.CusPhone)
                    .HasColumnName("cus_phone")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CusStaid).HasColumnName("cus_staid");

                entity.Property(e => e.CusState)
                    .HasColumnName("cus_state")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CusZip)
                    .HasColumnName("cus_zip")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CusZip4)
                    .HasColumnName("cus_zip4")
                    .HasMaxLength(4)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity => {
                entity.HasKey(e => e.OrdId);

                entity.Property(e => e.OrdId).HasColumnName("ord_id");

                entity.Property(e => e.OrdComments)
                    .HasColumnName("ord_comments")
                    .IsUnicode(false);

                entity.Property(e => e.OrdDate)
                    .HasColumnName("ord_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.OrdProid).HasColumnName("ord_proid");

                entity.Property(e => e.OrdStaid).HasColumnName("ord_staid");

                entity.Property(e => e.OrdSubject)
                    .HasColumnName("ord_subject")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrdDescription)
                    .HasColumnName("ord_description")
                    .HasMaxLength(300)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Project>(entity => {
                entity.HasKey(e => e.ProId);

                entity.Property(e => e.ProId).HasColumnName("pro_id");

                entity.Property(e => e.ProCusid).HasColumnName("pro_cusid");

                entity.Property(e => e.ProDate)
                    .HasColumnName("pro_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProDescription)
                    .HasColumnName("pro_description")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ProLocalpath)
                    .HasColumnName("pro_localpath")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ProRemotepath)
                    .HasColumnName("pro_remotepath")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ProSourcepath)
                    .HasColumnName("pro_sourcepath")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ProStaid).HasColumnName("pro_staid");

                entity.Property(e => e.ProUsrid).HasColumnName("pro_usrid");
            });

            modelBuilder.Entity<Status>(entity => {
                entity.HasKey(e => e.StaId);

                entity.Property(e => e.StaId).HasColumnName("sta_id");

                entity.Property(e => e.StaCode)
                    .IsRequired()
                    .HasColumnName("sta_code")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StaDescription)
                    .IsRequired()
                    .HasColumnName("sta_description")
                    .HasMaxLength(100)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<Request>(entity => {
                entity.HasKey(e => e.ReqId)
                    .HasName("PK_Requests");

                entity.Property(e => e.ReqId).HasColumnName("req_id");

                entity.Property(e => e.ReqAddress)
                    .HasColumnName("req_address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReqCity)
                    .HasColumnName("req_city")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReqContent)
                    .HasColumnName("req_content")
                    .IsUnicode(false);

                entity.Property(e => e.ReqDate)
                    .HasColumnName("req_date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ReqEmail)
                    .HasColumnName("req_email")
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.ReqFirst)
                    .HasColumnName("req_first")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReqLast)
                    .HasColumnName("req_last")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ReqPhone)
                    .HasColumnName("req_phone")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ReqState)
                    .HasColumnName("req_state")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ReqSubject)
                    .HasColumnName("req_subject")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.ReqType)
                    .HasColumnName("req_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReqZip)
                    .HasColumnName("req_zip")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity => {
                entity.HasKey(e => e.UsrId);

                entity.Property(e => e.UsrId).HasColumnName("usr_id");

                entity.Property(e => e.UsrCusid).HasColumnName("usr_cusid");

                entity.Property(e => e.UsrEmail)
                    .HasColumnName("usr_email")
                    .HasMaxLength(160)
                    .IsUnicode(false);

                entity.Property(e => e.UsrFirst)
                    .HasColumnName("usr_first")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UsrLast)
                    .HasColumnName("usr_last")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UsrName)
                    .HasColumnName("usr_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UsrNote)
                    .HasColumnName("usr_note")
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.UsrPassword)
                    .HasColumnName("usr_password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UsrRole)
                    .HasColumnName("usr_role")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UsrStaid).HasColumnName("usr_staid");

                entity.Property(e => e.UsrTitle)
                    .HasColumnName("usr_title")
                    .HasMaxLength(30);

                entity.Property(e => e.UsrToken)
                    .HasColumnName("usr_token")
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.UsrTokendate)
                    .HasColumnName("usr_tokendate")
                    .HasColumnType("datetime");

            });

            modelBuilder.Entity<Status>().HasData(
                new Status { StaId = 10000, StaCode = "Active", StaDescription = "Active" },
                new Status { StaId = 10001, StaCode = "Inactive", StaDescription = "Inactive" });

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CusId = 10000, CusNumber = "A1013100", CusCompany = "Company One", CusAddress1 = "123 Main Street", CusAddress2 = "Suite 100", CusCity = "Anytown", CusState = "CA", CusZip = "12345", CusZip4 = "1234", CusFirst = "John", CusLast = "Doe", CusPhone = "(800) 555-1212", CusFax = "(800) 555-1212", CusEmail = "customer@companyone.com", CusNote = "Seed Data", CusStaid = 10000 },
                new Customer { CusId = 10001, CusNumber = "A2013100", CusCompany = "Company Two", CusAddress1 = "231 Main Street", CusAddress2 = "Suite 200", CusCity = "Anytown", CusState = "CA", CusZip = "12345", CusZip4 = "1234", CusFirst = "Mary", CusLast = "Doe", CusPhone = "(800) 555-1212", CusFax = "(800) 555-1212", CusEmail = "customer@companytwo.com", CusNote = "Seed Data", CusStaid = 10000 },
                new Customer { CusId = 10002, CusNumber = "A3013100", CusCompany = "Company Three", CusAddress1 = "331 Main Street", CusAddress2 = "Suite 300", CusCity = "Anytown", CusState = "CA", CusZip = "12345", CusZip4 = "1234", CusFirst = "Davis", CusLast = "Doe", CusPhone = "(800) 555-1212", CusFax = "(800) 555-1212", CusEmail = "customer@companythree.com", CusNote = "Seed Data", CusStaid = 10000 });

            modelBuilder.Entity<User>().HasData(
                new User { UsrId = 10000, UsrCusid = 10000, UsrFirst = "Admin", UsrLast = "Account", UsrTitle = "Administrator", UsrName = "admin", UsrEmail = "admin@companyone.com", UsrPassword = "Testing123", UsrRole = "Admin", UsrNote = "Default Administrator Account", UsrStaid = 10000 },
                new User { UsrId = 10001, UsrCusid = 10000, UsrFirst = "Manager", UsrLast = "Account", UsrTitle = "Manager", UsrName = "manager", UsrEmail = "manager@companyone.com", UsrPassword = "Testing123", UsrRole = "Manager", UsrNote = "Default Managment Account", UsrStaid = 10000 },
                new User { UsrId = 10002, UsrCusid = 10000, UsrFirst = "User", UsrLast = "Account", UsrTitle = "Associate", UsrName = "user", UsrEmail = "user@companyone.com", UsrPassword = "Testing123", UsrRole = "User", UsrNote = "Default User Account", UsrStaid = 10000 });

        }

    }

}