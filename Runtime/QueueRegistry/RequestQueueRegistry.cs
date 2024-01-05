namespace UnityQueueRegistry
{
    public class RequestQueue<TYPE> : BaseQueue<TYPE>
    {

    }

    public class RequestQueueRegistry : BaseQueueRegistry
    {
        // GET

        public void Get<TYPE>(out RequestQueue<TYPE> result)
        {
            result = Get<TYPE, RequestQueue<TYPE>>();
        }

        // ADD

        public void Add<TYPE>(TYPE request)
        {
            Add<TYPE, RequestQueue<TYPE>>(request);
        }

        // REMOVE

        public void Remove<TYPE>(TYPE request)
        {
            Remove<TYPE, RequestQueue<TYPE>>(request);
        }
    }
}
