    // Declare variables and then initialize to zero.
    int num1 = 0; int num2 = 0;

    Console.WriteLine("==============================================================\r");
    Console.WriteLine("Welcolme to community Librart Movie DVD Management System\r");
    Console.WriteLine("==============================================================\n");

    // Main Menu
    Console.WriteLine("======================= Main Menu =========================\n");
.
    // Ask the user to choose an option.
    Console.WriteLine("\t1. Staff Login");
    Console.WriteLine("\t2. Member Login");
    Console.WriteLine("\t0. Exit\n");
    Console.Write("Enter your choice ==> (1/2/0)\n");


    // Use a switch statement to choose wich menu.
    switch (Console.ReadLine())
    {
        case "1":
            // Staff Menu
            Console.WriteLine("======================= Main Menu =========================\n");
        .
            // Ask the user to choose an option.
            Console.WriteLine("\t1. Staff Login");
            Console.WriteLine("\t2. Member Login");
            Console.WriteLine("\t0. Exit\n");
            Console.Write("Enter your choice ==> (1/2/0)\n");
            break;
        case "2":
            Console.WriteLine($"Your result: {num1} - {num2} = " + (num1 - num2));
            break;
        case "0":
            Console.WriteLine($"Your result: {num1} * {num2} = " + (num1 * num2));
            break;
    }


    // Wait for the user to respond before closing.
    Console.Write("Press any key to close the Calculator console app...");
    Console.ReadKey();