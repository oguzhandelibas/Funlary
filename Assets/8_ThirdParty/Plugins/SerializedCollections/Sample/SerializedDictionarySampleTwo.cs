using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    public class SerializedDictionarySampleTwo : MonoBehaviour
    {
        [SerializedDictionary("ownerID", "Person")]
        public SerializedDictionary<int, Person> People;

        [System.Serializable]
        public class Person
        {
            public string FirstName;
            public string LastName;
        }
    }
}