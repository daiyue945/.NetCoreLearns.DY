namespace UserMgr.WebAPI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UintOfWorkAttribute:Attribute
    {
        public Type[] DbContextTypes { get; set; }

        public UintOfWorkAttribute(params Type[] dbContextTypes)
        {
            this.DbContextTypes = dbContextTypes;
        }
    }
}
