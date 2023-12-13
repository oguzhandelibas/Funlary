using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Funlary
{
    public class User : MonoBehaviour
    {
        public string username = "User";
        public int score = 0;


        [SerializeField] private TextMeshProUGUI _username;
        [SerializeField] private TextMeshProUGUI _score;
        public TextMeshProUGUI _index;

        private void Awake()
        {
            SetInfo(username, score, 0, false);

        }

        public void SetInfo(string name, int score, int index, bool x)
        {
            _username.text = name;
            _score.text = score.ToString();
            index++;
            _index.text = "#" + index.ToString();
            gameObject.SetActive(x);
        }
    }
}
