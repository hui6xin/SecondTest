using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfFlipPageControl;

namespace WpfFlipPageDemo
{

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void NextClick(object sender, RoutedEventArgs args)
        {
            if (this.myBook.CurrentSheetIndex < this.myBook.GetItemsCount() / 2)
                this.myBook.CurrentSheetIndex++;
        }

        private void PreviousClick(object sender, RoutedEventArgs args)
        {
            if (this.myBook.CurrentSheetIndex > 0)
                this.myBook.CurrentSheetIndex--;
        }

        private void AutoNextClick(object sender, RoutedEventArgs e)
        {
            this.myBook.AnimateToNextPage(this.cbFromTop.SelectedIndex == 0, 700);
            this.myBook.Focus();
        }

        private void AutoPreviousClick(object sender, RoutedEventArgs e)
        {
            this.myBook.AnimateToPreviousPage(this.cbFromTop.SelectedIndex == 0, 700);
            this.myBook.Focus();
        }

        private void DisplayModeChecked(object sender, RoutedEventArgs e)
        {
            bool result = (sender as CheckBox).IsChecked.Value;
            this.myBook.DisplayMode = result ? BookDisplayMode.ZoomOnPage : BookDisplayMode.Normal;
        }

        private void AutoPreviousPageClick(object sender, RoutedEventArgs e)
        {
            this.myBook.MoveToPreviousPage();
        }

        private void AutoNextPageClick(object sender, RoutedEventArgs e)
        {
            this.myBook.MoveToNextPage();
        }

    }

}