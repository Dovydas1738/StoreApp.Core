﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Core.Contracts
{
    public interface ICacheService
    {
        Task DropCaches();
    }
}
