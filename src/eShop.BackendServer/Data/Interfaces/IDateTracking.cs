﻿using System;

namespace eShop.BackendServer.Data.Interfaces
{
    public interface IDateTracking
    {
        string ModifiedBy { get; set; }
        string CreatedBy { get; set; }
        DateTime CreateDate { get; set; }

        DateTime? LastModifiedDate { get; set; }
    }
}