# BlazorMvvm

A simple MVVM library for blazor

# Installation

Add the BlazorMvvm nuget to the project

In `startup.cs` add

````csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    services.AddBlazorMvvm();
    //...
}
````

# Usage

To use Blazor MVVM in a .razor page you first must create a ViewModel, then add that view model to the page

## Example

Adding a greeting page

**ViewModels/GreetingViewModel.cs**
````csharp
using BlazorMvvmExamples.BlazorMvvm;

public class GreetingViewModel : ViewModelBase
{
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _greeting;
    public string Greeting
    {
        get => _greeting;
        set => SetProperty(ref _greeting, value);
    }

    private readonly INavigationService _navigationService;

    public GreetingViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public void Greet()
    {
        Greeting = "Hello " + Name;
    }
}
````

**Pages/Greeting.razor**
````csharp
@page "/greeting"
@inherits MvvmComponentBase<GreetingViewModel>
<h1>Greeting example</h1>

<label>What is your name</label>
<input @bind="ViewModel.Name" />

<button @onclick="ViewModel.Greet">Greet</button>

<p>@ViewModel.Greeting</p>

@code {
    [Parameter]
    public string Name { get; set; }
}
````