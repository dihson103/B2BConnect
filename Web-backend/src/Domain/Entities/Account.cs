﻿using System.ComponentModel.DataAnnotations.Schema;
using Domain.Abstractioins.Enities;

namespace Domain.Entities;
public class Account : EntityBase<int>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsAdmin { get; set; } = false;
    public Business Business { get; set; }
    private Account()
    {
    }
}
