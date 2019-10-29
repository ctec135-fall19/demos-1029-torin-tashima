using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToArray
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**** Fun with LINQ to objects ****\n");

            // define an array of strings
            string[] currentVideoGames = {"Morrowind", "Uncharted 2",
                "Fallout 3", "Dexter", "System Shock 2"};

            // desired query: Games that have a space in the title

            #region First let's try it ye olde fashioned way
            string[] result = QueryOverStringsLongHand(currentVideoGames);

            Console.WriteLine("Returned results from longhand version");
            foreach (string s in result)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();
            #endregion

            #region Let's try the same thing using LINQ
            List<string> listResult = QueryOverStrings(currentVideoGames);

            Console.WriteLine("Returned results from query method");
            foreach (string s in listResult)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();
            #endregion
        }

        #region ye olde fashioned way
        static string[] QueryOverStringsLongHand(string[] s)
        {
            string[] resultsWithSpaces = new string[s.Length];

            // find the results
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].Contains(" "))
                    resultsWithSpaces[i] = s[i];
            }

            // sort results
            Array.Sort(resultsWithSpaces);

            // print results
            Console.WriteLine("Immediate results from longhand version.");
            foreach (string s1 in resultsWithSpaces)
            {
                if (s1 != null)
                    Console.WriteLine("Item: {0}", s1);
            }

            // generate a return array
            // figure out size
            int count = 0;
            foreach (string s2 in resultsWithSpaces)
            {
                if (s2 != null)
                    count++;
            }

            // create output array
            string[] outputArray = new string[count];

            // populate output array
            count = 0;
            foreach (string s3 in resultsWithSpaces)
            {
                if (s3 != null)
                {
                    outputArray[count] = s3;
                    count++;
                }
            }

            return outputArray;
        }
        #endregion

        #region
        static List<string> QueryOverStrings(string[] inputArray)
        {
            // build query
            //IEnumerable<string> subset = from ...
            var subset = from game in inputArray
                         where game.Contains(" ")
                         orderby game
                         select game;

            // print results
            ReflectOverQueryResults(subset, "Query Expression");

            // print results
            Console.WriteLine("  Immediate results using LINQ query");
            foreach (var s in subset)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();

            // demonstrate reuse of query
            inputArray[0] = "some string";
            Console.WriteLine("  Immediate results using LINQ query after change to data");
            foreach (var s in subset)
            {
                Console.WriteLine("Item: {0}", s);
            }
            Console.WriteLine();

            // demonstrate returning results - immediate execution
            List<string> outputList = (from game in inputArray
                                       where game.Contains(" ")
                                       orderby game
                                       select game).ToList<string>();

            return outputList;
        }

        static void ReflectOverQueryResults(object resultSet, string queryType)
        {
            Console.WriteLine("*** query type: {0} ***", queryType);
            Console.WriteLine("resultSet is of type: {0}", resultSet.GetType().Name);
            Console.WriteLine("resultSet location: {0}", resultSet.GetType().Assembly.GetName().Name);
        }
        #endregion
    }
}
