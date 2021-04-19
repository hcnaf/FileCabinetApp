﻿using System;
using System.Globalization;
using System.Xml;

namespace FileCabinetApp
{
    /// <summary>
    /// Console app, which manages user data by user commands.
    /// </summary>
    public static class Program
    {
        private const string DeveloperName = "Anh Tuan Phan Tran";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static readonly CultureInfo Culture = new CultureInfo("ru-RU");

        private static readonly Tuple<string, Action<string>>[] Commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("stat", Stat),
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("list", List),
            new Tuple<string, Action<string>>("edit", Edit),
            new Tuple<string, Action<string>>("find", Find),
        };

        private static readonly FileCabinetService FileCabinetService = new FileCabinetService();

        private static readonly string[][] HelpMessages = new string[][]
        {
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "stat", "prints the number of records.", "The 'stat' command prints the number of records." },
            new string[] { "create", "creates record.", "The 'create' command creates record." },
            new string[] { "list", "prints all records.", "The 'list' command prints all records." },
            new string[] { "edit", "edits record.", "The 'list' command edits record." },
            new string[] { "find [firstname/lastname/dateofbirth] [value]", "finds record by firstname/lastname/dateofbirth.", "The 'find' command finds record by firstname/lastname/dateofbirth." },
        };

        private static bool isRunning = true;

        /// <summary>
        /// Entry point.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            do
            {
                Console.Write("> ");
                var inputs = Console.ReadLine().Split(' ', 2);
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                var index = Array.FindIndex(Commands, 0, Commands.Length, i => i.Item1.Equals(command, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    const int parametersIndex = 1;
                    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                    Commands[index].Item2(parameters);
                }
                else
                {
                    PrintMissedCommandInfo(command);
                }
            }
            while (isRunning);
        }

        private static void PrintMissedCommandInfo(string command)
        {
            Console.WriteLine($"There is no '{command}' command.");
            Console.WriteLine();
        }

        private static void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(HelpMessages, 0, HelpMessages.Length, i => string.Equals(i[Program.CommandHelpIndex], parameters, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(HelpMessages[index][Program.ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (var helpMessage in HelpMessages)
                {
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[Program.CommandHelpIndex], helpMessage[Program.DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }

        private static void Stat(string parameters)
        {
            var recordsCount = FileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static void Create(string parameters)
        {
            DataInput(out var firstName, out var lastName, out var dateOfBirth, out var balance, out var securityCharacter, out var securityNumber);

            Console.WriteLine($"Record #{FileCabinetService.CreateRecord(firstName, lastName, dateOfBirth, balance, securityCharacter, securityNumber)} is created");
        }

        private static void Edit(string parameters)
        {
            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine(), Culture);
            if (id < 1 || id > FileCabinetService.GetStat())
            {
                Console.WriteLine($"Record #{id} is not found.");
                return;
            }

            DataInput(out var firstName, out var lastName, out var dateOfBirth, out var balance, out var securityCharacter, out var securityNumber);

            FileCabinetService.EditRecord(id, firstName, lastName, dateOfBirth, balance, securityCharacter, securityNumber);
            Console.WriteLine($"Record #{id} was edited.");
        }

        private static void Find(string parameters)
        {
            string objectType = parameters.Split(' ')[0].ToLower(Culture);
            string objectValue = parameters.Split(' ')[1].Trim('\"').ToLower(Culture);
            if (objectType == "firstname")
            {
                foreach (var record in FileCabinetService.FindByFirstName(objectValue))
                {
                    Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth:yyyy-MMM-dd}, {record.Balance}, {record.SecurityCharacter}{record.SecurityNumber}");
                }
            }

            if (objectType == "lastname")
            {
                foreach (var record in FileCabinetService.FindByLastName(objectValue))
                {
                    Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth:yyyy-MMM-dd}, {record.Balance}, {record.SecurityCharacter}{record.SecurityNumber}");
                }
            }

            if (objectType == "dateofbirth")
            {
                string[] dayMonthYear = objectValue.Split('-');
                DateTime dateOfBirth = new DateTime(int.Parse(dayMonthYear[0], Culture), int.Parse(dayMonthYear[1], Culture), int.Parse(dayMonthYear[2], Culture));
                foreach (var record in FileCabinetService.FindByDateOfBirth(dateOfBirth))
                {
                    Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth:yyyy-MMM-dd}, {record.Balance}, {record.SecurityCharacter}{record.SecurityNumber}");
                }
            }

            return;
        }

        private static void List(string parameters)
        {
            foreach (var record in FileCabinetService.GetRecords())
            {
                Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth:yyyy-MMM-dd}, {record.Balance}, {record.SecurityCharacter}{record.SecurityNumber}");
            }
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static void DataInput(out string firstName, out string lastName, out DateTime dateOfBirth, out decimal balance, out char securityCharacter, out short securityNumber)
        {
            Console.Write("First name: ");
            firstName = Console.ReadLine();
            if (firstName is null)
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            if (firstName.Length < 2 || firstName.Length > 60 || firstName.Contains(' '))
            {
                throw new ArgumentException("Invalid first name.");
            }

            Console.Write("Last name: ");
            lastName = Console.ReadLine();
            if (lastName is null)
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            if (lastName.Length < 2 || lastName.Length > 60 || lastName.Contains(' '))
            {
                throw new ArgumentException("Invalid last name.");
            }

            Console.Write("Date of birth: ");
            string[] dayMonthYear = Console.ReadLine().Split('/');
            dateOfBirth = new DateTime(int.Parse(dayMonthYear[2], Culture), int.Parse(dayMonthYear[1], Culture), int.Parse(dayMonthYear[0], Culture));
            if (dateOfBirth > DateTime.Now || dateOfBirth < new DateTime(1950, 1, 1))
            {
                throw new ArgumentOutOfRangeException(nameof(dateOfBirth));
            }

            Console.Write("Balance: ");
            balance = decimal.Parse(Console.ReadLine(), Culture);

            Console.Write("Secure charecter: ");
            securityCharacter = Console.ReadLine()[0];
            if (securityCharacter == '\0')
            {
                throw new ArgumentException("Security charecter is empty.");
            }

            Console.Write("Secure number: ");
            securityNumber = short.Parse(Console.ReadLine(), Culture);
        }
    }
}