// See https://aka.ms/new-console-template for more information


namespace PropertyManagement
{


public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        UI ui = new UI();
        ui.MainMenu();
    }
}

public class UI 
{

   public void MainMenu()
{
    Property.LoadSampleProperties(); // Load sample properties at the start
    Lease.SeedLeases(); // Load sample leases at the start
    Payment.SeedPayments(); // Load sample payments at the start

    Console.WriteLine("Please select what you would like to use:\n" +
                      "P: Property\n" +
                      "U: User\n" +
                      "M: Maintenance\n" +
                      "L: Lease\n" +
                      "R: Payment\n"+
                      "X: Exit the program\n");

    string response = Console.ReadLine().ToUpper();

    switch (response)
    {
        case "P":
        GoToPropertyClass();
        break;

        case "U":
        // User class
        break;

        case "M":
        //Maintenance class
        break;

        case "L":
        GoToLeaseClass();
        break;

        case "R":
        GoToPaymentClass();
        break;

        case "X":
        Console.WriteLine("Exiting the program. Goodbye!");
        Environment.Exit(0); // Exit the program
        break;

       default:
            Console.WriteLine("Invalid input. Please enter P, U, M, L, R, or X.");
            MainMenu(); // Call the MainMenu method again for valid input
            break;
    }
   
}

public void GoToPropertyClass()
{
    Console.WriteLine("Welcome to the properties.\n"+
    "Please select a letter corresponding to what you need to do\n"+
    "C: Create a new property\n"+
    "V: View a Property\n"+
    "E: Edit a property\n"+
    "A: View all properties\n");

    string response = Console.ReadLine().ToUpper();

    switch(response)
    {
        case "C":
        Property newProperty = Property.CreateProperty();
        Property.AddProperty(newProperty);// Add the new property to the list
        Console.WriteLine("Your property has been created successfully!");
        ClassToMenu("Property",GoToPropertyClass); // Call the method to return to the property menu or main menu
        break;

        case "V":
        ViewPropertyDetails(); // Call the method to view property details
        ClassToMenu("Property",GoToPropertyClass); // Call the method to return to the property menu or main menu
        break;

        case "A":
        DisplayAllProperties(); // Call the method to display all properties
        ClassToMenu("Property",GoToPropertyClass); // Call the method to return to the property menu or main menu
        break;

        case "E":
        //Edit a property
        break;

        default:
            Console.WriteLine("Invalid input. Please enter C, V, E, or A");
            break;  

    }
}

    public void GoToLeaseClass()
    {
        Console.WriteLine("Welcome to the Lease Menu.\n"+
    "Please select a letter corresponding to what you need to do\n"+
    "C: Create a new Lease\n"+
    "V: View a Lease\n"+
    "E: Edit a Lease\n"+
    "A: View all Leases\n");

    string response = Console.ReadLine().ToUpper();

    switch(response)
    {
        case "C":
        Lease newLease = Lease.CreateLease();
        Lease.AddLease(newLease);// Add the new lease to the list
        Console.WriteLine("Your lease has been created successfully!");
        ClassToMenu("Lease", GoToLeaseClass); // Call the method to return to the lease menu or main menu
        break;

        case "V":
        ViewLeaseDetails(); // Call the method to view lease details
        ClassToMenu("Lease", GoToLeaseClass); // Call the method to return to the lease menu or main menu
        break;

        case "A":
        DisplayAllLeases(); // Call the method to display all leases    
        ClassToMenu("Lease", GoToLeaseClass); // Call the method to return to the lease menu or main menu
        break;

        case "E":
        //Edit a property
        break;

        default:
            Console.WriteLine("Invalid input. Please enter C, V, E, A");
            break;  
    }

    }

    public void GoToPaymentClass()
{
    Console.WriteLine("Welcome to the Payments Menu.\n" +
                      "Please select what you want to do:\n" +
                      "C: Create a new Payment\n" +
                      "V: View a Payment\n"+
                      "A: View all Payments");

    string response = Console.ReadLine().ToUpper();

    switch (response)
    {
        case "C":
            Payment newPayment = Payment.CreatePayment();
            Payment.AddPayment(newPayment); // Add the new payment to the list
            Console.WriteLine("Your payment has been created successfully!");
            ClassToMenu("Payment", GoToPaymentClass); // Call the method to return to the payment menu or main menu
            break;

        case "V":
            ViewPaymentDetails();
            ClassToMenu("Payment", GoToPaymentClass); // Call the method to return to the payment menu or main menu
            break;

        case "A":
            DisplayAllPayments(); // Call the method to display all payments
            ClassToMenu("Payment", GoToPaymentClass); // Call the method to return to the payment menu or main menu
            break;

        default:
            Console.WriteLine("Invalid input. Please enter C, V, or A.");
            GoToPaymentClass();
            break;
    }
}

public void ViewPaymentDetails()
{
    Console.WriteLine("Enter the Payment ID you'd like to view:");
    int id = int.Parse(Console.ReadLine());

    Payment match = Payment.ListOfPayments.Find(p => p.PaymentID == id);
    if (match != null)
    {
        match.DisplayPaymentDetails();
        ClassToMenu("Payment", GoToPaymentClass);
    }
    else
    {
        Console.WriteLine("No payment found with that ID.");
        ViewPaymentDetails();
    }
}


