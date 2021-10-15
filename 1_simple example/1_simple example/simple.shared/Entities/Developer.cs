using System;
using System.ComponentModel.DataAnnotations;

namespace simple.shared.Entities
{
    public class Developer
    {
        public Developer(string firstName, string lastName, string age, decimal salary, int experience)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Salary = salary;
            Experience = experience;
        }

        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public decimal Salary { get; set; }
        public int Experience { get; set; }
    }
}
