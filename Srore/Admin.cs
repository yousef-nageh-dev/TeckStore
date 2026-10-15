namespace ShoppingProgramming;

//تعالي كدا لما نعمللا كلاس لادمن
class Admin : User
{
    public Admin(string name) : base(name) { }

    public void AddProduct(ECommerceStore store,Product product)
    {
        store.AddProduct(product);
    }
    public void GetProductById(ECommerceStore store,int id)
    {
        store.GetProductById(id);

    }
    public void DisplayAllProducts(ECommerceStore store)
    {

        store.DisplayProducts();


    }
    public void RemoveProductById(ECommerceStore store,int id)
    {
        store.RemoveProduct(id);
    }







}
