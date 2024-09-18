using ProyectoHibrido2.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoHibrido2.Services
{
    public class FavoritosService
    {
        private readonly SQLiteAsyncConnection _database;

        public FavoritosService()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"favoritos.db");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<ProductoFavorito>().Wait();
        }

        public async Task<ProductoFavorito>ReadAsync(int id)
        {
            return await _database.Table<ProductoFavorito>().Where(p=>p.ProductoId == id).FirstOrDefaultAsync();
        }

        public async Task<List<ProductoFavorito>> ReadAllAsync()
        {
            return await _database.Table<ProductoFavorito>().ToListAsync();
        }
        public async Task CreateAsync(ProductoFavorito productoFavorito)
        {
            await _database.InsertAsync(productoFavorito);
            
        }
        public async Task DeleteAsync(ProductoFavorito productoFavorito)
        {
            await _database.DeleteAsync(productoFavorito);
            
        }


    }
}
