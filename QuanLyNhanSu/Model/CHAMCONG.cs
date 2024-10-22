namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHAMCONG")]
    public partial class CHAMCONG
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAPHIEU { get; set; }

        [StringLength(10)]
        public string MANV { get; set; }

        public int? THANG { get; set; }

        public int? NAM { get; set; }

        public int? TONGSONGAY { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
