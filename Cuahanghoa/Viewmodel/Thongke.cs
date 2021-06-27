using Cuahanghoa.Model;
using Cuahanghoa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuahanghoa.Viewmodel
{
    public class Thongke: BaseViewModel
    {
        private Sanpham _Sanpham;
        public Sanpham Sanpham { get => _Sanpham; set { _Sanpham = value; OnPropertyChanged(); } }

        private int _STT;
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged("STT"); } }

        private int _LuongNhap;
        public int LuongNhap { get => _LuongNhap; set { _LuongNhap = value; OnPropertyChanged("LuongNhap"); } }

        private int _LuongXuat;
        public int LuongXuat { get => _LuongXuat; set { _LuongXuat = value; OnPropertyChanged("LuongXuat"); } }

        private int _LuongTon;
        public int LuongTon { get => _LuongTon; set { _LuongTon = value; OnPropertyChanged("LuongTon"); } }

        private double _GiaNhap;
        public double GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

        private double _GiaXuat;
        public double GiaXuat { get => _GiaXuat; set { _GiaXuat = value; OnPropertyChanged(); } }

        private double _GiaTon;
        public double GiaTon { get => _GiaTon; set { _GiaTon = value; OnPropertyChanged(); } }

        private double _GiaLai;
        public double GiaLai { get => _GiaLai; set { _GiaLai = value; OnPropertyChanged(); } }
    }
}
