using TestApp.Views;

namespace TestApp;

public partial class App : Application
{

    public App(DemoView demoView)
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }
}