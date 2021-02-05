﻿using System;

namespace eShop.BackendServer.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime CreateDate { get; set; }

        DateTime? LastModifiedDate { get; set; }
    }
}