using Cuahanghoa.Model;
using Cuahanghoa.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Cuahanghoa.Viewmodel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<Tonkho> _TonkhoList;
        public ObservableCollection<Tonkho> TonkhoList { get=> _TonkhoList; set { _TonkhoList = value; OnPropertyChanged("TonkhoList"); } }

        private Thongke _Thongke;
        public Thongke Thongke { get => _Thongke; set { _Thongke = value; OnPropertyChanged(); } }

        private DateTime? _DateBeginInventory;
        public DateTime? DateBeginInventory { get => _DateBeginInventory; set { _DateBeginInventory = value; OnPropertyChanged(); } }

        private DateTime? _DateEndInventory;
        public DateTime? DateEndInventory { get => _DateEndInventory; set { _DateEndInventory = value; OnPropertyChanged(); } }

        private DateTime? _DateBegin;
        public DateTime? DateBegin { get => _DateBegin; set { _DateBegin = value; OnPropertyChanged(); } }

        private DateTime? _DateEnd;
        public DateTime? DateEnd { get => _DateEnd; set { _DateEnd = value; OnPropertyChanged(); } }

        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand QuanlynhapkhoCommand { get; set; }
        public ICommand QuanlyxuatkhoCommand { get; set; }
        public ICommand QuanlysanphamCommand { get; set; }
        public ICommand QuanlynhanvienCommand { get; set; }
        public ICommand QuanlykhachhangCommand { get; set; }
        public ICommand QuanlynhacungcapCommand { get; set; }
        public ICommand UserWindowCommand { get; set; }
        public ICommand Inventory { get; set; }
        public ICommand InventoryCommand { get; set; }
        public ICommand ThongkeCommand { get; set; }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return  true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                loginWindow login = new loginWindow();
                login.ShowDialog();

                if (login.DataContext== null)
                {
                    return;
                }

                var loginVM = login.DataContext as LoginViewModel;
                if(loginVM.Islogin)
                {
                    p.Show();
                    LoadTonkhoData();
                }
                else
                {
                    p.Close();
                }
                
            });
            QuanlynhapkhoCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Quanlynhapkho quanlynhapkho = new Quanlynhapkho();
                quanlynhapkho.ShowDialog();
            });
            QuanlyxuatkhoCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Quanlyxuatkho quanlyxuatkho = new Quanlyxuatkho();
                quanlyxuatkho.ShowDialog();
            });
            QuanlysanphamCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Quanlysanpham quanlysanpham = new Quanlysanpham();
                quanlysanpham.ShowDialog();
            });
            QuanlynhanvienCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Quanlynhanvien quanlynhanvien = new Quanlynhanvien();
                quanlynhanvien.ShowDialog();

            });
            QuanlykhachhangCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Quanlykhachhang quanlykhachhang = new Quanlykhachhang();
                quanlykhachhang.ShowDialog();
            });
            QuanlynhacungcapCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                Quanlynhacungcap quanlynhacungcap = new Quanlynhacungcap();
                quanlynhacungcap.ShowDialog();
            });
            UserWindowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                UserWindow userWindow = new UserWindow();
                userWindow.ShowDialog();
            });

            InventoryCommand = new RelayCommand<object>((x) =>
             {
                 if (DateBeginInventory == null || DateEndInventory == null)
                     return false;
                 return true;

             }, (x) =>

             {
                 TonkhoList.Clear();
                 LoadTonkhoData2();
                 ICollectionView view = CollectionViewSource.GetDefaultView(TonkhoList);
                 view.Refresh();

             });

            ThongkeCommand = new RelayCommand<object>((x) =>
            {
                
                return true;

            }, (x) =>

            {
                TonkhoList.Clear();
                LoadTonkhoData();

            });

        }
        
        void LoadTonkhoData()
        {
            TonkhoList = new ObservableCollection<Tonkho>();
            Thongke = new Thongke();
            var objectList = DataProvider.Ins.DB.Sanpham;

            int luongNhap = 0;
            int luongXuat = 0;
            double tongTienNhap = 0;
            double tongTienXuat = 0;
            double tongTienTon = 0;
            double tongTienLai = 0;

            int i = 1;
            foreach (var item in objectList)
            {
                var inputList = DataProvider.Ins.DB.Phieunhapchitiet.Where(p => p.Idsanpham == item.Idsanpham);
                var outputList = DataProvider.Ins.DB.Phieuxuatchitiet.Where(p => p.Idsanpham == item.Idsanpham);

                int tongNhap = 0;
                int tongXuat = 0;
                double tienNhap = 0;
                double tienXuat = 0;

                if (inputList != null && inputList.Count() > 0)
                {
                    tongNhap = (int)inputList.Sum(p => p.Soluong);
                    tienNhap = (double)inputList.Sum(p => p.Gianhap);
                    tienXuat = (double)inputList.Sum(p => p.Giaxuat);
                    luongNhap += tongNhap;
                }

                if (outputList != null && outputList.Count() > 0)
                {
                    tongXuat = (int)outputList.Sum(p => p.Soluong);
                    luongXuat += tongXuat;
                }

                tongTienTon += (tongNhap - tongXuat) * tienNhap;
                tongTienLai += tongXuat * (tienXuat - tienNhap);
                tongTienNhap += tongNhap * tienNhap;
                tongTienXuat += tongXuat * tienXuat;

                Tonkho tonkho = new Tonkho();
                tonkho.STT = i;
                tonkho.Luongnhap = tongNhap;
                tonkho.Luongxuat = tongXuat;
                tonkho.Luongton = tongNhap - tongXuat;
                tonkho.Tiennhap = tongNhap * tienNhap;
                tonkho.Tienban = tongXuat * tienXuat;
                tonkho.Tienton = (tongNhap - tongXuat) * tienNhap;
                tonkho.Tienlai = tongXuat * (tienXuat - tienNhap);
                tonkho.sanpham = item;

                TonkhoList.Add(tonkho);

                i++;
            }

            Thongke.LuongNhap = luongNhap;
            Thongke.LuongXuat = luongXuat;
            Thongke.GiaNhap = tongTienNhap;
            Thongke.GiaXuat = tongTienXuat;
            Thongke.LuongTon = luongNhap - luongXuat;
            Thongke.GiaTon = tongTienTon;
            Thongke.GiaLai = tongTienLai;

        }

        void LoadTonkhoData2()
        {
            TonkhoList = new ObservableCollection<Tonkho>();
            Thongke = new Thongke();
            var objectList = DataProvider.Ins.DB.Sanpham;

            // Tạo danh sách trong ngày duyệt - mảng Input & mảng Ouput
            var dsNhap = DataProvider.Ins.DB.Phieunhap.Where(p => (p.Ngaynhap >= DateBeginInventory) && (p.Ngaynhap <= DateEndInventory));
            var dsXuat = DataProvider.Ins.DB.Phieuxuat.Where(p => (p.Ngayxuat >= DateBeginInventory) && (p.Ngayxuat <= DateEndInventory));

            int luongNhap = 0;
            int luongXuat = 0;

            double tongTienNhap = 0;
            double tongTienXuat = 0;
            double tongTienTon = 0;
            double tongTienLai = 0;

            // Duyệt mảng Object
            int i = 1;
            foreach (var item in objectList)
            {
                int tongNhap = 0;
                int tongXuat = 0;

                double tienNhap = 0;
                double tienXuat = 0;
                


                // Tìm mảng InputInfo nằm trong mảng Object và mảng Input (dsNhap)
                foreach (var item1 in dsNhap)
                {
                    var inputList = DataProvider.Ins.DB.Phieunhapchitiet.Where(p => (p.Idphieunhap == item1.Idphieunhap) && (p.Idsanpham == item.Idsanpham));
                    if (inputList != null && inputList.Count() > 0)
                    {
                        tongNhap = (int)inputList.Sum(p => p.Soluong);
                        tienNhap = (double)inputList.Sum(p => p.Gianhap);
                        
                        luongNhap += tongNhap;
                        tongTienNhap += tongNhap * tienNhap;

                       /* Tonkho tonkho = new Tonkho();
                        tonkho.STT = i;
                        tonkho.Luongnhap = tongNhap;
                        tonkho.Luongxuat = tongXuat;
                        tongTienTon += (tongNhap - tongXuat) * tienNhap;
                        tongTienLai += tongXuat * (tienXuat - tienNhap);

                        tonkho.Luongton = tongNhap - tongXuat;
                        tonkho.Tiennhap = tongNhap * tienNhap;
                        tonkho.Tienban = tongXuat * tienXuat;
                        tonkho.Tienton = (tongNhap - tongXuat) * tienNhap;
                        tonkho.Tienlai = tongXuat * (tienXuat - tienNhap);
                        tonkho.sanpham = item;

                        TonkhoList.Add(tonkho);

                        i++;*/
                    }
                    

                    /*Thongke.LuongNhap = luongNhap;
                    Thongke.LuongXuat = luongXuat;
                    Thongke.GiaNhap = tongTienNhap;
                    Thongke.GiaXuat = tongTienXuat;
                    Thongke.LuongTon = luongNhap - luongXuat;
                    Thongke.GiaTon = tongTienTon;
                    Thongke.GiaLai = tongTienLai;*/
                }

                // Tìm mảng OutputInfo nằm trong mảng Object và mảng Output (dsXuat)
                foreach (var item2 in dsXuat)
                {
                    var inputList1 = DataProvider.Ins.DB.Phieunhapchitiet.Where(p => p.Idsanpham == item.Idsanpham);
                    tienXuat = (double)inputList1.Sum(p => p.Giaxuat);
                    var outputList = DataProvider.Ins.DB.Phieuxuatchitiet.Where(p => p.Idphieuxuat == item2.Idphieuxuat && (p.Idsanpham == item.Idsanpham));
                    if (outputList != null && outputList.Count() > 0)
                    {

                        tongXuat = (int)outputList.Sum(p => p.Soluong);
                        //tienXuat = (double)outputList.Sum(p => p.Tongtien);

                        Tonkho tonkho = new Tonkho();
                        tonkho.STT = i;
                        tonkho.Luongnhap = tongNhap;
                        tonkho.Luongxuat = tongXuat;
                        tongTienTon += (tongNhap - tongXuat) * tienNhap;
                        tongTienLai += tongXuat * (tienXuat - tienNhap);

                        tonkho.Luongton = tongNhap - tongXuat;
                        tonkho.Tiennhap = tongNhap * tienNhap;
                        tonkho.Tienban = tongXuat * tienXuat;
                        tonkho.Tienton = (tongNhap - tongXuat) * tienNhap;
                        tonkho.Tienlai = tongXuat * (tienXuat - tienNhap);
                        tonkho.sanpham = item;

                        TonkhoList.Add(tonkho);

                        i++;
                    }

                    Thongke.LuongNhap = luongNhap;
                    Thongke.LuongXuat = luongXuat;
                    Thongke.GiaNhap = tongTienNhap;
                    Thongke.GiaXuat = tongTienXuat;
                    Thongke.LuongTon = luongNhap - luongXuat;
                    Thongke.GiaTon = tongTienTon;
                    Thongke.GiaLai = tongTienLai;
                }

            }

        }

    }
}
