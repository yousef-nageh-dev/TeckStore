using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace ShoppingProgramming;
public class ECommerceStore : IEnumerable<Product>
{
  public string Nmae { get; set; }

  private List<Product> products;

    public ECommerceStore(string nmae, List<Product> products)
    {
        Nmae = nmae;
        this.products = products??new List<Product>();
    }
   public  void AddProduct(Product product)
   {//تعالي كدا لما نعمل تيست للميسود دي 
       if(product == null) throw new ArgumentNullException(nameof(product));
         foreach (var item in products)
         {
             if (item ==product)
             {
                 item.Price = product.Price;
                 item.Quantity += product.Quantity;
                 return;
   
             }
         }
         products.Add(product);
   
   
   }
   public  void RemoveProduct(int id)
   {


      
        if (products.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n⚠️ The inventory is completely empty!");
            Console.ResetColor();
            return;
        }

        Product product = FindProductById(id);
        if (product == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n❌ Product with ID ({id}) was not found!");
            Console.ResetColor();
            return;
        }
        products.Remove(product);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n✅ Product '{product.Name}' (ID: {id}) has been removed successfully!");
        Console.ResetColor();
    }




    public void DisplayProducts()//يقفي المسود دي يتعرض المنتجات الل يف المتجر الاكتروني 
   {
        Console.Clear();

        Console.Clear();

        if (OperatingSystem.IsWindows())
        {
            Console.BufferHeight = Console.WindowHeight;
        }
    
        if (products.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n[Notice] Query returned 0 records. Store is currently empty.\n");
            Console.ResetColor();
            return;
        }

        const int idWidth = 6;
        const int nameWidth = 28;
        const int priceWidth = 15;
        const int quantityWidth = 10;

        Console.ForegroundColor = ConsoleColor.DarkCyan;

        Console.WriteLine();
        Console.WriteLine($"┌{"".PadLeft(idWidth, '─')}┬{"".PadLeft(nameWidth, '─')}┬{"".PadLeft(priceWidth, '─')}┬{"".PadLeft(quantityWidth, '─')}┐");

        Console.Write("│");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{"ID",-idWidth}");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("│");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{"Product Name",-nameWidth}");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("│");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{"Price",priceWidth}");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write("│");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"{"Quantity",-quantityWidth}");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("│");

        Console.WriteLine($"├{"".PadLeft(idWidth, '─')}┼{"".PadLeft(nameWidth, '─')}┼{"".PadLeft(priceWidth, '─')}┼{"".PadLeft(quantityWidth, '─')}┤");

        foreach (var product in products)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("│");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{product.Id,-idWidth}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("│");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{product.Name,-nameWidth}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("│");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{product.Price.ToString("C"),priceWidth}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("│");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"{product.Quantity,-quantityWidth}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("│");
        }

        Console.WriteLine($"└{"".PadLeft(idWidth, '─')}┴{"".PadLeft(nameWidth, '─')}┴{"".PadLeft(priceWidth, '─')}┴{"".PadLeft(quantityWidth, '─')}┘");

        Console.ForegroundColor = ConsoleColor.DarkGray;
        //Console.WriteLine($"\n({products.Count} row{(products.Count > 1 ? "s" : "")} returned)");

        Console.ResetColor();
   }



    public Product FindProductById(int id)
    { 
        foreach (var item in products)
        {
            if (item.Id == id)
                return item;
        }
        return null;
    }

    public void GetProductById(int id)
    {
        Product product = FindProductById(id);

        if (product is null)
        {
            // رسالة تنبيه ملونة بألوان الـ Box-drawing
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n┌──────────────────────────────────────────────────────────┐");
            Console.WriteLine($"│  [×] Error: Product with ID ({id}) was not found!         │");
            Console.WriteLine("└──────────────────────────────────────────────────────────┘");
            Console.ResetColor();
        }
        else
        {
            Console.WriteLine(product);
        }
    }
    public void SaveProductsToJson(string path)
    {
      File.WriteAllText(path, JsonSerializer.Serialize(products,new JsonSerializerOptions {WriteIndented=true } ));
    }






    public IEnumerator<Product> GetEnumerator()
    {
        return products.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
