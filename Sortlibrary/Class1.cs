
namespace Sortlibrary
{
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
