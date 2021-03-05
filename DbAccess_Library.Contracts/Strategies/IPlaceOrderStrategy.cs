using DbAccess_Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess_Library.Contracts.Strategies
{
    public interface IPlaceOrderStrategy
    {
        Task<int> Place(OrderModel order);
    }
}
