using System.ComponentModel.DataAnnotations;

namespace AccountManagement.DataAccess.EntityModels
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Add new columns
        public string NormalizedName { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
