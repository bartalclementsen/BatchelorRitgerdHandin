using System.Windows;

namespace Totalview.Testers.ServerStressTester
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            _viewModel = new MainViewModel();
            DataContext = _viewModel;

            InitializeComponent();
        }
    }
}
