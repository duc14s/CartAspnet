using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class CartItem : IEquatable<CartItem>
{
    #region Properties
    // Tạo thuộc tính getter và setter.
    public int Quantity { get; set; }
    public string QSize { get; set; }
    public int userP { get; set; }

    private int _productId;
    public int ProductId
    {
        get { return _productId; }
        set
        {
            _product = null;
            _productId = value;
        }
    }
    private bool _IsSize;
    public bool IsSize
    {
        get { return _IsSize; }
        set
        {
            _IsSize = value;
        }
    }

    private Product _product = null;
    public Product Prod
    {
        get
        {
            if (_product == null)
            {
                _product = new Product(ProductId);
            }
            return _product;
        }
    }

    public string Description
    {
        get { return Prod.Description; }
    }

    public string Name
    {
        get { return Prod.Name; }
    }
    public string Image
    {
        get { return Prod.Image; }
    }


    public decimal UnitPrice
    {
        get { return Prod.Price; }
    }

    public decimal TotalPrice
    {
        get { return UnitPrice * Quantity; }
    }

    #endregion
    public CartItem(int productId, int userId)
    {
        this.ProductId = productId;
        //this.userP = userId;
    }

    public bool Equals(CartItem item)
    {
        return item.ProductId == this.ProductId;
    }
}