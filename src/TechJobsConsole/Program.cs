﻿using System;
using System.Collections.Generic; //list,collections


namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)  
            //this method does not return any value, just contain some code, static: only one instance of method "Main" in memory
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options, "View Jobs by Search or List"
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options, view jobs by list options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices); //first choice, view jobs by search or list

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices); //view jobs by list options

                    if (columnChoice.Equals("all")) //if choose "all" in "List by", print all
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice); //print by List choices/column choices, namely: skill, employer, Location, position type

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        //Console.WriteLine("Search all fields not yet implemented.");
                        searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }
                    else
                    {
                       searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                       PrintJobs(searchResults);
                   }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            }

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:");//choiceHeader have 2 values: list or search,depend on user choose 0(search) or list(1) for the 1st question

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]); //choiceKeys are the column names??
                }

                string input = Console.ReadLine();
                choiceIdx = int.Parse(input);

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length)
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx];
        }

            private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        {
            //Console.WriteLine("PrintJobs is not implemented yet");

            if(someJobs.Count == 0)
            {
                Console.WriteLine("No Results Found.");
            }
            else
            {
                foreach(Dictionary<string, string> item in someJobs)
                {
                    Console.WriteLine("*****");
                    foreach(KeyValuePair<string, string> pair in item)
                    {
                        Console.WriteLine(pair.Key + ": " + pair.Value);
                    }
                    Console.WriteLine("*****\n");
                }
            }

        }
    }
}
