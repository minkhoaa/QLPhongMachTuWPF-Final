//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLPhongMachTuWPF.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class LICHHEN
    {
        public int MaLH { get; set; }
        public int MaNV { get; set; }
        public int MaBN { get; set; }
        public Nullable<System.DateTime> NgayHen { get; set; }
        public Nullable<System.TimeSpan> GioHen { get; set; }
        public Nullable<int> MaPK { get; set; }
    
        public virtual BENHNHAN BENHNHAN { get; set; }
        public virtual NHANVIEN NHANVIEN { get; set; }
        public virtual PHIEUKHAM PHIEUKHAM { get; set; }


    }
}
