using System;
using System.Linq;

namespace PropertyManagement
{
    public class Program
    {
        public static void Main()
        {
            Property.LoadFromFile();
            Lease.LoadFromFile();
            Payment.LoadFromFile();
            Console.Clear();
            UI ui = new UI();
            ui.MainMenu();
        }
    }

    public class UI
    {
        private UserManager userManager = new UserManager();
        private MaintenanceManager maintenanceManager = new MaintenanceManager();
        private InspectionManager inspectionManager = new InspectionManager();

        private void DisplayMenu(string title, string[] options, ConsoleColor titleColor = ConsoleColor.Cyan)
        {
            Console.Clear();
            PrintColoredText("====================================\n", titleColor);
            PrintColoredText($"  {title}\n", titleColor);
            PrintColoredText("====================================\n", titleColor);
            Console.WriteLine();
            for (int i = 0; i < options.Length; i++)
            {
                PrintColoredText($"  {i + 1}. {options[i]}\n", ConsoleColor.White);
            }
            Console.WriteLine();
            PrintColoredText("  Enter a number or 'B' to go back, 'X' to exit.\n", ConsoleColor.Yellow);
            Console.WriteLine();
        }

        private string GetUserChoice(int maxOptions)
        {
            while (true)
            {
                PrintColoredText("  Choice: ", ConsoleColor.Green);
                string input = Console.ReadLine().ToUpper();
                if (input == "B" || input == "X") return input;
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= maxOptions)
                    return input;
                PrintColoredText("  Invalid choice. Please enter a number, 'B', or 'X'.\n", ConsoleColor.Red);
            }
        }

