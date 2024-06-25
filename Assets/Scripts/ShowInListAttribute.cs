using System;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ShowInListAttribute : PropertyAttribute
{
    public List<object> list;
    public ShowInListAttribute()
    {
        
    }
}