    public void ClassToMenu(string menuName, Action subMenu)
    {
        Console.WriteLine("Would you like to return to the "+menuName+" menu? (Y/N)");
        string response = Console.ReadLine().ToUpper();

        if (response == "Y")
        {
            subMenu(); // Call the method again to go to the class menu
        }
        else if (response == "N")
        {
            Console.WriteLine("Returning to main menu...");
            MainMenu(); // Return to the main menu
        }
        else
        {
            Console.WriteLine("Invalid input. Returning to main menu...");
            MainMenu(); // Return to the main menu
        }
    }

    public void ViewPropertyDetails()
{
    Console.WriteLine("Please enter the property ID you would like to view:");
    string input = Console.ReadLine();
    int propertyID;

    if (int.TryParse(input, out propertyID))
    {
        Property match = Property.ListOfProperties.Find(p => p.PropertyID == propertyID);
        if (match != null)
        {
            match.DisplayPropertyDetails(); // Call the instance method on the matched property
            ClassToMenu("Property", GoToPropertyClass); // Call the method to return to the property menu or main menu
        }
        else
        {
            Console.WriteLine("Invalid Property ID. Please try again.");
            ViewPropertyDetails(); // Ask again
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a number for the Property ID.");
        ViewPropertyDetails(); // Ask again
    }
}

public void ViewLeaseDetails()
{
    Console.WriteLine("Please enter the lease term you would like to view:");
    string input = Console.ReadLine();
    int leaseID;

    if (int.TryParse(input, out leaseID))
    {
        Lease match = Lease.ListOfLeases.Find(l => l.LeaseID == leaseID);
        if (match != null)
        {
            match.DisplayLeaseDetails(); // Call the instance method on the matched lease
            Console.WriteLine("Would you like to return to the lease menu? (Y/N)");
            string returnToLeaseMenu = Console.ReadLine().ToUpper();
            ClassToMenu(returnToLeaseMenu, GoToLeaseClass); // Generic method to go back
        }
        else
        {
            Console.WriteLine("Invalid Lease Term. Please try again.");
            ViewLeaseDetails(); // Ask again
        }
    }
    else
    {
        Console.WriteLine("Invalid input. Please enter a number for the Lease Term.");
        ViewLeaseDetails(); // Ask again
    }
}

    public void DisplayAllLeases()
    {
        if (Lease.ListOfLeases.Count == 0)
    {
        Console.WriteLine("No leases to display.");
        return;
    }

    foreach (Lease lease in Lease.ListOfLeases)
    {
        lease.DisplayLeaseDetails();
    }
    }

    public void DisplayAllPayments()
    {
        if (Payment.ListOfPayments.Count == 0)
    {
        Console.WriteLine("No payments to display.");
        return;
    }

    foreach (Payment payment in Payment.ListOfPayments)
    {
        payment.DisplayPaymentDetails();
    }
    }

    public void DisplayAllProperties()
    {
         if (Property.ListOfProperties.Count == 0)
    {
        Console.WriteLine("No properties to display.");
        return;
    }

    foreach (Property property in Property.ListOfProperties)
    {
        property.DisplayPropertyDetails();
    }
    }
    

/*
    public void ViewPropertyDetails()
    {
        Console.WriteLine("Please enter the property ID you would like to view:");
    int propertyID = int.Parse(Console.ReadLine());

    Property match = Property.ListOfProperties.Find(p => p.PropertyID == propertyID);
    if (match != null)
    {
        match.DisplayPropertyDetails(); // Call the instance method on the matched property
        Console.WriteLine("Would you like to return to the property menu? (Y/N)");
        string returnToPropertyMenu = Console.ReadLine().ToUpper();
        PropertyToMenu(returnToPropertyMenu); // Call the method to return to the property menu or main menu
    }
    else
    {
        Console.WriteLine("Invalid Property ID. Please try again.");
        ViewPropertyDetails(); // Ask again
    }
    }
    */
}
}