using System;
using System.Collections.Generic;

namespace codility
{
    public class RecipeBook
    {
        private readonly Dictionary<string, Action> _recipes;
        private readonly Func<string> _inputProvider;
        private readonly Action<string> _outputProvider;


        public RecipeBook(Func<string> inputProvider, Action<string> outputProvider){
            _inputProvider = inputProvider;
            _outputProvider = outputProvider;

            _recipes = new Dictionary<string, Action>
            {
                {"beer", ServerBeer},
                {"juice", ServeJuice}
            };
        }

        public void MakeDrink(string drinkName){
            _recipes[drinkName]();
        }

        public IEnumerable<string> GetAvailableDrinks(){
            return _recipes.Keys;
        }

        private void UnavailableDrink(string drink){
             _outputProvider("Not appropriate input");
        }

        private void ServeJuice(){
            _outputProvider("Here you go! Sweet juice.");
        }

        private void ServerBeer(){
            _outputProvider("Not so fast cowboy. How old are you?");
            if(!int.TryParse(_inputProvider(), out var age))
            {
                HandleInvalidAge();
                return;
            }

            HandleBeerAgeCheck(age);            
        }

        private void HandleInvalidAge(){
            _outputProvider("Could not parse the age provided");
        }

        private void HandleBeerAgeCheck(int age){
            if(age >= 18){
                _outputProvider("Here you go! Cold beer.");
                return;
            }

            _outputProvider("Sorry! you are not old enough to drink beer");
        }
    }
}