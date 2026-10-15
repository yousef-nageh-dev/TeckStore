//اليوجكت مفعل ميزه توب ليفل استاتمنت 
//تعالي كدا لما نفتح الملف اللي فيه الكلاس اللس يتعمل مع اليوسر 
//تعالي كدا لما نروح للميسود اللي تعرض المنيو
using ShoppingProgramming;
using System.ComponentModel;
using System.Globalization;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Serialization;

public static class InputHandler//ده الكلاس اللس بتعامل مع السور 
{

   static ECommerceStore store=new ECommerceStore("TeckStore",JsonSerializer.Deserialize<List<Product>>(File.ReadAllText("Products.json")!));
  public static void Run()
  {
        UserType userType = DisplayRoleMenu();
        if(userType==UserType.Exit)
        {
            PrintExitMessage("👋 Thank you for using TeckStore Management System!");
            return;
        }
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("\n\n👉 Please Enter Your Name: ");
        string? name=Console.ReadLine();
        if(userType==UserType.Admin)
        {

          Admin admin=new Admin(name!);
          PrintWelcomeCard(admin.Name, "Admin", admin.LoginTime);//رحب ب الادمن
          RunAdminPanel(admin);


        }
        else
        {
            Customer customer=new Customer(name!);
            PrintWelcomeCard(customer.Name, "Customer", customer.LoginTime);
            RunCustomerPanel(customer);//رحي ي تاعميل

        }
        store.SaveProductsToJson("Products.json");// حط الكمنتجات الل يف التجر اللامكتروتي اللي في الخانه يالللي بشاور عيله الاوجكت اللي ايمه  عيل الهارد



  }

    private static void RunCustomerPanel(Customer customer)//يبقي الميسود دي بتتحكم في القواءسم بتاعت العميل
    {
        CustomerChoice customerChoice;
        while(true)
        {
            Console.Clear();
            customerChoice=DisplayCustomerMenu();
            if (customerChoice == CustomerChoice.Exit)
                break;
            HandleCustomerFlow(customer, customerChoice);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\n\nPress Enter to return to the main menu...");
            Console.ResetColor();
            Console.ReadKey();
        }
        PrintExitMessage("👋 Thank you for using TeckStore Management System!");


    }
    private static void  HandleCustomerFlow(Customer customer,CustomerChoice customerChoice)//يبقي الميسود دي بتعرض القوائم بتاع العميل او اليوسر
    {
        Console.Clear();
        switch(customerChoice)
        {
            case CustomerChoice.AddItemToCart:
                (int productId, int quantity) = GetCartInputFromUser();//deconstructor
                customer.AddToCart(store,productId, quantity);
                break;
            case CustomerChoice.ViewCart:
                customer.ViewMyCart();
                break;
            case CustomerChoice.Checkout:
                customer.CheckOut();
                break;
             case CustomerChoice.Undo:
                customer.Undo(store);
                break;

        }
    }
    private static (int productId, int quantity) GetCartInputFromUser()//يبقي الميسود دي بتطلي من السور ان دخل الي دي وكميه المنتج ال يعاوز يضفه
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n==================================================================");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("                    🛒 ADD PRODUCT TO CART                        ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==================================================================\n");
        Console.ResetColor();

        int id = InputUser("👉 Enter Product ID: ", positiveOnly: true);
        int qty = InputUser("\n👉 Enter Quantity: ", positiveOnly: true);

