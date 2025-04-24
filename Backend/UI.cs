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
    /*
    Main Menu:
        - Properties
        - User
        - Maintenance
        - Lease
        - Payments

    Properties:
    - create property
    - view property
    - edit property

    */

    

   public void MainMenu()
{
    Property.LoadSampleProperties(); // Load sample properties at the start

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
        //lease class
        break;

        case "R":
        // Payment class
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
    "E: Edit a property");

    string response = Console.ReadLine().ToUpper();

    switch(response)
    {
        case "C":
        Property newProperty = Property.CreateProperty();
        Property.AddProperty(newProperty);// Add the new property to the list
        Console.WriteLine("Your property has been created successfully!");
        Console.WriteLine("Would you like to return to the property menu? (Y/N)");
        string returnToPropertyMenu = Console.ReadLine().ToUpper();
        PropertyToMenu(returnToPropertyMenu); // Call the method to return to the property menu or main menu
        break;

        case "V":
        ViewPropertyDetails(); // Call the method to view property details
        break;

        case "E":
        //Edit a property
        break;

        default:
            Console.WriteLine("Invalid input. Please enter C, V, E");
            break;  

    }
}

    public void PropertyToMenu(string returnToPropertyMenu)
    {
        if (returnToPropertyMenu == "Y")
        {
            GoToPropertyClass(); // Call the method again to go to the property menu
        }
        else if (returnToPropertyMenu == "N")
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
    else
    {
        Console.WriteLine("Invalid input. Please enter a number for the Property ID.");
        ViewPropertyDetails(); // Ask again
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