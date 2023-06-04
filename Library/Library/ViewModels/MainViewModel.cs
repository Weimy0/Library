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
    public class MainViewModel : ViewModelBase
    {
        private List<Book> _getBooks = null!;

        public List<Book> GetBooks
        {
            get => _getBooks;

            set => Set(ref _getBooks, value, nameof(GetBooks));
        }

        private Book _selectedBook = null!;

        public Book SelectedBook
        {
            get => _selectedBook;
            set => Set(ref _selectedBook, value, nameof(SelectedBook));
        }


        private List<string> _sortList = new List<string>();

        public List<string> SortList
        {
            get => _sortList;
            set => Set(ref _sortList, value, nameof(SortList));
        }

        private string _selectedSort = null!;

        public string SelectedSort
        {
            get => _selectedSort;
            set
            {
                Set(ref _selectedSort, value, nameof(SelectedSort));
                if (value != null)
                    UpdateBooks();
            }
        }


        private List<string> _filtrList = new List<string>();

        public List<string> FiltrList
        {
            get => _filtrList;
            set => Set(ref _filtrList, value, nameof(FiltrList));
        }

        private string _selectedFiltr = null!;

        public string SelectedFiltr
        {
            get => _selectedFiltr;
            set
            {
                Set(ref _selectedFiltr, value, nameof(SelectedFiltr));
                if (value != null)
                    UpdateBooks();
            }
        }

        private string _searth = null!;

        public string Searth
        {
            get => _searth;
            set
            {
                Set(ref _searth, value, nameof(Searth));
                if (value != null)
                    UpdateBooks();
            }
        }


        private PeopleBookService PeopleBookService { get; set; } = null!;
        private BookService BookService { get; set; } = null!;
        private PeopleService PeopleService { get; set; } = null!;

        public MainViewModel(ApplicationDbContext db, User user)
        {
            BookService = new(db);
            PeopleService = new(db);
            PeopleBookService = new(db);

            FiltrList = new List<string>() { "Не выбрано" };
            FiltrList.AddRange(PeopleService.GetPeoples().Select(x => x.ShortFullName));

            SortList = new List<string>() { "Не выбрано", "По названию", "По дате издания", "По автору" };
            SelectedFiltr = FiltrList[0];
            SelectedSort = SortList[0];

            Update();
        }

        public void Update()
        {
            

            UpdateBooks();
        }

        private void UpdateBooks()
            => GetBooks = Sort(Filtr(SearthBlock(BookService.GetBooks())));

        private List<Book> Sort(List<Book> books)
        {
            if (SelectedSort == SortList[1])
                return books.OrderBy(x => x.Title).ToList();
            else if (SelectedSort == SortList[2])
                return books.OrderBy(x => x.DateOfPublication.Date).ToList();
            else if (SelectedSort == SortList[3])
                return books.OrderBy(x => x.Author).ToList();
            else
                return books;
        }

        private List<Book> Filtr(List<Book> books)
        {
            if (SelectedFiltr == FiltrList[0] || SelectedFiltr == null)
                return books;
            else
            {
                List<Book> list = new List<Book>();
                var b = PeopleBookService.GetPeopleBooks().Where(x => x.People.ShortFullName == SelectedFiltr).ToList();
                foreach (var item in b)
                {
                    if (!list.Contains(item.Book))
                        list.Add(item.Book);
                }
                return list;
            }
        }

        private List<Book> SearthBlock(List<Book> books)
        {
            if (Searth == null || Searth == string.Empty)
                return books;
            else
                return books.Where(x => x.Title.ToLower().Contains(Searth.ToLower()) ||
                    x.Author.ToLower().Contains(Searth.ToLower())).ToList();
        }

        private void IsExit()
        {
            var signInWinndow = new SignInWindow();
            var CurrentWindow = Application.Current.MainWindow;
            signInWinndow.Show();
            Application.Current.MainWindow = signInWinndow;
            CurrentWindow.Close();
        }

        private void ReturnOfTheBook()
        {
            new ReturnOfTheBookWindow(PeopleService, BookService, PeopleBookService).ShowDialog();
            Update();
        }

        private void TakeBook()
        {
            if (SelectedBook != null)
            {
                if (SelectedBook.Count > 0)
                {
                    new TakeBookWindow(PeopleService, SelectedBook, BookService, PeopleBookService).ShowDialog();
                    SelectedBook = null!;

                    Update();
                }
                else
                    MessageBox.Show("Книг нет в наличии!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Выберите книгу!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AddOrEditPeople()
        {
            new AddOrEditPeopleWindow(PeopleBookService, PeopleService, BookService, this).ShowDialog();
            Update();
        }

        public ICommand ReturnOfTheBookButton => new Command(x => ReturnOfTheBook());
        public ICommand TakeBookButton => new Command(x => TakeBook());
        public ICommand ExitButton => new Command(x => IsExit());
        public ICommand AddOrEditPeopleButton => new Command(x => AddOrEditPeople());
    }
}
