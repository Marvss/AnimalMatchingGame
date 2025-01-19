namespace AnimalMatchingGame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void PlayAgainButton_Clicked(object sender, EventArgs e)
        {
            //Properties can be change inside the c# code and the xaml code
            AnimalButtons.IsVisible = true;
            PlayAgainButton.IsVisible = false;
            //A List is a collection that stores set of values in order
            List<string> animalEmoji = [
                "🐶", "🐱", "🐭", "🐹", "🐰", "🦊", "🐻", "🐼",
                "🐶", "🐱", "🐭", "🐹", "🐰", "🦊", "🐻", "🐼",
                ];
            //this is a foreach loop, it goes thru a collection and executes the set of statements for each item in the collection
            foreach (var button in AnimalButtons.Children.OfType<Button>())
            {
                int index = Random.Shared.Next(animalEmoji.Count); //Pick a random number between 0 and the number of emoji left in the list and call it index
                string nextEmoji = animalEmoji[index];//Use the random numberr called index to get a random emoji from te list
                button.Text = nextEmoji;//Make the button display the selected emoji
                animalEmoji.RemoveAt(index);//remove the chosen emoji from the list
            }
            Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
        }

        int tenthsOfSecondsElapsed = 0;
        private bool TimerTick()
        {
            if (!this.IsLoaded) return false;
            tenthsOfSecondsElapsed++;
            TimeElapsed.Text = "Time elapsed: " +
                (tenthsOfSecondsElapsed / 10F).ToString("0.0s");

            if(PlayAgainButton.IsVisible)
            {
                tenthsOfSecondsElapsed = 0;
                return false;
            }
            return true;
        }

        Button lastClicked;//keeps track of the last button clicked
        bool findingMatch = false;//keeps track of whether the player is trying to find a match or not
        int matchesFound;//keeps track of the number of matches found

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(sender is Button buttonClicked)
            {
                if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && (findingMatch == false))         
                {                                                                                      
                   buttonClicked.BackgroundColor = Colors.Red;                                         //These lines are run when the player clicks
                   lastClicked = buttonClicked;                                                       //the first button of a potential match to change the color
                   findingMatch = true;                                                               
               }                                                                                       
               else                                                                                    
               {                                                                                       
                   if((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text) &&
                        (!string.IsNullOrWhiteSpace(buttonClicked.Text)))      
                   {                                                                                   //This happens when the player click the second button in the pair
                       matchesFound++;                                                                 //if the animals match, it adds one to matchesFound and blanks out the animals
                       lastClicked.Text = " ";                                                         //on both buttons. it also resets the color of the first button back
                       buttonClicked.Text = " ";                                                       
                   }                                                                                   
                   lastClicked.BackgroundColor = Colors.LightBlue;                                     
                   buttonClicked.BackgroundColor = Colors.LightBlue;
                   findingMatch = false;
                }
            }
            if(matchesFound == 8)
            {
                matchesFound = 0;                   //this block of code runs when the player has found all the matches
                AnimalButtons.IsVisible = false;    //hides the flexgrid of the game and pops up the play again button
                PlayAgainButton.IsVisible = true;   
            }
        }
    }

}
