using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.OS;
using Android.Util;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Model.Documents.Order;
using InvertoryHelper.View.Nomenclatures;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Documents.Orders
{
    public class OrderViewModel : ObservableObject
    {
        private Command _selectNomenclatureCommand;
        private OrderRowModel _selectedRow;

        public OrderRowModel SelectedRow
        {
            get => _selectedRow;
            set
            {
                _selectedRow = value;
                OnPropertyChanged("SelectedRow");
            }
        }

        public INavigation Navigation;

        public OrderModel Order { get; private set; }

        public OrderViewModel(OrderModel order = null)
        {
            Order = order ?? new OrderModel();
            MessagingCenter.Subscribe<Nomenclature>(this, "SelectedNomenclature", SelectedNomenclature);
        }

        public Command AddRowCommand => new Command(() =>
        {

            Order.OrderRows?.Add(new OrderRowModel());

        });

        public  Command SelectNomenclatureCommand => _selectNomenclatureCommand ?? (_selectNomenclatureCommand = new Command(
                                                         async (p) =>
                                                         {
                                                             SelectedRow = p as OrderRowModel;
                                                            await Navigation.PushAsync(new NomenclaturesPage(true));

                                                        }));

        private void SelectedNomenclature(Nomenclature nomenclature)
        {
            if (SelectedRow != null)
                SelectedRow.Nomenclature = nomenclature;
        }
    }
}
