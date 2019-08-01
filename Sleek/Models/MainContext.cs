using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Sleek.Models {
    public partial class MainContext : DbContext {

        // Services
        public static IConfiguration Configuration;

        public MainContext(DbContextOptions<MainContext> options, IConfiguration configuration) : base(options) {
            Configuration = configuration;
        }

        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("MainContext"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

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

                entity.Property(e => e.StaTimestamp)
                    .HasColumnName("sta_timestamp")
                    .IsRowVersion();
            });

            modelBuilder.Entity<User>(entity => {
                entity.HasKey(e => e.UsrId);

                entity.Property(e => e.UsrId).HasColumnName("usr_id");

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

                entity.Property(e => e.UsrTimestamp)
                    .HasColumnName("usr_timestamp")
                    .IsRowVersion();

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

                entity.Property(e => e.ReqTimestamp)
                    .HasColumnName("req_timestamp")
                    .IsRowVersion();

                entity.Property(e => e.ReqType)
                    .HasColumnName("req_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReqZip)
                    .HasColumnName("req_zip")
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

        }

    }

}
