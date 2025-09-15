using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.Models;
using Service.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace AppMovil.ViewModels
{
    public partial class PrestamosViewModel : ObservableObject
    {
        GenericService<Prestamo> prestamoService = new();

        [ObservableProperty]
        private bool _isBusy;
        [ObservableProperty]
        private string _mensajeVacio = "No tienes préstamos activos";

        public bool TienePrestamos => Prestamos.Count > 0;

        [ObservableProperty]
        private ObservableCollection<Prestamo> prestamos = new();
        public IRelayCommand GetAllComand { get; }

        public PrestamosViewModel()
        {
            GetAllComand = new RelayCommand(OnGetAll);
            _ = InitializeAsync();
        }


        private object InitializeAsync()
        {
            OnGetAll();
        }

        private async void OnGetAll()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var Prestamos = await prestamoService.GetAllAsync();
                prestamos = new ObservableCollection<Prestamo>(Prestamos ?? new List<Prestamo>());
            }
            finally
            {
                IsBusy = false;
            }
        }
    }

    public class PrestamoItem : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string TituloLibro { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public DateTime FechaPrestamo { get; set; }

        private DateTime _fechaVencimiento;
        public DateTime FechaVencimiento
        {
            get => _fechaVencimiento;
            set { _fechaVencimiento = value; OnPropertyChanged(); OnPropertyChanged(nameof(DiasRestantes)); OnPropertyChanged(nameof(EstaVencido)); }
        }

        public string EstadoPrestamo { get; set; } = string.Empty;

        public int DiasRestantes => (FechaVencimiento - DateTime.Now).Days;
        public bool EstaVencido => DateTime.Now > FechaVencimiento;
        public string ColorEstado => EstaVencido ? "#F44336" : (DiasRestantes <= 3 ? "#FF9800" : "#4CAF50");

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}
