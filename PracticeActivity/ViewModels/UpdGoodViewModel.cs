using PracticeActivity.Models;
using PracticeActivity.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticeActivity.ViewModels
{
    public class UpdGoodViewModel
    {
        public UpdGoodViewModel()
        {
            FillPage();
        }

        public string UpdDescripcion { get; set; }
        public string UpdPrecio { get; set; }
        public ICommand Update => new Command(UpdateGood);

        //Método para llenar los entry al cargar la pagina
        //con los datos a modificar
        public async void FillPage()
        {
            var myDescr = (App.Current.Properties["des"].ToString());
            var myPrec = (App.Current.Properties["preci"].ToString());

            UpdDescripcion = myDescr;
            UpdPrecio = myPrec;
        }

        //Método para actualizar el registro
        public async void UpdateGood()
        {
            var myId = (App.Current.Properties["id"].ToString());
            if (UpdDescripcion != null)
            {
                if (UpdPrecio != null)
                {
                    var good = new GoodsModel()
                    {
                        ID = int.Parse(myId),
                        Descripcion = UpdDescripcion,
                        Precio = UpdPrecio,
                    };
                    var save = await App.Database.SaveGoodsAsync(good);
                    if (save != null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "El registro ha sido modificado con exito", "ok");
                        await App.Current.MainPage.Navigation.PushAsync(new GoodList());
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "El campo del precio esta vacio, por favor ingreselo", "ok");
                }

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "El campo de la descripcion esta vacio, por favor ingrese descripcion", "ok");
            }
        }
    }
}
