namespace NET5.Models
{
    public record RecordPerson
    {
        public string FirstName { get; }
        public string LastName { get; }

        public RecordPerson(string firstName, string lastName) => (FirstName, LastName) = (firstName, lastName);
    }
}
