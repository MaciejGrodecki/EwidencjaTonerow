using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EwidencjaTonerow.Interfaces
{
    public interface IMainRepository: IDisposable
    {
        void Save();
        bool IsUniqueName(string name);
    }
}
