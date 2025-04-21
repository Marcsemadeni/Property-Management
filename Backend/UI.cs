// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

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
    Console.WriteLine("Please select what you would like to use:\n" +
                      "P: Property\n" +
                      "U: User\n" +
                      "M: Maintenance\n" +
                      "L: Lease\n" +
                      "R: Payment\n");

    string response = Console.ReadLine().ToUpper();

    switch (response)
    {
        case "P":
        //Property class
        break;

        case "U":
        // User class
        break;

        case "M":
        //Maintenance class
        break;

        case "L":
        //lease class

        case "R":
        // Payment class

       default:
            Console.WriteLine("Invalid input. Please enter P, U, M, L, or R.");
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
        //Create a property 
        break;

        case "V":
        // View properties details
        break;

        case "E":
        //Edit a property
        break;

        default:
            Console.WriteLine("Invalid input. Please enter C, V, E");
            break;

    }


}






}