using PracticeActivity.Models;
using PracticeActivity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticeActivity.ViewModels
{
    public class RegisterServiceViewModel
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
        public ICommand SaveService => new Command(SaveRegister);
        public string Description { get; set; }
        public string Price { get; set; }

        public async void SaveRegister()
        {
            if (Description != null)
            {
                if (Price != null)
                {
                    var service = new ServicesModel()
                    {
                        Descripcion = Description,
                        Precio = Price,
                    };
                    var Sav = await App.Database.SaveServiceAsync(service);
                    if (Sav != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "El registro fue guardado con exito", "ok");
                        await App.Current.MainPage.Navigation.PushAsync(new ServiceListPage());
                        //nos elimina la pagina anterior del estack de paginas
                        var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
                        foreach (var page in existingPages)
                        {
                            if (page.GetType() == typeof(ServiceListPage))
                                continue;
                            App.Current.MainPage.Navigation.RemovePage(page);
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "El registro no fue guardado", "ok");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "Debe ingrasar datos de precio", "ok");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Debe ingrasar descripcion para continuar", "ok");
            }

        }
    }
}
