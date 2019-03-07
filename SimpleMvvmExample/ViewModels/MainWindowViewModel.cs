using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SimpleMvvmExample.Models;

namespace SimpleMvvmExample.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private Services.DataStoringService _dataStorage = new Services.DataStoringService();

        // For combobox
        private ObservableCollection<Models.UserInformation> _customers = new ObservableCollection<Models.UserInformation>();
        public ObservableCollection<Models.UserInformation> Customers
        {
            get { return _customers; }
            set { SetProperty(ref _customers, value); }
        }

        private Models.UserInformation _selectedUser;
        public Models.UserInformation SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                UpdateUserInfo(value);
                SetProperty(ref _selectedUser, value);
            }
        }

        private void UpdateUserInfo(UserInformation value)
        {
            Firstname = value.FirstName;
            Lastname = value.LastName;
            Birthday = value.Birthday;
            CustomerId = value.CustomerId;
        }

        // Userinformation
        private string _firsname;
        public string Firstname
        {
            get { return _firsname; }
            set { SetProperty(ref _firsname, value); }
        }
        private string _lastname;
        public string Lastname
        {
            get { return _lastname; }
            set { SetProperty(ref _lastname, value); }
        }
        private string _customerId;
        public string CustomerId
        {
            get { return _customerId; }
            set { SetProperty(ref _customerId, value); }
        }

        private DateTime _birhtday;
        public DateTime Birthday
        {
            get { return _birhtday; }
            set { SetProperty(ref _birhtday, value); }
        }

        // Commands
        private readonly DelegateCommand _storeUser;
        public ICommand StoreUserCommand => _storeUser;

        private readonly DelegateCommand _createNew;
        public ICommand CreateNewCommand => _createNew;

        public MainWindowViewModel()
        {
            _createNew = new DelegateCommand(OnCreateNew);
            _storeUser = new DelegateCommand(OnStoreUserinfo);
            LoadUserData();
            InitializeUserinfo();
        }

        private void OnCreateNew(object obj)
        {
            InitializeUserinfo();
        }

        private void InitializeUserinfo()
        {
            Lastname = Firstname = string.Empty;
            Birthday = DateTime.Now;
            CustomerId = (Customers.Count+1).ToString("D9");
        }
        private void OnStoreUserinfo(object obj)
        {
            if (CustomerId == string.Empty ||
                Firstname == string.Empty ||
                Lastname == string.Empty)
            {
                MessageBox.Show("User information is not completed!");
            }
            else
            {
                var userInfo = new Models.UserInformation();
                userInfo.FirstName = Firstname;
                userInfo.LastName = Lastname;
                userInfo.Birthday = Birthday;
                userInfo.CustomerId = CustomerId;
                Customers.Add(userInfo);
                _dataStorage.StoreUserinformation(userInfo);

                InitializeUserinfo();
            }
        }

        private void LoadUserData()
        {
            var users = _dataStorage.LoadAll();
            Customers = new ObservableCollection<Models.UserInformation>(users);
        }
    }
}
