using System.ComponentModel.DataAnnotations.Schema;

namespace MoneyCheck.Domain.Common
{
  public class AuditableEntity
  {
    [Column("created_date")]
    public DateTime? CreatedDate { get; set; }

    [Column("last_modified_date")]
    public DateTime? LastModifiedDate { get; set; }
  }
}