using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card
{
    public class VirtualAddress
    {
        public int Sector { get; set; }

        /// <summary>
        /// mapping to virtual card
        /// </summary>
        public int Address { get; set; }

        public int Length { get; set; }

        public M1KeyMode KeyMode { get; set; }

        public byte[] Key { get; set; }
    }
}