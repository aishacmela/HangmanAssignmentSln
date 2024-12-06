using System.Text;

namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage
{
	//Name to guess 
	private string secreteWord = "Current Guess";
	//trcak guessed letters
	private StringBuilder displayWord;
	//Allowed attempts
	private int attempts = 8;
	//list of guessed letters
	private List<char> guessedLetters;
	public HangmanGamePage()
	{
		InitializeComponent();

	}
}