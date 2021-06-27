using Cuahanghoa.Model;
using Cuahanghoa.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cuahanghoa.Viewmodel
{
    class LoginViewModel : BaseViewModel
    {
        public bool Islogin { get; set; }
        private string _UserName;
        public string UserName { get => _UserName; set { _UserName = value; OnPropertyChanged(); } }
        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }
       


        public ICommand LoginCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        public LoginViewModel()
        {
            Islogin = false;
            LoginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Login(p);
            });
            CloseCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Exit(p);
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => {
                Password = p.Password;
            });
        }

        void Login(Window p)
        {
            if(p== null)
            {
                return;
            }
            var accCount=DataProvider.Ins.DB.Nguoidung.Where(x => x.Tendangnhap == UserName && x.Matkhau == Password).Count();

            if (accCount>0)
            {
                Islogin = true;
                p.Close();
            }
            else
            {
                Islogin = false;
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!");
            }
        }
        void Exit(Window p)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có muốn thoát hay không?","Đăng Nhập", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                p.Close();
            }
            else
            {
                p.Show();
            }
        }
    }
}
