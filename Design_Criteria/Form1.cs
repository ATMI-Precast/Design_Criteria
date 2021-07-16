using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;

using Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;
using Tekla.Structures.Geometry3d;
using TSG = Tekla.Structures.Geometry3d;
using TTC = Tekla.Structural.InteropAssemblies.TeddsCalc;
using TT = Tekla.Structural.InteropAssemblies.Tedds;
using ClosedXML.Excel;
using MOIW = Microsoft.Office.Interop.Word;

namespace Design_Criteria
{

    //TODO: VARIABLES FROM TEDDS FILE TO WORD - CREATE WORD FILE AFTER THE FACT - LOOK AT JASON'S CODE
    //TODO: ASSIGN VARIABLES TO EXCEL FILE TO SHOW
    //TODO: UPDATE INTERFACE

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string templatePath = "C:/Users/cachong/Desktop/Templates";

            templatePathSheet = templatePath + "Design Criteria Template.xlsx";
            existingFilePath = @"C:\Users\cachong\Desktop\";
            fileName = "DESIGN CRITERIA - Copy.docx";
            templatefile = templatePath + "Drawing List Template.xlsx";

            string tempRead = "";
            myModel.GetProjectInfo().GetUserProperty("FILE_PATH", ref tempRead);

            txt_TeddsFile.Text = tempRead;

