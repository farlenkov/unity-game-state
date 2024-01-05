using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityObjectRegistry
{
    internal abstract class ObjectList
    {
        internal abstract void ClearNew();
        internal abstract void Remove(object theObject);
    }

    internal class ObjectList<TYPE> : ObjectList
    {
        List<TYPE> allObjects = new();
        List<TYPE> newObjects = new();

        public IReadOnlyList<TYPE> All => allObjects;
        public IReadOnlyList<TYPE> New => newObjects;

        public void Add(TYPE theObject)
        {
            allObjects.Add(theObject);
            newObjects.Add(theObject);
        }

        internal override void Remove(object theObject)
        {
            var typedObject = (TYPE)theObject;
            var allIndex = allObjects.IndexOf(typedObject);
            var newIndex = newObjects.IndexOf(typedObject);

            if (allIndex >= 0)
                allObjects.RemoveAt(allIndex);

            if (newIndex >= 0)
                newObjects.RemoveAt(newIndex);
        }

        internal override void ClearNew()
        {
            newObjects.Clear();
        }
    }
}
