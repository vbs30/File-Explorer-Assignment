using System;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Transactions;

namespace Task2_File_Operations
{
    public class Program
    {
        public static string defaultPath = @"C:\Sample files";
        static void Main(string[] args)
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("Choose an operation to perform");

                Console.WriteLine("1 - Create File");
                Console.WriteLine("2 - Read File");
                Console.WriteLine("3 - Write File");
                Console.WriteLine("4 - Move File");
                Console.WriteLine("5 - Copy File");
                Console.WriteLine("6 - Update File");
                Console.WriteLine("7 - Rename File");
                Console.WriteLine("8 - Exit");

                Console.WriteLine("Choose any one operation from 1 - 8");

                int choiceofOperation;
                if (int.TryParse(Console.ReadLine(), out choiceofOperation))
                {
                    switch (choiceofOperation)
                    {
                        case 1:
                            Console.Clear();
                            PathSelectionforCreateOperation();
                            break;
                        case 2:
                            Console.Clear();
                            PathSelectionforReadOperation();
                            break;
                        case 3:
                            Console.Clear();
                            PathSelectionforWriteOperation();
                            break;
                        case 4:
                            Console.Clear();
                            PathSelectionforMoveOperation();
                            break;
                        case 5:
                            Console.Clear();
                            PathSelectionforCopyOperation();
                            break;
                        case 6:
                            Console.Clear();
                            PathSelectionforUpdateOperation();
                            break;
                        case 7:
                            Console.Clear();
                            PathSelectionforRenameOperation();
                            break;
                        case 8:
                            keepRunning = false;
                            break;
                        default:
                            Console.WriteLine("Invalid Input, Try again");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }
            }
        }


