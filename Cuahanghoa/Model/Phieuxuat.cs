//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cuahanghoa.Model
{
    using Cuahanghoa.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class Phieuxuat: BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Phieuxuat()
        {
            this.Phieuxuatchitiet = new HashSet<Phieuxuatchitiet>();
        }

        private string _Idphieuxuat;
        public string Idphieuxuat { get => _Idphieuxuat; set { _Idphieuxuat = value; OnPropertyChanged(); } }

        public Nullable<System.DateTime> _Ngayxuat;
        public Nullable<System.DateTime> Ngayxuat { get => _Ngayxuat; set { _Ngayxuat = value; OnPropertyChanged(); } }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Phieuxuatchitiet> Phieuxuatchitiet { get; set; }
    }
}