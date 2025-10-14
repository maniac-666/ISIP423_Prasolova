using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Person
{
    private string_name;
    private int_age;
    private string_contactInfo;

    public string Name
    {
        get => _name;
        set => _name = !
string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Имя не может быть пустым. Напишите банан.")
    }
}