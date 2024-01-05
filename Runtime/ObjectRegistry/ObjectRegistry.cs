using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityObjectRegistry
{
    public class ObjectRegistry
    {
        Dictionary<Type, ObjectList> Objects = new();
        List<Type> NewTypes = new();

        // GET

        public bool TryGetAll<TYPE>(out IReadOnlyList<TYPE> result)
        {
            if (!Objects.TryGetValue(typeof(TYPE), out var objects))
            {
                result = default;
                return false;
            }

            // Log.InfoEditor(
            //     "[ObjectRegistry: TryGetAll] {0} {1}",
            //     typeof(TYPE),
            //     objects.GetObjectType().Name);

            result = (objects as ObjectList<TYPE>).All;
            return true;
        }

        public bool TryGetNew<TYPE>(out IReadOnlyList<TYPE> result)
        {
            if (!Objects.TryGetValue(typeof(TYPE), out var objects))
            {
                result = default;
                return false;
            }

            result = (objects as ObjectList<TYPE>).New;

            // Log.InfoEditor(
            //     "[ObjectRegistry: TryGetNew] {0} {1}",
            //     typeof(TYPE),
            //     result.Count);

            return true;
        }

        // ADD

        public void Add<TYPE>(TYPE theObject)
        {
            Add(theObject, typeof(TYPE));
        }

        public void Add<TYPE>(TYPE theObject, Type type)
        {
            if (!Objects.TryGetValue(type, out var objects))
            {
                objects = new ObjectList<TYPE>();
                Objects.Add(type, objects);
            }

            (objects as ObjectList<TYPE>).Add(theObject);

            if (!NewTypes.Contains(type))
                NewTypes.Add(type);

            // Log.InfoEditor(
            //     "[ObjectRegistry: Add] {0} {1}",
            //     type.Name,
            //     theObject is MonoBehaviour
            //         ? (theObject as MonoBehaviour).gameObject.name
            //         : theObject.GetType().Name);
        }

        // REMOVE

        public void Remove<TYPE>(TYPE theObject)
        {
            Remove(theObject, typeof(TYPE));
        }

        public void Remove<TYPE>(TYPE theObject, Type type)
        {
            if (Objects.TryGetValue(type, out var objects))
                objects.Remove(theObject);
        }

        public void ClearNew()
        {
            var newCount = NewTypes.Count;

            if (newCount == 0)
                return;

            for (var i = 0; i < newCount; i++)
            {
                var newType = NewTypes[i];

                if (Objects.TryGetValue(newType, out var objects))
                    objects.ClearNew();
            }

            NewTypes.Clear();
        }
    }
}