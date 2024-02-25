using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Sortlibrary;

namespace SortWPF
{
    public partial class MainWindow : Window
    {
        private List<Bank> _banks = new List<Bank>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddBank_Click(object sender, RoutedEventArgs e)
        {
            string bankName = BankNameTextBox.Text;
            Bank bank = new Bank(bankName);
            _banks.Add(bank);
            UpdateUI();
        }

        private void AddBranch_Click(object sender, RoutedEventArgs e)
        {
            if (BankComboBox.SelectedItem != null)
            {
                Bank selectedBank = (Bank)BankComboBox.SelectedItem;
                string branchName = BranchNameTextBox.Text;
                Branch branch = new Branch(branchName);
                selectedBank.AddBranch(branch);
                UpdateUI();
            }
        }

        private void AddDeposit_Click(object sender, RoutedEventArgs e)
        {
            if (BranchComboBox.SelectedItem != null)
            {
                Branch selectedBranch = (Branch)BranchComboBox.SelectedItem;
                string depositorFullName = DepositorFullNameTextBox.Text;
                double amount = double.Parse(AmountTextBox.Text);
                Deposit deposit;
                if (DepositTypeComboBox.SelectedIndex == 0)
                {
                    int months = int.Parse(MonthsTextBox.Text);
                    deposit = new LongTermDeposit(depositorFullName, amount, months);
                }
                else
                {
                    deposit = new DemandDeposit(depositorFullName, amount);
                }
                selectedBranch.AddDeposit(deposit);
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            BankComboBox.ItemsSource = null;
            BankComboBox.ItemsSource = _banks;
            BankComboBox.DisplayMemberPath = "Name";

            if (BankComboBox.SelectedItem != null)
            {
                Bank selectedBank = (Bank)BankComboBox.SelectedItem;
                BranchComboBox.ItemsSource = null;
                BranchComboBox.ItemsSource = selectedBank.Branches.Select(b => new { Name = b.Name });
            }
            TotalBanksLabel.Content = _banks.Count;
            TotalBranchesLabel.Content = _banks.Sum(b => b.Branches.Count);
            TotalDepositsLabel.Content = _banks.Sum(b => b.Branches.Sum(branch => branch.Deposits.Count));
        }
        private void BankComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BankComboBox.SelectedItem != null)
            {
                Bank selectedBank = (Bank)BankComboBox.SelectedItem;
                BranchComboBox.ItemsSource = selectedBank.Branches;
            }
        }
    }
}
