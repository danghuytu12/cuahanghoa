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
    public class SanphamViewModel : BaseViewModel
    {
        private ObservableCollection<Sanpham> _List;
        public ObservableCollection<Sanpham> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Nhacungcap> _Nhacungcap;
        public ObservableCollection<Model.Nhacungcap> Nhacungcap { get => _Nhacungcap; set { _Nhacungcap = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Phieuxuatchitiet> _ListOutputInfo;
        public ObservableCollection<Model.Phieuxuatchitiet> ListOutputInfo { get => _ListOutputInfo; set { _ListOutputInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Phieunhapchitiet> _ListInputInfo;
        public ObservableCollection<Model.Phieunhapchitiet> ListInputInfo { get => _ListInputInfo; set { _ListInputInfo = value; OnPropertyChanged(); } }


        private string _Idsanpham;
        public string Idsanpham { get => _Idsanpham; set { _Idsanpham = value; OnPropertyChanged(); } }

        private string _Tensanpham;
        public string Tensanpham { get => _Tensanpham; set { _Tensanpham = value; OnPropertyChanged(); } }

       

        private Sanpham _SelectedItem;
        public Sanpham SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Idsanpham = SelectedItem.Idsanpham;
                    Tensanpham = SelectedItem.Tensanpham;
                    SelectedNhacungcap = SelectedItem.Nhacungcap;

                }
            }
        }


        private Nhacungcap _SelectedNhacungcap;
        public Nhacungcap SelectedNhacungcap
        {
            get => _SelectedNhacungcap;
            set
            {
                _SelectedNhacungcap = value;
                OnPropertyChanged();
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public SanphamViewModel()
        {
            List = new ObservableCollection<Sanpham>(DataProvider.Ins.DB.Sanpham);
            Nhacungcap = new ObservableCollection<Model.Nhacungcap>(DataProvider.Ins.DB.Nhacungcap);


            ListOutputInfo = new ObservableCollection<Model.Phieuxuatchitiet>(DataProvider.Ins.DB.Phieuxuatchitiet);
            ListInputInfo = new ObservableCollection<Model.Phieunhapchitiet>(DataProvider.Ins.DB.Phieunhapchitiet);

            AddCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedNhacungcap != null)
                    return true;
                return false;
            }, (p) =>
            {
                var Sanpham = new Sanpham() { Tensanpham = Tensanpham, Idsanpham = Idsanpham, Idnhacungcap=SelectedNhacungcap.Idnhacungcap };
                DataProvider.Ins.DB.Sanpham.Add(Sanpham);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(Sanpham);
                
            });
            EditCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null|| SelectedNhacungcap == null)
                    return false;

                var dsSanpham = DataProvider.Ins.DB.Sanpham.Where(x => x.Idsanpham == SelectedItem.Idsanpham);

                if (dsSanpham != null && dsSanpham.Count() != 0)
                    return true;
                return false;
            }, (p) =>
            {
                var Sanpham = DataProvider.Ins.DB.Sanpham.Where(x => x.Idsanpham == SelectedItem.Idsanpham).SingleOrDefault();
                Sanpham.Tensanpham = Tensanpham;
                Sanpham.Idsanpham = Idsanpham;
                Sanpham.Idnhacungcap = SelectedNhacungcap.Idnhacungcap;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Tensanpham = Tensanpham;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null || SelectedNhacungcap == null )
                    return false;

                var displayList = DataProvider.Ins.DB.Sanpham.Where(x => x.Idsanpham == SelectedItem.Idsanpham);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;

            }, (p) =>
            {
                var Object = DataProvider.Ins.DB.Sanpham.Where(x => x.Idsanpham == SelectedItem.Idsanpham).SingleOrDefault();
                var collection = DataProvider.Ins.DB.Phieuxuatchitiet.Where(x => x.Idsanpham == SelectedItem.Idsanpham).ToList();

                foreach (var item in collection)
                {
                    DataProvider.Ins.DB.Phieuxuatchitiet.Remove(item);
                    ListOutputInfo.Remove(item);
                    ListOutputInfo.Clear();
                    DataProvider.Ins.DB.SaveChanges();
                }


                var collection1 = DataProvider.Ins.DB.Phieunhapchitiet.Where(x => x.Idsanpham == SelectedItem.Idsanpham).ToList();
                foreach (var item in collection1)
                {
                    DataProvider.Ins.DB.Phieunhapchitiet.Remove(item);
                    ListInputInfo.Remove(item);
                    ListInputInfo.Clear();

                }

                DataProvider.Ins.DB.Sanpham.Remove(Object);
                List.Remove(Object);
                DataProvider.Ins.DB.SaveChanges();
                ICollectionView view = CollectionViewSource.GetDefaultView(ListOutputInfo);
                view.Refresh();
            });

            
        }
    }
}
