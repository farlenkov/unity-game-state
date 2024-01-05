namespace UnityQueueRegistry
{
    public class EventQueue<TYPE> : BaseQueue<TYPE>
    {

    }

    public class EventQueueRegistry : BaseQueueRegistry
    {
        // GET

        public void Get<TYPE>(out EventQueue<TYPE> result)
        {
            result = Get<TYPE, EventQueue<TYPE>>();
        }

        // ADD

        public void Add<TYPE>(TYPE theEvent)
        {
            Add<TYPE, EventQueue<TYPE>>(theEvent);
        }

        // REMOVE

        public void Remove<TYPE>(TYPE theEvent)
        {
            Remove<TYPE, EventQueue<TYPE>>(theEvent);
        }
    }
}
