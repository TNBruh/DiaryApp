using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace DiaryApp
{
    public class Option<T>
    {
        public T? data;
        public string? msg;
        public bool HasData
        {
            get
            {
                return data != null;
            }
        }
        public T UnwrappedData
        {
            get
            {
                return data!;
            }
        }

        
        public Option(T? data, string? msg = null)
        {
            if (data == null)
            {
                this.data = default(T);
                this.msg = msg != null ? msg : "Empty error message";
            } else
            {
                this.data = data;
                this.msg = null;
            }
        }
    }
}
