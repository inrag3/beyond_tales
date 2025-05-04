namespace Extensions
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.AI;
    using System;
    public static class CommonExtensions
    {
        
        public static Vector2 ToXZ(this Vector3 delta)
        {
            return new Vector2(delta.x, delta.z);
        }
        public static Vector3 ToDirectionXZ(this Vector3 delta)
        {
            delta.y = 0;
            delta.Normalize();
            return delta;
        }

        public static Vector3 ToVector3XZ(this Vector2 vec)
        {
            return new Vector3(vec.x, 0, vec.y);
        }

        public static Vector3 SetX(this Vector3 vec, float x)
        {
            return new Vector3(x, vec.y, vec.z);
        }
        
        public static Vector3 SetY(this Vector3 vec, float y)
        {
            return new Vector3(vec.x, y, vec.z);
        }
        
        public static Vector3 SetZ(this Vector3 vec, float z)
        {
            return new Vector3(vec.x, vec.y, z);
        }

        public static bool ContainBounds(this Bounds bounds, Bounds target)
        {
            return bounds.Contains(target.min) && bounds.Contains(target.max);
        }

        /// <summary>
        /// Use this with interfaces backed by unity objects to prevent strange null refs
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNullOrDestroyed(this object obj)
        {
            return obj == null || ((obj is UnityEngine.Object) && (UnityEngine.Object)obj == null);
        }

        public static void Shuffle<T>(this T[] list)
        {
            var n = list.Length;
            while (n > 1)
            {
                n--;
                var k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this T[] list, System.Random rnd)
        {
            var n = list.Length;
            while (n > 1)
            {
                n--;
                var k = rnd.Next(0, n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Shuffle<T>(this List<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static Texture2D ToTexture2D(this RenderTexture renderTexture)
        {
            var tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
            var old_rt = RenderTexture.active;
            RenderTexture.active = renderTexture;
            tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            tex.Apply();

            RenderTexture.active = old_rt;
            return tex;
        }

        public static bool OnNavMesh(this Vector3 position, out Vector3 navMeshPosition, float maxDistance = 1.5f, int layer = -1)
        {
            var hasPointOnNavMesh = NavMesh.SamplePosition(position, out var hit, maxDistance, layer);
            if (!hasPointOnNavMesh)
            {
                navMeshPosition = position;
                return false;
            }

            navMeshPosition = hit.position;
            return true;
        }

        public static Bounds ComputeBounds(this Transform transform)
        {
            var renderers = transform.GetComponentsInChildren<Renderer>(true);
            if (renderers.Length == 0)
            {
                return new Bounds(transform.position, Vector3.zero);
            }

            var res = renderers[0].bounds;
            for (int i = 1; i < renderers.Length; i++)
            {
                res.Encapsulate(renderers[i].bounds);
            }
            return res;
        }

        public static Transform[] Children(this Transform transform)
        {
            var res = new Transform[transform.childCount];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = transform.GetChild(i);
            }
            return res;
        }

        public static void ResetLocalTransform(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        // string.GetHashCode is not guaranteed to be the same on all machines
        public static int GetStableHashCode(this string text)
        {
            unchecked
            {
                int hash = 23;
                foreach (char c in text)
                {
                    hash = hash * 31 + c;
                }
                return hash;
            }
        }

        public static T Random<T>(this T[] array)
        {
            if (array.Length == 0)
            {
                return default(T);
            }

            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public static T Random<T>(this List<T> list, System.Random seed)
        {
            if (list.Count == 0)
            {
                return default(T);
            }
            int random = seed.Next(0, list.Count);
            return list[random];
        }

        public static T Random<T>(this List<T> list)
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            return list[UnityEngine.Random.Range(0, list.Count)];
        }

        public static T Random<T>(this IEnumerable<T> ienumerable)
        {
            if (ienumerable.Count() == 0)
            {
                return default(T);
            }
            return ienumerable.ToList().Random();
        }

        public static T Random<T>(this IEnumerable<T> ienumerable, System.Random seed)
        {
            if (ienumerable.Count() == 0)
            {
                return default(T);
            }

            return ienumerable.ToList().Random(seed);
        }

        public static T[] RandomN<T>(this T[] array, int n)
        {
            array.Shuffle();
            return array.Take(n).ToArray();
        }


        public static List<GameObject> InRadius(this GameObject[] array, Transform self, float radius)
        {
            if (array == null)
            {
                return new List<GameObject>();
            }

            var res = new List<GameObject>();
            for (int i = 0; i < array.Length; i++)
            {
                var dist = Vector3.Distance(self.position, array[i].transform.position);
                if (dist < radius)
                {
                    res.Add(array[i]);
                }
            }
            return res;
        }

        public static List<GameObject> InRadius(this GameObject[] array, Vector3 position, float radius)
        {
            if (array == null)
            {
                return new List<GameObject>();
            }

            var res = new List<GameObject>();
            for (int i = 0; i < array.Length; i++)
            {
                var dist = Vector3.Distance(position, array[i].transform.position);
                if (dist < radius)
                {
                    res.Add(array[i]);
                }
            }
            return res;
        }

        public static GameObject Closest(this GameObject[] array, Transform self, System.Func<GameObject, bool> predicate)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            var res = default(GameObject);
            var min = float.MaxValue;
            for (int i = 0; i < array.Length; i++)
            {
                var dist = Vector3.Distance(self.position, array[i].transform.position);
                if (dist < min && predicate(array[i]))
                {
                    min = dist;
                    res = array[i];
                }
            }
            return res;
        }
        public static GameObject Closest(this IEnumerable<GameObject> collection, Transform self, System.Func<GameObject, bool> predicate)
        {
            if (collection == null || collection.Count() == 0)
            {
                return null;
            }

            var res = default(GameObject);
            var min = float.MaxValue;
            foreach (var item in collection)
            {
                var dist = Vector3.Distance(self.position, item.transform.position);
                if (dist < min && predicate(item))
                {
                    min = dist;
                    res = item;
                }
            }

            return res;
        }

        public static GameObject Closest(this GameObject[] array, Transform self)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            var res = default(GameObject);
            var min = float.MaxValue;
            for (int i = 0; i < array.Length; i++)
            {
                var dist = Vector3.Distance(self.position, array[i].transform.position);
                if (dist < min)
                {
                    min = dist;
                    res = array[i];
                }
            }
            return res;
        }
        public static GameObject Closest(this IEnumerable<GameObject> collection, Transform self)
        {
            if (collection == null || collection.Count() == 0)
            {
                return null;
            }

            var res = default(GameObject);
            var min = float.MaxValue;
            foreach (var item in collection)
            {
                var dist = Vector3.Distance(self.position, item.transform.position);
                if (dist < min)
                {
                    min = dist;
                    res = item;
                }
            }
            return res;
        }

        public static GameObject Closest(this GameObject[] array, Vector3 position)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            var res = default(GameObject);
            var min = float.MaxValue;
            for (int i = 0; i < array.Length; i++)
            {
                var dist = Vector3.Distance(position, array[i].transform.position);
                if (dist < min)
                {
                    min = dist;
                    res = array[i];
                }
            }
            return res;
        }
        public static GameObject Closest(this IEnumerable<GameObject> collection, Vector3 position)
        {
            if (collection == null || collection.Count() == 0)
            {
                return null;
            }

            var res = default(GameObject);
            var min = float.MaxValue;
            foreach (var item in collection)
            {
                var dist = Vector3.Distance(position, item.transform.position);
                if (dist < min)
                {
                    min = dist;
                    res = item;
                }
            }

            return res;
        }

        public static GameObject Closest(this GameObject[] array, Vector3 position, System.Func<GameObject, bool> predicate)
        {
            if (array == null || array.Length == 0)
            {
                return null;
            }

            var res = default(GameObject);
            var min = float.MaxValue;
            for (int i = 0; i < array.Length; i++)
            {
                var dist = Vector3.Distance(position, array[i].transform.position);
                if (dist < min && predicate(array[i]))
                {
                    min = dist;
                    res = array[i];
                }
            }
            return res;
        }
        public static GameObject Closest(this IEnumerable<GameObject> collection, Vector3 position, System.Func<GameObject, bool> predicate)
        {
            if (collection == null || collection.Count() == 0)
            {
                return null;
            }

            var res = default(GameObject);
            var min = float.MaxValue;
            foreach (var item in collection)
            {
                var dist = Vector3.Distance(position, item.transform.position);
                if (dist < min && predicate(item))
                {
                    min = dist;
                    res = item;
                }
            }
            return res;
        }

        public static void Map<T>(this IEnumerable<T> collection, System.Action<T> action)
        {
            if (collection == null)
            {
                return;
            }

            foreach (var item in collection)
            {
                action(item);
            }
        }

        public static string FullObjectPath(this GameObject gameObject)
        {
            if (gameObject == null)
            {
                return "(Null GameObject)";
            }

            var res = "";
            var current = gameObject.transform;
            while (current != null)
            {
                res = string.Format("/{0}{1}", current.gameObject.name, res);
                current = current.parent;
            }
            return res;
        }

        public static string FullObjectPathWithHierarchyId(this GameObject gameObject)
        {
            var res = "";
            var current = gameObject.transform;
            while (current != null)
            {
                var id = current.GetSiblingIndex();
                res = string.Format("/{0}_{1}{2}", current.gameObject.name, id, res);
                current = current.parent;
            }
            return res;
        }

       
        public static float GetLength(this NavMeshPath path)
        {
            var length = 0f;

            if ((path.status != NavMeshPathStatus.PathInvalid) && (path.corners.Length > 1))
            {
                for (int i = 1; i < path.corners.Length; ++i)
                {
                    length += Vector3.Distance(path.corners[i - 1], path.corners[i]);
                }
            }

            return length;
        }

        public static Vector3 RandomPositionXZ(this Vector3 position, float distance = 1.0f)
        {
            Vector3 random = UnityEngine.Random.insideUnitSphere;
            random.y = 0.0f;
            return position + (random * distance);
        }

        public static IEnumerable<Enum> GetFlags(this Enum e)
        {
            return Enum.GetValues(e.GetType()).Cast<Enum>().Where(e.HasFlag);
        }

        public static List<Transform> GetAllChildrens(this Transform transform)
        {
            List<Transform> listChildren = new List<Transform>();
            GetAllChildrens(transform, listChildren);
            return listChildren;
        }

        private static void GetAllChildrens(Transform transfrom, List<Transform> listAllChildren)
        {
            if (transfrom.childCount == 0);
            foreach (var element in transfrom.Children())
            { 
                listAllChildren.Add(element);
                GetAllChildrens(element, listAllChildren);
            }
        }

        public static void SetLayerWithChildren(this Transform transform, int layer, Predicate<Transform> pred = null)
        {
            transform.gameObject.layer = layer;

            foreach (var child in transform.GetAllChildrens())
            {
                if (pred == null || pred(child))
                {
                    child.gameObject.layer = layer;
                }
            }
        }
    }
}