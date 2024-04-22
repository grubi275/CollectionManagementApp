using Microsoft.Maui.Controls;
using System;

namespace CollectionManagementApp
{
    public partial class EditItemPage : ContentPage
    {
        private CollectionItem _item;
        private DataManager _dataManager;

        public EditItemPage(CollectionItem item)
        {
            InitializeComponent();
            _item = item;
            _dataManager = DataManager.GetInstance(); 
            BindingContext = _item;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            _item.Name = NameEntry.Text;
            _item.Description = DescriptionEntry.Text;

            
            _dataManager.SaveCollections();

            Navigation.PopAsync();
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            
            var collection = _dataManager.Collections.Find(c => c.Items.Contains(_item));
            collection.Items.Remove(_item);
            
            _dataManager.SaveCollections();
          
            Navigation.PopAsync();
        }
    }
}
