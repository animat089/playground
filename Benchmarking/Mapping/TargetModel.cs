namespace Benchmarking.Mapping;

public class Response
{
    public Company Company { get; set; }
}

public class Company
{
    public string Name { get; set; }
    public string City { get; set; }

    public string State { get; set; }

    public IEnumerable<Person> Employees { get; set; }
}

public class Book
{
    public string Title { get; set; }

    public string Author { get; set; }

    public string PublicationYear { get; set; }
}

public class Person
{
    public string Name { get; set; }

    public int Age { get; set; }

    public string Position { get; set; }

    public IEnumerable<Book> Books { get; set; }
}
