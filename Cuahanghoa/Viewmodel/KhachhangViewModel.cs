using Cuahanghoa.Model;
using Cuahanghoa.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Cuahanghoa.Viewmodel
{
    public class KhachhangViewModel: BaseViewModel
    {
        private ObservableCollection<Khachhang> _List;
        public ObservableCollection<Khachhang> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Phieuxuatchitiet> _Listphieuxuatchitiet;
        public ObservableCollection<Model.Phieuxuatchitiet> Listphieuxuatchitiet { get => _Listphieuxuatchitiet; set { _Listphieuxuatchitiet = value;OnPropertyChanged(); } }

        

        private int _ID;
        public int Id { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }

        private DateTime? _Ngaysinh;
        public DateTime? Ngaysinh { get=> _Ngaysinh; set { _Ngaysinh = value;OnPropertyChanged(); } }

        private string _Diachi;
        public string Diachi { get => _Diachi; set { _Diachi = value; OnPropertyChanged(); } }

        private string _Dienthoai;
        public string Dienthoai { get => _Dienthoai; set { _Dienthoai = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private Khachhang _SelectedItem;
        public Khachhang SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id = SelectedItem.Idkhachhang;
                    Ngaysinh = SelectedItem.Ngaysinh;
                    DisplayName = SelectedItem.Tenkhachhang;
                    Diachi = SelectedItem.Diachi;
                    Dienthoai = SelectedItem.Dienthoai;
                    Email = SelectedItem.Email;

                }
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public KhachhangViewModel()
        {
            List = new ObservableCollection<Khachhang>(DataProvider.Ins.DB.Khachhang);
            Listphieuxuatchitiet = new ObservableCollection<Model.Phieuxuatchitiet>(DataProvider.Ins.DB.Phieuxuatchitiet);

            AddCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                var Khachhang = new Khachhang() { Tenkhachhang = DisplayName, Diachi = Diachi, Dienthoai = Dienthoai, Email = Email, Ngaysinh=Ngaysinh };
                DataProvider.Ins.DB.Khachhang.Add(Khachhang);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Khachhang);
                
            });
            EditCommand = new RelayCommand<object>((p) => {
                if (SelectedItem == null)
                    return false;

                var dsKhachhang = DataProvider.Ins.DB.Khachhang.Where(x => x.Idkhachhang == SelectedItem.Idkhachhang);

                if (dsKhachhang != null && dsKhachhang.Count() != 0)
                    return true;
                return false;
            }, (p) => {
                var Khachhang = DataProvider.Ins.DB.Khachhang.Where(x => x.Idkhachhang == SelectedItem.Idkhachhang).SingleOrDefault();
                Khachhang.Tenkhachhang = DisplayName;
                Khachhang.Diachi = Diachi;
                Khachhang.Ngaysinh = Ngaysinh;
                Khachhang.Dienthoai = Dienthoai;
                Khachhang.Email = Email;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Tenkhachhang = DisplayName;
            });

            DeleteCommand = new RelayCommand<object>((p) => {
                if (SelectedItem == null)
                    return false;

                var dsKhachhang = DataProvider.Ins.DB.Khachhang.Where(x => x.Idkhachhang == SelectedItem.Idkhachhang);

                if (dsKhachhang != null && dsKhachhang.Count() != 0)
                    return true;
                return false;
            }, (p) => {
                var khachhang = DataProvider.Ins.DB.Khachhang.Where(x => x.Idkhachhang == SelectedItem.Idkhachhang).SingleOrDefault();
                var phieuxuatchitiet = DataProvider.Ins.DB.Phieuxuatchitiet.Where(x => x.Idkhachhang == SelectedItem.Idkhachhang).ToList();
                foreach(var item in phieuxuatchitiet)
                {
                    DataProvider.Ins.DB.Phieuxuatchitiet.Remove(item);
                    Listphieuxuatchitiet.Remove(item);
                    Listphieuxuatchitiet.Clear();
                    DataProvider.Ins.DB.SaveChanges();
                }
                DataProvider.Ins.DB.Khachhang.Remove(khachhang);
                List.Remove(khachhang);
                DataProvider.Ins.DB.SaveChanges();
                ICollectionView view = CollectionViewSource.GetDefaultView(Listphieuxuatchitiet);
                view.Refresh();

            });
        }
    }
}
