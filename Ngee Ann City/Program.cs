using System.Data;
using System.Runtime.CompilerServices;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

public class Program
{
    static char?[][] field;
    private static void Main(string[] args)
    {
        // Set game variables
        IDictionary<string, int> game_vars = new Dictionary<string, int>();
        game_vars.Add("num_buildings", 0);
        game_vars.Add("coins", 16);
        game_vars.Add("score", 0);
        game_vars.Add("turn", 1);

        IDictionary<string, char> buildings = new Dictionary<string, char>();
        buildings.Add("Residential",'R');
        buildings.Add("Commercial",'C');
        buildings.Add("Industry",'I');
        buildings.Add("Park",'O');
        buildings.Add("Road",'*');




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
                Console.Write(string.Format("{0,4}", $"{i,3}"));
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
        static KeyValuePair<TKey, TValue> RandomBuilding<TKey, TValue>(IDictionary<TKey,TValue> dict)
        {
            Random random = new Random();
            int index = random.Next(0, dict.Count);
            return dict.ElementAt(index);
        }


        KeyValuePair<string, char> building1 = RandomBuilding(buildings);
        KeyValuePair<string, char> building2 = RandomBuilding(buildings);
        KeyValuePair<string, char> spawn1 = RandomBuilding(buildings);
        KeyValuePair<string, char> spawn2 = RandomBuilding(buildings);

        void SpawnBuilding(char?[][] field)
        {
            Random rnd = new Random();
            int pos1 = rnd.Next(0, 19);
            int pos2 = rnd.Next(0, 19);
            int pos3 = rnd.Next(0, 19);
            int pos4 = rnd.Next(0, 19);
            field[pos1][pos2] = spawn1.Value;
            field[pos3][pos4] = spawn2.Value;
        }

        void PlaceBuilding(char building, string pos, char?[][] field)
        {
            char[] bpos = pos.ToCharArray();

            int row = bpos[0] - 'A';
            int col = int.Parse(pos.Substring(1)) - 1;

            if (row >= 0 && row < field.Length && col >= 0 && col < field[0].Length && field[row][col] == null)
            {
                field[row][col] = building;
                Console.WriteLine("Building placed at position " + pos);
                ShowField(field); // Display the field after placing the building
            }
            else
            {
                Console.WriteLine("Invalid position or position already occupied. Please choose another position.");
            }
        }


        // GameMenu()
        // Displays game menu
        void GameMenu()
        {
            Console.Write("\nNumber Of Buildings: " + game_vars["num_buildings"] + "   ");
            Console.Write("Coins: " + game_vars["coins"] + "   ");
            Console.Write("Score: " + game_vars["score"] + "   ");
            Console.WriteLine("Turn: " + game_vars["turn"]);
            Console.Write("1." + building1.Key + ": " + building1.Value + "   ");
            Console.Write("2." + building2.Key + ": " + building2.Value + "   ");
            Console.WriteLine("0.Exit Game");
            Console.WriteLine("Each building costs 1 coin");
        }

        // Menu()
        // Displays start menu
        void Menu()
        {
            Console.WriteLine("Welcome to Ngee Ann City!");
            Console.WriteLine("------------------------");
            Console.WriteLine("1. Start a new game");
            Console.WriteLine("2. Load saved game");
            Console.WriteLine("3. Display high scores");
            Console.WriteLine("0. Exit game");
            Console.WriteLine("------------------------");
        }

        // InitialiseGame()
        // Sets the default game variables for new game
        void InitialiseGame(IDictionary<string, int> game_vars)
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
            field = DrawField();
            SpawnBuilding(field);
            ShowField(field);
        }


        void LoadSaved()
        {
            try
            {
                string path = "saved_game.json"; // Path to the saved game file

                if (File.Exists(path))
                {
                    string gameJson = File.ReadAllText(path); // Read the file content

                    // Deserialize the JSON string back into the game_vars dictionary
                    var gamedata = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(gameJson);

                    char?[][] field = gamedata["Field"].ToObject<char?[][]>();
                    Dictionary<string, int> game_vars = gamedata["GameVar"].ToObject < Dictionary<string, int> > ();

                    Console.WriteLine("Game loaded successfully!");
                    InitialiseGame(game_vars);
                    ShowField(field);
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
            var gamedata = new
            {
                GameVar = game_vars,
                Field = field
            };

            try
            {
                // Serialize game state to JSON
                string gameJson = Newtonsoft.Json.JsonConvert.SerializeObject(gamedata);

                // Write the serialized game state to the file
                File.WriteAllText(path, gameJson);

                Console.WriteLine("Game saved successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error saving the game: " + e.Message);
            }
        }

        void ExitGame()
        {
            Console.WriteLine("Exiting the game. Goodbye!");
            Environment.Exit(0);
        }

        int Build()
        {
            //Console.WriteLine("1. Build");
            //Console.WriteLine("0. Exit to main menu");
            Console.Write("Please choose an option: ");
            while (true)
            {
                try
                {
                    //int OptionB = Convert.ToInt32(Console.ReadLine());
                    //return OptionB;
                }
                catch
                {
                    Console.WriteLine("Enter an integer.");
                }
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
                    StartGame();
                    GameMenu();
                    InitialiseGame(game_vars);
                }
                else if (Choice == 2)
                {
                    LoadSaved();
                    GameMenu();
                    InitialiseGame(game_vars);
                }
                else if (Choice == 3)
                {
                    Displayhighscore();
                }
                else if (Choice == 0)
                {
                    ExitGame();
                    break;
                }
                else
                {
                    Console.WriteLine("This option is not available.");
                }

                while (Choice == 1 ^ Choice == 2)
                {
                    try
                    {
                        int OptionB = Build();
                        if (OptionB == 1)
                        {
                            //Console.Write("Please choose your option: ");
                            try
                            {
                                int choice = Convert.ToInt32(Console.ReadLine());
                                if (choice == 1)
                                {
                                    Console.Write("Where would you like to place " + building1.Value + ": ");
                                    string position = Console.ReadLine();

                                    // Ensure field is initialized before calling PlaceBuilding
                                    if (field != null)
                                    {
                                        PlaceBuilding(building1.Value, position, field);
                                        GameMenu();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Field is not initialized. Cannot place the building.");
                                        GameMenu();
                                    }

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
                                        ExitGame();
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
                        else if (OptionB == 0)
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
                                ExitGame();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Please enter valid option");
                            }
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
            }
            catch
            {
                Console.WriteLine("Please enter a number");
            }
        }
    }
}