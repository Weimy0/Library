using Library.Controls;
using Library.Data;
using Library.Entities;
using Library.Services;
using Library.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Library.ViewModels
{
    public class TakeBookViewModel : ViewModelBase
    {
        private Book _book = null!;
        private List<string> _peoples = null!;

        public List<string> Peoples
        {
            get => _peoples;
            set => Set(ref _peoples, value, nameof(Peoples));
        }

        private string _selectedPeople = null!;

        public string SelectedPeople
        {
            get => _selectedPeople;
            set => Set(ref _selectedPeople, value, nameof(SelectedPeople));
        }

        private PeopleService PeopleService { get; set; } = null!;
        private PeopleBookService PeopleBookService { get; set; } = null!;
        private BookService BookService { get; set; } = null!;
        private TakeBookWindow _view = null!;
        public TakeBookViewModel(PeopleService peopleService, Book book, BookService bookService, TakeBookWindow takeBookWindow, PeopleBookService peopleBookService)
        {
            _book = book;
            BookService = bookService;
            PeopleService = peopleService;
            _view = takeBookWindow;

           PeopleBookService = peopleBookService;

            Peoples = new List<string>() { "Не выбрано" };
            Peoples.AddRange(PeopleService.GetPeoples().Select(x => x.ShortFullName));

            SelectedPeople = Peoples[0];
        }

        private void TakeBook()
        {
            if (SelectedPeople != null && SelectedPeople != Peoples[0])
            {
                PeopleBook peopleBook =new PeopleBook();
                peopleBook.People = PeopleService.GetPeoples().Single(x => x.ShortFullName == SelectedPeople);
                peopleBook.Book = _book;
                _book.Count -= 1;
                PeopleBookService.Insert(peopleBook);
                BookService.Update(_book);
                MessageBox.Show("Книга взята!", "", MessageBoxButton.OK, MessageBoxImage.None);
                _view.Close();
            }
            else
                MessageBox.Show("Выберите человека!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public ICommand TakeBookButton => new Command(x => TakeBook());
    }
}
