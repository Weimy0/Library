﻿using Library.Entities;
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
    /// Логика взаимодействия для TakeBookWindow.xaml
    /// </summary>
    public partial class TakeBookWindow : Window
    {
        public TakeBookWindow(PeopleService peopleService, Book book, BookService bookService, PeopleBookService peopleBookService)
        {
            InitializeComponent();
            DataContext = new TakeBookViewModel(peopleService, book, bookService, this, peopleBookService);
        }
    }
}
