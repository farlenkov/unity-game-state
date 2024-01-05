using System;
using System.Collections.Generic;

namespace UnityQueueRegistry
{
    public interface IBaseQueue
    {
        public void Clear();
    }

    public class BaseQueue<TYPE> : List<TYPE>, IBaseQueue
    {

    }

    public abstract class BaseQueueRegistry
    {
        List<IBaseQueue> QueueList = new();
        Dictionary<Type, IBaseQueue> QueueIndex = new();

        // GET

        protected QUEUE Get<TYPE, QUEUE>() where QUEUE : BaseQueue<TYPE>, new()
        {
            var type = typeof(TYPE);

            if (!QueueIndex.TryGetValue(typeof(TYPE), out var queue))
            {
                queue = new QUEUE();
                QueueList.Add(queue);
                QueueIndex.Add(type, queue);
            }

            return queue as QUEUE;
        }

        // ADD

        protected void Add<TYPE, QUEUE>(TYPE item) where QUEUE : BaseQueue<TYPE>, new()
        {
            var queue = Get<TYPE, QUEUE>();
            queue.Add(item);
        }

        // REMOVE

        protected void Remove<TYPE, QUEUE>(TYPE item) where QUEUE : BaseQueue<TYPE>, new()
        {
            var type = typeof(TYPE);

            if (QueueIndex.TryGetValue(type, out var baseQueue))
            {
                var queue = baseQueue as QUEUE;
                var index = queue.IndexOf(item);

                if (index >= 0)
                    queue.RemoveAt(index);
            }
        }

        public void Clear()
        {
            for (var i = 0; i < QueueList.Count; i++)
                QueueList[i].Clear();
        }
    }
}