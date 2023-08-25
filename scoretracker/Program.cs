using Newtonsoft.Json;

namespace scoretracker
{
    internal class Program
    {
        enum Menus
        {
            MAIN = 4,
            ADDCOURSE,
            ADDHOLES,
            VIEWCOURSES,
            VIEWSCORES,
            ADDSCORE
        }

        static int GetInt(string prompt) {
            int userVariable;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out userVariable))
            {
                Console.WriteLine("Error: Invalid entry.");
                Console.Write(prompt);
            }
            return userVariable;
        }

        static void PrintMenu(Menus type)
        {
            Console.Clear();
            Console.WriteLine(" Aaron's Golf tracker\n");
            switch (type)
            {
                case Menus.MAIN:
                    Console.WriteLine(" 0 - Exit");
                    Console.WriteLine(" 1 - Add a course");
                    Console.WriteLine(" 2 - View courses");
                    Console.WriteLine(" 3 - Add a score");
                    Console.WriteLine(" 4 - View past scores");
                    Console.WriteLine();
                    break;
                case Menus.ADDCOURSE:
                    break;
                case Menus.ADDHOLES:
                    break;
                case Menus.VIEWCOURSES:
                    Console.WriteLine(" 0 - Go back");
                    for (int i = 0; i < Courses.Count; i++)
                    {
                        Console.WriteLine($" {i + 1} - {Courses[i].Name} ({Courses[i].Section})");
                    }
                    break;
                case Menus.ADDSCORE:
                    Console.WriteLine(" 0 - Go back");
                    for (int i = 0; i < Courses.Count; ++i)
                    {
                        Console.WriteLine($" {i + 1} - {Courses[i].Name} ({Courses[i].Section})");
                    }
                    break;
                case Menus.VIEWSCORES:
                    break;
            }
        }

        static void MainMenu()
        {
            while (true)
            {
                PrintMenu(Menus.MAIN);
                int userChoice;

                Console.Write(" Enter an option: ");
                if (!int.TryParse(Console.ReadLine(), out userChoice))
                {
                    Console.WriteLine(" Error: Entry is not an integer.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }

                if (userChoice == 0) return;

                if (userChoice < 0 || userChoice > (int)Menus.MAIN)
                {
                    Console.WriteLine(" Error: Entry is not in the list.");
                    Thread.Sleep(1000);
                    Console.Clear();
                    continue;
                }

                switch (userChoice)
                {
                    case 1:
                        AddCourse();
                        break;
                    case 2:
                        ViewCourses();
                        break;
                    case 3:
                        AddScore();
                        break;
                    case 4:
                        ViewScores();
                        break;
                }
            }
        }

        static void AddCourse()
        {
            PrintMenu(Menus.ADDCOURSE);
            Console.Write(" Enter the name of the course: ");
            string courseName = Console.ReadLine() ?? Guid.NewGuid().ToString();

            Course newCourse = new Course(courseName);
            int numHoles = GetInt(" Enter the number of holes: ");
            newCourse.NumHoles = numHoles;

            string section;
            Console.Write(" Enter the section of the course played on: ");
            while ((section = Console.ReadLine() ?? "").Length <= 0)
            {
                Console.WriteLine(" Error: Invalid input.");
                Console.Write(" Enter the section of the course played on: ");
            }
            newCourse.Section = section;

            Console.WriteLine();
            Console.WriteLine(newCourse.ToString());
            Console.Write("\n Does this look correct? (y/n): ");
            string okay;
            while ((okay = Console.ReadLine()!).ToUpper() != "Y" && okay != "N")
            {
                Console.WriteLine(" Error: 'y' or 'n'");
                Console.Write(" Does this look correct? (y/n): ");
            }
            if (okay == "N")
                AddCourse();

            AddHoles(newCourse);
        }

        static void AddHoles(Course newCourse)
        {
            PrintMenu(Menus.ADDHOLES);
            for (int i = 0; i < newCourse.NumHoles; i++)
            {
                int par = GetInt($" Enter par for hole #{i + 1}: ");
                int distance = GetInt($" Enter distance for hole #{i + 1}: ");
                newCourse.AddHole(par, distance);
            }
            Courses.Add(newCourse);
        }

        static void ViewCourses()
        {
            while (true)
            {
                PrintMenu(Menus.VIEWCOURSES);
                Console.WriteLine();

                Console.Write(" Enter the course number to view details: ");
                int userChoice;
                while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 0 || userChoice > Courses.Count)
                {
                    Console.WriteLine(" Error: Invalid option selected.");
                    Console.Write(" Enter the course number to view details: ");
                }
                if (userChoice == 0) { return; }
                Console.WriteLine();

                Console.WriteLine(Courses[userChoice - 1].ToString());
                Console.WriteLine();
                Console.Write(" If you wish to delete this entry, type \"delete\", otherwise, leave blank: ");
                if (Console.ReadLine() == "delete")
                {
                    Courses.Remove(Courses[userChoice - 1]);
                }
            }
        }

        static void AddScore()
        {
            int userInt;
            string userString;
            DateOnly userDate;

            PrintMenu(Menus.ADDSCORE);
            Console.WriteLine();

            /* Populate the course */
            Console.Write(" Enter course played on: ");
            while (!int.TryParse(Console.ReadLine(), out userInt) || userInt < 0 || userInt > Courses.Count)
            {
                Console.WriteLine(" Error: Invalid option selected.");
                Console.Write(" Enter course played on: ");
            }
            if (userInt == 0) { return; }
            Console.WriteLine();

            Score newScore = new Score();
            newScore.CourseName = $"{Courses[userInt - 1].Name} ({Courses[userInt - 1].Section})";

            /* Populate the date */
            Console.Write(" Enter date played(yyyy-mm-dd): ");
            while (!DateOnly.TryParse(Console.ReadLine(), out userDate))
            {
                Console.WriteLine("Error: Could not parse date. Please try again.");
                Console.Write(" Enter date played(yyyy-mm-dd): ");
            }
            Console.WriteLine();
            newScore.Date = userDate.ToString();

            /* Populate the name of player */
            Console.Write(" Enter the name of the player: ");
            while ((userString = Console.ReadLine() ?? "").Length <= 0)
            {
                Console.WriteLine("Error: Invalid data entered.");
                Console.Write(" Enter the name of the player: ");
            }
            Console.WriteLine();
            newScore.Player = userString;

            /* Populate number of strokes */
            int courseNum = userInt - 1;
            userInt = GetInt(" Enter number of strokes: ");
            newScore.Strokes = userInt;
            newScore.TotalScore = newScore.Strokes - Courses[courseNum].Par;

            Scores.Add(newScore);

        }

        static void ViewScores()
        {
            PrintMenu(Menus.VIEWSCORES);
            Console.WriteLine(" {0, -4} {1, -10} {2, -20} {3, -25} {4, 10} {5, 10} {6, 8}",
                "#", "Date", "Player", "Course",
                "Par", "Strokes", "Score");
            for (int i = 0; i < Scores.Count; i++)
            {
                Console.WriteLine(" {0, -4} {1, -10} {2, -20} {3, -25} {4, 10} {5, 10} {6, 8}",
                    (i + 1), Scores[i].Date, Scores[i].Player, Scores[i].CourseName,
                    (Scores[i].Strokes - Scores[i].TotalScore), Scores[i].Strokes, Scores[i].TotalScore);
            }
            Console.WriteLine();
            Console.Write(" To delete a score, enter its #, otherwise, leave blank: ");
            int userChoice;
            if(int.TryParse(Console.ReadLine(), out userChoice) && userChoice > 0 && userChoice <= Scores.Count)
            {
                Scores.Remove(Scores[userChoice - 1]);
            }
        }

        static List<Course> ReadCourseFile(string path)
        {
            try
            {
                string? json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<Course>>(json) ?? new List<Course>();
            }
            catch (IOException e)
            {
                Console.WriteLine(" IO Error: " + e.Message);
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine(" Json Error: " + e.Message);
            }
            return new List<Course>();
        }

        static List<Score> ReadScoreFile(string path)
        {
            try
            {
                string? json = File.ReadAllText(path);
                return JsonConvert.DeserializeObject<List<Score>>(json) ?? new List<Score>();
            }
            catch (IOException e)
            {
                Console.WriteLine(" IO Error: " + e.Message);
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine(" Json Error: " + e.Message);
            }
            return new List<Score>();
        }

        static List<Course> Courses = new List<Course>();
        static List<Score> Scores = new List<Score>();

        static void Main(string[] args)
        {
            Courses = ReadCourseFile(@"..\..\..\Json\courses.json");
            Scores = ReadScoreFile(@"..\..\..\Json\scores.json");
            Scores.Sort((x, y) => -x.Date!.CompareTo(y.Date)); //sort scores based on date
            MainMenu();

            //save courses
            string coursesJson = JsonConvert.SerializeObject(Courses);
            string scoresJson = JsonConvert.SerializeObject(Scores);

            try
            {
                File.WriteAllText(@"..\..\..\Json\courses.json", coursesJson);
                File.WriteAllText(@"..\..\..\Json\scores.json", scoresJson);
            } catch (Exception ex)
            {
                Console.WriteLine("Error: Could not save data.\n" +  ex.Message);
            }
        }
    }
}