namespace OneWithMany
{
     class Comment
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public Article Articles { get; set; }
        public long ArticlesId { get; set; }
    }
}