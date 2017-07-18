using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InvertoryHelper.Common;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;
using InvertoryHelper.Model;

[assembly:Dependency(typeof(DataRepository))]
namespace InvertoryHelper.Model
{
    public class DataRepository : IDataRepository
    {
        private SQLiteConnection db;

        public DataRepository()
        {
            var path = Path.Combine(DependencyService.Get<ISpecPlatform>().GetDatabasePath(), "InvertoryHelperDB");

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    db = new SQLiteConnection(new SQLitePlatformAndroidN(), path);
                    break;

                //Other platforms
            }
        }

        public List<Nomenclature> GetNomenclatures()
        {
            var tmp = db.GetAllWithChildren<Nomenclature>();
            var tmp2 = tmp.ToList();
            return tmp2;
        }

        public Nomenclature GetNomenclature(Guid uid)
        {
            throw new NotImplementedException();
        }

        public List<Unit> GetUnits()
        {
            return db.GetAllWithChildren<Unit>().ToList();
        }

        public Unit GetUnit(Guid uid)
        {
            throw new NotImplementedException();
        }

        public List<NomenclaturesKind> GetNomenclaturesKinds()
        {
            return db.GetAllWithChildren<NomenclaturesKind>().ToList();
        }

        public NomenclaturesKind GetNomenclaturesKind(Guid uid)
        {
            throw new NotImplementedException();
        }

        public List<Characteristic> GetCharacteristics()
        {
            return db.GetAllWithChildren<Characteristic>().ToList();
        }

        public Characteristic GetCharacteristic(Guid uid)
        {
            throw new NotImplementedException();
        }

        public List<Storage> GetStorages()
        {
            return db.GetAllWithChildren<Storage>().ToList();
        }

        public Storage GetStorage(Guid uid)
        {
            throw new NotImplementedException();
        }

        public List<Barcode> GetBarcodes()
        {
            return db.GetAllWithChildren<Barcode>().ToList();
        }

        public Barcode GetBarcode(Guid uid)
        {
            throw new NotImplementedException();
        }

        public Barcode GetBarcode(string code)
        {
            throw new NotImplementedException();
        }

        public Guid SaveNomenclature(Nomenclature nomenclature)
        {
            throw new NotImplementedException();
        }

        public Guid SaveCharacteristic(Characteristic characteristic)
        {
            throw new NotImplementedException();
        }

        public Guid SaveUnit(Unit unit)
        {
            throw new NotImplementedException();
        }

        public Guid SaveNomenclaturesKind(NomenclaturesKind nomenclaturesKind)
        {
            throw new NotImplementedException();
        }

        public Guid SaveStorage(Storage storage)
        {
            throw new NotImplementedException();
        }

        public List<Price> GetPrices()
        {
            throw new NotImplementedException();
        }

        public Price GetPrice(Nomenclature nomenclature, Characteristic characteristic)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //db.Close();
            //db = null;
        }

        public void CreateDb()
        {
            if (db != null)
            {
                db.CreateTable<Unit>();
                db.CreateTable<NomenclaturesKind>();
                db.CreateTable<Nomenclature>();
                db.CreateTable<Characteristic>();
                db.CreateTable<Barcode>();
                db.CreateTable<Price>();
                db.CreateTable<Storage>();

            }
        }
    }
}