        return (id, qty); // C# Tuple
    }

     private static  void  RunAdminPanel(Admin admin)//يبقي المسود دي تتحكم في القواىم بتاعت الادمن
      {
            AdminChoice adminChoice;
            while(true)
            {
                Console.Clear();
                adminChoice = DisplayAdminMenu();
                if (adminChoice == AdminChoice.Exit)
                    break;
                HandleAdminFlow(admin, adminChoice);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("\n\nPress Enter to return to the main menu...");
                Console.ResetColor();
                Console.ReadKey();
     
            }
            PrintExitMessage("👋 Thank you for using TeckStore Management System!");
     
     
      }

   private static void  HandleAdminFlow(Admin admin, AdminChoice adminChoice)//سيقي الميسود دي يتعرض القوائم يتاعت  الادمن 
    {
        Console.Clear();
        switch (adminChoice)
        {
            case AdminChoice.AddProduct:
                admin.AddProduct(store, GetProductInputFromUser());
                break;
            case AdminChoice.ViewProductById:
                admin.GetProductById(store,InputUser("👉 Enter Product ID: ", positiveOnly: true));
                break;
            case AdminChoice.DisplayAllProducts:
                Console.Clear();
                admin.DisplayAllProducts(store);
                break;
            case AdminChoice.RemoveProductById:
                admin.RemoveProductById(store, InputUser("👉 Enter Product ID: ", positiveOnly: true));
                break;
           
               

        }



    }
    private static Product GetProductInputFromUser()//يبقي الميسود دي بتطلي من الادمن ان يدخل الداتا لتاعت المنتج اللي هوا عاوزه يضفه في المتجر
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n==================================================================");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("                  ➕ ADD NEW PRODUCT DETAILS                      ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==================================================================\n");
        Console.ResetColor();

        string name = "";
        while (string.IsNullOrWhiteSpace(name))
        {//يبقي كدا الكميلر مش هخرج من اللوب دي غير لما سور يدخل قيمه
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("👉 Enter Product Name: ");
            Console.ResetColor();
            name = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                PrintError("\n❌ Product name cannot be empty! Please try again.\n");
            }
        }

        decimal price = InputUser("\n👉 Enter Product Price ($): ");
       
        int quantity = InputUser("\n👉 Enter Product Quantity/Stock: ", positiveOnly: true);

        return new Product( price, quantity,name);
    }














    private static void PrintWelcomeCard(string name, string role, DateTime loginTime)//يبقي الميسود دي بترحي ي اليوسر 
    {
        Console.Clear();

        // تنسيق التاريخ والوقت بشكل شيك (مثال: Sunday, Jul 26, 2026 - 12:59 AM)
        string formattedDate = loginTime.ToString("dddd, MMM dd, yyyy - hh:mm tt");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("==================================================================\n");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"   🎉 WELCOME BACK, {name.ToUpper()}!\n");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine($"   Role: [{role}] | Session Started: {formattedDate}");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n==================================================================\n\n");
        Console.ResetColor();
        for (int i = 0; i < 20; i++)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            Console.Write("▓");
            Thread.Sleep(200); // تأخير بسيط يديك إحساس اللودينج
        }
        Console.Clear();

    }



  private  static int InputUser(string message ,bool positiveOnly=false)// يبقي الميسود دي  يطبع المسدج اللي  هتسقبله ويطلم من السور ان يدخل قيمه من النوع انت 
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(message);
            int num = 0;

            while (!int.TryParse(Console.ReadLine(), out num) || (positiveOnly && num < 1))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (positiveOnly)
                    PrintError("\n[❌ Invalid input! Please enter a positive number:");
                else
                    PrintError("\n[❌ Invalid input! Please enter a valid number:");
                Console.ResetColor();
            }
        Console.ResetColor();
            return num;
        
    }
   private static void PrintExitMessage(string Message)//يبقي الميسود بتطبع الرساله اللي هي هتستقبلها في نص الشاشه بلون الاحمر
    {
        Console.Clear();
        string message = Message;
        ConsoleColor color = ConsoleColor.Red;

        int left = (Console.WindowWidth - message.Length) / 2; // الموقع الأفقي
        int top = Console.WindowHeight / 2;                     // الموقع الرأسي

        Console.SetCursorPosition(left, top);
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }








    private static UserType  DisplayRoleMenu()//يبقي الميسود دي يتعرض المنيو يتاع نوع اليوسر 
    {// دي القائمه اللي فيها المنيو بتاع نزع اليوسر 
        Console.ForegroundColor = ConsoleColor.Yellow;
        // Banner المتجر الضخم (يوضع في أول شاشة فقط)
        Console.WriteLine(@"
████████╗███████╗██╗   ██╗████████╗███████╗████████╗██████╗ ██████╗ ███╗   ██╗███████╗
╚══██╔══╝██╔════╝██║   ██║╚══██╔══╝██╔════╝╚══██╔══╝██╔══██╗██╔══██╗████╗  ██║██╔════╝
   ██║   █████╗  ██║   ██║   ██║   ███████╗   ██║   ██████╔╝██████╔╝██╔██╗ ██║█████╗  
   ██║   ██╔══╝  ██║   ██║   ██║   ╚════██║   ██║   ██╔══██╗██╔══██╗██║╚██╗██║██╔══╝  
   ██║   ███████╗╚██████╔╝   ██║   ███████║   ██║   ██║  ██║██║  ██║██║ ╚████║███████╗
   ╚═╝   ╚══════╝ ╚═════╝    ╚═╝   ╚══════╝   ╚═╝   ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝
    ");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("                             👤  SELECT USER TYPE  👤                            ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================\n");

        PrintOption(1, "🔑", "Admin (Store Owner)");
        PrintOption(2, "🛒", "Customer (Store User)");
        PrintOption(3, "🚪", "Exit Application");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================\n");
        Console.ResetColor();
        return (UserType)Input(1, 3);
    }
    private static AdminChoice DisplayAdminMenu()//يبقي الميسود دي يتعرض المنسو بتاع  الادمن
    {//دي القائمه اللي فيها المنيو بتاع الادمن
        Console.ForegroundColor = ConsoleColor.Yellow;
        // عنوان خاص بـ Admin Panel بنفس الـ Style
        Console.WriteLine(@"
 ░█████╗ ██████╗ ███╗   ███╗██╗███╗   ██╗    ██████╗ █████╗ ███╗   ██╗███████╗██╗     
██╔══██╗██╔══██╗████╗ ████║██║████╗  ██║    ██╔══██╗██╔══██╗████╗  ██║██╔════╝██║     
███████║██║  ██║██╔████╔██║██║██╔██╗ ██║    ██████╔╝███████║██╔██╗ ██║█████╗  ██║     
██╔══██║██║  ██║██║╚██╔╝██║██║██║╚██╗██║    ██╔═══╝ ██╔══██║██║╚██╗██║██╔══╝  ██║     
██║  ██║██████╔╝██║ ╚═╝ ██║██║██║ ╚████║    ██║     ██║  ██║██║ ╚████║███████╗███████╗
╚═╝  ╚═╝╚═════╝ ╚═╝     ╚═╝╚═╝╚═╝  ╚═══╝    ╚═╝     ╚═╝  ╚═╝╚═╝  ╚═══╝╚══════╝╚══════╝
    ");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("                           🛠️  ADMIN MANAGEMENT PANEL  🛠️                        ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================\n");

        PrintOption(1, "➕", "Add New Product");
        PrintOption(2, "🔍", "View Product By ID");
        PrintOption(3, "📋", "Display All Products");
        PrintOption(4, "🗑️", "Remove Product By ID");
        PrintOption(5, "🚪", "Exit To Main Menu");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================\n");
        Console.ResetColor();
        return (AdminChoice)Input(1, 5);
    }
    private static CustomerChoice DisplayCustomerMenu()//يبقي الميسود دي بتعرض المنيو بتاع اليوسر العادي
    {//دي القائمه اللي فيها المنيو بتعاع  اليوسر العادي 
        Console.ForegroundColor = ConsoleColor.Yellow;
        // عنوان خاص بـ Customer Store بنفس الـ Style
        Console.WriteLine(@"
 ░█████╗██╗   ██╗███████╗████████╗██████╗ ███╗   ███╗███████╗██████╗ 
██╔════╝██║   ██║██╔════╝╚══██╔══╝██╔══██╗████╗ ████║██╔════╝██╔══██╗
██║     ██║   ██║███████╗   ██║   ██║  ██║██╔████╔██║█████╗  ██████╔╝
██║     ██║   ██║╚════██║   ██║   ██║  ██║██║╚██╔╝██║██╔══╝  ██╔══██╗
╚█████╗ ╚██████╔╝███████║   ██║   ██████╔╝██║ ╚═╝ ██║███████╗██║  ██║
 ╚════╝  ╚═════╝ ╚══════╝   ╚═╝   ╚═════╝ ╚═╝     ╚═╝╚══════╝╚═╝  ╚═╝
    ");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================");
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("                             🛒  CUSTOMER SHOPPING CART  🛒                         ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================\n");

        PrintOption(1, "🛍️", "Add Item To Cart");
        PrintOption(2, "🛒", "View Shopping Cart");
        PrintOption(3, "💳", "Proceed To Checkout");
        PrintOption(4, "🔙", "Undo Last Action");
        PrintOption(5, "🚪", "Exit To Main Menu");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=========================================================================================\n");
        Console.ResetColor();

        return (CustomerChoice)Input(1, 5);
    
    }
    static void PrintOption(int number, string icon, string title)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("   [");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(number);
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("]  ");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{icon}  {title}\n"); // \n للسطر الفاضي بين كل اختيار
    }

    //تعالي كدا لما نعمل ميسود علشان تعرض المنيو 
   private static int Input(int from, int to)//يبقي الميسود دي تبفضب طلي من اليوسر ان يدخل رقم لحد لما يدخر رقم يقع لبين الرقمين اللي هيا هتسقبيهم 
    {
        int num = 0;

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"\n👉 Please enter a number between {from} and {to}: ");
            Console.ResetColor();
            if (int.TryParse(Console.ReadLine(), out num))
            {
                if (num >= from && num <= to)
                {
                    return num;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"\n⚠️ Out of range! The number must be between {from} and {to}.\n");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintError("\n❌ Invalid input! You must enter a valid integer number (digits only).\n");
                Console.ResetColor();
            }
        }
    }
  private  static void PrintError(string message)//يبقي الميسود دي بتصفر وبتطبع المسدج اللي هتقبله بالون الاحمر
    {
        Console.Beep();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(message);
        //Console.ResetColor();
    }
}
//تعايل كدا لما نعمل انيم علشان نخزن فيه نوع اليوسلا 
public enum UserType
{
    Admin = 1,    // أمين المكتبة / صاحب المتجر
    Customer = 2, // يوزر عادي
    Exit = 3      // الخروج من التطبيق بالكامل
}
//تعالي كدا لما نعمل انسم علشان نخزن فيه الاختيارات اللي امين المكتيه مسموحله بيها
public enum AdminChoice
{
    AddProduct = 1,       // إضافة منتج
    ViewProductById = 2,  // عرض منتج عن طريق الـ ID
    DisplayAllProducts = 3,// عرض كل المنتجات
    RemoveProductById = 4, // حذف منتج عن طريق الـ ID
    Exit = 5              // الخروج
}
//تعالي كدا لما نعمل انسم علان نخزن فيه الاخيارات اللي السوسر عادي مسموحله ببييها 
public enum CustomerChoice
{
    AddItemToCart = 1,
    ViewCart = 2,
    Checkout = 3,
    Undo = 4,
    Exit = 5
}
