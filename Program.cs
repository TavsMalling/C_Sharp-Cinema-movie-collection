using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //This enables me using the IO library to write to and read from external data sources.

namespace Assignment_50__Cinema
{
    class MovingPicture
    {

        /*This class contains all the general variables that all items made of this object will contain, it also contains get/set for all variables*/
        protected string type;
        protected string title;
        protected string description;
        protected string director;
        protected string country;
        protected int budget;
        protected string budgetCurrency;
        protected double budgetInDKK;
        protected int year;
        protected int score;
        protected int length;

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Director
        {
            get
            {
                return director;
            }
            set
            {
                director = value;
            }
        }
        public string Country
        {
            get
            {
                return country;
            }
            set
            {
                country = value;
            }
        }
        public int Budget
        {
            get
            {
                return budget;
            }
            set
            {
                budget = value;
            }
        }
        public string BudgetCurrency
        {
            get
            {
                return budgetCurrency;
            }
            set
            {
                budgetCurrency = value;
            }
        }
        public double BudgetInDKK
        {
            get
            {
                return budgetInDKK;
            }
            set
            {
                budgetInDKK = value;
            }
        }
        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                year = value;
            }
        }
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }
    }

    class Movie : MovingPicture
    {
        /*Movie is a child-class from MovingPicture, which it inherits from, so it contains everything that MovingPicture does but it also has 2 unigue variables*/
        private List<string> genre;
        private List<string> cast;

        public List<string> Genre
        {
            get
            {
                return genre;
            }
            set
            {
                genre = value;
            }
        }

        public List<string> Cast
        {
            get
            {
                return cast;
            }
            set
            {
                cast = value;
            }
        }


    }

    class Documentary : MovingPicture
    {
        /*Documentary is a child-class from MovingPicture, which it inherits from, so it contains everything that MovingPicture does but it also has a unigue variable*/
        private List<string> theme;

        public List<string> Theme
        {
            get
            {
                return theme;
            }
            set
            {
                theme = value;
            }
        }
    }

    class TxtManagement
    {
        private string filePath = @".MovieList.txt";
        private List<MovingPicture> movieList = new List<MovingPicture>();

        public List<MovingPicture> GetMovieList()
        {
            ConvertFromFile();
            return movieList;
        }

        private List<string> ReadFromFile()
        {
            /*I read everything from my .txt file and return every line into a list which gets returned to wherever it gets called from.*/
            List<string> movies = File.ReadAllLines(filePath).ToList();
            return movies;
        }
        private void ConvertFromFile()
        {
            /*I get a list of string which I split and then use the array positions to declare the value of variables from either an instance of my Movie or Documentary class and add 
             that instance to a list of MovingPicture Objects, since Movie and Documentary class are both technically MovingPicture Objects*/
            List<string> movies = ReadFromFile();


            foreach (string item in movies)
            {
                if (item != "")
                {
                    string[] fromFile = item.Split(", ");

                    if (fromFile[0] == "Movie")
                    {
                        Movie movieItem = new Movie();
                        movieItem.Type = fromFile[0];
                        movieItem.Title = fromFile[1];
                        movieItem.Description = fromFile[2];
                        movieItem.Director = fromFile[3];
                        movieItem.Country = fromFile[4];
                        movieItem.Budget = Convert.ToInt32(fromFile[5]);
                        movieItem.BudgetCurrency = fromFile[6];
                        movieItem.BudgetInDKK = Convert.ToDouble(fromFile[7]);
                        movieItem.Year = Convert.ToInt32(fromFile[8]);
                        movieItem.Score = Convert.ToInt32(fromFile[9]);
                        movieItem.Length = Convert.ToInt32(fromFile[10]);
                        movieItem.Cast = fromFile[12].Split(';').ToList();
                        movieItem.Genre = fromFile[13].Split(';').ToList();
                        movieList.Add(movieItem);
                    }
                    else if (fromFile[0] == "Documentary")
                    {
                        Documentary movieItem = new Documentary();
                        movieItem.Type = fromFile[0];
                        movieItem.Title = fromFile[1];
                        movieItem.Description = fromFile[2];
                        movieItem.Director = fromFile[3];
                        movieItem.Country = fromFile[4];
                        movieItem.Budget = Convert.ToInt32(fromFile[5]);
                        movieItem.BudgetCurrency = fromFile[6];
                        movieItem.BudgetInDKK = Convert.ToDouble(fromFile[7]);
                        movieItem.Year = Convert.ToInt32(fromFile[8]);
                        movieItem.Score = Convert.ToInt32(fromFile[9]);
                        movieItem.Length = Convert.ToInt32(fromFile[10]);
                        movieItem.Theme = fromFile[11].Split(';').ToList();
                        movieList.Add(movieItem);
                    }
                }

            }

        }

        private List<string> ConvertToFile()
        {

            /*This method collects all info needed to fill in the variables needed to make a new MovingPicture object of either possible subclasses.
             There's also some input correction to make sure it only recieves valid data from the user.*/
            List<string> newMovie = new List<string>();
            string choice = "new";

            string[] currencyOption = { "USD", "DKK", "EUR", "GBP", "CNY", "RUB" };
            string movieChoiceHolder = "";
            bool validInput = true;
            do
            {
               
                do
                {
                    Console.Clear();
                    movieChoiceHolder = UserInput("What type of movie is it? Documentary or movie").ToLower();
                    validInput = true;
                    if (movieChoiceHolder != "movie" && movieChoiceHolder != "documentary")
                    {
                        Console.Clear();
                        Console.WriteLine("You can only choose between movie and documentary");
                        validInput = false;
                        Console.ReadKey();
                    }

                } while (validInput == false);

                string movieChoice = movieChoiceHolder.Substring(0, 1).ToUpper() + movieChoiceHolder.Substring(1);
                string title = UserInput("What is the title?");
                string description = UserInput("Write a short description");
                string director = UserInput("Who is the director?");
                string country = UserInput("Where was it made?");
                int budget = 0;
                do
                {
                    Console.Clear();
                    validInput = int.TryParse(UserInput("What was the budget?").Replace(".", ""), out budget);
                    if (validInput == false)
                    {
                        Console.Clear();
                        Console.WriteLine("Please write an integer!");
                        Console.ReadKey();
                    }

                } while (validInput != true);

                string budgetCurrency = "";
                do
                {
                    Console.Clear();
                    budgetCurrency = UserInput("In which currency? USD, DKK, EUR, GBP, CNY, RUB").ToUpper();

                    if (currencyOption.Contains(budgetCurrency) == false)
                    {
                        Console.Clear();
                        validInput = false;
                        Console.WriteLine("Choose between the given currencies!");
                        Console.ReadKey();
                    }
                } while (validInput == false);

                double budgetInDKK = Math.Round(CalcBudgetDKK(budget, budgetCurrency), 2);
                string year = UserInput("What year?");
                string score = UserInput("How would you rate it? 1 being the worst and 5 the best");
                string length = UserInput("How long is it? In minutes");
                string theme = "";
                string cast = "";
                string genre = "";

                if (movieChoice == "Documentary")
                {
                    theme = UserInput("What are the themes? Seperate them by comma and space ', '").Replace(", ", ";");
                }
                else if (movieChoice == "Movie")
                {

                    genre = UserInput("What are the genres? Seperate them by commas and a space ', '").Replace(", ", ";");
                    cast = UserInput("Write the cast, seperated by a comma and a space ', '").Replace(", ", ";");
                }
                newMovie.Add($"{movieChoice}, {title}, {description}, {director}, {country}, {budget}, {budgetCurrency}, {budgetInDKK}, {year}, {score}, {length}, {theme}, {cast}, {genre}");

                choice = UserInput("To exit, type anything and to enter another movie type \"new\"").ToLower();

            } while (choice == "new");

            return newMovie;

        }
        public string UserInput(string Questiontext)
        {
            /*Gets a string, prints the string and returns the users input*/
            Console.WriteLine(Questiontext);
            string answer = Console.ReadLine();
            Console.Clear();
            return answer;
        }
        private double CalcBudgetDKK(int budget, string currency)
        {
            /*Calculates the budget in Danish Crowns from the 5 currency options*/
            double value = 0;
            if (currency == "USD")
            {
                value = budget * 6.91;
            }
            else if (currency == "EUR")
            {
                value = budget * 7.47;
            }
            else if (currency == "GBP")
            {
                value = budget * 8.92;
            }
            else if (currency == "CNY")
            {
                value = budget / 0.98;
            }
            else if (currency == "RUB")
            {
                value = budget * 9.32;
            }

            return value;
        }
        public void WriteToFile()
        {
            /*Writes all strings from a list to a .txt document*/
            Console.Clear();
            List<string> movies = ReadFromFile();
            List<string> newMovie = ConvertToFile();
            foreach (string item in newMovie)
            {
                movies.Add(item);
            }

            File.WriteAllLines(filePath, movies);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void PrintMovies(List<MovingPicture> movieList, string selection)
        {
            /*Prints a list of MovingPictures while using a selection to decide which ones to print*/
            Console.Clear();

            int i = 1;

            if(movieList.Count() == 0)
            {
                Console.WriteLine("There was nothing matching your search in our database");

            }
            foreach (MovingPicture item in movieList)
            {
                if ((item.Type.ToLower() == "movie") && (selection == "movie" || selection == "all"))
                {
                    Console.WriteLine($"{i} -> Title: {item.Title}, Description: {item.Description}, Director: {item.Director}, Country: {item.Country}, Budget: {item.BudgetInDKK} DKK, Year: {item.Year}, Score: {item.Score}/5, Length: {item.Length} min, Cast: {ConvertFromList(((Movie)item).Cast)}, Genre: {ConvertFromList(((Movie)item).Genre)}\n");
                    i++;
                }
                else if ((item.Type.ToLower() == "documentary") && (selection == "documentary" || selection == "all"))
                {
                    Console.WriteLine($"{i} -> Title: {item.Title}, Description: {item.Description}, Director: {item.Director}, Country: {item.Country}, Budget: {item.BudgetInDKK} DKK, Year: {item.Year}, Score: {item.Score}/5, Length: {item.Length} min, Theme: {ConvertFromList(((Documentary)item).Theme)}\n");
                    i++;
                }
                
            }
        }

        static string ConvertFromList(List<string> itemList)
        {
            /*Turns a given list<string> into a single string seperated by ", "*/
            int i = 0;
            string returnString = "";
            foreach (string item in itemList) 
            {
                if(i == itemList.Count - 1)
                {
                    returnString += item;
                }
                else
                {
                    returnString += item + ", ";
                }
                i++;
            }
            return returnString;
        }

        static void SearchFunction()
        {
            /*Gets a list of MovingPicture and checks if the search query is contained inside that object for each item in the list and then adds that object to another list if true
             which is then printed with PrintMovies() to create a search results page.*/

            TxtManagement txt = new TxtManagement();
            List<MovingPicture> movieList = txt.GetMovieList();
            List<MovingPicture> movieResults = new List<MovingPicture>();

            Console.Clear();
            string[] options = new string[] { "title", "genre", "theme", "director", "actor", "length", "short-movie" };
            string searchChoice = "";
            string searchQuery = "";

            bool validChoice = true;
            do
            {
                Console.Clear();
                searchChoice = txt.UserInput("What do you want to search for? Title, Genre, Theme, Director, Actor, Length, Short-movie").ToLower();
                if (options.Contains(searchChoice) == false)
                {
                    Console.Clear();
                    Console.WriteLine("You can only use the given categories. Press 'e' to exit or any other key to continue");
                    char exitChar = Console.ReadKey().KeyChar;
                    switch (exitChar)
                    {
                        case 'e':
                            MainMenu();
                            break;

                        default:
                            validChoice = false;
                            break;
                    }
                }
            } while (validChoice == false);
            
            
            if (searchChoice != "short-movie")
            {
                searchQuery = txt.UserInput("Type your search query").ToLower();
            }
            
            
            if (searchChoice == "genre")
            {
                
                foreach (MovingPicture item in movieList)
                {
                    if(item.Type == "Movie")
                    {
                        if (ConvertFromList(((Movie)item).Genre).ToLower().Contains(searchQuery) == true) /*Here I cast the MovingPicture object to a Movie object knowing that only Movie objects contain the 
                            type "Movie"*/
                        {
                            movieResults.Add(item);
                        }
                    } 
                }
            }
            else if (searchChoice == "actor")
            {

                foreach (MovingPicture item in movieList)
                {
                    if(item.Type == "Movie")
                    {
                        if (ConvertFromList(((Movie)item).Cast).ToLower().Contains(searchQuery) == true)
                        {
                            movieResults.Add(item);
                        }
                    }
                    
                }
            }
            else if (searchChoice == "theme")
            {

                foreach (MovingPicture item in movieList)
                {
                    if(item.Type == "Documentary")
                    {
                        if (ConvertFromList(((Documentary)item).Theme).ToLower().Contains(searchQuery) == true)
                        {
                            movieResults.Add(item);
                        }
                    }
                    
                }
            }
            else if (searchChoice == "director")
            {

                foreach (MovingPicture item in movieList)
                {
                    if (item.Director.ToLower().Contains(searchQuery))
                    {
                        movieResults.Add(item);
                    }
                }
            }
            else if (searchChoice == "length")
            {
                double searchLength = Convert.ToDouble(searchQuery);
                foreach (MovingPicture item in movieList)
                {
                    if (item.Length == searchLength || ((item.Length >= searchLength - 0.25 * searchLength) && (item.Length <= searchLength + 0.25 * searchLength)))
                    {
                        movieResults.Add(item);
                    }
                }
            }
            else if (searchChoice == "title")
            { 
                foreach (MovingPicture item in movieList)
                {
                    if (item.Title.ToLower().Contains(searchQuery) == true)
                    {
                        movieResults.Add(item);
                    }
                }
            }
            else if(searchChoice == "short-movie")
            {
                foreach (MovingPicture item in movieList)
                {
                    if (item.Length <= 60)
                    {
                        movieResults.Add(item);
                    }
                }
            }

            PrintMovies(movieResults, "all");



        }

        static void MainMenu()
        {
            /*This is a menu*/
            TxtManagement txt = new TxtManagement();
            bool exit = false;
            List<MovingPicture> movieList = txt.GetMovieList();
            do
            {
                Console.Clear();
                Console.WriteLine("1 -> See list of movies");
                Console.WriteLine("2 -> See list of documentaries");
                Console.WriteLine("3 -> See all content");
                Console.WriteLine("4 -> Search");
                Console.WriteLine("5 -> Add Content");
                Console.WriteLine("6 -> Exit");

                bool boolChoice = int.TryParse(Convert.ToString(Console.ReadKey().KeyChar), out int intChoice);
                switch (intChoice)
                {
                    case 1:
                        PrintMovies(movieList, "movie");
                        Console.ReadKey();
                        break;
                    case 2:
                        PrintMovies(movieList, "documentary");
                        
                        Console.ReadKey();
                        break;
                    case 3:
                        PrintMovies(movieList, "all");
                        Console.ReadKey();
                        break;
                    case 4:
                        SearchFunction();
                        Console.ReadKey();
                        break;
                    case 5:
                        txt.WriteToFile();
                        MainMenu();
                        break;
                    case 6:
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Choose an option and try again");
                        Console.ReadKey();
                        break;
                }
            } while (exit == false); 
        }
    }
}

 