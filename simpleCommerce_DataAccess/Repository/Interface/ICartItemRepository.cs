﻿using simpleCommerce_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleCommerce_DataAccess.Repository.Interface
{
    public interface ICartItemRepository:IRepository<CartItem>
    {
        public void Update(CartItem cartItem);
    }
}
