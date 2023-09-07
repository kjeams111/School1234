using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericContainer<T>
{
    private T[] items;
    private int currentindex = 0;
    
    public GenericContainer(int capacity)
    {
        items = new T[capacity];
    }

    public void Add (T item)
    {
        if(currentindex <  items.Length)
                {
            items[currentindex] = item;
            currentindex++;
        }
    }

    public T[] GetItems()
    {
        return items;
    }
   
    
}
