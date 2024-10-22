namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LUONG")]
    public partial class LUONG
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MANV { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BACLUONG { get; set; }

        public decimal? LUONGCB { get; set; }

        public decimal? PHUCAP { get; set; }

        public decimal? TONGTHUONG { get; set; }

        public decimal? TONGUNG { get; set; }

        public decimal? TONGTRU { get; set; }

        public decimal? SOTIEN { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
