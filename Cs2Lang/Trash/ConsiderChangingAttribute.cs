using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cs2Lang.Trash
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
    class ConsiderChangingAttribute : Attribute
    {
        public ConsiderChangingAttribute()
        {
            Message = null;
        }

        public ConsiderChangingAttribute(string message)
        {
            Message = message;
        }

        public string Message { get; }


    }
}
