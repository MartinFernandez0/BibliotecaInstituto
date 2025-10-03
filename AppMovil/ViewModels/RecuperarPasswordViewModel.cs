using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Service.DTOs;
using Service.Interfaces;
using Service.Services;

namespace AppMovil.ViewModels
{
    public partial class RecuperarPasswordViewModel : ObservableObject
    {
        AuthService _authService = new();

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnviarCommand))]
        private string mail = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EnviarCommand))]
        private bool isBusy = false;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private string successMessage = string.Empty;

        public IRelayCommand EnviarCommand { get; }
        public IRelayCommand VolverCommand { get; }

        public RecuperarPasswordViewModel()
        {
            EnviarCommand = new AsyncRelayCommand(OnEnviar, CanEnviar);
            VolverCommand = new AsyncRelayCommand(OnVolver);
        }

        private bool CanEnviar()
        {
            return !IsBusy && !string.IsNullOrWhiteSpace(Mail);
        }

        private async Task OnEnviar()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                SuccessMessage = string.Empty;

                // Validaci�n b�sica de email
                if (!Mail.Contains("@") || !Mail.Contains("."))
                {
                    ErrorMessage = "Por favor, ingrese un correo electr�nico v�lido";
                    return;
                }

                LoginDTO loginReset = new LoginDTO
                {
                    Username = Mail,
                    Password = string.Empty // No se necesita contrase�a para recuperaci�n
                };

                bool result = await _authService.ResetPassword(loginReset);

                if (result)
                {
                    SuccessMessage = "Se han enviado las instrucciones a su correo electr�nico";
                    // Esperar 3 segundos para que el usuario vea el mensaje antes de volver
                    await Task.Delay(3000);
                    await OnVolver();
                }
                else
                {
                    ErrorMessage = "No se pudo enviar las instrucciones. Verifique su correo electr�nico e intente nuevamente.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al enviar las instrucciones: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnVolver()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}