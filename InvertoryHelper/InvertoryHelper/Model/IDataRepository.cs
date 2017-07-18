using System;
using System.Collections.Generic;

namespace InvertoryHelper.Model
{
    public interface IDataRepository : IDisposable
    {
        void CreateDb();

        List<Nomenclature> GetNomenclatures();

        Nomenclature GetNomenclature(Guid uid);

        List<Unit> GetUnits();

        Unit GetUnit(Guid uid);

        List<NomenclaturesKind> GetNomenclaturesKinds();

        NomenclaturesKind GetNomenclaturesKind(Guid uid);

        List<Characteristic> GetCharacteristics();

        Characteristic GetCharacteristic(Guid uid);

        List<Storage> GetStorages();

        Storage GetStorage(Guid uid);

        List<Barcode> GetBarcodes();

        Barcode GetBarcode(Guid uid);

        Barcode GetBarcode(string code);

        List<Price> GetPrices();

        Price GetPrice(Nomenclature nomenclature, Characteristic characteristic);

        Guid SaveNomenclature(Nomenclature nomenclature);

        Guid SaveCharacteristic(Characteristic characteristic);

        Guid SaveUnit(Unit unit);

        Guid SaveNomenclaturesKind(NomenclaturesKind nomenclaturesKind);

        Guid SaveStorage(Storage storage);
    }
}