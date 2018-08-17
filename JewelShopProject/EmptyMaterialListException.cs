using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class EmptyMaterialListException : ApplicationException
    {
        public EmptyMaterialListException() { }
        public EmptyMaterialListException(string message) : base(message) { }
        public EmptyMaterialListException(string message, Exception inner) : base(message, inner) { }
    }
}
