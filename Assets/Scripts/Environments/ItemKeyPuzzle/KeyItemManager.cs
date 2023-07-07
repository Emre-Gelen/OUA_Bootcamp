using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class KeyItemManager : MonoBehaviour
{
    [SerializeField] private KeyItem keyType;
    

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidingObject = other.gameObject;

        if (collidingObject.CompareTag("Player"))
        {
            collidingObject.GetComponent<PlayerKeyHolder>().GiveKey(keyType);
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Reflection kullanarak anahtar?n rengini otomatik olarak belirlemeye çal??t?m, ancak struct'larda nedense beceremedim.
    private Color ColorHandler(KeyItem keyType)
    {
        Color color = Color.red;
        string colorName = keyType.ToString().ToLower();
        Debug.Log(colorName);
        FieldInfo field = typeof(Color).GetField(colorName, BindingFlags.Public | BindingFlags.Static);
        Debug.Log(field.Name);
        color = (Color)field.GetValue(null);
        
        return color;
        
    }
}
