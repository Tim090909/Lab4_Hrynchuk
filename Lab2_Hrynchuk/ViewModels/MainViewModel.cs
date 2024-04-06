using Lab4_Hrynchuk.Tools;
using Lab4_Hrynchuk.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4_Hrynchuk.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<PersonViewModel> _users;
        public RelayCommand<object> ShowAddWindowCommand { get; private set; }
        public ObservableCollection<PersonViewModel> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged(nameof(Users)); }
        }

        public MainViewModel()
        {
            GenerateAndPopulateUsers();
            ShowAddWindowCommand = new RelayCommand<object>(param => ShowAddWindow());
        }

        private void GenerateAndPopulateUsers()
        {
            Users = new ObservableCollection<PersonViewModel>();
            Random random = new Random();
            string[] firstNames = { "John", "Alice", "Michael", "Emily", "James", "Emma", "William", "Olivia", "Daniel", "Sophia" };
            string[] lastNames = { "Smith", "Johnson", "Brown", "Taylor", "Wilson", "Davis", "Miller", "White", "Clark", "Hall" };

            for (int i = 0; i < 50; i++)
            {
                string firstName = firstNames[random.Next(0, firstNames.Length)];
                string lastName = lastNames[random.Next(0, lastNames.Length)];
                string email = $"{firstName.ToLower()}{lastName.ToLower()}@gmail.com";
                DateTime birthDate = GetRandomBirthDate();

                PersonViewModel user = new PersonViewModel
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = email,
                    BirthDate = birthDate,
                };

                Users.Add(user);
            }
        }

        private DateTime GetRandomBirthDate()
        {
            Random random = new Random();
            DateTime start = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range)).Date;
        }

        private void ShowAddWindow()
        {
            Views.InputView inputWindow = new Views.InputView();
            inputWindow.DataContext = this;
            inputWindow.ShowDialog();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
