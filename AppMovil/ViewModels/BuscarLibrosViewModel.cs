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
        private string searchText = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private ObservableCollection<Libro> libros = new();

        // Propiedades para los filtros
        [ObservableProperty]
        private bool filtrarPorTitulo = true;

        [ObservableProperty]
        private bool filtrarPorAutor = false;

        [ObservableProperty]
        private bool filtrarPorEditorial = false;

        [ObservableProperty]
        private bool filtrarPorGenero = false;

        [ObservableProperty]
        private bool mostrarFiltros = false;

        private List<Libro> _todosLosLibros = new();

        public IRelayCommand BuscarCommand { get; }
        public IRelayCommand LimpiarCommand { get; }
        public IRelayCommand ToggleFiltrosCommand { get; }

        public BuscarLibrosViewModel()
        {
            BuscarCommand = new RelayCommand(OnBuscar);
            LimpiarCommand = new RelayCommand(OnLimpiar);
            ToggleFiltrosCommand = new RelayCommand(OnToggleFiltros);
            _ = InicializarAsync();
        }

        private async Task InicializarAsync()
        {
            OnBuscar();
        }

        partial void OnSearchTextChanged(string value)
        {
            if (string.IsNullOrEmpty(value)) OnBuscar();
        }

        // Los cambios en filtros tambi�n disparan nueva b�squeda
        partial void OnFiltrarPorTituloChanged(bool value) => OnBuscar();
        partial void OnFiltrarPorAutorChanged(bool value) => OnBuscar();
        partial void OnFiltrarPorEditorialChanged(bool value) => OnBuscar();
        partial void OnFiltrarPorGeneroChanged(bool value) => OnBuscar();

        private async void OnBuscar()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                // Obtener todos los libros si no los tenemos
                if (!_todosLosLibros.Any())
                {
                    var todosLibros = await _libroService.GetAllAsync("");
                    _todosLosLibros = todosLibros?.ToList() ?? new List<Libro>();
                }

                // Filtrar seg�n el texto de b�squeda y los filtros seleccionados
                var librosFiltrados = FiltrarLibros(_todosLosLibros);

                Libros = new ObservableCollection<Libro>(librosFiltrados);
            }
            finally
            {
                IsBusy = false;
            }
        }

        //private List<Libro> FiltrarLibros(List<Libro> libros)
        //{
        //    if (string.IsNullOrWhiteSpace(SearchText))
        //        return libros;

        //    var terminoBusqueda = SearchText.ToLower().Trim();

        //    return libros.Where(libro =>
        //    {
        //        bool coincide = false;

        //        // Filtrar por t�tulo si est� seleccionado
        //        if (FiltrarPorTitulo && !string.IsNullOrEmpty(libro.Titulo))
        //        {
        //            coincide |= libro.Titulo.ToLower().Contains(terminoBusqueda);
        //        }

        //        // Filtrar por editorial si est� seleccionado
        //        if (FiltrarPorEditorial && libro.Editorial?.Nombre != null)
        //        {
        //            coincide |= libro.Editorial.Nombre.ToLower().Contains(terminoBusqueda);
        //        }

        //        // Filtrar por autor si est� seleccionado (esto requerir�a navegaci�n a LibroAutor)
        //        if (FiltrarPorAutor)
        //        {
        //            // Por simplicidad, buscaremos en descripci�n o sinopsis
        //            // En un escenario real, necesitar�as incluir la relaci�n LibroAutor
        //            coincide |= (!string.IsNullOrEmpty(libro.Descripcion) && libro.Descripcion.ToLower().Contains(terminoBusqueda)) ||
        //                       (!string.IsNullOrEmpty(libro.Sinopsis) && libro.Sinopsis.ToLower().Contains(terminoBusqueda));
        //        }

        //        // Filtrar por g�nero si est� seleccionado (similar al autor)
        //        if (FiltrarPorGenero)
        //        {
        //            // Por simplicidad, buscaremos en descripci�n o sinopsis
        //            coincide |= (!string.IsNullOrEmpty(libro.Descripcion) && libro.Descripcion.ToLower().Contains(terminoBusqueda)) ||
        //                       (!string.IsNullOrEmpty(libro.Sinopsis) && libro.Sinopsis.ToLower().Contains(terminoBusqueda));
        //        }

        //        return coincide;
        //    }).ToList();
        //}

        private void OnLimpiar()
        {
            SearchText = string.Empty;
            // Mantener los filtros pero ejecutar b�squeda limpia
            OnBuscar();
        }

        private void OnToggleFiltros()
        {
            MostrarFiltros = !MostrarFiltros;
        }
    }

}