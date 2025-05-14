using Newtonsoft.Json;
using UnityEngine;

namespace _Project.Runtime.Core.SaveSystem
{
    [System.Serializable]
    public class SerializableVector3{
        public float x;
        public float y;
        public float z;

        [JsonIgnore]
        public Vector3 UnityVector{
            get{
                return new Vector3(x, y, z);
            }
        }

        public SerializableVector3(Vector3 v){
            x = v.x;
            y = v.y;
            z = v.z;
        }

      
    }
}