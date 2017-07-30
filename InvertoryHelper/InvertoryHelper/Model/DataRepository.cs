using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InvertoryHelper.Common;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;
using InvertoryHelper.Model;
using SQLite.Net.Async;
using SQLiteNetExtensionsAsync.Extensions;
using System.Linq.Expressions;
using System.Diagnostics;

[assembly: Dependency(typeof(DataRepository))]
namespace InvertoryHelper.Model
{
    public class DataRepository : IDataRepository
    {

        private static readonly DataRepository instance = new DataRepository();

        public static DataRepository Instance
        {
            get => instance;
        }

        private SQLiteAsyncConnection db;
        private List<Nomenclature> nomenclaturesList;
        private List<Characteristic> characteristicsList;
        private List<Unit> unitList;
        private List<Price> priceList;
        private List<Barcode> barcodeslList;
        private List<Storage> storagesList;

        public bool IsLoading { get; private set; }

        private DataRepository()
        {
            var path = Path.Combine(DependencyService.Get<ISpecPlatform>().GetDatabasePath(), "InvertoryHelperDB");

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    db = new SQLite.Net.Async.SQLiteAsyncConnection(new Func<SQLiteConnectionWithLock>(
                        () => new SQLiteConnectionWithLock(new SQLitePlatformAndroidN(), new SQLiteConnectionString(path, true))));

                    break;

                    //Other platforms
            }


            Task.Run(CreateDbAsync);

        }

        private async Task CheckLoad()
        {

            while (IsLoading)
            {
                await Task.Delay(100);
            }

        }

        public async Task<List<Nomenclature>> GetNomenclaturesAsync(Func<Nomenclature, bool> e = null)
        {
            await CheckLoad();

            try {

            if (e == null)
                return nomenclaturesList.ToList();
            else
                return nomenclaturesList.Where(e).ToList();
            }
            catch ( Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<Nomenclature>();
            }

        }

        public async Task<List<Unit>> GetUnitsAsync(Func<Unit, bool> e = null)
        {
            await CheckLoad();

            if (e == null)
                return unitList.ToList();
            else
                return unitList.Where(e).ToList();
        }

        public async Task<Guid>  SaveNomenclatureAsync(Nomenclature nomenclature)
        {

            try
            {
                
                int index = nomenclaturesList.FindIndex((N) => N.Uid == nomenclature.Uid);

                if (index != -1)
                {
                    await db.UpdateWithChildrenAsync(nomenclature);
                    nomenclaturesList[index] = nomenclature;
                }
                else
                {
                    await db.InsertAsync(nomenclature);
                    nomenclaturesList.Add(nomenclature);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Guid.Empty;
            }

            return nomenclature.Uid;

        }

        private async Task CreateDbAsync()
        {
            if (db != null)
            {

                IsLoading = true;

                try
                {
                    await db.CreateTableAsync<Unit>();
                    await db.CreateTableAsync<NomenclaturesKind>();
                    await db.CreateTableAsync<Nomenclature>();
                    await db.CreateTableAsync<Characteristic>();
                    await db.CreateTableAsync<Barcode>();
                    await db.CreateTableAsync<Price>();
                    await db.CreateTableAsync<Storage>();

                    if(await db.Table<Nomenclature>().CountAsync() == 0)
                    for (int i = 1; i<= 100; i++)
                    {
                            await db.InsertAsync(new Nomenclature()
                            {
                                Name = string.Format("Nomenclature {0}", i),
                                Artikul = i.ToString(),
                                Uid = new Guid()
                            });
                    }

                    if (await db.Table<Unit>().CountAsync() == 0)
                        for (int i = 1; i <= 10; i++)
                        {
                            await db.InsertAsync(new Unit()
                            {
                                Name = string.Format("Unit {0}", i),
                                Uid = new Guid()
                            });
                        }

                    nomenclaturesList = await db.GetAllWithChildrenAsync<Nomenclature>();
                    characteristicsList = await db.GetAllWithChildrenAsync<Characteristic>();
                    unitList = await db.GetAllWithChildrenAsync<Unit>();
                    priceList = await db.GetAllWithChildrenAsync<Price>();
                    barcodeslList = await db.GetAllWithChildrenAsync<Barcode>();
                    storagesList = await db.GetAllWithChildrenAsync<Storage>();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsLoading = false;
                }

            }
        }


    }
}