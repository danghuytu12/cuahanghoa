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
    public class NhacungcapViewModel : BaseViewModel
    {
        private ObservableCollection<Nhacungcap> _List;
        public ObservableCollection<Nhacungcap> List { get => _List; set { _List = value; OnPropertyChanged(); } }

        

        private int _ID;
        public int Id { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _Tennhacungcap;
        public string Tennhacungcap { get => _Tennhacungcap; set { _Tennhacungcap = value; OnPropertyChanged(); } }

        private string _Diachi;
        public string Diachi { get => _Diachi; set { _Diachi = value; OnPropertyChanged(); } }

        private string _Dienthoai;
        public string Dienthoai { get => _Dienthoai; set { _Dienthoai = value; OnPropertyChanged(); } }

        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }

        private Nhacungcap _SelectedItem;
        public Nhacungcap SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    Id = SelectedItem.Idnhacungcap;
                    Tennhacungcap = SelectedItem.Tennhacungcap;
                    Diachi = SelectedItem.Diachi;
                    Dienthoai = SelectedItem.Dienthoai;
                    Email = SelectedItem.Email;

                }
            }
        }

        private ObservableCollection<Model.Phieuxuatchitiet> _ListOutputInfo;
        public ObservableCollection<Model.Phieuxuatchitiet> ListOutputInfo { get => _ListOutputInfo; set { _ListOutputInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Phieunhapchitiet> _ListInputInfo;
        public ObservableCollection<Model.Phieunhapchitiet> ListInputInfo { get => _ListInputInfo; set { _ListInputInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<Model.Sanpham> _ListObject;
        public ObservableCollection<Model.Sanpham> ListObject { get => _ListObject; set { _ListObject = value; OnPropertyChanged(); } }


        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public NhacungcapViewModel()
        {
            List = new ObservableCollection<Nhacungcap>(DataProvider.Ins.DB.Nhacungcap);

            ListOutputInfo = new ObservableCollection<Model.Phieuxuatchitiet>(DataProvider.Ins.DB.Phieuxuatchitiet);
            ListInputInfo = new ObservableCollection<Model.Phieunhapchitiet>(DataProvider.Ins.DB.Phieunhapchitiet);
            ListObject = new ObservableCollection<Model.Sanpham>(DataProvider.Ins.DB.Sanpham);

            AddCommand = new RelayCommand<object>((p) => 
            {
                    return true;
            }, (p) => {
                var nhacungcap = new Nhacungcap() { Tennhacungcap= Tennhacungcap, Diachi=Diachi,Dienthoai=Dienthoai,Email=Email};
                DataProvider.Ins.DB.Nhacungcap.Add(nhacungcap);
                DataProvider.Ins.DB.SaveChanges();

                List.Add(nhacungcap);
            });

            EditCommand = new RelayCommand<object>((p) => {
                if (SelectedItem ==null)
                    return false;

                var dsnhacungcap = DataProvider.Ins.DB.Nhacungcap.Where(x => x.Idnhacungcap == SelectedItem.Idnhacungcap);

                if (dsnhacungcap != null && dsnhacungcap.Count() != 0)
                    return true;
                return false;
            }, (p) => {
                var nhacungcap = DataProvider.Ins.DB.Nhacungcap.Where(x => x.Idnhacungcap == SelectedItem.Idnhacungcap).SingleOrDefault();
                nhacungcap.Tennhacungcap = Tennhacungcap;
                nhacungcap.Diachi = Diachi;
                nhacungcap.Dienthoai = Dienthoai;
                nhacungcap.Email = Email;
                DataProvider.Ins.DB.SaveChanges();

                SelectedItem.Tennhacungcap = Tennhacungcap;
            });

            DeleteCommand = new RelayCommand<object>((p) =>
            {
                if (SelectedItem == null)
                    return false;

                var displayList = DataProvider.Ins.DB.Nhacungcap.Where(x => x.Idnhacungcap == SelectedItem.Idnhacungcap);
                if (displayList != null && displayList.Count() != 0)
                    return true;
                return false;
            }, (p) => {
                var nhacungcap = DataProvider.Ins.DB.Nhacungcap.Where(x => x.Idnhacungcap == SelectedItem.Idnhacungcap).SingleOrDefault();
                var sanpham = DataProvider.Ins.DB.Sanpham.Where(x => x.Idnhacungcap == SelectedItem.Idnhacungcap).ToList();
                foreach (var item in sanpham)
                {
                    if (item != null)
                    {
                        var collection = DataProvider.Ins.DB.Phieuxuatchitiet.Where(x => x.Idsanpham == item.Idsanpham).ToList();
                        if (collection != null)
                        {
                            foreach (var i in collection)
                            {
                                if (i != null)
                                {
                                    DataProvider.Ins.DB.Phieuxuatchitiet.Remove(i);
                                    ListOutputInfo.Remove(i);

                                }

                                ICollectionView view1 = CollectionViewSource.GetDefaultView(ListOutputInfo);
                                view1.Refresh();
                            }


                        }
                        var col = DataProvider.Ins.DB.Phieunhapchitiet.Where(x => x.Idsanpham == item.Idsanpham).ToList();
                        foreach (var j in col)
                        {
                            if (j != null)
                            {
                                DataProvider.Ins.DB.Phieunhapchitiet.Remove(j);
                                ListInputInfo.Remove(j);

                            }

                            ICollectionView view1 = CollectionViewSource.GetDefaultView(ListInputInfo);
                            view1.Refresh();

                        }


                        DataProvider.Ins.DB.Sanpham.Remove(item);
                        ListObject.Remove(item);


                    }

                }

                DataProvider.Ins.DB.Nhacungcap.Remove(nhacungcap);
                List.Remove(nhacungcap);
                DataProvider.Ins.DB.SaveChanges();
                ICollectionView view = CollectionViewSource.GetDefaultView(List);
                view.Refresh();
            });
        }
    }
}