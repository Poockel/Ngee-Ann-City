using System.Data;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;

// Set game variables
IDictionary<string, int> game_vars = new Dictionary<string, int>();
game_vars.Add("num_buildings", 0);
game_vars.Add("coins", 16);
game_vars.Add("score", 0);
game_vars.Add("turn", 1);

List<char> buildings = new List<char>();
buildings.Add('R');
buildings.Add('C');
buildings.Add('I');
buildings.Add('O');
buildings.Add('*');

static char?[][] DrawField()
{
    char?[][] field = new char?[20][];

    // Initialize the grid with null values
    for (int i = 0; i < 20; i++)
    {
        field[i] = new char?[20];
        for (int j = 0; j < 20; j++)
        {
            field[i][j] = null;
        }
    }

    return field;
}

// DrawField(row,col)
// Draws a 20x20 field to place buildings
static void ShowField(char?[][] field)
{
    Console.Write("    ");
    for (int i = 1; i <= field[0].Length; i++)
    {
        Console.Write(String.Format("{0,4}",$"{i,3}"));
    }
    Console.WriteLine();

    for (int i = 0; i < field.Length; i++)
    {
        Console.Write((char)('A' + i) + "   ");

        for (int j = 0; j < field[i].Length; j++)
        {
            Console.Write("+---");
        }
        Console.WriteLine("+");

        Console.Write("    ");
        for (int j = 0; j < field[i].Length; j++)
        {
            if (field[i][j] == null)
            {
                Console.Write("|   ");
            }
            else
            {
                Console.Write($"| {field[i][j]} ");
            }
        }
        Console.WriteLine("|");
    }

    Console.Write("    ");
    for (int j = 0; j < field[0].Length; j++)
    {
        Console.Write("+---");
    }
    Console.WriteLine("+");
}

// RandomBuilding()
// Function to get a random building from the buidlings list
static T RandomBuidling<T>(List<T> list)
{
    var random = new Random();
    int index = random.Next(list.Count);
    return list[index];
}

char building1 = RandomBuidling(buildings);
char building2 = RandomBuidling(buildings);
char spawn1 = RandomBuidling(buildings);
char spawn2 = RandomBuidling(buildings);

void SpawnBuilding(char?[][] field)
{
    Random rnd = new Random();
    int pos1 = rnd.Next(0, 19);
    int pos2 = rnd.Next(0, 19);
    int pos3 = rnd.Next(0, 19);
    int pos4 = rnd.Next(0, 19);
    field[pos1][pos2] = spawn1;
    field[pos3][pos4] = spawn2;
    ShowField(field);
}

static void PlaceBuilding(string building, string pos)
{
    char[] bpos = pos.ToCharArray();
}


// GameMenu()
// Displays game menu
void GameMenu()
{
    Console.Write("\nNumber Of Buildings: "+game_vars["num_buildings"]+"   ");
    Console.Write("Coins: "+game_vars["coins"]+"   ");
    Console.Write("Score: "+game_vars["score"]+"   ");
    Console.WriteLine("Turn: " + game_vars["turn"]);
    Console.Write("1.Building: " + building1 + "   ");
    Console.Write("2.Building: " + building2 + "   ");
    Console.WriteLine("0.Exit Game");
    Console.WriteLine("Each building costs 1 coin");
}

// Menu()
// Displays start menu
void Menu()
{
    Console.WriteLine("------------------------");
    Console.WriteLine("1. Start a new game");
    Console.WriteLine("2. Load saved game");
    Console.WriteLine("3. Display high scores");
    Console.WriteLine("0. Exit game");
    Console.WriteLine("------------------------");
}

// InitialiseGame()
// Sets the default game variables for new game
void InitialiseGame()
{
    game_vars["num_buildings"] = 0;
    game_vars["coins"] = 16;
    game_vars["score"] = 0;
    game_vars["turn"] = 1;
}

// StartGame()
// Starts the game

void StartGame()
{
    char?[][]field = DrawField();
    SpawnBuilding(field);
    GameMenu();
}

void LoadSaved()
{
    try
    {
        string path = "saved_game.json"; // Path to the saved game file

        if (File.Exists(path))
        {
            string gameStateJson = File.ReadAllText(path); // Read the file content

            // Deserialize the JSON string back into the game_vars dictionary
            game_vars = JsonSerializer.Deserialize<Dictionary<string, int>>(gameStateJson);

            Console.WriteLine("Game loaded successfully!");
            StartGame();
        }
        else
        {
            Console.WriteLine("No saved game found.");
        }
    }
    catch (IOException e)
    {
        Console.WriteLine("Error loading the game: " + e.Message);
    }
}

void Displayhighscore()
{

}

void SaveGame()
{
    string path = "saved_game.json";

    try
    {
        // Serialize game state to JSON
        string gameStateJson = JsonSerializer.Serialize(game_vars);

        // Write the serialized game state to the file
        File.WriteAllText(path, gameStateJson);

        Console.WriteLine("Game saved successfully!");
    }
    catch (Exception e)
    {
        Console.WriteLine("Error saving the game: " + e.Message);
    }
}


//-------------------------------------------------------------------------------------
//                                    MAIN GAME
//-------------------------------------------------------------------------------------

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
        while(Choice == 1)
        {
            StartGame();
            Console.Write("Please choose your option: ");
            try
            {
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                {
                    Console.Write("Where would you like to place " + building1 + ": ");
                    Console.ReadLine();
                }
                else if (choice == 2)
                {
                    Console.Write("Where would you like to place " + building2 + ": ");
                    Console.ReadLine();
                }
                else if (choice == 0)
                {
                    Console.Write("Would you like to save your game? (y/n): ");
                    string option = Console.ReadLine();
                    if (option == "y")
                    {
                        SaveGame();
                        Console.WriteLine("Thanks for playing!");
                        break;
                    }
                    else if (option == "n")
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
    }
    catch
    {
        Console.WriteLine("Please enter a number");
    }
}

