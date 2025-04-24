// Ben
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
    Console.WriteLine("Please enter the lease ID:");
    int LeaseID = int.Parse(Console.ReadLine());

    Console.WriteLine("Please enter the lease term (months):");
    int leaseTerm = int.Parse(Console.ReadLine());

    Console.Write("Enter the monthly payment: ");
    double payment = double.Parse(Console.ReadLine());

    Console.Write("Enter the security deposit amount: ");
    double deposit = double.Parse(Console.ReadLine());

    Console.Write("Enter the contract description or filename: ");
    string contract = Console.ReadLine();

    return new Lease(LeaseID, leaseTerm, payment, deposit, contract);
}

public static void SeedLeases()
{
    ListOfLeases.Add(new Lease(1, 6, 1200.00, 600.00, "Short-term lease with option to renew."));
    ListOfLeases.Add(new Lease(2, 12, 1100.00, 1100.00, "Standard 1-year lease."));
    ListOfLeases.Add(new Lease(3, 18, 1050.00, 1200.00, "Longer lease with discounted rate."));
    ListOfLeases.Add(new Lease(4, 24, 1000.00, 1000.00, "2-year lease with early termination fee."));
    ListOfLeases.Add(new Lease(5, 3, 1300.00, 500.00, "Trial lease—3 months only."));
}


    public static void AddLease(Lease lease)
    {
        // Add the property to the list
        ListOfLeases.Add(lease);
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


}
}