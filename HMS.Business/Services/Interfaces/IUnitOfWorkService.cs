﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Business.Services.Interfaces
{
    public interface IUnitOfWorkService
    {
        public IUserService userService { get; }
    }
}
