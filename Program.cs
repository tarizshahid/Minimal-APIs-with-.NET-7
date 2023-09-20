var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Book> books = new List<Book>
{
    new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
    new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
    new Book { Id = 3, Title = "1984", Author = "George Orwell" },
    new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen" },
    new Book { Id = 5, Title = "The Catcher in the Rye", Author = "J.D. Salinger" }
};


app.MapGet("/GetAllBooks",()=>
{
    return books;
});

app.MapGet("/GetBookbyId{id}", (int id) =>
{
    var book = books.Find(x=>x.Id==id);
    if(book is null)
    {
        return Results.NotFound($"Book with ID:{id} doesn't exist");
    }
    return Results.Ok(book);
});

app.MapPost("/AddBook", (Book book) =>
{
    books.Add(book);
    return Results.Ok($"Book added: {book.Title}");
});

app.MapPut("/UpdateBook{id}", (Book Updatedbook,int id) =>
{
    var book = books.Find(x => x.Id == id);
    if (book is null)
    {
        return Results.NotFound($"Book with ID:{id} doesn't exist");
    }
    book.Title = Updatedbook.Title;
    book.Author = Updatedbook.Author;
    return Results.Ok(books);
    
});

app.MapDelete("/DeleteBookbyId{id}", (int id) =>
{
    var book = books.Find(x => x.Id == id);
    if (book is null)
    {
        return Results.NotFound($"Book with ID:{id} doesn't exist");
    }
    books.Remove(book);
    return Results.Ok(books);
});


app.Run();

class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}
