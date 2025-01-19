namespace AgriMarket.Models.ViewModels
{
    
        public class OrderDetailViewModel
        {
            public int Id { get; set; }  
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string? Status { get; set; }
            public DateTime OrderDate { get; set; }
            public List<Product> Products { get; set; }

            public class Product
            {
                public int Id { get; set; } 
                public string ProductName { get; set; }
                public decimal Price { get; set; }
                public int Quantity { get; set; } 
                public int OrderProductId { get; set; }
                public OrderProduct OrderProduct { get; set; }
            }
        }
    }

