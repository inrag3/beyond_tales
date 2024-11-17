//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using R3;

//public class Detector : MonoBehaviour
//{
//    private ReactiveProperty<Item> _item = new ReactiveProperty<Item>(null);
//    public ReadOnlyReactiveProperty<Item> Item => _item.ToReadOnlyReactiveProperty();

//    private void OnTriggerEnter( Collider collider)
//    {
//        if (!collider.TryGetComponent(out Item item)) 
//            return;
//    }
   
//}
