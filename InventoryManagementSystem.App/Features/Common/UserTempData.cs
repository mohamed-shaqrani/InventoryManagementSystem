﻿using InventoryManagementSystem.App.Entities;

namespace InventoryManagementSystem.App.Features.Common;
public class UserTempData
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string Phone { get; set; }
    public string ImagePath { get; set; }

    public Role Role { get; set; }
}
