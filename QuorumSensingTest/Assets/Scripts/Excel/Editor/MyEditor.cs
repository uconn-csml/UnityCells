using UnityEngine;
using UnityEditor;
using System.Collections;
using OfficeOpenXml;
using System.IO;
using System.Collections.Generic;

public class MyEditor : Editor
{

    [MenuItem("MyEditor/test")] 
    static void test()
    {
        Excel xls = new Excel();
        ExcelTable table = new ExcelTable();
        table.TableName = "test";
        string outputPath = Application.dataPath + "/Test/Test2.xlsx";
        xls.Tables.Add(table);
        xls.Tables[0].SetValue(1, 1, "1");
        xls.Tables[0].SetValue(1, 2, "2");
        xls.Tables[0].SetValue(2, 1, "3");
        xls.Tables[0].SetValue(2, 2, "4");
        xls.ShowLog();
        ExcelHelper.SaveExcel(xls, outputPath);
    }

    [MenuItem("MyEditor/LoadXls")] 
    static void LoadXls()
    {
        string path = Application.dataPath + "/Test/Test3.xlsx";
        Excel xls =  ExcelHelper.LoadExcel(path);
        xls.ShowLog();
    }

 
    [MenuItem("MyEditor/WriteXls")] 
    static void WriteXls()
    {
        Excel points = new Excel();
        ExcelTable table2 = new ExcelTable();
        table2.TableName = "test";
        string outputPath = Application.dataPath + "/Test/Test2.xlsx";
        points.Tables.Add(table2);
        for (int i = 1; i < 2001; i++) 
        {
            points.Tables[0].SetValue(i, 1, GlobalVariables.envIL2Sum[i].ToString());

            points.Tables[0].SetValue(i, 3, GlobalVariables.cellIL2Positive[i].ToString());
            points.Tables[0].SetValue(i, 4, GlobalVariables.antigenWeightPositive[i].ToString());
            points.Tables[0].SetValue(i, 5, GlobalVariables.recWeightPositive[i].ToString());
            points.Tables[0].SetValue(i, 6, GlobalVariables.recIL2Positive[i].ToString());

            points.Tables[0].SetValue(i, 8, GlobalVariables.cellIL2Negative[i].ToString());
            points.Tables[0].SetValue(i, 9, GlobalVariables.antigenWeightNegative[i].ToString());
            points.Tables[0].SetValue(i, 10, GlobalVariables.recWeightNegative[i].ToString());
            points.Tables[0].SetValue(i, 11, GlobalVariables.recIL2negative[i].ToString());

            points.Tables[0].SetValue(i, 12, GlobalVariables.envIL2Max[i].ToString());
        }
        //points.Tables[0].SetValue(1, 1, "envSum");
        //points.Tables[0].SetValue(1, 3, "IL2 Pos");
        //points.Tables[0].SetValue(1, 4, "antigen weight");
        //points.Tables[0].SetValue(1, 5, "rec IL2 weight");
        //points.Tables[0].SetValue(1, 6, "rec IL2");
        //points.Tables[0].SetValue(1, 8, "Il2 neg");
        //points.Tables[0].SetValue(1, 9, "antigen weight");
        //points.Tables[0].SetValue(1, 10, "rec IL2 weight");
        //points.Tables[0].SetValue(1, 11, "rec IL2");

        //points.ShowLog();
        ExcelHelper.SaveExcel(points, outputPath);

        //Excel xls = new Excel();
        //ExcelTable table = new ExcelTable();
        //table.TableName = "test";
        //string outputPath = Application.dataPath + "/Test/Test2.xlsx";
        //xls.Tables.Add(table);
        //xls.Tables[0].SetValue(1, 1, Random.Range(1000,100000).ToString());
        //xls.ShowLog();
        //ExcelHelper.SaveExcel(xls, outputPath);
    }


}
