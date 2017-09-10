using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Android.Util;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Model.Documents.Order;
using InvertoryHelper.Model.Documents.Recount;
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
        private readonly SQLiteAsyncConnection _db;
        private List<Barcode> _barcodesList;
        private List<Characteristic> _characteristicsList;
        private List<NomenclaturesKind> _nomenclatureKindsList;
        private List<Nomenclature> _nomenclaturesList;
        private List<Price> _priceList;
        private List<Storage> _storagesList;
        private List<Unit> _unitList;

        private DataRepository()
        {
            var path = Path.Combine(DependencyService.Get<IOnPlatform>().GetDatabasePath(), "InvertoryHelperDB");

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    _db = new SQLiteAsyncConnection(() => new SQLiteConnectionWithLock(new SQLitePlatformAndroidN(),
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
                    return _nomenclaturesList;
                return _nomenclaturesList.Where(e).ToList();
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
                return _unitList;
            return _unitList.Where(e).ToList();
        }

        public async Task<List<NomenclaturesKind>> GetNomenclatureKindsAsync(Func<NomenclaturesKind, bool> e = null)
        {
            await CheckLoad();

            if (e == null)
                return _nomenclatureKindsList;
            return _nomenclatureKindsList.Where(e).ToList();
        }

        public async Task<List<Barcode>> GetBarcodesAsync(Func<Barcode, bool> e = null)
        {
            await CheckLoad();

            if (e == null)
                return _barcodesList;
            return _barcodesList.Where(e).ToList();
        }

        public async Task<List<Characteristic>> GetCharacteristicsAsync(Func<Characteristic, bool> e = null)
        {
            await CheckLoad();

            if (e == null)
                return _characteristicsList;
            return _characteristicsList.Where(e).ToList();
        }

        public async Task<List<Price>> GetPricesAsync(Func<Price, bool> e = null)
        {
            await CheckLoad();

            if (e == null)
                return _priceList;
            return _priceList.Where(e).ToList();
        }

        public async Task<List<Order>> GetOrdersAsync(Func<Order, bool> e = null)
        {
            await CheckLoad();

            return await _db.GetAllWithChildrenAsync<Order>();
        }

        public async Task<Guid> SaveNomenclatureAsync(Nomenclature nomenclature)
        {
            await CheckLoad();

            try
            {
                var index = _nomenclaturesList.FindIndex(N => N.Uid == nomenclature.Uid);

                if (index != -1)
                {
                    await _db.UpdateWithChildrenAsync(nomenclature);
                    _nomenclaturesList[index] = nomenclature;
                }
                else
                {
                    await _db.InsertWithChildrenAsync(nomenclature);
                    _nomenclaturesList.Add(nomenclature);
                }
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return Guid.Empty;
            }

            return nomenclature.Uid;
        }

        public async Task<Guid> SaveUnitAsync(Unit unit)
        {
            await CheckLoad();

            try
            {
                var index = _unitList.FindIndex(N => N.Uid == unit.Uid);

                if (index != -1)
                {
                    await _db.UpdateAsync(unit);
                    _unitList[index] = unit;
                }
                else
                {
                    await _db.InsertWithChildrenAsync(unit);
                    _unitList.Add(unit);
                }
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return Guid.Empty;
            }

            return unit.Uid;
        }

        public async Task<Guid> SaveCharacteristicAsync(Characteristic characteristic)
        {
            await CheckLoad();

            try
            {
                var index = _characteristicsList.FindIndex(N => N.Uid == characteristic.Uid);

                if (index != -1)
                {
                    await _db.UpdateWithChildrenAsync(characteristic);
                    _characteristicsList[index] = characteristic;
                }
                else
                {
                    await _db.InsertWithChildrenAsync(characteristic);
                    _characteristicsList.Add(characteristic);
                }
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return Guid.Empty;
            }

            return characteristic.Uid;
        }

        public async Task<Guid> SaveNomenclatureKindAsync(NomenclaturesKind nomenclatureKind)
        {
            await CheckLoad();

            try
            {
                var index = _nomenclatureKindsList.FindIndex(N => N.Uid == nomenclatureKind.Uid);

                if (index != -1)
                {
                    await _db.UpdateAsync(nomenclatureKind);
                    _nomenclatureKindsList[index] = nomenclatureKind;
                }
                else
                {
                    await _db.InsertWithChildrenAsync(nomenclatureKind);
                    _nomenclatureKindsList.Add(nomenclatureKind);
                }
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return Guid.Empty;
            }

            return nomenclatureKind.Uid;
        }

        public async Task<Guid> SaveBarcodeAsync(Barcode barcode)
        {
            await CheckLoad();

            try
            {
                var index = _barcodesList.FindIndex(N => N.Uid == barcode.Uid);

                if (index != -1)
                {
                    await _db.UpdateWithChildrenAsync(barcode);
                    _barcodesList[index] = barcode;
                }
                else
                {
                    await _db.InsertWithChildrenAsync(barcode);
                    _barcodesList.Add(barcode);
                }
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return Guid.Empty;
            }

            return barcode.Uid;
        }

        public async Task<Guid> SavePriceAsync(Price price)
        {
            await CheckLoad();

            try
            {
                var index = _priceList.FindIndex(N => N.Uid == price.Uid);

                if (index != -1)
                {
                    await _db.UpdateWithChildrenAsync(price);
                    _priceList[index] = price;
                }
                else
                {
                    await _db.InsertWithChildrenAsync(price);
                    _priceList.Add(price);
                }
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return Guid.Empty;
            }

            return price.Uid;
        }

        public async Task<Guid> SaveOrderAsync(Order order)
        {
            await CheckLoad();

            try
            {
                if (order.Uid != Guid.Empty)
                {
                    var rowsToDelete = _db.GetWithChildrenAsync<Order>(order.Uid).Result.OrderRows;

                    foreach (var orderRow in rowsToDelete)
                        await _db.DeleteAsync<OrderRow>(orderRow.Uid);
                }

                await _db.InsertOrReplaceWithChildrenAsync(order);
                await _db.InsertAllWithChildrenAsync(order.OrderRows);
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return Guid.Empty;
            }

            return order.Uid;
        }

        public async Task<Order> GetOrderAsync(Guid orderUid)
        {
            try
            {
                var order = await _db.GetWithChildrenAsync<Order>(orderUid);

                if (order == null)
                    return new Order();

                foreach (var orderRow in order.OrderRows)
                    await _db.GetChildrenAsync(orderRow);

                return order;
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return new Order();
            }
        }

        public async Task<List<Recount>> GetRecountsAsync(Func<Recount, bool> e = null)
        {
            await CheckLoad();

            return await _db.GetAllWithChildrenAsync<Recount>();
        }

        public async Task<Recount> GetRecountAsync(Guid recountUid)
        {
            try
            {
                var recount = await _db.GetWithChildrenAsync<Recount>(recountUid);

                if (recount == null)
                    return new Recount();

                foreach (var recountRow in recount.RecountRows)
                    await _db.GetChildrenAsync(recountRow);

                return recount;
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return new Recount();
            }
        }

        public async Task<Guid> SaveRecountAsync(Recount recount)
        {
            await CheckLoad();

            try
            {
                if (recount.Uid != Guid.Empty)
                {
                    var rowsToDelete = _db.GetWithChildrenAsync<Recount>(recount.Uid).Result.RecountRows;

                    foreach (var recountRow in rowsToDelete)
                        await _db.DeleteAsync<RecountRow>(recountRow.Uid);
                }

                await _db.InsertOrReplaceWithChildrenAsync(recount);
                await _db.InsertAllWithChildrenAsync(recount.RecountRows);
            }
            catch (Exception ex)
            {
                Log.Error("DatabaseError", ex.Message);
                return Guid.Empty;
            }

            return recount.Uid;
        }

        private async Task CheckLoad()
        {
            while (IsLoading)
                await Task.Delay(100);
        }

        private async Task CreateDbAsync()
        {
            if (_db != null)
            {
                IsLoading = true;

                try
                {
                    await _db.CreateTableAsync<Unit>();
                    await _db.CreateTableAsync<NomenclaturesKind>();
                    await _db.CreateTableAsync<Nomenclature>();
                    await _db.CreateTableAsync<Characteristic>();
                    await _db.CreateTableAsync<Barcode>();
                    await _db.CreateTableAsync<Price>();
                    await _db.CreateTableAsync<Storage>();
                    await _db.CreateTableAsync<Order>();
                    await _db.CreateTableAsync<OrderRow>();
                    await _db.CreateTableAsync<Recount>();
                    await _db.CreateTableAsync<RecountRow>();

                    _nomenclaturesList = await _db.GetAllWithChildrenAsync<Nomenclature>();
                    _characteristicsList = await _db.GetAllWithChildrenAsync<Characteristic>();
                    _unitList = await _db.GetAllWithChildrenAsync<Unit>();
                    _nomenclatureKindsList = await _db.GetAllWithChildrenAsync<NomenclaturesKind>();
                    _priceList = await _db.GetAllWithChildrenAsync<Price>();
                    _barcodesList = await _db.GetAllWithChildrenAsync<Barcode>();
                    _storagesList = await _db.GetAllWithChildrenAsync<Storage>();
                }
                catch (Exception ex)
                {
                    Log.Error("DatabaseError", ex.Message);
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }
    }
}