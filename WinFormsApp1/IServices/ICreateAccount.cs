using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.entity;

namespace WinFormsApp1.IServices
{
    public interface ICreateAccount
    {
        public Task CreateAcc(int sl);
    }
}
