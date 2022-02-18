using OnlineShop.Db.Models;
using OnlineShopWeb.Models;
using System.Collections.Generic;

namespace OnlineShopWeb.Helpers
{
    public static class Mapping
    {
        public static List<ProductViewModel> ToProductViewModels (List<Product> dbProducts)
        {
            var productViewModels = new List<ProductViewModel>();
            foreach (var dbProduct in dbProducts)
            {
                productViewModels.Add(ToProductViewModel(dbProduct));
            }
            return productViewModels;
        }

        public static ProductViewModel ToProductViewModel(Product dbProduct)
        {
            var productViewModel = new ProductViewModel
                {
                    Id = dbProduct.Id,
                    Name = dbProduct.Name,
                    ImagePath = dbProduct.ImagePath,
                    Cost = dbProduct.Cost,
                    Descriprion = dbProduct.Descriprion,
                    ConcurrencyToken=dbProduct.ConcurrencyToken
                };
            return productViewModel;
        }

        public static CartViewModel ToCartViewModel (Cart dbCart)
        {
            if (dbCart == null)
                return null;
            var cartViewModel = new CartViewModel
            {
                Id = dbCart.Id,
                UserId = dbCart.UserId,
                Items = new List<CartItemViewModel>()
            };
            foreach (var cartItem in dbCart.Items)
            {
                cartViewModel.Items.Add(new CartItemViewModel
                {
                    Id = cartItem.Id,
                    Count = cartItem.Count,
                    Product = ToProductViewModel(cartItem.Product)
                });
            }
            return cartViewModel;
        }

        public static FavoritesViewModel ToFavoritesViewModel (Favorites dbFavorites)
        {
            if (dbFavorites == null)
                return null;
            var favoritesViewModel = new FavoritesViewModel
            {
                Id = dbFavorites.Id,
                UserId = dbFavorites.UserId,
                products = new List<ProductViewModel>()
            };

            foreach (var product in dbFavorites.Products)
            {
                favoritesViewModel.products.Add(ToProductViewModel(product));
            }
            return favoritesViewModel;
        }

        public static ProductCompareViewModel ToProductCompareViewModel(ProductCompare dbProductCompare)
        {
            if (dbProductCompare == null)
                return null;
            var productCompareViewModel = new ProductCompareViewModel
            {
                Id = dbProductCompare.Id,
                UserId = dbProductCompare.UserId,
                products = new List<ProductViewModel>()
            };

            foreach (var product in dbProductCompare.Products)
            {
                productCompareViewModel.products.Add(ToProductViewModel(product));
            }
            return productCompareViewModel;
        }

        public static List<OrderViewModel> ToOrderViewModels (List<Order> dbOrders)
        {
            var ordersViewModels = new List<OrderViewModel>();
            foreach (var dbOrder in dbOrders)
            {
                ordersViewModels.Add(ToOrderViewModel(dbOrder));
            }
            return ordersViewModels;
        }

        public static OrderViewModel ToOrderViewModel(Order dbOrder)
        {
            if (dbOrder == null)
                return null;
            var orderViewModel = new OrderViewModel
            {
                Id = dbOrder.Id,
                UserId = dbOrder.UserId,
                CreateTime = dbOrder.CreateTime,
                OrderState = ToOrderStatesViewModel(dbOrder.OrderState),
                OrderDetails = new List<OrderDetailsViewModel>(),
                OrderShippingDetails = new OrderShippingDetails()
            };

            foreach (var orderDetail in dbOrder.OrderDetails)
            {
                orderViewModel.OrderDetails.Add(ToOrderDetailsViewModel(orderDetail));
            }
            return orderViewModel;
        }

        public static OrderStatesViewModel ToOrderStatesViewModel (OrderStates dbOrderStates)
        {
            var orderStatesViewModel = (OrderStatesViewModel)dbOrderStates;
            return orderStatesViewModel;
        }

        public static OrderDetailsViewModel ToOrderDetailsViewModel (OrderDetails dbOrderDetails)
        {
            var orderDetailsViewModel = new OrderDetailsViewModel
            {
                Id = dbOrderDetails.Id,
                ProductAmount = dbOrderDetails.ProductAmount,
                ProductCost = dbOrderDetails.ProductCost,
                ProductId = dbOrderDetails.ProductId,
                ProductName = dbOrderDetails.ProductName
            };
            return orderDetailsViewModel;
        }

        public static UserViewModel ToUserViewModel(User user)
        {
            var userViewModel = new UserViewModel
            {
                FirstName = user.FirstName,
                LastName=user.LastName,
                Phone=user.Phone,
                UserName=user.UserName
            };
            return userViewModel;
        }

        public static EditUser ToEditUser(User user)
        {
            var editUser = new EditUser
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                UserName = user.UserName
            };
            return editUser;
        }

    }
}
