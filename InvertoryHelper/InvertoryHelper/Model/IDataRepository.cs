using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvertoryHelper.Model
{
    public interface IDataRepository
    {
        Task<List<Nomenclature>> GetNomenclaturesAsync(Func<Nomenclature, bool> e = null);
        Task<List<Unit>> GetUnitsAsync(Func<Unit, bool> e = null);

        Task<List<NomenclaturesKind>> GetNomenclatureKindsAsync(Func<NomenclaturesKind, bool> e = null);

        Task<List<Characteristic>> GetCharacteristicsAsync(Func<Characteristic, bool> e = null);

        Task<List<Barcode>> GetBarcodesAsync(Func<Barcode, bool> e = null);

        Task<List<Price>> GetPricesAsync(Func<Price, bool> e = null);

        Task<Guid> SaveNomenclatureAsync(Nomenclature nomenclature);

        Task<Guid> SaveUnitAsync(Unit unit);

        Task<Guid> SaveCharacteristicAsync(Characteristic characteristic);

        Task<Guid> SaveNomenclatureKindAsync(NomenclaturesKind nomenclatureKind);

        Task<Guid> SaveBarcodeAsync(Barcode barcode);

        Task<Guid> SavePriceAsync(Price price);
    }
}