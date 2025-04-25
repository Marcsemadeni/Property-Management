// Ben

using System.IO;
using System.Text.Json;

namespace PropertyManagement
{
public class Lease
{
    public int LeaseID{ get; set; } // Unique identifier for the lease
    public int leaseTerm{ get; set; }
    public double payment{ get; set; }
    public double deposit{ get; set; }
    public string contract{ get; set; }
    public static List<Lease> ListOfLeases = new List<Lease>(); // List to hold properties



    public Lease(int LeaseID,int leaseTerm, double payment, double deposit, string contract)
    {
        this.LeaseID = LeaseID;
        this.leaseTerm = leaseTerm;
        this.payment = payment;
        this.deposit = deposit;
        this.contract = contract;
    }

public static Lease CreateLease()
{
    //int LeaseID = UI.GetValidatedInt("Please enter the lease ID: ");

    int leaseID;

    while (true)
    {
        leaseID = UI.GetValidatedInt("Please enter the lease ID: ");
        bool exists = Lease.ListOfLeases.Any(l => l.LeaseID == leaseID);

        if (exists)
        {
            Console.WriteLine("That Lease ID already exists. Please enter a different ID.");
        }
        else
        {
            break;
        }
    }
    int leaseTerm   = UI.GetValidatedInt("Please enter the lease term (months): ");
    double payment  = UI.GetValidatedDouble("Enter the monthly payment: ");
    double deposit  = UI.GetValidatedDouble("Enter the security deposit amount: ");

    Console.Write("Enter the contract description or filename: ");
    string contract = Console.ReadLine();

    return new Lease(leaseID, leaseTerm, payment, deposit, contract);
}
public static void SeedLeases()
{
    // ListOfLeases.Add(new Lease(1, 6, 1200.00, 600.00, "Short-term lease with option to renew."));
    // ListOfLeases.Add(new Lease(2, 12, 1100.00, 1100.00, "Standard 1-year lease."));
    // ListOfLeases.Add(new Lease(3, 18, 1050.00, 1200.00, "Longer lease with discounted rate."));
    // ListOfLeases.Add(new Lease(4, 24, 1000.00, 1000.00, "2-year lease with early termination fee."));
    // ListOfLeases.Add(new Lease(5, 3, 1300.00, 500.00, "Trial lease—3 months only."));
}


    public static void AddLease(Lease lease)
    {
        // Add the property to the list
        ListOfLeases.Add(lease);
        SaveToFile(); 
        Console.WriteLine("Lease added successfully.");
    }

    public void DisplayLeaseDetails()
{
    Console.WriteLine("----- Lease Details -----");
    Console.WriteLine($"Lease ID: {LeaseID}");
    Console.WriteLine($"Lease Term: {leaseTerm} months");
    Console.WriteLine($"Monthly Payment: ${payment}");
    Console.WriteLine($"Security Deposit: ${deposit}");
    Console.WriteLine($"Contract Info: {contract}");
    Console.WriteLine("-------------------------");
}

public void DisplayAllLeases()
{
   if (ListOfLeases.Count == 0)
    {
        Console.WriteLine("No leases to display.");
        return;
    }

    foreach (Lease lease in ListOfLeases)
    {
        lease.DisplayLeaseDetails();
        Console.WriteLine("---------------------------");
    }
}

public static void SaveToFile()
{
    string json = JsonSerializer.Serialize(ListOfLeases, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText("lease.json", json);
}

public static void LoadFromFile()
{
     if (File.Exists("lease.json") && new FileInfo("lease.json").Length > 0)
    {
        string json = File.ReadAllText("lease.json");
        ListOfLeases = JsonSerializer.Deserialize<List<Lease>>(json) ?? new List<Lease>();
    }
    else
    {
        ListOfLeases = new List<Lease>();
    }
}



}
}