        public static void PathSelectionforCreateOperation()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("1 - Proceed with {0}", defaultPath);
                Console.WriteLine("2 - Proceed to enter custom path to folder  Eg. C:\\Sample files - Copy");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose any one operation from 1 - 3");
                int choiceofOperation;
                if (int.TryParse(Console.ReadLine(), out choiceofOperation))
                {
                    switch (choiceofOperation)
                    {
                        case 1:
                            Console.Clear();
                            CreateOperation(defaultPath);
                            keepRunning = false;
                            break;

                        case 2:
                            Console.Clear();
                            CustomPathInputForCreateOperation();
                            keepRunning = false;
                            break;

                        case 3:
                            Console.Clear();
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Input, Try again");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }
            }
        }
        public static void CustomPathInputForCreateOperation()
        {
            Console.Clear();
            Console.WriteLine("Enter a valid folder path: ");
            Console.WriteLine();

            string path = Console.ReadLine();
            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path))
                {
                    CreateOperation(path);
                }
                else
                {
                    Console.WriteLine("The path you entered is invalid or the directory does not exist. Please try again");
                    Thread.Sleep(1000);
                    CustomPathInputForCreateOperation();
                }
            }
            else
            {
                Console.WriteLine("Wrong input, Please try again");
                Thread.Sleep(1000);
                CustomPathInputForCreateOperation();
            }
        }
        public static void CreateOperation(string path)
        {
            Console.Clear();
            bool keepRunning = true;

            while (keepRunning)
            {
                Console.WriteLine("1 - Do you want to create new folder in {0}", path);
                Console.WriteLine("2 - Do you want to create new file in {0}", path);
                Console.WriteLine("3 - Back");
                int choiceOfOperation = int.Parse(Console.ReadLine());

                switch (choiceOfOperation)
                {
                    case 1:
                        Console.Clear();
                        CreateFolder(path);
                        break;

                    case 2:
                        Console.Clear();
                        CreateFile(path);
                        break;

                    case 3:
                        keepRunning = false;
                        PathSelectionforCreateOperation();
                        break;

                    default:
                        Console.WriteLine("Invalid selection, please select approriate option");
                        break;
                }
            }
        }

        public static void CreateFolder(string path)
        {
            Console.WriteLine("Enter the new file name that should be saved inside the path {0}", path);
            string newFileName = Console.ReadLine();

            if (FileNameValidation(newFileName))
            {
                string newFilePath = Path.Combine(path, newFileName);
                try
                {
                    if (Directory.Exists(newFilePath))
                    {
                        Console.WriteLine("That path exists already. Please try again");
                        Thread.Sleep(1000);
                        CreateOperation(path);
                    }
                    else
                    {
                        DirectoryInfo di = Directory.CreateDirectory(newFilePath);
                        Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(newFilePath));
                        Thread.Sleep(1000);
                        CreateOperation(newFilePath);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Please enter correct file name");
                Thread.Sleep(1000);
                CreateFolder(path);
            }
        }
        public static void CreateFile(string path)
        {
            Console.WriteLine("Enter the new text file name (must end with .txt) ", path);
            string fileName = Console.ReadLine();
            if (FileNameValidation(fileName))
            {
                if (!fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("File name must end with '.txt'. Please try again.");
                    CreateFile(path);
                }

                string filePath = Path.Combine(path, fileName);

                if (File.Exists(filePath))
                {
                    Console.WriteLine("That file already exists. Please enter a different file name.");
                    Thread.Sleep(1000);
                    CreateFile(path);
                }
                try
                {
                    using (FileStream fs = File.Create(filePath))
                    {
                        Console.WriteLine("The text file was created successfully at {0}.", filePath);
                        Thread.Sleep(1000);
                        CreateOperation(filePath);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The process failed: {0}", e.ToString());
                }
            }
            else
            {
                Console.WriteLine("Please enter correct file name");
                Thread.Sleep(1000);
                CreateFile(path);
            }
        }




        public static void PathSelectionforReadOperation()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("1 - Proceed with {0}", defaultPath);
                Console.WriteLine("2 - Proceed to enter custom path to folder  Eg. C:\\Sample files - Copy");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose any one operation from 1 - 3");
                int choiceofOperation;
                if (int.TryParse(Console.ReadLine(), out choiceofOperation))
                {
                    switch (choiceofOperation)
                    {
                        case 1:
                            Console.Clear();
                            ReadOperation(defaultPath);
                            keepRunning = false;
                            break;

                        case 2:
                            Console.Clear();
                            CustomPathInputForRead();
                            keepRunning = false;
                            break;

                        case 3:
                            Console.Clear();
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Input, Try again");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }
            }
        }
        public static void CustomPathInputForRead()
        {
            Console.Clear();
            Console.WriteLine("Enter a valid folder path: ");
            Console.WriteLine();

            string path = Console.ReadLine();
            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path))
                {
                    ReadOperation(path);
                }
                else
                {
                    Console.WriteLine("The path you entered is invalid or the directory does not exist. Please try again");
                    Thread.Sleep(1000);
                    CustomPathInputForRead();
                }
            }
            else
            {
                Console.WriteLine("Wrong input, Please try again");
                Thread.Sleep(1000);
                CustomPathInputForRead();
            }
        }
        public static void ReadOperation(string path)
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                string[] filePaths = printFiles(path);

                Console.WriteLine("Select a file to read by entering the file number:");

                if (int.TryParse(Console.ReadLine(), out int choiceOfFile) && choiceOfFile >= 1 && choiceOfFile <= filePaths.Length)
                {
                    string selectedFilePath = filePaths[choiceOfFile - 1];
                    try
                    {
                        using (StreamReader streamReader = new StreamReader(selectedFilePath))
                        {
                            Console.WriteLine("Data of {0}:", Path.GetFileName(selectedFilePath));
                            Console.WriteLine(streamReader.ReadToEnd());
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error reading file: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice, please enter a valid file number.");
                }

                Console.WriteLine("Press '0' to redirect to the previous page, or 'm' to redirect to the Main Page");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    PathSelectionforReadOperation();
                    keepRunning = false;
                }
                else if (input == "m")
                {
                    Main(new string[] { });
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }
        }




        public static void PathSelectionforWriteOperation()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("1 - Proceed with {0}", defaultPath);
                Console.WriteLine("2 - Proceed to enter custom path to folder  Eg. C:\\Sample files - Copy");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose any one operation from 1 - 3");
                int choiceofOperation;
                if (int.TryParse(Console.ReadLine(), out choiceofOperation))
                {
                    switch (choiceofOperation)
                    {
                        case 1:
                            Console.Clear();
                            WriteOperation(defaultPath);
                            keepRunning = false;
                            break;

                        case 2:
                            Console.Clear();
                            CustomPathInputForWrite();
                            keepRunning = false;
                            break;

                        case 3:
                            Console.Clear();
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Input, Try again");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }
            }
        }
        public static void CustomPathInputForWrite()
        {
            Console.Clear();
            Console.WriteLine("Enter a valid folder path: ");
            Console.WriteLine();

            string path = Console.ReadLine();
            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path))
                {
                    WriteOperation(path);
                }
                else
                {
                    Console.WriteLine("The path you entered is invalid or the directory does not exist. Please try again");
                    Thread.Sleep(1000);
                    CustomPathInputForWrite();
                }
            }
            else
            {
                Console.WriteLine("Wrong input, Please try again");
                Thread.Sleep(1000);
                CustomPathInputForWrite();
            }
        }
        public static void WriteOperation(string path)
        {
            Console.Clear();
            bool keepRunning = true;

            while (keepRunning)
            {
                string[] filePaths = printFiles(path);

                Console.WriteLine();
                Console.WriteLine("Select a file to write to by entering the file number:");
                if (int.TryParse(Console.ReadLine(), out int choiceOfFile) && choiceOfFile >= 1 && choiceOfFile <= filePaths.Length)
                {
                    string selectedFilePath = filePaths[choiceOfFile - 1];

                    try
                    {
                        Console.WriteLine("Enter the data you want to write to the file {0}:", Path.GetFileName(selectedFilePath));
                        string dataToWrite = Console.ReadLine();
                        using (StreamWriter streamWriter = new StreamWriter(selectedFilePath, append: true))
                        {
                            streamWriter.WriteLine(dataToWrite);
                        }

                        Console.WriteLine("Data has been written to the file.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error writing to file: " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice, please enter a valid file number.");
                }

                Console.WriteLine();
                Console.WriteLine("Press '0' to redirect to the previous page, or 'm' to redirect to the Main Page");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    PathSelectionforWriteOperation();
                    keepRunning = false;
                }
                else if (input == "m")
                {
                    Main(new string[] { });
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }
        }




        public static void PathSelectionforUpdateOperation()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("1 - Proceed with {0}", defaultPath);
                Console.WriteLine("2 - Proceed to enter custom path to folder  Eg. C:\\Sample files - Copy");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose any one operation from 1 - 3");
                int choiceofOperation;
                if (int.TryParse(Console.ReadLine(), out choiceofOperation))
                {
                    switch (choiceofOperation)
                    {
                        case 1:
                            Console.Clear();
                            UpdateOperation(defaultPath);
                            keepRunning = false;
                            break;

                        case 2:
                            Console.Clear();
                            CustomPathInputForUpdate();
                            keepRunning = false;
                            break;

                        case 3:
                            Console.Clear();
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Input, Try again");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }
            }
        }
        public static void CustomPathInputForUpdate()
        {
            Console.Clear();
            Console.WriteLine("Enter a valid folder path: ");
            Console.WriteLine();

            string path = Console.ReadLine();
            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path))
                {
                    UpdateOperation(path);
                }
                else
                {
                    Console.WriteLine("The path you entered is invalid or the directory does not exist. Please try again");
                    Thread.Sleep(1000);
                    CustomPathInputForUpdate();
                }
            }
            else
            {
                Console.WriteLine("Wrong input, Please try again");
                Thread.Sleep(1000);
                CustomPathInputForUpdate();

            }
        }

        public static void UpdateOperation(string path)
        {
            Console.Clear();
            bool keepRunning = true;

            while (keepRunning)
            {
                string[] filePaths = printFiles(path);

                Console.WriteLine();
                Console.WriteLine("Select a file to read by entering the file number:");

                if (int.TryParse(Console.ReadLine(), out int choiceOfFile) && choiceOfFile > 0 && choiceOfFile <= filePaths.Length)
                {
                    string selectedFilePath = filePaths[choiceOfFile - 1];
                    SelectedPathOptions(selectedFilePath);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to one of the available files.");
                    keepRunning = false;
                    UpdateOperation(path);
                }
            }
        }

        public static void SelectedPathOptions(string path)
        {
            Console.Clear();
            Console.WriteLine("What would you want to perform on the file {0}", path);

            Console.WriteLine("1 - Append");
            Console.WriteLine("2 - Overwrite");
            Console.WriteLine("3 - Truncate");
            Console.WriteLine("4 - Back");

            Console.WriteLine("Select appropriate choice");

            int choiceofoperation = int.Parse(Console.ReadLine());


            switch (choiceofoperation)
            {
                case 1:
                    Console.Clear();
                    AppendData(path);
                    break;

                case 2:
                    Console.Clear();
                    OverwriteData(path);
                    break;

                case 3:
                    Console.Clear();
                    TruncateData(path);
                    break;

                case 4:
                    Console.Clear();
                    UpdateOperation(path);
                    break;

                default:
                    Console.WriteLine("Invalid selection, please select again");
                    break;
            }
            Console.WriteLine("Please select one of the options");
        }

        public static void AppendData(string path)
        {
            try
            {
                Console.WriteLine("Enter the data you want to write to the file {0}:", Path.GetFileName(path));
                string dataToWrite = Console.ReadLine();
                using (StreamWriter streamWriter = new StreamWriter(path, append: true))
                {
                    streamWriter.WriteLine(dataToWrite);
                }

                Console.WriteLine("Data has been written to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to file: " + ex.Message);
            }
            Console.WriteLine();
            Console.WriteLine("Press '0' to redirect to the previous page, or 'm' to redirect to the Main Page");
        }
        public static void OverwriteData(string path)
        {
            try
            {
                Console.WriteLine("Enter the data you want to write to the file {0}:", Path.GetFileName(path));
                string dataToWrite = Console.ReadLine();
                using (StreamWriter streamWriter = new StreamWriter(path, append: false))
                {
                    streamWriter.WriteLine(dataToWrite);
                }

                Console.WriteLine("Data has been written to the file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to file: " + ex.Message);
            }
            Console.WriteLine();
            Console.WriteLine("Press '0' to redirect to the previous page, or 'm' to redirect to the Main Page");
        }
        public static void TruncateData(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Truncate))
                {
                }

                Console.WriteLine("File content has been truncated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error truncating the file: " + ex.Message);
            }
            Console.WriteLine();
            Console.WriteLine("Press '0' to redirect to the previous page, or 'm' to redirect to the Main Page");
        }





        public static void PathSelectionforRenameOperation()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("1 - Proceed with {0}", defaultPath);
                Console.WriteLine("2 - Proceed to enter custom path to folder  Eg. C:\\Sample files - Copy");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose any one operation from 1 - 3");
                int choiceofOperation;
                if (int.TryParse(Console.ReadLine(), out choiceofOperation))
                {
                    switch (choiceofOperation)
                    {
                        case 1:
                            Console.Clear();
                            RenameOperation(defaultPath);
                            keepRunning = false;
                            break;

                        case 2:
                            Console.Clear();
                            CustomPathInputForRename();
                            keepRunning = false;
                            break;

                        case 3:
                            Console.Clear();
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Input, Try again");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }
            }
        }
        public static void CustomPathInputForRename()
        {
            Console.Clear();
            Console.WriteLine("Enter a valid folder path: ");
            Console.WriteLine();

            string path = Console.ReadLine();

            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path))
                {
                    RenameOperation(path);
                }
                else
                {
                    Console.WriteLine("The path you entered is invalid or the directory does not exist. Please try again");
                    Thread.Sleep(1000);
                    CustomPathInputForRename();
                }
            }
            else
            {
                Console.WriteLine("Wrong input, Please try again");
                Thread.Sleep(1000);
                CustomPathInputForRename();
            }
        }
        
        public static void RenameOperation(string path)
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                string[] filePaths = printFiles(path);

                Console.WriteLine();
                Console.WriteLine("Select a file to read by entering the file number:");

                if (int.TryParse(Console.ReadLine(), out int choiceOfFile) && choiceOfFile > 0 && choiceOfFile <= filePaths.Length)
                {
                    string selectedFilePath = filePaths[choiceOfFile - 1];
                    Console.WriteLine("You can proceed to rename the file: {0}", selectedFilePath);
                    Console.WriteLine();

                    Console.WriteLine("Enter the new file name (without path)");

                    string directoryPath = Path.GetDirectoryName(selectedFilePath);
                    string oldFileName = Path.GetFileName(selectedFilePath);
                    string oldFileExtension = Path.GetExtension(selectedFilePath);
                    string newFileName = Console.ReadLine();
                    if (FileNameValidation(newFileName))
                    {
                        string newFilePath = Path.Combine(directoryPath, newFileName + oldFileExtension);
                        try
                        {
                            File.Move(selectedFilePath, newFilePath);
                            Console.WriteLine("File renamed successfully to: {0}", newFilePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error renaming file: {0}", ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Please enter valid fileName to rename");
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to one of the available files.");
                }

                Console.WriteLine("Press '0' to go back or 'm' to go to the main menu.");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    PathSelectionforRenameOperation();
                    keepRunning = false;
                }
                else if (input == "m")
                {
                    Main(new string[] { });
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }
        }




        public static void PathSelectionforMoveOperation()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("1 - Proceed with {0}", defaultPath);
                Console.WriteLine("2 - Proceed to enter custom path to folder  Eg. C:\\Sample files - Copy");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose any one operation from 1 - 3");
                int choiceofOperation;
                if (int.TryParse(Console.ReadLine(), out choiceofOperation))
                {
                    switch (choiceofOperation)
                    {
                        case 1:
                            Console.Clear();
                            MoveOperation(defaultPath);
                            keepRunning = false;
                            break;

                        case 2:
                            Console.Clear();
                            CustomPathInputForMove();
                            keepRunning = false;
                            break;

                        case 3:
                            Console.Clear();
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Input, Try again");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }
            }
        }
        public static void CustomPathInputForMove()
        {
            Console.Clear();
            Console.WriteLine("Enter a valid folder path: ");
            Console.WriteLine();

            string path = Console.ReadLine();

            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path))
                {
                    MoveOperation(path);
                }
                else
                {
                    Console.WriteLine("The path you entered is invalid or the directory does not exist. Please try again");
                    Thread.Sleep(1000);
                    CustomPathInputForMove();
                }
            }
            else
            {
                Console.WriteLine("Wrong input, Please try again");
                Thread.Sleep(1000);
                CustomPathInputForMove();
            }
        }
        public static void MoveOperation(string path)
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                string[] filePaths = printFiles(path);

                Console.WriteLine();
                Console.WriteLine("Select a file to move by entering the file number:");

                if (int.TryParse(Console.ReadLine(), out int choiceOfFile) && choiceOfFile > 0 && choiceOfFile <= filePaths.Length)
                {
                    string selectedFilePath = filePaths[choiceOfFile - 1];
                    Console.WriteLine("You can proceed to move the file: {0}", selectedFilePath);
                    Console.WriteLine();

                    string oldFileName = Path.GetFileName(selectedFilePath);

                    Console.WriteLine("Enter the new destination path:");

                    string newPath = Console.ReadLine();

                    if (Directory.Exists(newPath))
                    {
                        string newFilePath = Path.Combine(newPath, oldFileName);

                        try
                        {
                            File.Move(selectedFilePath, newFilePath);
                            Console.WriteLine("File moved successfully to: {0}", newFilePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error moving file: {0}", ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid path. Please enter a valid destination path.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to one of the available files.");
                }

                Console.WriteLine("Press '0' to go back or 'm' to go to the main menu.");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    PathSelectionforMoveOperation();
                    keepRunning = false;
                }
                else if (input == "m")
                {
                    Main(new string[] { });
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }
        }




        public static void PathSelectionforCopyOperation()
        {
            bool keepRunning = true;
            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("1 - Proceed with {0}", defaultPath);
                Console.WriteLine("2 - Proceed to enter custom path to folder  Eg. C:\\Sample files - Copy");
                Console.WriteLine("3 - Back");

                Console.WriteLine("Choose any one operation from 1 - 3");
                int choiceofOperation;
                if (int.TryParse(Console.ReadLine(), out choiceofOperation))
                {
                    switch (choiceofOperation)
                    {
                        case 1:
                            Console.Clear();
                            CopyOperation(defaultPath);
                            keepRunning = false;
                            break;

                        case 2:
                            Console.Clear();
                            CustomPathInputForCopy();
                            keepRunning = false;
                            break;

                        case 3:
                            Console.Clear();
                            keepRunning = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Input, Try again");
                            Thread.Sleep(1000);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Thread.Sleep(1000);
                }
            }
        }
        public static void CustomPathInputForCopy()
        {
            Console.Clear();
            Console.WriteLine("Enter a valid folder path: ");
            Console.WriteLine();

            string path = Console.ReadLine();

            if (Path.IsPathRooted(path))
            {
                if (Directory.Exists(path))
                {
                    CopyOperation(path);
                }
                else
                {
                    Console.WriteLine("The path you entered is invalid or the directory does not exist. Please try again");
                    Thread.Sleep(1000);
                    CustomPathInputForCopy();
                }
            }
            else
            {
                Console.WriteLine("Wrong input, Please try again");
                Thread.Sleep(1000);
                CustomPathInputForCopy();
            }
        }
        public static void CopyOperation(string path)
        {
            Console.Clear();
            bool keepRunning = true;
            while (keepRunning)
            {
                string[] filePaths = printFiles(path);

                Console.WriteLine();
                Console.WriteLine("Select a file to copy by entering the file number:");

                if (int.TryParse(Console.ReadLine(), out int choiceOfFile) && choiceOfFile > 0 && choiceOfFile <= filePaths.Length)
                {
                    string selectedFilePath = filePaths[choiceOfFile - 1];
                    Console.WriteLine("Do you really want to copy {0}", Path.GetFileName(selectedFilePath));
                    Console.WriteLine("Select YES or NO");
                    string copyDecision = Console.ReadLine();
                    if (copyDecision.ToLower() == "yes" || copyDecision.ToLower() == "y")
                    {
                        Console.WriteLine("You can proceed to copy the file: {0}", Path.GetFileName(selectedFilePath));
                        Console.WriteLine();

                        string oldFileName = Path.GetFileNameWithoutExtension(selectedFilePath);
                        string fileExtension = Path.GetExtension(selectedFilePath);

                        if (Directory.Exists(path))
                        {
                            string newFileName = GenerateUniqueFileName(path, oldFileName, fileExtension);
                            string newFilePath = Path.Combine(path, newFileName);

                            try
                            {
                                File.Copy(selectedFilePath, newFilePath);
                                Console.WriteLine("File copied successfully to: {0}", newFilePath);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error copying file: {0}", ex.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid path. Please enter a valid destination path.");
                        }
                    }
                    else if(copyDecision.ToLower() == "no" || copyDecision.ToLower() == "n")
                    {
                        CopyOperation(path);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please select 'Yes' or 'No'");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number corresponding to one of the available files.");
                }

                Console.WriteLine("Press '0' to go back or 'm' to go to the main menu.");

                string input = Console.ReadLine();
                if (input == "0")
                {
                    PathSelectionforCopyOperation();
                    keepRunning = false;
                }
                else if (input == "m")
                {
                    Main(new string[] { });
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("Please enter a valid input");
                }
            }
        }


        private static string[] printFiles(string path)
        {
            string[] filePaths = Directory.GetFiles(path);
            Console.WriteLine("Available files:");
            Console.WriteLine();
            for (int i = 0; i < filePaths.Length; ++i)
            {
                Console.WriteLine("{0} - {1}", i + 1, Path.GetFileName(filePaths[i]));
            }
            return filePaths;
        }
        private static string GenerateUniqueFileName(string directory, string baseName, string extension)
        {
            int copyNumber = 1;
            string newFileName;
            do
            {
                newFileName = $"{baseName}({copyNumber}){extension}";
                copyNumber++;
            } while (File.Exists(Path.Combine(directory, newFileName)));

            return newFileName;
        }

        private static bool FileNameValidation(string fileName)
        {
            string fileNameRegex = @"^[0-9a-zA-Z\-_()]+$";
            Regex regex = new Regex(fileNameRegex);

            return regex.IsMatch(fileName);
            
        }

    }
}
