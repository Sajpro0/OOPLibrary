using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using static System.Collections.Specialized.BitVector32;

namespace OOPLibrary
{
    public partial class MainWindow : Window
    {
        private Library library;

        public MainWindow()
        {
            InitializeComponent();
            library = new Library();
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            string title = tbTitle.Text;
            string author = tbAuthor.Text;
            string isbn = tbISBN.Text;
            int year = int.Parse(tbYear.Text);
            string specific = tbSpecific.Text;
            string bookType = (cbBookType.SelectedItem as ComboBoxItem)?.Content.ToString();

            Book book = null;

            if (bookType == "Fiction")
            {
                book = new Fiction(title, author, isbn, year, specific);
            }
            else if (bookType == "Non-Fiction")
            {
                book = new NonFiction(title, author, isbn, year, specific);
            }
            else if (bookType == "Comic")
            {
                book = new Comic(title, author, isbn, year, specific);
            }

            if (book != null)
            {
                library.AddBook(book);
                MessageBox.Show("Book added.");
            }
        }

        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            lbBookList.Items.Clear();
            foreach (var book in library.BookList)
            {
                lbBookList.Items.Add(book.ToString());
            }
        }

        private void btnSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            library.SaveToFile("book_list.txt");
            MessageBox.Show("Book list saved.");
        }
    }

    [XmlInclude(typeof(Fiction)),XmlInclude(typeof(NonFiction)),XmlInclude(typeof(Comic))]
    public class Book
    {
        public string Title, Author, ISBN;
        public int Year;

		public Book(string title, string author, string isbn, int year)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Year = year;
        }

        public Book()
        {
            Title = "Untitled";
            Author = "None";
            ISBN = "None";
            Year = int.MinValue;
        }
    }

	public class Fiction : Book
	{
        public string Genre;
		public Fiction(string title, string author, string isbn, int year,string genre) : base(title, author, isbn, year)
		{
            Genre = genre;
		}
        public Fiction() : base()
        {
            Genre = "None";
        }
	}

	public class NonFiction : Book
	{
        public string Field;
		public NonFiction(string title, string author, string isbn, int year, string field) : base(title, author, isbn, year)
		{
            Field = field;
		}
        public NonFiction() : base()
        {
            Field = "None";
        }
	}
    public class Comic : Book
	{
        public string Illustrator;
		public Comic(string title, string author, string isbn, int year, string illustrator) : base(title, author, isbn, year)
		{
            Illustrator = illustrator;
		}

        public Comic() : base()
        {
            Illustrator = "Noone";
        }
	}

    public class Library
    {
        public List<Book> BookList = new List<Book>();
        public void AddBook(Book book)
        {
            BookList.Add(book);
        }

        public string ShowBookList()
        {
            string str = "";
            foreach (Book book in BookList)
            {
                str += $"{book.Title}  {book.Author}  {book.ISBN}  {book.Year}  " + ((book is Fiction b1) ? ("Genre: " + b1.Genre) : (book is NonFiction b2) ? ("Field: " + b2.Field) : (book is Comic b3) ? ("Illustrator: " + b3.Illustrator) : "Null") + Environment.NewLine;
            }
            return str;
        }

        public void SaveToFile(string path)
        {
            XmlSerializer xml = new XmlSerializer(typeof(List<Book>));
            using (var fs = File.Create(path))
                xml.Serialize(fs, BookList);
        }
    }
}