using System.ComponentModel.DataAnnotations;

namespace Polley.WebApi.Data;

public class BaseEntity
{
    [Key] public int Id { get; set; }
}