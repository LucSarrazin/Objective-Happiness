using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vilager : MonoBehaviour
{
    [Header("Vilager Parameters")]
    public string name;
    [SerializeField] public class VariableHolder
    {
        public string var1 = "Villager";
        public string var2 = "Vagabond";
        public string var3 = "Teste";
    }

    public VariableHolder instance = new VariableHolder();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
