using Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using Avalonia.Platform;
using Avalonia.Media.Imaging;

namespace DrinkingApp.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        public ObservableCollection<Ingredient> Ingredients { get; }
        public ObservableCollection<Drink> AvailableDrinks { get; }

        private bool _showIngredients;
        public bool ShowIngredients
        {
            get { return _showIngredients; }
            set { this.RaiseAndSetIfChanged(ref _showIngredients, value); }
        }

        private bool _showWelcome;
        public bool ShowWelcome
        {
            get { return _showWelcome; }
            set { this.RaiseAndSetIfChanged(ref _showWelcome, value); }
        }

        public ReactiveCommand<Unit, Unit> ShowIngredientsCommand { get; }

        private Drink _selectedDrink;
        public Drink SelectedDrink
        {
            get { return _selectedDrink; }
            set
            {
                _selectedDrink = value;
                this.RaisePropertyChanged(nameof(SelectedDrink));
            }
        }

        public MainWindowViewModel()
        {
            ShowWelcome = true;

            ShowIngredientsCommand = ReactiveCommand.Create(() =>
            {
                ShowIngredients = !ShowIngredients;
                if (ShowIngredients)
                {
                    ShowWelcome = false;
                }
            });

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            Ingredients = new ObservableCollection<Ingredient>
            {
        new Ingredient { Name = "Vodka", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Wyborowa.jpg"))) },
        new Ingredient { Name = "Rum", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/rum.png"))) },
        new Ingredient { Name = "Mint", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Mint.png"))) },
        new Ingredient { Name = "Sugar", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Sugar.png"))) },
        new Ingredient { Name = "Lime juice", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Limonka.png")))},
        new Ingredient { Name = "Soda water" , Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Soda.png")))},
        new Ingredient { Name = "Tomato juice", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/tomato.png"))) },
        new Ingredient { Name = "Lemon juice", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Cytryna.png"))) },
        new Ingredient { Name = "Worcestershire sauce", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/sauce.png")))},
        new Ingredient { Name = "Tabasco", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Tabasco.png")))},
        new Ingredient { Name = "Raspberry syrup", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/raspberry.png")))},
        // Add more ingredients...
    };

            AvailableDrinks = new ObservableCollection<Drink>();

            foreach (var ingredient in Ingredients)
            {
                ingredient.PropertyChanged += Ingredient_PropertyChanged;
            }
        }

        private void Ingredient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Ingredient.IsChecked))
            {
                RecalculateAvailableDrinks();
            }
        }

        private void RecalculateAvailableDrinks()
        {
            AvailableDrinks.Clear();

            var selectedIngredients = Ingredients.Where(i => i.IsChecked).ToList();

            foreach (var drink in AllDrinks)
            {
                if (drink.Ingredients.All(i => selectedIngredients.Any(si => si.Name == i)))
                {
                    AvailableDrinks.Add(drink);
                }
            }
        }

        private readonly List<Drink> AllDrinks = new List<Drink>
        {
    new Drink { Name = "Mojito", Ingredients = new List<string> { "Rum", "Mint", "Sugar", "Lime juice", "Soda water" }, Description = "MOJITOS - are bubbly rum cocktails that taste minty-fresh, citrusy and a little sweet."},
    new Drink { Name = "Bloody Mary", Ingredients = new List<string> { "Vodka", "Tomato juice", "Lemon juice", "Worcestershire sauce", "Tabasco" }, Description = "BLOODY MARY - It's spicy, salty, and overall a savory flavor. It reminds me of vegetable soup and salsa, all rolled into one." },
    new Drink { Name = "Mad dog", Ingredients = new List<string> { "Vodka", "Tabasco", "Raspberry syrup"}, Description = "MAD DOG - is a Polish alcoholic drink consisting of a 1 cl shot of vodka, a shot of raspberry."}
    // Add more drinks...
};

    }

}
