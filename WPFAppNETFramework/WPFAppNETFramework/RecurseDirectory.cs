// Filename: RecurseDirectory.cs
// By Mike Sutherland
// First Created: Apr. 17, 2019
// Reference
// https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-iterate-through-a-directory-tree

using System;
using System.Collections.Generic;
using System.IO;

public class StackBasedIteration
{
    static void TraverseDirectory(string[] args)
    {
        // Specify the starting folder on the command line, or in 
        // Visual Studio in the Project > Properties > Debug pane.
        TraverseTree(args[0]);

       // Console.WriteLine("Press any key");
       // Console.ReadKey();
    }

    public static void TraverseTree(string root)
    {
        // Data structure to hold names of subfolders to be
        // examined for files.
        Stack<string> dirs = new Stack<string>(20);
        UInt64 nFiles = 0;
        long nSize = 0;
        long nDir = 1;
        string filepath = "Temp.tsv"; //tab delimited file
        string tmpFolderPath = Path.GetTempPath();

        filepath = tmpFolderPath + filepath;

        if (!System.IO.Directory.Exists(root))
        {
            throw new ArgumentException();
        }
        dirs.Push(root);

        if (dirs.Count > 0)
        {
            // ref. https://stackoverflow.com/questions/39749136/how-do-i-create-a-csv-file-in-c-sharp
            // You can write a csv file using streamwriter. Your file will be located in bin/Debug (if running debug mode and not stating otherwise).
            // Look in the following path for file... C:\Users\mike\source\repos\WPFAppNETFramework\WPFAppNETFramework\bin\Debug
            using (StreamWriter writer = new StreamWriter(new FileStream(filepath,
            FileMode.Create, FileAccess.Write))) // will overwrite file if it exists.
            {
                //writer.WriteLine("sep=,");
                writer.WriteLine("sep=\t"); //Filenames can have commas in them in Windows.
                // writer.WriteLine("FileName, Extension, Directory, FileSize, CreateDateTime"); \t is for tab
                writer.WriteLine("FileName" + "\t" + "Extension" + "\t" + "Directory" + "\t" + "FileSize" + "\t" + "CreateDateTime");
            }
        }
        while (dirs.Count > 0)
        {
            string currentDir = dirs.Pop();
            string[] subDirs;
            try
            {
                subDirs = System.IO.Directory.GetDirectories(currentDir);
            }
            // An UnauthorizedAccessException exception will be thrown if we do not have
            // discovery permission on a folder or file. It may or may not be acceptable 
            // to ignore the exception and continue enumerating the remaining files and 
            // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
            // will be raised. This will happen if currentDir has been deleted by
            // another application or thread after our call to Directory.Exists. The 
            // choice of which exceptions to catch depends entirely on the specific task 
            // you are intending to perform and also on how much you know with certainty 
            // about the systems on which this code will run.

            //Exception thrown: 'System.ArgumentException' in mscorlib.dll
//An exception of type 'System.ArgumentException' occurred in mscorlib.dll but was not handled in user code
//Illegal characters in path.
            catch (System.ArgumentException e) //ms added for Linux file name with @
            {
                System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                continue;
            }
            catch (System.NotSupportedException e)
            {
                System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                continue;
            }
            catch (UnauthorizedAccessException e)
            {
                System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                continue;
            }
            catch (System.IO.DirectoryNotFoundException e)
            {
                System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                continue;
            }

            string[] files = null;
            try
            {
                files = System.IO.Directory.GetFiles(currentDir);
            }
            catch (System.ArgumentException e) //ms added for Linux file name with @
            {
                System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                continue;
            }
            catch (UnauthorizedAccessException e)
            {
                System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                //Console.WriteLine(e.Message);
                continue;
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                //Console.WriteLine(e.Message);
                continue;
            }
            // Perform the required action on each file here.
            // Modify this block to perform your required task.
            foreach (string file in files)
            {
                try
                {
                    // Perform whatever action is required in your scenario.
                    // Exception thrown: 'System.NotSupportedException' in mscorlib.dll
                    // An exception of type 'System.NotSupportedException' occurred in mscorlib.dll but was not handled in user code
                    // The given path's format is not supported.
                    System.IO.FileInfo fi = new System.IO.FileInfo(file);
                    nFiles++;
                    nSize += fi.Length;
                    //Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                    //Removing the following five statements meant that now it can traverse my C: drive on my Samsung in about 2.5 minutes versus about 35 minutes approx.
                    //System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                    //System.Diagnostics.Debug.WriteLine("File name: " + fi.Name);
                    //System.Diagnostics.Debug.WriteLine("File Extension: " + fi.Extension);
                    //System.Diagnostics.Debug.WriteLine("Directory name: " + fi.DirectoryName);
                    //System.Diagnostics.Debug.WriteLine("File Length: " + fi.Length);
                    //System.Diagnostics.Debug.WriteLine("File Creation Time: " + fi.CreationTime);
                    //System.Diagnostics.Debug.WriteLine("------------------------------------------------"); // may need a new line at the beginning of this
                    //ref. https://social.msdn.microsoft.com/Forums/vstudio/en-US/0271c11d-4cf3-452b-af65-6c06473669fb/adding-row-into-existing-csv-file-using-c?forum=csharpgeneral
                    List<string> newLines = new List<string>
                    {
                        //fi.Name + "," + fi.Extension + "," + fi.DirectoryName + "," + fi.Length + "," + fi.CreationTime
                        fi.Name + "\t" + fi.Extension + "\t" + fi.DirectoryName + "\t" + fi.Length + "\t" + fi.CreationTime
                    };
                    File.AppendAllLines(filepath, newLines);
                }
                catch (System.ArgumentException e) //ms added
                {
                    System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                    System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                    System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                    continue;
                }
                catch (System.IO.FileNotFoundException e)
                {
                    // If file was deleted by a separate application
                    //  or thread since the call to TraverseTree()
                    // then just continue.
                    System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                    System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                    System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                    continue;
                }
                catch (System.NotSupportedException e) //ms added
                {
                    System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
                    System.Diagnostics.Debug.WriteLine("\n" + e.Message);
                    System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
                    continue;
                }
            }

            // Push the subdirectories onto the stack for traversal.
            // This could also be done before handing the files.
            foreach (string str in subDirs)
            {
                dirs.Push(str);
                nDir++;
            }
        }
        System.Diagnostics.Debug.WriteLine("\n-----------------------------------------------");
        System.Diagnostics.Debug.WriteLine("Number of Files: " + nFiles);
        System.Diagnostics.Debug.WriteLine("Number of Directories: " + nDir);
        System.Diagnostics.Debug.WriteLine("Total File Size in Bytes: " + nSize + " Bytes");
        System.Diagnostics.Debug.WriteLine("-----------------------------------------------\n");
    }
}