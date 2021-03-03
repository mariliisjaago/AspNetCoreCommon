﻿using DbAccess_Library.Contracts.Repos;
using DbAccess_Library.Contracts.Strategies;

namespace DbAccess_Library.Strategies
{
    public class DeleteOrder : IDeleteOrder
    {
        private readonly IOrdersRepo _ordersRepo;

        public DeleteOrder(IOrdersRepo ordersRepo)
        {
            _ordersRepo = ordersRepo;
        }

        public void Delete(int id)
        {
            _ordersRepo.Delete(id);


        }
    }
}
