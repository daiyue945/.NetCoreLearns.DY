namespace Cache
{
    public class MyDbContext
    {
        public static Book? GetById(long id)
        {
            switch (id)
            {
                case 0:
                    return new Book(0, "C#");
                case 1:
                    return new Book(1, "JAVA");
                case 2:
                    return new Book(2, "TypeScript");
                default:
                    return null;
            }
        }

        public static Task<Book?> GetByIdAsync(long id)
        {
            Book? book= MyDbContext.GetById(id);

            return Task.FromResult(book);
        }
    }
}
