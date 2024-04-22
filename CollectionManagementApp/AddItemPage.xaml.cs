using Microsoft.Maui.Controls;
using System;

namespace CollectionManagementApp
{
    public partial class AddItemPage : ContentPage
    {
        private Collection _collection;
        private DataManager _dataManager;

        public AddItemPage(Collection collection, DataManager dataManager) 
        {
            InitializeComponent();
            _collection = collection;
            _dataManager = dataManager; 
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            string name = NameEntry.Text;
            string description = DescriptionEntry.Text;

            if (!string.IsNullOrWhiteSpace(name))
            {
                CollectionItem newItem = new CollectionItem { Name = name, Description = description };
                _collection.Items.Add(newItem);

                
                _dataManager.SaveCollections();

                Navigation.PopAsync();
            }
           
        }

       
    }
}
