namespace ShoppingProgramming;

public class Customer : User
{
    //الاتريوس بتاع الكلاس ده هيقي عياره عن اوجكت من النوع علشان اخزن فيه عريق التسوق يتاعت اليورس  
    private readonly ShoppingCart cart;
    public Customer(string name) : base(name)
    {
        cart = new ShoppingCart();

    }

    public void AddToCart(ECommerceStore store, int id, int quantity)//يبقي الميسود دي يتسحب كميه   اللي هيتعا من التجر الايكتلاوني اللي هيا هتيقبله وتحطها في عريف التوق بتاعت اليور 
    {
        Product product = store.FindProductById(id);
        if (product is null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[❌ Error] Product with ID '{id}' was not found in the store!\n");
            Console.ResetColor();
            return;
        }
        
        if (quantity <= 0 || quantity > product.Quantity)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[❌ Error] Invalid quantity requested! Available stock is: {product.Quantity}\n");
            Console.ResetColor();
            return;

        }
        product.Quantity -= quantity;
        cart.AddItem(new Product( product.Price, quantity, product.Name));


        
    }
    public void ViewMyCart()//يبقي المسود دي بتعرض المتتجات اللي في عرق التس
    {//يبقي انا كدا عاوز الميسود دي تعرض المنتجات الل يف عربه التسوق اللي في الحانه االلي بشاور عليه الاوجكت اللي اسمه 
    cart.DisplayCartItems();
    }
    public void Undo(ECommerceStore store)//يبقي انا كدا عاوز الميسود دي تعمل ان دول للعرق التسوق اللي في الخانه اللي يشاور عليه الاوجكت اللي اسمه 
    {
    
        Product removedProduct = cart.Undo();

        // Guard Clause: لو السلة كانت فاضية و مفيش حاجة اتلغت
        if (removedProduct is null)
        {
            return;
        }

        // 2. ندور على المنتج في المتجر عشان نرجعله الكمية
        Product storeProduct = store.FindProductById(removedProduct.Id);

        // لو المنتج موجود في المتجر نرجع الكمية
        if (storeProduct is not null)
        {
            storeProduct.Quantity += removedProduct.Quantity;
        }
    

    }
    public void CheckOut()
    {
        cart.Checkout();
    }



}
