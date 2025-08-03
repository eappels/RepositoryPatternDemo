using TestApp.ViewModels;

namespace TestApp.Views;

public partial class DemoView : ContentPage
{
    public DemoView()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((DemoViewModel)BindingContext).Read();
    }
}