using System.Data;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;

IDictionary<string, int> game_vars = new Dictionary<string, int>();
game_vars.Add("num_buildings", 0);
game_vars.Add("score", 0);
game_vars.Add("coins", 16);

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

static T RandomBuidling<T>(List<T> list)
{
    var random = new Random();
    int index = random.Next(list.Count);
    return list[index];
}

string building1 = RandomBuidling(buildings);
string building2 = RandomBuidling(buildings);

void GameMenu()
{
    Console.Write("\nNumber Of Buildings: "+game_vars["num_buildings"]+"   ");
    Console.Write("Coins: "+game_vars["coins"]+"   ");
    Console.WriteLine("Score: "+game_vars["score"]+"   ");
    Console.Write("1.Building: " + building1 + "   ");
    Console.Write("2.Building: " + building2 + "   ");
    Console.WriteLine("0.Exit Game");
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


void InitialiseGame()
{
    game_vars["num_buildings"] = 0;
    game_vars["coins"] = 16;
    game_vars["score"] = 0;
}

void StartGame()
{
    DrawField(20,20);
    GameMenu();
}

void LoadSaved()
{

}

void Displayhighscore()
{

}

void SaveGame()
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
            InitialiseGame();
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
        Console.WriteLine("Please enter a number");
    }
}

while (true)
{
    DrawField(20,20);
    GameMenu();
    Console.Write("Please choose your option: ");
    try
    {
        int Choice = Convert.ToInt32(Console.ReadLine());
        if(Choice == 1)
        {
            Console.Write("Where would you like to place " + building1 + ": ");
            Console.ReadLine();
        }
        else if(Choice == 2)
        {
            Console.Write("Where would you like to place " + building2 + ": ");
            Console.ReadLine();
        }
        else if (Choice == 0)
        {
            Console.Write("Would you like to save your game? (y/n): ");
            string choice = Console.ReadLine();
            if (choice == "y")
            {
                SaveGame();
            }
            else if (choice == "n")
            {
                Console.WriteLine("Thanks for playing!");
                break;
            }
            else
            {
                Console.WriteLine("Please enter valid option");
            }
        }
        else
        {
            Console.WriteLine("Please enter a number");
        }
    }
    catch
    {
        Console.WriteLine("Please enter a number");
    }
}