            //reportPath = myModel.GetInfo().ModelPath + "/Reports/" + System.Text.RegularExpressions.Regex.Replace(myModel.GetInfo().ModelName, ".db1", "");
            //existingFilePath = reportPath + "_DesignCriteria_(0).xlsx";
        }

       
        #region Global Variables

        IXLWorkbook myWorkbookXML;
        IXLWorksheet mySheetXML;
        Model myModel = new Model();
        WorkPlaneHandler myWPHandler;
        string templatePathSheet;
        string existingFilePath;
        string fileName;
        string reportPath;
        string filePath;
        string templatefile;
        public string docPath = "\\attributes";
        string BuildingCode;
        string Standard;
        string RiskCategory;

        #endregion

        SortedDictionary<int, TeddsVariables> variableList = new SortedDictionary<int, TeddsVariables>();
        SortedDictionary<int, TeddsVariables> DeadLoads = new SortedDictionary<int, TeddsVariables>();
        SortedDictionary<int, TeddsVariables> LiveLoads = new SortedDictionary<int, TeddsVariables>();
        List<string> variableNamesLL = new List<string>();

        #region User Interface

        private void btn_GetVars_Click(object sender, EventArgs e)
        {
            RunTedds(variableList, LiveLoads, DeadLoads);

            listBox1.Items.Add("BUILDING CODE = " + BuildingCode);
            listBox1.Items.Add("REFERENCED STANDARD = " + Standard);
            listBox1.Items.Add("RISK CATEGORY = " + RiskCategory);
            listBox1.Items.Add("");
            listBox1.Items.Add("LIVE LOADS");
            listBox1.Items.Add("");

            foreach (var item in LiveLoads)
            {
                ListViewItem Item1 = new ListViewItem();
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();

                if (item.Value.SubClass1Name == null && item.Value.SubClass2Name == null)
                {
                    Item1.Text = item.Value.ClassName + "  =  " + item.Value.Value;
                    listBox1.Items.Add(Item1.Text);
                }

                else if (item.Value.SubClass1Name != null && item.Value.SubClass2Name == null)
                {
                    Item1.Text = item.Value.ClassName + "    |    " + item.Value.SubClass1Name + "  =  " + item.Value.Value;
                    listBox1.Items.Add(Item1.Text);
                }

                else if (item.Value.SubClass1Name != null && item.Value.SubClass2Name != null)
                {
                    Item1.Text = item.Value.ClassName + "    |    " + item.Value.SubClass1Name + "    |    " + item.Value.SubClass2Name + "  =  " + item.Value.Value;
                    listBox1.Items.Add(Item1.Text);
                }


            }

            listBox1.Items.Add("");
            listBox1.Items.Add("DEAD LOADS");
            listBox1.Items.Add("");

            foreach (var item in DeadLoads)
            {
                ListViewItem Item = new ListViewItem();
                ListViewItem Item2 = new ListViewItem();

                if (item.Value.FloorNumber.Equals("floor1"))
                {
                    Item.Text = item.Value.ClassName.ToString() + "   ||   " + item.Value.SubClass1Name.ToString() + "   ||   " + item.Value.SubClass2Name.ToString() + "   ||   " + item.Value.Value.ToString();
                    Item2.Text = item.Value.FloorName;

                    listBox1.Items.Add(Item.Text);
                }

                if (item.Value.FloorNumber.Equals("floor2"))
                {
                    Item.Text = item.Value.ClassName.ToString() + "   ||   " + item.Value.SubClass1Name.ToString() + "   ||   " + item.Value.SubClass2Name.ToString() + "   ||   " + item.Value.Value.ToString();
                    Item2.Text = item.Value.FloorName;

             
                    listBox1.Items.Add(Item.Text);
                }

                if (item.Value.FloorNumber.Equals("floor3"))
                {
                    Item.Text = item.Value.ClassName.ToString() + "   ||   " + item.Value.SubClass1Name.ToString() + "   ||   " + item.Value.SubClass2Name.ToString() + "   ||   " + item.Value.Value.ToString();
                    Item2.Text = item.Value.FloorName;

                    listBox1.Items.Add(Item.Text);
                }

            }


        }

        private void txt_TeddsFile_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_OpenExplorer_Click(object sender, EventArgs e)
        {
            OpenExplorer();
        }

        private void btn_RunTedds_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void btn_UpdateDC_Click(object sender, EventArgs e)
        {

        }

        #endregion

        void RunTedds(SortedDictionary<int, TeddsVariables> variableList, SortedDictionary<int, TeddsVariables> LiveLoads, SortedDictionary<int, TeddsVariables> DeadLoads)
        {
            string docPath = myModel.GetInfo().ModelPath + "\\attributes\\ATMI_" + "DesignCriteria" + "_TDS.txt";
            string evalString = "EvalFile(\"" + docPath + "\", Append)";
            int counter2 = 1;
            char[] separator = new char[] { '\t' };

            TTC.ICalculator calculator = new TTC.Calculator();
            calculator.Initialize();

            if (File.Exists(docPath))
            {
                calculator.Functions.Eval(evalString);
            }

            string variables = calculator.GetVariables();

            calculator.Initialize("", variables);
            calculator.Functions.Eval("EvalCalcItem(\"$(UserLbrDir)ATMI_PDDesignCriteria_v21.lbr\",\"PDDesignCriteria_v21\")");

            BuildingCode = calculator.Functions.GetVar("BC").ToString();
            Standard = calculator.Functions.GetVar("Standard").ToString();
            RiskCategory = calculator.Functions.GetVar("Risk").ToString();

            #region Get Load Names

            //GetLiveLoads
            for (int counter = 1; counter < 15; counter ++)
            {
                string value1 = "";
                string value2 = "";
                string value3 = "";

                if (calculator.Functions.VarExists("rowSortedLoads" + counter.ToString()))
                {
                    TeddsVariables LL = new TeddsVariables();
                    string variableName = calculator.Functions.GetVar("rowSortedLoads" + counter.ToString()).ToString();
                    LL.ClassName = variableName.Split(separator)[0].ToString();

                    value1 = variableName.Split(separator)[1].ToString();

                    if (value1 != null)
                    {
                        LL.SubClass1Name = value1;
                    }

                    value2 = variableName.Split(separator)[2].ToString();

                    if (value1 != null)
                    {
                        LL.SubClass2Name = value2;
                    }

                    LL.Value = variableName.Split(separator)[3].ToString();


                    LiveLoads.Add(counter2, LL);
                    counter2++;
                }
                
            }
            
            //GetFirstFloorDeadLoads
            for (double counter = 1; counter < 10; counter++)
            {
                if (calculator.Functions.VarExists("ClassD_1." + counter.ToString()) && calculator.Functions.VarExists("ValueD_1." + counter.ToString()))
                {
                   
                    TeddsVariables floor1 = new TeddsVariables();
                    floor1.FloorNumber = "floor1";
                    floor1.ClassName = calculator.Functions.GetVar("ClassD_1." + counter.ToString()).ToString();
                    floor1.SubClass1Name = calculator.Functions.GetVar("Sub1ClassD_1." + counter.ToString()).ToString();
                    floor1.SubClass2Name = calculator.Functions.GetVar("Sub2ClassD_1." + counter.ToString()).ToString();
                    floor1.Value = calculator.Functions.GetVar("ValueD_1." + counter.ToString()).ToString();
                    floor1.FloorName = calculator.Functions.GetVar("Floor1").ToString();

                    DeadLoads.Add(counter2, floor1);
                    counter2++;
                }
            }

            //GetSecondFloorDeadLoads
            for (double counter = 1; counter < 10; counter++)
            {
                if (calculator.Functions.VarExists("ClassD_2." + counter.ToString()) && calculator.Functions.VarExists("ValueD_2." + counter.ToString()))
                {

                    TeddsVariables floor2 = new TeddsVariables();
                    floor2.FloorNumber = "floor2";
                    floor2.ClassName = calculator.Functions.GetVar("ClassD_2." + counter.ToString()).ToString();
                    floor2.SubClass1Name = calculator.Functions.GetVar("Sub1ClassD_2." + counter.ToString()).ToString();
                    floor2.SubClass2Name = calculator.Functions.GetVar("Sub2ClassD_2." + counter.ToString()).ToString();
                    floor2.Value = calculator.Functions.GetVar("ValueD_2." + counter.ToString()).ToString();
                    floor2.FloorName = calculator.Functions.GetVar("Floor2").ToString();

                    DeadLoads.Add(counter2, floor2);
                    counter2++;
                }
            }

            //GetThirdFloorDeadLoads
            for (double counter = 1; counter < 10; counter++)
            {
                if (calculator.Functions.VarExists("ClassD_3." + counter.ToString()) && calculator.Functions.VarExists("ValueD_1." + counter.ToString()))
                {

                    TeddsVariables floor3 = new TeddsVariables();
                    floor3.FloorNumber = "floor3";
                    floor3.ClassName = calculator.Functions.GetVar("ClassD_3." + counter.ToString()).ToString();
                    floor3.SubClass1Name = calculator.Functions.GetVar("Sub1ClassD_3." + counter.ToString()).ToString();
                    floor3.SubClass2Name = calculator.Functions.GetVar("Sub2ClassD_3." + counter.ToString()).ToString();
                    floor3.Value = calculator.Functions.GetVar("ValueD_3." + counter.ToString()).ToString();
                    floor3.FloorName = calculator.Functions.GetVar("Floor3").ToString();

                    DeadLoads.Add(counter2, floor3);
                    counter2++;
                }
            }

            #endregion

            evalString = "SaveSectionVarsTextFile(\"" + docPath + "\")";

            calculator.Functions.Eval(evalString);

            calculator = null;
            GC.Collect();

        }

        #region Classes

        public class TeddsVariables

        {
            public string FloorNumber = "";
            public string FloorName = "";
            public string LoadType = "";
            public string ClassName = "";
            public string SubClass1Name = "";
            public string SubClass2Name = "";
            public string Value = "";
        }

        #endregion

        #region Excel
        bool CreateNewWorksheet()
        {
            filePath = existingFilePath;
            string worksheetName = "Design Criteria";

            if (!File.Exists(filePath))
            {
                try
                {
                    File.Copy(templatefile, filePath);
                    myWorkbookXML = new XLWorkbook(filePath);
                    mySheetXML = myWorkbookXML.Worksheet(worksheetName);
                    return true;
                }

                catch
                {
                    MessageBox.Show(this, "template report copying failed");
                    return false;
                }
            }

            else
            {
                myWorkbookXML = new XLWorkbook(filePath);
                mySheetXML = myWorkbookXML.Worksheet(worksheetName);
                return true;
            }
        }

        bool EstablishExcel()
        {
            filePath = existingFilePath;
            DirectoryInfo dInfo = new DirectoryInfo(myModel.GetInfo().ModelPath + "/Reports/");
            FileInfo[] drawingLists = dInfo.GetFiles();

            foreach (FileInfo file in drawingLists)
            {
                if (file.Name.Contains("_DesignCriteria_"))
                {
                    filePath = file.FullName;
                }

                if (!File.Exists(filePath))
                {
                    MessageBox.Show(this, "Could not locate DrawingList");
                }
            }

            myWorkbookXML = new XLWorkbook(filePath);
            mySheetXML = myWorkbookXML.Worksheet("Design Criteria");
            return true;
        }

        void CloseExcel()

        {
            myWorkbookXML.Save();
            myWorkbookXML.Dispose();

        }
        void PopulateExcel()
        {
            if (EstablishExcel())
            {

            }

        }

        #endregion

        #region Get Existing Tedds File
        void OpenExplorer()

        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                txt_TeddsFile.Text = filePath;
            }

        }
        void OpenFile()
        {
            myModel.GetProjectInfo().SetUserProperty("FILE_PATH", filePath);
            MOIW.Application ap = new MOIW.Application();
            ap.Visible = true;
            MOIW.Document document = ap.Documents.Open(filePath);
        }


        #endregion

      
    }
}
