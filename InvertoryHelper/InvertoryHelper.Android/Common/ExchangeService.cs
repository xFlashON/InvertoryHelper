using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Android.Util;
using InvertoryHelper.Common;
using InvertoryHelper.Droid.Common;
using InvertoryHelper.Droid.ExchangeReference;
using InvertoryHelper.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExchangeService))]

namespace InvertoryHelper.Droid.Common
{
    public class ExchangeService : IWebExchange
    {
        private static bool isBusy;

        public async Task<ExchangeResult> GetData()
        {
            if (isBusy)
                return new ExchangeResult {Sucsess = false, Message = "Exchange in process"};


            var repo = DataRepository.Instance;

            if (repo.IsLoading)
                return new ExchangeResult {Sucsess = false, Message = "Database is busy"};

            try
            {
                isBusy = true;

                var url = string.Empty;
                var login = string.Empty;
                var pwd = string.Empty;
                var node = string.Empty;

                if (Application.Current.Properties.ContainsKey("ExchangeUrl"))
                    url = ((string) Application.Current.Properties["ExchangeUrl"]).Replace("\n", string.Empty);

                if (Application.Current.Properties.ContainsKey("Login"))
                    login = (string) Application.Current.Properties["Login"];

                if (Application.Current.Properties.ContainsKey("Password"))
                    pwd = (string) Application.Current.Properties["Password"];

                if (Application.Current.Properties.ContainsKey("NodeId"))
                    node = (string) Application.Current.Properties["NodeId"];

                var srv = new ExchangeERP_MobApp();

                srv.Url = url;

                srv.PreAuthenticate = true;
                srv.Credentials = new NetworkCredential(login, pwd);

                var units = srv.GetMeasures(node, false);

                foreach (var unit in units)
                {
                    var result = await repo.SaveUnitAsync(new Unit
                    {
                        Uid = new Guid(((ЕдиницаИзмерения) unit).Ссылка),
                        Name = ((ЕдиницаИзмерения) unit).Наименование
                    });

                    if (result == Guid.Empty)
                        throw new Exception("Error loading units");
                }

                srv.GetMeasures(node, true);

                var nomenklatureKinds = srv.GetNomenklaturesKind(node, false);

                foreach (var nomenklatureKind in nomenklatureKinds)
                {
                    var result = await repo.SaveNomenclatureKindAsync(new NomenclaturesKind
                    {
                        Uid = new Guid(nomenklatureKind.Ссылка),
                        Name = nomenklatureKind.Наименование
                    });

                    if (result == Guid.Empty)
                        throw new Exception("Error loading nomenklatures kinds");
                }

                srv.GetNomenklaturesKind(node, true);

                var characteristics = srv.GetCharacteristics(node, false);

                foreach (var characteristic in characteristics)
                {
                    NomenclaturesKind nomenclaturesKind = null;

                    if (!string.IsNullOrEmpty(characteristic.Владелец))
                        nomenclaturesKind = repo
                            .GetNomenclatureKindsAsync(u => u.Uid == new Guid(characteristic.Владелец))
                            .Result.FirstOrDefault();

                    var result = await repo.SaveCharacteristicAsync(new Characteristic
                    {
                        Uid = new Guid(characteristic.Ссылка),
                        Name = characteristic.Наименование,
                        NomenclaturesKind = nomenclaturesKind
                    });

                    if (result == Guid.Empty)
                        throw new Exception("Error loading characteristics");
                }

                srv.GetCharacteristics(node, true);

                var nomenclatures = srv.GetNomenklatures(node, false);
                var count = 0;


                foreach (var nomenclature in nomenclatures)
                {
                    //too long loading
                    if (count++ > 100)
                        break;

                    if (nomenclature.ЭтоГруппа)
                        continue;

                    Unit unit = null;
                    NomenclaturesKind nomenclaturesKind = null;

                    if (!string.IsNullOrEmpty(nomenclature.ЕдиницаИзмерения))
                        unit = repo.GetUnitsAsync(u => u.Uid == new Guid(nomenclature.ЕдиницаИзмерения))
                            .Result.FirstOrDefault();

                    if (!string.IsNullOrEmpty(nomenclature.ВидНоменклатуры))
                        nomenclaturesKind = repo
                            .GetNomenclatureKindsAsync(u => u.Uid == new Guid(nomenclature.ВидНоменклатуры))
                            .Result.FirstOrDefault();

                    var result = await repo.SaveNomenclatureAsync(new Nomenclature
                    {
                        Uid = new Guid(nomenclature.Ссылка),
                        Name = nomenclature.Наименование,
                        Artikul = nomenclature.Артикул,
                        NomenclaturesKind = nomenclaturesKind,
                        BaseUnit = unit
                    });

                    if (result == Guid.Empty)
                        throw new Exception("Error loading nomenclatures");
                }

                srv.GetNomenklatures(node, true);


                var storages = srv.GetStorages(node, false);

                var storageUid = string.Empty;

                var firstOrDefault = storages.FirstOrDefault(s => s.Наименование.Contains("Мал"));
                if (firstOrDefault != null)
                    storageUid = firstOrDefault.Ссылка;


                var prices = srv.GetPrices(node, storageUid, false);

                count = 0;

                foreach (var price in prices)
                {
                    Nomenclature nomenclature = null;
                    Characteristic characteristic = null;

                    if (!string.IsNullOrEmpty(price.Номенклатура))
                        nomenclature = repo.GetNomenclaturesAsync(u => u.Uid == new Guid(price.Номенклатура))
                            .Result.FirstOrDefault();

                    if (!string.IsNullOrEmpty(price.ХарактеристикаНоменклатуры))
                        characteristic = repo
                            .GetCharacteristicsAsync(u => u.Uid == new Guid(price.ХарактеристикаНоменклатуры))
                            .Result.FirstOrDefault();

                    if (nomenclature != null)
                    {
                        //too long loading
                        if (count++ > 100)
                            break;

                        var exRows = await repo.GetPricesAsync(p =>
                        {
                            return nomenclature.Equals(p.Nomenclature) &&
                                   (characteristic?.Equals(p.Characteristic) ?? p.Characteristic == null);
                        });

                        var exPrice = exRows.FirstOrDefault();

                        if (exPrice == null)
                            exPrice = new Price
                            {
                                Nomenclature = nomenclature,
                                Characteristic = characteristic,
                                price = price.Цена
                            };
                        else
                            exPrice.price = price.Цена;

                        var result = await repo.SavePriceAsync(exPrice);

                        if (result == Guid.Empty)
                            throw new Exception("Error loading prices");
                    }
                }

                srv.GetPrices(node, storageUid, true);

                var barcodes = srv.GetBarcodes(node, false);

                count = 0;

                foreach (var barcode in barcodes)
                {
                    Nomenclature nomenclature = null;
                    Characteristic characteristic = null;

                    if (!string.IsNullOrEmpty(barcode.Номенклатура))
                        nomenclature = repo.GetNomenclaturesAsync(u => u.Uid == new Guid(barcode.Номенклатура))
                            .Result.FirstOrDefault();

                    if (!string.IsNullOrEmpty(barcode.ХарактеристикаНоменклатуры))
                        characteristic = repo
                            .GetCharacteristicsAsync(u => u.Uid == new Guid(barcode.ХарактеристикаНоменклатуры))
                            .Result.FirstOrDefault();

                    if (nomenclature != null)
                    {
                        //too long loading
                        if (count++ > 100)
                            break;

                        var exRows = await repo.GetBarcodesAsync(p =>
                        {
                            return nomenclature.Equals(p.Nomenclature) &&
                                   (characteristic?.Equals(p.Characteristic) ?? p.Characteristic == null);
                        });

                        var exBarcode = exRows.FirstOrDefault();

                        if (exBarcode == null)
                            exBarcode = new Barcode
                            {
                                Nomenclature = nomenclature,
                                Characteristic = characteristic,
                                Code = barcode.Штрихкод
                            };
                        exBarcode.Code = barcode.Штрихкод;

                        var result = await repo.SaveBarcodeAsync(exBarcode);

                        if (result == Guid.Empty)
                            throw new Exception("Error loading barcodes");
                    }
                }

                srv.GetBarcodes(node, true);
            }
            catch (Exception ex)
            {
                Log.Error("Error", ex.Message);
                return new ExchangeResult {Sucsess = false, Message = ex.Message};
            }
            finally
            {
                isBusy = false;
            }

            return new ExchangeResult {Sucsess = true};
        }

        public async Task<ExchangeResult> SendData()
        {
            throw new NotImplementedException();
        }
    }
}