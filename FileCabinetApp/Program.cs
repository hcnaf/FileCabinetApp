using System;
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

        private static IFileCabinetService fileCabinetService;

        private static bool isRunning = true;

        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">Launch parameters.</param>
        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
#pragma warning disable CA1062 // Main parameters cannot be null.
            if (args.Length == 0)
            {
                fileCabinetService = new FileCabinetService(new DefaultValidator());
                Console.WriteLine("Using default validation rules.");
            }
            else if (args.Length == 1)
            {
                StartProgramWithParameters(args[0].Split('='));
            }
            else
            {
                StartProgramWithParameters(args);
            }

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

        private static void StartProgramWithParameters(string[] args)
        {
            if (args[0].Equals("-v", StringComparison.InvariantCultureIgnoreCase) || args[0].Equals("--validation-rules", StringComparison.InvariantCultureIgnoreCase))
            {
                if (args[1].Equals("custom", StringComparison.InvariantCultureIgnoreCase))
                {
                    fileCabinetService = new FileCabinetService(new CustomValidator());
                    Console.WriteLine("Using custom validation rules.");
                }
                else
                {
                    fileCabinetService = new FileCabinetService(new DefaultValidator());
                    Console.WriteLine("Using default validation rules.");
                }
            }
            else
            {
                fileCabinetService = new FileCabinetService(new DefaultValidator());
                Console.WriteLine("Using default validation rules.");
            }
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
            var recordsCount = fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static void Create(string parameters)
        {
            Console.WriteLine($"Record #{fileCabinetService.CreateRecord(DataInput())} is created");
        }

        private static void Edit(string parameters)
        {
            Console.Write("Id: ");
            int id = int.Parse(Console.ReadLine(), Culture);
            if (id < 1 || id > fileCabinetService.GetStat())
            {
                Console.WriteLine($"Record #{id} is not found.");
                return;
            }

            fileCabinetService.EditRecord(id, DataInput());
            Console.WriteLine($"Record #{id} was edited.");
        }

        private static void Find(string parameters)
        {
            string objectType = parameters.Split(' ')[0].ToLower(Culture);
            string objectValue = parameters.Split(' ')[1].Trim('\"').ToLower(Culture);
            if (objectType == "firstname")
            {
                foreach (var record in fileCabinetService.FindByFirstName(objectValue))
                {
                    Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth:yyyy-MMM-dd}, {record.Balance}, {record.SecurityCharacter}{record.SecurityNumber}");
                }
            }

            if (objectType == "lastname")
            {
                foreach (var record in fileCabinetService.FindByLastName(objectValue))
                {
                    Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth:yyyy-MMM-dd}, {record.Balance}, {record.SecurityCharacter}{record.SecurityNumber}");
                }
            }

            if (objectType == "dateofbirth")
            {
                string[] dayMonthYear = objectValue.Split('-');
                DateTime dateOfBirth = new DateTime(int.Parse(dayMonthYear[0], Culture), int.Parse(dayMonthYear[1], Culture), int.Parse(dayMonthYear[2], Culture));
                foreach (var record in fileCabinetService.FindByDateOfBirth(dateOfBirth))
                {
                    Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth:yyyy-MMM-dd}, {record.Balance}, {record.SecurityCharacter}{record.SecurityNumber}");
                }
            }

            return;
        }

        private static void List(string parameters)
        {
            foreach (var record in fileCabinetService.GetRecords())
            {
                Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth:yyyy-MMM-dd}, {record.Balance}, {record.SecurityCharacter}{record.SecurityNumber}, {record.PaymentSystem}, {record.Residency}, {record.CountryCode}.");
            }
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static FileCabinetRecord DataInput()
        {
            var record = new FileCabinetRecord();

            Console.Write("First name: ");
            record.FirstName = ReadInput(
                new Func<string, Tuple<bool, string, string>>(StringConverter),
                new Func<string, Tuple<bool, string>>(FirstNameValidator));

            Console.Write("Last name: ");
            record.LastName = ReadInput(
                new Func<string, Tuple<bool, string, string>>(StringConverter),
                new Func<string, Tuple<bool, string>>(LastNameValidator));

            Console.Write("Date of birth: ");
            record.DateOfBirth = ReadInput(
                new Func<string, Tuple<bool, string, DateTime>>(DateConverter),
                new Func<DateTime, Tuple<bool, string>>(DateOfBirthValidator));

            Console.Write("Balance: ");
            record.Balance = ReadInput(
                new Func<string, Tuple<bool, string, decimal>>(DecimalConverter),
                new Func<decimal, Tuple<bool, string>>(BalanceValidator));

            Console.Write("Secure charecter: ");
            record.SecurityCharacter = ReadInput(
                new Func<string, Tuple<bool, string, char>>(CharConverter),
                new Func<char, Tuple<bool, string>>(SecurityCharecterValidator));

            Console.Write("Secure number: ");
            record.SecurityNumber = ReadInput(
                new Func<string, Tuple<bool, string, short>>(ShortConverter),
                new Func<short, Tuple<bool, string>>(SecurityNumberValidator));

            Console.Write("Payment system (Visa/MasterCard): ");
            record.PaymentSystem = ReadInput(
                new Func<string, Tuple<bool, string, string>>(StringConverter),
                new Func<string, Tuple<bool, string>>(PaymentSystemValidator));

            Console.Write("Residence (True/False): ");
            record.Residency = ReadInput(
                new Func<string, Tuple<bool, string, bool>>(BooleanConverter),
                new Func<bool, Tuple<bool, string>>(ResidenceValidator));

            Console.Write("Country code: ");
            record.CountryCode = ReadInput(
                new Func<string, Tuple<bool, string, int>>(IntConverter),
                new Func<int, Tuple<bool, string>>(CountryCodeValidator));

            return record;
        }

        private static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine();
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
        }

        private static Tuple<bool, string, string> StringConverter(string str)
        {
            if (str is null)
            {
                return new Tuple<bool, string, string>(false, "Value is null", string.Empty);
            }

            return new Tuple<bool, string, string>(true, string.Empty, str);
        }

        private static Tuple<bool, string> FirstNameValidator(string firstName)
        {
            if (firstName.Length < 2 || firstName.Length > 60 || firstName.Contains(' '))
            {
                return new Tuple<bool, string>(false, "Invalid first name");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string> LastNameValidator(string lastName)
        {
            if (lastName.Length < 2 || lastName.Length > 60 || lastName.Contains(' '))
            {
                return new Tuple<bool, string>(false, "Invalid last name.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string> PaymentSystemValidator(string paymentSystem)
        {
            if (!paymentSystem.Equals("Visa", StringComparison.InvariantCultureIgnoreCase) &&
                !paymentSystem.Equals("MasterCard", StringComparison.InvariantCultureIgnoreCase))
            {
                return new Tuple<bool, string>(false, "Invalid payment system.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, DateTime> DateConverter(string date)
        {
            string[] dayMonthYear = date.Split('/');
            if (!int.TryParse(dayMonthYear[0], out int day))
            {
                return new Tuple<bool, string, DateTime>(
                    false,
                    "Invalid day.",
                    DateTime.MinValue);
            }

            if (!int.TryParse(dayMonthYear[1], out int month))
            {
                return new Tuple<bool, string, DateTime>(
                    false,
                    "Invalid month.",
                    DateTime.MinValue);
            }

            if (!int.TryParse(dayMonthYear[2], out int year))
            {
                return new Tuple<bool, string, DateTime>(
                    false,
                    "Invalid year.",
                    DateTime.MinValue);
            }

            return new Tuple<bool, string, DateTime>(
                true,
                string.Empty,
                new DateTime(year, month, day));
        }

        private static Tuple<bool, string> DateOfBirthValidator(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now || dateOfBirth < new DateTime(1950, 1, 1))
            {
                return new Tuple<bool, string>(false, "Date of birth is out of range.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, char> CharConverter(string ch)
        {
            if (ch.Length < 1)
            {
                return new Tuple<bool, string, char>(false, "Security character is empty.", '\0');
            }

            return new Tuple<bool, string, char>(true, string.Empty, ch[0]);
        }

        private static Tuple<bool, string> SecurityCharecterValidator(char securityCharecter)
        {
            if (!char.IsLetter(securityCharecter))
            {
                return new Tuple<bool, string>(false, "Securuty charecter must be a letter.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, int> IntConverter(string number)
        {
            if (int.TryParse(number, out int res))
            {
                return new Tuple<bool, string, int>(true, string.Empty, res);
            }

            return new Tuple<bool, string, int>(false, "Invalid number.", -1);
        }

        private static Tuple<bool, string> CountryCodeValidator(int number)
        {
            if (number < 1 || number > 999)
            {
                return new Tuple<bool, string>(false, "Invalid country code.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, short> ShortConverter(string number)
        {
            if (short.TryParse(number, out short res))
            {
                return new Tuple<bool, string, short>(true, string.Empty, res);
            }

            return new Tuple<bool, string, short>(false, "Invalid number.", -1);
        }

        private static Tuple<bool, string> SecurityNumberValidator(short number)
        {
            if (number < 0 || number > 9999)
            {
                return new Tuple<bool, string>(false, "Invalid security number.");
            }

            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, decimal> DecimalConverter(string dec)
        {
            if (decimal.TryParse(dec, out decimal res))
            {
                return new Tuple<bool, string, decimal>(true, string.Empty, res);
            }
            else
            {
                return new Tuple<bool, string, decimal>(false, "Invalid decimal value.", decimal.MinValue);
            }
        }

        private static Tuple<bool, string> BalanceValidator(decimal balance)
        {
            return new Tuple<bool, string>(true, string.Empty);
        }

        private static Tuple<bool, string, bool> BooleanConverter(string b)
        {
            if (b.Equals("true", StringComparison.InvariantCultureIgnoreCase))
            {
                return new Tuple<bool, string, bool>(true, string.Empty, true);
            }
            else if (b.Equals("false", StringComparison.InvariantCultureIgnoreCase))
            {
                return new Tuple<bool, string, bool>(true, string.Empty, false);
            }
            else
            {
                return new Tuple<bool, string, bool>(false, "Invalid residence", false);
            }
        }

        private static Tuple<bool, string> ResidenceValidator(bool residence)
        {
            return new Tuple<bool, string>(true, string.Empty);
        }
    }
}