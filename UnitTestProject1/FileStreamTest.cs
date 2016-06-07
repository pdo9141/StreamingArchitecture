using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Security.AccessControl;

namespace UnitTestProject1
{
    [TestClass]
    public class FileStreamTest
    {
        [TestMethod]
        public void Open_Read_Write_Create_Differences_Test()
        {
            // OpenRead returns a read-only stream for an existing file
            // OpenWrite creates the file and returns a write-only stream
            // Create creates the file and returns a read/write stream
            // If the file already exists
            // ----Create truncates existing content
            // ----OpenWrite leaves existing content and sets Position to 0
            // --------You can explicitly advance the pointer to the end of stream
        }

        [TestMethod]
        public void Constructors_Test()
        {
            // 15 constructor overloads
            // Across these 15 overloads, two ways to point to the required file
            // string path for managed coded
            // IntPtr handle for operating system file handle interoperability (obsolete in 4.5)
            // SafeFileHandle handle is now encouraged for interoperability
        }

        [TestMethod]
        public void Instantiating_FilePath_Test()
        {
            // "c:\myfiles\data.txt"
            // AppDomain.CurrentDomain.BaseDirectory returns application base directory
            //----"C:\Users\pdo\Desktop\StreamCourse\Demos\FileStream\bin\Debug\"
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "data.txt", FileMode.OpenOrCreate);

            // You can use the UNC path for network locations
            // "\\pdo\Shares\data.txt"
            // "\\127.0.0.1\Shares\data.txt"
        }

        [TestMethod]
        public void FileMode_Test()
        {
            // CreateNew creates new file, if already exists, exception thrown
            // Create creates new file, if already exists, it's overwritten
            // Open opens existing file, sets position to 0, if not exist, exception thrown
            // OpenOrCreate opens existing file, creates new file if it doesn't exist
            // Truncate opens a file, deletes file content (size = 0 bytes)
            // Append opens file and sets position to end of file
            //----If file doesn't exist, it is created
            //----Only writing is allowed
            //----Only appending data is allowed
            //----Seek only works forwards

            // don't try and outsmart machine, you'll get runtime error since no reading allowed with Append
            FileStream f = new FileStream(@"C:\temp\test.txt", FileMode.Append, FileAccess.ReadWrite);
            int x = f.ReadByte();
        }

        [TestMethod]
        public void FileAccess_Test()
        {
            // By default FileStream will open a file in read/write access mode
            //----With the exception of FileMode.Append option
            // Read file can only be read
            // Write file can only be written to
            // Read/Write reading and writing operations are supported
            // FileAccess.Read and FileAccess.ReadWrite cannot be mixed with FileMode.Append
        }

        [TestMethod]
        public void Example_Test()
        {
            using (FileStream fs = new FileStream(@"C:\temp\data.txt", FileMode.Create, FileAccess.Write))
            {
                fs.WriteByte(100);
                fs.Position = 0;

                // defensive coding here, ReadByte would have thrown an error
                // due to these FileMode/FileAccess combination
                if (fs.CanRead)
                    fs.ReadByte();
            }
        }

        [TestMethod]
        public void FileShare_Test()
        {
            // Files get locked by a FileStream until the stream is closed
            // No other stream can access the file
            // Be careful when you allow write sharing
            //----This might affect file data and code quality
            //----When two streams are allowed to share a file, unexpected results can happen
            // None sharing is not allowed, default
            // Delete subsequent streams can delete file
            // Inheritable file handle can be inherited by child processes
            // Read streams can open file for reading only
            // Write streams can open file for writing only
            // ReadWrite streams can open file for reading and writing

            using (FileStream fs1 = new FileStream(@"C:\temp\data.txt",
                FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (FileStream fs2 = new FileStream(@"C:\temp\data.txt",
                    FileMode.Open, FileAccess.Read))
                {
                }
            }
        }

        [TestMethod]
        public void Access_Control_Test()
        {
            // What users have access to
            // What permissions users have
            // This is different than FileAccess
            //----FileAccess contols FileStream open mode (read, write, read/write)
            //----Access control sets access rules for system users
            // FileSecurity class in System.Security.AccessControl namespace

            // This is similar to setting security permissions of file using UI
            FileSecurity fs = new FileSecurity();
            fs.AddAccessRule(new FileSystemAccessRule(@"pdopc\pdo",
                FileSystemRights.FullControl,
                AccessControlType.Allow));

            FileStream fstream = new FileStream(@"C:\temp\data.txt",
                FileMode.Create,
                FileSystemRights.Write,
                FileShare.None,
                8,
                FileOptions.Encrypted,
                fs);
        }

        [TestMethod]
        public void Internal_Buffer_Test()
        {
            // FileStream implements an internal buffer, default size is 4096 bytes
            // Buffer size can be set using constructor overloads/
            //----Increasing buffer size will reduce I/O load but increase memory consumption
            //----Reducing buffer size will increase I/O load but reduce memory consumption
            // 4k size will be mostly sufficient, carefully selected by Microsoft
            // Solid-state drives (SSD) handle I/O much faster than hard disk drives (HDD)
        }

        [TestMethod]
        public void File_Caching_Test()
        {
            // Recall that Flush writes internal buffer data into the backing store
            // For FileStream, there is another layer of caching at OS level
            // Flush flushes FileStream's internal buffer but not the system cache
            // FileStream.Flush(true) overload flushes the system cache
            // FileOptions.WriteThrough turns off system caching
            //----Unless you have SSDs, disabling system cache can cause performance issues for frequent reads/writes
            //----For huge amount of data, system caching can also become a bottleneck
            //----Moral of story, study your situation and do the tradeoffs
            // FileOptions.SequentialScan
            //----File accessed sequentially moving the pointer forward
            //----Cache manager optimizes caching for sequential access
            //----Using SequentialScan while performing random access prevents cache manager from optimizing caching
            //----Example: developing a video player softward
            // FileOptions.RandomAccess
            //----File accessed randomly
            //----Cache manager optimizes caching for random access
            //----Using RandomAccess while performing sequential access prevents cache manager from optimizing caching
            //----Example: developing a video editing software
            // If none is specified, cache manager will try to detect access pattern
            // DeleteOnClose deletes file once FileStream is closed, useful for temporary files
            // Encrypted encrypts file with Encrypting File System (EFS)
            // Asynchronous allows asynchronous access to the file
        }
    }
}
