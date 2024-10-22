namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TRANGTHAINV")]
    public partial class TRANGTHAINV
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MANV { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string MAHS { get; set; }

        [StringLength(50)]
        public string TRANGTHAI { get; set; }

        public virtual HOSONV HOSONV { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
