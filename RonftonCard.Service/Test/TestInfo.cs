using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Service.Test
{
    public class TestInfo
    {
        public String Id { get; set; }

        public String Desc { get; set; }

        public int Price { get; set; }

        public override string ToString()
        {
            return String.Format("id = {0}, price = {1}, desc = {2}", this.Id , this.Price, this.Desc); 
        }
    }
}
