using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeActivity.Models
{
    public class GoodsModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Descripcion { get; set; }

        [MaxLength(50)]
        public string Precio { get; set; }
    }
}
