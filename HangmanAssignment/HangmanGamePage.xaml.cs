using System.Text;

namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage
{
	private string secreteWord = "Current Guess";
	public StringBuilder displayWord;
	private int attempts = 8;
	private int attemptsLeft { get; set; }
	private List<char> guessedLetters;
	private List<string> wordList = new List<string> { "Certain", "doubt" };


	public HangmanGamePage()
	{
		InitializeComponent();
		startGame();
	}

	//start a new game
	private void startGame()
	{
		//select a a random word form the list
		Random random = new Random();
		secreteWord = wordList[random.Next(wordList.Count)];

		//Initialize game state 
		displayWord = new StringBuilder(new string('_', secreteWord.Length));
		attemptsLeft = attempts;
		guessedLetters = new List<char>();

		//Update ui to show the initial game state
		UpdateGameUi();
	}

	//updates the ui
	private void UpdateGameUi()
	{
		//update word display
		DisplayWordLabel.Text = string.Join(" ", displayWord.ToString().ToCharArray());

		//show number of attempts left
		DisplayAttemptsLabel.Text = $"Attempts left:{attemptsLeft}";

		//updated the hangman image based on the number of attempts left
		HangmanImage.Source = $"hang{attempts - attemptsLeft}.png";
	}

	private void Guess(char guessedLetter)
	{
		// Check if the letter has already been guessed
		if (guessedLetters.Contains(guessedLetter))
		{
			DisplayMessage("You already guessed that letter!");
			return; // Exit the method to prevent further processing
		}

		// Add the guessed letter to the list of guessed letters
		guessedLetters.Add(guessedLetter);

		// Check if the guessed letter exists in the secret word
		if (secreteWord.Contains(guessedLetter))
		{
			// Update the displayWord to reveal the correctly guessed letter
			for (int i = 0; i < secreteWord.Length; i++)
			{
				if (secreteWord[i] == guessedLetter)
				{
					displayWord[i] = guessedLetter; // Replace underscore with the guessed letter
				}
			}

			// Check if the word has been fully guessed
			if (!displayWord.ToString().Contains("_"))
			{
				DisplayMessage("Congratulations! You guessed the word!");
				ResetGame(); // Restart the game
				return; // Exit the method since the game is won
			}
		}
		else
		{
			// Decrease the number of attempts left if the guess was incorrect
			attemptsLeft--;

			// Check if there are no remaining attempts
			if (attemptsLeft == 0)
			{
				DisplayMessage($"Game Over! The word was {secreteWord}");
				ResetGame(); // Restart the game
				return; // Exit the method since the game is over
			}
		}

		// Update the UI after processing the guess
		UpdateGameUi();
	}

	//resert game
	private void ResetGame()
	{
		startGame();
	}


	//handle button click to process a guess 
	private void OnGuessButtonClicked(object sender, EventArgs e)
	{
		//get guessed letter from an imput field
		char guessedLetter = GuessInput.Text.Trim().ToLower()[0];

		//clear onput field
		GuessInput.Text = string.Empty;

		//call guess method to process the letter 
		Guess(guessedLetter);
	}



	private void DisplayMessage(string message)
	{
		// Displays a message to the user using a pop-up alert
		DisplayAlert("Info", message, "OK");
	}
}

	