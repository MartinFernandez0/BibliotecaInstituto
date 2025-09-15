using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.Models;
using Service.Services;
using System.Collections.ObjectModel;

namespace AppMovil.ViewModels
{
    public partial class BuscarLibrosViewModel : ObservableObject
    {
        GenericService<Libro> _libroService = new GenericService<Libro>();

        [ObservableProperty]
        //[NotifyCanExecuteChangedFor(nameof(BuscarCommand))]
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isBusy;
        private readonly List<string> _todosLosLibros;

        [ObservableProperty]
        private ObservableCollection<Libro> libros { get; set; } = new();
        public IRelayCommand BuscarCommand { get; }
        public IRelayCommand LimpiarCommand { get; }

        public BuscarLibrosViewModel()
        {
            BuscarCommand = new RelayCommand(OnBuscar);
            _ = InicializarAsync();
        }

        private async Task InicializarAsync()
        {
            var libros = await _libroService.GetAllAsync();
            Libros = new ObservableCollection<Libro>(libros ?? new List<Libro>());
        }

        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) OnBuscar();
        }

        private async void OnBuscar()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                var libro = await _libroService.GetAllAsync(SearchText);
                Libros = new ObservableCollection<Libro>(libro ?? new List<Libro>());
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}