

using System.Diagnostics;

namespace ShoppingProgramming;

//تعالي كدا لما نفتح الملف اللي فيه كلاس المنتجات 
//تعالي كدا لما نعمل كلاس للمنتجات
//تعالكي كدا لما نخبي الكلاس ده يعمل اميلتشن لاتنرفيس اللي اسمه علشان نقدر نقارن الاوجكت اللي احنا هناكلايتهم من الكلاس ده ييعض 
//تعالي كدا لما نعمل تيست لكلاس ده
//تعالي كدا لما نعمل ديبجر دس يبلي ببكبااس ده
[DebuggerDisplay("Id : {Id} Price {Price} Quantity : {Quantity} Name : {Name} ")]
public class Product : IEquatable<Product>, IComparable<Product>
{
    private static int _nextId = 1;
    private int id;
    private decimal price;
    private int quantity;
    private string name;
    public int Id
    {
        get => id;
        private set => id = value >= 1 ? value : 1;
    }
    public decimal Price
    {
        get => price;
        set => price = value >= 1.1m ? value : 1.1m;
    }
    public int Quantity
    {
        get => quantity;
        set => quantity = value >= 0 ? value : 0;
    }
    public string Name
    {
        get => name;
        set => name = !string.IsNullOrWhiteSpace(value) ? value : "UnDefine";
    }

    public Product( decimal price, int quantity, string name)
    {
        Id = _nextId++;
        Price = price;
        Quantity = quantity;
        Name = name;
    }
    public override int GetHashCode()//يبقي الميسود دي بترجع هاش فالو هو هاش كود للااوجكتت
    {
        return HashCode.Combine(Id);
    }
    public static bool operator ==(Product? product1, Product? product2)
    {
        if (ReferenceEquals(product1, product2)) return true;
        if (product1 is null || product2 is null) return false;
        return product1.Equals(product2);
    }
    public static bool operator !=(Product? product1, Product? product2) => !(product1 == product2);

    public override bool Equals(object? obj)//يبقي الميسود دي بترجع ترو لو الداتا لتاعت المنتج اللي هها هتسقبها يتاوس الداتا بتاعت المنتج ده 
    {
        if (obj is null || obj is not Product)
            return false;
        return Equals((Product)obj);
    }

    public bool Equals(Product? other)//يبقي الميسود دي بترجع ترو لو الداتا بتاعت المنتج اللي هيا هتسقبله يتساوي الداتا بتاعت االمنتج ده
    {
        if (other is null) return false;
        return (other.Id == Id);
    }

    public int CompareTo(Product? other)
    {
        if (other is null)
            return 1;
        return Id.CompareTo(other.Id);
    }
    public override string ToString()//يبقي الكميسود دي لترجع الداتا بتاعت المنتج  بكل احتراف
    {


        Console.ForegroundColor= ConsoleColor.DarkBlue;
        const int totalWidth = 62;
        const int labelWidth = 12;
        const int valueWidth = totalWidth - labelWidth - 3;

        string price = Price.ToString("C2");

        return
            $"┌{new string('─', totalWidth)}┐\n" +
            $"│{"\t\tPRODUCT DETAILS",-(totalWidth-13)}│\n" +
            $"├{new string('─', totalWidth)}┤\n" +
            $"│ {"ID",-labelWidth}: {Id,-valueWidth}│\n" +
            $"│ {"Name",-labelWidth}: {Name,-valueWidth}│\n" +
            $"│ {"Price",-labelWidth}: {price,-valueWidth}│\n" +
            $"│ {"Quantity",-labelWidth}: {Quantity,-valueWidth}│\n" +
            $"└{new string('─', totalWidth)}┘";
       Console.ResetColor();
    }



}
    
    





