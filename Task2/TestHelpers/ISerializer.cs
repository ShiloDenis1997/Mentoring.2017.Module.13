using System.IO;

namespace Task.TestHelpers
{
    public interface ISerializer<TData>
    {
        TData Deserialize(MemoryStream stream);
        void Serialize(TData data, MemoryStream stream);
    }
}
