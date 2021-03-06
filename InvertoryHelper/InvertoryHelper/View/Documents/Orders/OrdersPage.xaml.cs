﻿using System;
using InvertoryHelper.Model.Documents.Order;
using InvertoryHelper.ViewModel.Documents.Orders;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InvertoryHelper.View.Documents.Orders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public OrdersPage()
        {
            InitializeComponent();

            var vm = new OrdersViewModel();
            vm.Navigation = Navigation;
            BindingContext = vm;
        }

        private void OrdersPage_OnAppearing(object sender, EventArgs e)
        {
            var vm = BindingContext as OrdersViewModel;

            if (vm != null)
                MessagingCenter.Subscribe<Order>(vm, "SaveOrder", o => { vm.LoadOrders(); });
        }

        private void OrdersPage_OnDisappearing(object sender, EventArgs e)
        {
            var vm = BindingContext as OrdersViewModel;

            if (vm != null)
                MessagingCenter.Unsubscribe<Order>(vm, "SaveOrder");
        }
    }
}