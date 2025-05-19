using System;
using System.IO;

class BudgetTracker
{
    static string filePath = "budget.txt";

    static void Main()
    {
        Console.WriteLine(" Simple Budget Tracker\n");

        bool running = true;
        while (running)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. Show Report");
            Console.WriteLine("4. Exit");

            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTransaction("Income");
                    break;
                case "2":
                    AddTransaction("Expense");
                    break;
                case "3":
                    ShowReport();
                    break;
                case "4":
                    running = false;
                    break;
                default:
                    Console.WriteLine(" Invalid choice.");
                    break;
            }
        }
    }

    static void AddTransaction(string type)
    {
        Console.Write($"Enter amount ({type}): ");
        string input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal amount))
        {
            string line = $"{DateTime.Now};{type};{amount}";
            File.AppendAllText(filePath, line + Environment.NewLine);
            Console.WriteLine(" Transaction saved successfully.");
        }
        else
        {
            Console.WriteLine(" Invalid amount.");
        }
    }

    static void ShowReport()
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("No data found.");
            return;
        }

        decimal totalIncome = 0;
        decimal totalExpense = 0;

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(';');
            if (parts.Length == 3)
            {
                string type = parts[1];
                decimal amount = decimal.Parse(parts[2]);

                if (type == "Income")
                    totalIncome += amount;
                else if (type == "Expense")
                    totalExpense += amount;
            }
        }

        decimal balance = totalIncome - totalExpense;

        Console.WriteLine("\n Financial Report:");
        Console.WriteLine($"Total Income: {totalIncome:C}");
        Console.WriteLine($"Total Expenses: {totalExpense:C}");
        Console.WriteLine($"Current Balance: {balance:C}");
    }
}
