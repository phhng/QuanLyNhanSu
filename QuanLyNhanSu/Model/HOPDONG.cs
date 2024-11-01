namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOPDONG")]
    public partial class HOPDONG
    {
        [Key]
        [StringLength(10)]
        public string MAHD { get; set; }

        [StringLength(10)]
        public string MANV { get; set; }

        [StringLength(100)]
        public string LOAIHD { get; set; }

        [Column(TypeName = "date")]
        public DateTime? TUNGAY { get; set; }

        public decimal? LUONGCB { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DENNGAY { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
