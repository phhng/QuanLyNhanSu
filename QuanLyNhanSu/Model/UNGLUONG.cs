namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UNGLUONG")]
    public partial class UNGLUONG
    {
        [StringLength(10)]
        public string MANV { get; set; }

        [Key]
        [StringLength(10)]
        public string MAUL { get; set; }

        public decimal? SOTIEN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYUNG { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
