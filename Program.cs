/*using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;*/

namespace MovieLibrary
{
    class Program
    {
        public static void Main(string[] args)
        {
            //might need {Environment.CurrentDirectory}, what is that?
            string MovieListFile = $"{Environment.CurrentDirectory}/movies.csv";

            //exception handle existing file
            if (!File.Exists(MovieListFile))
            //might need to replace console with logger
            {
                ;
                Console.WriteLine("File does not exist : {FILE} ", MovieListFile);
            }
            else
            {
                //jump into menu
                string usersChoice;
                do
                {
                    //display menu
                    Console.WriteLine("1) Display Movies ");
                    Console.WriteLine("2) Add Movie");
                    Console.WriteLine("Enter to quit");

                    usersChoice = Console.ReadLine();
                    //might need to log
                    /*                    logger.LogInformation("User choice: {Choice}", choice);
                     *                    */
                    //make lists
                    List<long> MovieIds = new List<long>();
                    List<string> MovieTitles = new List<string>();
                    List<string> MovieGenres = new List<string>();

                    try
                    {
                        
                        StreamReader sr = new StreamReader(MovieListFile);
                        
                        //dont read header
                        sr.ReadLine();
                        //Console.WriteLine(sr);
                        while (!sr.EndOfStream)
                        {
                            string theMovieElements = sr.ReadLine();
                            int index = theMovieElements.IndexOf('"');
                            //Console.WriteLine(index);
                            //if no quote, then no comma
                            if (index == -1)
                            {
                                //split the movie elements by the comma into an array
                                string[] movieElements = theMovieElements.Split(',');
                                //add 1st array elemnt to movie id list
                                MovieIds.Add(long.Parse(movieElements[0]));
                                //add 2nd array elemnt to moive title list
                                MovieTitles.Add(movieElements[1]);
                                //add 3rd array elemnt to movie genre list
                                // replace "|" with ", "
                                MovieGenres.Add(movieElements[2].Replace("|", ", "));

                            }
                            //else statement uf title does have quotes
                            else
                            {
                                //extract movie title by deleting movie id and comma and genre
                                //first genre
                                MovieIds.Add(long.Parse(theMovieElements.Substring(0, index - 1)));
                                //second movie id and quote
                                theMovieElements = theMovieElements.Substring(index + 1);
                                index = theMovieElements.IndexOf('"');
                                //add movie title to movie title list
                                MovieTitles.Add(theMovieElements.Substring(0, index));
                                theMovieElements = theMovieElements.Substring(index + 2);
                                //add genre from the movie elements
                                MovieGenres.Add(theMovieElements.Replace("|", ","));

                            }
                        }
                        //close movie file when done
                        sr.Close();

                    }
                    catch (Exception ex)
                    {
                        //logger.LogError(ex.Message);
                    }
                    // logger.LogInformation("Movies in file {Count}", MovieIds.Count);

                    if (usersChoice == "1")
                    {
                        //list in hundreds

                        int movieCounter = 100;
                        for (int i = 0; i < movieCounter; i++)
                        {
                            Console.WriteLine($"Id: {MovieIds[i]}");
                            Console.WriteLine($"Title: {MovieTitles[i]}");
                            Console.WriteLine($"Genre(s): {MovieGenres[i]}");
                            Console.WriteLine();
                        }
                       /* var myMovies = MovieIds.Take(100);
                        //Ask for another hundred
                        Console.WriteLine("Do you want to display 100 more? (Y/N)");
                        string usersResponse = Console.ReadLine();
                        while (usersResponse.ToLower() == "y")
                        {
                            movieCounter += 100;
                            myMovies = MovieIds.Skip(movieCounter).Take(100);
                            Console.WriteLine(MovieIds.Take(100));
                            Console.WriteLine($"Id: {MovieIds}");
                            Console.WriteLine($"Title: {MovieTitles}");
                            Console.WriteLine($"Genre(s): {MovieGenres}");*//*
                            
                            Console.WriteLine("Do you want to display 100 more? (Y/N)");
                            usersResponse = Console.ReadLine();

                        }

                        movieCounter = 0;
*/

                    }
                    else if (usersChoice == "2")
                    {
                        // Add movie to list
                        // get user input for movie title
                        Console.WriteLine("Enter the movie title");
                        string usersMovieTitle = Console.ReadLine();
                        //make sure not duplicate, string match
                        if (MovieTitles.Contains(usersMovieTitle))
                        {
                            Console.WriteLine("Movie Title already exists.");
                            //logger.LogInformation("Duplicate movie title {Title}", movieTitle);

                        }
                        else
                        {
                            Console.WriteLine(MovieIds);
                            //make new movie element
                            long newMovieId = MovieIds.Max() + 1;
                            //get genres
                            List<string> genresList = new List<string>();
                            string myGenre;
                            do
                            {
                                Console.WriteLine("Enter the movie genres or type 'exit' when finsihed");
                                myGenre = Console.ReadLine();
                                //validate response
                                if (myGenre != "exit" && myGenre.Length > 0)
                                {
                                    genresList.Add(myGenre);
                                }
                               ;



                            } while (myGenre != "exit");
                            //handle response if no genres listed
                            if (genresList.Count == 0)
                            {
                                genresList.Add("N/A");
                            }
                            //join genres by | so when displayed it with split correctly
                            string joinGenres = string.Join("|", genresList);
                            usersMovieTitle = usersMovieTitle.IndexOf(',') != -1 ? $"\"{usersMovieTitle}\"" : usersMovieTitle;
                            Console.WriteLine($"{newMovieId}, {usersMovieTitle}, {joinGenres}");

                            StreamWriter sw = new StreamWriter(MovieListFile, true);
                            sw.WriteLine($"{newMovieId}, {usersMovieTitle}, {joinGenres}");
                            sw.Close();

                            MovieIds.Add(newMovieId);
                            MovieTitles.Add(usersMovieTitle);
                            MovieGenres.Add(joinGenres);

                        }


                    }


                } while (usersChoice == "1" || usersChoice == "2");
            }
        }
    }
}

