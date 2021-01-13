using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IFaultedConsumer
    {
        public List<int> ValidationErrors { get; set; }
    }
}
