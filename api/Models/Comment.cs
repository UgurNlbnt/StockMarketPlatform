using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        public int Id { get; set; } // Yorumun benzersiz kimliği
        public string Title { get; set; } = string.Empty;// Yorumun başlığı
        public string Content { get; set; } = string.Empty;  // Yorumun içeriği
        public DateTime CreatedOn { get; set; } = DateTime.Now; // Yorumun oluşturulma tarihi
        public int? StockId { get; set; } // Nullable int, çünkü bir yorumun bir hisseye ait olması zorunlu değil.
        //navigation
        public Stock? Stock { get; set; } // Nullable Stock, çünkü bir yorumun bir hisseye ait olması zorunlu değil.
    }
}