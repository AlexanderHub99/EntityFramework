using Microsoft.EntityFrameworkCore;

namespace ComplexTypes.Model
{

    [Owned]
    public class UserProfile
    {
        public int Id { get; set; }

        public int Age { get; set; }

        public string? Name { get; set; }
    }
}