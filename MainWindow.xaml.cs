using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _1103Game_profile {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        struct Monster {
            public string Name;
            public int Health;
            public int Gold;
            public int Experience;
            public int PhysicalPower;
            public int MagicalPower;
            public int PhysicalDefense;
            public int MagicalDefense;
            public string Family;
            public string WeakAgainst;
            public string StrongAgainst;

        }//end struct

        //GLOBAL VARIABLE TO HOLD PERSON DATA
        Monster[] globalMonsterData;
        public MainWindow() {
            InitializeComponent();
            string filePath = "C:\\Users\\MCA\\Downloads\\monsterImages\\monster.csv";

            //GET RECORD COUNT
            int records = CountCsvRecords(filePath, true);

            //SET SLIDER TO MATCH
            sldRecord.Maximum = records - 1;

            //LOAD DATA FROM CSV & RETURN ARRAY OF PERSON
            globalMonsterData = ProcessCsvDataIntoMonsterStruct(filePath, records, true);

            //UPDATE FORM WITH 1ST PERSON'S DATA
            UpdateForm(0);

            LoadImage(imgMain, $"C:\\Users\\MCA\\Downloads\\monsterImages\\images\\images\\{ txtName.Text}.png");

        }//end main
        #region Events
        private void sldRecord_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            //CHECK IF NO DATA HAS BEEN LOADED. EXIT IF THIS IS THE CASE.
           /* if (globalMonsterData == null) {
                sldRecord.Value = 0;
                return;
            }//end if*/

            //CREATE AN INT TO SNAP SLIDER VALUE TO
            int sliderInt = (int)sldRecord.Value;

            //UPDATE LABEL SHOWING THE CURRENTLY SELECTED RECORD
            //lblMonsterID.Content = sliderInt.ToString();

            //UPDATE FORM
            UpdateForm(sliderInt);

            //load images to form
            LoadImage(imgMain, $"C:\\Users\\MCA\\Downloads\\monsterImages\\images\\images\\{txtName.Text}.png");
        }//end function
        #endregion

        #region Functions / Methods
        int CountCsvRecords(string filePath, bool skipHeader) {
            //VARIABLES
            int recordCount = 0;

            //OPEN THE FILE TO COUNT THE NUMBER OF RECORDS
            StreamReader infile = new StreamReader(filePath);

            //CONSUME HEADER WITH A READLINE
            if (skipHeader) {
                infile.ReadLine();
            }//end if

            //COUNT RECORDS
            while (infile.EndOfStream == false) {
                infile.ReadLine();
                recordCount += 1;
            }//end while

            //CLOSE FILE
            infile.Close();

            return recordCount;
        }//end function

        Monster[] ProcessCsvDataIntoMonsterStruct(string filePath, int recordsToRead, bool skipHeader) {
            //VARS
            Monster[] returnArray = new Monster[recordsToRead];
            int currentRecordCount = 0;

            //OPEN THE FILE TO COUNT THE NUMBER OF RECORDS
            StreamReader infile = new StreamReader(filePath);

            //CONSUME HEADER WITH A READLINE
            if (skipHeader) {
                infile.ReadLine();
            }//end if

            //COUNT RECORDS
            while (infile.EndOfStream == false && currentRecordCount < recordsToRead) {
                string record = infile.ReadLine();
                string[] fields = record.Split(",");

                //CHECK IF FIELD IS BLANK, IF SO FILL WITH "?"
                for (int i = 0; i < fields.GetLength(0); i++) {
                    if (fields[i] == "" || fields[i] == "?") {
                        fields[i] = "0";
                    }//end if

                }//end for

                returnArray[currentRecordCount].Name = fields[0];
                returnArray[currentRecordCount].Health = int.Parse(fields[1]);
                returnArray[currentRecordCount].Gold = int.Parse(fields[2]);
                returnArray[currentRecordCount].Experience = int.Parse(fields[3]);
                returnArray[currentRecordCount].PhysicalPower = int.Parse(fields[4]);                
                returnArray[currentRecordCount].MagicalPower = int.Parse(fields[5]);
                returnArray[currentRecordCount].PhysicalDefense = int.Parse(fields[6]);
                returnArray[currentRecordCount].MagicalDefense = int.Parse(fields[7]);
                returnArray[currentRecordCount].Family = fields[8];
                returnArray[currentRecordCount].WeakAgainst = fields[9];
                returnArray[currentRecordCount].StrongAgainst = fields[10];

                currentRecordCount += 1;
            }//end while

            //CLOSE FILE
            infile.Close();

            return returnArray;
        }//end function

        void UpdateForm(int monsterIndex) {

            //GRAB MONSTER FROM THE GLOBAL ARRAY
            Monster currentMonster = globalMonsterData[monsterIndex];

            //UPDATE TEXTBOXES ON THE FORM
            txtName.Text = currentMonster.Name;
            txtHealth.Text = currentMonster.Health.ToString();
            txtGold.Text = currentMonster.Gold.ToString();
            txtExperience.Text = currentMonster.Experience.ToString();
            txtPhyPower.Text = currentMonster.PhysicalPower.ToString();
            txtMagicPower.Text = currentMonster.MagicalPower.ToString();
            txtPhyDefense.Text = currentMonster.PhysicalDefense.ToString();
            txtMagicDefense.Text = currentMonster.MagicalDefense.ToString();
            txtFamily.Text = currentMonster.Family;
            if (txtFamily.Text == "0") {
                txtFamily.Text = "N/A";
            }//end if
            txtWeakAgainst.Text = currentMonster.WeakAgainst;
            if (txtWeakAgainst.Text == "0") {
                txtWeakAgainst.Text = "N/A";
            }//end if
            txtStrong.Text = currentMonster.StrongAgainst;
            if (txtStrong.Text == "0") {
                txtStrong.Text = "N/A";
            }
                /* txtFamily.Text = currentMonster.Family;
                 txtWeakAgainst.Text = currentMonster.WeakAgainst;
                 txtStrong.Text = currentMonster.StrongAgainst;*/

                txtMonsterId.Text = monsterIndex.ToString();
        }//end function
        void LoadImage(Image ImgTarget, string imageFilePath) {
            //changes empty png to not available jpeg
            if (File.Exists(imageFilePath) == false) {
                imageFilePath = "C:\\Users\\MCA\\Downloads\\notavailableImage.jpg";

            }
            //Create a Bitmap
            BitmapImage bmpImage = new BitmapImage();
            //Set bitmap for editing

            bmpImage.BeginInit();
            
            bmpImage.UriSource = new Uri(imageFilePath);// load the image
            
            bmpImage.EndInit();
            //set the source of the image control to the bitmap
            ImgTarget.Source = bmpImage;
        }//end function

        #endregion

        #region HELPER FUNCTIONS
        #region Arrays
        static void FillArray(char[,] array, char char1) {
            for (int y = 0; y < array.GetLength(1); y++) {
                for (int x = 0; x < array.GetLength(0); x++) {
                    array[x, y] = char1;
                }//end for
            }//end for
        }//end function

        static void PrintArray(char[,] array, char char1) {
            //LOOP THROUGH ALL ROWS
            for (int y = 0; y < array.GetLength(1); y++) {
                //LOOP THROUGH ALL COLUMNS OF EACH ROW
                for (int x = 0; x < array.GetLength(0); x++) {

                    //OUTPUT VALUE IN ARRAY AT CURRENT COLUMN
                    Console.Write(array[x, y] + " ");
                }//end for

                //MOVE TO NEXT LINE FOR THE NEXT ROW
                Console.WriteLine();
            }//end for
        }//end function
        #endregion Arrays

        #region Input/Try Parse

        //WRITE TO CONSOLE
        static void Print(string message) {
            Console.WriteLine(message);
        }//end function

        //PARSE INPUT & TRY PARSE INPUT
        static string Input(string message) {
            Console.Write(message);
            return Console.ReadLine();
        }//end function

        static decimal InputDecimal(string message) {
            string value = Input(message);
            return decimal.Parse(value);
        }//end function

        static decimal TryInputDecimal(string message) {
            //VARIABLES
            decimal parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID DECIMAL HAS BEEN SUBMITTED
            do {
                gotParsed = decimal.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static double InputDouble(string message) {
            string value = Input(message);
            return double.Parse(value);
        }//end function

        static double TryInputDouble(string message) {
            //VARIABLES
            double parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID DOUBLE HAS BEEN SUBMITTED
            do {
                gotParsed = double.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function

        static int InputInt(string message) {
            string value = Input(message);
            return int.Parse(value);
        }//end function

        static int TryInputInt(string message) {
            //VARIABLES
            int parsedValue = 0;
            bool gotParsed = false;

            //VALIDATION LOOP UNTIL VALID INT HAS BEEN SUBMITTED
            do {
                gotParsed = int.TryParse(Input(message), out parsedValue);
            } while (gotParsed == false);

            //RETURN PARSED VALUE
            return parsedValue;
        }//end function
        #endregion Input/Try Parse

        #region Bools

        //BOOL INPUT YES OR NO (Y/N)
        static bool InputYesNo(string message) {
            //WRITE MESSAGE TO CONSOLE FOR INPUT 
            Console.Write(message);

            //GET THE KEY THAT WAS PRESSED
            char keyPressed = Console.ReadKey().KeyChar;

            //FORCE A NEW LINE
            Console.WriteLine();

            //CONVERT KEY PRESSED TO LOWER CASE
            char lowerCaseKey = char.ToLower(keyPressed);

            //COMAPRE
            bool pressedYes = lowerCaseKey == 'y';

            return pressedYes;
        }//end function
        #endregion Bool

        #region Colors
        //PRINT AND CHANGE COLORS
        static void PrintColor(string message, ConsoleColor color = ConsoleColor.White) {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }//end function
        static void ConsoleSetForeColor(byte red, byte grn, byte blu) {
            Console.Write($"\x1b[38;2;{red};{grn};{blu}m");
        }//end function

        static void ConsoleSetBackColor(byte red, byte grn, byte blu) {
            Console.Write($"\x1b[48;2;{red};{grn};{blu}m");
        }//end function

        static void ConsoleSetBlock(int xPos, int yPos, byte[] color) {
            //STORE OLD COLORS
            ConsoleColor origForeground = Console.ForegroundColor;
            ConsoleColor origBackground = Console.BackgroundColor;

            //SET BLOCK COLOR
            byte red = color[0];
            byte grn = color[1];
            byte blu = color[2];

            ConsoleSetForeColor(red, grn, blu);
            ConsoleSetBackColor(red, grn, blu);

            //MOVE CURSOR TO POSITION
            Console.SetCursorPosition(xPos, yPos);

            //DRAW BLOCK
            Console.Write(" ");

            //CHANGE COLORS BACK
            Console.ForegroundColor = origForeground;
            Console.BackgroundColor = origBackground;
        }//end function
        #endregion Colors

        #endregion HELPER FUNCTIONS



        private void btnFirst_Click(object sender, RoutedEventArgs e) {
            sldRecord.Value = 0;
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e) {
            sldRecord.Value = sldRecord.Value - 1;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e) {
            sldRecord.Value = sldRecord.Value + 1;
        }

        private void btnLast_Click(object sender, RoutedEventArgs e) {
            sldRecord.Value = sldRecord.Maximum;
        }
    }
}
