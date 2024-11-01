namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BAOHIEM")]
    public partial class BAOHIEM
    {
        [StringLength(10)]
        public string MANV { get; set; }

        [Key]
        [StringLength(10)]
        public string MABAOHIEM { get; set; }

        [StringLength(50)]
        public string LOAIBAOHIEM { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYLAP { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYHETHAN { get; set; }

        public decimal? SOTIEN { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
