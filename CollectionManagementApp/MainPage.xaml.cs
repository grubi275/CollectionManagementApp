using Microsoft.Maui.Controls;
using System;

namespace CollectionManagementApp
{
    public partial class MainPage : ContentPage
    {
        private DataManager _dataManager;

        public MainPage()
        {
            InitializeComponent();
            _dataManager = DataManager.GetInstance(); 
            ShowCollections();
            ShowExistingCollections();
        }

        private async void AddCollectionButton_Clicked(object sender, EventArgs e)
        {
            
            string collectionName = await DisplayPromptAsync("New Collection", "Enter collection name:");
            if (!string.IsNullOrWhiteSpace(collectionName))
            {
                Collection newCollection = new Collection { Name = collectionName, Items = new System.Collections.Generic.List<CollectionItem>() };
                _dataManager.Collections.Add(newCollection);
                _dataManager.SaveCollections();
                ShowCollections();
                ShowExistingCollections();
            }
        }

        private async void CollectionItem_Clicked(object sender, EventArgs e)
        {
            
            var item = (TextCell)sender;
            var selectedCollection = _dataManager.Collections.Find(collection => collection.Name == item.Text);
            if (selectedCollection != null)
            {
                await Navigation.PushAsync(new CollectionPage(selectedCollection, _dataManager)); 
            }
        }

        private async void Collection_Tapped(object sender, EventArgs e)
        {
            
            var tappedLabel = (Label)sender;
            var selectedCollection = _dataManager.Collections.Find(collection => collection.Name == tappedLabel.Text);
            if (selectedCollection != null)
            {
                await Navigation.PushAsync(new CollectionPage(selectedCollection, _dataManager)); 
            }
        }

        private void ShowCollections()
        {
            CollectionListView.ItemsSource = null; 
            CollectionListView.ItemsSource = _dataManager.Collections; 
        }

        private void ShowExistingCollections()
        {
            ExistingCollectionsStackLayout.Children.Clear();

            foreach (var collection in _dataManager.Collections)
            {
                Label collectionLabel = new Label();
                collectionLabel.Text = collection.Name;
                collectionLabel.FontSize = 20;
                collectionLabel.GestureRecognizers.Add(new TapGestureRecognizer { Command = new Command(() => Collection_Tapped(collectionLabel, EventArgs.Empty)) });
                ExistingCollectionsStackLayout.Children.Add(collectionLabel);
            }
        }
    }
}
