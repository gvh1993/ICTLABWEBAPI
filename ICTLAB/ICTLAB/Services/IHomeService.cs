using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICTLAB.Models;

namespace ICTLAB.Services
{
    interface IHomeService
    {
        List<Home> GetAllHomes();
        bool Create(string name);
        bool Delete(string name);
    }
}
