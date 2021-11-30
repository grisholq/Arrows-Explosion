using System.Collections.Generic;
using UnityEngine;

namespace DCFAEngine
{
    public class KeyValueNamesAttribute : PropertyAttribute
    {
        public struct KeyValueNames
        {
            public readonly string keyName;
            public readonly string valueName;

            public KeyValueNames(string keyName, string valueName)
            {
                this.keyName = keyName;
                this.valueName = valueName;
            }
        }

        public string keyName => keyValueNamesArray[0].keyName;
        public string valueName => keyValueNamesArray[0].valueName;

        public KeyValueNames keyValueNames => keyValueNamesArray[0];
        public KeyValueNames[] keyValueNamesArray;

        public KeyValueNamesAttribute(string keyName, string valueName) : this(new KeyValueNames(keyName, valueName)) { }

        public KeyValueNamesAttribute(string keyName1, string valueName1, string keyName2, string valueName2) : 
            this(new KeyValueNames(keyName1, valueName1), new KeyValueNames(keyName2, valueName2)) { }

        public KeyValueNamesAttribute(params KeyValueNames[] names)
        {
            keyValueNamesArray = new KeyValueNames[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                keyValueNamesArray[i] = names[i];
            }
        }
    }
}