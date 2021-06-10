using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Totalview.Testers.ServerStressTester
{
    public class MainViewModel : ViewModelBase
    {
        /* ----------------------------------------------------------------------------  */
        /*                                  PROPERTIES                                   */
        /* ----------------------------------------------------------------------------  */
        public ICommand ConnectCommand { get; }

        private string _numberOfClients;
        public string NumberOfClients
        {
            get => _numberOfClients;
            set => SetProperty(ref _numberOfClients, value);
        }

        private string _address = "https://localhost:5003";
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public ObservableCollection<ClientViewModel> Clients { get; }

        /* ----------------------------------------------------------------------------  */
        /*                                 CONSTRUCTORS                                  */
        /* ----------------------------------------------------------------------------  */
        public MainViewModel()
        {
            NumberOfClients = "10";
            Clients = new ObservableCollection<ClientViewModel>();

            ConnectCommand = new RelayCommand(Connect, () => true);
        }

        /* ----------------------------------------------------------------------------  */
        /*                                PRIVATE METHODS                                */
        /* ----------------------------------------------------------------------------  */
        private void Connect()
        {
            Random random = new Random();
            int numberOfClients = int.Parse(NumberOfClients);

            for (int i = 0; i < numberOfClients; i++)
            {
                var c = new ClientViewModel(random)
                {
                    Name = (i + 1).ToString()
                };
                c.StartConnecting(Address);
                Clients.Add(c);
            }

            foreach (var client in Clients)
            {
                client.Start();
            }
        }
    }
}
