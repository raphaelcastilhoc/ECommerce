namespace ECommerce.SeedWork
{
    public abstract class Entity<T> where T : struct
    {
        public T MyProperty { get; private set; }
    }
}
