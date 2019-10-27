using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ShoppingCart
{
    #region Properties

    public List<CartItem> Items { get; private set; }

    #endregion

    #region Singleton Implementation

    //public static readonly ShoppingCart Instance;
    //// Hàm khởi tạo
    //static ShoppingCart()
    //{
    //    // Nếu chưa có session chưa giỏ hàng, thì khởi tạo và đưa thông tin giỏ hàng vào 
    //    // ngược lại thì lấy thông tin trong giỏ hàng
    //    if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
    //    {
    //        Instance = new ShoppingCart();
    //        Instance.Items = new List<CartItem>();
    //        HttpContext.Current.Session["ASPNETShoppingCart"] = Instance;
    //    }
    //    else
    //    {
    //        Instance = (ShoppingCart)HttpContext.Current.Session["ASPNETShoppingCart"];
    //    }
    //}

    // The static constructor is called as soon as the class is loaded into memory
    //public static ShoppingCart GetShoppingCart()
    //{
    //    // If the cart is not in the session, create one and put it there
    //    if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
    //    {
    //        ShoppingCart cart = new ShoppingCart();
    //        //cart.Items = new List();
    //        cart.Items = new List<CartItem>();
    //        HttpContext.Current.Session["ASPNETShoppingCart"] = cart;
    //    }

    //    return (ShoppingCart)HttpContext.Current.Session["ASPNETShoppingCart"];
    //}

    public static ShoppingCart Instance
    {
        get
        {

            if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
            {

                // we are creating a local variable and thus

                // not interfering with other users sessions

                ShoppingCart instance = new ShoppingCart();
              
                instance.Items = new List<CartItem>();
               // instance.Items = new List<CartItem>();
                HttpContext.Current.Session["ASPNETShoppingCart"] = instance;

                return instance;

            }
            else
            {

                // we are returning the shopping cart for the given user

                return (ShoppingCart)HttpContext.Current.Session["ASPNETShoppingCart"];

            }

        }
    }
     
    protected ShoppingCart() { }

    #endregion

    #region Item Modification Methods
    /**
	 * AddItem() - Add một item vào trong giỏ hàng
	 */
    public void AddItem(int productId, string strSize, int userPu)
    {
        // Tạo mới một Cartitem
        CartItem newItem = new CartItem(productId, userPu)
        {
            QSize = strSize,
            userP = userPu
        };
        if (Items.Contains(newItem))
        {
            foreach (CartItem item in Items)
            {
                if (item.Equals(newItem))
                {
                    item.Quantity++;
                    return;
                }
            }
        }
        else
        {
            newItem.Quantity = 1;
            Items.Add(newItem);
        }
    }

    /**
     * SetItemQuantity() - Thay đổi số lượng của sản phẩm trong giỏ hàng
     */
    public void SetItemQuantity(int productId, int quantity, string strSZ, int userPu)
    {
        // Nếu số lượng bằng 0 thì xóa item
        if (quantity == 0)
        {
            RemoveItem(productId, userPu);
            return;
        }

        // Tìm và update số lượng cho item trong giỏ hàng
        CartItem updatedItem = new CartItem(productId, userPu);
        foreach (CartItem item in Items)
        {
            if (item.Equals(updatedItem))
            {
                item.QSize = strSZ;
                //item.userP = userPu;
                item.Quantity = quantity;
                return;
            }
        }
    }
    public void RemoveAll()
    {
        Items.Clear();
    }
    /**
     * RemoveItem() - Xóa item trong giỏ hàng
     */
    public void RemoveItem(int productId, int userId)
    {
        CartItem removedItem = new CartItem(productId, userId);
        Items.Remove(removedItem);
    }
    #endregion

    #region Reporting Methods
    /**
	 * GetSubTotal() - Tính tổng tiền
	 */
    public decimal GetSubTotal()
    {
        decimal subTotal = 0;
        foreach (CartItem item in Items)
        {
            subTotal += item.TotalPrice;
        }
        return subTotal;
        //return (subTotal+10);
    }
    #endregion
}