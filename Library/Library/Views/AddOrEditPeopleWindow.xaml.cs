using Library.Services;
using Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Library.Views
{
    /// <summary>
    /// Логика взаимодействия для AddOrEditPeopleWindow.xaml
    /// </summary>
    public partial class AddOrEditPeopleWindow : Window
    {
        public AddOrEditPeopleWindow(PeopleBookService peopleBookService, PeopleService peopleService, BookService bookService, MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = new AddOrEditPeopleViewModel(peopleBookService, peopleService, bookService, mainViewModel);
        }
    }
}
