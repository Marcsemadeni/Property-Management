// Ben

using System.IO;
using System.Text.Json;


namespace PropertyManagement
{

public class Payment
    {
        public int PaymentID { get; set; }
        public int LeaseID { get; set; }
        public int Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public static List<Payment> ListOfPayments = new List<Payment>();

        public Payment(int paymentID, int leaseID, int amount, DateTime datePaid)
        {
            PaymentID = paymentID;
            LeaseID = leaseID;
            Amount = amount;
            DatePaid = datePaid;
        }

        public static Payment CreatePayment()
        {
        int paymentID;

    // Check if PaymentID already exists
    while (true)
    {
        paymentID = UI.GetValidatedInt("Please enter the payment ID: ");
        bool exists = Payment.ListOfPayments.Any(p => p.PaymentID == paymentID);

        if (exists)
        {
            Console.WriteLine("That Payment ID already exists. Please enter a different ID.");
        }
        else
        {
            break;
        }
    }

        int leaseID   = UI.GetValidatedInt("Please enter the lease ID: ");
        int amount    = UI.GetValidatedInt("Enter the payment amount: ");

        DateTime datePaid = UI.GetValidatedDateTime("Enter the date paid (MM/DD/YYYY): ");

        return new Payment(paymentID, leaseID, amount, datePaid);
        }

        public static void SeedPayments()
    {
        // ListOfPayments.Add(new Payment(1, 1, 1200, new DateTime(2024, 01, 15)));
        // ListOfPayments.Add(new Payment(2, 2, 1100, new DateTime(2024, 02, 01)));
        // ListOfPayments.Add(new Payment(3, 3, 1050, new DateTime(2024, 03, 01)));
        // ListOfPayments.Add(new Payment(4, 1, 1200, new DateTime(2024, 02, 15)));
        // ListOfPayments.Add(new Payment(5, 5, 1300, new DateTime(2024, 04, 01)));
    }

        public static void AddPayment(Payment newPayment)
        {
            ListOfPayments.Add(newPayment);
            SaveToFile();
        }

        public void DisplayPaymentDetails()
        {
            Console.WriteLine("");
            Console.WriteLine($"Payment ID: {PaymentID}");
            Console.WriteLine($"Lease ID: {LeaseID}");
            Console.WriteLine($"Amount: ${Amount}");
            Console.WriteLine($"Date Paid: {DatePaid.ToShortDateString()}");
            Console.WriteLine("----------------------------");

        }

        public static void LoadSamplePayments()
        {
            ListOfPayments.Add(new Payment(1, 1, 1800, DateTime.Now.AddDays(-30)));
            ListOfPayments.Add(new Payment(2, 2, 2400, DateTime.Now.AddDays(-15)));
        }

        public void DisplayALLPayments()
        {
             if (ListOfPayments.Count == 0)
    {
        Console.WriteLine("No payments to display.");
        return;
    }

    foreach (Payment payment in ListOfPayments)
    {
        payment.DisplayPaymentDetails();
        Console.WriteLine("---------------------------");
    }
        }

public static void SaveToFile()
{
    string json = JsonSerializer.Serialize(ListOfPayments, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText("payment.json", json);
}

public static void LoadFromFile()
{
   if (File.Exists("payment.json") && new FileInfo("payment.json").Length > 0)
    {
        string json = File.ReadAllText("payment.json");
        ListOfPayments = JsonSerializer.Deserialize<List<Payment>>(json) ?? new List<Payment>();
    }
    else
    {
        ListOfPayments = new List<Payment>();
    }
}


    }
}