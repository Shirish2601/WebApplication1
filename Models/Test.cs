using Models.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Test
    {
        public IDataReader reader = new FileReader();

        public void TestMethod()
        {
            reader.Read(@"C:\Users\Hadp_shi\Desktop\Shirish\New folder\WebApplication1\Models\Matrix.txt");
        }
        static void Main()
        {
            Test t = new();
            t.TestMethod();
        }
    }
}
