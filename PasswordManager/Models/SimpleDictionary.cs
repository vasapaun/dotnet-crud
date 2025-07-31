namespace PasswordManager.Models;

using System;
using System.Collections.Generic;

public class SimpleDictionary<TKey, TValue>
{
    private const int DefaultCapacity = 16;
    private LinkedList<KeyValuePair<TKey, TValue>>?[] _buckets;

    public SimpleDictionary()
    {
        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[DefaultCapacity];
    }

    private int GetBucketIndex(TKey key)
    {
        return Math.Abs(key.GetHashCode()) % _buckets.Length;
    }

    public void Add(TKey key, TValue value)
    {
        int bucketIndex = GetBucketIndex(key);

        if (_buckets[bucketIndex] == null)
            _buckets[bucketIndex] = new LinkedList<KeyValuePair<TKey, TValue>>();

        foreach (var pair in _buckets[bucketIndex])
        {
            if (pair.Key != null && pair.Key.Equals(key))
                throw new ArgumentException("Key already exists");
        }

        _buckets[bucketIndex].AddLast(new KeyValuePair<TKey, TValue>(key, value));
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        int bucketIndex = GetBucketIndex(key);
        value = default!;
        if (_buckets[bucketIndex] == null)
            return false;

        foreach (var pair in _buckets[bucketIndex])
        {
            if (pair.Key.Equals(key))
            {
                value = pair.Value;
                return true;
            }
        }

        return false;
    }
    
    public bool TrySetValue(TKey key, TValue value)
    {
        int bucketIndex = GetBucketIndex(key);
        var bucket = _buckets[bucketIndex];
        if (bucket == null) return false;

        var node = bucket.First;
        while (node != null)
        {
            if (node.Value.Key.Equals(key))
            {
                node.Value = new KeyValuePair<TKey, TValue>(key, value);
                return true;
            }
            node = node.Next;
        }

        return false;
    }

    public void Update(TKey key, TValue value)
    {
        int bucketIndex = GetBucketIndex(key);
        if (_buckets[bucketIndex] == null)
            throw new ArgumentException("User does not exist.");

        foreach (var pair in _buckets[bucketIndex])
        {
            if (pair.Key.Equals(key))
            {
                TrySetValue(key, value);
            }
        }
    }

    public bool Remove(TKey key)
    {
        int bucketIndex = GetBucketIndex(key);
        if (_buckets[bucketIndex] == null)
            return false;
        foreach (var pair in _buckets[bucketIndex])
        {
            if (pair.Key.Equals(key))
            {
                _buckets[bucketIndex].Remove(pair);
                return true;
            }
        }

        return true;
    }

    public bool UserExists(TKey key)
    {
        int bucketIndex = GetBucketIndex(key);
        
        if(_buckets[bucketIndex] == null)
            return false;
        
        foreach (var pair in _buckets[bucketIndex])
        {
            if (pair.Key != null && pair.Key.Equals(key))
            {
                return true;
            }
        }

        return false;
    }
    
    public List<KeyValuePair<TKey, TValue>> ToList()
    {
        List<KeyValuePair<TKey, TValue>> list = new();

        foreach (var bucket in _buckets)
        {
            if (bucket == null) continue;
            
            foreach (var pair in bucket)
            {
                list.Add(new KeyValuePair<TKey, TValue>(pair.Key, pair.Value));
            }
        }

        return list;
    }
    
}