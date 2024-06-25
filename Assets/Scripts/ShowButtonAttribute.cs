using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
public class ShowButtonAttribute : SpaceAttribute
{
    public readonly string ButtonText;
    public ShowButtonAttribute(string buttonText)
    {
        ButtonText = buttonText;
    }

    public ShowButtonAttribute()
    {
        ButtonText = "Press";
    }
}
