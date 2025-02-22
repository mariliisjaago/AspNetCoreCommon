﻿using DbAccess_Library.Models;
using System.Threading.Tasks;

namespace DbAccess_Library.Contracts.Strategies
{
    public interface IGetOrderStrategy
    {
        Task<(OrderModel Order, string FoodTitle)> GetOrderAndFoodTitle(int id);
        Task<OrderModel> Get(int id);
    }
}