using System.Data;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System;
using System. Collections.Generic;

IDictionary<string, int> game_vars = new Dictionary<string, int>();
game_vars.Add("num_buildings", 0);
game_vars.Add("points", 0);
game_vars.Add("coins", 0);

List<string> buildings = new List<string>();
buildings.Add("R");
buildings.Add("C");
buildings.Add("I");
buildings.Add("O");
buildings.Add("*");

void DrawField(int rows, int columns)
{
    char?[][] grid = new char?[rows][];
    for (int i = 0; i < rows; i++)
    {
        grid[i] = new char?[columns];
    }

    Console.Write("    ");
    for (int i = 1; i <= columns; i++)
    {
        Console.Write(String.Format("{0,4}",$"{i,3}"));
    }
    Console.WriteLine();

    for (int i = 0; i < rows; i++)
    {
        Console.Write("    ");

        for (int j = 0; j < columns; j++)
        {
            grid[i][j] = null;
            Console.Write("+---");
        }
        Console.WriteLine("+");

        Console.Write((char)('A' + i) + "   ");
        Console.Write("");
        for (int j = 0; j < columns; j++)
        {
            Console.Write("|   ");
        }
        Console.WriteLine("|");
    }

    Console.Write("    ");
    for (int j = 0; j < columns; j++)
    {
        Console.Write("+---");
    }
    Console.WriteLine("+");
}
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
    DrawField(20,20);
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

int i = 1;
char c = (char)i;
Console.WriteLine(c);

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
            break;
        }
        else if (Choice == 2)
        {
            LoadSaved();
            break;
        }
        else if (Choice == 3)
        {
            Displayhighscore();
        }
        else if (Choice == 0)
        {
            ExitGame();
            Console.WriteLine("Thanks for playing!");
            break;
        }
        else
        {
            Console.WriteLine("This option is not available.");
        }
    }
    catch
    {
        Console.WriteLine("Select the number beside the option please.");
    }
}

