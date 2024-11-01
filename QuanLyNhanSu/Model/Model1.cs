using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace QuanLyNhanSu.Model
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model11")
        {
        }

        public virtual DbSet<BANGCAP> BANGCAPs { get; set; }
        public virtual DbSet<BAOHIEM> BAOHIEMs { get; set; }
        public virtual DbSet<CHAMCONG> CHAMCONGs { get; set; }
        public virtual DbSet<CHUCVU> CHUCVUs { get; set; }
        public virtual DbSet<HOPDONG> HOPDONGs { get; set; }
        public virtual DbSet<HOSONV> HOSONVs { get; set; }
        public virtual DbSet<LUONG> LUONGs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<PHONGBAN> PHONGBANs { get; set; }
        public virtual DbSet<PHUCAP> PHUCAPs { get; set; }
        public virtual DbSet<QUYEN> QUYENs { get; set; }
        public virtual DbSet<TANGCA> TANGCAs { get; set; }
        public virtual DbSet<TRANGTHAINV> TRANGTHAINVs { get; set; }
        public virtual DbSet<UNGLUONG> UNGLUONGs { get; set; }
        public virtual DbSet<USER> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BAOHIEM>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BAOHIEM>()
                .Property(e => e.MABAOHIEM)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CHAMCONG>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HOPDONG>()
                .Property(e => e.MAHD)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HOPDONG>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HOSONV>()
                .Property(e => e.MAHS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HOSONV>()
                .Property(e => e.CCCD_CMND)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HOSONV>()
                .HasMany(e => e.TRANGTHAINVs)
                .WithRequired(e => e.HOSONV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LUONG>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.MAHS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .HasMany(e => e.LUONGs)
                .WithRequired(e => e.NHANVIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHANVIEN>()
                .HasMany(e => e.TRANGTHAINVs)
                .WithRequired(e => e.NHANVIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHUCAP>()
                .Property(e => e.MAPC)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PHUCAP>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<QUYEN>()
                .HasMany(e => e.USERS)
                .WithMany(e => e.QUYENs)
                .Map(m => m.ToTable("USER_ROLES").MapLeftKey("ROLE_ID").MapRightKey("USER_ID"));

            modelBuilder.Entity<TANGCA>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TANGCA>()
                .Property(e => e.HESO)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TANGCA>()
                .Property(e => e.SOGIO)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TANGCA>()
                .Property(e => e.TIENTANGCA)
                .HasPrecision(5, 2);

            modelBuilder.Entity<TRANGTHAINV>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TRANGTHAINV>()
                .Property(e => e.MAHS)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UNGLUONG>()
                .Property(e => e.MANV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<UNGLUONG>()
                .Property(e => e.MAUL)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<USER>()
                .Property(e => e.SDT)
                .IsFixedLength()
                .IsUnicode(false);
        }
    }
}
