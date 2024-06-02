using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TESTING
{
    public class VariableStoreTesting : MonoBehaviour
    {
        public int var_int = 0;
        public float var_flt = 0;
        public bool var_bool = false;
        public string var_str = "";


        private void Start()
        {
            VariableStore.CreateDatabase("DB_Links");

            VariableStore.CreateVariable("L_int", var_int, () => var_int, value => var_int = value);

            VariableStore.CreateDatabase("DB_Numbers");
            VariableStore.CreateDatabase("DB_Booleans");

            VariableStore.CreateVariable("DB_Numbers.num1", 1);
            VariableStore.CreateVariable("DB_Numbers.num5", 5);
            VariableStore.CreateVariable("DB_Booleans.lightIsOn", true);
            VariableStore.CreateVariable("DB_Numbers.float1", 7.5f);
            VariableStore.CreateVariable("str1", "Hello");
            VariableStore.CreateVariable("str2", "World");
            

            VariableStore.PrintAllDatabases();

            VariableStore.PrintAllVariables();  
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Q))
            {
                VariableStore.PrintAllVariables();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                string variable = "DB_Numbers.num1";
                VariableStore.TryGetValue(variable, out object v);
                VariableStore.TrySetValue(variable, (int)v + 5);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                VariableStore.TryGetValue("DB_Numbers.num1", out object num1);
                VariableStore.TryGetValue("DB_Numbers.num5", out object num2);

                Debug.Log($"num1 + num2 = {(int)num1 + (int)num2}");
            }

            if (Input.GetKeyDown(KeyCode.Z))
            {
                VariableStore.TryGetValue("L_int", out object linked_integer);
                VariableStore.TrySetValue("L_int", (int)linked_integer + 5);
            }

            if(Input.GetKeyDown(KeyCode.G))
            {
                VariableStore.RemoveVariable("DB_Links.L_int");
                VariableStore.RemoveVariable("DB_Booleans.lightIsOn");
            }

            if(Input.GetKeyDown(KeyCode.H))
            {
                VariableStore.RemoveAllVariables();
            }

        }

    }
}