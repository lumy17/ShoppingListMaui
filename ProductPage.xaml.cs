using Moldovan_Luminita_Lab7.Models;

namespace Moldovan_Luminita_Lab7;


public partial class ProductPage : ContentPage
{
	ShopList s1;
	public ProductPage(ShopList slist)
	{
		InitializeComponent();
		s1 = slist;
	}
	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		var product = (Product)BindingContext;
		await App.Database.SaveProductAsync(product);
		listView.ItemsSource = await App.Database.GetProductsAsync();
	}
	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		var product = (Product)BindingContext;
		await App.Database.DeleteProductAsync(product);
		listView.ItemsSource = await App.Database.GetProductsAsync();
	}
	protected override async void OnAppearing()
	{
		base.OnAppearing();
		listView.ItemsSource = await App.Database.GetProductsAsync();
	}
    async void OnAddButtonClicked(object sender, EventArgs e)
    {
        Product p;
        if (listView.SelectedItem != null)
        {
            p = listView.SelectedItem as Product;
            var lp = new ListProduct()
            {
                ShopListID = s1.ID,
                ProductID = p.ID
            };
            await App.Database.SaveListProductAsync(lp);
            p.ListProducts = new List<ListProduct> { lp };
            await Navigation.PopAsync();
        }
	}


}