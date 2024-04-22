using Microsoft.Maui.Controls;
using System;

namespace CollectionManagementApp
{
    public partial class CollectionPage : ContentPage
    {
        private Collection _collection;
        private DataManager _dataManager;

        public CollectionPage(Collection collection, DataManager dataManager) 
        {
            InitializeComponent();
            _collection = collection;
            _dataManager = dataManager; 
            BindingContext = _collection;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            RefreshCollection(); 
        }

        private async void AddItemButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddItemPage(_collection, _dataManager)); 
        }

        private void CollectionItemsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is CollectionItem selectedItem)
            {
                Navigation.PushAsync(new EditItemPage(selectedItem));
            }
        }

        public void RefreshCollection()
        {
            CollectionItemsListView.ItemsSource = null;
            CollectionItemsListView.ItemsSource = _collection.Items;
        }
    }
}
