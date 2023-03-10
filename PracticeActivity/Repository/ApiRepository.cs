using PracticeActivity.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PracticeActivity.Repository
{
    public class ApiRepository
    {
        readonly SQLiteAsyncConnection database;

        public ApiRepository(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            //Creando La tabla tipo Bienes
            database.CreateTableAsync<GoodsModel>().Wait();
            // Creando la tabla para el modelo servicios
            database.CreateTableAsync<ServicesModel>().Wait();
        }
        ///Metodo Para solicitar los datos para llenar la lista de bienes
        public Task<List<GoodsModel>> GetGoodsAsync()
        {
            return database.Table<GoodsModel>().ToListAsync();
        }

        ///Para solicitar los datos para llenar la lista de servicios
        public Task<List<ServicesModel>> GetServicesAsync()
        {
            return database.Table<ServicesModel>().ToListAsync();
        }


        //Metodo Para solicitar los datos po ID y poderlos modificar para bienes
        public Task<GoodsModel> GetGoodsAsync(int id)
        {
            return database.Table<GoodsModel>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        //Metodo Para solicitar los datos po ID y poderlos modificar para servicios
        public Task<ServicesModel> GetServicesAsync(int id)
        {
            return database.Table<ServicesModel>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        //Metodo para guardar el listado o registro de bienes en la base de datos
        public Task<int> SaveGoodsAsync(GoodsModel good)
        {
            if (good.ID != 0)
            {
                return database.UpdateAsync(good);
            }
            else
            {
                return database.InsertAsync(good);
            }
        }
        //Metodo para guardar el listado o registro de servicios en la base de datos
        public Task<int> SaveServiceAsync(ServicesModel service)
        {
            if (service.ID != 0)
            {
                return database.UpdateAsync(service);
            }
            else
            {
                return database.InsertAsync(service);
            }
        }

        //Para borrar un registro del listado de bienes
        public Task<int> DeleteGoodAsync(GoodsModel good)
        {
            return database.DeleteAsync(good);
        }
        //Para borrar un registro del listado de servicios
        public Task<int> DeleteServiceAsync(ServicesModel service)
        {
            return database.DeleteAsync(service);
        }

    }
}
