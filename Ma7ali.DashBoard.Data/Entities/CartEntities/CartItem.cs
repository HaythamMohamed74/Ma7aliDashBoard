
namespace Ma7ali.DashBoard.Data.Entities.CartEntities;
public class CartItem : BaseEntity
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImgUrl { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int CartId { get; set; }
    public Cart Cart { get; set; }
}