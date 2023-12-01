using System.Runtime.CompilerServices;

void Menu()
{
    Console.WriteLine("------------------------");
    Console.WriteLine("1. Start a new game");
    Console.WriteLine("2. Load saved game");
    Console.WriteLine("3. Display high scores");
    Console.WriteLine("0. Exit game");
    Console.WriteLine("------------------------");
}

void StartGame()
{

}

void LoadSaved()
{

}

void Displayhighscore()
{

}

void ExitGame()
{

}

while (true)
{
    Menu();
    Console.Write("Select Option: ");
    try
    {
        int Choice = Convert.ToInt32(Console.ReadLine());

        if (Choice == 1)
        {
            StartGame();
        }
        else if (Choice == 2)
        {
            LoadSaved();
        }
        else if (Choice == 3)
        {
            Displayhighscore();
        }
        else if (Choice == 0)
        {
            ExitGame();
        }
        else
        {
            Console.WriteLine("This option is not available.");
        }
    }
    catch
    {
        Console.WriteLine("Option must be an integer.");
    }
}