using Library.Controls;
using Library.Entities;
using Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class AddOrEditPeopleViewModel : ViewModelBase
    {
        private List<People> _getPeoples = null!;

        public List<People> GetPeoples
        {
            get => _getPeoples;
            set => Set(ref _getPeoples, value, nameof(GetPeoples));
        }

        private People _selectedPeople = null!;

        public People SelectedPeople
        {
            get => _selectedPeople;
            set => Set(ref _selectedPeople, value, nameof(SelectedPeople));
        }

        private People _people = new People();

        public People People
        {
            get => _people;
            set => Set(ref _people, value, nameof(People));
        }

        private PeopleBookService PeopleBookService = null!;
        private PeopleService PeopleService = null!;
        private BookService BookService = null!;

        private MainViewModel _mainViewModel = null!;
        public AddOrEditPeopleViewModel(PeopleBookService peopleBookService, PeopleService peopleService, BookService bookService, MainViewModel mainViewModel)
        {
            _mainViewModel= mainViewModel;
            PeopleBookService = peopleBookService;
            PeopleService = peopleService;
            BookService = bookService;
            Update();
        }

        private void Update()
            => GetPeoples = PeopleService.GetPeoples().Where(x => x.Id != 1).ToList();

        private People people1 = new People();
        private void AddPeople()
        {
            people1 = new People();
            people1.Surname = People.Surname;
            people1.Name = People.Name;
            people1.Lastname = People.Lastname;
            if (people1.Surname == null || people1.Surname == string.Empty)
                MessageBox.Show("Введите фамилию");
            else if (people1.Name == null || people1.Name == string.Empty)
                MessageBox.Show("Введите имя");
            else if (people1.Lastname == null || people1.Lastname == string.Empty)
                MessageBox.Show("Введите отчество");
            else
            {
                people1.PostId = 2;
                if (!Check())
                {
                    PeopleService.Insert(people1);
                    UpdateMain();
                    Update();
                }
                else
                    MessageBox.Show("Такой пользователь есть");
            }
        }

        private void UpdateMain()
        {
            _mainViewModel.FiltrList = new List<string>() { "Не выбрано" };
            _mainViewModel.FiltrList.AddRange(PeopleService.GetPeoples().Select(x => x.ShortFullName));
        }

        private bool Check()
         => PeopleService.GetPeoples().Any(x=>x.Surname == people1.Surname && x.Name == people1.Name && x.Lastname == people1.Lastname);

        private void DeletePeople()
        {
            if (SelectedPeople != null)
            {
                var peopleBooks = PeopleBookService.GetPeopleBooks().Where(x => x.People == SelectedPeople).ToList();
                PeopleBookService.Delete(peopleBooks, BookService);

                PeopleService.Delete(SelectedPeople);
                Update();
            }
        }

        public ICommand AddButton => new Command(x => AddPeople());
        public ICommand DeleteButton => new Command(x => DeletePeople());
    }
}
