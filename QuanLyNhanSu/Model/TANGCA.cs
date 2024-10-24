namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TANGCA")]
    public partial class TANGCA
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MAPHIEU { get; set; }

        [StringLength(10)]
        public string MANV { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYPHU { get; set; }

        [StringLength(50)]
        public string HINHTHUC { get; set; }

        public decimal? HESO { get; set; }

        public decimal? SOGIO { get; set; }

        public decimal? TIENTANGCA { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }
    }
}
