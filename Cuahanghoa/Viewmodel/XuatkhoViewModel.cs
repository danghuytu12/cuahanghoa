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
    class XuatkhoViewModel : BaseViewModel
    {
        private ObservableCollection<Phieuxuatchitiet> _List;
        public ObservableCollection<Phieuxuatchitiet> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Tonkho> _TonkhoList;
        public ObservableCollection<Tonkho> TonkhoList { get => _TonkhoList; set { _TonkhoList = value; OnPropertyChanged("TonkhoList"); } }

        private ObservableCollection<Model.Phieunhapchitiet> _Phieunhapchitiet;
        public ObservableCollection<Model.Phieunhapchitiet> Phieunhapchitiet { get => _Phieunhapchitiet; set { _Phieunhapchitiet = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Sanpham> _Sanpham;
        public ObservableCollection<Model.Sanpham> Sanpham { get => _Sanpham; set { _Sanpham = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Phieuxuat> _Phieuxuat;
        public ObservableCollection<Model.Phieuxuat> Phieuxuat { get => _Phieuxuat; set { _Phieuxuat = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Khachhang> _Khachhang;
        public ObservableCollection<Model.Khachhang> Khachhang { get => _Khachhang; set { _Khachhang = value; OnPropertyChanged(); } }

        

        private string _Idphieuxuatchitiet;
        public string Idphieuxuatchitiet { get => _Idphieuxuatchitiet; set { _Idphieuxuatchitiet = value; OnPropertyChanged(); } }


        private int? _Soluong;
        public int? Soluong { get => _Soluong; set { _Soluong = value; OnPropertyChanged(); } }

        private Phieuxuatchitiet _SelectedItem;
        public Phieuxuatchitiet SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Idphieuxuatchitiet = SelectedItem.Idphieuxuatchitiet;
                    SelectedSanpham = SelectedItem.Sanpham;
                    SelectedPhieuxuat = SelectedItem.Phieuxuat;
                    SelectedPhieunhapchitiet = SelectedItem.Phieunhapchitiet;
                    Soluong = SelectedItem.Soluong;
                    SelectedKhachhang = SelectedItem.Khachhang;
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
        private Model.Phieuxuat _SelectedPhieuxuat;
        public Model.Phieuxuat SelectedPhieuxuat
        {
            get => _SelectedPhieuxuat;
            set
            {
                _SelectedPhieuxuat = value;
                OnPropertyChanged();
            }
        }


        private Model.Phieunhapchitiet _SelectedPhieunhapchitiet;
        public Model.Phieunhapchitiet SelectedPhieunhapchitiet
        {
            get => _SelectedPhieunhapchitiet;
            set
            {
                _SelectedPhieunhapchitiet = value;
                OnPropertyChanged();
            }
        }

        private Model.Khachhang _SelectedKhachhang;
        public Model.Khachhang SelectedKhachhang
        {
            get => _SelectedKhachhang;
            set
            {
                _SelectedKhachhang = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public XuatkhoViewModel()
        {
            List = new ObservableCollection<Phieuxuatchitiet>(DataProvider.Ins.DB.Phieuxuatchitiet);
            Sanpham = new ObservableCollection<Model.Sanpham>(DataProvider.Ins.DB.Sanpham);
            Phieuxuat= new ObservableCollection<Model.Phieuxuat>(DataProvider.Ins.DB.Phieuxuat);
            Khachhang = new ObservableCollection<Model.Khachhang>(DataProvider.Ins.DB.Khachhang);
            Phieunhapchitiet = new ObservableCollection<Model.Phieunhapchitiet>(DataProvider.Ins.DB.Phieunhapchitiet);

            AddCommand = new RelayCommand<object>((p) =>
            {

                if (SelectedSanpham == null &&  SelectedKhachhang == null && SelectedPhieunhapchitiet == null && SelectedPhieuxuat == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                var xuatkho = new Model.Phieuxuatchitiet() {Idphieuxuatchitiet=Idphieuxuatchitiet,Idphieuxuat=SelectedPhieuxuat.Idphieuxuat, Idsanpham = SelectedSanpham.Idsanpham, Soluong = Soluong,Idkhachhang=SelectedKhachhang.Idkhachhang, Idphieunhapchitiet=SelectedPhieunhapchitiet.Idphieunhapchitiet };
                DataProvider.Ins.DB.Phieuxuatchitiet.Add(xuatkho);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(xuatkho);

            });
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var dsxuatkho = DataProvider.Ins.DB.Phieuxuatchitiet.Where(x => x.Idphieuxuatchitiet == SelectedItem.Idphieuxuatchitiet);

                if (dsxuatkho != null && dsxuatkho.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var xuatkho = DataProvider.Ins.DB.Phieuxuatchitiet.Where(x => x.Idphieuxuatchitiet == SelectedItem.Idphieuxuatchitiet).SingleOrDefault();
                xuatkho.Soluong = Soluong;
                xuatkho.Idsanpham = SelectedSanpham.Idsanpham;
                xuatkho.Idphieuxuat = SelectedPhieuxuat.Idphieuxuat;
                xuatkho.Idphieunhapchitiet = SelectedPhieunhapchitiet.Idphieunhapchitiet;
                xuatkho.Idkhachhang = SelectedKhachhang.Idkhachhang;
                DataProvider.Ins.DB.SaveChanges();

                


            });

            DeleteCommand = new RelayCommand<object>((p) => {
                if (SelectedItem == null|| SelectedPhieunhapchitiet == null|| SelectedSanpham==null||SelectedPhieuxuat== null)
                    return false;

                var dsnhapkho = DataProvider.Ins.DB.Phieunhapchitiet.Where(x => x.Idphieunhapchitiet == SelectedItem.Idphieunhapchitiet);
                if (dsnhapkho != null && dsnhapkho.Count() != 0)
                    return true;
                return false;
            }, (p) => {
               
                var xuatkho = DataProvider.Ins.DB.Phieuxuatchitiet.Where(x => x.Idphieunhapchitiet == SelectedItem.Idphieunhapchitiet).SingleOrDefault();
               

                DataProvider.Ins.DB.Phieuxuatchitiet.Remove(xuatkho);
                List.Remove(xuatkho);
                DataProvider.Ins.DB.SaveChanges();
                ICollectionView view = CollectionViewSource.GetDefaultView(List);
                view.Refresh();
            });
            
        }
    }
}
