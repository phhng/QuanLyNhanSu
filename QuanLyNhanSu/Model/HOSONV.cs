namespace QuanLyNhanSu.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOSONV")]
    public partial class HOSONV
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOSONV()
        {
            NHANVIENs = new HashSet<NHANVIEN>();
            TRANGTHAINVs = new HashSet<TRANGTHAINV>();
        }

        [Key]
        [StringLength(10)]
        public string MAHS { get; set; }

        public int? MABC { get; set; }

        [StringLength(100)]
        public string HOTEN { get; set; }

        [Column(TypeName = "date")]
        public DateTime? NGAYSINH { get; set; }

        [StringLength(10)]
        public string GIOITINH { get; set; }

        [StringLength(20)]
        public string CCCD_CMND { get; set; }

        [StringLength(255)]
        public string AVATAR { get; set; }

        [StringLength(255)]
        public string DIACHI { get; set; }

        [StringLength(255)]
        public string QUEQUAN { get; set; }

        public virtual BANGCAP BANGCAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANVIEN> NHANVIENs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRANGTHAINV> TRANGTHAINVs { get; set; }
    }
}
