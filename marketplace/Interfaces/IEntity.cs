using System.ComponentModel.DataAnnotations;

namespace marketplace.Interfaces
{
    public interface IEntity<T>
    {
        [Key]
        T id { get; set; }
    }
}
