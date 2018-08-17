using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelShopProject
{
    public class EmptyComboBoxException : ApplicationException
    {
        public EmptyComboBoxException() { }
        public EmptyComboBoxException(string message) : base(message) { }
        public EmptyComboBoxException(string message, Exception inner) : base(message, inner) { }
    }
}
