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
    public class NhapkhoViewModel : BaseViewModel
    {
        private ObservableCollection<Phieunhapchitiet> _Listnhapkho;
        public ObservableCollection<Phieunhapchitiet> Listnhapkho { get => _Listnhapkho; set { _Listnhapkho = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Sanpham> _Sanpham;
        public ObservableCollection<Model.Sanpham> Sanpham { get => _Sanpham; set { _Sanpham = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Phieunhap> _Phieunhap;
        public ObservableCollection<Model.Phieunhap> Phieunhap { get => _Phieunhap; set { _Phieunhap = value; OnPropertyChanged(); } }


        private ObservableCollection<Model.Phieuxuatchitiet> _Listphieuxuatchitiet;
        public ObservableCollection<Model.Phieuxuatchitiet> Listphieuxuatchitiet { get => _Listphieuxuatchitiet; set { _Listphieuxuatchitiet = value; OnPropertyChanged(); } }

        private string _Idphieunhapchitiet;
        public string Idphieunhapchitiet { get => _Idphieunhapchitiet; set { _Idphieunhapchitiet = value; OnPropertyChanged(); } }


        private int? _Soluong;
        public int? Soluong { get => _Soluong; set { _Soluong = value; OnPropertyChanged(); } }

        private double? _Gianhap;
        public double? Gianhap { get => _Gianhap; set { _Gianhap = value; OnPropertyChanged(); } }

        private double? _Giaxuat;
        public double? Giaxuat { get => _Giaxuat; set { _Giaxuat = value; OnPropertyChanged(); } }

        private Phieunhapchitiet _SelectedItem;
        public Phieunhapchitiet SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Idphieunhapchitiet = SelectedItem.Idphieunhapchitiet;
                    SelectedSanpham = SelectedItem.Sanpham;
                    SelectedPhieunhap = SelectedItem.Phieunhap;
                    Soluong = SelectedItem.Soluong;
                    Gianhap = SelectedItem.Gianhap;
                    Giaxuat = SelectedItem.Giaxuat;
                }
            }
        }

        private Model.Sanpham _SelectedSanpham;
        public Model.Sanpham SelectedSanpham
        {
            get => _SelectedSanpham;
            set
            {
                _SelectedSanpham = value;
                OnPropertyChanged();
            }
        }

        private Model.Phieunhap _SelectedPhieunhap;
        public Model.Phieunhap SelectedPhieunhap
        {
            get => _SelectedPhieunhap;
            set
            {
                _SelectedPhieunhap = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public NhapkhoViewModel()
        {
            Listnhapkho = new ObservableCollection<Phieunhapchitiet>(DataProvider.Ins.DB.Phieunhapchitiet);
            Sanpham = new ObservableCollection<Model.Sanpham>(DataProvider.Ins.DB.Sanpham);
            Phieunhap = new ObservableCollection<Model.Phieunhap>(DataProvider.Ins.DB.Phieunhap);
            Listphieuxuatchitiet = new ObservableCollection<Model.Phieuxuatchitiet>(DataProvider.Ins.DB.Phieuxuatchitiet);

            AddCommand = new RelayCommand<object>((p) => {

                if (SelectedSanpham == null  && SelectedPhieunhap == null)
                    return false;
                return true;
            }, (p) => {

               
                var nhapkho = new Model.Phieunhapchitiet() { Idsanpham=SelectedSanpham.Idsanpham, Idphieunhap = SelectedPhieunhap.Idphieunhap, Soluong = Soluong, Gianhap = Gianhap, Giaxuat = Giaxuat, Idphieunhapchitiet= Idphieunhapchitiet };
                DataProvider.Ins.DB.Phieunhapchitiet.Add(nhapkho);
                DataProvider.Ins.DB.SaveChanges();

                Listnhapkho.Add(nhapkho);
            });

            EditCommand = new RelayCommand<object>((p) => {
                if (SelectedItem == null)
                    return false;

                var dsnhapkho = DataProvider.Ins.DB.Phieunhapchitiet.Where(x => x.Idphieunhapchitiet == SelectedItem.Idphieunhapchitiet);

                if (dsnhapkho != null && dsnhapkho.Count() != 0)
                    return true;
                return false;
            }, (p) => {
                var nhapkho = DataProvider.Ins.DB.Phieunhapchitiet.Where(x => x.Idphieunhapchitiet == SelectedItem.Idphieunhapchitiet).SingleOrDefault();
                nhapkho.Soluong = Soluong;
                nhapkho.Gianhap = Gianhap;
                nhapkho.Giaxuat = Giaxuat;
                nhapkho.Idsanpham = SelectedSanpham.Idsanpham;
                nhapkho.Idphieunhap = SelectedPhieunhap.Idphieunhap;
              
                DataProvider.Ins.DB.SaveChanges();
            });

            DeleteCommand = new RelayCommand<object>((p) => {
                if (SelectedItem == null )
                    return false;

                var dsnhapkho = DataProvider.Ins.DB.Phieunhapchitiet.Where(x => x.Idphieunhapchitiet == SelectedItem.Idphieunhapchitiet);
                if (dsnhapkho != null && dsnhapkho.Count() != 0)
                    return true;
                return false;
            }, (p) => {
                var nhapkho = DataProvider.Ins.DB.Phieunhapchitiet.Where(x => x.Idphieunhapchitiet == SelectedItem.Idphieunhapchitiet).SingleOrDefault();
                var xuatkho = DataProvider.Ins.DB.Phieuxuatchitiet.Where(x => x.Idphieunhapchitiet == SelectedItem.Idphieunhapchitiet).ToList();
                foreach(var item in xuatkho)
                {
                    DataProvider.Ins.DB.Phieuxuatchitiet.Remove(item);
                    Listphieuxuatchitiet.Remove(item);
                    Listphieuxuatchitiet.Clear();
                    DataProvider.Ins.DB.SaveChanges();
                }

                DataProvider.Ins.DB.Phieunhapchitiet.Remove(nhapkho);
                Listnhapkho.Remove(nhapkho);
                DataProvider.Ins.DB.SaveChanges();
                ICollectionView view = CollectionViewSource.GetDefaultView(Listnhapkho);
                view.Refresh();
            });
        }
    }
}
