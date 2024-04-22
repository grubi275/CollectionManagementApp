using Microsoft.Maui.Controls;
using System;

namespace CollectionManagementApp
{
    public partial class NewCollectionPage : ContentPage
    {
        private DataManager _dataManager;

        public NewCollectionPage()
        {
            InitializeComponent();
            _dataManager = DataManager.GetInstance();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            string collectionName = CollectionNameEntry.Text;
            if (!string.IsNullOrWhiteSpace(collectionName))
            {
                Collection newCollection = new Collection { Name = collectionName, Items = new System.Collections.Generic.List<CollectionItem>() };
                _dataManager.Collections.Add(newCollection);
                _dataManager.SaveCollections();
               
                await Navigation.PopAsync();
            }
            
        }
    }
}
