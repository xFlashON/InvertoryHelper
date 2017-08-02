﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.XamarinAndroid;
using SQLiteNetExtensionsAsync.Extensions;
using Xamarin.Forms;

[assembly: Dependency(typeof(DataRepository))]

namespace InvertoryHelper.Model
{
    public class DataRepository : IDataRepository
    {
        private List<Barcode> barcodeslList;
        private List<Characteristic> characteristicsList;

        private readonly SQLiteAsyncConnection db;
        private List<NomenclaturesKind> nomenclatureKindsList;
        private List<Nomenclature> nomenclaturesList;
        private List<Price> priceList;
        private List<Storage> storagesList;
        private List<Unit> unitList;

        private DataRepository()
        {
            var path = Path.Combine(DependencyService.Get<ISpecPlatform>().GetDatabasePath(), "InvertoryHelperDB");

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    db = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(new SQLitePlatformAndroidN(),
                        new SQLiteConnectionString(path, true)));

                    break;

                //Other platforms
            }


            Task.Run(CreateDbAsync);
        }

        public static DataRepository Instance { get; } = new DataRepository();

        public bool IsLoading { get; private set; }

        public async Task<List<Nomenclature>> GetNomenclaturesAsync(Func<Nomenclature, bool> e = null)
        {
            await CheckLoad();

            try
            {
                if (e == null)
                    return nomenclaturesList;
                return nomenclaturesList.Where(e).ToList();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return new List<Nomenclature>();
            }
        }

        public async Task<List<Unit>> GetUnitsAsync(Func<Unit, bool> e = null)
        {
            await CheckLoad();

            if (e == null)
                return unitList;
            return unitList.Where(e).ToList();
        }

        public async Task<List<NomenclaturesKind>> GetNomenclatureKindsAsync(Func<NomenclaturesKind, bool> e = null)
        {
            await CheckLoad();

            if (e == null)
                return nomenclatureKindsList;
            return nomenclatureKindsList.Where(e).ToList();
        }

        public async Task<List<Barcode>> GetBarcodesAsync(Func<Barcode, bool> e = null)
        {
            await CheckLoad();

            if (e == null)
                return barcodeslList;
            return barcodeslList.Where(e).ToList();
        }

        public async Task<List<Characteristic>> GetCharacteristicsAsync(Func<Characteristic, bool> e = null)
        {

            await CheckLoad();

            if (e == null)
                return characteristicsList;
            return characteristicsList.Where(e).ToList();

        }

        public async Task<Guid> SaveNomenclatureAsync(Nomenclature nomenclature)
        {
            try
            {
                var index = nomenclaturesList.FindIndex(N => N.Uid == nomenclature.Uid);

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

        public async Task<Guid> SaveUnitAsync(Unit unit)
        {
            try
            {
                var index = unitList.FindIndex(N => N.Uid == unit.Uid);

                if (index != -1)
                {
                    await db.UpdateAsync(unit);
                    unitList[index] = unit;
                }
                else
                {
                    await db.InsertAsync(unit);
                    unitList.Add(unit);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Guid.Empty;
            }

            return unit.Uid;
        }

        public async Task<Guid> SaveCharacteristicAsync(Characteristic characteristic)
        {
            try
            {
                var index = characteristicsList.FindIndex(N => N.Uid == characteristic.Uid);

                if (index != -1)
                {
                    await db.UpdateAsync(characteristic);
                    characteristicsList[index] = characteristic;
                }
                else
                {
                    await db.InsertAsync(characteristic);
                    characteristicsList.Add(characteristic);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Guid.Empty;
            }

            return characteristic.Uid;
        }

        public async Task<Guid> SaveNomenclatureKindAsync(NomenclaturesKind nomenclatureKind)
        {
            try
            {
                var index = nomenclatureKindsList.FindIndex(N => N.Uid == nomenclatureKind.Uid);

                if (index != -1)
                {
                    await db.UpdateAsync(nomenclatureKind);
                    nomenclatureKindsList[index] = nomenclatureKind;
                }
                else
                {
                    await db.InsertAsync(nomenclatureKind);
                    nomenclatureKindsList.Add(nomenclatureKind);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Guid.Empty;
            }

            return nomenclatureKind.Uid;
        }

        private async Task CheckLoad()
        {
            while (IsLoading)
                await Task.Delay(100);
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

                    if (await db.Table<Nomenclature>().CountAsync() == 0)
                        for (var i = 1; i <= 100; i++)
                            await db.InsertAsync(new Nomenclature
                            {
                                Name = string.Format("Nomenclature {0}", i),
                                Artikul = i.ToString(),
                                Uid = new Guid()
                            });

                    if (await db.Table<Unit>().CountAsync() == 0)
                        for (var i = 1; i <= 10; i++)
                            await db.InsertAsync(new Unit
                            {
                                Name = string.Format("Unit {0}", i),
                                Uid = new Guid()
                            });

                    nomenclaturesList = await db.GetAllWithChildrenAsync<Nomenclature>();
                    characteristicsList = await db.GetAllWithChildrenAsync<Characteristic>();
                    unitList = await db.GetAllWithChildrenAsync<Unit>();
                    nomenclatureKindsList = await db.GetAllWithChildrenAsync<NomenclaturesKind>();
                    priceList = await db.GetAllWithChildrenAsync<Price>();
                    barcodeslList = await db.GetAllWithChildrenAsync<Barcode>();
                    storagesList = await db.GetAllWithChildrenAsync<Storage>();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

    }
}