using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
using System.Data;
using System.Data.SqlClient;
using CarlisleWeb;

public class Product
{

    public int Id { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public bool IsSize { get; set; }

    public Product(int id)
    {
        this.IsSize = false;
        string ConnStr = connectDB.GetconnStr(0, 0);
        this.Id = id;
        string dbo2sp_login = @"select CategoryID, Titles, Notes, Bodys, Pricea, Priceb, ImgUrl,IsSize from products where ProductID=@ProductID and IsActive=@IsActive";
        using (SqlConnection conn = new SqlConnection(ConnStr))
        {
            SqlCommand mycommand = new SqlCommand(dbo2sp_login, conn);
            mycommand.CommandType = CommandType.Text;
            mycommand.Parameters.AddWithValue("@ProductID", id);
            mycommand.Parameters.AddWithValue("@IsActive", true);

            mycommand.Connection.Open();
            SqlDataReader dr = mycommand.ExecuteReader();

            if (dr.Read())
            {
                if (!Convert.IsDBNull(dr["Pricea"]))
                {
                    this.Price = Convert.ToDecimal(dr["Pricea"]);
                }
                this.Description = "" + dr["Notes"];
                this.Name = "" + dr["Titles"];
                if (!Convert.IsDBNull(dr["ImgUrl"]))
                {
                    this.Image = "" + dr["ImgUrl"];
                }
                if (!Convert.IsDBNull(dr["IsSize"]))
                {
                    this.IsSize = Convert.ToBoolean(dr["IsSize"]);
                }
                
            }
            else
            {

            }
            dr.Close();
            mycommand.Dispose();
            conn.Close();
            conn.Dispose();
        }

    }

}