﻿using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.App.Entities;
public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public bool IsAuthenticated { get; set; }

    public DateTime PasswordResetCodeExpiration { get; set; }
    [StringLength(6)]
    public string? PasswordResetCode { get; set; } = string.Empty;
}

