﻿using System;
using InvertoryHelper.Common;
using InvertoryHelper.Model;
using InvertoryHelper.Resourses;
using Xamarin.Forms;

namespace InvertoryHelper.ViewModel.Units
{
    public class UnitItemViewModel : BaseViewModel
    {
        private readonly Unit unit;
        public INavigation Navigation;

        public UnitItemViewModel(UnitModel Unit = null)
        {
            unit = new Unit();

            if (Unit == null)
            {
                Title = Resource.AddUnit;
            }
            else
            {
                Title = Resource.EditUnit;
                unit.Uid = Unit.Uid;
                Name = Unit.Name;
            }
        }

        public string Name
        {
            get => unit.Name;
            set
            {
                unit.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public Command SaveButton => new Command(async () =>
        {
            if (unit != null)
            {
                var uid = await DataRepository.Instance.SaveUnitAsync(unit);

                if (uid == Guid.Empty)
                {
                    MessagingCenter.Send("Error! Unit is not saved!", "DisplayAlert");
                    return;
                }
            }

            MessagingCenter.Send(unit, "SaveUnit");
            await Navigation?.PopAsync();
        });

        public Command CancelButton => new Command(async () => { await Navigation?.PopAsync(); });
    }
}