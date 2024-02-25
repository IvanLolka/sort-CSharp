using System;
using System.Collections.Generic;
using System.Linq;

namespace Sort
{
    class Program
    {
        static void Main()
        {
            List<Bank> banks = new List<Bank>();

            while (true)
            {
                Console.WriteLine("Желаете добавить новый банк? (y/n)");
                string addBankChoice = Console.ReadLine();
                if (addBankChoice.ToLower() != "y")
                    break;

                Console.WriteLine("Введите название нового банка:");
                string bankName = Console.ReadLine();
                Bank bank = new Bank(bankName);
                banks.Add(bank);

                while (true)
                {
                    Console.WriteLine("Желаете добавить новый филиал в этот банк? (y/n)");
                    string addBranchChoice = Console.ReadLine();
                    if (addBranchChoice.ToLower() != "y")
                        break;

                    Console.WriteLine("Введите название нового филиала:");
                    string branchName = Console.ReadLine();
                    Branch branch = new Branch(branchName);
                    bank.AddBranch(branch);

                    while (true)
                    {
                        Console.WriteLine("Желаете добавить новый вклад в этот филиал? (y/n)");
                        string addDepositChoice = Console.ReadLine();
                        if (addDepositChoice.ToLower() != "y")
                            break;

                        Console.WriteLine("Выберите тип вклада (1 - Долгосрочный, 2 - До востребования):");
                        int depositTypeChoice = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите ФИО вкладчика:");
                        string depositorFullName = Console.ReadLine();
                        Console.WriteLine("Введите сумму вклада:");
                        double amount = double.Parse(Console.ReadLine());

                        Deposit deposit;
                        if (depositTypeChoice == 1)
                        {
                            Console.WriteLine("Введите количество месяцев:");
                            int months = int.Parse(Console.ReadLine());
                            deposit = new LongTermDeposit(depositorFullName, amount, months);
                        }
                        else
                        {
                            deposit = new DemandDeposit(depositorFullName, amount);
                        }

                        branch.AddDeposit(deposit);
                    }
                }
            }

            Console.WriteLine("Хотите добавить вклад в существующий филиал? (y/n)");
            string addDepositToExistingBranchChoice = Console.ReadLine();
            if (addDepositToExistingBranchChoice.ToLower() == "y")
            {
                Console.WriteLine("Выберите банк:");
                foreach (var bank in banks)
                {
                    Console.WriteLine($"- {bank.Name}");
                }
                string selectedBankName = Console.ReadLine();
                var selectedBank = banks.FirstOrDefault(b => b.Name == selectedBankName);
                if (selectedBank != null)
                {
                    Console.WriteLine("Выберите филиал:");
                    foreach (var branch in selectedBank.Branches)
                    {
                        Console.WriteLine($"- {branch.Name}");
                    }
                    string selectedBranchName = Console.ReadLine();
                    var selectedBranch = selectedBank.Branches.FirstOrDefault(b => b.Name == selectedBranchName);
                    if (selectedBranch != null)
                    {
                        Console.WriteLine("Введите тип вклада (1 - Долгосрочный, 2 - До востребования):");
                        int depositTypeChoice = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите ФИО вкладчика:");
                        string depositorFullName = Console.ReadLine();
                        Console.WriteLine("Введите сумму вклада:");
                        double amount = double.Parse(Console.ReadLine());

                        Deposit deposit;
                        if (depositTypeChoice == 1)
                        {
                            Console.WriteLine("Введите количество месяцев:");
                            int months = int.Parse(Console.ReadLine());
                            deposit = new LongTermDeposit(depositorFullName, amount, months);
                        }
                        else
                        {
                            deposit = new DemandDeposit(depositorFullName, amount);
                        }

                        selectedBranch.AddDeposit(deposit);
                    }
                    else
                    {
                        Console.WriteLine("Филиал не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Банк не найден.");
                }
            }

            int totalBanks = banks.Count;
            int totalBranches = banks.SelectMany(b => b.Branches).Count();
            int totalDeposits = banks.SelectMany(b => b.Branches).SelectMany(b => b.Deposits).Count();

            Console.WriteLine($"Минимальное количество экземпляров класса «банк»: {totalBanks};");
            Console.WriteLine($"Минимальное количество экземпляров класса «филиал»: {totalBranches} (в каждом банке);");
            Console.WriteLine($"Минимальное количество экземпляров класса «вклад»: {totalDeposits} (в каждом филиале);");
        }

    }

    // Класс банка
    public class Bank
    {
        public string Name { get; set; }
        public List<Branch> Branches { get; private set; }

        public Bank(string name)
        {
            Name = name;
            Branches = new List<Branch>();
        }

        public void AddBranch(Branch branch)
        {
            Branches.Add(branch);
        }
    }

    // Класс филиала
    public class Branch
    {
        public string Name { get; set; }
        public List<Deposit> Deposits { get; private set; }

        public Branch(string name)
        {
            Name = name;
            Deposits = new List<Deposit>();
        }

        public void AddDeposit(Deposit deposit)
        {
            Deposits.Add(deposit);
        }
    }

    // Базовый класс вклада
    public abstract class Deposit
    {
        public string DepositorFullName { get; set; }
        public double Amount { get; set; }

        public Deposit(string depositorFullName, double amount)
        {
            DepositorFullName = depositorFullName;
            Amount = amount;
        }

        public abstract double CalculateAmount(int months);
    }

    // Долгосрочный вклад
    public class LongTermDeposit : Deposit
    {
        private const double InterestRate = 0.1;

        public int Months { get; set; }

        public LongTermDeposit(string depositorFullName, double amount, int months)
            : base(depositorFullName, amount)
        {
            Months = months;
        }

        public override double CalculateAmount(int months)
        {
            return Amount + Amount * InterestRate * months;
        }
    }

    // Вклад до востребования
    public class DemandDeposit : Deposit
    {
        public DemandDeposit(string depositorFullName, double amount)
            : base(depositorFullName, amount)
        {

        }

        public override double CalculateAmount(int months)
        {
            return Amount;
        }
    }
}