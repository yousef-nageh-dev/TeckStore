namespace ShoppingProgramming;
//تعالي كدا لما نعمل  فايل علشان نحط فيه كلاس عرق التسوق
//تعالي كدا لما نفتح الملف اللي في كلاس عريه التسوق
class ShoppingCart
{
    private Stack<Product> products;

    public ShoppingCart()
    {
        this.products = products ?? new Stack<Product>();
    }
    public void AddItem(Product product)//يبقس الميسود دي يضيف المنتج اللي هيا هتقيبه في عريه التسوق
    { 
           if (product is null)
           {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("\n[Error] Cannot add a null product to the cart.\n");
               Console.ResetColor();
               return;
           }
           
           products.Push(product);
           
           Console.ForegroundColor = ConsoleColor.Green;
           Console.WriteLine($"\n[Success] Added '{product.Name}' to the cart.\n");
           Console.ResetColor();
    }
   
    public void DisplayCartItems()//يبقي الميسود دي لتعرض المنتجات الل يف عره التسوق
    {
        if( products.Count == 0 ) 
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Warning] The shopping cart is empty!");
            Console.ResetColor();
            return;
        }
        foreach( var item in products )
        {
          Console.WriteLine( item );
        }

    }
  
    public void Checkout()//يبقي الميسود دي يتحسب سعر   سعر الكلي  بتاع المنتجات اللي في عريق التسوق
    {
   
        if (products.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[❌ Error] Cannot checkout! Cart is empty!");
            Console.ResetColor();
            return;
        }

        decimal total = 0;
        foreach (var item in products)
        {
            total += item.Price * item.Quantity;
        }

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("==========================================");
        Console.WriteLine("          🎉 ORDER RECEIPT 🎉             ");
        Console.WriteLine("==========================================\n");
        Console.WriteLine($" Status       : Checkout Completed");
        Console.WriteLine($" Total Amount : {total:C2}");
        Console.WriteLine("\n==========================================\n");
        Console.ResetColor();

        products.Clear(); // تفريغ السلة بعد الشراء
    }
   public Product  Undo()// يبقي الميسود دي بتحذف ويتطب الداتا بتاعت اخر منتج في عرق التسوق
   {
    
        if (!products.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[❌ Warning] Nothing to undo! Shopping cart is empty.\n");
            Console.ResetColor();
            return null;
        }

        var removedProduct = products.Pop();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("[✔ Success] Removed last added item:\n\n");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(removedProduct);

        Console.ResetColor();
        return removedProduct;
   }



}

