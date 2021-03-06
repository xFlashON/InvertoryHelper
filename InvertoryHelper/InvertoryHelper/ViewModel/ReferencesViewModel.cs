﻿using System.Windows.Input;
using InvertoryHelper.Common;
using InvertoryHelper.View.Barcodes;
using InvertoryHelper.View.Characteristics;
using InvertoryHelper.View.NomenclatureKinds;
using InvertoryHelper.View.Nomenclatures;
using InvertoryHelper.View.Prices;
using InvertoryHelper.View.Units;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel
{
    public class ReferencesViewModel : BaseViewModel
    {
        public INavigation navigation;

        public ICommand OpenReference => new Command(OpenReferenceCmd);

        public ICommand LoadDictionariesCmd => new Command(async () =>
        {
            var result = await DependencyService.Get<IWebExchange>().GetData();

            if (result.Sucsess)
                MessagingCenter.Send("Loading complete", "DisplayAlert");
            else
                MessagingCenter.Send(result.Message, "DisplayAlert");
        });


        private async void OpenReferenceCmd(object param)
        {
            switch (param)
            {
                case "Nomenclatures":
                {
                    if (navigation != null)
                        await navigation?.PushAsync(new NomenclaturesPage());
                }
                    break;
                case "Units":
                {
                    if (navigation != null)
                        await navigation?.PushAsync(new UnitsPage());
                }

                    break;
                case "NomenclatureKinds":
                {
                    if (navigation != null)
                        await navigation?.PushAsync(new NomenclatureKindsPage());
                }
                    break;

                case "Characteristics":
                {
                    if (navigation != null)
                        await navigation?.PushAsync(new CharacteristicsPage());
                }

                    break;

                case "Barcodes":
                {
                    if (navigation != null)
                        await navigation?.PushAsync(new BarcodesPage());
                }

                    break;

                case "Prices":
                {
                    if (navigation != null)
                        await navigation?.PushAsync(new PricesPage());
                }

                    break;
            }
        }
    }
}