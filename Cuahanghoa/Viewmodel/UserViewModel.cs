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
using System.Windows.Data;
using System.Windows.Input;

namespace Cuahanghoa.Viewmodel
{
    public class UserViewModel : BaseViewModel
    {
        private ObservableCollection<Nguoidung> _List;
        public ObservableCollection<Nguoidung> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Phanquyennguoidung> _Phanquyennguoidung;
        public ObservableCollection<Model.Phanquyennguoidung> Phanquyennguoidung { get => _Phanquyennguoidung; set { _Phanquyennguoidung = value; OnPropertyChanged(); } }

        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }

        private string _Tenhienthi;
        public string Tenhienthi { get => _Tenhienthi; set { _Tenhienthi = value; OnPropertyChanged(); } }

        private string _Tendangnhap;
        public string Tendangnhap { get => _Tendangnhap; set { _Tendangnhap = value; OnPropertyChanged(); } }

        private string _Matkhau;
        public string Matkhau { get => _Matkhau; set { _Matkhau = value; OnPropertyChanged(); } }

        private int _Idquyen;
        public int Idquyen { get => _Idquyen; set { _Idquyen = value; OnPropertyChanged(); } }

        private Nguoidung _SelectedItem;
        public Nguoidung SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id = SelectedItem.Idnguoidung;
                    Tenhienthi = SelectedItem.Tenhienthi;
                    Tendangnhap = SelectedItem.Tendangnhap;
                    SelectedPhanquyennguoidung = SelectedItem.Phanquyennguoidung;
                }
            }
        }
        private Phanquyennguoidung _SelectedPhanquyennguoidung;
        public Phanquyennguoidung SelectedPhanquyennguoidung
        {
            get => _SelectedPhanquyennguoidung;
            set
            {
                _SelectedPhanquyennguoidung = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }

        public UserViewModel()
        {
            List = new ObservableCollection<Model.Nguoidung>(DataProvider.Ins.DB.Nguoidung);
            Phanquyennguoidung = new ObservableCollection<Model.Phanquyennguoidung>(DataProvider.Ins.DB.Phanquyennguoidung);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedPhanquyennguoidung == null )
                    return false;
                return true;

            }, (p) =>

            {
                var Nguoidung = new Model.Nguoidung() {Tenhienthi = Tenhienthi, Tendangnhap = Tendangnhap, Idquyen = SelectedPhanquyennguoidung.Idquyen,Matkhau=Matkhau };

                DataProvider.Ins.DB.Nguoidung.Add(Nguoidung);
                DataProvider.Ins.DB.SaveChanges();
                List.Add(Nguoidung);
            });

            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem== null || SelectedPhanquyennguoidung == null)
                    return false;

                var dsnguoidung = DataProvider.Ins.DB.Nguoidung.Where(x => x.Idnguoidung == SelectedItem.Idnguoidung);

                if (dsnguoidung != null && dsnguoidung.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var Nguoidung = DataProvider.Ins.DB.Nguoidung.Where(x => x.Idnguoidung == SelectedItem.Idnguoidung).SingleOrDefault();
                Nguoidung.Matkhau = Matkhau;
                Nguoidung.Tendangnhap = Tendangnhap;
                Nguoidung.Tenhienthi = Tenhienthi;
                Nguoidung.Idquyen = SelectedPhanquyennguoidung.Idquyen;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Tendangnhap = Tendangnhap;

                ICollectionView view = CollectionViewSource.GetDefaultView(List);
                view.Refresh();
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;
                return true;

            }, (p) =>
            {
                var Nguoidung = DataProvider.Ins.DB.Nguoidung.Where(x => x.Idnguoidung == SelectedItem.Idnguoidung).SingleOrDefault();
                DataProvider.Ins.DB.Nguoidung.Remove(Nguoidung);
                List.Remove(Nguoidung);
                DataProvider.Ins.DB.SaveChanges();
            });

            ChangePasswordCommand = new RelayCommand<Nguoidung>((p) =>
            {
                return true;

            }, (p) =>
            {
                MessageBox.Show("Vui lòng liên hệ người quản lý");
            });
        }
    }
}
