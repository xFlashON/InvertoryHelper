using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvertoryHelper.Model
{
    public interface IDataRepository
    {
        Task<List<Nomenclature>> GetNomenclaturesAsync(Func<Nomenclature, bool> e = null);
        Task<List<Unit>> GetUnitsAsync(Func<Unit, bool> e = null);

        Task<Guid> SaveNomenclatureAsync(Nomenclature nomenclature);
 

    }
}