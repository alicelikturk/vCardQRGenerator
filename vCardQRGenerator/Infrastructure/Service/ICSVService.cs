using System;
namespace vCardQRGenerator.Infrastructure.Service
{
	public interface ICSVService
	{
        public IEnumerable<T> ReadCSV<T>(Stream file);
    }
}

