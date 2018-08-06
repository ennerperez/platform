namespace Platform.Model
{
#if PORTABLE

    namespace Core
    {
#endif

    namespace CRUD
    {
        public enum Operation
        {
            Unknow = -1,
            None = 0,
            Create = 1,
            Read = 2,
            Update = 3,
            Delete = 4
        }
    }

#if PORTABLE
    }

#endif
}