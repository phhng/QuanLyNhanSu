namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHUCAP")]
    public partial class PHUCAP
    {
        [Key]
        [StringLength(10)]
        public string MAPC { get; set; }

        [StringLength(10)]
        public string MANV { get; set; }

        [StringLength(100)]
        public string CHEDOPC { get; set; }

        public decimal? SOTIEN { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
