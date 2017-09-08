using System;
using Android.Util;
using InvertoryHelper.Resourses;
using InvertoryHelper.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.BehaviorsPack;

namespace InvertoryHelper.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var vm = new MainViewModel();
            vm.navigation = Navigation;
            BindingContext = vm;

            MessagingCenter.Subscribe<string>(this, "DisplayAlert", DisplayMessage);
        }

        private async void DisplayMessage(string message)
        {
            //try
            //{
                await DisplayAlert(Resource.Message, message, Resource.Close);
            //}
            //catch (Exception ex)
            //{
            //    Log.Error("Error", ex.Message);
            //}
            
        }
    }
}