        public void MainMenu()
        {
            string[] options = {
                "Dashboard",
                "Properties",
                "Users",
                "Maintenance",
                "Inspections",
                "Leases",
                "Payments"
            };

            while (true)
            {
                DisplayMenu("Property Management System", options);
                string choice = GetUserChoice(options.Length);

                switch (choice)
                {
                    case "1": DisplayDashboard(); break;
                    case "2": GoToPropertyClass(); break;
                    case "3": GoToUserMenu(); break;
                    case "4": GoToMaintenanceMenu(); break;
                    case "5": GoToInspectionMenu(); break;
                    case "6": GoToLeaseClass(); break;
                    case "7": GoToPaymentClass(); break;
                    case "B": continue;
                    case "X":
                        PrintColoredText("  Exiting the program. Goodbye!\n", ConsoleColor.Yellow);
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private void DisplayDashboard()
        {
            Console.Clear();
            PrintColoredText("====================================\n", ConsoleColor.Cyan);
            PrintColoredText("  Dashboard\n", ConsoleColor.Cyan);
            PrintColoredText("====================================\n", ConsoleColor.Cyan);
            Console.WriteLine();
            PrintColoredText($"  Total Properties: {Property.ListOfProperties.Count}\n", ConsoleColor.White);
            PrintColoredText($"  Total Leases: {Lease.ListOfLeases.Count}\n", ConsoleColor.White);
            PrintColoredText($"  Total Payments: {Payment.ListOfPayments.Count}\n", ConsoleColor.White);
            PrintColoredText($"  Upcoming Inspections: {inspectionManager.GetUpcomingInspections().Count}\n", ConsoleColor.White);
            PrintColoredText($"  Overdue Maintenance Tasks: {maintenanceManager.GetOverdueTasks().Count}\n", ConsoleColor.White);
            Console.WriteLine();
            PrintColoredText("  Press any key to return to the main menu.\n", ConsoleColor.Yellow);
            Console.ReadKey();
            MainMenu();
        }

        public void GoToPropertyClass()
        {
            string[] options = {
                "Create a new property",
                "View a property",
                "Edit a property",
                "Search properties",
                "View all properties"
            };

            while (true)
            {
                DisplayMenu("Properties", options);
                string choice = GetUserChoice(options.Length);

                switch (choice)
                {
                    case "1":
                        Property newProperty = Property.CreateProperty();
                        Property.AddProperty(newProperty);
                        PrintColoredText($"  Property at {newProperty.Address} created successfully!\n", ConsoleColor.Green);
                        PauseAndReturn(() => GoToPropertyClass());
                        break;
                    case "2":
                        ViewPropertyDetails();
                        break;
                    case "3":
                        EditProperty();
                        break;
                    case "4":
                        SearchProperties();
                        break;
                    case "5":
                        DisplayAllProperties();
                        PauseAndReturn(() => GoToPropertyClass());
                        break;
                    case "B": MainMenu(); return;
                    case "X": Environment.Exit(0); break;
                }
            }
        }

        private void EditProperty()
        {
            PrintColoredText("  Enter the Property ID to edit: ", ConsoleColor.Green);
            if (!int.TryParse(Console.ReadLine(), out int propertyID))
            {
                PrintColoredText("  Invalid ID. Please enter a number.\n", ConsoleColor.Red);
                PauseAndReturn(() => GoToPropertyClass());
                return;
            }

            Property property = Property.ListOfProperties.Find(p => p.PropertyID == propertyID);
            if (property == null)
            {
                PrintColoredText("  Property not found.\n", ConsoleColor.Red);
                PauseAndReturn(() => GoToPropertyClass());
                return;
            }

            PrintColoredText("  Enter new details (press Enter to keep current value):\n", ConsoleColor.Yellow);
            PrintColoredText($"  Address ({property.Address}): ", ConsoleColor.Green);
            string address = Console.ReadLine();
            if (!string.IsNullOrEmpty(address)) property.Address = address;

            PrintColoredText($"  Bedrooms ({property.Bedrooms}): ", ConsoleColor.Green);
            if (int.TryParse(Console.ReadLine(), out int bedrooms)) property.Bedrooms = bedrooms;

            PrintColoredText($"  Bathrooms ({property.Baths}): ", ConsoleColor.Green);
            if (int.TryParse(Console.ReadLine(), out int baths)) property.Baths = baths;

            PrintColoredText($"  Monthly Rent ({property.MonthlyRent}): ", ConsoleColor.Green);
            if (int.TryParse(Console.ReadLine(), out int rent)) property.MonthlyRent = rent;

            Property.SaveToFile();
            PrintColoredText("  Property updated successfully!\n", ConsoleColor.Green);
            PauseAndReturn(() => GoToPropertyClass());
        }

        private void SearchProperties()
        {
            PrintColoredText("  Enter search term (address or ID): ", ConsoleColor.Green);
            string searchTerm = Console.ReadLine().ToLower();
            var results = Property.ListOfProperties
                .Where(p => p.Address.ToLower().Contains(searchTerm) || p.PropertyID.ToString().Contains(searchTerm))
                .ToList();

            if (results.Count == 0)
            {
                PrintColoredText("  No properties found.\n", ConsoleColor.Red);
            }
            else
            {
                PrintColoredText("  Search Results:\n", ConsoleColor.Cyan);
                foreach (var property in results)
                {
                    property.DisplayPropertyDetails();
                }
            }
            PauseAndReturn(() => GoToPropertyClass());
        }

        public void GoToUserMenu()
        {
            string[] options = {
                "Add Tenant",
                "Add Staff",
                "Add Generic User",
                "Remove User",
                "Show All Users"
            };

            while (true)
            {
                DisplayMenu("Users", options);
                string choice = GetUserChoice(options.Length);

                switch (choice)
                {
                    case "1":
                        var tenant = TenantManager.CreateTenantFromInput();
                        userManager.AddUser(tenant);
                        PauseAndReturn(() => GoToUserMenu());
                        break;
                    case "2":
                        PrintColoredText("  Name: ", ConsoleColor.Green);
                        string staffName = Console.ReadLine();
                        PrintColoredText("  Phone Number: ", ConsoleColor.Green);
                        string staffPhone = Console.ReadLine();
                        PrintColoredText("  Email: ", ConsoleColor.Green);
                        string staffEmail = Console.ReadLine();
                        var staff = new Staff { Name = staffName, PhoneNumber = staffPhone, Email = staffEmail };
                        userManager.AddUser(staff);
                        PauseAndReturn(() => GoToUserMenu());
                        break;
                    case "3":
                        PrintColoredText("  Name: ", ConsoleColor.Green);
                        string name = Console.ReadLine();
                        PrintColoredText("  Phone Number: ", ConsoleColor.Green);
                        string phone = Console.ReadLine();
                        PrintColoredText("  Email: ", ConsoleColor.Green);
                        string email = Console.ReadLine();
                        var user = new User { Name = name, PhoneNumber = phone, Email = email };
                        userManager.AddUser(user);
                        PauseAndReturn(() => GoToUserMenu());
                        break;
                    case "4":
                        PrintColoredText("  Email of user to remove: ", ConsoleColor.Green);
                        string emailToRemove = Console.ReadLine();
                        userManager.RemoveUser(emailToRemove);
                        PauseAndReturn(() => GoToUserMenu());
                        break;
                    case "5":
                        var allUsers = userManager.GetAllUsers();
                        if (allUsers.Count == 0)
                            PrintColoredText("  No users found.\n", ConsoleColor.Red);
                        else
                            foreach (var u in allUsers)
                                PrintColoredText($"  {u.GetType().Name}: {u.Name} - {u.Email}\n", ConsoleColor.White);
                        PauseAndReturn(() => GoToUserMenu());
                        break;
                    case "B": MainMenu(); return;
                    case "X": Environment.Exit(0); break;
                }
            }
        }

        public void GoToMaintenanceMenu()
        {
            string[] options = {
                "Add Task",
                "Update Task",
                "List Tasks",
                "Remove Task"
            };

            while (true)
            {
                DisplayMenu("Maintenance", options);
                string choice = GetUserChoice(options.Length);

                switch (choice)
                {
                    case "1":
                        maintenanceManager.AddTask(maintenanceManager.CreateMaintenancePrompt());
                        PauseAndReturn(() => GoToMaintenanceMenu());
                        break;
                    case "2":
                        PrintColoredText("  Enter title to update: ", ConsoleColor.Green);
                        string title = Console.ReadLine();
                        PrintColoredText("  Mark as complete? (y/n): ", ConsoleColor.Green);
                        bool isCompleted = Console.ReadLine().ToLower() == "y";
                        maintenanceManager.UpdateTask(title, isCompleted);
                        PauseAndReturn(() => GoToMaintenanceMenu());
                        break;
                    case "3":
                        NestedGetTasks();
                        break;
                    case "4":
                        PrintColoredText("  Enter title to remove: ", ConsoleColor.Green);
                        string removeTitle = Console.ReadLine();
                        maintenanceManager.RemoveTask(removeTitle);
                        PauseAndReturn(() => GoToMaintenanceMenu());
                        break;
                    case "B": MainMenu(); return;
                    case "X": Environment.Exit(0); break;
                }
            }
        }

        public void NestedGetTasks()
        {
            string[] options = {
                "Overdue",
                "High Priority",
                "Upcoming",
                "All Tasks"
            };

            DisplayMenu("List Maintenance Tasks", options);
            string choice = GetUserChoice(options.Length);

            List<Maintenance> tasks = choice switch
            {
                "1" => maintenanceManager.GetOverdueTasks(),
                "2" => maintenanceManager.GetHighPriorityTasks(),
                "3" => maintenanceManager.GetUpcomingTasks(7),
                _ => maintenanceManager.GetAllTasks()
            };

            if (tasks.Count == 0)
                PrintColoredText("  No tasks found.\n", ConsoleColor.Red);
            else
                foreach (var task in tasks)
                    PrintColoredText($"  {task}\n", ConsoleColor.White);

            PauseAndReturn(() => GoToMaintenanceMenu());
        }

        public void GoToInspectionMenu()
        {
            string[] options = {
                "Schedule Inspection",
                "List Inspections",
                "Remove Inspection",
                "Generate Inspection Report"
            };

            while (true)
            {
                DisplayMenu("Inspections", options);
                string choice = GetUserChoice(options.Length);

                switch (choice)
                {
                    case "1":
                        PrintColoredText("  Enter inspection type: ", ConsoleColor.Green);
                        string type = Console.ReadLine();
                        PrintColoredText("  Enter property ID: ", ConsoleColor.Green);
                        string propertyID = Console.ReadLine();
                        PrintColoredText("  Enter property address: ", ConsoleColor.Green);
                        string address = Console.ReadLine();
                        PrintColoredText("  Enter inspector name: ", ConsoleColor.Green);
                        string inspector = Console.ReadLine();
                        PrintColoredText("  Enter date (MM/DD/YYYY): ", ConsoleColor.Green);
                        DateTime date;
                        while (!DateTime.TryParse(Console.ReadLine(), out date))
                        {
                            PrintColoredText("  Invalid date. Please enter again (MM/DD/YYYY): \n", ConsoleColor.Red);
                        }
                        var inspection = new Inspection();
                        inspection.Schedule(type, date, propertyID, address, inspector);
                        inspectionManager.AddInspection(inspection);
                        PauseAndReturn(() => GoToInspectionMenu());
                        break;
                    case "2":
                        var all = inspectionManager.GetAllInspections();
                        if (all.Count == 0)
                            PrintColoredText("  No inspections found.\n", ConsoleColor.Red);
                        else
                            foreach (var ins in all)
                                PrintColoredText($"  {ins}\n", ConsoleColor.White);
                        PauseAndReturn(() => GoToInspectionMenu());
                        break;
                    case "3":
                        PrintColoredText("  Enter property ID of inspection to remove: ", ConsoleColor.Green);
                        string pID = Console.ReadLine();
                        PrintColoredText("  Enter date of inspection to remove (MM/DD/YYYY): ", ConsoleColor.Green);
                        DateTime removeDate;
                        while (!DateTime.TryParse(Console.ReadLine(), out removeDate))
                        {
                            PrintColoredText("  Invalid date. Please enter again (MM/DD/YYYY): \n", ConsoleColor.Red);
                        }
                        inspectionManager.RemoveInspection(pID, removeDate);
                        PauseAndReturn(() => GoToInspectionMenu());
                        break;
                    case "4":
                        GenerateInspectionReport();
                        PauseAndReturn(() => GoToInspectionMenu());
                        break;
                    case "B": MainMenu(); return;
                    case "X": Environment.Exit(0); break;
                }
            }
        }

        private void GenerateInspectionReport()
        {
            PrintColoredText("  Enter property ID of inspection: ", ConsoleColor.Green);
            string pID = Console.ReadLine();
            var inspections = inspectionManager.GetAllInspections().Where(i => i.PropertyID == pID).ToList();
            if (inspections.Count == 0)
            {
                PrintColoredText("  No inspections found for this property.\n", ConsoleColor.Red);
                return;
            }

            PrintColoredText("  Inspection Reports:\n", ConsoleColor.Cyan);
            foreach (var ins in inspections)
            {
                if (ins.ReportGenerated)
                    PrintColoredText($"  {ins.ReportContent}\n", ConsoleColor.White);
                else
                    PrintColoredText($"  No report generated for {ins.Type} on {ins.DateScheduled.ToShortDateString()}.\n", ConsoleColor.Yellow);
            }
        }

        public void GoToLeaseClass()
        {
            string[] options = {
                "Create a new lease",
                "View a lease",
                "Edit a lease",
                "View all leases"
            };

            while (true)
            {
                DisplayMenu("Leases", options);
                string choice = GetUserChoice(options.Length);

                switch (choice)
                {
                    case "1":
                        Lease newLease = Lease.CreateLease();
                        Lease.AddLease(newLease);
                        PrintColoredText("  Lease created successfully!\n", ConsoleColor.Green);
                        PauseAndReturn(() => GoToLeaseClass());
                        break;
                    case "2":
                        ViewLeaseDetails();
                        break;
                    case "3":
                        EditLease();
                        break;
                    case "4":
                        DisplayAllLeases();
                        PauseAndReturn(() => GoToLeaseClass());
                        break;
                    case "B": MainMenu(); return;
                    case "X": Environment.Exit(0); break;
                }
            }
        }

        private void EditLease()
        {
            PrintColoredText("  Enter the Lease ID to edit: ", ConsoleColor.Green);
            if (!int.TryParse(Console.ReadLine(), out int leaseID))
            {
                PrintColoredText("  Invalid ID. Please enter a number.\n", ConsoleColor.Red);
                PauseAndReturn(() => GoToLeaseClass());
                return;
            }

            Lease lease = Lease.ListOfLeases.Find(l => l.LeaseID == leaseID);
            if (lease == null)
            {
                PrintColoredText("  Lease not found.\n", ConsoleColor.Red);
                PauseAndReturn(() => GoToLeaseClass());
                return;
            }

            PrintColoredText("  Enter new details (press Enter to keep current value):\n", ConsoleColor.Yellow);
            PrintColoredText($"  Lease Term ({lease.leaseTerm}): ", ConsoleColor.Green);
            if (int.TryParse(Console.ReadLine(), out int term)) lease.leaseTerm = term;

            PrintColoredText($"  Monthly Payment ({lease.payment}): ", ConsoleColor.Green);
            if (double.TryParse(Console.ReadLine(), out double payment)) lease.payment = payment;

            PrintColoredText($"  Security Deposit ({lease.deposit}): ", ConsoleColor.Green);
            if (double.TryParse(Console.ReadLine(), out double deposit)) lease.deposit = deposit;

            Lease.SaveToFile();
            PrintColoredText("  Lease updated successfully!\n", ConsoleColor.Green);
            PauseAndReturn(() => GoToLeaseClass());
        }

        public void GoToPaymentClass()
        {
            string[] options = {
                "Create a new payment",
                "View a payment",
                "View all payments"
            };

            while (true)
            {
                DisplayMenu("Payments", options);
                string choice = GetUserChoice(options.Length);

                switch (choice)
                {
                    case "1":
                        Payment newPayment = Payment.CreatePayment();
                        Payment.AddPayment(newPayment);
                        PrintColoredText("  Payment created successfully!\n", ConsoleColor.Green);
                        PauseAndReturn(() => GoToPaymentClass());
                        break;
                    case "2":
                        ViewPaymentDetails();
                        break;
                    case "3":
                        DisplayAllPayments();
                        PauseAndReturn(() => GoToPaymentClass());
                        break;
                    case "B": MainMenu(); return;
                    case "X": Environment.Exit(0); break;
                }
            }
        }

        public void ViewPaymentDetails()
        {
            if (Payment.ListOfPayments.Count == 0)
            {
                PrintColoredText("  There are no payments to view.\n", ConsoleColor.Red);
                PauseAndReturn(() => GoToPaymentClass());
                return;
            }

            PrintColoredText("  Enter the Payment ID you'd like to view: ", ConsoleColor.Green);
            int id = GetValidatedInt("");
            Payment match = Payment.ListOfPayments.Find(p => p.PaymentID == id);
            if (match != null)
            {
                match.DisplayPaymentDetails();
                PauseAndReturn(() => GoToPaymentClass());
            }
            else
            {
                PrintColoredText("  No payment found with that ID.\n", ConsoleColor.Red);
                PauseAndReturn(() => ViewPaymentDetails());
            }
        }

        public void ViewPropertyDetails()
        {
            PrintColoredText("  Enter the Property ID you'd like to view: ", ConsoleColor.Green);
            if (!int.TryParse(Console.ReadLine(), out int propertyID))
            {
                PrintColoredText("  Invalid input. Please enter a number.\n", ConsoleColor.Red);
                PauseAndReturn(() => ViewPropertyDetails());
                return;
            }

            Property match = Property.ListOfProperties.Find(p => p.PropertyID == propertyID);
            if (match != null)
            {
                match.DisplayPropertyDetails();
                PauseAndReturn(() => GoToPropertyClass());
            }
            else
            {
                PrintColoredText("  No property found with that ID.\n", ConsoleColor.Red);
                PauseAndReturn(() => ViewPropertyDetails());
            }
        }

        public void ViewLeaseDetails()
        {
            PrintColoredText("  Enter the Lease ID you'd like to view: ", ConsoleColor.Green);
            if (!int.TryParse(Console.ReadLine(), out int leaseID))
            {
                PrintColoredText("  Invalid input. Please enter a number.\n", ConsoleColor.Red);
                PauseAndReturn(() => ViewLeaseDetails());
                return;
            }

            Lease match = Lease.ListOfLeases.Find(l => l.LeaseID == leaseID);
            if (match != null)
            {
                match.DisplayLeaseDetails();
                PauseAndReturn(() => GoToLeaseClass());
            }
            else
            {
                PrintColoredText("  No lease found with that ID.\n", ConsoleColor.Red);
                PauseAndReturn(() => ViewLeaseDetails());
            }
        }

        public void DisplayAllLeases()
        {
            if (Lease.ListOfLeases.Count == 0)
            {
                PrintColoredText("  No leases to display.\n", ConsoleColor.Red);
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
                PrintColoredText("  No payments to display.\n", ConsoleColor.Red);
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
                PrintColoredText("  No properties to display.\n", ConsoleColor.Red);
                return;
            }

            foreach (Property property in Property.ListOfProperties)
            {
                property.DisplayPropertyDetails();
            }
        }

        private void PauseAndReturn(Action returnAction)
        {
            Console.WriteLine();
            PrintColoredText("  Press any key to continue...\n", ConsoleColor.Yellow);
            Console.ReadKey();
            returnAction();
        }

        public static int GetValidatedInt(string prompt)
        {
            while (true)
            {
                PrintColoredText($"  {prompt}", ConsoleColor.Green);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int value))
                    return value;
                PrintColoredText("  Invalid input. Please enter a valid number.\n", ConsoleColor.Red);
            }
        }

        public static double GetValidatedDouble(string prompt)
        {
            while (true)
            {
                PrintColoredText($"  {prompt}", ConsoleColor.Green);
                string input = Console.ReadLine();
                if (double.TryParse(input, out double value))
                    return value;
                PrintColoredText("  Invalid input. Please enter a valid number.\n", ConsoleColor.Red);
            }
        }

        public static DateTime GetValidatedDateTime(string prompt)
        {
            while (true)
            {
                PrintColoredText($"  {prompt}", ConsoleColor.Green);
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out DateTime value))
                    return value;
                PrintColoredText("  Invalid date. Please enter the date in MM/DD/YYYY format.\n", ConsoleColor.Red);
            }
        }

        public static void PrintColoredText(string text, ConsoleColor color)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = originalColor;
        }
    }
}