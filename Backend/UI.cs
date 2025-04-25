// See https://aka.ms/new-console-template for more information


namespace PropertyManagement
{


public class Program
{
    public static void Main()
    {
        // Property.LoadSampleProperties(); // Load sample properties at the start
        // Lease.SeedLeases(); // Load sample leases at the start
        // Payment.SeedPayments(); // Load sample payments at the start
        UI ui = new UI();
        ui.MainMenu();
    }
}

public class UI 
{
    

   public void MainMenu()
{
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

        // case "E":
        // //Edit a property
        // break;

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

        // case "E":
        // //Edit a lease
        // break;

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
    if (Payment.ListOfPayments.Count == 0)
    {
        Console.WriteLine("There are no payments to view.");
        ClassToMenu("Payment", GoToPaymentClass);
        return;
    }

    Console.WriteLine("Enter the Payment ID you'd like to view:");
    int id = UI.GetValidatedInt("");

    Payment match = Payment.ListOfPayments.Find(p => p.PaymentID == id);
    if (match != null)
    {
        match.DisplayPaymentDetails();
        ClassToMenu("Payment", GoToPaymentClass);
    }
    else
    {
        Console.WriteLine("No payment found with that ID.");
        ViewPaymentDetails(); // Optionally call again or return to menu
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
    if (Property.ListOfProperties.Count == 0)
    {
        Console.WriteLine("There are no properties to view.");
        ClassToMenu("Property", GoToPropertyClass);
        return;
    }

    Console.WriteLine("Enter the Property ID you'd like to view:");
    int id = UI.GetValidatedInt("");

    Property match = Property.ListOfProperties.Find(p => p.PropertyID == id);
    if (match != null)
    {
        match.DisplayPropertyDetails();
        ClassToMenu("Property", GoToPropertyClass);
    }
    else
    {
        Console.WriteLine("No property found with that ID.");
        ViewPropertyDetails(); // again, you could return to menu here too
    }
}

public void ViewLeaseDetails()
{
    if (Lease.ListOfLeases.Count == 0)
    {
        Console.WriteLine("There are no leases to view.");
        ClassToMenu("Lease", GoToLeaseClass);
        return;
    }

    Console.WriteLine("Enter the Lease ID you'd like to view:");
    int id = UI.GetValidatedInt("");

    Lease match = Lease.ListOfLeases.Find(l => l.LeaseID == id);
    if (match != null)
    {
        match.DisplayLeaseDetails();
        ClassToMenu("Lease", GoToLeaseClass);
    }
    else
    {
        Console.WriteLine("No lease found with that ID.");
        ViewLeaseDetails(); // optional: could also return to menu instead
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

    public static int GetValidatedInt(string prompt)
{
    int value;
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();
        if (int.TryParse(input, out value))
            return value;

        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}

public static double GetValidatedDouble(string prompt)
{
    double value;
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();
        if (double.TryParse(input, out value))
            return value;

        Console.WriteLine("Invalid input. Please enter a valid number.");
    }
}

public static DateTime GetValidatedDateTime(string prompt)
{
    DateTime value;
    while (true)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();
        if (DateTime.TryParse(input, out value))
            return value;

        Console.WriteLine("Invalid date. Please enter the date in MM/DD/YYYY format.");
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