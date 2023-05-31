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

        public ReactiveCommand<Unit, bool> ShowIngredientsCommand { get; }


        public MainWindowViewModel()
        {
           

            ShowIngredientsCommand = ReactiveCommand.Create(() => ShowIngredients = !ShowIngredients);

            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();

            Ingredients = new ObservableCollection<Ingredient>
    {
        new Ingredient { Name = "Vodka", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Wyborowa.jpg"))) },
        new Ingredient { Name = "Rum", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/rum.jpg"))) },
        new Ingredient { Name = "Mint", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Mint.png"))) },
        new Ingredient { Name = "Sugar", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Sugar.png"))) },
        new Ingredient { Name = "Lime juice", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Limonka.png")))},
        new Ingredient { Name = "Soda water" , Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Soda.png")))},
        new Ingredient { Name = "Tomato juice" },
        new Ingredient { Name = "Lemon juice", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Cytryna.png"))) },
        new Ingredient { Name = "Worcestershire sauce"},
        new Ingredient { Name = "Tabasco", Image = new Bitmap(assets.Open(new Uri("avares://DrinkingApp/Assets/Tabasco.png")))},
        // Add more ingredients...
    };

            // Initialize the list of available drinks
            AvailableDrinks = new ObservableCollection<Drink>();

            // Subscribe to property changed events for each ingredient
            foreach (var ingredient in Ingredients)
            {
                ingredient.PropertyChanged += Ingredient_PropertyChanged;
            }
        }

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

        private void Ingredient_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Ingredient.IsChecked))
            {
                // Recalculate the available drinks whenever an ingredient's IsChecked property changes
                RecalculateAvailableDrinks();
            }
        }

        



        private void RecalculateAvailableDrinks()
        {
            // Clear the current list of available drinks
            AvailableDrinks.Clear();

            // Get the list of selected ingredients
            var selectedIngredients = Ingredients.Where(i => i.IsChecked).ToList();

            // Check which drinks can be made with the selected ingredients
            foreach (var drink in AllDrinks)
            {
                if (drink.Ingredients.All(i => selectedIngredients.Any(si => si.Name == i)))
                {
                    // If all of the drink's ingredients are selected, add it to the available drinks
                    AvailableDrinks.Add(drink);
                }
            }
        }

        private readonly List<Drink> AllDrinks = new List<Drink>
        {
    new Drink { Name = "Mojito", Ingredients = new List<string> { "Rum", "Mint", "Sugar", "Lime juice", "Soda water" }, Description = "MOJITOS - are bubbly rum cocktails that taste minty-fresh, citrusy and a little sweet."},
    new Drink { Name = "Bloody Mary", Ingredients = new List<string> { "Vodka", "Tomato juice", "Lemon juice", "Worcestershire sauce", "Tabasco" }, Description = "BLOODY MARY - It's spicy, salty, and overall a savory flavor. It reminds me of vegetable soup and salsa, all rolled into one." },
    // Add more drinks...
};

    }

}