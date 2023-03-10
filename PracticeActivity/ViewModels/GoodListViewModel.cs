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
    public class GoodListViewModel
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

        public ICommand Delete => new Command(DeleteGood);
        public ICommand Update => new Command(UpdateGood);
        public GoodsModel SelectGood { get; set; }
        public ObservableCollection<GoodsModel> GoodList { get; set; }

        //Metodo para eliminar un registro seleccionado de la lista
        public async void DeleteGood()
        {
            if (SelectGood != null)
            {
                var res = await App.Current.MainPage.DisplayAlert("Alerta", "Desea eliminar el registro seleccionado", "si", "no");
                if (res)
                {
                    var del = App.Database.DeleteGoodAsync(SelectGood);
                    await App.Current.MainPage.DisplayAlert("Alerta", "El registro ha sido eliminado con exito", "ok");
                    await App.Current.MainPage.Navigation.PushAsync(new GoodList());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "El registro no ha sido eliminado", "ok");
                    await App.Current.MainPage.Navigation.PushAsync(new GoodList());
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione el registro que de sea eliminar", "ok");
            }
        }
        //Método para editar o actualizar un registro de la lista
        public async void UpdateGood()
        {
            if (SelectGood != null)
            {
                App.Current.Properties["id"] = SelectGood.ID;
                App.Current.Properties["des"] = SelectGood.Descripcion;
                App.Current.Properties["preci"] = SelectGood.Precio;
                await App.Current.MainPage.Navigation.PushAsync(new UpdGoodPage());


            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Seleccione el " +
                    "registro que de sea modificar", "ok");
            }
        }

        //Metodo para llenar la lista de la página
        public GoodListViewModel()
        {
            FillList();
        }
        private async void FillList()
        {
            GoodList = new ObservableCollection<GoodsModel>();
            var MyList = await App.Database.GetGoodsAsync();
            foreach (var item in MyList)
            {
                GoodList.Add(new GoodsModel
                {
                    ID = item.ID,
                    Descripcion = item.Descripcion,
                    Precio = item.Precio,
                });
            }
        }
    }
}
