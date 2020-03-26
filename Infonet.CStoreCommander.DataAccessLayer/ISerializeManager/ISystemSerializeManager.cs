using Infonet.CStoreCommander.EntityLayer.Entities.System;
using System.Threading.Tasks;

namespace Infonet.CStoreCommander.DataAccessLayer.ISerializeManager
{
    public interface ISystemSerializeManager
    {
        Task<Register> GetRegisterSettings(byte registerNumber);
    }
}
