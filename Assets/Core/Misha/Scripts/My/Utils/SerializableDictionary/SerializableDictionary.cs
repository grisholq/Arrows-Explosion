using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DCFAEngine
{
    public abstract class SerializableDictionary { }

    [Serializable]
    public class SerializableDictionary<TKey, TValue> : SerializableDictionary, ISerializationCallbackReceiver, IDictionary<TKey, TValue>/*, IDictionary*/
    {
        [Serializable]
        private class SerializableKeyValuePair
        {
            public TKey Key;
            public TValue Value;

            public SerializableKeyValuePair(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public void SetValue(TValue value) => Value = value;
        }

        [SerializeField]
        private List<SerializableKeyValuePair> pairs = new List<SerializableKeyValuePair>();
        private Dictionary<TKey, int> keyIndexes = new Dictionary<TKey, int>();

        #region ISerializationCallbackReceiver
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            int count = pairs.Count;
            keyIndexes.Clear();
            for (int i = 0; i < count; i++)
                if (pairs[i].Key != null)
                    keyIndexes[pairs[i].Key] = i;

            if (!typeof(TValue).IsSubclassOf(typeof(UnityEngine.Object)))
            {
                foreach (var pair in this)
                {
                    Type type = pair.Value.GetType();
                    FieldInfo[] fields = type.GetFields();

                    for (int i = 0; i < fields.Length; i++)
                    {
                        DictionaryKeyAttribute keyAttributes = fields[i].GetCustomAttribute<DictionaryKeyAttribute>(true);
                        if (keyAttributes != null)
                        {
                            fields[i].SetValue(pair.Value, pair.Key);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
        #endregion

        #region IDictionary
        public TValue this[TKey key]
        {
            get => pairs[keyIndexes[key]].Value;
            set
            {
                if (keyIndexes.TryGetValue(key, out int index))
                    pairs[index].SetValue(value);
                else
                    Add(key, value);
            }
        }
        public ICollection<TKey> Keys => keyIndexes.Keys;
        public ICollection<TValue> Values => (ICollection<TValue>)pairs.Select(pair => pair.Value); //Не раотает InvalidCastException: Specified cast is not valid.

        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException("ключ не должен быть равен null");

            if (ContainsKey(key))
                throw new ArgumentException("этот ключ уже содержится в списке");

            keyIndexes[key] = pairs.Count;
            pairs.Add(new SerializableKeyValuePair(key, value));
        }
        public bool ContainsKey(TKey key) => keyIndexes.ContainsKey(key);
        public bool Remove(TKey key)
        {
            if (keyIndexes.TryGetValue(key, out int index))
            {
                keyIndexes.Remove(key);

                pairs.RemoveAt(index);
                for (int i = index; i < pairs.Count; i++)
                    keyIndexes[pairs[i].Key] = i;

                return true;
            }
            return false;
        }
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (keyIndexes.TryGetValue(key, out int index))
            {
                value = pairs[index].Value;
                return true;
            }

            value = default;
            return false;
        }
        #endregion

        #region ICollection
        public int Count => pairs.Count; 
        public bool IsReadOnly => false;

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            Add(keyValuePair.Key, keyValuePair.Value);
        }
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return keyIndexes.ContainsKey(item.Key);
        }
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("аргумент array является null");

            if (array.Length - arrayIndex < pairs.Count)
                throw new ArgumentException("выходит за пределы");

            for (int i = 0; i < pairs.Count; i++)
            {
                SerializableKeyValuePair item = pairs[i];
                array[arrayIndex + i] = new KeyValuePair<TKey, TValue>(item.Key, item.Value);
            }
        }
        public void Clear()
        {
            pairs.Clear();
            keyIndexes.Clear();
        }
        #endregion

        #region IEnumerable
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in pairs)
                yield return new KeyValuePair<TKey, TValue>(item.Key, item.Value);
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();        
        #endregion
    }
}