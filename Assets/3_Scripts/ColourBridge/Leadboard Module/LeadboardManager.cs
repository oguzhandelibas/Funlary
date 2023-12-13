using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Funlary
{
    public class LeadboardManager : AbstractSingleton<LeadboardManager>
    {
        public User[] users;
        
        void Start()
        {
            for (var i = 0; i < users.Length; i++)
            {
                users[i]._index.text = "#" + (i + 1).ToString();
            }
        }
    }
}
