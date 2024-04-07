﻿using Infrastructure.Entity.BaseEntity;
using Infrastructure.Entity.User;

namespace Infrastructure.Entity.product;

public class productSelles:BaseEntity<int>
{
    public string NameProduct { get; set; }

    public string Emile { get; set; }
    
    public string Phone { get; set; }
    
    public bool IsAvailable  { get; set; }
    
    public DateTime ProductDate { get; set; }
    
    public loginUser users { get; set; }
    
    public long userId { get; set; }
}