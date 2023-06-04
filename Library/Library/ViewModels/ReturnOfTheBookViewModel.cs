using Library.Entities;
using Library.Services;
using Library.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Library.Controls;

namespace Library.ViewModels
{
    public class ReturnOfTheBookViewModel : ViewModelBase
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
            set
            {
                Set(ref _selectedPeople, value, nameof(SelectedPeople));
                if (value != null)
                    Update();
            }
        }

        private List<PeopleBook> _getBooks = null!;

        public List<PeopleBook> GetBooks
        {
            get => _getBooks;
            set => Set(ref _getBooks, value, nameof(GetBooks));
        }

        private PeopleBook _selectedBook = null!;

        public PeopleBook SelectedBook
        {
            get => _selectedBook;
            set => Set(ref _selectedBook, value, nameof(SelectedBook));
        }

        private PeopleService PeopleService { get; set; } = null!;
        private PeopleBookService PeopleBookService { get; set; } = null!;
        private BookService BookService { get; set; } = null!;
        private ReturnOfTheBookWindow _view = null!;
        public ReturnOfTheBookViewModel(PeopleService peopleService, BookService bookService, ReturnOfTheBookWindow returnOfTheBookWindow, PeopleBookService peopleBookService )
        {
            BookService = bookService;
            PeopleService = peopleService;
            _view = returnOfTheBookWindow;

            PeopleBookService = peopleBookService;

            Peoples = new List<string>() { "Не выбрано" };
            Peoples.AddRange(PeopleService.GetPeoples().Select(x => x.ShortFullName));
            SelectedPeople = Peoples[0];

            Update();

            SelectedPeople = Peoples[0];
        }

        private void Update()
        {
            GetBooks = GetBooksList();
        }

        private List<PeopleBook> GetBooksList()
        {
            var books = new List<PeopleBook>();
            if (SelectedPeople == Peoples[0] || SelectedPeople == null)
                return books;
            else
                return PeopleBookService.GetPeopleBooks().Where(x => x.People.ShortFullName == SelectedPeople).ToList();
        }

        private void ReturnOfTheBook()
        {
            if (SelectedPeople != null && SelectedPeople != Peoples[0])
            {
                if(SelectedBook != null)
                {
                    SelectedBook.Book.Count += 1;
                    BookService.Update(SelectedBook.Book);
                    PeopleBookService.Delete(SelectedBook);
                    MessageBox.Show("Книга была успешно возвращена!", "", MessageBoxButton.OK, MessageBoxImage.None);
                    _view.Close();
                }
                else
                    MessageBox.Show("Выберите книгу!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Выберите человека!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public ICommand ReturnOfTheBookButton => new Command(x => ReturnOfTheBook());
    }
}
