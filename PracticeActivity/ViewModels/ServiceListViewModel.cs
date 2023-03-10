using PracticeActivity.Models;
using PracticeActivity.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticeActivity.ViewModels
{
    public class ServiceListViewModel
    {
        public ICommand Exit => new Command(ButtonsPage);

        public async void ButtonsPage()
        {
            //nos envía a la pagina de registrar alumnos
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            //nos elimina la pagina anterior del estack de paginas
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(MainPage))
                    continue;
                App.Current.MainPage.Navigation.RemovePage(page);
            }

        }
        
        public ICommand Delete => new Command(DeleteAlumn);
        public ICommand Update => new Command(UpdateAlumn);
        public ServicesModel SelectService { get; set; }
        public ObservableCollection<ServicesModel> ServiceList { get; set; }

        //Metodo para eliminar un registro seleccionado de la lista
        public async void DeleteAlumn()
        {
            if (SelectService != null)
            {
                var res = await App.Current.MainPage.DisplayAlert("Alerta", "Desea eliminar el registro seleccionado", "si", "no");
                if (res)
                {
                    var del = App.Database.DeleteServiceAsync(SelectService);
                    await App.Current.MainPage.DisplayAlert("Alerta", "El registro ha sido eliminado con exito", "ok");
                    await App.Current.MainPage.Navigation.PushAsync(new ServiceListPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "El registro no ha sido eliminado", "ok");
                    await App.Current.MainPage.Navigation.PushAsync(new ServiceListPage());
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione el registro que de sea eliminar", "ok");
            }
        }
        //Método para editar o actualizar un registro de la lista
        public async void UpdateAlumn()
        {
            if (SelectService != null)
            {
                App.Current.Properties["id"] = SelectService.ID;
                App.Current.Properties["des"] = SelectService.Descripcion;
                App.Current.Properties["preci"] = SelectService.Precio;
                await App.Current.MainPage.Navigation.PushAsync(new UpdServicePage());


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione el " +
                    "registro que de sea modificar", "ok");
            }
        }

        //Metodo para llenar la lista de la página
        public ServiceListViewModel()
        {
            FillList();
        }
        private async void FillList()
        {
            ServiceList = new ObservableCollection<ServicesModel>();
            var MyList = await App.Database.GetServicesAsync();
            foreach (var item in MyList)
            {
                ServiceList.Add(new ServicesModel
                {
                    ID = item.ID,
                   Descripcion = item.Descripcion,
                   Precio = item.Precio,
                });
            }
        }


    }
}
