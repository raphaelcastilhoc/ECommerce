namespace ECommerce.SeedWork
{
    public abstract class Entity<T> where T : struct
    {
        public T Id { get; private set; }
    }
}
