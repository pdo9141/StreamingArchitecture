using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO.IsolatedStorage;

namespace UnitTestProject1
{
    [TestClass]
    public class IsolatedStorageFileStreamTests
    {
        [TestMethod]
        public void IsolatedStorage_Basics_Test()
        {
            // Isolated Storage is "some hidden location"
            // User-specific or machine-specific data
            // Used through API
            // Would be useful
            //----when application have restricted-access, Silverlight, ClickOnce, etc.
            //----storing temporary user or machine-specific data
            //----want a guaranteed application-unique location
            //----partial trust applications
            // Isolated storage data is not secured against unwanted access
            // User browsing the file system can dig out the data
            // Depending on strongly signed or Authenticode (similar to certificate authority for esignig), isolated storage will be same or different
            // You can set quotas for ma data that can be written to a store
            //----Important for partial trust applications
            //----Default is 1 MB, IsolatedStorageFile.Quota to query quota
            //----IncreaseQuotaTo to increase quota, cannot be reduced anymore after increased
        }

        [TestMethod]
        public void IsolatedStorage_Sample_Test()
        {
            IsolatedStorageFile ifs3 = IsolatedStorageFile.GetUserStoreForAssembly();
            using (var t = new IsolatedStorageFileStream("data.txt", System.IO.FileMode.Create, ifs3))
            {
                t.WriteByte(234);
            }

            IsolatedStorageFile ifs4 = IsolatedStorageFile.GetMachineStoreForAssembly();
            using (var t = new IsolatedStorageFileStream("data.txt", System.IO.FileMode.Create, ifs4))
            {
                t.WriteByte(234);
            }
        }
    }
}
