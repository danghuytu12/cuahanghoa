using Cuahanghoa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuahanghoa.Model
{
    public class Tonkho: BaseViewModel
    {
        private Sanpham _sanpham;
        public Sanpham sanpham { get=> _sanpham; set { _sanpham = value;OnPropertyChanged(); } }

        private int _STT;
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged(); } }

        private int _Luongnhap;
        public int Luongnhap { get => _Luongnhap; set { _Luongnhap = value; OnPropertyChanged(); } }

        private int _Luongxuat;
        public int Luongxuat { get => _Luongxuat; set { _Luongxuat = value; OnPropertyChanged(); } }

        private int _Luongton;
        public int Luongton { get => _Luongton; set { _Luongton = value; OnPropertyChanged(); } }

        private double _Tienton;
        public double Tienton { get => _Tienton; set { _Tienton = value; OnPropertyChanged(); } }

        private double _Tienban;
        public double Tienban { get => _Tienban; set { _Tienban = value; OnPropertyChanged(); } }

        private double _Tiennhap;
        public double Tiennhap { get => _Tiennhap; set { _Tiennhap = value; OnPropertyChanged(); } }

        private double _Tienlai;
        public double Tienlai { get => _Tienlai; set { _Tienlai = value; OnPropertyChanged(); } }

    }
}
