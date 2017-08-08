using System;
using System.Linq;
using InvertoryHelper.Model;
using Xamarin.Forms;

namespace InvertoryHelper.Common
{
    public class NomenclatureEntryValidation : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Completed += Bindable_Completed;
        }

        private void Bindable_Completed(object sender, EventArgs e)
        {
            var entry = (Entry) sender;

            if (entry.Text == string.Empty)
                return;

            var nomenclature = DataRepository.Instance
                .GetNomenclaturesAsync(n => n.Name.StartsWith(entry.Text, StringComparison.CurrentCultureIgnoreCase) ||
                                            n.Artikul != null && n.Artikul.Contains(entry.Text)).Result
                .FirstOrDefault();

            if (nomenclature != null)
                entry.Text = nomenclature.Name;
            else
                entry.Text = string.Empty;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Completed -= Bindable_Completed;
        }
    }
}